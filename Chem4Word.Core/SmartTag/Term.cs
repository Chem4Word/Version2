// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
namespace Chem4Word.Core.SmartTag
{
    /// <summary>
    /// This class represent an individual instance of Chemical term.
    /// </summary>
    internal class Term
    {
        private readonly string id;
        private readonly string value;

        /// <summary>
        /// Initializes a new instance of the Term class.
        /// </summary>
        /// <param name="value">Chemistcal term</param>
        /// <param name="moleculeId">Molecule id.</param>
        internal Term(string value, string moleculeId)
        {
            this.value = value;
            this.id = moleculeId;
        }

        /// <summary>
        /// Gets Chemical term.
        /// </summary>
        internal string Value
        {
            get { return this.value; }
        }

        /// <summary>
        /// Gets molecule id.
        /// </summary>
        internal string MoleculeId
        {
            get { return this.id; }
        }
    }
}