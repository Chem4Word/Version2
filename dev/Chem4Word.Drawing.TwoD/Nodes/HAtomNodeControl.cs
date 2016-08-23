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
    public class HAtomNodeControl : AbstractNodeControl
    {
        private DrawingVisual textAdornmentVisual;

        #region constructors

        public HAtomNodeControl(ContextObject contextObject,
                                CmlAtom atom, IChemCanvas canvas)
        {
            CmlAtom = atom;
            this.contextObject = contextObject;
            this.canvas = canvas;
            children = new VisualCollection(this);
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
            if (null != principalNodeText)
            {
                children.Remove(principalNodeText);
            }

            string isotopeLabel = "H";
            if (!contextObject.DisplayHydrogenIsotopeNumber)
            {
                switch (CmlAtom.IsotopeNumber)
                {
                    case 1:
                        break;
                    case 2:
                        isotopeLabel = "D";
                        break;
                    case 3:
                        isotopeLabel = "T";
                        break;
                    default:
                        if (CmlAtom.IsotopeNumber.HasValue)
                        {
                            isotopeLabel = CmlAtom.IsotopeNumber.Value.ToString(CultureInfo.InvariantCulture);
                        }
                        break;
                }
            }

            principalNodeText = new DrawingVisual();
            DrawingContext drawingContext = principalNodeText.RenderOpen();
            var text = new FormattedText(isotopeLabel,
                                         CultureInfo.GetCultureInfo("en-us"),
                                         FlowDirection.LeftToRight,
                                         new Typeface("Verdana"),
                                         11, Brushes.DarkGray);

            // QUESTION 
            // can we do this conversion to screen better?
            var x = canvas.ToScreenX(CmlAtom.Point2.X);
            var y = canvas.ToScreenY(CmlAtom.Point2.Y);

            Attachment = new Point(x, y);
            double textOffsetHeight = text.Height/2;
            double textOffsetWidth = text.Width/2;

            drawingContext.DrawText(text, new Point(x - textOffsetWidth, y - textOffsetHeight));

            drawingContext.Close();
            children.Add(principalNodeText);

            // North East - Charge, and/or Radical
            //
            //First, the charge text
            string chargeStr = CmlAtom.FormalCharge.HasValue
                                   ? Math.Abs(CmlAtom.FormalCharge.Value).ToString(CultureInfo.InvariantCulture)
                                   : string.Empty;

            if (chargeStr.Length > 0)
            {
                if (CmlAtom.FormalCharge.Value == 1)
                {
                    chargeStr = "+";
                }
                else if (CmlAtom.FormalCharge.Value == 0)
                {
                    chargeStr = string.Empty;
                }
                else if (CmlAtom.FormalCharge.Value == -1)
                {
                    chargeStr = "-";
                }
                else if (CmlAtom.FormalCharge.Value > 1)
                {
                    chargeStr += "+";
                }
                else
                {
                    chargeStr += "-";
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

            // Do the isotope bit.
            if (CmlAtom.IsotopeNumber.HasValue && contextObject.DisplayHydrogenIsotopeNumber)
            {
                var isotopeText = new FormattedText(CmlAtom.IsotopeNumber.Value.ToString(),
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
            children.Add(textAdornmentVisual);

            // now draw an (invisible?) ellipse around the atom so it can be selected etc
            if (null != frame)
            {
                children.Remove(frame);
            }
            frame = new DrawingVisual();
            isInitialised = true;
            drawingContext = frame.RenderOpen();
            double radius = 0;
            if (principalNodeText.DescendantBounds.Height > principalNodeText.DescendantBounds.Width)
            {
                radius = principalNodeText.DescendantBounds.Height/2;
            }
            else
            {
                radius = principalNodeText.DescendantBounds.Width/2;
            }
            radius += framePadding;
            frame.Opacity = 1;

            #endregion FOR EDIT

            SetPenAndBrush();
            drawingContext.DrawEllipse(fill, pen, new Point(x, y), radius, radius);
            drawingContext.Close();
            children.Insert(0, frame);
        }

        #endregion private methods
    }
}