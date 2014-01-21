// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Windows.Media;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.UI.TwoD
{
    public class DottedEdgeControl : AbstractEdgeControl
    {
        public DottedEdgeControl(ContextObject contextObject,
                                 CmlBond bond, AbstractNodeControl startNode,
                                 AbstractNodeControl endNode, ChemCanvas canvas)
        {
            Bond = bond;
            this.canvas = canvas;
            this.contextObject = contextObject;
            this.StartNode = startNode;
            this.EndNode = endNode;
            children = new VisualCollection(this);
            Init();
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

            Geometry backGeometry = GetSelectBackGeometry();
            Geometry forwardGeometry = GetSelectForwardGeometry();

            Pen transPen = new Pen(Brushes.Transparent, 1.0);

            SetPenAndBrush();

            StreamGeometry arrow = GetArrow(finalBondGeometry);

            if (arrow != null)
            {
                Pen arrowPen = new Pen(Brushes.Green, 3.0);
                drawingContext.DrawGeometry(Brushes.Green, arrowPen, arrow);
            }

            pen.DashStyle = new DashStyle(new double[] {2, 2}, 0);
            drawingContext.DrawGeometry(fill, pen, finalBondGeometry);
            drawingContext.DrawGeometry(Brushes.Transparent, transPen, hitTestRectangle);

            drawingContext.Close();
            children.Add(label);
        }
    }
}