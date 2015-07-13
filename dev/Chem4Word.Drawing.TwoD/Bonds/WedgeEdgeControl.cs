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
using Numbo;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.Drawing.TwoD.Bonds
{
    public class WedgeEdgeControl : AbstractEdgeControl
    {
        public WedgeEdgeControl(ContextObject contextObject, CmlBond bond, INode startNode, INode endNode, IChemCanvas canvas)
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

            SetPenAndBrush();
            //drawingContext.PushClip(clipGeometry);
            drawingContext.DrawGeometry(fill, pen, wedge);
            //drawingContext.DrawLine(pen, start, end);

            drawingContext.Close();
            children.Add(label);
        }

        protected override void SetPenAndBrush()
        {
            pen = new Pen(Brushes.Transparent, 0);
            if (IsActive || IsMouseOver || IsSelected)
            {
                fill = Brushes.Orange;
            }
            else
            {
                fill = canvas.IsCmlValid ? Brushes.Blue : Brushes.Red;
            }

            switch (canvas.DrawingMode)
            {
                case DrawingMode.Select:
                    ArrowFill = Brushes.Green;
                    break;
                case DrawingMode.BondSelect:
                    ArrowFill = Brushes.Transparent;
                    break;
                case DrawingMode.Draw:
                    ArrowFill = Brushes.Transparent;
                    break;
                case DrawingMode.Delete:
                    ArrowFill = Brushes.Red;
                    break;
                default:
                    throw new NumboException("drawing mode is not valid");
            }

            ArrowPen = new Pen(ArrowFill, 3.0);
        }
    }
}