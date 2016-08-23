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
    public class TripleEdgeControl : AbstractEdgeControl
    {
        public TripleEdgeControl(ContextObject contextObject, CmlBond bond, INode startNode, INode endNode, IChemCanvas canvas)
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

            LineGeometry middleBondGeometry = GetInitialBondGeometry();

            Vector bondVector = Point.Subtract(middleBondGeometry.StartPoint, middleBondGeometry.EndPoint);
            Vector displacementVector = new Vector(bondVector.Y, -bondVector.X);
            displacementVector.Normalize();
            displacementVector = Vector.Multiply(2.5, displacementVector);

            Point topStart = middleBondGeometry.StartPoint + displacementVector;
            Point topEnd = middleBondGeometry.EndPoint + displacementVector;
            Point bottomStart = middleBondGeometry.StartPoint - displacementVector;
            Point bottomEnd = middleBondGeometry.EndPoint - displacementVector;

            LineGeometry topBondGeometry = new LineGeometry(topStart, topEnd);
            LineGeometry bottomBondGeometry = new LineGeometry(bottomStart, bottomEnd);

            SetPenAndBrush();

            drawingContext.DrawGeometry(fill, pen, topBondGeometry);
            drawingContext.DrawGeometry(fill, pen, middleBondGeometry);
            drawingContext.DrawGeometry(fill, pen, bottomBondGeometry);
            //drawingContext.DrawLine(pen, start, end);

            drawingContext.Close();
            this.children.Add(label);
        }
    }
}