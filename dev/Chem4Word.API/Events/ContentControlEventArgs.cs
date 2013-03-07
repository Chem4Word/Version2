// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;

namespace Chem4Word.Api.Events
{
    /// <summary>
    /// Class for Control Control event arguments.
    /// </summary>
    public class ContentControlEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the ChemistryZone
        /// </summary>
        public IChemistryZone ContentChemistryZone { get; set; }

        /// <summary>
        /// Gets or sets a boolean value indicating the action required.
        /// </summary>
        public bool Action { get; set; }
    }
}