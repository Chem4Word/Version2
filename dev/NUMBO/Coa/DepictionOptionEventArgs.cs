// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;

namespace Numbo.Coa
{
    /// <summary>
    /// Class for Depiction Option Changed event arguments.
    /// </summary>
    public class DepictionOptionEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the selected depiction option for Chemistry zones.
        /// </summary>
        public DepictionOption NewDepictionOption
        { get; set; }

        /// <summary>
        /// Gets or sets the old depiction option for Chemistry zones.
        /// </summary>
        public DepictionOption OldDepictionOption
        { get; set; }
    }
}