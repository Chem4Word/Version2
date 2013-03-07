// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Chem4Word.UI.WebServices;

namespace Chem4Word.UI.Converters
{
    /// <summary>
    /// returns true if the observable collection contains items - otherwise false
    /// </summary>
    class ObservableCollectionToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var observableCollection = value as ObservableCollection<PubChemResultItem>;
            if (observableCollection != null)
            {
                return observableCollection.Count > 0;
            }
            return false;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
