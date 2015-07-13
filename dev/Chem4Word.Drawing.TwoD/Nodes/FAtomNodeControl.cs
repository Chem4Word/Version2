// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Chem4Word.Drawing.TwoD.Common;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.Drawing.TwoD.Nodes
{
    /// <summary>
    /// Specific NodeControl to show Fluorine atoms
    /// </summary>
    public class FAtomNodeControl : AbstractNodeControl
    {
        #region constructors

        public FAtomNodeControl(ContextObject contextObject,
                                CmlAtom atom,
                                IChemCanvas canvas)
        {
            this.CmlAtom = atom;
            this.contextObject = contextObject;
            this.canvas = canvas;
            this.children = new VisualCollection(this);

            Init();
        }

        #endregion constructors

        #region Overrides

        /// <summary>
        /// <see cref="AbstractNodeControl#Refresh"/>
        /// </summary>
        protected override void Refresh()
        {
            if (null != this.principalNodeText)
            {
                this.children.Remove(this.principalNodeText);
            }
            // Create an instance of a DrawingVisual.
            this.principalNodeText = new DrawingVisual();

            // Retrieve the DrawingContext from the DrawingVisual.
            DrawingContext drawingContext = this.principalNodeText.RenderOpen();

            // Draw a formatted text string into the DrawingContext.
            FormattedText text = new FormattedText(this.labelCentreText,
                                                   CultureInfo.
                                                       GetCultureInfo("en-us"),
                                                   FlowDirection.LeftToRight,
                                                   new Typeface("Verdana"),
                                                   14,
                                                   Brushes.LawnGreen);

            double x = this.canvas.ToScreenX(this.CmlAtom.Point2.X);
            double y = this.canvas.ToScreenY(this.CmlAtom.Point2.Y);
            this.Attachment = new Point(x, y);

            double textOffsetHeight = text.Height/2;
            double textOffsetWidth = text.Width/2;

            drawingContext.DrawText(text, new Point(x - textOffsetWidth, y - textOffsetHeight));

            // Close the DrawingContext to persist changes to the DrawingVisual.
            drawingContext.Close();

            this.children.Add(principalNodeText);

            if (null != this.frame)
            {
                this.children.Remove(this.frame);
            }
            this.frame = new DrawingVisual();

            // Retrieve the DrawingContext from the DrawingVisual.
            drawingContext = this.frame.RenderOpen();

            double radius = 0;
            radius = Math.Max(principalNodeText.DescendantBounds.Height, principalNodeText.DescendantBounds.Width)/2;

            radius += framePadding;

            this.frame.Opacity = 1;

            if (IsActive)
            {
                this.pen = new Pen(Brushes.Orange, 1);
                if (IsSelected)
                {
                    this.fill = Brushes.Orange;
                    this.frame.Opacity = 0.5;
                }
                else
                {
                    this.fill = null;
                }
            }
            else
            {
                this.pen = null;
                if (IsSelected)
                {
                    this.fill = Brushes.Blue;
                    this.frame.Opacity = 0.25;
                }
                else
                {
                    this.fill = null;
                }
            }

            drawingContext.DrawEllipse(fill, pen, new Point(x, y), radius, radius);

            drawingContext.Close();

            this.children.Insert(0, this.frame);
        }

        #endregion Overrides
    }
}