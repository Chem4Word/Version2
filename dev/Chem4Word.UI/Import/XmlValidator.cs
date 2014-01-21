// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Xml;
using System.Xml.Linq;

namespace Chem4Word.UI.Import
{
    /// <summary>
    /// Checks the well-formedness of the XML
    /// </summary>
    internal class XmlValidator : IValidator
    {
        private readonly ImportMediator mediator;

        private string briefMessage = string.Empty;

        private XDocument document;

        private string longMessage = string.Empty;

        private XmlReader xmlReader;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">The mediator to control the data flow</param>
        internal XmlValidator(ImportMediator mediator)
        {
            this.mediator = mediator;
            this.mediator.RegisterXmlValidator(this);
        }

        /// <summary>
        /// Checks that the XML in the document is well formed
        /// </summary>
        /// <returns>true if no problems found, false otherwise</returns>
        public bool Process()
        {
            Reset();
            bool ok = false;
            try
            {
                document = XDocument.Load(xmlReader);
                ok = true;
            }
            catch (XmlException e)
            {
                briefMessage = "The xml is not well formed";
                longMessage = e.ToString();
            }
            catch (Exception e)
            {
                briefMessage = "Unexpected exception:\n\t" + e.Message;
                longMessage = e.ToString();
            }
            finally
            {
                xmlReader.Close();
            }
            return ok;
        }

        public void Start()
        {
            mediator.XmlValidate();
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

        public void SetXmlReader(XmlReader reader)
        {
            xmlReader = reader;
        }

        public XDocument GetXDocument()
        {
            return document;
        }
    }
}