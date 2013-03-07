// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using Chem4Word.Api.Events;
using Microsoft.Office.Interop.Word;
using Numbo.Coa;

namespace Chem4Word.Api.Core
{
    public interface ICoreClass
    {
        /// <summary>
        /// 
        /// </summary>
        event EventHandler<ContentControlEventArgs> ContentControlSelectionChanged;

        /// <summary>
        /// Gets the active chemistry document.
        /// </summary>
        /// <returns>Return the active document <see cref="IChemistryDocument"/></returns>
        IChemistryDocument ActiveChemistryDocument { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chemistryZone"></param>
        /// <param name="chemistryZonePropertiesForCopiedZone">if null then uses current values in the chemistryZone</param>
        /// <returns></returns>
        IChemistryZone CreateCopy(IChemistryZone chemistryZone, ChemistryZoneProperties chemistryZonePropertiesForCopiedZone);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chemistryZone"></param>
        /// <param name="chemistryZonePropertiesForLinkedZone">if null then uses current values in the chemistryZone</param>
        /// <returns></returns>
        IChemistryZone CreateLink(IChemistryZone chemistryZone, ChemistryZoneProperties chemistryZonePropertiesForLinkedZone);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="chemistryZone"></param>
        /// <returns></returns>
        IChemistryZone BreakLinks(IChemistryZone chemistryZone);

        IChemistryZone ImportCmlFile(string fileName);

        /// <summary>
        /// Takes the DepictionOption and converts the content to a suitable format (ie OMML) 
        /// for inclusion in the document. An OMaths zone is then added to the richTextContentControl 
        /// and the value set to be the OMML. OMaths.BuildUp() is called then font set to the chemistry font. 
        /// </summary>
        /// <param name="richTextContentControl"></param>
        /// <param name="oneDDepictionOption"></param>
        /// <exception cref="ArgumentNullException">if oneDDepictionOption is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">if the DepictionOption is not for a 1D zone</exception>
        void CreateOneDZoneContent(ref ContentControl richTextContentControl, DepictionOption oneDDepictionOption);
    }
}