// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using Chem4Word.UI.Commands;
using log4net;
using Numbo;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.UI.TwoD
{
    public abstract class AbstractEdgeControl : FrameworkElement
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (AbstractEdgeControl));
        private bool hasMouseDownedOnBond;
        private Point initialMouseDownPoint;
        private long mouseDownTicks;

        public AbstractEdgeControl()
        {
            //IsHitTestVisible = false;
            Cursor = Cursors.Arrow;
        }

        protected DirectionalSelectionMode DirectionalSelectionMode { get; private set; }

        #region public properties

        /// <summary>
        /// Is this bond currently selected
        /// </summary>
        public bool IsSelected
        {
            get { return canvas.SelectedBonds.ContainsKey(Bond.Id); }
            set
            {
                if (value == false)
                {
                    if (canvas.SelectedBonds.ContainsKey(Bond.Id))
                    {
                        canvas.SelectedBonds.Remove(Bond.Id);
                    }
                }
                else
                {
                    if (!canvas.SelectedBonds.ContainsKey(Bond.Id))
                    {
                        canvas.SelectedBonds.Add(Bond.Id, Bond);
                    }
                }
            }
        }

        /// <summary>
        /// Is this the active bond
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The CMLBond which this EdgeControl wraps
        /// </summary>
        public CmlBond Bond { get; protected set; }

        #endregion public properties

        public event EventHandler EdgeClicked;

        /// <summary>
        /// Create the on screen representation of this node.
        /// Must be overriden by each specific node control.
        /// </summary>
        protected abstract void Refresh();

        public void Invalidate()
        {
            Refresh();
        }

        protected LineGeometry GetInitialBondGeometry()
        {
            Point start = StartNode.Attachment;

            Point end = EndNode.Attachment;

            label.Opacity = 0.5;

            //Set up the geometries for the bounding rectangles of the atoms.
            RectangleGeometry startRectangle = new RectangleGeometry(StartNode.RetrieveBounds);
            RectangleGeometry endRectangle = new RectangleGeometry(EndNode.RetrieveBounds);

            // Let's try drawing the bond as a very thin rectangle.
            // What's our first stab at a bond length?
            double deltaX = end.X - start.X;
            double deltaY = end.Y - start.Y;
            double initialBondLength = Math.Sqrt(Math.Pow(deltaX, 2.0) + Math.Pow(deltaY, 2.0));

            // Now let's create our bond with it's origin on the start node's coords.
            Rect bondRect = new Rect(new Point(start.X, start.Y - 0.001),
                                     new Point(start.X + initialBondLength, start.Y + 0.002));

            // Now let's rotate.
            double bondRotationAngle = Math.Atan2(deltaY, deltaX)*180/Math.PI;
            RotateTransform bondRotation = new RotateTransform(bondRotationAngle, start.X, start.Y);
            RectangleGeometry bondGeometry = new RectangleGeometry(bondRect, 1.5, 1.5, bondRotation);

            CombinedGeometry combinedGeometry1 = new CombinedGeometry(GeometryCombineMode.Exclude, bondGeometry,
                                                                      startRectangle);
            CombinedGeometry combinedGeometry2 = new CombinedGeometry(GeometryCombineMode.Exclude, combinedGeometry1,
                                                                      endRectangle);

            double newBondLength =
                Math.Sqrt(Math.Pow(combinedGeometry2.Bounds.Width, 2.0) + Math.Pow(combinedGeometry2.Bounds.Height, 2.0));
            Point nearestPointToOrigin = new Point();

            if (bondRotationAngle <= 0 && bondRotationAngle >= -90)
            {
                nearestPointToOrigin = combinedGeometry2.Bounds.BottomLeft;
            }
            else if (bondRotationAngle <= -90 && bondRotationAngle >= -180)
            {
                nearestPointToOrigin = combinedGeometry2.Bounds.BottomRight;
            }
            else if (bondRotationAngle > 0 && bondRotationAngle <= 90)
            {
                nearestPointToOrigin = combinedGeometry2.Bounds.TopLeft;
            }
            else if (bondRotationAngle >= 90 && bondRotationAngle <= 180)
            {
                nearestPointToOrigin = combinedGeometry2.Bounds.TopRight;
            }
            else
            {
                throw new NumboException(
                    "Something has changed in layout-world. The angle has strayed outside the expected ranges.");
            }

            double offset =
                Math.Sqrt(Math.Pow(nearestPointToOrigin.X - start.X, 2.0) +
                          Math.Pow(nearestPointToOrigin.Y - start.Y, 2.0));

            bondRotationAngle = bondRotationAngle*Math.PI/180;

            //double startX = start.X + offset * Math.Cos(bondRotationAngle);
            //double startY = start.Y + offset * Math.Sin(bondRotationAngle);
            //double endX = end.X - offset * Math.Cos(bondRotationAngle);
            //double endY = end.Y - offset * Math.Sin(bondRotationAngle);

            double startX = start.X + offset*Math.Cos(bondRotationAngle);
            double startY = start.Y + offset*Math.Sin(bondRotationAngle);
            double endX = startX + newBondLength*Math.Cos(bondRotationAngle);
            double endY = startY + newBondLength*Math.Sin(bondRotationAngle);

            LineGeometry finalBondGeometry = new LineGeometry(new Point(startX, startY), new Point(endX, endY));
            //drawingContext.PushClip(clipGeometry);
            return finalBondGeometry;
        }

        #region private methods        

        /// <summary>
        /// Provide a required override for the VisualChildrenCount property.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return this.children == null ? 0 : this.children.Count; }
        }

        /// <summary>
        /// Initialise the node control and draw it
        /// </summary>
        protected void Init()
        {
            this.IsActive = false;
            Refresh();
        }

        protected Geometry GetHitTestRectangle(LineGeometry finalBondGeometry)
        {
            Vector bondVector = new Vector(finalBondGeometry.EndPoint.X - finalBondGeometry.StartPoint.X,
                                           finalBondGeometry.EndPoint.Y - finalBondGeometry.StartPoint.Y);
            Vector perpToBondVector = new Vector(-bondVector.Y, bondVector.X);
            perpToBondVector.Normalize();
            perpToBondVector *= 5;

            StreamGeometry hitTestRectangle = new StreamGeometry();

            Point p1 = finalBondGeometry.StartPoint + perpToBondVector;
            Point p2 = finalBondGeometry.EndPoint + perpToBondVector;
            Point p3 = finalBondGeometry.EndPoint - perpToBondVector;
            Point p4 = finalBondGeometry.StartPoint - perpToBondVector;

            using (StreamGeometryContext ctx = hitTestRectangle.Open())
            {
                ctx.BeginFigure(p1, true, true);
                ctx.LineTo(p2, true, false);
                ctx.LineTo(p3, true, false);
                ctx.LineTo(p4, true, false);
            }
            return hitTestRectangle;
        }

        protected StreamGeometry GetArrow(LineGeometry finalBondGeometry)
        {
            Vector bondVector = new Vector(finalBondGeometry.EndPoint.X - finalBondGeometry.StartPoint.X,
                                           finalBondGeometry.EndPoint.Y - finalBondGeometry.StartPoint.Y);
            double bondLength = bondVector.Length;
            bondVector.Normalize();
            Vector perpToBondVector = new Vector(-bondVector.Y, bondVector.X);
            Vector bisectorVector;
            StreamGeometry arrow = null;

            switch (DirectionalSelectionMode)
            {
                case DirectionalSelectionMode.None:
                    break;
                case DirectionalSelectionMode.Backwards:
                    bisectorVector = new Vector(bondVector.X + perpToBondVector.X, bondVector.Y + perpToBondVector.Y);
                    bisectorVector.Normalize();
                    Point p1 = finalBondGeometry.StartPoint + bondVector*bondLength*2/3 + perpToBondVector*5;
                    Point p2 = finalBondGeometry.StartPoint + perpToBondVector*5;
                    Point p3 = p2 + bisectorVector*10;
                    arrow = new StreamGeometry();
                    using (StreamGeometryContext ctx = arrow.Open())
                    {
                        ctx.BeginFigure(p1, false, false);
                        ctx.LineTo(p2, true, true);
                        ctx.LineTo(p3, true, true);
                    }
                    break;
                case DirectionalSelectionMode.Forwards:
                    bisectorVector = new Vector(-bondVector.X + perpToBondVector.X, -bondVector.Y + perpToBondVector.Y);
                    bisectorVector.Normalize();
                    p1 = finalBondGeometry.StartPoint + bondVector*bondLength/3 + perpToBondVector*5;
                    p2 = finalBondGeometry.EndPoint + perpToBondVector*5;
                    p3 = p2 + bisectorVector*10;
                    arrow = new StreamGeometry();
                    using (StreamGeometryContext ctx = arrow.Open())
                    {
                        ctx.BeginFigure(p1, false, false);
                        ctx.LineTo(p2, true, true);
                        ctx.LineTo(p3, true, true);
                    }
                    break;
                default:
                    throw new NumboException("directional selection is not valid");
            }
            return arrow;
        }

        protected virtual void SetPenAndBrush()
        {
            if (IsActive || IsMouseOver || IsSelected)
            {
                pen = new Pen(Brushes.Orange, 2);
                fill = IsSelected ? Brushes.Orange : null;
            }
            else
            {
                pen = canvas.IsCmlValid ? new Pen(Brushes.Blue, 2) : new Pen(Brushes.Red, 2);
                fill = IsSelected ? Brushes.Blue : null;
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

        /// <summary>
        /// Provide a required override for the GetVisualChild method.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= this.children.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return this.children[index];
        }

        #endregion private methods

        #region Overrides

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            IsActive = true;

            Refresh();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            IsActive = false;

            Refresh();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            mouseDownTicks = DateTime.Now.Ticks;
            initialMouseDownPoint = e.GetPosition(canvas);
            hasMouseDownedOnBond = true;
            canvas.IsPerformingDirectionBondSelect = true;
            Mouse.Capture(this);

            e.Handled = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            hasMouseDownedOnBond = false;
            DirectionalSelectionMode = DirectionalSelectionMode.None;
            Refresh();

            if (Mouse.Captured == this)
            {
                Mouse.Capture(null);
            }

            if (canvas.IsPerformingDirectionBondSelect && canvas.DrawingMode == CanvasContainer.DrawingMode.Delete)
            {
                //canvas.DeletePressed();
                ChemCommands.DeleteSelection.Execute(null, canvas);
            }
            canvas.IsPerformingDirectionBondSelect = false;

            if (TimeSpan.FromTicks(DateTime.Now.Ticks - mouseDownTicks) < TimeSpan.FromMilliseconds(300))
            {
                if (canvas.DrawingMode == CanvasContainer.DrawingMode.Delete)
                {
                    XElement[] bond = {Bond.DelegateElement};
                    canvas.TakeSnapshotOfContextObject();
                    Cid.DeleteBondsAndFixChemistry(contextObject, bond);
                    canvas.Refresh();
                }
                else if (canvas.DrawingMode == CanvasContainer.DrawingMode.BondSelect)
                {
                    if (EdgeClicked != null)
                    {
                        EdgeClicked(this, EventArgs.Empty);
                    }
                }
            }
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);

            mouseDownTicks = DateTime.Now.Ticks;
        }

        private IEnumerable<XElement> GetThisAsXEelement()
        {
            return new[] {Bond.DelegateElement};
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (hasMouseDownedOnBond)
            {
                if (canvas.DrawingMode != CanvasContainer.DrawingMode.BondSelect)
                {
                    Point currentLoc = e.GetPosition(canvas);
                    currentLoc = new Point(currentLoc.X - canvas.StandardXOffset,
                                           currentLoc.Y - canvas.StandardYOffset);

                    // Is it in the mouse in the 'select back' rectangle??
                    Geometry backGeometry = GetSelectBackGeometry();
                    Geometry forwardGeometry = GetSelectForwardGeometry();

                    if (backGeometry.FillContains(currentLoc))
                    {
                        DirectionalSelectionMode = DirectionalSelectionMode.Backwards;
                        Refresh();

                        canvas.DoDirectionalSelect(Bond, StartNode.CmlAtom);

                        return;
                    }

                    if (forwardGeometry.FillContains(currentLoc))
                    {
                        DirectionalSelectionMode = DirectionalSelectionMode.Forwards;
                        Refresh();

                        canvas.DoDirectionalSelect(Bond, EndNode.CmlAtom);

                        return;
                    }

                    Log.Debug(string.Format(CultureInfo.InvariantCulture, "Select None - Mouse = {0}", currentLoc));
                    DirectionalSelectionMode = DirectionalSelectionMode.None;
                    canvas.DoDirectionalSelect(null, null);
                    Refresh();
                    return;
                }
            }
        }

        protected Geometry GetSelectForwardGeometry()
        {
            LineGeometry bondLine = GetInitialBondGeometry();
            Vector bondVector = new Vector(bondLine.EndPoint.X - bondLine.StartPoint.X,
                                           bondLine.EndPoint.Y - bondLine.StartPoint.Y);
            double bondLength = bondVector.Length;
            bondVector.Normalize();
            Vector perpToBondVector = new Vector(-bondVector.Y, bondVector.X);
            perpToBondVector.Normalize();
            perpToBondVector *= 200;

            Point p1 = bondLine.EndPoint + perpToBondVector + bondVector*200;
            Point p2 = p1 - bondVector*(bondLength/3 + 200);
            Point p3 = p2 - perpToBondVector*2;
            Point p4 = p3 + bondVector*(bondLength/3 + 200);

            StreamGeometry backGeometry = new StreamGeometry();

            using (StreamGeometryContext ctx = backGeometry.Open())
            {
                ctx.BeginFigure(p1, true, true);
                ctx.LineTo(p2, true, false);
                ctx.LineTo(p3, true, false);
                ctx.LineTo(p4, true, false);
            }

            return backGeometry;
        }

        protected Geometry GetSelectBackGeometry()
        {
            LineGeometry bondLine = GetInitialBondGeometry();
            Vector bondVector = new Vector(bondLine.EndPoint.X - bondLine.StartPoint.X,
                                           bondLine.EndPoint.Y - bondLine.StartPoint.Y);
            double bondLength = bondVector.Length;
            bondVector.Normalize();
            Vector perpToBondVector = new Vector(-bondVector.Y, bondVector.X);
            perpToBondVector.Normalize();
            perpToBondVector *= 200;

            Point p1 = bondLine.StartPoint + perpToBondVector - bondVector*200;
            Point p2 = p1 + bondVector*(bondLength/3 + 200);
            Point p3 = p2 - perpToBondVector*2;
            Point p4 = p3 - bondVector*(bondLength/3 + 200);

            StreamGeometry backGeometry = new StreamGeometry();

            using (StreamGeometryContext ctx = backGeometry.Open())
            {
                ctx.BeginFigure(p1, true, true);
                ctx.LineTo(p2, true, false);
                ctx.LineTo(p3, true, false);
                ctx.LineTo(p4, true, false);
            }

            return backGeometry;
        }

        #endregion

        #region private properties

        /// <summary>
        /// QUESTION
        /// should we pass this canvas around or can we always get to it as "Parent"
        /// </summary>
        protected ChemCanvas canvas;

        /// <summary>
        /// Required to provide the extention functionallity from FrameworkElement
        /// </summary>
        protected VisualCollection children;

        /// <summary>
        /// The context menu associated with this node control
        /// </summary>
        protected ContextMenu contextMenu;

        /// <summary>
        /// QUESTION
        /// should we pass the contextObject around because it is always the same one
        /// and could be accessed from "Parent"
        /// </summary>
        protected ContextObject contextObject;

        /// <summary>
        /// the following properties should be derived from ChemSS in future
        /// </summary>
        protected Brush fill;

        protected DrawingVisual frame;
        protected double framePadding = 12;
        protected DrawingVisual label;

//        protected System.Windows.Media.Pen pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.SlateBlue, 1.5);
        protected Pen pen = new Pen(Brushes.Black, 1.5);
        public AbstractNodeControl StartNode { get; internal set; }
        public AbstractNodeControl EndNode { get; internal set; }

        protected Brush ArrowFill { get; set; }
        protected Pen ArrowPen { get; set; }

        #endregion private properties
    }

    public enum DirectionalSelectionMode
    {
        None,
        Backwards,
        Forwards
    }
}