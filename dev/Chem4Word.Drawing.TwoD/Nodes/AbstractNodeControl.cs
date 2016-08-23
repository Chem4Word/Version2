// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Chem4Word.Drawing.TwoD.Common;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.Drawing.TwoD.Nodes
{
    /// <summary>
    /// Provides the general functionality for nodes but requires that the 
    /// actual visual depiction is overriden for each instance class.
    /// </summary>
    public abstract class AbstractNodeControl : FrameworkElement, INode
    {
        protected AbstractNodeControl()
        {
            Cursor = Cursors.Arrow;
        }

        #region public properties

        public virtual Rect RetrieveBounds
        {
            get { return GetBounds(this); }
        }

        /// <summary>
        /// The point at which an Attachment (edge) should point at
        /// </summary>
        public Point Attachment { protected set; get; }

        /// <summary>
        /// Is this atom currently selected
        /// </summary>
        public bool IsSelected
        {
            get { return canvas.SelectedAtoms.ContainsKey(CmlAtom.Id); }
            set
            {
                if (value == false)
                {
                    if (canvas.SelectedAtoms.ContainsKey(CmlAtom.Id))
                    {
                        canvas.SelectedAtoms.Remove(CmlAtom.Id);
                    }
                }
                else
                {
                    if (!canvas.SelectedAtoms.ContainsKey(CmlAtom.Id))
                    {
                        canvas.SelectedAtoms.Add(CmlAtom.Id, CmlAtom);
                    }
                }
            }
        }

        /// <summary>
        /// Is this the active atom
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The CmlAtom which this NodeControl wraps
        /// </summary>
        public CmlAtom CmlAtom { protected set; get; }

        protected virtual Rect GetBounds(Visual visual)
        {
            Rect temp = VisualTreeHelper.GetDescendantBounds(visual);

            if (temp.Width > 0)
            {
                return new Rect(temp.TopLeft.X - 2, temp.TopLeft.Y - 2, temp.Width + 4, temp.Height + 4);
            }

            return Rect.Empty;
        }

        /// <summary>
        /// Indicates the underlying CML has changed
        /// </summary>
        public event EventHandler<CmlChangedEventArgs> CmlChangedEvent;

        #endregion public properties

        #region private methods

        /// <summary>
        /// Provide a required override for the VisualChildrenCount property.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return children.Count; }
        }

        /// <summary>
        /// Create the on screen representation of this node.
        /// Must be overriden by each specific node control.
        /// </summary>
        protected abstract void Refresh();

        public void Invalidate()
        {
            Refresh();
        }

        /// <summary>
        /// Initialise the node control and draw it
        /// </summary>
        protected void Init()
        {
            IsActive = false;
            labelCentreText = CmlAtom.ElementType;
            Refresh();
        }

        ///// <summary>
        ///// Handle events raised by clicking on menu item
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void MenuClick(object sender, RoutedEventArgs e)
        //{
        //    string newElementType = ((MenuItem) sender).Header.ToString();
        //    if (!newElementType.Equals(principalNodeText))
        //    {
        //        canvas.ConnectionTableChanged = true;
        //    }
        //    ContextObject co = Cid.ChangeElementType(contextObject, new[] {CmlAtom.DelegateElement},
        //                                             newElementType);
        //    CmlChangedEvent(this, new CmlChangedEventArgs(co));
        //}

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            if (! MouseButtonState.Pressed.Equals(e.LeftButton))
            {
                IsActive = false;
            }

            Refresh();
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            IsActive = true;
            Refresh();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (NodeMouseDown != null)
            {
                NodeMouseDown(this, EventArgs.Empty);
            }

            e.Handled = true;
        }

        protected virtual void SetPenAndBrush()
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
                    fill = null;
                }
            }
        }

        /// <summary>
        /// Provide a required override for the GetVisualChild method.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= children.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return children[index];
        }

        #endregion private methods

        #region private properties

        /// <summary>
        /// QUESTION
        /// should we pass this canvas around or can we always get to it as "Parent"
        /// </summary>
        protected IChemCanvas canvas;

        /// <summary>
        /// Required to provide the extention functionallity from FrameworkElement
        /// </summary>
        protected VisualCollection children;

        /// <summary>
        /// QUESTION
        /// should we pass the contextObject around because it is always the same one
        /// and could be accessed from "Parent"
        /// </summary>
        protected ContextObject contextObject;

        protected Brush fill;

        /// <summary>
        /// The area around the (in)visible text which can be selected
        /// </summary>
        protected DrawingVisual frame;

        protected double framePadding = 12;

        protected bool isInitialised;

        /// <summary>
        /// the following properties should be derived from ChemSS in future
        /// </summary>
        protected string labelCentreText;

        protected Pen pen = new Pen(Brushes.Blue, 0.5);
        protected DrawingVisual principalNodeText;

        #endregion private properties

        public event EventHandler NodeMouseDown;

        #region Implementation of IVisual

        public FrameworkElement AsVisual()
        {
            return this;
        }

        #endregion
    }
}