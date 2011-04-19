// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;
using Chem4Word.AddIn.Gallery;
using Chem4Word.AddIn.Navigator;
using Chem4Word.Api;
using Chem4Word.Api.Events;
using Chem4Word.Core.Events;
using Chem4Word.UI.About;
using Chem4Word.UI.Navigator;
using Chem4Word.UI.Properties;
using log4net;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools;
using Numbo.Coa;
using stdole;
using Application = Microsoft.Office.Interop.Word.Application;
using Clipboard = System.Windows.Clipboard;
using CustomTaskPane = Microsoft.Office.Tools.CustomTaskPane;
using IDataObject = System.Windows.IDataObject;
using MessageBox = System.Windows.MessageBox;
using Version = System.Version;

[assembly: CLSCompliant(true)]

namespace Chem4Word.AddIn {
    /// <summary>
    ///   Derives ChemistryRibon class from Office.IRibbonExtensibility
    /// </summary>
    [ComVisible(true)]
    public class ChemistryRibbon : IRibbonExtensibility, IDisposable {
        private static readonly string defaultSearchBoxText = "Search term";
        private static readonly string searchAreaDocument = "Document";
        private static readonly string searchAreaOpsin = "OPSIN";
        private static readonly string searchAreaPubChem = "PubChem";

        private static readonly ILog Log = LogManager.GetLogger(typeof (ChemistryRibbon));

        private readonly Core.CoreClass core;
        private readonly CustomTaskPaneCollection customTaskPanes;
        private readonly Application wordApp;
        private AboutBox aboutBox;

        private string curDir = "";
        private IEnumerable<DepictionOption> depictionOptions;
        private bool editEnable;
        private bool importCmlEnable;
        private bool inlineChemEnable;
        private ChemistryNavigatorHostControl navCustControl;

        private bool navVisible;
        private bool opsinLookUpServerAvailable = true;
        private bool pubChemSearchAvailable = true;
        private IRibbonUI ribbon;
        private bool saveSelEnable;
        private bool searchBoxEnable;
        private string searchBoxText = defaultSearchBoxText;
        private string searchLocation = "Document";
        private bool tweak2DEnable;
        private bool viewOptionEnable;

        /// <summary>
        ///   Initializes a new instance of the ChemistryRibbon class.Default Constructor
        /// </summary>
        public ChemistryRibbon() {}

        /// <summary>
        ///   Initializes a new instance of the ChemistryRibbon class.
        /// </summary>
        /// <param name = "chemistryCore">controller between Chem4Word UI and Word API</param>
        /// <param name = "customTaskPanes">Collection of custom task panes in application</param>
        /// <param name = "wordApp">Word application</param>
        public ChemistryRibbon(Core.CoreClass chemistryCore, CustomTaskPaneCollection customTaskPanes,
                               Application wordApp) {
            core = chemistryCore;
            this.customTaskPanes = customTaskPanes;
            this.wordApp = wordApp;

            pubChemSearchAvailable = true;
            // Register Events
            core.ContentControlSelectionChanged += ActiveDocumentContentControlSelectionChanged;
            core.WindowActivate += CoreWindowActivate;
            core.DocumentBeforeClose += CoreDocumentBeforeClose;
        }

        private bool ViewOptionEnable {
            get { return viewOptionEnable; }
            set {
                if (value) {
                    ActivateChemsitryTab();
                }
                viewOptionEnable = value;
                //ribbon.InvalidateControl("viewOption");
                //ribbon.InvalidateControl("dynamicViewMenu");
            }
        }

        private bool SearchBoxEnable {
            get { return searchBoxEnable; }
            set {
                searchBoxEnable = value;
                //ribbon.Invalidate();
                ribbon.InvalidateControl("searchBox");
                //ribbon.InvalidateControl("searchGroup");
            }
        }

        private bool ImportCmlFileEnable {
            get { return importCmlEnable; }
            set {
                importCmlEnable = value;
                ribbon.InvalidateControl("importDropDown");
            }
        }

        private bool InlineChemTextEnable {
            get { return inlineChemEnable; }
            set {
                inlineChemEnable = value;
                //ribbon.InvalidateControl("inlineText");
                ribbon.Invalidate();
            }
        }

        private bool OPSINLookUpServerAvailable {
            get { return opsinLookUpServerAvailable; }
        }

        private bool PubChemSearchAvailable {
            get { return pubChemSearchAvailable; }
            set {
                pubChemSearchAvailable = value;
                ribbon.InvalidateControl("importDropDown");
            }
        }

        private bool SaveSelectionButtonEnable {
            get { return saveSelEnable; }
            set {
                saveSelEnable = value;
                ImportCmlFileEnable = !value;
                ribbon.InvalidateControl("saveSelection");
                ribbon.InvalidateControl("editLabels");
                ribbon.InvalidateControl("viewCMLButton");
                ribbon.InvalidateControl("dynamicViewMenu");
                ribbon.InvalidateControl("importDropDown");
            }
        }

        private bool EditEnable {
            get { return editEnable; }
            set {
                editEnable = value;
                ribbon.InvalidateControl("splitButton");
            }
        }

        private bool Tweak2DButtonEnable {
            get { return tweak2DEnable; }
            set {
                tweak2DEnable = value;
                ribbon.InvalidateControl("tweak2D");
            }
        }

        private bool NavigatorButtonVisible {
            get { return navVisible; }
            set {
                navVisible = value;
                ribbon.InvalidateControl("navigator");
            }
        }

        public string SearchLocation {
            get { return searchLocation; }
            private set {
                searchLocation = value;
                ribbon.InvalidateControl("searchLocationMenu");
                PerformSearch();
            }
        }

        #region IDisposable Members

        /// <summary>
        ///   Default dispose method
        /// </summary>
        public void Dispose() {
            Dispose(true);
        }

        #endregion

        #region IRibbonExtensibility Members

        /// <summary>
        ///   Get the XML that describes the format of the Chemistry Ribbon
        /// </summary>
        /// <param name = "ribbonID">ID of the ribbon</param>
        /// <returns>Chemistry Ribbon XML</returns>
        public string GetCustomUI(string ribbonID) {
            return GetResourceText("Chem4Word.AddIn.ChemistryRibbon.xml");
        }

        #endregion

        public string GetContent(IRibbonControl control) {
            var myStringBuilder =
                new StringBuilder(@"<menu xmlns=""http://schemas.microsoft.com/office/2006/01/customui"" >");
            try {
//                XDocument cml = core.ActiveChemistryDocument.SelectedChemistryZone.Cml;
                var chemistryZone = core.ActiveChemistryDocument.SelectedChemistryZone;
                var currentDepictionOption =
                    DepictionOption.CreateDepictionOption(chemistryZone.Cml,
                                                          chemistryZone.Properties.
                                                              DocumentDepictionOptionXPath);
                var contextObject = chemistryZone.AsContextObject();
                depictionOptions = Depiction.PossibleDepictionOptions(contextObject, contextObject.Cml.Root);
                for (int i = 0; i < depictionOptions.Count(); i++) {
                    DepictionOption depictionOption = depictionOptions.ElementAt(i);
                    string label = Depiction.Is2D(depictionOption) ? "2D" : depictionOption.GetAsLatexFormattedString();
                    string type = depictionOption.HumanUnderstandableOption;
                    myStringBuilder.Append(
                        string.Format(CultureInfo.InvariantCulture,
                                      "<button id=\"button{0}\" tag=\"{0}\" label=\"{1}\" onAction=\"OnAction\" screentip=\"change to this representation\" supertip=\"{2}\" ",
                                      i, label, type));
                    if (currentDepictionOption == depictionOption) {
                        myStringBuilder.Append(string.Format(CultureInfo.InvariantCulture,
                                                             "imageMso=\"MenuAcceptInvitation\""));
                    }
                    myStringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "/>"));

                    if (string.Equals("2D", label, StringComparison.OrdinalIgnoreCase)) {
                        myStringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "<menuSeparator />"));
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE,
                                MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            myStringBuilder.Append(
                string.Format(CultureInfo.InvariantCulture,
                              "<button id=\"addNewDepictionOptionButton\" tag=\"addNewDepictionOption\" label=\"Other ...\" onAction=\"AddNewDepictionOption\"/>"));
            myStringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "</menu>"));
            return myStringBuilder.ToString();
        }

        public void AddNewDepictionOption(IRibbonControl control) {
            DepictionOption newDepictionOption = core.AddNewLabel();
            if (newDepictionOption != null) {
                core.ChangeExistingDocumentDepiction(core.ActiveChemistryDocument.SelectedChemistryZone,
                                                     newDepictionOption, false);
            }
            ribbon.InvalidateControl("dynamicViewMenu");
        }

        public void OnAction(IRibbonControl control) {
            try {
                int position;
                if (int.TryParse(control.Tag, out position)) {
                    var newDepictionOption = depictionOptions.ElementAt(position);
                    core.ChangeExistingDocumentDepiction(core.ActiveChemistryDocument.SelectedChemistryZone,
                                                         newDepictionOption, true);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE,
                                MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void ActivateChemsitryTab() {
            ribbon.ActivateTab("chemistryTab");
            ribbon.InvalidateControl("chemistryTab");
        }

        private void SetTweak2DButton(IChemistryZone chemistryZone) {
            if (chemistryZone == null) {
                Tweak2DButtonEnable = false;
            } else {
                DepictionOption depictionOption = DepictionOption.CreateDepictionOption(chemistryZone.Cml,
                                                                                        chemistryZone.Properties.
                                                                                            DocumentDepictionOptionXPath);

                Tweak2DButtonEnable = depictionOption.HasAssociatedTwoD();
            }
        }

        /// <summary>
        ///   Check the type of chemistry zone whether it is in 2D mode or text mode.
        ///   Open the Editors respectively depending on the type of the Chemistr Zone.
        /// </summary>
        /// <param name = "sender"></param>
        public void EditClick(IRibbonControl sender) {
            try {
                var chemistryZone = core.ActiveChemistryDocument.SelectedChemistryZone;
                core.LoadAppropriateEditor(chemistryZone);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE,
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void HelpClick(IRibbonControl sender) {
            var helpFileName = Path.Combine(core.GetAssemblyDirectory, "Chemistry_Add-in_for_Word_User_Guide.docx");
            if (File.Exists(helpFileName)) {
                Process.Start(helpFileName);
            } else {
                MessageBox.Show(Properties.Resources.FILE_NOT_FOUND_MESSAGE, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE,
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CoreWindowActivate(object sender, ChemistryDocumentEventArgs e) {
            wordApp.ActiveDocument.ContentControlBeforeDelete += ActiveDocumentContentControlBeforeDelete;
            // Refresh Navigator visible state
            navVisible = false;
            foreach (CustomTaskPane custTaskPane in customTaskPanes) {
                try {
                    if (custTaskPane.Window == e.CurrentWindow) {
                        navVisible = custTaskPane.Visible;
                    }
                } catch (ArgumentNullException nullException) {
                    // When use create a new ChemistryDocument (Ctrl + N) and close the window,
                    // this senario cause error when we trying to access taskPane.Window.
                    // So that we need to prevent this Error by ignore the exception.
                    nullException.HelpLink = "No Help File";
                }
            }
            //ribbon.InvalidateControl("navigator");
            PubChemSearchAvailable = true;
            // Refresh Save Selection button visible state
            IChemistryZone chemistryZone = core.ActiveChemistryDocument.SelectedChemistryZone;
            SetTweak2DButton(chemistryZone);
            if (core.ActiveChemistryDocument.SelectedChemistryZone != null) {
                SaveSelectionButtonEnable = true;
                SearchBoxEnable = false;
                Tweak2DButtonEnable =
                    WdContentControlType.wdContentControlPicture.Equals(
                        core.ActiveChemistryDocument.SelectedChemistryZone.ContentControl.Type);
                InlineChemTextEnable = false;
                ViewOptionEnable = true;
                EditEnable = true;
            } else {
                SearchBoxEnable = true;
                SaveSelectionButtonEnable = false;
                Tweak2DButtonEnable = false;
                ViewOptionEnable = false;
                EditEnable = false;
            }
            if (searchAreaDocument.Equals(searchLocation)) {
                SearchBoxEnable = true;
            }

            ribbon.InvalidateControl("chemistryTab");
        }

        private void ActiveDocumentContentControlBeforeDelete(ContentControl oldContentControl, bool inUndoRedo) {
            EditEnable = false;
            ViewOptionEnable = false;
        }

        //Create callback methods here. For more information about adding callback methods, select the Ribbon XML item in Solution Explorer and then press F1

        /// <summary>
        ///   Save the ribbon interface pointer locally
        /// </summary>
        /// <param name = "ribbonUI">pointer to ribbon interface</param>
        public void RibbonLoad(IRibbonUI ribbonUI) {
            ribbon = ribbonUI;
        }

        /// <summary>
        ///   Respond to Choose Depiction Option pick on ribbon
        /// </summary>
        /// <param name = "sender">ID of ribbon control picked</param>
        public void ViewOptionClick(IRibbonControl sender) {
            try {
                core.SwitchViewOptions();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE,
                                MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        /// <summary>
        ///   Get whether the Depiction Option button in the Ribbon is enabled or not
        /// </summary>
        /// <param name = "control">ID of ribbon control</param>
        /// <returns>true if enabled, false otherwise</returns>
        public bool GetDepictionOptionEnable(IRibbonControl control) {
            return ViewOptionEnable;
            ribbon.InvalidateControl(control.Id);
        }

        public bool GetSearchBoxEnabled(IRibbonControl control) {
            return SearchBoxEnable;
        }

        public bool GetImportCmlFileEnable(IRibbonControl control) {
            return ImportCmlFileEnable;
        }

        public void SearchBoxChange(IRibbonControl control, string text) {
            if (text.Trim().Length > 0) {
                searchBoxText = text;
                PerformSearch();
            }
        }

        public string SearchBoxText(IRibbonControl control) {
            return (searchBoxText.Trim().Length > 0) ? searchBoxText : defaultSearchBoxText;
        }

        private void PerformSearch() {
            if ((!defaultSearchBoxText.Equals(searchBoxText)) && searchBoxText.Trim().Length > 0) {
                try {
                    if (searchAreaOpsin.Equals(searchLocation)) {
                        core.OpsinLookUpSearch(searchBoxText.Trim());
                    } else if (searchAreaPubChem.Equals(searchLocation)) {
                        core.PubChemLookUpSearch(searchBoxText.Trim());
                    } else {
                        core.DocumentSearch(searchBoxText.Trim());
                    }
                } catch (Exception ex) {
                    Log.Error(ex);
                    MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                                    MessageBoxImage.Stop);
                } finally {
                    searchBoxText = (searchBoxText.Trim().Length > 0) ? searchBoxText : defaultSearchBoxText;
                    ribbon.InvalidateControl("searchBox");
                }
            }
        }

        public string SearchLocationMenuLabel(IRibbonControl control) {
            return SearchLocation;
        }

        public void SearchDocumentButton(IRibbonControl control) {
            SearchLocation = searchAreaDocument;
        }

        public void SearchPubChemButton(IRibbonControl control) {
            SearchLocation = searchAreaPubChem;
        }

        public void SearchOpsinButton(IRibbonControl control) {
            SearchLocation = searchAreaOpsin;
        }

        ///<summary>
        ///  Respond to click of Load from... OPSIN ribbon button
        ///</summary>
        ///<param name = "sender">ID of ribbon control</param>
        public void OpsinLookUpClick(IRibbonControl sender) {
            try {
                core.OpsinLookUpClick();
            } catch (Exception ex) {
                Log.Error(ex);
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                                MessageBoxImage.Stop);
            }
        }

        ///<summary>
        ///  Respond to click of Load from... PubChem ribbon button
        ///</summary>
        ///<param name = "sender">ID of ribbon control</param>
        public void PubChemLookUpClick(IRibbonControl sender) {
            try {
                core.PubChemLookUpClick();
            } catch (Exception ex) {
                Log.Error(ex);
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                                MessageBoxImage.Stop);
            }
        }

        /// <summary>
        ///   Respond to pick of Import CML button on ribbon
        /// </summary>
        /// <param name = "sender">ID of ribbon control</param>
        public void ImportCmlFileClick(IRibbonControl sender) {
            try {
                OpenFileDialog dialog = new OpenFileDialog
                                            {
                                                InitialDirectory =
                                                    string.Empty.Equals(curDir)
                                                        ? Environment.GetFolderPath(
                                                            Environment.SpecialFolder.MyDocuments)
                                                        : curDir,
                                                Filter = Properties.Resources.OpenFileDialogFileTypeFilter,
                                                FilterIndex = 1,
                                                RestoreDirectory = false
                                            };

                if (dialog.ShowDialog() == DialogResult.OK) {
                    string fileName = dialog.SafeFileName;
                    curDir = dialog.FileName.Replace(fileName, "");
                    core.ImportCmlFile(dialog.FileName);
                    Log.Error("file imported: " + dialog.FileName);
                }
            } catch (Exception ex) {
                Log.Error(ex);
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                                MessageBoxImage.Stop);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name = "sender"></param>
        public void InlineControlClick(IRibbonControl sender) {
            try {
                OpenFileDialog dialog = new OpenFileDialog
                                            {
                                                InitialDirectory =
                                                    curDir == string.Empty
                                                        ? Environment.GetFolderPath(
                                                            Environment.SpecialFolder.MyDocuments)
                                                        : curDir,
                                                Filter = Properties.Resources.OpenFileDialogFileTypeFilter,
                                                FilterIndex = 1,
                                                RestoreDirectory = false
                                            };

                if (dialog.ShowDialog() == DialogResult.OK) {
                    string fileName = dialog.SafeFileName;
                    curDir = dialog.FileName.Replace(fileName, "");
                    core.CreateInlineControl(dialog.FileName);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                                MessageBoxImage.Stop);
            }
        }

        /// <summary>
        ///   Respond to pick of Save Selection in Gallery button on ribbon
        /// </summary>
        /// <param name = "sender">ID of ribbon control</param>
        public void SaveSelectionClick(IRibbonControl sender) {
            try {
                var input = new InputString {ShowInTaskbar = false, TopLevel = true};
                if (input.ShowDialog() == DialogResult.OK) {
                    var galleryName = input.GalleryName.Text.Trim();
                    if (galleryName != string.Empty) {
                        core.SaveSelectionIntoGallery(galleryName);
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                                MessageBoxImage.Stop);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name = "control"></param>
        /// <returns></returns>
        public bool GetInlineChemTextEnable(IRibbonControl control) {
            return InlineChemTextEnable;
        }

        /// <summary>
        ///   Get whether the Save Selection button in the Ribbon is enabled or not
        /// </summary>
        /// <param name = "control">ID of ribbon control</param>
        /// <returns>true if enabled, false otherwise</returns>
        public bool GetSaveSelectionEnable(IRibbonControl control) {
            return SaveSelectionButtonEnable;
        }

        public bool OpsinLookUpEnable(IRibbonControl control) {
            return OPSINLookUpServerAvailable;
        }

        private bool PubChemSearchEnable(IRibbonControl control) {
            return PubChemSearchAvailable;
        }

        private void ActiveDocumentContentControlSelectionChanged(object sender, ContentControlEventArgs e) {
            PubChemSearchAvailable = true;
            if (e.Action) {
                IChemistryZone chemistryZone = core.ActiveChemistryDocument.SelectedChemistryZone;
                SetTweak2DButton(chemistryZone);
                if (chemistryZone != null)
                {
                    if (core.FetchItem == null) {
                        IDataObject iObject = Clipboard.GetDataObject();
                        if (iObject.GetDataPresent("Object Descriptor")) {
                            core.FetchItem = iObject.GetData("Object Descriptor");
                            MemoryStream ms = (MemoryStream) core.FetchItem;
                            if (ms.CanRead) {
                                core.GetCml = core.ActiveChemistryDocument.SelectedChemistryZone.Cml;
                            }
                        }
                    }
                    core.SetCml = chemistryZone.Cml;

                    SaveSelectionButtonEnable = true;
                    SearchBoxEnable = false;
                    InlineChemTextEnable = false;
                    ViewOptionEnable = true;
                    EditEnable = true;
                    if (
                        WdContentControlType.wdContentControlPicture.Equals(
                            core.ActiveChemistryDocument.SelectedChemistryZone.ContentControl.Type)) {
                        Tweak2DButtonEnable = true;
                    }
                }
            } else {
                SaveSelectionButtonEnable = false;
                SearchBoxEnable = true;
                ViewOptionEnable = false;
                Tweak2DButtonEnable = false;
                EditEnable = false;
                if (wordApp.Selection.ContentControls.Count == 0 &&
                    wordApp.Selection.Range.Start != wordApp.Selection.Range.End) {
                    InlineChemTextEnable = true;
                } else {
                    InlineChemTextEnable = false;
                }
            }
            if (searchAreaDocument.Equals(searchLocation)) {
                SearchBoxEnable = true;
            }
            ribbon.InvalidateControl("searchBox");
        }

        /// <summary>
        ///   Respond to pick of Tweak 2D Chemistry Zone button on ribbon
        /// </summary>
        /// <param name = "sender">ID of ribbon control</param>
        public void Tweak2DClick(IRibbonControl sender) {
            try {
                core.Tweak2D(core.ActiveChemistryDocument.SelectedChemistryZone);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                                MessageBoxImage.Stop);
            }
        }

        public bool GetEditEnable(IRibbonControl control) {
            return EditEnable;
        }

        public void OptionClick(IRibbonControl sender) {
            try {
                core.InvokeUserSetting();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                                MessageBoxImage.Stop);
            }
        }

        public void CheckForUpdateClick(IRibbonControl sender) {
            try {
                Process.Start(Properties.Resources.UPDATE_SITE_ADDRESS);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                                MessageBoxImage.Stop);
            }
        }

        /// <summary>
        ///   Get whether the Tweak 2-D Zone button in the Ribbon is enabled or not
        /// </summary>
        /// <param name = "control">ID of ribbon control</param>
        /// <returns>true if enabled, false otherwise</returns>
        public bool GetTweak2DEnable(IRibbonControl control) {
            return Tweak2DButtonEnable;
        }

        public void AboutClick(IRibbonControl sender) {
            aboutBox = new AboutBox();
            Version procuctVersion = Assembly.GetExecutingAssembly().GetName().Version;
            aboutBox.GetVersion = procuctVersion.ToString();
            object[] attributes =
                Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
            aboutBox.Getcopyright =
                (((AssemblyCopyrightAttribute) attributes[0]).Copyright.Split(new[] {'©'}, StringSplitOptions.None))[1];
            aboutBox.Show();
        }

        /// <summary>
        ///   Respond to user attempted to edit depiction options
        /// </summary>
        /// <param name = "sender"></param>
        public void EditLabelsClick(IRibbonControl sender) {
            try {
                core.EditLabels(core.ActiveChemistryDocument.SelectedChemistryZone);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                                MessageBoxImage.Stop);
            }
        }

        /// <summary>
        ///   Respond to user attempted to view CML
        /// </summary>
        /// <param name = "sender"></param>
        public void ViewCmlClick(IRibbonControl sender) {
            try {
                core.ViewCml(core.ActiveChemistryDocument.SelectedChemistryZone);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                                MessageBoxImage.Stop);
            }
        }

        /// <summary>
        ///   Respond to pick of Create Inline Chemistry Zone button on ribbon
        /// </summary>
        /// <param name = "sender">ID of ribbon control</param>
        public void InlineChemTextClick(IRibbonControl sender) {
            try {
                core.AddInlineChemText();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                                MessageBoxImage.Stop);
            }
        }

        /// <summary>
        ///   Respond to pick of Navigator button on ribbon
        /// </summary>
        /// <param name = "sender">ID of ribbon control</param>
        /// <param name = "pressed">whether the Navigator button in the ribbon was pressed to generate this call</param>
        public void NavigatorClick(IRibbonControl sender, bool pressed) {
            CustomTaskPane custTaskPane = null;
            foreach (CustomTaskPane taskPane in customTaskPanes) {
                if (wordApp.ActiveWindow == taskPane.Window) {
                    custTaskPane = taskPane;
                }
            }

            if (pressed) {
                if (custTaskPane == null) {
                    navCustControl = new ChemistryNavigatorHostControl(core.ActiveChemistryDocument);

                    custTaskPane = customTaskPanes.Add(navCustControl, Properties.Resources.CHEMISTRY_NAVIGATOR_TITLE,
                                                       wordApp.ActiveWindow);
                    custTaskPane.Visible = true;
                    foreach (CustomTaskPane taskPane in customTaskPanes) {
                        //if (Properties.Resources.CHEMISTRY_NAVIGATOR_TITLE.Equals(custTaskPane.Title))
                        if (Properties.Resources.CHEMISTRY_NAVIGATOR_TITLE.Equals(taskPane.Title)) {
                            custTaskPane.Width = 270;
                        }
                    }
                    custTaskPane.VisibleChanged += TaskPaneVisibleChanged;
                } else {
                    custTaskPane.Visible = true;
                }
            } else {
                if (custTaskPane != null) {
                    custTaskPane.Visible = false;
                }
            }
        }

        private void CoreDocumentBeforeClose(object sender, ChemistryDocumentEventArgs e) {
            foreach (var taskPane in from taskPane in customTaskPanes
                                                where taskPane.Control is ChemistryNavigatorHostControl
                                                let navHost = taskPane.Control as ChemistryNavigatorHostControl
                                                let chemNavigator = navHost.innerUI.Child as ChemistryNavigator
                                                where e.CurrentDocument == chemNavigator.ChemistryDocument.WordDocument
                                                select taskPane) {
                taskPane.Visible = false;
                customTaskPanes.Remove(taskPane);
                break;
            }
        }

        /// <summary>
        ///   Get whether the Navigator button in the Ribbon is enabled or not
        /// </summary>
        /// <param name = "sender">ID of ribbon control</param>
        /// <returns>true if enabled, false otherwise</returns>
        public bool GetNavigatorPressed(IRibbonControl sender) {
            return NavigatorButtonVisible;
        }

        private void TaskPaneVisibleChanged(object sender, EventArgs e) {
            var custTaskPane = (CustomTaskPane) sender;
            NavigatorButtonVisible = custTaskPane.Visible;
        }

        private static string GetResourceText(string resourceName) {
            var asm = Assembly.GetExecutingAssembly();
            var resourceNames = asm.GetManifestResourceNames();
            foreach (var t in
                resourceNames.Where(t => string.Equals(resourceName, t, StringComparison.OrdinalIgnoreCase))) {
                using (
                    var resourceReader = new StreamReader(asm.GetManifestResourceStream(t))) {
                    return resourceReader.ReadToEnd();
                }
            }
            return null;
        }

        /// <summary>
        ///   Dispose method with a bool value
        /// </summary>
        /// <param name = "disposing">boolean argument</param>
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                // free managed resources
                if (navCustControl != null) {
                    navCustControl.Dispose();
                    navCustControl = null;
                }
            }
        }

        #region GetImageButton

        /// <summary>
        ///   Load Import CML image
        /// </summary>
        /// <param name = "control">ID of ribbon control to associate image with</param>
        /// <returns>pointer to the image</returns>
        public IPictureDisp GetChemImage(IRibbonControl control) {
            return ImageConverter.Convert(Properties.Resources.Chem4Word_Icon);
        }

        public IPictureDisp GetFromFileImage(IRibbonControl control) {
            return ImageConverter.Convert(Properties.Resources.From_File);
        }

        public IPictureDisp GetEditLabelsImage(IRibbonControl control) {
            return ImageConverter.Convert(Properties.Resources.Edit_Labels);
        }

        public IPictureDisp GetViewAsXmlImage(IRibbonControl control) {
            return ImageConverter.Convert(Properties.Resources.xml);
        }

        public IPictureDisp GetViewOptionsImage(IRibbonControl control) {
            return ImageConverter.Convert(Properties.Resources.view);
        }

        public IPictureDisp GetDocumentStylesImage(IRibbonControl control) {
            return ImageConverter.Convert(Properties.Resources.DocumentStyles);
        }

        public IPictureDisp GetOptionsImage(IRibbonControl control) {
            return ImageConverter.Convert(Properties.Resources.Options);
        }

        public IPictureDisp GetNavigatorImage(IRibbonControl control) {
            return ImageConverter.Convert(Properties.Resources.molecule_navigation);
        }

        public IPictureDisp GetHelpImage(IRibbonControl control) {
            return ImageConverter.Convert(Properties.Resources.help);
        }

        public IPictureDisp GetEditImage(IRibbonControl control) {
            return ImageConverter.Convert(Properties.Resources.edit);
        }

        /// <summary>
        ///   Load Create 1-D Chemical Zone image
        /// </summary>
        /// <param name = "control">ID of ribbon control to associate image with</param>
        /// <returns>pointer to the image</returns>
        public IPictureDisp Get1DChemZoneImage(IRibbonControl control) {
            return ImageConverter.Convert(Properties.Resources.OneDChemZone);
        }

        /// <summary>
        ///   Load Create 1-D Chemical Zone image
        /// </summary>
        /// <param name = "control">ID of ribbon control to associate image with</param>
        /// <returns>pointer to the image</returns>
        public IPictureDisp GetOpsinImage(IRibbonControl control) {
            return ImageConverter.Convert(Properties.Resources.Search);
        }

        #endregion
    }
}