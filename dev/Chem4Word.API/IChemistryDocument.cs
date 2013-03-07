// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Chem4Word.Api.Core;
using Chem4Word.Api.Events;
using Microsoft.Office.Interop.Word;

namespace Chem4Word.Api
{
    public interface IChemistryDocument : IEnumerable<IChemistryZone>
    {
        /// <summary>
        /// Gets Word document associated with this IChemistryDocument. 
        /// <see cref="Microsoft.Office.Interop.Word.Document"/>
        /// </summary>
        Document WordDocument { get; }

        /// <summary>
        /// Get the currently selected IChemistryZone.
        /// </summary>
        IChemistryZone SelectedChemistryZone { get; }

        /// <summary>
        /// Gets number of IChemistryZones in this document.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets Chem4Word ICore associated with this IChemistryDocument. 
        /// <see cref="ICoreClass"/>
        /// </summary>
        ICoreClass Core { get; }

        bool EventTurnOn { get; set; }
        IChemistryZone GetChemistryZone(string id);

        IChemistryZone DocumentContentControlAfterAdd(ContentControl newContentControl,
                                                      ChemistryZoneProperties newChemistryZoneProperties,
                                                      IChemistryZone sourceChemZone);

        IChemistryZone DocumentContentControlAfterAdd(ContentControl newContentControl, XDocument cml,
                                                      ChemistryZoneProperties properties);

        /// <summary>
        /// Get the collection of chemistry zones from this IChemistryDocument which are also bound to the same CML as this one
        /// </summary>
        /// <param name="chemistryZone"></param>
        /// <returns>a collection of the chemistry zones which are also bound to the same CML as this one</returns>
        ICollection<IChemistryZone> GetOtherCommonBindingZones(IChemistryZone chemistryZone);

        /// <summary>
        /// Rebind Document Content Control to new Cml file.
        /// </summary>
        /// <param name="chemistryZone">the IChemistryZone to which the </param>
        /// <param name="newChemistryZoneProperties"></param>
        IChemistryZone RebindDocumentContentControl(IChemistryZone chemistryZone,
                                                    ChemistryZoneProperties newChemistryZoneProperties);

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<ContentControlEventArgs> ContentControlAfterAdd;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<ContentControlEventArgs> ContentControlBeforeDelete;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<ContentControlEventArgs> ContentControlAfterDelete;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<ContentControlEventArgs> ContentControlOnEnter;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<ContentControlEventArgs> ContentControlOnExit;
    }
}