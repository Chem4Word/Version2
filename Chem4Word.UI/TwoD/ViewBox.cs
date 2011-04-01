// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using log4net;

namespace Chem4Word.UI.TwoD {
    public class ViewBox : FrameworkElement {
        private static double VisibleStrokeWidth {get { return 0.75;}}
        private static double HiddenStrokeWidth { get { return 10; } }
        private static double HalfHiddenStrokeWidth {
            get {return HiddenStrokeWidth/2;}
        }

        private static double TwiceHiddenStrokeWidth {get {return 2*HiddenStrokeWidth; }}
        private static double QuadrupleHiddenStrokeWidth { get { return 4 * HiddenStrokeWidth; } }


        private readonly Pen emptyPen = new Pen(Brushes.Transparent, 0);

        /// <summary>
        ///   QUESTION
        ///   should we pass this canvas around or can we always get to it as "Parent"
        /// </summary>
        private readonly ChemCanvas theCanvas;

        private readonly Pen visibleStrokePen = new Pen(Brushes.Red, VisibleStrokeWidth) {DashStyle = DashStyles.Dash};
        public EventHandler EastControlMouseDown;
        public EventHandler NorthControlMouseDown;
        public EventHandler NorthWestControlMouseDown;
        public EventHandler NorthEastControlMouseDown;
        public EventHandler SouthWestControlMouseDown;
        public EventHandler SouthEastControlMouseDown;
        public EventHandler SouthControlMouseDown;
        public EventHandler WestControlMouseDown;

        /// <summary>
        ///   Required to provide the extention functionallity from FrameworkElement
        /// </summary>
        private VisualCollection children;

        private RectangleGeometry eastSide;
        private EllipseGeometry northEastCorner;
        private RectangleGeometry northSide;
        private EllipseGeometry northWestCorner;
        private EllipseGeometry southEastCorner;
        private RectangleGeometry southSide;
        private EllipseGeometry southWestCorner;
        private RectangleGeometry westSide;

        public ViewBox(ChemCanvas chemCanvas) {
            children = new VisualCollection(this);
            theCanvas = chemCanvas;
            var toolTip = new ToolTip
            {
                Content = "move this box around to define the view in the document"
            };
            this.ToolTip = toolTip;
            Refresh();
        }

        public static double MininumWidth {
            get { return TwiceHiddenStrokeWidth; }
        }

        public static double MininumHeight {
            get { return TwiceHiddenStrokeWidth; }
        }

        /// <summary>
        ///   Provide a required override for the VisualChildrenCount property.
        /// </summary>
        protected override int VisualChildrenCount {
            get { return children.Count; }
        }

        public double X { get; private set; }
        public double Y { get; private set; }
        public double W { get; private set; }
        public double H { get; private set; }

        private void Refresh() {
            var drawingVisual = new DrawingVisual();
            var drawingContext =
                drawingVisual.RenderOpen();

            var initialRect = theCanvas.ContextObject.CoViewBox;

           var rect = new Rect(theCanvas.ToScreenX(initialRect.X), theCanvas.ToScreenY(initialRect.Y),
                                theCanvas.ToScreenX(initialRect.Width),
                                Math.Abs(theCanvas.ToScreenY(initialRect.Height)));

            if (rect.Width <(4 * HiddenStrokeWidth + 2 * VisibleStrokeWidth)) {
                rect.Width = 4 * HiddenStrokeWidth + 2 * VisibleStrokeWidth;
            }
            if (rect.Height < (4 * HiddenStrokeWidth + 2 * VisibleStrokeWidth))
            {
                rect.Height = 4 * HiddenStrokeWidth + 2 * VisibleStrokeWidth;
            }


            rect.X -= VisibleStrokeWidth;
            rect.Y -= VisibleStrokeWidth;
            rect.Width += TwiceHiddenStrokeWidth;
            rect.Height += TwiceHiddenStrokeWidth;

            X = theCanvas.FromScreenX(rect.X);
            Y = theCanvas.FromScreenY(rect.Y);
            W = Math.Abs(
               theCanvas.FromScreenX(rect.Width));
            H = Math.Abs(theCanvas.FromScreenY(rect.Height));

            var rectangleGeometry = new RectangleGeometry(rect);
            drawingContext.DrawGeometry(null, visibleStrokePen, rectangleGeometry);

            var westAndEastSize = new Size(HiddenStrokeWidth, rect.Height - QuadrupleHiddenStrokeWidth);

            westSide =
                new RectangleGeometry(
                    new Rect(new Point(rect.BottomLeft.X - HalfHiddenStrokeWidth, rect.Y + TwiceHiddenStrokeWidth),
                             westAndEastSize));
            drawingContext.DrawGeometry(Brushes.Transparent, emptyPen, westSide);

            eastSide =
                new RectangleGeometry(
                    new Rect(new Point(rect.BottomRight.X - HalfHiddenStrokeWidth, rect.Y + TwiceHiddenStrokeWidth),
                             westAndEastSize));
            drawingContext.DrawGeometry(Brushes.Transparent, emptyPen, eastSide);

            var northAndSouthSize = new Size(rect.Width - QuadrupleHiddenStrokeWidth, HiddenStrokeWidth);

            northSide =
                new RectangleGeometry(
                    new Rect(
                        new Point(rect.TopLeft.X + TwiceHiddenStrokeWidth, rect.TopLeft.Y - HalfHiddenStrokeWidth),
                        northAndSouthSize));
            drawingContext.DrawGeometry(Brushes.Transparent, emptyPen, northSide);

            southSide =
                new RectangleGeometry(
                    new Rect(
                        new Point(rect.BottomLeft.X + TwiceHiddenStrokeWidth, rect.BottomRight.Y - HalfHiddenStrokeWidth),
                        northAndSouthSize));
            drawingContext.DrawGeometry(Brushes.Transparent, emptyPen, southSide);

            northWestCorner = new EllipseGeometry(rect.TopLeft, TwiceHiddenStrokeWidth, TwiceHiddenStrokeWidth);
            drawingContext.DrawGeometry(Brushes.Transparent, emptyPen, northWestCorner);

            southWestCorner = new EllipseGeometry(rect.BottomLeft, TwiceHiddenStrokeWidth, TwiceHiddenStrokeWidth);
            drawingContext.DrawGeometry(Brushes.Transparent, emptyPen, southWestCorner);

            northEastCorner = new EllipseGeometry(rect.TopRight, TwiceHiddenStrokeWidth, TwiceHiddenStrokeWidth);
            drawingContext.DrawGeometry(Brushes.Transparent, emptyPen, northEastCorner);

            southEastCorner = new EllipseGeometry(rect.BottomRight, TwiceHiddenStrokeWidth, TwiceHiddenStrokeWidth);
            drawingContext.DrawGeometry(Brushes.Transparent, emptyPen, southEastCorner);

            

            drawingContext.Close();
            children.Add(drawingVisual);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            var position = e.GetPosition(this);
            if (westSide.Bounds.Contains(position)) {
                Cursor = Cursors.SizeWE;
            } else if (eastSide.Bounds.Contains(position)) {
                Cursor = Cursors.SizeWE;
            } else if (northSide.Bounds.Contains(position)) {
                Cursor = Cursors.SizeNS;
            } else if (southSide.Bounds.Contains(position)) {
                Cursor = Cursors.SizeNS;
            } else if (northWestCorner.Bounds.Contains(position)) {
                Cursor = Cursors.SizeNWSE;
            } else if (southWestCorner.Bounds.Contains(position)) {
                Cursor = Cursors.SizeNESW;
            } else if (northEastCorner.Bounds.Contains(position)) {
                Cursor = Cursors.SizeNESW;
            } else if (southEastCorner.Bounds.Contains(position)) {
                Cursor = Cursors.SizeNWSE;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);
            var position = e.GetPosition(this);
            if (westSide.Bounds.Contains(position)) {
                if (WestControlMouseDown != null) {
                    WestControlMouseDown(this, EventArgs.Empty);
                }
            } else if (eastSide.Bounds.Contains(position)) {
                if (EastControlMouseDown != null) {
                    EastControlMouseDown(this, EventArgs.Empty);
                }
            } else if (northSide.Bounds.Contains(position)) {
                if (NorthControlMouseDown != null) {
                    NorthControlMouseDown(this, EventArgs.Empty);
                }
            } else if (southSide.Bounds.Contains(position)) {
                if (SouthControlMouseDown != null) {
                    SouthControlMouseDown(this, EventArgs.Empty);
                }
            } else if (northWestCorner.Bounds.Contains(position)) {
                if (NorthWestControlMouseDown != null) {
                    NorthWestControlMouseDown(this, EventArgs.Empty);
                }
            }
            else if (southWestCorner.Bounds.Contains(position))
            {
                if (SouthWestControlMouseDown != null)
                {
                    SouthWestControlMouseDown(this, EventArgs.Empty);
                }
            }
            else if (northEastCorner.Bounds.Contains(position))
            {
                if (NorthEastControlMouseDown != null)
                {
                    NorthEastControlMouseDown(this, EventArgs.Empty);
                }
            }
            else if (southEastCorner.Bounds.Contains(position))
            {
                if (SouthEastControlMouseDown != null)
                {
                    SouthEastControlMouseDown(this, EventArgs.Empty);
                }
            }

            e.Handled = true;
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            var position = e.GetPosition(this);
            if (westSide.Bounds.Contains(position) ||eastSide.Bounds.Contains(position)||northSide.Bounds.Contains(position)||southSide.Bounds.Contains(position)
                ||northWestCorner.Bounds.Contains(position)||southWestCorner.Bounds.Contains(position)||northEastCorner.Bounds.Contains(position)||southEastCorner.Bounds.Contains(position)) {

                // show a help box
//                var contextMenu = new ContextMenu();
//                var menuItem = new MenuItem();
//                menuItem.Header = ;
//                contextMenu.Items.Add(menuItem);
//                this.ContextMenu=
//                contextMenu;
//                this.ContextMenu.BringIntoView();
            } else {
                
            }

            e.Handled = true;
        }

        /// <summary>
        ///   Provide a required override for the GetVisualChild method.
        /// </summary>
        /// <param name = "index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index) {
            if (index < 0 || index >= children.Count) {
                throw new ArgumentOutOfRangeException("index");
            }
            return children[index];
        }
    }
}