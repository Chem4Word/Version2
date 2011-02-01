// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using Chem4Word.Api.Properties;
using Numbo.Coa;

namespace Chem4Word.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ChemistryZoneProperties
    {
        private ChemistryZoneProperties()
        {
            // the default is to use a sans serif font
            OneDZoneFont = Resources.ChemistryFontSansSerif;
        }

        public ChemistryZoneProperties(string documentDepictionOptionXPath, string navigatorDepictionOptionXPath,
                                       bool collapseNavigatorDepiction)
            : this()
        {
            DocumentDepictionOptionXPath = documentDepictionOptionXPath;
            NavigatorDepictionOptionXPath = navigatorDepictionOptionXPath;
            CollapseNavigatorDepiction = collapseNavigatorDepiction;
        }

        //public ChemistryZoneProperties(DepictionOption documentDepictionOption, DepictionOption navigatorDepictionOption)
        //    : this()
        //{
        //    DocumentDepictionOptionXPath = documentDepictionOption.AbsoluteXPathToDepictionOption;
        //    NavigatorDepictionOptionXPath = navigatorDepictionOption.AbsoluteXPathToDepictionOption;
            
        //}

        public ChemistryZoneProperties(DepictionOption documentDepictionOption, DepictionOption navigatorDepictionOption, bool collapseNavigatorDepiction)
            : this()
        {
            DocumentDepictionOptionXPath = documentDepictionOption.AbsoluteXPathToDepictionOption;
            NavigatorDepictionOptionXPath = navigatorDepictionOption.AbsoluteXPathToDepictionOption;
            CollapseNavigatorDepiction = collapseNavigatorDepiction;
        }

        /// <summary>
        /// Gets _documentDepiction information.
        /// </summary>
        public string DocumentDepictionOptionXPath { get; private set; }

        /// <summary>
        /// Gets navigator _documentDepiction.
        /// </summary>
        public string NavigatorDepictionOptionXPath { get; private set; }

        public string OneDZoneFont { get; set; }
        public bool CollapseNavigatorDepiction { get; set; }

        public void SetDocumentDepictionOption(DepictionOption documentDepictionOption)
        {
            if (documentDepictionOption == null)
            {
                throw new ArgumentNullException("documentDepictionOption");
            }
            DocumentDepictionOptionXPath = documentDepictionOption.AbsoluteXPathToDepictionOption;
        }

        public void SetNavigatorDepictionOption(DepictionOption navigatorDepictionOption)
        {
            if (navigatorDepictionOption == null)
            {
                throw new ArgumentNullException("navigatorDepictionOption");
            }
            NavigatorDepictionOptionXPath = navigatorDepictionOption.AbsoluteXPathToDepictionOption;
        }

        public ChemistryZoneProperties Clone()
        {
            var cloned = new ChemistryZoneProperties
                                                 {
                                                     OneDZoneFont = OneDZoneFont,
                                                     DocumentDepictionOptionXPath = DocumentDepictionOptionXPath,
                                                     NavigatorDepictionOptionXPath = NavigatorDepictionOptionXPath,
                                                     CollapseNavigatorDepiction = CollapseNavigatorDepiction
                                                 };
            return cloned;
        }
    }
}