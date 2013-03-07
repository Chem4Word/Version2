// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Xml.Linq;
using Microsoft.Office.Interop.Word;
using Numbo.Coa;

namespace Chem4Word.Api
{
    /// <summary>
    /// This interface represent contract between Chemistry Zone to its client.
    /// </summary>
    public interface IChemistryZone
    {
        /// <summary>
        /// 
        /// </summary>
        string ID { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ContentControl ContentControl { get; }

        /// <summary>
        /// 
        /// </summary>
        XDocument Cml { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ChemistryZoneProperties Properties { get; set; }

        event EventHandler<EventArgs> ChemistryZoneUpdated;

        event EventHandler<EventArgs> ChemistryZonePropertiesUpdated;

        /// <summary>
        /// 
        /// </summary>
        void Choose();

        ContextObject AsContextObject();

        /// <summary>
        /// 
        /// </summary>
        void Refresh();

        void Unlock();

        void Lock();
    }
}