// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Chem4Word.UI.Converters
{
    public sealed class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value;
            ListView listView =
                ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            // Get the index of a ListViewItem
            var index =
                listView.ItemContainerGenerator.IndexFromContainer(item);

            
            
            if (index%2 == 0)
            {
                return new SolidColorBrush(Brushes.LightBlue.Color) {Opacity = 0.35};
            } 
            else
            {
                return new SolidColorBrush(Brushes.Beige.Color) {Opacity = 0.35};
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
