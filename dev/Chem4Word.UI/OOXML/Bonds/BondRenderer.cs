using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Chem4Word.Common;
using Chem4Word.UI.TwoD;
using DocumentFormat.OpenXml;
using Euclid;
using Numbo.Cml;
using Numbo.Cml.Helpers;
using A = DocumentFormat.OpenXml.Drawing;
using Point = System.Windows.Point;
using Wpg = DocumentFormat.OpenXml.Office2010.Word.DrawingGroup;
using Wps = DocumentFormat.OpenXml.Office2010.Word.DrawingShape;

namespace Chem4Word.UI.OOXML.Bonds
{
    public class BondRenderer
    {
        private Rect m_canvasExtents;
        private long m_ooxmlId;
        private List<BondLine> m_BondLines;
        private double m_medianBondLength;
        private Telemetry _telemetry;

        public BondRenderer(Rect canvasExtents, List<BondLine> bondLines, ref long ooxmlId, double medianBondLength, Telemetry telemetry)
        {
            m_canvasExtents = canvasExtents;
            m_ooxmlId = ooxmlId;
            m_BondLines = bondLines;
            m_medianBondLength = medianBondLength;
            _telemetry = telemetry;
        }

        /// <summary>
        /// Creates the lines for a bond
        /// </summary>
        /// <param name="bond"></param>
        public void CreateBondLines(CmlBond bond)
        {
            //Debug.WriteLine("Bond: " + bond.Id);
            string module = "CreateBondLines()";
            //_telemetry.Write(module, "Debug", "Bond: " + bond.ToString());

            IEnumerable<CmlAtom> bondatoms = bond.GetAtoms();
            IEnumerable<Ring> rings = bond.GetRingList();
            int ringCount = 0;
            foreach (Ring r in rings)
            {
                ringCount++;
                break;
            }
            //Debug.WriteLine("  Ring Count: " + ringCount);

            CmlAtom fromAtom = bondatoms.ElementAt(0);
            Point bondStart = new Point((double)fromAtom.X2, (double)fromAtom.Y2);
            //IEnumerable<CmlBond> fromAtomBonds = fromAtom.GetLigandBonds();
            //int fromAtomBondCount = fromAtomBonds.ToArray<CmlBond>().Length;

            CmlAtom toAtom = bondatoms.ElementAt(1);
            Point bondEnd = new Point((double)toAtom.X2, (double)toAtom.Y2);
            //IEnumerable<CmlBond> toAtomBonds = toAtom.GetLigandBonds();
            //int toAtomBondCount = toAtomBonds.ToArray<CmlBond>().Length;

            //Debug.WriteLine("    From : " + fromAtom.ElementType + " [" + fromAtom.Id + "] at " + bondStart.X + ", " + bondStart.Y);
            //Debug.WriteLine("      To : " + toAtom.ElementType + " [" + toAtom.Id + "] at " + bondEnd.X + ", " + bondEnd.Y);

            #region Create Bond Line objects

            CmlBondStereo stereo = bond.GetBondStereo();
            string stereoType = "";
            if (stereo != null)
            {
                stereoType = stereo.GetValue();
            }

            //Debug.WriteLine("  Order: " + bond.Order);
            //Debug.WriteLine("  Stereo: " + stereoType);
            switch (bond.Order)
            {
                case "0":
                    m_BondLines.Add(new BondLine(bondStart, bondEnd, "dotted"));
                    break;

                case "0.5":
                    m_BondLines.Add(new BondLine(bondStart, bondEnd, "dashed"));
                    break;

                case "S":
                    switch (stereoType)
                    {
                        case "":
                            m_BondLines.Add(new BondLine(bondStart, bondEnd, "solid"));
                            break;
                        case "H":
                            m_BondLines.Add(new BondLine(bondStart, bondEnd, "hash"));
                            break;
                        case "W":
                            m_BondLines.Add(new BondLine(bondStart, bondEnd, "wedge"));
                            break;
                        default:
                            m_BondLines.Add(new BondLine(bondStart, bondEnd, "solid"));
                            break;
                    }
                    break;

                case "A":
                        BondLine a = new BondLine(bondStart, bondEnd, "solid");
                        m_BondLines.Add(a);
                        BondLine a1 = a.GetParallel(-BondOffset());
                        Point newStarta = new Point(a1.StartX, a1.StartY);
                        Point newEnda = new Point(a1.EndX, a1.EndY);
                        CoordinateTool.AdjustLineAboutMidpoint(ref newStarta, ref newEnda, -BondOffset());
                        a1 = new BondLine(newStarta, newEnda, "dotted");
                        m_BondLines.Add(a1);
                    break;

                case "D":
                    if (stereoType.Equals("S"))
                    {
                        // Crossed lines
                        BondLine d = new BondLine(bondStart, bondEnd, "solid");
                        BondLine d1 = d.GetParallel(-(BondOffset() / 2));
                        BondLine d2 = d.GetParallel(BondOffset() / 2);
                        m_BondLines.Add(new BondLine(new Point(d1.StartX, d1.StartY), new Point(d2.EndX, d2.EndY), "solid"));
                        m_BondLines.Add(new BondLine(new Point(d2.StartX, d2.StartY), new Point(d1.EndX, d1.EndY), "solid"));
                    }
                    else
                    {
                        bool shifted = false;
                        if (ringCount == 0)
                        {
                            if (fromAtom.ElementType.Equals("C") && toAtom.ElementType.Equals("C"))
                            {
                                shifted = false;
                            }
                            else
                            {
                                shifted = true;
                            }
                        }

                        if (fromAtom.GetLigandBonds().Count == 1 | toAtom.GetLigandBonds().Count == 1)
                        {
                            shifted = true;
                        }

                        if (shifted)
                        {
                            BondLine d = new BondLine(bondStart, bondEnd, "solid");
                            m_BondLines.Add(d.GetParallel(-(BondOffset() / 2)));
                            m_BondLines.Add(d.GetParallel(BondOffset() / 2));
                        }
                        else
                        {
                            BondLine d = new BondLine(bondStart, bondEnd, "solid");
                            m_BondLines.Add(d);
                            BondLine d1 = null;
                            Vector2 vec2 = bond.GetPrettyDoubleBondVector();
                            if (vec2 != null) // Bonds in a ring
                            {
                                Vector dv = new Vector(vec2.X, vec2.Y);
                                dv *= BondOffsetPercent(dv);
                                Point startPoint = new Point(bondStart.X + dv.X, bondStart.Y + dv.Y);
                                Point endPoint = new Point(bondEnd.X + dv.X, bondEnd.Y + dv.Y);
                                CoordinateTool.AdjustLineAboutMidpoint(ref startPoint, ref endPoint, -BondOffset());
                                d1 = new BondLine(startPoint, endPoint, "solid");
                                m_BondLines.Add(d1);
                            }
                            else
                            {
                                d1 = d.GetParallel(-(BondOffset()));
                                Point newStartd = new Point(d1.StartX, d1.StartY);
                                Point newEndd = new Point(d1.EndX, d1.EndY);
                                CoordinateTool.AdjustLineAboutMidpoint(ref newStartd, ref newEndd, -BondOffset());
                                d1 = new BondLine(newStartd, newEndd, "solid");
                                m_BondLines.Add(d1);
                            }
                        }
                    }
                    break;

                case "T":
                    // Draw main bond line
                    BondLine t = new BondLine(bondStart, bondEnd, "solid");
                    m_BondLines.Add(t);
                    m_BondLines.Add(t.GetParallel(BondOffset()));
                    m_BondLines.Add(t.GetParallel(-(BondOffset())));
                    break;

                default:
                    m_BondLines.Add(new BondLine(bondStart, bondEnd, "solid"));
                    break;
            }

            #endregion
        }

        public void DrawWedgeBond(Wpg.WordprocessingGroup wordprocessingGroup1, BondLine bl)
        {
            BondLine wedgeLeft = bl.GetParallel(BondOffset());
            BondLine wedgeRight = bl.GetParallel(-BondOffset());

            List<Point> points = new List<Point>();
            points.Add(new Point(bl.StartX, bl.StartY));
            points.Add(new Point(wedgeLeft.EndX, wedgeLeft.EndY));
            points.Add(new Point(wedgeRight.EndX, wedgeRight.EndY));

            switch (bl.Type)
            {
                case "wedge":
                    DrawFilledTriangle(wordprocessingGroup1, points);
                    break;
                case "hash":
                    //DrawBondLine(wordprocessingGroup1, bl);
                    DrawHashBondLines(wordprocessingGroup1, points);
                    break;
                default:
                    DrawBondLine(wordprocessingGroup1, bl);
                    break;
            }
        }

        public void DrawBondLine(Wpg.WordprocessingGroup wordprocessingGroup1, BondLine bl)
        {
            Point startPoint = new Point(bl.StartX, bl.StartY);
            Point endPoint = new Point(bl.EndX, bl.EndY);
            Rect extents = new Rect(startPoint, endPoint);

            // Move Bond Line Extents and Points to have 0,0 Top Left Reference
            startPoint.Offset(-m_canvasExtents.Left, -m_canvasExtents.Top);
            endPoint.Offset(-m_canvasExtents.Left, -m_canvasExtents.Top);
            extents.Offset(-m_canvasExtents.Left, -m_canvasExtents.Top);

            // Move points into New Bond Line Extents
            startPoint.Offset(-extents.Left, -extents.Top);
            endPoint.Offset(-extents.Left, -extents.Top);

            switch (bl.Type)
            {
                case "solid":
                    DrawSolidLine(wordprocessingGroup1, extents, startPoint, endPoint);
                    break;
                case "dotted":
                    DrawDottedLine(wordprocessingGroup1, extents, startPoint, endPoint);
                    break;
                case "dashed":
                    DrawDashedLine(wordprocessingGroup1, extents, startPoint, endPoint);
                    break;
                default:
                    DrawSolidLine(wordprocessingGroup1, extents, startPoint, endPoint);
                    break;
            }
        }

        private void DrawFilledTriangle(Wpg.WordprocessingGroup wordprocessingGroup1, List<Point> points)
        {
            UInt32Value atomLabelId = UInt32Value.FromUInt32((uint)m_ooxmlId++);
            string atomLabelName = "WedgeBond" + atomLabelId;

            double minX = double.MaxValue;
            double maxX = double.MinValue;
            double minY = double.MaxValue;
            double maxY = double.MinValue;

            foreach (Point p in points)
            {
                maxX = Math.Max(p.X, maxX);
                minX = Math.Min(p.X, minX);
                maxY = Math.Max(p.Y, maxY);
                minY = Math.Min(p.Y, minY);
            }

            Rect extents = new Rect(minX, minY, maxX - minX, maxY - minY);

            // Create modifyable Points
            Point p0 = new Point(points[0].X, points[0].Y);
            Point p1 = new Point(points[1].X, points[1].Y);
            Point p2 = new Point(points[2].X, points[2].Y);

            // Move Points to have 0,0 Top Left Reference within the drawing canvas
            p0.Offset(-m_canvasExtents.Left, -m_canvasExtents.Top);
            p1.Offset(-m_canvasExtents.Left, -m_canvasExtents.Top);
            p2.Offset(-m_canvasExtents.Left, -m_canvasExtents.Top);

            // Move shape's extents to correct place in drawing canvas
            extents.Offset(-m_canvasExtents.Left, -m_canvasExtents.Top);

            // Move points again to put them inside the shape's extents
            p0.Offset(-extents.Left, -extents.Top);
            p1.Offset(-extents.Left, -extents.Top);
            p2.Offset(-extents.Left, -extents.Top);

            Int64Value top = Helper.ScaleCmlForWord(extents.Y);
            Int64Value left = Helper.ScaleCmlForWord(extents.X);
            Int64Value width = Helper.ScaleCmlForWord(extents.Width);
            Int64Value height = Helper.ScaleCmlForWord(extents.Height);

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

            string xCoOrdinate = Helper.ScaleCmlForWord(p0.X).ToString();
            string yCoOrdinate = Helper.ScaleCmlForWord(p0.Y).ToString();

            A.MoveTo moveTo10 = new A.MoveTo();
            A.Point point19 = new A.Point() { X = xCoOrdinate, Y = yCoOrdinate };
            moveTo10.Append(point19);
            path10.Append(moveTo10);

            xCoOrdinate = Helper.ScaleCmlForWord(p1.X).ToString();
            yCoOrdinate = Helper.ScaleCmlForWord(p1.Y).ToString();

            A.LineTo lineTo10 = new A.LineTo();
            A.Point point20 = new A.Point() { X = xCoOrdinate, Y = yCoOrdinate };
            lineTo10.Append(point20);
            path10.Append(lineTo10);

            xCoOrdinate = Helper.ScaleCmlForWord(p2.X).ToString();
            yCoOrdinate = Helper.ScaleCmlForWord(p2.Y).ToString();

            A.LineTo lineTo19 = new A.LineTo();
            A.Point point29 = new A.Point() { X = xCoOrdinate, Y = yCoOrdinate };
            lineTo19.Append(point29);
            path10.Append(lineTo19);
            A.CloseShapePath closeShapePath1 = new A.CloseShapePath();
            path10.Append(closeShapePath1);

            pathList10.Append(path10);

            customGeometry10.Append(adjustValueList10);
            customGeometry10.Append(rectangle10);
            customGeometry10.Append(pathList10);

            A.SolidFill solidFill10 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex() { Val = "000000" };
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

        private void DrawHashBondLines(Wpg.WordprocessingGroup wordprocessingGroup1, List<Point> points)
        {
            // Create modifyable Points
            Point p0 = new Point(points[0].X, points[0].Y);
            Point p1 = new Point(points[1].X, points[1].Y);
            Point p2 = new Point(points[2].X, points[2].Y);

            // Draw Tail Line
            BondLine blTail = new BondLine(points[1], points[2], "solid");
            DrawBondLine(wordprocessingGroup1, blTail);

            // Prep for intermediate lines
            Point midPoint = CoordinateTool.GetMidPoint(points[1], points[2]);
            Vector v = points[0] - midPoint;
            double ticks = v.Length / 2;
            v = v / ticks;

            BondLine blNext;

            Point outIntersectP1 = new Point();
            Point outCloseP1 = new Point();
            Point outCloseP2 = new Point();
            Point outIntersectP2 = new Point();

            Point leftTailEnd = p1;
            Point rightTailEnd = p2;

            bool linesIntersect = false;
            bool segmentsIntersect = false;
            // Draw itermediate lines
            for (int i = 1; i <= (int)ticks; i++ )
            {
                p1.Offset(v.X, v.Y);
                p2.Offset(v.X, v.Y);
                CoordinateTool.FindIntersection(leftTailEnd, p0, p1, p2,
                    out linesIntersect, out segmentsIntersect, out outIntersectP1, out outCloseP1, out outCloseP2);
                CoordinateTool.FindIntersection(rightTailEnd, p0, p1, p2,
                    out linesIntersect, out segmentsIntersect, out outIntersectP2, out outCloseP1, out outCloseP2);
                blNext = new BondLine(outIntersectP1, outIntersectP2, "solid");
                DrawBondLine(wordprocessingGroup1, blNext);
            }

            // Draw small line for nose
            Point n1 = new Point(points[0].X - 0.025, points[0].Y - 0.025);
            Point n2 = new Point(points[0].X + 0.025, points[0].Y + 0.025);
            //BondLine blNose = new BondLine(points[0], points[0], "solid");
            BondLine blNose = new BondLine(n1, n2, "solid");
            DrawBondLine(wordprocessingGroup1, blNose);
        }

        private void DrawSolidLine(Wpg.WordprocessingGroup wordprocessingGroup1, Rect extents, Point bondStart, Point bondEnd)
        {
            UInt32Value bondLineId = UInt32Value.FromUInt32((uint)m_ooxmlId++);
            string bondLineName = "BondLine" + bondLineId;

            Int64Value width = Helper.ScaleCmlForWord(extents.Width);
            Int64Value height = Helper.ScaleCmlForWord(extents.Height);
            Int64Value top = Helper.ScaleCmlForWord(extents.Top);
            Int64Value left = Helper.ScaleCmlForWord(extents.Left);

            Wps.WordprocessingShape wordprocessingShape1 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Wps.NonVisualDrawingProperties() { Id = bondLineId, Name = bondLineName };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties1 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties1 = new Wps.ShapeProperties();

            A.Transform2D transform2D1 = new A.Transform2D();
            A.Offset offset2 = new A.Offset() { X = left, Y = top };
            A.Extents extents2 = new A.Extents() { Cx = width, Cy = height };

            transform2D1.Append(offset2);
            transform2D1.Append(extents2);

            A.CustomGeometry customGeometry1 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();
            A.Rectangle rectangle1 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList1 = new A.PathList();

            A.Path path1 = new A.Path() { Width = width, Height = height };

            A.MoveTo moveTo1 = new A.MoveTo();
            A.Point point1 = new A.Point() { X = Helper.ScaleCmlForWord(bondStart.X).ToString(), Y = Helper.ScaleCmlForWord(bondStart.Y).ToString() };

            moveTo1.Append(point1);

            A.LineTo lineTo1 = new A.LineTo();
            A.Point point2 = new A.Point() { X = Helper.ScaleCmlForWord(bondEnd.X).ToString(), Y = Helper.ScaleCmlForWord(bondEnd.Y).ToString() };

            lineTo1.Append(point2);

            path1.Append(moveTo1);
            path1.Append(lineTo1);

            pathList1.Append(path1);

            customGeometry1.Append(adjustValueList1);
            customGeometry1.Append(rectangle1);
            customGeometry1.Append(pathList1);

            A.Outline outline1 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill1 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha1 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex1.Append(alpha1);

            solidFill1.Append(rgbColorModelHex1);

            outline1.Append(solidFill1);

            shapeProperties1.Append(transform2D1);
            shapeProperties1.Append(customGeometry1);
            shapeProperties1.Append(outline1);

            Wps.ShapeStyle shapeStyle1 = new Wps.ShapeStyle();
            A.LineReference lineReference1 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference1 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference1 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference1 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle1.Append(lineReference1);
            shapeStyle1.Append(fillReference1);
            shapeStyle1.Append(effectReference1);
            shapeStyle1.Append(fontReference1);
            Wps.TextBodyProperties textBodyProperties1 = new Wps.TextBodyProperties();

            wordprocessingShape1.Append(nonVisualDrawingProperties1);
            wordprocessingShape1.Append(nonVisualDrawingShapeProperties1);
            wordprocessingShape1.Append(shapeProperties1);
            wordprocessingShape1.Append(shapeStyle1);
            wordprocessingShape1.Append(textBodyProperties1);

            wordprocessingGroup1.Append(wordprocessingShape1);
        }

        private void DrawDottedLine(Wpg.WordprocessingGroup wordprocessingGroup1, Rect extents, Point bondStart, Point bondEnd)
        {
            UInt32Value bondLineId = UInt32Value.FromUInt32((uint)m_ooxmlId++);
            string bondLineName = "DottedBondLine" + bondLineId;

            Int64Value width = Helper.ScaleCmlForWord(extents.Width);
            Int64Value height = Helper.ScaleCmlForWord(extents.Height);
            Int64Value top = Helper.ScaleCmlForWord(extents.Top);
            Int64Value left = Helper.ScaleCmlForWord(extents.Left);

            Wps.WordprocessingShape wordprocessingShape1 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Wps.NonVisualDrawingProperties() { Id = bondLineId, Name = bondLineName };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties1 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties1 = new Wps.ShapeProperties();

            A.Transform2D transform2D1 = new A.Transform2D();
            A.Offset offset2 = new A.Offset() { X = left, Y = top };
            A.Extents extents2 = new A.Extents() { Cx = width, Cy = height };

            transform2D1.Append(offset2);
            transform2D1.Append(extents2);

            A.CustomGeometry customGeometry1 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();
            A.Rectangle rectangle1 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList1 = new A.PathList();

            A.Path path1 = new A.Path() { Width = width, Height = height };

            A.MoveTo moveTo1 = new A.MoveTo();
            A.Point point1 = new A.Point() { X = Helper.ScaleCmlForWord(bondStart.X).ToString(), Y = Helper.ScaleCmlForWord(bondStart.Y).ToString() };

            moveTo1.Append(point1);

            A.LineTo lineTo1 = new A.LineTo();
            A.Point point2 = new A.Point() { X = Helper.ScaleCmlForWord(bondEnd.X).ToString(), Y = Helper.ScaleCmlForWord(bondEnd.Y).ToString() };

            lineTo1.Append(point2);

            path1.Append(moveTo1);
            path1.Append(lineTo1);

            pathList1.Append(path1);

            customGeometry1.Append(adjustValueList1);
            customGeometry1.Append(rectangle1);
            customGeometry1.Append(pathList1);

            A.Outline outline1 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill1 = new A.SolidFill();
            A.PresetDash presetDash1 = new A.PresetDash() { Val = A.PresetLineDashValues.Dash };

            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha1 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex1.Append(alpha1);

            solidFill1.Append(rgbColorModelHex1);

            outline1.Append(solidFill1);
            outline1.Append(presetDash1);

            shapeProperties1.Append(transform2D1);
            shapeProperties1.Append(customGeometry1);
            shapeProperties1.Append(outline1);

            Wps.ShapeStyle shapeStyle1 = new Wps.ShapeStyle();
            A.LineReference lineReference1 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference1 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference1 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference1 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle1.Append(lineReference1);
            shapeStyle1.Append(fillReference1);
            shapeStyle1.Append(effectReference1);
            shapeStyle1.Append(fontReference1);
            Wps.TextBodyProperties textBodyProperties1 = new Wps.TextBodyProperties();

            wordprocessingShape1.Append(nonVisualDrawingProperties1);
            wordprocessingShape1.Append(nonVisualDrawingShapeProperties1);
            wordprocessingShape1.Append(shapeProperties1);
            wordprocessingShape1.Append(shapeStyle1);
            wordprocessingShape1.Append(textBodyProperties1);

            wordprocessingGroup1.Append(wordprocessingShape1);
        }

        private void DrawDashedLine(Wpg.WordprocessingGroup wordprocessingGroup1, Rect extents, Point bondStart, Point bondEnd)
        {
            UInt32Value bondLineId = UInt32Value.FromUInt32((uint)m_ooxmlId++);
            string bondLineName = "DashedBondLine" + bondLineId;

            Int64Value width = Helper.ScaleCmlForWord(extents.Width);
            Int64Value height = Helper.ScaleCmlForWord(extents.Height);
            Int64Value top = Helper.ScaleCmlForWord(extents.Top);
            Int64Value left = Helper.ScaleCmlForWord(extents.Left);

            Wps.WordprocessingShape wordprocessingShape1 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Wps.NonVisualDrawingProperties() { Id = bondLineId, Name = bondLineName };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties1 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties1 = new Wps.ShapeProperties();

            A.Transform2D transform2D1 = new A.Transform2D();
            A.Offset offset2 = new A.Offset() { X = left, Y = top };
            A.Extents extents2 = new A.Extents() { Cx = width, Cy = height };

            transform2D1.Append(offset2);
            transform2D1.Append(extents2);

            A.CustomGeometry customGeometry1 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();
            A.Rectangle rectangle1 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList1 = new A.PathList();

            A.Path path1 = new A.Path() { Width = width, Height = height };

            A.MoveTo moveTo1 = new A.MoveTo();
            A.Point point1 = new A.Point() { X = Helper.ScaleCmlForWord(bondStart.X).ToString(), Y = Helper.ScaleCmlForWord(bondStart.Y).ToString() };

            moveTo1.Append(point1);

            A.LineTo lineTo1 = new A.LineTo();
            A.Point point2 = new A.Point() { X = Helper.ScaleCmlForWord(bondEnd.X).ToString(), Y = Helper.ScaleCmlForWord(bondEnd.Y).ToString() };

            lineTo1.Append(point2);

            path1.Append(moveTo1);
            path1.Append(lineTo1);

            pathList1.Append(path1);

            customGeometry1.Append(adjustValueList1);
            customGeometry1.Append(rectangle1);
            customGeometry1.Append(pathList1);

            A.Outline outline1 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill1 = new A.SolidFill();
            A.PresetDash presetDash1 = new A.PresetDash() { Val = A.PresetLineDashValues.LargeDash };

            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha1 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex1.Append(alpha1);

            solidFill1.Append(rgbColorModelHex1);

            outline1.Append(solidFill1);
            outline1.Append(presetDash1);

            shapeProperties1.Append(transform2D1);
            shapeProperties1.Append(customGeometry1);
            shapeProperties1.Append(outline1);

            Wps.ShapeStyle shapeStyle1 = new Wps.ShapeStyle();
            A.LineReference lineReference1 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference1 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference1 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference1 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle1.Append(lineReference1);
            shapeStyle1.Append(fillReference1);
            shapeStyle1.Append(effectReference1);
            shapeStyle1.Append(fontReference1);
            Wps.TextBodyProperties textBodyProperties1 = new Wps.TextBodyProperties();

            wordprocessingShape1.Append(nonVisualDrawingProperties1);
            wordprocessingShape1.Append(nonVisualDrawingShapeProperties1);
            wordprocessingShape1.Append(shapeProperties1);
            wordprocessingShape1.Append(shapeStyle1);
            wordprocessingShape1.Append(textBodyProperties1);

            wordprocessingGroup1.Append(wordprocessingShape1);
        }

        private double BondOffset()
        {
            return (m_medianBondLength * Helper.MULTIPLE_BOND_OFFSET_PERCENTAGE);
        }

        private double BondOffsetPercent(Vector v)
        {
            double standardOffset = m_medianBondLength * Helper.MULTIPLE_BOND_OFFSET_PERCENTAGE;
            double ratio = standardOffset / v.Length;
            return ratio;
        }

    }
}