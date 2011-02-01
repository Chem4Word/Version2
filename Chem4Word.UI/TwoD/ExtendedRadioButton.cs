// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Windows;
using System.Windows.Controls;

namespace Chem4Word.UI.TwoD
{
    public class RadioButtonExtended : RadioButton
    {
        static bool _isChanging;

        public RadioButtonExtended()
        {
            Checked += RadioButtonExtended_Checked;
            Unchecked += RadioButtonExtended_Unchecked;
        }

        void RadioButtonExtended_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!_isChanging)
                IsCheckedReal = false;
        }

        void RadioButtonExtended_Checked(object sender, RoutedEventArgs e)
        {
            if (!_isChanging)
                IsCheckedReal = true;
        }

        public bool? IsCheckedReal
        {
            get { return (bool?)GetValue(IsCheckedRealProperty); }
            set
            {
                SetValue(IsCheckedRealProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for IsCheckedReal. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedRealProperty =
        DependencyProperty.Register("IsCheckedReal", typeof(bool?), typeof(RadioButtonExtended),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Journal |
        FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
        IsCheckedRealChanged));

        public static void IsCheckedRealChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            _isChanging = true;
            ((RadioButtonExtended)d).IsChecked = (bool)e.NewValue;
            _isChanging = false;
        }
    }
}