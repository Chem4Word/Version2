// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using Chem4Word.Drawing.TwoD.Common;
using Chem4Word.UI.TwoD;

namespace Chem4Word.UI.Converters
{
    internal class DrawingModeToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                if (parameter.ToString().Equals("Atom"))
                {
                    return (DrawingMode) value == DrawingMode.Select;
                }
                if (parameter.ToString().Equals("Bond"))
                {
                    return (DrawingMode) value == DrawingMode.BondSelect;
                }
            }

            string param = parameter == null ? "null" : parameter.ToString();
            throw new NotSupportedException("Unsupported drawing mode " + param);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                var val = (bool) value;
                if (parameter.ToString().Equals("Atom")) {
                    return val ? DrawingMode.Select : DrawingMode.BondSelect;
                }

                if (parameter.ToString().Equals("Bond")) {
                    return val ? DrawingMode.BondSelect : DrawingMode.Select;
                }
            }

            throw new ArgumentException("Not supported");
        }
    }
}