// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Globalization;
using System.Xml.Linq;
using Numbo.Coa;

namespace Chem4Word.UI.Import
{
    /// <summary>
    /// Performs CMLLite normalisation
    /// </summary>
    internal class CmlLiteNormaliser : IValidator
    {
        private readonly ImportMediator mediator;

        private string briefMessage = string.Empty;

        private XDocument document;
        private string longMessage = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">The mediator to control the data flow</param>
        internal CmlLiteNormaliser(ImportMediator mediator)
        {
            this.mediator = mediator;
            this.mediator.RegisterCmlLiteNormaliser(this);
        }

        /// <summary>
        /// Find out if there are any warnings in the CMLLite.
        /// </summary>
        public bool WarningsFound { get; private set; }

        /// <summary>
        /// Perform the normalisation
        /// </summary>
        /// <returns>true if no problems found, false otherwise</returns>
        public bool Process()
        {
            Reset();
            try
            {
                var contextObject = new ContextObject(document);
                contextObject = Cid.NormalizeOnImport(contextObject, document.Root);
                document = contextObject.Cml;
            }
            catch (Exception e)
            {
                WarningsFound = true;
                briefMessage = "Problems were encountered whilst trying to normalise the CMLLite";
                longMessage = string.Format(CultureInfo.InvariantCulture, "{0}\n\n{1}", e.Message, e.StackTrace);
            }
            return !WarningsFound;
        }

        public void Start()
        {
            mediator.CmlLiteNormalise();
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
            WarningsFound = false;
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