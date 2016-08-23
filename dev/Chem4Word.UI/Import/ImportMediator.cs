// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml.XPath;
using log4net;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.UI.Import
{
    /// <summary>
    /// Controls the flow of data for the import of a file containing CMLLite  
    /// </summary>
    public class ImportMediator
    {
        #region Severity enum

        /// <summary>
        /// Sets the severity of the import parsing process
        /// </summary>
        public enum Severity
        {
            ///<summary>
            /// Will not make *any* changes to the incoming file
            /// and any problems will result in a failure to import
            /// (no pop up windows will appear)
            ///</summary>
            Strict,
            ///<summary>
            /// Will not make *any* changes to the incoming file without getting
            /// the users permission first - requires pop up windows
            ///</summary>
            Prompt,
            ///<summary>
            /// Will automatically try to fix any problems encountered 
            /// by changing the CML. Pop up windows will only appear if 
            /// a problem cannot be automatically fixed 
            ///</summary>
            Auto
        }

        #endregion

        private static readonly ILog Log = LogManager.GetLogger(typeof (ImportMediator));

        private CmlLiteExistenceValidator cmlliteExistenceValidator;
        private CmlLiteNormaliser cmlliteNormaliser;
        private SchematronValidator schematronValidator;
        private SchemaValidator schemaValidator;
        private UriReaderValidator uriReaderValidator;
        private bool worked;
        private XmlValidator xmlValidator;

        /// <summary>
        /// Creates a new import mediator and populates it with relevant the validators / normalisers. 
        /// </summary>
        public ImportMediator()
        {
            new UriReaderValidator(this);
            new XmlValidator(this);
            new SchemaValidator(this);
            new SchematronValidator(this);
            new CmlLiteNormaliser(this);
            new CmlLiteExistenceValidator(this);
            ParseSeverity = Severity.Auto;
            Headless = false;
        }

        ///<summary>
        /// Set the severity of the parsing for this import 
        ///</summary>
        public Severity ParseSeverity { get; set; }

        /// <summary>
        /// If headless then no popup windows will appear to inform the user
        /// about why a failure has occured 
        /// </summary>
        public bool Headless { get; set; }

        public string LongMessage { get; set; }
        public string BriefMessage { get; set; }

        /// <summary>
        /// Begin the import process from a URL
        /// </summary>
        /// <param name="inputUri">The URI of the CML to import</param>
        public void Start(string inputUri)
        {
            uriReaderValidator.SetInputUri(inputUri);
            uriReaderValidator.Start();
        }

        /// <summary>
        /// Begin the import process from a pre-existing XDocument
        /// </summary>
        /// <param name="xDocument">The XDocument containing the CML to import</param>
        public void Start(XDocument xDocument) {
            if (xDocument != null)
            {
                schemaValidator.SetInputXDocument(xDocument);
                schemaValidator.Start();
            }
            else
            {
                BriefMessage = "The document to import is null";
                LongMessage =
                    "The document to import is null\nNo further details are available at this time.";
                if (!Headless)
                {
                    var ifc = new ImportFailedControl
                                                  {
                                                      BriefDescription = BriefMessage,
                                                      LongDescription = LongMessage
                                                  };
                    ifc.ShowDialog();
                }
                else
                {
                    Log.Error(string.Format(CultureInfo.InvariantCulture, "Import Error Brief Description: {0}", BriefMessage));
                    Log.Error(string.Format(CultureInfo.InvariantCulture, "Import Error Long Description: {0}", LongMessage));
                }
            }
        }

        /// <summary>
        /// Find out if the import/validation processs has completed successfully
        /// </summary>
        /// <returns>true if the import process completed successfully false otherwise</returns>
        public bool Worked()
        {
            return worked;
        }

        /// <summary>
        /// Get the ContextObject created as a result of importing the CML
        /// </summary>
        /// <returns>The ContextObject created from importing the CML</returns>
        public ContextObject GetContextObject()
        {
            return new ContextObject(cmlliteExistenceValidator.GetDocument());
        }

        /// <summary>
        /// Register the URI Reader validator with this import mediator
        /// </summary>
        /// <param name="v">the URI reader validator to register</param>
        internal void RegisterUriReaderValidator(UriReaderValidator v)
        {
            uriReaderValidator = v;
        }

        /// <summary>
        /// Register the XmlValidator with this import mediator
        /// </summary>
        /// <param name="v">the XmlValidator to register</param>
        internal void RegisterXmlValidator(XmlValidator v)
        {
            xmlValidator = v;
        }

        /// <summary>
        /// Register the SchemaValidatorwith this import mediator
        /// </summary>
        /// <param name="v">the SchemaValidator to register</param>
        internal void RegisterSchemaValidator(SchemaValidator v)
        {
            schemaValidator = v;
        }

        /// <summary>
        /// Register the SchematronValidator with this import mediator
        /// </summary>
        /// <param name="v">the SchematronValidator to register</param>
        internal void RegisterSchematronValidator(SchematronValidator v)
        {
            schematronValidator = v;
        }

        /// <summary>
        /// Register the CMLLiteNormaliser with this import mediator
        /// </summary>
        /// <param name="v">the CMLLiteNormaliser to register</param>
        internal void RegisterCmlLiteNormaliser(CmlLiteNormaliser v)
        {
            cmlliteNormaliser = v;
        }

        /// <summary>
        /// Register the CmlLiteExistenceValidator with this import mediator
        /// </summary>
        /// <param name="v">the CmlLiteExistenceValidator to register</param>
        internal void RegisterCmlLiteExistenceValidator(CmlLiteExistenceValidator v)
        {
            cmlliteExistenceValidator = v;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void UriReaderValidate()
        {
            if (uriReaderValidator.Process())
            {
                xmlValidator.SetXmlReader(uriReaderValidator.GetXmlReader());
                xmlValidator.Start();
            }
            else
            {
                DisplayError(uriReaderValidator);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void XmlValidate()
        {
            if (xmlValidator.Process())
            {
                schemaValidator.SetInputXDocument(xmlValidator.GetXDocument());
                schemaValidator.Start();
            }
            else
            {
                DisplayError(xmlValidator);
            }
        }

        /// <summary>
        /// Validate again the CML Schema
        /// </summary>
        internal void SchemaValidate()
        {
            if (schemaValidator.Process())
            {
                schematronValidator.SetDocument(schemaValidator.GetDocument());
                schematronValidator.Start();
            }
            else
            {
                DisplayError(schemaValidator);
            }
        }

        /// <summary>
        /// Validate against the schematron and then allow user to 
        /// select warnings to fix/ignore
        /// </summary>
        internal void SchematronValidate()
        {
            if (schematronValidator.Process())
            {
                // now check if there are any warnings
                if (schematronValidator.WarningsFound)
                {
                    ImportWarningControl iwc = new ImportWarningControl(schematronValidator.GetImportWarnings());
                    if (iwc.ShowDialog().Value)
                    {
                        ICollection<ImportWarning> importWarnings =
                            iwc.SelectedImportWarnings;

                        XDocument doc = schematronValidator.GetDocument();
                        ContextObject co = new ContextObject(doc);
                        foreach (ImportWarning warning in importWarnings)
                        {
                            XElement xElement = co.Cml.XPathSelectElement(warning.Location);
                            if (xElement.Name.LocalName.Equals(CmlFormula.Tag))
                            {
                                co = Cid.FixMissingConciseFormula(co, xElement);
                            }
                            //else 
                            //{
                            //    /// JAT TODO  see if there are other elements we can mend
                            //    ;
                            //}
                        }
                    }
                }
                cmlliteNormaliser.SetDocument(schematronValidator.GetDocument());
                cmlliteNormaliser.Start();
            }
            else
            {
                DisplayError(schematronValidator);
            }
        }

        /// <summary>
        /// Normalise the CMLLite present in the document
        /// </summary>
        internal void CmlLiteNormalise()
        {
            if (cmlliteNormaliser.Process())
            {
                worked = true;
                cmlliteExistenceValidator.SetDocument(cmlliteNormaliser.GetDocument());
                cmlliteExistenceValidator.Start();
            }
            else
            {
                DisplayError(cmlliteNormaliser);
            }
        }

        /// <summary>
        /// Check that there is actually some CMLLite in the document
        /// </summary>
        internal void CmlLiteExistenceValidate()
        {
            if (cmlliteExistenceValidator.Process())
            {
                worked = true;
            }
            else
            {
                DisplayError(cmlliteExistenceValidator);
                worked = false;
            }
        }

        /// <summary>
        /// Display the error from the validator - if Headless then nothing will be shown but it will be logged
        /// </summary>
        /// <param name="validator">the validator containing the error</param>
        private void DisplayError(IValidator validator)
        {
            if (!Headless)
            {
                var ifc = new ImportFailedControl
                                              {
                                                  BriefDescription = validator.BriefMessage(),
                                                  LongDescription = validator.LongMessage()
                                              };
                ifc.ShowDialog();
            }
            else
            {
                Log.Error(string.Format(CultureInfo.InvariantCulture, "Import Error Brief Description: {0}", validator.BriefMessage()));
                Log.Error(string.Format(CultureInfo.InvariantCulture, "Import Error Long Description: {0}", validator.LongMessage()));
            }
        }
    }
}