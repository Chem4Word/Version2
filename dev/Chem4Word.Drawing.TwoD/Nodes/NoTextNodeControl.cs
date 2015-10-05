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
using log4net;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.Drawing.TwoD.Nodes

{
    public class NoTextNodeControl : AbstractNodeControl
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (NoTextNodeControl));
        private DrawingVisual textAdornmentVisual;

        #region constructors

        public NoTextNodeControl(ContextObject contextObject,
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

        public override Rect RetrieveBounds
        {
            get { return GetBounds(textAdornmentVisual); }
        }

        protected override Rect GetBounds(Visual visual)
        {
            Rect temp = VisualTreeHelper.GetDescendantBounds(visual);

            if (temp.Width > 0 || temp.Height > 0)
            {
                EllipseGeometry centre = new EllipseGeometry(Attachment, 1d, 1d);
                RectangleGeometry bounds1 =
                    new RectangleGeometry(new Rect(temp.TopLeft.X - 2, temp.TopLeft.Y - 2, temp.Width + 4,
                                                   temp.Height + 4));

                CombinedGeometry cg = new CombinedGeometry(GeometryCombineMode.Union, centre, bounds1);
                return cg.Bounds;
            }

            return Rect.Empty;
        }

        /// <summary>
        /// <see cref="AbstractNodeControl#Refresh"/>
        /// </summary>
        protected override void Refresh()
        {
            children.Clear();

            #region FOR EDIT

            principalNodeText = new DrawingVisual();
            DrawingContext drawingContext;

            /// QUESTION 
            /// can we do this conversion to screen better?
            double x = canvas.ToScreenX(CmlAtom.Point2.X);

            double y = canvas.ToScreenY(CmlAtom.Point2.Y);

            //Log.Debug(string.Format(CultureInfo.InvariantCulture, "id {0} x, y: {1}, {2} X, Y: {3}, {4}", this.CmlAtom.Id, x, y, this.CmlAtom.Point2.X,
            //                        this.CmlAtom.Point2.Y));

            Attachment = new Point(x, y);
            double textOffsetWidth = 4.0;
            double textOffsetHeight = 8.0;

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
            if (null != frame)
            {
                children.Remove(frame);
            }
            frame = new DrawingVisual();
            isInitialised = true;
            drawingContext = frame.RenderOpen();
            double radius = framePadding + 3;

            frame.Opacity = 1;

            #endregion FOR EDIT

            SetPenAndBrush();

            drawingContext.DrawEllipse(fill, pen, new Point(x, y), radius, radius);
            drawingContext.Close();
            children.Insert(0, frame);
        }

        protected override void SetPenAndBrush()
        {
            if (IsActive)
            {
                pen = new Pen(Brushes.Orange, 1);
                if (IsSelected)
                {
                    fill = Brushes.Orange;
                    frame.Opacity = 0.5;
                }
                else
                {
                    fill = Brushes.Transparent;
                }
            }
            else
            {
                pen = null;
                if (IsSelected)
                {
                    fill = Brushes.Blue;
                    frame.Opacity = 0.25;
                }
                else
                {
                    fill = Brushes.Transparent;
                }
            }
        }

        #endregion private methods
    }
}