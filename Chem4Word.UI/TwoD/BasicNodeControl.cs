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
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.UI.TwoD
{
    public class BasicNodeControl : AbstractNodeControl
    {
        private DrawingVisual textAdornmentVisual;

        #region constructors

        public BasicNodeControl(ContextObject contextObject,
                                CmlAtom atom,
                                ChemCanvas canvas)
        {
            this.CmlAtom = atom;
            this.contextObject = contextObject;
            this.canvas = canvas;
            this.children = new VisualCollection(this);
            Init();
        }

        #endregion constructors

        #region private methods

        /// <summary>
        /// <see cref="AbstractNodeControl#Refresh"/>
        /// </summary>
        protected override void Refresh()
        {
            #region FOR EDIT

            /// should this be be using RichText boxes / System.Windows.Shapes.Line etc
            /// rather than DrawingVisuals
            /// these would surely be better for serialising to disk if we wanted to?
            if (null != this.principalNodeText)
            {
                children.Remove(principalNodeText);
            }
            principalNodeText = new DrawingVisual();
            var drawingContext = principalNodeText.RenderOpen();
            var text = new FormattedText(labelCentreText,
                                         CultureInfo.GetCultureInfo("en-us"),
                                         FlowDirection.LeftToRight,
                                         new Typeface("Verdana"),
                                         11, Brushes.DarkGray);

            /// QUESTION 
            /// can we do this conversion to screen better?
            double x = this.canvas.ToScreenX(this.CmlAtom.Point2.X);

            double y = this.canvas.ToScreenY(this.CmlAtom.Point2.Y);
            
            this.Attachment = new Point(x, y);
            double textOffsetHeight = text.Height/2;
            double textOffsetWidth = text.Width/2;

            drawingContext.DrawText(text, new Point(x - textOffsetWidth, y - textOffsetHeight));

            drawingContext.Close();
            this.children.Add(principalNodeText);

            // North East - Charge, and/or Radical
            //
            //First, the charge text
            string chargeStr = CmlAtom.FormalCharge.HasValue
                                   ? Math.Abs(CmlAtom.FormalCharge.Value).ToString(CultureInfo.InvariantCulture)
                                   : string.Empty;

            if (chargeStr.Length > 0)
            {
                switch (CmlAtom.FormalCharge.Value)
                {
                    case 1:
                        chargeStr = "+";
                        break;
                    case 0:
                        chargeStr = string.Empty;
                        break;
                    case -1:
                        chargeStr = "-";
                        break;
                    default:
                        if (CmlAtom.FormalCharge.Value > 1)
                        {
                            chargeStr += "+";
                        }
                        else
                        {
                            chargeStr += "-";
                        }
                        break;
                }
            }

            textAdornmentVisual = new DrawingVisual();
            drawingContext = textAdornmentVisual.RenderOpen();
            var chargeText = new FormattedText(chargeStr,
                                               CultureInfo.GetCultureInfo("en-us"),
                                               FlowDirection.LeftToRight,
                                               new Typeface("Verdana"),
                                               9, Brushes.DarkGray);

            drawingContext.DrawText(chargeText, new Point(x + textOffsetWidth + 1, y - textOffsetHeight - 3));

            //Radical
            if (CmlAtom.SpinMultiplicity.HasValue)
            {
                double currentX = x + textOffsetWidth + 4 + chargeText.Width;

                for (int i = 0; i < CmlAtom.SpinMultiplicity.Value - 1; i++)
                {
                    drawingContext.DrawEllipse(Brushes.DarkGray, new Pen(Brushes.DarkGray, 0.0),
                                               new Point(currentX, y - textOffsetHeight + 2), 2, 2);
                    currentX += 5;
                }
            }

            //Isotope
            if (CmlAtom.IsotopeNumber.HasValue)
            {
                var isotopeText = new FormattedText(CmlAtom.IsotopeNumber.Value.ToString(CultureInfo.InvariantCulture),
                                                    CultureInfo.GetCultureInfo("en-us"),
                                                    FlowDirection.LeftToRight,
                                                    new Typeface("Verdana"),
                                                    9, Brushes.DarkGray);

                drawingContext.DrawText(isotopeText,
                                        new Point(x - textOffsetWidth - isotopeText.Width, y - textOffsetHeight - 3));
            }

            //Label
            //TODO: Using ID to start with
            string labelStr = "";

            foreach (CmlLabel label in CmlAtom.GetLabels())
            {
                labelStr += label.Value + " ";
            }

            labelStr = labelStr.Trim();

            var labelText = new FormattedText(labelStr,
                                              CultureInfo.GetCultureInfo("en-us"),
                                              FlowDirection.LeftToRight,
                                              new Typeface("Verdana"),
                                              9, Brushes.DarkGray);

            drawingContext.DrawText(labelText,
                                    new Point(x - textOffsetWidth - labelText.Width, y + textOffsetHeight/2 - 4));

            drawingContext.Close();
            this.children.Add(textAdornmentVisual);

            // now draw an (invisible?) ellipse around the atom so it can be selected etc
            if (null != this.frame)
            {
                this.children.Remove(this.frame);
            }
            this.frame = new DrawingVisual();
            this.isInitialised = true;
            drawingContext = this.frame.RenderOpen();
            double radius = 0;
                if (this.principalNodeText.DescendantBounds.Height > this.principalNodeText.DescendantBounds.Width)
                {
                    radius = this.principalNodeText.DescendantBounds.Height/2;
                }
                else
                {
                    radius = this.principalNodeText.DescendantBounds.Width/2;
                }
            radius += this.framePadding;
            this.frame.Opacity = 1;

            #endregion FOR EDIT

            SetPenAndBrush();
            drawingContext.DrawEllipse(fill, pen, new Point(x, y), radius, radius);
            drawingContext.Close();
            this.children.Insert(0, this.frame);
        }

        #endregion private methods
    }
}