// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------

using System.Windows;
using System.Windows.Media;
using Chem4Word.Drawing.TwoD.Common;
using Chem4Word.Drawing.TwoD.Nodes;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.Drawing.TwoD.Bonds
{
    /// <summary>
    /// 
    /// </summary>
    public class HatchEdgeControl : AbstractEdgeControl
    {
        public HatchEdgeControl(ContextObject contextObject, CmlBond bond, INode startNode, INode endNode, IChemCanvas canvas)
            : base(contextObject, bond, startNode, endNode, canvas)
        {
        }

        protected override void Refresh()
        {
            if (null != this.label)
            {
                this.children.Remove(this.label);
            }
            this.label = new DrawingVisual();
            DrawingContext drawingContext = this.label.RenderOpen();

            LineGeometry initialBondGeometry = GetInitialBondGeometry();
            Geometry hitTestRectangle = GetHitTestRectangle(initialBondGeometry);

            Vector bondVector = Point.Subtract(initialBondGeometry.StartPoint, initialBondGeometry.EndPoint);
            Vector displacementVector = new Vector(bondVector.Y, -bondVector.X);
            displacementVector.Normalize();
            displacementVector = Vector.Multiply(4, displacementVector);

            Point p1 = initialBondGeometry.StartPoint;
            Point p2 = initialBondGeometry.EndPoint + displacementVector;
            Point p3 = initialBondGeometry.EndPoint - displacementVector;

            StreamGeometry wedge = new StreamGeometry();

            using (StreamGeometryContext ctx = wedge.Open())
            {
                ctx.BeginFigure(p1, true, true);
                ctx.LineTo(p2, true, false);
                ctx.LineTo(p3, true, false);
            }

            GeometryGroup hatchGroup = new GeometryGroup();

            Point currentStart = initialBondGeometry.StartPoint + displacementVector;
            Point currentEnd = initialBondGeometry.StartPoint - displacementVector;
            int numberOfHatches = (int)(bondVector.Length / 4.0);
            bondVector.Normalize();
            bondVector = bondVector * 4.0;

            for (int i = 0; i < numberOfHatches; i++)
            {
                LineGeometry line = new LineGeometry(currentStart, currentEnd);
                hatchGroup.Children.Add(line);

                currentStart -= bondVector;
                currentEnd -= bondVector;
            }

            //pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 2);
            SetPenAndBrush();
            drawingContext.PushClip(wedge);
            drawingContext.DrawGeometry(fill, pen, hatchGroup);
            drawingContext.Pop();

            Pen transPen = new Pen(Brushes.Transparent, 1.0);
            drawingContext.DrawGeometry(Brushes.Transparent, transPen, hitTestRectangle);
            //drawingContext.DrawLine(pen, start, end);

            drawingContext.Close();
            this.children.Add(label);
        }
    }
}