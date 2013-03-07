// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using Chem4Word.Api;

namespace Chem4Word.Core.Events
{
    /// <summary>
    /// Class for chemistry document changed event args.
    /// </summary>
    public class ChemistryDocumentEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the current word document.
        /// </summary>
        public Microsoft.Office.Interop.Word.Document CurrentDocument { get; set; }

        /// <summary>
        /// Gets or sets the Chemistry document collection.
        /// </summary>
        public IChemistryDocument ChemistryDocument { get; set; }

        /// <summary>
        /// Gets or sets the current word window.
        /// </summary>
        public Microsoft.Office.Interop.Word.Window CurrentWindow { get; set; }
    }
}