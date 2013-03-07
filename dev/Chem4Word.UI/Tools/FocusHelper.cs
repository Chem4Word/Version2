// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Chem4Word.UI.Tools
{
    public static class FocusHelper
    {
        public static void Focus(UIElement element)
        {
            if (!element.Focus())
            {
                element.Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() => element.Focus()));
            }
        }

        public static void FocusTextBoxCursorAtEnd(TextBox textBox)
        {
            Focus(textBox);
            textBox.SelectionStart = textBox.Text.Length;
        }

        public static void FocusTextBoxCursorAtStart(TextBox textBox)
        {
            Focus(textBox);
            textBox.SelectionStart = 0;
        }

        public static void FocusTextBoxCursorSelectAll(TextBox textBox)
        {
            Focus(textBox);
            textBox.SelectionStart = 0;
            textBox.SelectionLength = textBox.Text.Length;
        }
    }
}