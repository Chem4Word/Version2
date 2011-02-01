// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using log4net;
using Numbo;

namespace Chem4Word.UI.TwoD
{
    /// <summary>
    /// Interaction logic for DragControl.xaml
    /// </summary>
    public partial class DragControl
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (DragControl));
        private double initialPanX;
        private double initialPanY;
        private double mouseDownX;
        private double mousDownY;
        private bool isTracking;
        private ButtonDirection buttonDirection = ButtonDirection.None;
        private Timer timer;
        private const double Increment = 10;

        public DragControl()
        {
            this.InitializeComponent();

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                TimerCallback tc = new TimerCallback(TimerCallback);
                timer = new Timer(TimerCallback, null, 100, 100);
            }
        }

        #region ChemCanvas

        /// <summary>
        /// ChemCanvas Dependency Property
        /// </summary>
        public static readonly DependencyProperty ChemCanvasProperty =
            DependencyProperty.Register("ChemCanvas", typeof (ChemCanvas), typeof (DragControl),
                                        new FrameworkPropertyMetadata((ChemCanvas) null));

        /// <summary>
        /// Gets or sets the ChemCanvas property.  This dependency property 
        /// indicates ....
        /// </summary>
        public ChemCanvas ChemCanvas
        {
            get { return (ChemCanvas) GetValue(ChemCanvasProperty); }
            set { SetValue(ChemCanvasProperty, value); }
        }

        #endregion

        private void TimerCallback(object state)
        {
            Action a = delegate
                           {
                               switch (buttonDirection)
                               {
                                   case ButtonDirection.None:
                                       break;
                                   case ButtonDirection.North:
                                       ChemCanvas.PanY -= Increment;
                                       break;
                                   case ButtonDirection.East:
                                       ChemCanvas.PanX += Increment;
                                       break;
                                   case ButtonDirection.South:
                                       ChemCanvas.PanY += Increment;
                                       break;
                                   case ButtonDirection.West:
                                       ChemCanvas.PanX -= Increment;
                                       break;
                                   default:
                                       throw new NumboException("button direction is not valid");
                               }

                               if (buttonDirection != ButtonDirection.None)
                               {
                                   ChemCanvas.Init();
                                   ChemCanvas.Refresh();
                               }
                           };

            Dispatcher.Invoke(DispatcherPriority.Normal, a);
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(ellipse))
            {
                Log.Debug("Mouse down");
                initialPanX = ChemCanvas.PanX;
                initialPanY = ChemCanvas.PanY;
                isTracking = true;

                Point p = e.GetPosition(this);
                mouseDownX = p.X;
                mousDownY = p.Y;
            }
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isTracking)
            {
                Log.Debug("Mouse up");
                isTracking = false;
                Mouse.Capture(null);
            }
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (isTracking)
            {
                Log.Debug("Mouse move");
                Point p = e.GetPosition(this);
                ChemCanvas.PanX = initialPanX + p.X - mouseDownX;
                ChemCanvas.PanY = initialPanY + p.Y - mousDownY;

                ChemCanvas.Init();
                ChemCanvas.Refresh();
            }
        }

        private void North_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(North))
            {
                buttonDirection = ButtonDirection.North;
            }
        }

        private void East_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(North))
            {
                buttonDirection = ButtonDirection.East;
            }
        }

        private void South_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(North))
            {
                buttonDirection = ButtonDirection.South;
            }
        }

        private void West_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(North))
            {
                buttonDirection = ButtonDirection.West;
            }
        }

        private void Direction_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            buttonDirection = ButtonDirection.None;
        }

        private enum ButtonDirection
        {
            None,
            North,
            East,
            South,
            West
        }
    }
}