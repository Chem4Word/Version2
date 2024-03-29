﻿// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------

using System.Windows.Media;
using Chem4Word.Drawing.TwoD.Common;
using Chem4Word.Drawing.TwoD.Nodes;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.Drawing.TwoD.Bonds
{
    public class BasicEdgeControl : AbstractEdgeControl
    {
        public BasicEdgeControl(ContextObject contextObject, CmlBond bond, INode startNode, INode endNode, IChemCanvas canvas)
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

            drawingContext.DrawGeometry(fill, pen, finalBondGeometry);
            drawingContext.DrawGeometry(Brushes.Transparent, transPen, hitTestRectangle);

            drawingContext.Close();
            children.Add(label);
        }
    }
}