// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Euclid
{
    /// <summary>
    /// utilities for real numbers and arrays of real numbers
    /// </summary>
    public class Real
    {
        public static Nullable<double> GetMedian(double[] doubleList)
        {
            Nullable<double> median = null;
            if (doubleList.Length > 0)
            {
                List<double> dList = new List<double>();
                foreach (double d in doubleList)
                {
                    dList.Add(d);
                }
                dList.Sort();
                median = dList[doubleList.Length/2];
            }
            return median;
        }
    }
}