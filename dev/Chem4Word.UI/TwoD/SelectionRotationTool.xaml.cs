// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Numbo.Cml;

namespace Chem4Word.UI.TwoD
{
    /// <summary>
    /// Interaction logic for SelectionRotationTool.xaml
    /// </summary>
    public partial class SelectionRotationTool
    {
        public SelectionRotationTool()
        {
            InitializeComponent();
        }

        internal void Initialise(ChemCanvas canvas, SortedDictionary<string, CmlAtom> selectedAtoms, double xOffset,
                                 double yOffset)
        {
            Rect rect = CoordinateTool.GetBounds2D(selectedAtoms.Values);

            double marginExtra = 20;

            var p1 = new Point(canvas.ToScreenX(rect.TopLeft.X) - marginExtra + xOffset,
                               canvas.ToScreenY(rect.TopLeft.Y) + marginExtra + yOffset);
            var p2 = new Point(canvas.ToScreenX(rect.TopRight.X) + marginExtra + xOffset,
                               canvas.ToScreenY(rect.TopRight.Y) + marginExtra + yOffset);
            var p3 = new Point(canvas.ToScreenX(rect.BottomRight.X) + marginExtra + xOffset,
                               canvas.ToScreenY(rect.BottomRight.Y) - marginExtra + yOffset);
            var p4 = new Point(canvas.ToScreenX(rect.BottomLeft.X) - marginExtra + xOffset,
                               canvas.ToScreenY(rect.BottomLeft.Y) - marginExtra + yOffset);

            Width = p2.X - p1.X;
            Height = p1.Y - p3.Y + 45;

            Canvas.SetLeft(this, p1.X);
            Canvas.SetTop(this, p4.Y - 45);
        }

        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);

        //    if (Mouse.Captured == this && e.MouseDevice.LeftButton == MouseButtonState.Pressed)
        //    {
        //        Point p = e.GetPosition(this);

        //        Vector v1 = new Point(Width/2, 0) - _centreOfSelection;
        //        Vector v2 = p - _centreOfSelection;

        //        v1.Normalize();
        //        v2.Normalize();

        //        double newAngle = Math.Atan2(v2.Y, v2.X) - Math.Atan2(v1.Y, v1.X);
        //        _lastAngle = newAngle * 180 / Math.PI* 2;

        //        log.Debug(_lastAngle);

        //        _rotateTransform.Angle = _lastAngle;
        //    }

        //    e.Handled = true;
        //}
    }
}