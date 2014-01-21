// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using Chem4Word.UI.Properties;

namespace Chem4Word.UI.Import
{
    /// <summary>
    /// Performs CMLLite validation according to the rules specified in
    /// schematron - this is not chemical validation
    /// </summary>
    internal class SchematronValidator : IValidator
    {
        private const string SvrlNamespace = "http://purl.oclc.org/dsdl/svrl";

        private static readonly XPathExpression ChildSvrlText =
            XPathExpression.Compile("./*[local-name()='text' and namespace-uri()='" + SvrlNamespace + "']");

        private static readonly XPathExpression FailedAssertXPath =
            XPathExpression.Compile("//*[local-name()='failed-assert' and namespace-uri()='" + SvrlNamespace + "']");

        private static readonly XPathExpression WarningsXPath =
            XPathExpression.Compile("//*[local-name()='successful-report' and namespace-uri()='" + SvrlNamespace + "']");

        private readonly ImportMediator mediator;
        private readonly XslCompiledTransform xslt;

        private string briefMessage;

        private XDocument document;
        private StringBuilder longMessage;
        private ICollection<ImportWarning> warnings = new List<ImportWarning>(0);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">The mediator to control the data flow</param>
        internal SchematronValidator(ImportMediator mediator)
        {
            this.mediator = mediator;
            xslt = new XslCompiledTransform();
            xslt.Load(XmlReader.Create(new StringReader(Resources.cmllite)));
            this.mediator.RegisterSchematronValidator(this);
        }

        /// <summary>
        /// Find out if there are any warnings in the CMLLite.
        /// </summary>
        public bool WarningsFound { get; private set; }

        /// <summary>
        /// Perform the validation against the schematron
        /// </summary>
        /// <returns>true if no problems found, false otherwise</returns>
        public bool Process()
        {
            Reset();
            StringBuilder result = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(result))
            {
                xslt.Transform(document.CreateReader(), writer);
            }
            // now examine the transformed document to see it contains errors or warnings
            XPathDocument docNav = null;
            using (StringReader sr = new StringReader(result.ToString()))
            {
                docNav = new XPathDocument(sr);
            }
            XPathNavigator navigator = docNav.CreateNavigator();
            XPathNodeIterator failedAssertionNodes = navigator.Select(FailedAssertXPath);
            bool errors = failedAssertionNodes.Count > 0;
            if (errors)
            {
                briefMessage = "Molecular convention problem found. Can't import this file";
                ICollection<ImportWarning> failures = ConvertToImportWarnings(failedAssertionNodes);
                SetFailureMessages(failures);
            }
            XPathNodeIterator warningsNodes = navigator.Select(WarningsXPath);
            WarningsFound = warningsNodes.Count > 0;
            if (WarningsFound)
            {
                warnings = ConvertToImportWarnings(warningsNodes);
            }
            return !errors;
        }

        public void Start()
        {
            mediator.SchematronValidate();
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

        private void SetFailureMessages(IEnumerable<ImportWarning> failures)
        {
            foreach (ImportWarning failure in failures)
            {
                longMessage.Append("message: ");
                longMessage.AppendLine(failure.Message);
                longMessage.Append("location: ");
                longMessage.AppendLine(failure.Location);
                longMessage.AppendLine();
            }
        }

        public ICollection<ImportWarning> GetImportWarnings()
        {
            return warnings;
        }

        public void SetDocument(XDocument document)
        {
            this.document = document;
        }

        public XDocument GetDocument()
        {
            return document;
        }

        private static ICollection<ImportWarning> ConvertToImportWarnings(XPathNodeIterator iterator)
        {
            ICollection<ImportWarning> collection = new List<ImportWarning>(iterator.Count);
            while (iterator.MoveNext())
            {
                XPathNavigator navigator = iterator.Current;
                string location = navigator.GetAttribute("location", "");
                string message = navigator.SelectSingleNode(ChildSvrlText).Value;
                collection.Add(new ImportWarning(message, location));
            }
            return collection;
        }
    }
}