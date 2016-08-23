// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Xml.Linq;
using System.Xml.XPath;
using Numbo.Cml;

namespace Chem4Word.UI.Import
{
    internal class CmlLiteExistenceValidator : IValidator
    {
        private readonly ImportMediator mediator;
        private string briefMessage = string.Empty;

        private XDocument document;
        private string longMessage = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">The mediator to control the data flow</param>
        internal CmlLiteExistenceValidator(ImportMediator mediator)
        {
            this.mediator = mediator;
            this.mediator.RegisterCmlLiteExistenceValidator(this);
        }

        /// <summary>
        /// Check that the document contains at least some CMLLite
        /// </summary>
        /// <returns>true if no problems found, false otherwise</returns>
        public bool Process()
        {
            Reset();
            if (document == null)
            {
                throw new InvalidOperationException("document must be set before Process can be called");
            }
            bool found =
                (bool)
                document.XPathEvaluate("count(//*[local-name()='" + CmlMolecule.Tag + "' and namespace-uri()='" +
                                       CmlConstants.Cmlns + "']/*[local-name()='" + CmlAtomArray.Tag + "' and namespace-uri()='" +
                                       CmlConstants.Cmlns + "']/*[local-name()='" + CmlAtom.Tag + "' and namespace-uri()='" +
                                       CmlConstants.Cmlns + "']) > 0");
            if (!found)
            {
                found = (bool)
                document.XPathEvaluate("count(//*[local-name()='" + CmlMolecule.Tag + "' and namespace-uri()='" +
                                       CmlConstants.Cmlns + "']/*[local-name()='" + CmlName.Tag + "' and namespace-uri()='" +
                                       CmlConstants.Cmlns + "']) > 0");
                if (!found) {
                    found = (bool)
                    document.XPathEvaluate("count(//*[local-name()='" + CmlMolecule.Tag + "' and namespace-uri()='" +
                                           CmlConstants.Cmlns + "']/*[local-name()='" + CmlFormula.Tag + "' and namespace-uri()='" +
                                           CmlConstants.Cmlns + "']) > 0");
                    if (!found) {
                        briefMessage = "No displayable data found in this document";
                        longMessage = "To display a molecule in the Chemistry Add-in to Microsoft Word it must either have a 2D representation, a formula or a name.";
                    }
                }
            }
            return found;
        }
        
        public void Start()
        {
            mediator.CmlLiteExistenceValidate();
        }

        public string BriefMessage()
        {
            return briefMessage;
        }

        public string LongMessage()
        {
            return longMessage;
        }

        private void Reset()
        {
            briefMessage = string.Empty;
            longMessage = string.Empty;
        }

        public void SetDocument(XDocument document)
        {
            this.document = document;
        }

        public XDocument GetDocument()
        {
            return document;
        }
    }
}