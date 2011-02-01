// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Windows.Controls;
using System.Windows.Input;

namespace Chem4Word.UI.TwoD
{
    public class NoFocusScrollViewer : ScrollViewer
    {
        //This whole class is to prevent the ScrollViewer taking focus on mouse down. If we 
        //don't use this, the KeyBindings don't work.

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            // No op
        }

        //protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        //{
        //    // No op
        //}

        //protected override void OnKeyUp(System.Windows.Input.KeyEventArgs e)
        //{
        //    // No op
        //}
    }
}