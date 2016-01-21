// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml.Linq;
using Chem4Word.Api;
using Chem4Word.Core.com.chemspider.www;
using Chem4Word.Core.UserSetting;
using Chem4Word.UI.Import;
using Chem4Word.UI.Properties;
using Chem4Word.UI.UIControls;
using Microsoft.Office.Interop.SmartTag;
using Microsoft.Office.Tools;
using Microsoft.Office.Tools.Word;
using Numbo.Coa;
using Action=Microsoft.Office.Tools.Word.Action;
using WordTools = Microsoft.Office.Tools.Word;

namespace Chem4Word.Core.SmartTag
{
    /// <summary>
    /// This class function as Chemistry Smart Tag. It recognize user input text into Word document, where user can convert those recognized text as Chemistry Zone.
    /// </summary>
    public class ChemistrySmartTag : Microsoft.Office.Tools.Word.SmartTag
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly CoreClass core;

        private readonly TermDictionaryManager termDictionary;

        /// <summary>
        /// Initializes a new instance of the ChemistrySmartTag class.
        /// </summary>
        /// <param name="chem4WordCore">Chem4Word core object. <see cref="CoreClass"/></param>
        public ChemistrySmartTag(CoreClass chem4WordCore)
            : base("Chem4WordSmartTag#tag", "Chem4Word SmartTag")
        {
            string module = "ChemistrySmartTag() Constructor";
            this.core = chem4WordCore;
            try
            {
                // Load Dictionary
                termDictionary = new TermDictionaryManager(core.assemblyDirectoryName + @"\SmartTag");
                termDictionary.LoadLocalDictionary(core.localAppDataFolder + @"\SmartTag");

                Action convertToChemistryZone = new Action("Convert To Chemistry Zone");
                Actions = new ActionBase[] {convertToChemistryZone};
                convertToChemistryZone.Click += ConvertToChemistryZoneClick;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE);
                core.WriteTelemetry(module, "Exception", ex.Message);
                new ErrorReport(core.GetTelemetry(), module, ex.Message).ShowDialog();
            }
        }

        internal bool IsTermExists(string value)
        {
            return termDictionary.IsTermExists(value.Trim());
        }

        internal void AddTerm(string value, string cml)
        {
            termDictionary.AddTerm(value, cml);
        }

        #region WordAPI Callbacks

        /// <summary>
        /// Event handler when user click on "Convert to Chemistry Zone" action button.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e"><see cref="Microsoft.Office.Tools.Word.ActionEventArgs"/></param>
        private void ConvertToChemistryZoneClick(object sender, ActionEventArgs e)
        {
            string module = "ChemistrySmartTag.ConvertToChemistryZoneClick()";
            try
            {
                core.WriteTelemetry(module, "Information", e.Range.Text);
                // Read CML from Properties bag
                ISmartTagProperties propertyBag = e.Properties;
                string id = propertyBag.get_Read("id");

                if (termDictionary.Contain(id))
                {
                    XDocument cml = XDocument.Parse(termDictionary.GetValue(id));
                    ImportMediator m = new ImportMediator();
                    switch (Setting.Import)
                    {
                        case ImportSetting.StrictFail:
                            m.ParseSeverity = ImportMediator.Severity.Strict;
                            break;
                        case ImportSetting.Prompt:
                            m.ParseSeverity = ImportMediator.Severity.Prompt;
                            break;
                        case ImportSetting.Auto:
                            m.ParseSeverity = ImportMediator.Severity.Auto;
                            break;
                    }
                    m.Start(cml);

                    if (m.Worked())
                    {
                        DepictionOption documentDepictionOption = null;
                        IEnumerable<DepictionOption> possibleDepictionOptions =
                            Depiction.PossibleDepictionOptions(m.GetContextObject());
                        foreach (DepictionOption option in possibleDepictionOptions)
                        {
                            if (
                                string.Compare(option.GetAsLatexFormattedString(), e.Range.Text,
                                               StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                documentDepictionOption = option;
                                break;
                            }
                        }
                        if (documentDepictionOption == null)
                        {
                            documentDepictionOption = CoreClass.GetPreferedDocumentDepiction(m.GetContextObject(),
                                                                                             Setting.
                                                                                                 DocumentPreferedDepiction);
                        }
                        var navigatorDepictionOption =
                            CoreClass.GetPreferedNavigatorDepiction(m.GetContextObject(),
                                                                    Setting.NavigatorPreferedDepiction);
                        var chemistryZoneProperties = new ChemistryZoneProperties(documentDepictionOption,
                                                                                  navigatorDepictionOption, true);
                        //core.AddNewContextObjectToDocument(e.Range, m.GetContextObject(), documentDepictionOption,
                        //                                   navigatorDepictionOption);
                        core.AddNewContextObjectToDocument(e.Range, m.GetContextObject(), chemistryZoneProperties);
                    }
                }
                else
                {
                    MessageBox.Show("Cannot find " + id + ".cml in application folder.",
                                    Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE);
                core.WriteTelemetry(module, "Exception", ex.Message);
                new ErrorReport(core.GetTelemetry(), module, ex.Message).ShowDialog();
            }
        }

        /// <summary>
        /// Overrided recognition function of Chem4Word SmartTag, since the default function split token by special character and space.
        /// Chemistry term could has special character and space in a single term.
        /// </summary>
        /// <param name="text">Text of the current paragraph.</param>
        /// <param name="site"><see cref="Microsoft.Office.Interop.SmartTag.ISmartTagRecognizerSite"/></param>
        /// <param name="tokenList"><see cref="Microsoft.Office.Interop.SmartTag.ISmartTagTokenList"/></param>
        protected override void Recognize(string text, ISmartTagRecognizerSite site, ISmartTagTokenList tokenList)
        {
            string module = "ChemistrySmartTag.Recognize()";
            try
            {
                core.WriteTelemetry(module, "Information", text);
                foreach (Term term in termDictionary)
                {
                    Regex r = new Regex("(?i)\\b" + Regex.Escape(term.Value) + "\\b");
                    Match m = r.Match(ParagraphText);
                    while (m.Success)
                    {
                        // Create a smart tag token and a property bag for the 
                        // recognized term.
                        ISmartTagProperties propertyBag = site.GetNewPropertyBag();

                        // Write a new property value.
                        propertyBag.Write("id", term.MoleculeId);

                        // Attach the smart tag to the term in the document
                        PersistTag(m.Index, m.Length, propertyBag);
                        m = m.NextMatch();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE);
                core.WriteTelemetry(module, "Exception", ex.Message);
                new ErrorReport(core.GetTelemetry(), module, ex.Message).ShowDialog();
            }
        }

        #endregion
    }
}