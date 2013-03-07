// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;

namespace Chem4Word.Core.SmartTag
{
    [Serializable]
    internal class ChemistryZoneMatch
    {
        /// <summary>
        /// The MoleculeId of the chemistry zone.
        /// </summary>
        internal string MoleculeId { get; set; }

        /// <summary>
        /// The ContentId of the Zone.
        /// </summary>
        internal string ContentId { get; set; }

        /// <summary>
        /// The chemistry zone text.
        /// </summary>
        internal string Text { get; set; }

        /// <summary>
        /// Start index of the chemistry zone in the document.
        /// </summary>
        internal int StartIndex { get; set; }

        /// <summary>
        /// End index of the chemistry zone in the document.
        /// </summary>
        internal int EndIndex { get; set; }
    }
}