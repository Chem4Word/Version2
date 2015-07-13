// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Chem4Word.Drawing.TwoD.Common;
using Chem4Word.Drawing.TwoD.Nodes;
using Euclid;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.Drawing.TwoD.Bonds
{
    public class DoubleEdgeControl : AbstractEdgeControl
    {
        public DoubleEdgeControl(ContextObject contextObject, CmlBond bond, INode startNode, INode endNode, IChemCanvas canvas)
            : base(contextObject, bond, startNode, endNode, canvas)
        {
        }

        protected override void Refresh()
        {
            Vector2 vec2 = Bond.GetPrettyDoubleBondVector();

            if (vec2 == null)
            {
                StandardDraw();
                return;
            }

            Vector displacementVector = new Vector(canvas.ToScreenX(vec2.X), canvas.ToScreenY(vec2.Y));
            displacementVector.Normalize();
            displacementVector *= 3;

            if (null != this.label)
            {
                this.children.Remove(this.label);
            }

            this.label = new DrawingVisual();
            DrawingContext drawingContext = this.label.RenderOpen();

            LineGeometry centerLineBondGeometry = GetInitialBondGeometry();

            Geometry hitTestRectangle = GetHitTestRectangle(centerLineBondGeometry);
            Pen transPen = new Pen(Brushes.Transparent, 1.0);

            CmlAtom nonHAtom;
            string[] ligandIDs;
            GetAtomAndLigandIDs(0, out nonHAtom, out ligandIDs);

            //Vector bondVector = new Vector(centerLineBondGeometry.EndPoint.X - centerLineBondGeometry.StartPoint.X,
            //                               centerLineBondGeometry.EndPoint.Y - centerLineBondGeometry.StartPoint.Y);

            Point startPoint = Point.Add(centerLineBondGeometry.StartPoint, displacementVector);
            Point endPoint = Point.Add(centerLineBondGeometry.EndPoint, displacementVector);

            LineGeometry otherBondGeometry = new LineGeometry(startPoint, endPoint);

            StreamGeometry arrow = GetArrow(centerLineBondGeometry);

            if (arrow != null)
            {
                Pen arrowPen = new Pen(Brushes.Green, 3.0);
                drawingContext.DrawGeometry(Brushes.Green, arrowPen, arrow);
            }

            SetPenAndBrush();

            drawingContext.DrawGeometry(fill, pen, centerLineBondGeometry);
            drawingContext.DrawGeometry(Brushes.Transparent, transPen, hitTestRectangle);

            drawingContext.PushClip(GetClipGeometry(displacementVector));

            drawingContext.DrawGeometry(fill, pen, otherBondGeometry);

            drawingContext.Pop();

            //drawingContext.DrawLine(pen, start, end);

            drawingContext.Close();
            this.children.Add(label);
        }

        private void StandardDraw()
        {
            if (null != this.label)
            {
                this.children.Remove(this.label);
            }

            this.label = new DrawingVisual();
            DrawingContext drawingContext = this.label.RenderOpen();

            LineGeometry centerLineBondGeometry = GetInitialBondGeometry();

            Geometry hitTestRectangle = GetHitTestRectangle(centerLineBondGeometry);
            Pen transPen = new Pen(Brushes.Transparent, 1.0);

            Vector bondVector = Point.Subtract(centerLineBondGeometry.StartPoint, centerLineBondGeometry.EndPoint);
            Vector displacementVector = new Vector(bondVector.Y, -bondVector.X);
            displacementVector.Normalize();
            displacementVector = Vector.Multiply(1.5, displacementVector);

            Point topStart = centerLineBondGeometry.StartPoint + displacementVector;
            Point topEnd = centerLineBondGeometry.EndPoint + displacementVector;
            Point bottomStart = centerLineBondGeometry.StartPoint - displacementVector;
            Point bottomEnd = centerLineBondGeometry.EndPoint - displacementVector;

            LineGeometry topBondGeometry = new LineGeometry(topStart, topEnd);
            LineGeometry bottomBondGeometry = new LineGeometry(bottomStart, bottomEnd);

            SetPenAndBrush();

            StreamGeometry arrow = GetArrow(centerLineBondGeometry);

            if (arrow != null)
            {
                Pen arrowPen = new Pen(Brushes.Green, 3.0);
                drawingContext.DrawGeometry(Brushes.Green, arrowPen, arrow);
            }

            drawingContext.DrawGeometry(Brushes.Transparent, transPen, hitTestRectangle);
            drawingContext.DrawGeometry(fill, pen, topBondGeometry);
            drawingContext.DrawGeometry(fill, pen, bottomBondGeometry);
            //drawingContext.DrawLine(pen, start, end);

            drawingContext.Close();
            this.children.Add(label);
        }

        private Geometry GetClipGeometry(Vector displacementVector)
        {
            //Create clip region
            List<Geometry> clips = GetClipGeometries(displacementVector);

            Geometry clipGeometry = null;

            foreach (Geometry clip in clips)
            {
                if (clipGeometry == null)
                {
                    clipGeometry = clip;
                }
                else
                {
                    clipGeometry = new CombinedGeometry(GeometryCombineMode.Intersect, clipGeometry, clip);
                }
            }

            return clipGeometry;
        }

        private List<Geometry> GetClipGeometries(Vector displacementVector)
        {
            List<Geometry> geometryList = new List<Geometry>();

            //Get clip geometries for start node first.
            LineGeometry initialBondGeometry = GetInitialBondGeometry();
            Vector bondVector = new Vector(initialBondGeometry.EndPoint.X - initialBondGeometry.StartPoint.X,
                                           initialBondGeometry.EndPoint.Y - initialBondGeometry.StartPoint.Y) * -1;
            bondVector.Normalize();

            // Get the start atom and its ligand IDs.
            AddClips(bondVector, displacementVector, geometryList, 1);

            bondVector *= -1;

            AddClips(bondVector, displacementVector, geometryList, 0);

            return geometryList;
        }

        private void AddClips(Vector bondVector, Vector displacementVector, List<Geometry> geometryList, int atomIndex)
        {
            CmlAtom startAtom;
            string[] ligandIDs;
            GetAtomAndLigandIDs(atomIndex, out startAtom, out ligandIDs);
            CmlAtom endAtom = Bond.GetAtomAtOtherEnd(startAtom);

            // Get the atoms ajoined to startAtom
            CmlAtom[] startAtoms = GetAtoms(startAtom, ligandIDs);

            foreach (CmlAtom atom in startAtoms)
            {
                if (atom.ElementType == "H")
                {
                    continue;
                }

                Point startAtomLocation = new Point(canvas.ToScreenX(startAtom.GetCentroid().X),
                                                    canvas.ToScreenY(startAtom.GetCentroid().Y));
                Point endAtomLocation = new Point(canvas.ToScreenX(endAtom.GetCentroid().X),
                                                  canvas.ToScreenY(endAtom.GetCentroid().Y));
                Point otherAtomLocation = new Point(canvas.ToScreenX(atom.GetCentroid().X),
                                                    canvas.ToScreenY(atom.GetCentroid().Y));

                //Make sure we're creating a clip region on the right side.
                Point checkPoint = startAtomLocation + (endAtomLocation - startAtomLocation) / 2 + displacementVector;
                StreamGeometry clipCheck = new StreamGeometry();

                using (StreamGeometryContext ctx = clipCheck.Open())
                {
                    ctx.BeginFigure(startAtomLocation, true, true);
                    ctx.LineTo(endAtomLocation, true, false);
                    ctx.LineTo(otherAtomLocation, true, false);
                }

                if (!(clipCheck.FillContains(checkPoint) || clipCheck.FillContains(checkPoint)))
                {
                    continue;
                }

                Vector ajoiningBondVector = otherAtomLocation - startAtomLocation;
                ajoiningBondVector.Normalize();

                //Find the bisector
                Vector bisector = bondVector + ajoiningBondVector;
                bisector.Normalize();
                bisector *= 100; // Give it some arbitrary large size

                // Let's get our points for the clip geometry
                Point p1 = startAtomLocation;
                Point p2 = startAtomLocation + bisector;
                Point p3 = endAtomLocation + displacementVector;
                Point p4 = endAtomLocation;

                StreamGeometry clip = new StreamGeometry();

                using (StreamGeometryContext ctx = clip.Open())
                {
                    ctx.BeginFigure(p1, true, true);
                    ctx.LineTo(p2, true, false);
                    ctx.LineTo(p3, true, false);
                    ctx.LineTo(p4, true, false);
                }

                //Discard this region if it doesn't contain at least one point on the double bond.
                if (clip.FillContains(startAtomLocation + displacementVector) ||
                    clip.FillContains(endAtomLocation + displacementVector))
                {
                    geometryList.Add(clip);
                }
            }
        }

        private CmlAtom[] GetAtoms(CmlAtom atom, string[] ligandIDs)
        {
            CmlBond[] bonds = new CmlBond[ligandIDs.Length];
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atom);

            for (int i = 0; i < ligandIDs.Length; i++)
            {
                bonds[i] = molecule.GetBondById(ligandIDs[i]);
            }

            List<CmlAtom> childAtoms = new List<CmlAtom>();

            foreach (CmlBond b in bonds)
            {
                childAtoms.Add(b.GetAtomAtOtherEnd(atom));
            }

            return childAtoms.ToArray();
        }

        private void GetAtomAndLigandIDs(int atomIndex, out CmlAtom atom, out string[] ligandIDs)
        {
            atom = Bond.GetAtoms().ElementAt(atomIndex);
            ligandIDs = GetLigandIDs(atom);
        }

        private string[] GetLigandIDs(CmlAtom atom)
        {
            ICollection<string> tempLigandIDs = atom.GetLigandBondIds();
            StringCollection temp = new StringCollection();
            temp.AddRange(tempLigandIDs.ToArray());
            temp.Remove(Bond.Id);
            string[] ligandIDs = new string[temp.Count];
            temp.CopyTo(ligandIDs, 0);
            return ligandIDs;
        }
    }
}