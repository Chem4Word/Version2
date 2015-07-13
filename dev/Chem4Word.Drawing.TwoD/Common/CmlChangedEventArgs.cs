// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------

using System;
using Numbo.Coa;

namespace Chem4Word.Drawing.TwoD.Common
{
    /// <summary>
    /// A class to encapsulate all the things required in a CmlChangeEvent
    /// </summary>
    public class CmlChangedEventArgs : EventArgs
    {
        public CmlChangedEventArgs(ContextObject contextObject)
        {
            this.ContextObject = contextObject;
        }

        public ContextObject ContextObject { get; private set; }
    }
}