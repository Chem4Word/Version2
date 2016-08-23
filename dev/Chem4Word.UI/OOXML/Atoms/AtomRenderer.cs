using System;
using System.Collections.Generic;
using System.Windows;
using Chem4Word.Common;
using Chem4Word.UI.TwoD;
using DocumentFormat.OpenXml;
using Numbo.Cml;
using Numbo.Cml.Helpers;
using Numbo.Coa;
using A = DocumentFormat.OpenXml.Drawing;
using Point = System.Windows.Point;
using Wpg = DocumentFormat.OpenXml.Office2010.Word.DrawingGroup;
using Wps = DocumentFormat.OpenXml.Office2010.Word.DrawingShape;

namespace Chem4Word.UI.OOXML.Atoms
{
    public class AtomRenderer
    {
        private Rect m_canvasExtents;
        private long m_ooxmlId;
        private OoXmlCharacterSet m_charset;
        private List<AtomLabelCharacter> m_AtomLabelCharacters;
        private Telemetry _telemetry;

        public AtomRenderer(Rect canvasExtents, List<AtomLabelCharacter> atomLabelCharacters, ref long ooxmlId, Telemetry telemetry)
        {
            m_charset = new OoXmlCharacterSet();
            m_canvasExtents = canvasExtents;
            m_ooxmlId = ooxmlId;
            m_AtomLabelCharacters = atomLabelCharacters;
            _telemetry = telemetry;
        }

        public void CreateAtomLabelCharacters(CmlAtom atom, OoXmlOptions options)
        {
            //string module = "CreateAtomLabelCharacters()";

            //Debug.WriteLine("Atom: " + atom.Id + " is " + atom.ElementType);
            //_telemetry.Write(module, "Debug", "Atom: " + atom.Id + " is " + atom.ElementType);

            Point p1;
            Point p2;
            double angle1 = 0;
            double angle2 = 180;

            Point atomCentre = new Point((double)atom.X2, (double)atom.Y2);
            string atomLabel = atom.ElementType;
            //Debug Code
            //atomLabel = "H" + "abcdefHghijklmnopqrstuvwxHCyz" + "H";
            //atomLabel = "ABCHDEFGHIJKLMNOHPQRSHTUVWXYZ";

            // Get Charge for use later on
            int iCharge = 0;
            if (atom.FormalCharge != null)
            {
                iCharge = atom.FormalCharge.Value;
            }
            int iAbsCharge = Math.Abs(iCharge);

            // Get Implicit Hydrogen Count for use later on
            int implicitHCount = ChemicalIntelligence.GetImplicitHydrogenCount(atom);

            Point cursorPosition = new Point((double)atom.X2, (double)atom.Y2);
            Point chargeCursorPosition = new Point((double)atom.X2, (double)atom.Y2);
            Point thisCharacterPosition;

            double lastOffset = 0;

            //Debug.WriteLine("  X: " + atom.X2 + " Y: " + atom.Y2 + " Implicit H Count: " + implicitHCount);

            ICollection<CmlBond> bonds = atom.GetLigandBonds();
            IEnumerable<Ring> rings = atom.GetRingList();
            int ringCount = 0;
            foreach (Ring r in rings)
            {
                ringCount++;
            }
            //Debug.WriteLine("  Bond Count: " + bonds.Count);
            //Debug.WriteLine("  Ring Count: " + ringCount);

            List<CmlAtom> connectedAtoms = new List<CmlAtom>();
            foreach (CmlBond bond in bonds)
            {
                CmlAtom other = bond.GetAtomAtOtherEnd(atom);
                connectedAtoms.Add(other);
            }

            #region Decide if atom label is to be displayed

            bool showLabel = true;
            if (atomLabel.Equals("C"))
            {
                if (ringCount > 0 || bonds.Count > 1)
                {
                    showLabel = false;
                }
                if (bonds.Count == 2)
                {
                    p1 = new Point((double)atom.X2, (double)atom.Y2);
                    p2 = new Point((double)connectedAtoms[0].X2, (double)connectedAtoms[0].Y2);
                    Vector v1 = p1 - p2;
                    p2 = new Point((double)connectedAtoms[1].X2, (double)connectedAtoms[1].Y2);
                    Vector v2 = p1 - p2;
                    angle1 = CoordinateTool.AngleBetween(v1, v2);
                    if (180 - (Math.Abs(angle1)) < 8)
                    {
                        showLabel = true;
                    }
                }
                // Force on if atom has charge
                if (iAbsCharge > 0)
                {
                    showLabel = true;
                }
            }

            #endregion

            if (showLabel)
            {
                #region Set Up Atom Colours

                string atomColour = "000000";
                if (options.ColouredAtoms)
                {
                    switch (atomLabel)
                    {
                        case "C":
                            atomColour = "808080";
                            break;
                        case "H":
                            atomColour = "808080";
                            break;
                        case "N":
                            atomColour = "0000ff";
                            break;
                        case "O":
                            atomColour = "ff0000";
                            break;
                        case "F":
                            atomColour = "ff8080";
                            break;
                        case "P":
                            atomColour = "ff8000";
                            break;
                        case "S":
                            atomColour = "808000";
                            break;
                        case "Cl":
                            atomColour = "00c000";
                            break;
                        case "Br":
                            atomColour = "c04000";
                            break;
                        default:
                            atomColour = "000000";
                            break;
                    }
                }

                #endregion

                #region Step 1 - Measure Bounding Box for all Characters of label

                double xMin = cursorPosition.X;
                double yMin = cursorPosition.Y;
                double xMax = cursorPosition.X;
                double yMax = cursorPosition.Y;

                for (int idx = 0; idx < atomLabel.Length; idx++)
                {
                    char chr = atomLabel[idx];
                    OoXmlCharacter c = m_charset.GetOoXmlCharacter(chr);
                    if (c != null)
                    {
                        thisCharacterPosition = new Point(cursorPosition.X, cursorPosition.Y);

                        // Handle Descenders
                        thisCharacterPosition.Offset(0, Helper.ScaleTtfToCml(c.GlyphOrigin.Y + c.BlackBoxY));
                        // Apply tweaks
                        if (c.OffsetY != 0)
                        {
                            thisCharacterPosition.Offset(0, Helper.ScaleTtfToCml(c.OffsetY));
                        }

                        xMax = Math.Max(xMax, cursorPosition.X + Helper.ScaleTtfToCml(c.BlackBoxX));
                        yMax = Math.Max(yMax, cursorPosition.Y + Helper.ScaleTtfToCml(c.BlackBoxY));

                        // Uncomment the following to disable offsetting for terminal atoms
                        //if (bonds.Count == 1)
                        //{
                        //    break;
                        //}

                        if (idx < atomLabel.Length - 1)
                        {
                            // Move to next Character position
                            cursorPosition.Offset(Helper.ScaleTtfToCml(c.CellIncX), 0);
                        }
                    }
                }

                #endregion

                #region Step 2 - Reset Cursor such that the text is centered about the atom's co-ordinates

                double width = xMax - xMin;
                double height = yMax - yMin;
                cursorPosition = new Point(xMin - (width / 2), yMin - (height / 2));

                #endregion

                #region Step 3 - Place the characters

                foreach (char chr in atomLabel)
                {
                    OoXmlCharacter c = m_charset.GetOoXmlCharacter(chr);
                    if (c != null)
                    {
                        thisCharacterPosition = new Point(cursorPosition.X, cursorPosition.Y);

                        // Handle Descenders
                        thisCharacterPosition.Offset(0, Helper.ScaleTtfToCml(c.GlyphOrigin.Y + c.BlackBoxY));
                        // Apply tweaks
                        if (c.OffsetY != 0)
                        {
                            thisCharacterPosition.Offset(0, Helper.ScaleTtfToCml(c.OffsetY));
                        }
                        AtomLabelCharacter alc = new AtomLabelCharacter(thisCharacterPosition, c, atomColour, chr);
                        m_AtomLabelCharacters.Add(alc);

                        // Move to next Character position
                        lastOffset = Helper.ScaleTtfToCml(c.CellIncX);
                        cursorPosition.Offset(Helper.ScaleTtfToCml(c.CellIncX), 0);
                        chargeCursorPosition = new Point(cursorPosition.X, cursorPosition.Y);
                    }
                }

                #endregion

                #region Step 4 - Add Implicit H if required

                if (options.ShowHydrogens && implicitHCount > 0)
                {

                    OoXmlCharacter hydrogenCharacter = m_charset.GetOoXmlCharacter('H');
                    string numbers = "012345";
                    OoXmlCharacter implicitValueCharacter = m_charset.GetOoXmlCharacter(numbers[implicitHCount]);

                    #region Determine NESW

                    string nesw = "E";
                    switch (bonds.Count)
                    {
                        case 1:
                            p1 = new Point((double)atom.X2, (double)atom.Y2);
                            p2 = new Point((double)connectedAtoms[0].X2, (double)connectedAtoms[0].Y2);
                            angle2 = CoordinateTool.AngleBetween(p1, p2);
                            break;
                        case 2:
                            if (ringCount > 0)
                            {
                                // Find outgoing angle
                                p1 = new Point((double)atom.X2, (double)atom.Y2);
                                p2 = CoordinateTool.GetMidPoint(
                                    new Point((double)connectedAtoms[0].X2, (double)connectedAtoms[0].Y2),
                                    new Point((double)connectedAtoms[1].X2, (double)connectedAtoms[1].Y2));
                                angle2 = CoordinateTool.AngleBetween(p1, p2);
                            }
                            else
                            {
                                if (Math.Abs(angle1) > 98)
                                {
                                    // Find average bond angle
                                    p1 = new Point((double)atom.X2, (double)atom.Y2);
                                    p2 = new Point((double)connectedAtoms[0].X2, (double)connectedAtoms[0].Y2);
                                    double angle2a = CoordinateTool.AngleBetween(p1, p2);
                                    p2 = new Point((double)connectedAtoms[1].X2, (double)connectedAtoms[1].Y2);
                                    double angle2b = CoordinateTool.AngleBetween(p2, p1);
                                    angle2 = (angle2a + angle2b) / 2;
                                }
                                else
                                {
                                    // Find outgoing angle
                                    p1 = new Point((double)atom.X2, (double)atom.Y2);
                                    p2 = CoordinateTool.GetMidPoint(
                                        new Point((double)connectedAtoms[0].X2, (double)connectedAtoms[0].Y2),
                                        new Point((double)connectedAtoms[1].X2, (double)connectedAtoms[1].Y2));
                                    angle2 = CoordinateTool.AngleBetween(p1, p2);
                                }
                            }
                            break;
                        default:
                            break;
                    }

                    //Debug.WriteLine("Angle1: " + angle1);
                    //Debug.WriteLine("Angle2: " + angle2);

                    if (Math.Abs(angle1) > 98)
                    {
                        if (Math.Abs(angle2) < 45)
                        {
                            if (angle1 >= 0)
                            {
                                nesw = "N";
                            }
                            else
                            {
                                nesw = "S";
                            }
                        }
                        else
                        {
                            if (angle1 >= 0)
                            {
                                nesw = "E";
                            }
                            else
                            {
                                nesw = "W";
                            }
                        }
                    }
                    else
                    {
                        if (angle2 > 45 && angle2 < 135)
                        {
                            nesw = "N";
                        }
                        if (angle2 > -45 && angle2 < 45)
                        {
                            nesw = "W";
                        }
                        if (angle2 > -135 && angle2 < -45)
                        {
                            nesw = "S";
                        }
                    }

                    #endregion

                    #region Add H
                    if (hydrogenCharacter != null)
                    {
                        switch (nesw)
                        {
                            case "N":
                                if (bonds.Count > 1)
                                {
                                    if (iCharge == 0)
                                    {
                                        cursorPosition.Offset(-lastOffset, Helper.ScaleTtfToCml(-hydrogenCharacter.BlackBoxY) - Helper.CHARACTER_CLIPPING_MARGIN);
                                    }
                                    else
                                    {
                                        cursorPosition.Offset(-lastOffset, Helper.ScaleTtfToCml(-hydrogenCharacter.BlackBoxY) - Helper.CHARACTER_CLIPPING_MARGIN);
                                        if (implicitHCount > 1)
                                        {
                                            cursorPosition.Offset(0, Helper.ScaleTtfToCml(-implicitValueCharacter.BlackBoxY * Helper.SUBSCRIPT_SCALE_FACTOR / 2) - Helper.CHARACTER_CLIPPING_MARGIN);
                                        }
                                    }
                                }
                                break;
                            case "E":
                                // Leave as is
                                break;
                            case "S":
                                if (bonds.Count > 1)
                                {
                                    cursorPosition.Offset(-lastOffset, Helper.ScaleTtfToCml(hydrogenCharacter.BlackBoxY) + Helper.CHARACTER_CLIPPING_MARGIN);
                                }
                                break;
                            case "W":
                                if (implicitHCount == 1)
                                {
                                    cursorPosition.Offset(Helper.ScaleTtfToCml(-(hydrogenCharacter.CellIncX * 2 )), 0);
                                }
                                else
                                {
                                    cursorPosition.Offset(Helper.ScaleTtfToCml(-(hydrogenCharacter.CellIncX * 2.5)), 0);
                                }
                                break;
                        }

                        thisCharacterPosition = new Point(cursorPosition.X, cursorPosition.Y);

                        // Handle Descenders
                        thisCharacterPosition.Offset(0, Helper.ScaleTtfToCml(hydrogenCharacter.GlyphOrigin.Y + hydrogenCharacter.BlackBoxY));
                        if (hydrogenCharacter.OffsetY != 0)
                        {
                            thisCharacterPosition.Offset(0, Helper.ScaleTtfToCml(hydrogenCharacter.OffsetY));
                        }
                        AtomLabelCharacter alc = new AtomLabelCharacter(thisCharacterPosition, hydrogenCharacter, atomColour, 'H');
                        m_AtomLabelCharacters.Add(alc);

                        // Move to next Character position
                        cursorPosition.Offset(Helper.ScaleTtfToCml(hydrogenCharacter.CellIncX), 0);
                        if (nesw.Equals("E"))
                        {
                            chargeCursorPosition = new Point(cursorPosition.X, cursorPosition.Y);
                        }
                    }
                    #endregion

                    #region Add number
                    if (implicitHCount > 1)
                    {
                        if (implicitValueCharacter != null)
                        {
                            thisCharacterPosition = new Point(cursorPosition.X, cursorPosition.Y);

                            // Handle Descenders
                            thisCharacterPosition.Offset(0, Helper.ScaleTtfToCml(implicitValueCharacter.GlyphOrigin.Y + implicitValueCharacter.BlackBoxY));
                            if (implicitValueCharacter.OffsetY != 0)
                            {
                                thisCharacterPosition.Offset(0, Helper.ScaleTtfToCml(implicitValueCharacter.OffsetY) * Helper.SUBSCRIPT_SCALE_FACTOR);
                            }

                            // Drop the subscript Character
                            thisCharacterPosition.Offset(0, Helper.ScaleTtfToCml(hydrogenCharacter.BlackBoxX * Helper.SUBSCRIPT_DROP_FACTOR));

                            AtomLabelCharacter alc = new AtomLabelCharacter(thisCharacterPosition, implicitValueCharacter, atomColour, numbers[implicitHCount]);
                            alc.IsSubScript = true;
                            m_AtomLabelCharacters.Add(alc);

                            // Move to next Character position
                            cursorPosition.Offset(Helper.ScaleTtfToCml(implicitValueCharacter.CellIncX) * Helper.SUBSCRIPT_SCALE_FACTOR, 0);
                        }
                    }
                    #endregion

                }

                #endregion

                #region Step 5 - Add Charge if required

                if (iCharge != 0)
                {
                    char sign = '.';
                    OoXmlCharacter chargeSignCharacter = null;
                    if (iCharge >= 1)
                    {
                        sign = '+';
                        chargeSignCharacter = m_charset.GetOoXmlCharacter('+');
                    }
                    else if (iCharge <= 1)
                    {
                        sign = '-';
                        chargeSignCharacter = m_charset.GetOoXmlCharacter('-');
                    }

                    if (iAbsCharge > 1)
                    {
                        string digits = iAbsCharge.ToString();
                        // Insert digits
                        foreach (char chr in digits)
                        {
                            OoXmlCharacter chargeValueCharacter = m_charset.GetOoXmlCharacter(chr);

                            thisCharacterPosition = new Point(chargeCursorPosition.X, chargeCursorPosition.Y);

                            // Raise the superscript Character
                            thisCharacterPosition.Offset(0, -Helper.ScaleTtfToCml(chargeValueCharacter.BlackBoxX * Helper.SUPERSCRIPT_RAISE_FACTOR));

                            AtomLabelCharacter alcc = new AtomLabelCharacter(thisCharacterPosition,
                                chargeValueCharacter, atomColour, chr);
                            alcc.IsSubScript = true;
                            m_AtomLabelCharacters.Add(alcc);

                            // Move to next Character position
                            chargeCursorPosition.Offset(Helper.ScaleTtfToCml(chargeValueCharacter.CellIncX) * Helper.SUBSCRIPT_SCALE_FACTOR, 0);
                        }
                    }

                    // Insert sign
                    thisCharacterPosition = new Point(chargeCursorPosition.X, chargeCursorPosition.Y);

                    // Raise the superscript Character
                    thisCharacterPosition.Offset(0, -Helper.ScaleTtfToCml(chargeSignCharacter.BlackBoxX * Helper.SUPERSCRIPT_RAISE_FACTOR));

                    AtomLabelCharacter alcs = new AtomLabelCharacter(thisCharacterPosition,
                        chargeSignCharacter, atomColour, sign);
                    alcs.IsSubScript = true;
                    m_AtomLabelCharacters.Add(alcs);
                }

                #endregion
            }
        }

        public void DrawAtomLabelCharacter(Wpg.WordprocessingGroup wordprocessingGroup1, AtomLabelCharacter alc)
        {
            Point characterPosition = new Point(alc.Position.X, alc.Position.Y);
            characterPosition.Offset(-m_canvasExtents.Left, -m_canvasExtents.Top);

            UInt32Value atomLabelId = UInt32Value.FromUInt32((uint)m_ooxmlId++);
            string atomLabelName = "AtomLabel" + atomLabelId;

            Int64Value width = Helper.ScaleTtfForWord(alc.Character.BlackBoxX);
            Int64Value height = Helper.ScaleTtfForWord(alc.Character.BlackBoxY);
            if (alc.IsSubScript)
            {
                width = Helper.ScaleTtfSubScriptForWord(alc.Character.BlackBoxX);
                height = Helper.ScaleTtfSubScriptForWord(alc.Character.BlackBoxY);
            }
            Int64Value top = Helper.ScaleCmlForWord(characterPosition.Y);
            Int64Value left = Helper.ScaleCmlForWord(characterPosition.X);

            Wps.WordprocessingShape wordprocessingShape10 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties10 = new Wps.NonVisualDrawingProperties() { Id = atomLabelId, Name = atomLabelName };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties10 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties10 = new Wps.ShapeProperties();

            A.Transform2D transform2D10 = new A.Transform2D();
            A.Offset offset11 = new A.Offset() { X = left, Y = top };
            A.Extents extents11 = new A.Extents() { Cx = width, Cy = height };

            transform2D10.Append(offset11);
            transform2D10.Append(extents11);

            A.CustomGeometry customGeometry10 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList10 = new A.AdjustValueList();
            A.Rectangle rectangle10 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList10 = new A.PathList();

            A.Path path10 = new A.Path() { Width = width, Height = height };

            foreach (OoXmlPolygon poly in alc.Character.OoXmlPolygons)
            {
                int idx = 0;
                int last = poly.TTFPoints.Count - 1;
                foreach (Point p in poly.TTFPoints)
                {
                    string xCoOrdinate = Helper.ScaleTtfForWord(p.X - alc.Character.GlyphOrigin.X).ToString();
                    string yCoOrdinate = Helper.ScaleTtfForWord(p.Y - alc.Character.GlyphOrigin.Y).ToString();
                    if (alc.IsSubScript)
                    {
                        xCoOrdinate = Helper.ScaleTtfSubScriptForWord(p.X - alc.Character.GlyphOrigin.X).ToString();
                        yCoOrdinate = Helper.ScaleTtfSubScriptForWord(p.Y - alc.Character.GlyphOrigin.Y).ToString();
                    }

                    // First Point - MoveTo
                    if (idx == 0)
                    {
                        A.MoveTo moveTo10 = new A.MoveTo();
                        A.Point point19 = new A.Point() { X = xCoOrdinate, Y = yCoOrdinate };
                        moveTo10.Append(point19);
                        path10.Append(moveTo10);
                    }
                    // Last Point - LineTo and CloseShapePath
                    else if (idx == last)
                    {
                        A.LineTo lineTo19 = new A.LineTo();
                        A.Point point29 = new A.Point() { X = xCoOrdinate, Y = yCoOrdinate };
                        lineTo19.Append(point29);
                        path10.Append(lineTo19);
                        A.CloseShapePath closeShapePath1 = new A.CloseShapePath();
                        path10.Append(closeShapePath1);
                    }
                    // Other Points - LineTo
                    else
                    {
                        A.LineTo lineTo10 = new A.LineTo();
                        A.Point point20 = new A.Point() { X = xCoOrdinate, Y = yCoOrdinate };
                        lineTo10.Append(point20);
                        path10.Append(lineTo10);
                    }
                    idx++;
                }
            }

            pathList10.Append(path10);

            customGeometry10.Append(adjustValueList10);
            customGeometry10.Append(rectangle10);
            customGeometry10.Append(pathList10);

            A.SolidFill solidFill10 = new A.SolidFill();

            // Set Colour
            A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex() { Val = alc.Colour };
            A.Alpha alpha10 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex10.Append(alpha10);

            solidFill10.Append(rgbColorModelHex10);

            shapeProperties10.Append(transform2D10);
            shapeProperties10.Append(customGeometry10);
            shapeProperties10.Append(solidFill10);

            Wps.ShapeStyle shapeStyle10 = new Wps.ShapeStyle();
            A.LineReference lineReference10 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference10 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference10 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference10 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle10.Append(lineReference10);
            shapeStyle10.Append(fillReference10);
            shapeStyle10.Append(effectReference10);
            shapeStyle10.Append(fontReference10);
            Wps.TextBodyProperties textBodyProperties10 = new Wps.TextBodyProperties();

            wordprocessingShape10.Append(nonVisualDrawingProperties10);
            wordprocessingShape10.Append(nonVisualDrawingShapeProperties10);
            wordprocessingShape10.Append(shapeProperties10);
            wordprocessingShape10.Append(shapeStyle10);
            wordprocessingShape10.Append(textBodyProperties10);

            wordprocessingGroup1.Append(wordprocessingShape10);
        }

    }
}