﻿// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Media;
using Numbo;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.UI.TwoD
{
    public class WedgeEdgeControl : AbstractEdgeControl
    {
        public WedgeEdgeControl(ContextObject contextObject, CmlBond bond, AbstractNodeControl startNode,
                                AbstractNodeControl endNode, ChemCanvas canvas)
        {
            this.Bond = bond;
            this.canvas = canvas;
            this.contextObject = contextObject;
            this.StartNode = startNode;
            this.EndNode = endNode;
            this.children = new VisualCollection(this);
            Init();
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
                case CanvasContainer.DrawingMode.Select:
                    ArrowFill = Brushes.Green;
                    break;
                case CanvasContainer.DrawingMode.BondSelect:
                    ArrowFill = Brushes.Transparent;
                    break;
                case CanvasContainer.DrawingMode.Draw:
                    ArrowFill = Brushes.Transparent;
                    break;
                case CanvasContainer.DrawingMode.Delete:
                    ArrowFill = Brushes.Red;
                    break;
                default:
                    throw new NumboException("drawing mode is not valid");
            }

            ArrowPen = new Pen(ArrowFill, 3.0);
        }
    }
}