using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using Chem4Word.UI.OOXML.Atoms;
using Chem4Word.UI.OOXML.Bonds;
using Chem4Word.UI.TwoD;
using Chem4Word.UI.UIControls;
using DocumentFormat.OpenXml;
using Numbo.Cml;
using Numbo.Cml.Helpers;
using A = DocumentFormat.OpenXml.Drawing;
using Point = System.Windows.Point;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Wpg = DocumentFormat.OpenXml.Office2010.Word.DrawingGroup;

namespace Chem4Word.UI.OOXML
{
    public class OoXmlMolecule
    {
        private long m_ooxmlId = 1;

        private CmlMolecule m_molecule;
        private IEnumerable<CmlBond> m_bonds;
        private IEnumerable<CmlAtom> m_atoms;
        private double m_medianBondLength;
        private Rect m_moleculeExtents;
        private Rect m_canvasExtents;
        private List<AtomLabelCharacter> m_AtomLabelCharacters;
        private List<BondLine> m_BondLines;
        //private SortedDictionary<string, Ring> m_rings;
        private C4wOptions m_options;

        public OoXmlMolecule(string cml, C4wOptions options)
        {
            #region Load cml string into CmlMolecule

            XmlDocument docBefore = new XmlDocument();
            docBefore.LoadXml(cml);
            XmlNamespaceManager nsmgr1 = new XmlNamespaceManager(docBefore.NameTable);
            nsmgr1.AddNamespace("cml", "http://www.xml-cml.org/schema");
            XmlNode beforeMolecule = docBefore.SelectSingleNode("//cml:molecule", nsmgr1);
            XElement xe = XElement.Parse(beforeMolecule.OuterXml);

            m_molecule = new CmlMolecule(xe);
            m_bonds = m_molecule.GetAllBonds();
            m_atoms = m_molecule.GetAllAtoms();
            //Debug.WriteLine("Atoms: " + atoms.Count);
            //Debug.WriteLine("Bonds: " + bonds.Count);

            m_medianBondLength = GeometryTool.GetMedianBondLength2D(m_bonds);
            //Debug.WriteLine("Median Bond Length: " + medianBondLength);

            m_moleculeExtents = CoordinateTool.GetBounds2D(m_atoms);

            // Create canvas Extents with margin of offset
            m_canvasExtents = new Rect(m_moleculeExtents.Left,
                                            m_moleculeExtents.Top,
                                            m_moleculeExtents.Width,
                                            m_moleculeExtents.Height);

            //Debug.WriteLine("Drawing Bounds:-");
            //Debug.WriteLine("  Left: " + moleculeExtents.Left + " Top: " + moleculeExtents.Top);
            //Debug.WriteLine("  Width: " + moleculeExtents.Width + " Height: " + moleculeExtents.Height);

            #endregion

            m_options = options;
            m_AtomLabelCharacters = new List<AtomLabelCharacter>();
            m_BondLines = new List<BondLine>();
            //m_rings = new SortedDictionary<string, Ring>();

            //int i = 0;
            //foreach (CmlBond bond in m_bonds)
            //{
            //    i++;
            //    Debug.WriteLine("Bond " + i + " " + bond );
            //    IEnumerable<Ring> rings = bond.GetRingList();
            //    foreach (Ring r in rings)
            //    {
            //        string ringId = r.BondIdSet.Count.ToString("00") + ":" + string.Join("-", r.BondIdSet);
            //        if (!m_rings.ContainsKey(ringId))
            //        {
            //            Debug.WriteLine("Adding ring " + ringId);
            //            Point2 pp = r.GetCentroid();
            //            m_rings.Add(ringId, r);
            //        }
            //        else
            //        {
            //            Debug.WriteLine("Ring " + ringId + " already found");
            //        }
            //    }
            //}

            //Debug.WriteLine("Ring List :-");
            //foreach (KeyValuePair<string, Ring> kvp in m_rings)
            //{
            //    Ring r = (Ring)kvp.Value;
            //    Debug.WriteLine("Ring " + kvp.Key + " " + r.BondIdSet.Count);
            //    Debug.WriteLine(" Centroid at " + r.GetCentroid());
            //}
        }

        public Run GenerateRun()
        {
            Run run1 = new Run();

            ProgressBar pb = new ProgressBar();

            Debug.WriteLine("OoXmlMolecule.GenerateRun() Starting Step 1");
            DateTime started = DateTime.Now;
            #region Step 1 - Generate Atom Labels

            AtomRenderer ar = new AtomRenderer(m_canvasExtents, m_AtomLabelCharacters, ref m_ooxmlId);

            // Create Characters
            if (m_atoms.Count() > 1)
            {
                pb.Show();
            }
            pb.Message = "Processing Atoms";
            pb.Value = 0;
            pb.Maximum = m_atoms.Count() - 1;

            foreach (CmlAtom atom in m_atoms)
            {
                //Debug.WriteLine("Atom: " + atom.Id + " " + atom.ElementType);
                pb.Increment(1);
                ar.CreateAtomLabelCharacters(atom, m_options);
            }

            #endregion
            TimeSpan ts = DateTime.Now - started;
            Debug.WriteLine("Elapsed time " + ts.TotalMilliseconds.ToString("##,##0.0") + "ms");

            Debug.WriteLine("OoXmlMolecule.GenerateRun() Starting Step 2");
            started = DateTime.Now;
            #region Step 2 - Generate Bond Lines

            BondRenderer br = new BondRenderer(m_canvasExtents, m_BondLines, ref m_ooxmlId, m_medianBondLength);

            if (m_bonds.Count() > 1)
            {
                pb.Show();
            }
            pb.Message = "Processing Bonds";
            pb.Value = 0;
            pb.Maximum = m_bonds.Count() - 1;

            foreach (CmlBond bond in m_bonds)
            {
                pb.Increment(1);
                br.CreateBondLines(bond);
            }
            #endregion
            ts = DateTime.Now - started;
            Debug.WriteLine("Elapsed time " + ts.TotalMilliseconds.ToString("##,##0.0") + "ms");

            Debug.WriteLine("OoXmlMolecule.GenerateRun() Starting Step 3");
            started = DateTime.Now;
            #region Step 3 - Increase canvas size
            // to accomodate extra space required by label characters

            //Debug.WriteLine(m_canvasExtents);

            double xMin = m_moleculeExtents.Left;
            double xMax = m_moleculeExtents.Right;
            double yMin = m_moleculeExtents.Top;
            double yMax = m_moleculeExtents.Bottom;

            foreach (AtomLabelCharacter alc in m_AtomLabelCharacters)
            {
                xMin = Math.Min(xMin, alc.Position.X);
                xMax = Math.Max(xMax, alc.Position.X + Helper.ScaleTtfToCml(alc.Character.BlackBoxX));
                yMin = Math.Min(yMin, alc.Position.Y);
                yMax = Math.Max(yMax, alc.Position.Y + Helper.ScaleTtfToCml(alc.Character.BlackBoxY));
            }

            // Create new canvas extents
            m_canvasExtents = new Rect(xMin - Helper.DRAWING_MARGIN,
                                        yMin - Helper.DRAWING_MARGIN,
                                        xMax - xMin + (2 * Helper.DRAWING_MARGIN),
                                        yMax - yMin + (2 * Helper.DRAWING_MARGIN));

            //Debug.WriteLine(m_canvasExtents);

            #endregion
            ts = DateTime.Now - started;
            Debug.WriteLine("Elapsed time " + ts.TotalMilliseconds.ToString("##,##0.0") + "ms");

            Debug.WriteLine("OoXmlMolecule.GenerateRun() Starting Step 4");
            started = DateTime.Now;
            #region Step 4 - Shrink bond lines
            // so that they do not overlap label characters

            foreach (AtomLabelCharacter alc in m_AtomLabelCharacters)
            {
                Rect a = new Rect(alc.Position.X - Helper.CHARACTER_CLIPPING_MARGIN, alc.Position.Y - Helper.CHARACTER_CLIPPING_MARGIN,
                                Helper.ScaleTtfToCml(alc.Character.BlackBoxX) + (Helper.CHARACTER_CLIPPING_MARGIN * 2),
                                Helper.ScaleTtfToCml(alc.Character.BlackBoxY) + (Helper.CHARACTER_CLIPPING_MARGIN * 2));

                //Debug.WriteLine("Character: " + alc.Ascii + " Rectangle: " + a);

                if (alc.IsSubScript)
                {
                    a.Width = Helper.ScaleTtfToCml(alc.Character.BlackBoxX) * Helper.SUBSCRIPT_SCALE_FACTOR;
                    a.Height = Helper.ScaleTtfToCml(alc.Character.BlackBoxY) * Helper.SUBSCRIPT_SCALE_FACTOR;
                }

                foreach (BondLine bl in m_BondLines)
                {
                    Point start = new Point(bl.StartX, bl.StartY);
                    Point end = new Point(bl.EndX, bl.EndY);

                    //Debug.WriteLine("  Line Start Point: " + start);
                    //Debug.WriteLine("  Line   End Point: " + end);

                    if (CohenSutherland.ClipLine(a, ref start, ref end))
                    {
                        //Debug.WriteLine("    Clipped Line Start Point: " + start);
                        //Debug.WriteLine("    Clipped Line   End Point: " + end);

                        bool bClipped = false;

                        if (bl.StartX == start.X && bl.StartY == start.Y)
                        {
                            bl.StartX = end.X;
                            bl.StartY = end.Y;
                            bClipped = true;
                        }
                        if (bl.EndX == end.X && bl.EndY == end.Y)
                        {
                            bl.EndX = start.X;
                            bl.EndY = start.Y;
                            bClipped = true;
                        }

                        if (bClipped)
                        {
                            start = new Point(bl.StartX, bl.StartY);
                            end = new Point(bl.EndX, bl.EndY);
                            //Debug.WriteLine("    New Line Start Point: " + start);
                            //Debug.WriteLine("    New Line   End Point: " + end);
                        }
                        else
                        {
                            // Line was clipped at both ends; hopefully never get here.
                            Vector vstart = new Point(bl.StartX, bl.StartY) - start;
                            Vector vend = new Point(bl.EndX, bl.EndY) - end;
                            if (vstart.Length < vend.Length)
                            {
                                bl.StartX = end.X;
                                bl.StartY = end.Y;
                            }
                            else
                            {
                                bl.EndX = start.X;
                                bl.EndY = start.Y;
                            }
                        }
                    }
                }
            }

            #endregion
            ts = DateTime.Now - started;
            Debug.WriteLine("Elapsed time " + ts.TotalMilliseconds.ToString("##,##0.0") + "ms");

            Debug.WriteLine("OoXmlMolecule.GenerateRun() Starting Step 5");
            started = DateTime.Now;
            #region Step 5 - Create main OoXml drawing objects

            DocumentFormat.OpenXml.Wordprocessing.Drawing drawing1 = new DocumentFormat.OpenXml.Wordprocessing.Drawing();
            A.Graphic graphic1 = CreateGraphic();
            A.GraphicData graphicData1 = CreateGraphicData();
            Wpg.WordprocessingGroup wordprocessingGroup1 = new Wpg.WordprocessingGroup();

            // Create Inline Drawing using canvas extents
            Wp.Inline inline1 = CreateInline(graphicData1, wordprocessingGroup1);

            #endregion
            ts = DateTime.Now - started;
            Debug.WriteLine("Elapsed time " + ts.TotalMilliseconds.ToString("##,##0.0") + "ms");

            Debug.WriteLine("OoXmlMolecule.GenerateRun() Starting Step 6");
            started = DateTime.Now;
            #region Step 6 - Create and append OoXml objects for Atom Labels

            ar = new AtomRenderer(m_canvasExtents, m_AtomLabelCharacters, ref m_ooxmlId);

            if (m_atoms.Count() > 1)
            {
                pb.Show();
            }
            pb.Message = "Rendering Atoms";
            pb.Value = 0;
            pb.Maximum = m_AtomLabelCharacters.Count - 1;

            foreach (AtomLabelCharacter alc in m_AtomLabelCharacters)
            {
                pb.Increment(1);
                ar.DrawAtomLabelCharacter(wordprocessingGroup1, alc);
            }

            #endregion
            ts = DateTime.Now - started;
            Debug.WriteLine("Elapsed time " + ts.TotalMilliseconds.ToString("##,##0.0") + "ms");

            Debug.WriteLine("OoXmlMolecule.GenerateRun() Starting Step 7");
            started = DateTime.Now;
            #region Step 7 - Create and append OoXml objects for Bond Lines

            br = new BondRenderer(m_canvasExtents, m_BondLines, ref m_ooxmlId, m_medianBondLength);

            if (m_bonds.Count() > 1)
            {
                pb.Show();
            }
            pb.Message = "Rendering Bonds";
            pb.Value = 0;
            pb.Maximum = m_BondLines.Count - 1;

            foreach (BondLine bl in m_BondLines)
            {
                pb.Increment(1);
                switch (bl.Type)
                {
                    case "wedge":
                    case "hash":
                        br.DrawWedgeBond(wordprocessingGroup1, bl);
                        break;
                    default:
                        br.DrawBondLine(wordprocessingGroup1, bl);
                        break;
                }
            }

            #endregion
            ts = DateTime.Now - started;
            Debug.WriteLine("Elapsed time " + ts.TotalMilliseconds.ToString("##,##0.0") + "ms");

            Debug.WriteLine("OoXmlMolecule.GenerateRun() Starting Step 8");
            started = DateTime.Now;
            #region Step 8 - Append OoXml drawing objects to OoXml run object

            graphicData1.Append(wordprocessingGroup1);
            graphic1.Append(graphicData1);
            inline1.Append(graphic1);
            drawing1.Append(inline1);
            run1.Append(drawing1);

            #endregion
            ts = DateTime.Now - started;
            Debug.WriteLine("Elapsed time " + ts.TotalMilliseconds.ToString("##,##0.0") + "ms");

            pb.Hide();
            return run1;
        }

        private A.Graphic CreateGraphic()
        {
            A.Graphic graphic1 = new A.Graphic();
            graphic1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            return graphic1;
        }

        private A.GraphicData CreateGraphicData()
        {
            return new A.GraphicData() { Uri = "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup" };
        }

        private Wp.Inline CreateInline(A.GraphicData graphicData, Wpg.WordprocessingGroup wordprocessingGroup1)
        {
            UInt32Value inlineId = UInt32Value.FromUInt32((uint)m_ooxmlId++);

            Int64Value width = Helper.ScaleCmlForWord(m_canvasExtents.Width);
            Int64Value height = Helper.ScaleCmlForWord(m_canvasExtents.Height);

            Wp.Inline inline1 = new Wp.Inline() { DistanceFromTop = (UInt32Value)0U, DistanceFromBottom = (UInt32Value)0U, DistanceFromLeft = (UInt32Value)0U, DistanceFromRight = (UInt32Value)0U };
            Wp.Extent extent1 = new Wp.Extent() { Cx = width, Cy = height };

            Wp.EffectExtent effectExtent1 = new Wp.EffectExtent() { LeftEdge = 0L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L };
            Wp.DocProperties docProperties1 = new Wp.DocProperties() { Id = inlineId, Name = "moleculeGroup" };

            Wpg.NonVisualGroupDrawingShapeProperties nonVisualGroupDrawingShapeProperties1 = new Wpg.NonVisualGroupDrawingShapeProperties();

            Wpg.GroupShapeProperties groupShapeProperties1 = new Wpg.GroupShapeProperties();

            A.TransformGroup transformGroup1 = new A.TransformGroup();
            A.Offset offset1 = new A.Offset() { X = 0L, Y = 0L };
            A.Extents extents1 = new A.Extents() { Cx = width, Cy = height };
            A.ChildOffset childOffset1 = new A.ChildOffset() { X = 0L, Y = 0L };
            A.ChildExtents childExtents1 = new A.ChildExtents() { Cx = width, Cy = height };

            transformGroup1.Append(offset1);
            transformGroup1.Append(extents1);
            transformGroup1.Append(childOffset1);
            transformGroup1.Append(childExtents1);

            groupShapeProperties1.Append(transformGroup1);
            wordprocessingGroup1.Append(nonVisualGroupDrawingShapeProperties1);
            wordprocessingGroup1.Append(groupShapeProperties1);

            inline1.Append(extent1);
            inline1.Append(effectExtent1);
            inline1.Append(docProperties1);

            return inline1;
        }
    }
}
