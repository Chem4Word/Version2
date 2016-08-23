// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Media;
using Chem4Word.Drawing.TwoD.Common;
using Chem4Word.Drawing.TwoD.Nodes;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.Drawing.TwoD.Bonds
{
    public class WavyEdgeControl : AbstractEdgeControl
    {
        public WavyEdgeControl(ContextObject contextObject, CmlBond bond, INode startNode, INode endNode, IChemCanvas canvas)
            : base(contextObject, bond, startNode, endNode, canvas)
        {
        }

        protected override void Refresh()
        {
            if (null != label)
            {
                children.Remove(label);
            }
            label = new DrawingVisual();
            DrawingContext drawingContext = label.RenderOpen();

            LineGeometry finalBondGeometry = GetInitialBondGeometry();

            Geometry hitTestRectangle = GetHitTestRectangle(finalBondGeometry);

            Pen transPen = new Pen(Brushes.Transparent, 1.0);

            SetPenAndBrush();

            StreamGeometry arrow = GetArrow(finalBondGeometry);

            if (arrow != null)
            {
                Pen arrowPen = new Pen(Brushes.Green, 3.0);
                drawingContext.DrawGeometry(Brushes.Green, arrowPen, arrow);
            }

            Vector unitBondVector = finalBondGeometry.EndPoint - finalBondGeometry.StartPoint;
            double bondLength = unitBondVector.Length;
            unitBondVector.Normalize();
            double size = 5f;
            Vector perpVec = new Vector(-unitBondVector.Y, unitBondVector.X) * size;

            Point startPoint = finalBondGeometry.StartPoint;
            Point currentPoint = startPoint;
            double currentLength = (currentPoint - startPoint).Length;

            int dummy;

            StreamGeometry wavyGeometry = new StreamGeometry();

            using (StreamGeometryContext ctx = wavyGeometry.Open())
            {
                ctx.BeginFigure(startPoint, false, false);

                for (int i = 0; currentLength < bondLength; i++)
                {
                    Point nextControlPoint;
                    Point nextCurrentPoint = currentPoint + unitBondVector * 2 * size;

                    if (Math.DivRem(i, 2, out dummy) == 0)
                    {
                        nextControlPoint = currentPoint + unitBondVector * 5 + perpVec * size;
                    }
                    else
                    {
                        nextControlPoint = currentPoint + unitBondVector * 5 - perpVec * size;
                    }

                    ctx.BezierTo(currentPoint, nextControlPoint, nextCurrentPoint, true, true);

                    currentPoint = nextCurrentPoint;
                    currentLength = (currentPoint - startPoint).Length;
                }
            }

            drawingContext.DrawGeometry(fill, pen, wavyGeometry);
            drawingContext.DrawGeometry(Brushes.Transparent, transPen, hitTestRectangle);

            drawingContext.Close();
            children.Add(label);
        }
    }
}