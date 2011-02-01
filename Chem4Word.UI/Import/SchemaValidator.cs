// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Chem4Word.UI.Properties;

namespace Chem4Word.UI.Import
{
    /// <summary>
    /// Checks the vocabulary of the CML in the document - mininal content model
    /// checking is performed (i.e. only those elements which may only have string content) 
    /// </summary>
    public class SchemaValidator : IValidator
    {
        private readonly ImportMediator mediator;

        private readonly XmlSchemaSet schemas;

        private string briefMessage = string.Empty;
        private XDocument document;

        private StringBuilder longMessage = new StringBuilder(0);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">The mediator to control the data flow</param>
        internal SchemaValidator(ImportMediator mediator)
        {
            this.mediator = mediator;
            schemas = new XmlSchemaSet();
            schemas.Add(null, XmlReader.Create(new StringReader(Resources.CMLSchema)));
            this.mediator.RegisterSchemaValidator(this);
        }

        /// <summary>
        /// Perform the validation against the schema (checks the vocabulary)
        /// </summary>
        /// <returns>true if no problems found, false otherwise</returns>
        public bool Process()
        {
            Reset();
            bool errors = false;
            document.Validate(schemas, (o, e) =>
                                       {
                                           longMessage.Append(e.Message + "\n");
                                           longMessage.Append(e.Exception.ToString() + "\n");
                                           errors = true;
                                       });
            if (errors)
            {
                briefMessage = "The file contains invalid CML";
            }
            return !errors;
        }

        public void Start()
        {
            mediator.SchemaValidate();
        }

        public string BriefMessage()
        {
            return briefMessage;
        }

        public string LongMessage()
        {
            return longMessage.ToString();
        }

        private void Reset()
        {
            briefMessage = string.Empty;
            longMessage = new StringBuilder();
        }

        public void SetInputXDocument(XDocument document)
        {
            this.document = document;
        }

        public XDocument GetDocument()
        {
            return document;
        }
    }
}