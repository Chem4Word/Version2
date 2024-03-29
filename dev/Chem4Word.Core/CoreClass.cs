﻿// -----------------------------------------------------------------------
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
using System.Net;
using System.Resources;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Chem4Word.Api;
using Chem4Word.Api.Core;
using Chem4Word.Api.Events;
using Chem4Word.Common;
using Chem4Word.Common.Utilities;
using Chem4Word.Core.Events;
using Chem4Word.Core.SmartTag;
using Chem4Word.Core.UserSetting;
using Chem4Word.UI;
using Chem4Word.UI.ChemDoodle;
using Chem4Word.UI.CmlViewer;
using Chem4Word.UI.Import;
using Chem4Word.UI.ManageView;
using Chem4Word.UI.OOXML;
using Chem4Word.UI.Properties;
using Chem4Word.UI.Tools;
using Chem4Word.UI.TwoD;
using Chem4Word.UI.UIControls;
using Chem4Word.UI.WebServices;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using log4net;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Word.Extensions;
using Microsoft.Win32;
using Numbo;
using Numbo.Cml;
using Numbo.Coa;
using Application = Microsoft.Office.Interop.Word.Application;
using Document = Microsoft.Office.Interop.Word.Document;
using MessageBox = System.Windows.MessageBox;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Point = System.Windows.Point;
using ProgressBar = Chem4Word.UI.UIControls.ProgressBar;
using Shape = Microsoft.Office.Interop.Word.Shape;
using Timer = System.Timers.Timer;
using Window = Microsoft.Office.Interop.Word.Window;

namespace Chem4Word.Core
{
    /// <summary>
    ///   This function as a controller between Chem4Word UI and Word API.
    ///   This class also controll how Chemistry Zone was Created, Edited, Navigated, etc...
    /// </summary>
    public class CoreClass : ICoreClass
    {

        private bool overwrite = true;

        private static readonly ILog Log = LogManager.GetLogger(typeof(CoreClass));
        private readonly string AddInManifestKeyName = @"Manifest";

        private readonly string AddInRegistryKeyPath =
            @"Software\Microsoft\Office\Word\Addins\Chemistry Add-in for Word";

        internal readonly string assemblyDirectoryName;
        private readonly Dictionary<Document, IChemistryDocument> documentDictionary;
        private readonly GalleryDictionaryManager galleryDictionaryManager;

        internal readonly string localAppDataFolder;
        private readonly ChemistrySmartTag smartTag;
        private readonly Application wordApp;

        private static List<String> _chemistryZoneNames;
        private static List<ChemistryZoneMatch> _zoneMatches;

        private static TermDictionaryManager _termDictionary;

        private static Timer _purgeTimer;
        //private ContentControl controlToTimer;

        private static Telemetry _telemetry;

        /// <summary>
        /// Get The Screen position of Word
        /// </summary>
        public Point WordTopLeft
        {
            get
            {
                Point p = new Point();

                // Get position of Standard CommandBar (<ALT>+F)
                CommandBar menubar = wordApp.CommandBars["Standard"];
                p.X = menubar.Left + 6;
                p.Y = menubar.Top + 12;

                return p;
            }
        }

        /// <summary>
        /// Gets an instance of telemetry object
        /// </summary>
        /// <returns></returns>
        public Telemetry GetTelemetry()
        {
            return _telemetry;
        }

        static CoreClass()
        {
            // Required to initialise the WPF context, prevents a bug in the RibbonControl.
            System.Windows.Window w = new System.Windows.Window();
        }

        /// <summary>
        ///   Initializes a new instance of the Core class.
        /// </summary>
        public CoreClass(Application wordApplication)
        {
            string module = "CoreClass()";

            Log.Info("creating a new CORE - word app: " + wordApplication);
            _telemetry = new Telemetry();

            try
            {
                // Initialization
                localAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                  Properties.Resources.CHEM4WORD_LOCATION);
                assemblyDirectoryName = GetAssemblyDirectoryName();
                wordApp = wordApplication;

                galleryDictionaryManager = new GalleryDictionaryManager();
                documentDictionary = new Dictionary<Document, IChemistryDocument>();

                // Load SmartTag
                smartTag = new ChemistrySmartTag(this);

                // Load user setting
                InitialiseUserSettings();

                // Check if [LocalApplicationData]\Chem4Word\Chemistry Gallery\Chem4Word{version}.dotx
                //  is missing and recover it from Program Folder if necessary
                CheckForRecovery();

                //Register Events
                wordApp.WindowActivate += ApplicationWindowActivate;
                wordApp.WindowSelectionChange += WordAppWindowSelectionChange;

                wordApp.DocumentOpen += WordAppDocumentOpen;
                wordApp.DocumentChange += WordAppDocumentChange;
                wordApp.DocumentBeforeClose += WordAppDocumentBeforeClose;
                wordApp.DocumentBeforeSave += WordAppDocumentBeforeSave;

                wordApp.WindowDeactivate += ApplicationWindowDeactivate;
                wordApp.WindowBeforeRightClick += ChemistryContextMenuHelper.Instance(this, wordApp).WindowBeforeRightClick;
                wordApp.WindowBeforeDoubleClick += WordAppWindowBeforeDoubleClick;

#if DEBUG
#else
                // Ensure Open Xml SDK 2.0 is installed
                EnsureOpenXmlSdkIsInstalled();
#endif
                // Check to see if we are running the latest version
                CheckForUpdates();
            }
            catch (Exception ex)
            {
                _telemetry.Write(module, "Exception", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Ensure that Open XML SDK is installed and recover it if necessary
        /// </summary>
        private void EnsureOpenXmlSdkIsInstalled()
        {
            string module = "CoreClass.EnsureOpenXmlSdkIsInstalled()";
            bool openXmlSdkIsInstalled = false;

            try
            {
                string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".docx");
                // Create a Wordprocessing document.
                using (WordprocessingDocument package = WordprocessingDocument.Create(tempFile, WordprocessingDocumentType.Document))
                {
                    // Add a new main document part.
                    package.AddMainDocumentPart();

                    // Create the Document DOM.
                    package.MainDocumentPart.Document =
                        new DocumentFormat.OpenXml.Wordprocessing.Document(
                            new Body(
                                new Paragraph(
                                    new Run(
                                        new Text("Hello World!")))));

                    // Save changes to the main document part.
                    package.MainDocumentPart.Document.Save();
                }

                if (File.Exists(tempFile))
                {
                    openXmlSdkIsInstalled = true;
                    File.Delete(tempFile);
                }
            }
            catch (Exception)
            {
                // Do Nothing
            }

            try
            {
                if (!openXmlSdkIsInstalled)
                {
                    _telemetry.Write(module, "Information", "Installing Open XML SDK 2.0");

                    string openXmkSdkDirectLink = "https://download.microsoft.com/download/2/7/F/27FF6744-D970-4FFB-90B8-5226B2B82E0A/OpenXMLSDKv2.msi";

                    string tempPath = Path.GetTempPath();
                    string msiFile = Path.Combine(tempPath, Guid.NewGuid() + "-OpenXMLSDKv2.msi");

                    WebClient client = new WebClient();
                    client.DownloadFile(openXmkSdkDirectLink, msiFile);

                    if (File.Exists(msiFile))
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = msiFile;
                        p.StartInfo.Arguments = "/passive";
                        p.Start();
                    }
                    else
                    {
                        // Try again from our backup location
                        openXmkSdkDirectLink = "https://www.doublewide.co.uk/files/OpenXMLSDKv2.msi";
                        client.DownloadFile(openXmkSdkDirectLink, msiFile);

                        if (File.Exists(msiFile))
                        {
                            Process p = new Process();
                            p.StartInfo.FileName = msiFile;
                            p.StartInfo.Arguments = "/passive";
                            p.Start();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Do Nothing !
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int WordVersion
        {
            get
            {
                return _telemetry.WordVersion;
            }
        }

        /// <summary>
        ///   Used to retrieve the item.
        /// </summary>
        public object FetchItem { get; set; }

        /// <summary>
        ///   Used to retrieve the XDocument.
        /// </summary>
        public XDocument SetCml { get; set; }

        /// <summary>
        ///   Used to retrieve the XDocument.
        /// </summary>
        public XDocument GetCml { get; set; }

        /// <summary>
        ///   Used to retrieve the assembly directory name.
        /// </summary>
        public string GetAssemblyDirectory
        {
            get { return assemblyDirectoryName; }
        }

        /// <summary>
        /// </summary>
        public ChemistrySmartTag SmartTag
        {
            get { return smartTag; }
        }

        #region ICoreClass Members

        /// <summary>
        ///   Gets the active document.
        /// </summary>
        /// <returns>Return an active document <see cref = "ChemistryDocument" /></returns>
        public IChemistryDocument ActiveChemistryDocument { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chemistryZone"></param>
        /// <param name="chemistryZonePropertiesForCopiedZone"></param>
        /// <returns></returns>
        public IChemistryZone CreateCopy(IChemistryZone chemistryZone,
                                         ChemistryZoneProperties chemistryZonePropertiesForCopiedZone)
        {
            if (chemistryZonePropertiesForCopiedZone == null)
            {
                chemistryZonePropertiesForCopiedZone = chemistryZone.Properties;
            }
            IChemistryZone chemZone = CreateDuplicatedChemistryZone(chemistryZone, chemistryZonePropertiesForCopiedZone,
                                                                    false);
            return chemZone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chemistryZone"></param>
        /// <param name="chemistryZonePropertiesForLinkedZone"></param>
        /// <returns></returns>
        public IChemistryZone CreateLink(IChemistryZone chemistryZone,
                                         ChemistryZoneProperties chemistryZonePropertiesForLinkedZone)
        {
            if (chemistryZone == null)
            {
                MessageBox.Show(
                    "Direct insertion as a bold number is not yet supported.", Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE,
                    MessageBoxButton.OK, MessageBoxImage.Stop);
                return null;
            }
            if (chemistryZonePropertiesForLinkedZone == null)
            {
                chemistryZonePropertiesForLinkedZone = chemistryZone.Properties;
            }
            IChemistryZone chemZone = CreateDuplicatedChemistryZone(chemistryZone, chemistryZonePropertiesForLinkedZone,
                                                                    true);
            return chemZone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chemistryZone"></param>
        /// <returns></returns>
        public IChemistryZone BreakLinks(IChemistryZone chemistryZone)
        {
            MessageBox.Show(
                "Breaking links is not yet supported.",
                Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK, MessageBoxImage.Stop);
            return null;
        }

        /// <summary>
        ///   Create Chemistry Zone from user selected CML file.
        ///   This method will create Chemistry Zone if the selected CML file is valid. Otherwise, it will not.
        /// </summary>
        /// <param name = "fileName">File name that point to Cml or Xml file is going to be import into Word document</param>
        public IChemistryZone ImportCmlFile(string fileName)
        {
            string module = "CoreClass.ImportCmlFile()";

            string temp = Path.GetTempPath();
            string tempCmlFileName = Path.Combine(temp, "Blank.cml");

            if (!fileName.Equals(tempCmlFileName))
            {
                string extension = Path.GetExtension(fileName).ToLower();
                _telemetry.Write(module, "Information", "Importing " + extension + " file");
            }

            IChemistryZone chemistryZone = null;
            ImportMediator importMediator = new ImportMediator();
            switch (Setting.Import)
            {
                case ImportSetting.StrictFail:
                    importMediator.ParseSeverity = ImportMediator.Severity.Strict;
                    break;

                case ImportSetting.Prompt:
                    importMediator.ParseSeverity = ImportMediator.Severity.Prompt;
                    break;

                case ImportSetting.Auto:
                    importMediator.ParseSeverity = ImportMediator.Severity.Auto;
                    break;
            }

            importMediator.Start(fileName);
            if (importMediator.Worked())
            {
                ContextObject contextObject = Cid.RemoveMoleculeBoldNumberLabels(importMediator.GetContextObject(),
                                                                                 importMediator.GetContextObject().Cml.Root);

                DepictionOption documentDepictionOption = GetPreferedDocumentDepiction(contextObject,
                                                                                       Setting.DocumentPreferedDepiction);
                DepictionOption navigatorDepictionOption = GetPreferedNavigatorDepiction(contextObject,
                                                                                         Setting.NavigatorPreferedDepiction);
                ChemistryZoneProperties chemistryZoneProperties = new ChemistryZoneProperties(documentDepictionOption,
                                                                                              navigatorDepictionOption,
                                                                                              true);

                CmlMolecule mol = new CmlMolecule((XElement)documentDepictionOption.MachineUnderstandableOption);
                double? averageBondLength = mol.GetAverageBondLength();

                if (!fileName.Equals(tempCmlFileName))
                {
                    if (mol.GetAllBonds().Count > 0)
                    {
                        _telemetry.Write(module, "Information", "Bond Lengths; Median: "
                                                                +
                                                                SafeDouble.AsString(mol.GetMedianBondLength(), "#0.000")
                                                                + " Average: " +
                                                                SafeDouble.AsString(averageBondLength, "#0.000"));
                    }
                }

                if (WordVersion > 2007 && averageBondLength < 5)
                {
                    mol.ScaleToAverageBondLength(20);
                    _telemetry.Write(module, "Information",
                        "Changed average bond length from "
                        + SafeDouble.AsString(averageBondLength, "#0.000") + " to 20");
                }

                Range range = wordApp.ActiveDocument.ActiveWindow.Selection.Range;
                chemistryZone = AddNewContextObjectToDocument(range, contextObject,
                                                              chemistryZoneProperties);
            }
            else
            {
                _telemetry.Write(module, "Error", "Import failed " + importMediator.BriefMessage);
            }
            return chemistryZone;
        }

        ///<summary>
        ///</summary>
        ///<param name = "richTextContentControl"></param>
        ///<param name = "oneDDepictionOption"></param>
        public void CreateOneDZoneContent(ref ContentControl richTextContentControl, DepictionOption oneDDepictionOption)
        {
            string module = "CoreClass.CreateOneDZoneContent()";

            if (oneDDepictionOption == null)
            {
                throw new ArgumentNullException("oneDDepictionOption");
            }
            if (Depiction.Is2D(oneDDepictionOption))
            {
                throw new ArgumentOutOfRangeException("oneDDepictionOption");
            }

            _telemetry.Write(module, "Information", oneDDepictionOption.DepictionOptionDescription);

            bool oldValue = wordApp.AutoCorrect.CorrectSentenceCaps;
            wordApp.AutoCorrect.CorrectSentenceCaps = false;
            richTextContentControl.Range.Text = oneDDepictionOption.GetAsOMath();
            // add the internal Math zone to access the math editor
            richTextContentControl.Range.OMaths.Add(richTextContentControl.Range);
            richTextContentControl.Range.Bold = Depiction.IsMoleculeBoldNumberLabel(oneDDepictionOption) ? 1 : 0;
            richTextContentControl.Range.Text = oneDDepictionOption.GetAsOMath();
            richTextContentControl.Range.OMaths.BuildUp();
            richTextContentControl.Range.Italic = 0;
            richTextContentControl.Range.Font.Name = Properties.Resources.ChemistryFont;
            wordApp.AutoCorrect.CorrectSentenceCaps = oldValue;
        }

        /// <summary>
        /// </summary>
        public event EventHandler<ContentControlEventArgs> ContentControlSelectionChanged;

        #endregion ICoreClass Members

        private void InitialiseUserSettings()
        {
            string module = "CoreClass.InitialiseUserSettings()";
            try
            {
                Setting.LoadSettings(localAppDataFolder + @"\User Setting.xml");
            }
            catch (Exception ex)
            {
                _telemetry.Write(module, "Exception", ex.Message);
            }
        }

        private void WordAppWindowBeforeDoubleClick(Selection Sel, ref bool Cancel)
        {
            switch (Sel.ContentControls.Count)
            {
                case 0:
                    break;

                case 1:
                    {
                        object firstIndex = 1;
                        ContentControl cc = Sel.ContentControls.get_Item(ref firstIndex);

                        // check if the content control is a chemistry zone
                        if (Properties.Resources.ChemistryZoneAlias.Equals(cc.Title))
                        {
                            LoadAppropriateEditor(ActiveChemistryDocument.SelectedChemistryZone);
                        }
                        break;
                    }
            }
        }

        ///<summary>
        ///</summary>
        ///<param name = "chemistryZone"></param>
        public void LoadAppropriateEditor(IChemistryZone chemistryZone)
        {
            if (
                Depiction.Is2D(
                    DepictionOption.CreateDepictionOption(chemistryZone.Cml,
                                                          chemistryZone.Properties
                                                              .DocumentDepictionOptionXPath)))
            {
                //Tweak2D(chemistryZone);
                TweakDoodle2D(chemistryZone, false);
            }
            else
            {
                EditLabels(chemistryZone);
            }
        }

        /// <summary>
        ///   Method to find the assembly directory name.
        /// </summary>
        /// <returns>
        ///   Returns the install path of the AddIn from the registry.
        /// </returns>
        private string GetAssemblyDirectoryName()
        {
            string addInInstallPath = String.Empty;

            string codebase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codebase);
            string path = Uri.UnescapeDataString(uri.Path);
            addInInstallPath = Path.GetDirectoryName(path);

            return addInInstallPath;
        }

        private void WordAppDocumentChange()
        {
            try
            {
                var activeDocument = wordApp.ActiveDocument;
                try
                {
                    if (documentDictionary == null || documentDictionary.Count < 1 ||
                        !(documentDictionary.ContainsKey(activeDocument)))
                    {
                        WordAppDocumentOpen(wordApp.ActiveDocument);
                    }
                }
                catch (Exception exception)
                {
                    Log.Error("Error in WordAppDocumentChange", exception);
                }
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                // this means that the final document is closed so
                // we don't need to do anything
            }
        }

        /// <summary>
        ///   Create duplicated Chemistry Zone at the current cursor.
        /// </summary>
        /// <param name = "chemZone">Source Chemistry Zone.</param>
        /// <param name = "chemistryZoneProperties"></param>
        /// <param name = "linked">True: Linked CML, False: Unlinked CML.</param>
        public IChemistryZone CreateDuplicatedChemistryZone(IChemistryZone chemZone,
                                                            ChemistryZoneProperties chemistryZoneProperties, bool linked)
        {
            // TODO BOLD NUMBER options ...
            string module = "CoreClass.CreateDuplicatedChemistryZone()";
            WriteTelemetry(module, "Information", "Called");
            // waiting for user options class
            // do we destroy old number, autogenerate new number or prompt for new one?
            if (wordApp.Selection.Range.ContentControls.Count > 0)
            {
                for (int indx = 1; indx <= wordApp.Selection.Range.ContentControls.Count; indx++)
                {
                    object objIndex = indx;
                    ContentControl contentControl = wordApp.Selection.Range.ContentControls.get_Item(ref objIndex);

                    if (Properties.Resources.ChemistryZoneAlias.Equals(contentControl.Title))
                    {
                        MessageBox.Show(
                            "Cannot create nested Chemistry Zone.\nDeselect Chemistry Zone before create duplicated Zone.",
                            Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE,
                            MessageBoxButton.OK, MessageBoxImage.Warning);

                        return null;
                    }
                }
            }

            DepictionOption documentDepictionOption =
                DepictionOption.CreateDepictionOption(chemZone.Cml, chemistryZoneProperties.DocumentDepictionOptionXPath);
            ContentControl control;
            if (Depiction.Is2D(documentDepictionOption))
            {
                // Existing code goes here
                // zone is 2D - therefore a picture content control is necessary
                var contexObject = chemZone.AsContextObject();
                var cmlMolecule = new CmlMolecule((XElement)documentDepictionOption.MachineUnderstandableOption);
                object missing = Type.Missing;

                if (WordVersion > 2007)
                {
                    DateTime started = DateTime.Now;

                    WriteTelemetry(module, "Information", "Atoms: " + cmlMolecule.GetAllAtoms().Count());
                    WriteTelemetry(module, "Information", "Bonds: " + cmlMolecule.GetAllBonds().Count());

                    OoXmlOptions options = new OoXmlOptions();
                    options.ColouredAtoms = Setting.RenderAtomsInColour;
                    options.ShowHydrogens = Setting.RenderImplicitHydrogens;
                    options.TopLeft = WordTopLeft;

                    string guidString = Guid.NewGuid().ToString("N");
                    string bookmarkName = "C4W_" + guidString;
                    OoXmlFile ooXmlFile = new OoXmlFile(_telemetry);
                    string tempfileName = ooXmlFile.CreateFromCml(contexObject.Cml.ToString(), guidString, options);

                    control = wordApp.ActiveDocument.ContentControls.Add(
                        WdContentControlType.wdContentControlRichText, ref missing);
                    control.SetPlaceholderText(Text: " ");

                    DateTime started2 = DateTime.Now;
                    control.Range.InsertFile(tempfileName, bookmarkName);
                    TimeSpan ts2 = DateTime.Now - started2;
                    //WriteTelemetry(module, "Information", "Range.InsertFile took " + ts2.TotalMilliseconds.ToString("#,##0.0") + "ms");

                    if (wordApp.ActiveDocument.Bookmarks.Exists(bookmarkName))
                    {
                        wordApp.ActiveDocument.Bookmarks[bookmarkName].Delete();
                    }
                    File.Delete(tempfileName);

                    TimeSpan ts = DateTime.Now - started;
                    WriteTelemetry(module, "Information", "Rendering OOXML took " + ts.TotalMilliseconds.ToString("#,##0.0") + "ms");
                }
                else
                {
                    var editor = new CanvasContainer(contexObject, cmlMolecule.CloneMolecule(1.54, false));
                    editor.GeneratePng(false);

                    control = wordApp.ActiveDocument.ContentControls.Add(
                        WdContentControlType.wdContentControlPicture, ref missing);
                    control.SetPlaceholderText(Text: " ");

                    control.Range.InlineShapes.AddPicture(editor.PngFileOutput, ref missing, ref missing, ref missing);

                    try
                    {
                        // Sometime the the open state of the file is not update quickly enough,
                        // So that we need to invoke GC to refresh the environment states.
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        // Delete the png file
                        File.Delete(editor.PngFileOutput);
                    }
                    catch (IOException)
                    {
                        /*
                         * IOException occurs if the specified file is in use in which case
                         * we just move on - another temp file will exist in the temp directory
                         * which is not a tragedy
                         */
                    }
                }
            }
            else
            {
                // zone is 1D
                object missing = Type.Missing;
                control = wordApp.ActiveDocument.ContentControls.Add(
                    WdContentControlType.wdContentControlRichText, ref missing);
                control.SetPlaceholderText(Text: " ");
                CreateOneDZoneContent(ref control, documentDepictionOption);
            }

            control.Title = Properties.Resources.ChemistryZoneAlias;

            IChemistryZone newChemistryZone;
            if (linked)
            {
                newChemistryZone = ActiveChemistryDocument.DocumentContentControlAfterAdd(control,
                                                                                          chemistryZoneProperties,
                                                                                          chemZone);
            }
            else
            {
                var newChemistryZoneProperties = chemistryZoneProperties.Clone();
                newChemistryZone = ActiveChemistryDocument.DocumentContentControlAfterAdd(control, chemZone.Cml,
                                                                                          newChemistryZoneProperties);
            }
            // Select the added Chemistry Zone.
            wordApp.Selection.SetRange(control.Range.End + 1, control.Range.End + 1);
            newChemistryZone.Lock();
            return newChemistryZone;
        }

        /// <summary>
        /// </summary>
        public event EventHandler<ChemistryDocumentEventArgs> WindowActivate;

        /// <summary>
        ///   Event raised when the application is deactivated.
        /// </summary>
        public event EventHandler<ChemistryDocumentEventArgs> WindowDeactivateEvent;

        /// <summary>
        /// </summary>
        public event EventHandler<ChemistryDocumentEventArgs> DocumentBeforeClose;

        private static ControlProperties.ChemistryControlType TypeFromTypeName(string typeName)
        {
            string shortName = typeName.Substring(typeName.LastIndexOf('.') + 1);
            return
                (ControlProperties.ChemistryControlType)
                Enum.Parse(typeof(ControlProperties.ChemistryControlType), shortName);
        }

        private static void WordAppDocumentBeforeSave(Document doc, ref bool saveAsUI,
                                                      ref bool cancel)
        {
            if (doc.HasVstoObject())
            {
                Microsoft.Office.Tools.Word.Document vstoDoc = doc.GetVstoObject();
                List<ControlProperties> savedControls = new List<ControlProperties>();

                if (vstoDoc.Controls.Count > 0)
                {
                    for (int i = 0; i < vstoDoc.Controls.Count; i++)
                    {
                        DocumentHostControl vstoControl =
                            vstoDoc.Controls[i] as DocumentHostControl;

                        // Check to see if the control is a Windows Forms control.
                        if (vstoControl != null)
                        {
                            ControlProperties.ChemistryControlType type =
                                TypeFromTypeName(vstoControl.GetType().ToString());

                            // The control is a Windows Forms control, so determine whether the control is
                            // floating or inline with the text.
                            Shape controlShape = vstoDoc.Controls.GetShapeForControl(vstoControl);
                            if (controlShape != null)
                            {
                                // The control is a floating Windows Forms control, so save its
                                // top and left properties.
                                savedControls.Add(new ControlProperties(
                                                      vstoControl.Name,
                                                      type,
                                                      controlShape.Left,
                                                      controlShape.Top,
                                                      controlShape.Width,
                                                      controlShape.Height,
                                                      vstoControl.CustomXMLId,
                                                      GetPersistentProperties(vstoControl)));
                            }
                            else
                            {
                                // The control is an inline Windows Forms control, so save its
                                // start and end properties.
                                InlineShape controlInlineShape =
                                    vstoDoc.Controls.GetInlineShapeForControl(vstoControl);
                                Debug.Assert(controlInlineShape != null);
                                savedControls.Add(new ControlProperties(
                                                      vstoControl.Name,
                                                      type,
                                                      controlInlineShape.Range.Start,
                                                      controlInlineShape.Range.End,
                                                      vstoControl.Width,
                                                      vstoControl.Height,
                                                      vstoControl.CustomXMLId,
                                                      GetPersistentProperties(vstoControl)));
                            }
                        }
                    }
                }

                savedControls.Sort(new ControlCollectionComparer());
                ControlsStorage.Store(doc, savedControls.ToArray());
            }
        }

        private static KeyValuePair<string, object>[] GetPersistentProperties(DocumentHostControl c)
        {
            List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
            DocumentHostControl documentHostControl;
            if ((documentHostControl = c) != null)
            {
                list.Add(new KeyValuePair<string, object>("CMLCustomXML", documentHostControl.CustomXMLId));
                list.Add(new KeyValuePair<string, object>("Width", documentHostControl.Width));
                list.Add(new KeyValuePair<string, object>("Height", documentHostControl.Height));
            }
            return list.ToArray();
        }

        private void CheckForUpdates()
        {
            string module = "CoreClass.CheckForUpdates()";

            try
            {
                if (!string.IsNullOrEmpty(assemblyDirectoryName))
                {
                    #region CheckForUpdate

                    bool doCheck = true;

                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Chem4Word", true);
                    if (key == null)
                    {
                        key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Chem4Word");
                    }

                    string lastChecked = null;
                    try
                    {
                        lastChecked = key.GetValue("Last Update Check").ToString();
                    }
                    catch
                    {
                        // Should only happen if the value does not exist
                    }

                    if (!string.IsNullOrEmpty(lastChecked))
                    {
                        DateTime last;
                        if (DateTime.TryParse(lastChecked, out last))
                        {
                            TimeSpan delta = DateTime.Today - last;
                            if (delta.Days < 7)
                            {
                                doCheck = false;
                            }
                        }
                    }

                    if (doCheck)
                    {
                        key.SetValue("Last Update Check", DateTime.Today.ToString("yyyy-MM-dd"));

                        string existingVersionXmlFile = Path.Combine(assemblyDirectoryName, @"Data\This-Version.xml");
                        if (File.Exists(existingVersionXmlFile))
                        {
                            XDocument currentVersion = XDocument.Load(existingVersionXmlFile);
                            string currentVersionNumber = currentVersion.Root.Element("Number").Value;
                            DateTime currentReleaseDate = SafeDate.Parse(currentVersion.Root.Element("Released").Value);
                            Debug.WriteLine("Current Version " + currentVersionNumber + " Released " + currentReleaseDate.ToString("dd-MMM-yyyy"));

                            string tempPath = Path.GetTempPath();
                            string chem4wordVersionsXml = "Chem4Word-Versions.xml";

                            string oldVersionXmlFile = Path.Combine(tempPath, chem4wordVersionsXml);
                            if (File.Exists(oldVersionXmlFile))
                            {
                                File.Delete(oldVersionXmlFile);
                            }

                            string guid = Guid.NewGuid().ToString();
                            string latestVersionXmlFile = Path.Combine(tempPath, guid + "-" + chem4wordVersionsXml);

                            string versionsLink = "http://www.chem4word.co.uk/files/" + chem4wordVersionsXml;
                            WebClient client = new WebClient();
                            client.Headers.Add("user-agent", "Chem4Word Add-In");
                            client.DownloadFile(versionsLink, latestVersionXmlFile);
                            client.Dispose();

                            bool updateRequired = false;

                            if (File.Exists(latestVersionXmlFile))
                            {
                                string fileContents = File.ReadAllText(latestVersionXmlFile);
                                if (fileContents.Contains("<ChangeLog>"))
                                {
                                    #region Got Our File

                                    XDocument latestVersion = XDocument.Load(latestVersionXmlFile);
                                    var versions = latestVersion.XPathSelectElements("//Version");
                                    foreach (var version in versions)
                                    {
                                        var thisVersionNumber = version.Element("Number").Value;
                                        DateTime thisVersionDate = SafeDate.Parse(version.Element("Released").Value);
                                        Debug.WriteLine("New Version " + thisVersionNumber + " Released " + thisVersionDate.ToString("dd-MMM-yyyy"));
                                        if (thisVersionDate > currentReleaseDate)
                                        {
                                            updateRequired = true;
                                            break;
                                        }
                                    }

                                    try
                                    {
                                        File.Delete(latestVersionXmlFile);
                                    }
                                    catch (Exception)
                                    {
                                        // Do Nothing
                                    }

                                    if (updateRequired)
                                    {
                                        AutomaticUpdate au = new AutomaticUpdate(_telemetry);
                                        au.TopLeft = WordTopLeft;
                                        au.CurrentVersion = currentVersion;
                                        au.NewVersions = latestVersion;

                                        DialogResult dr = au.ShowDialog();
                                    }

                                    #endregion
                                }
                                else
                                {
                                    _telemetry.Write(module, "Exception", "File Chem4Word-Versions.xml is corrupt");
                                    _telemetry.Write(module, "Exception(Data)", fileContents);

                                    UpdateFailure f = new UpdateFailure();
                                    f.TopLeft = WordTopLeft;
                                    f.WebPage = fileContents;
                                    f.ShowDialog();
                                }
                            }
                        }

                    }
                    #endregion
                }
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.Message);
                Log.Error(Ex.Message);
                _telemetry.Write(module, "Exception", Ex.Message);
                _telemetry.Write(module, "Exception(Data)", Ex.StackTrace);
                if (Ex.InnerException != null)
                {
                    _telemetry.Write(module, "Exception", Ex.InnerException.Message);
                    _telemetry.Write(module, "Exception(Data)", Ex.InnerException.StackTrace);
                }
            }
        }

        /// <summary>
        ///   Check Chem4Word Gallery dictionary files in Local Application Data. C:\Users\[user-name]\AppData\Local\Chemistry Gallery
        ///   If there are some files missing, the Add-in will recover those files from Program folder. C:\Program Files\Chem4Word\Data
        /// </summary>
        private void CheckForRecovery()
        {
            string module = "CoreClass.CheckForRecovery()";
            try
            {
                if (!string.IsNullOrEmpty(assemblyDirectoryName))
                {
                    #region Templates

                    string templateFileName = "Chem4Word2010.dotx";
                    string sourcePath = @"Template-2010";
                    if (WordVersion == 2007)
                    {
                        templateFileName = "Chem4Word2007.dotx";
                        sourcePath = @"Template-2007";
                    }

                    // If files are missing, recover them
                    if (!Directory.Exists(localAppDataFolder + @"\Chemistry Gallery"))
                    {
                        Directory.CreateDirectory(localAppDataFolder + @"\Chemistry Gallery");
                    }

                    if (!File.Exists(localAppDataFolder + @"\Chemistry Gallery\" + templateFileName))
                    {
                        string[] galleryFiles = Directory.GetFiles(assemblyDirectoryName + @"\Data\" + sourcePath);
                        foreach (string file in galleryFiles)
                        {
                            FileInfo fileInfor = new FileInfo(file);
                            if (!File.Exists(localAppDataFolder + @"\Chemistry Gallery\" + fileInfor.Name))
                            {
                                File.Copy(assemblyDirectoryName + @"\Data\" + sourcePath + @"\" + fileInfor.Name,
                                          localAppDataFolder + @"\Chemistry Gallery\" + fileInfor.Name,
                                          overwrite);
                            }
                        }
                    }

                    #endregion Templates
                }
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.Message);
                Log.Error(Ex.Message);
                _telemetry.Write(module, "Exception", "Templates - " + Ex.Message);
            }
            try
            {
                if (!string.IsNullOrEmpty(assemblyDirectoryName))
                {
                    #region Smart Tags

                    if (!Directory.Exists(localAppDataFolder + @"\SmartTag"))
                    {
                        Directory.CreateDirectory(localAppDataFolder + @"\SmartTag");
                    }

                    if (!File.Exists(localAppDataFolder + @"\SmartTag\smart-tag-dict.xml"))
                    {
                        string[] smartTagFiles = Directory.GetFiles(assemblyDirectoryName + @"\Data\SmartTag");
                        foreach (string file in smartTagFiles)
                        {
                            FileInfo fileInfor = new FileInfo(file);
                            if (!File.Exists(localAppDataFolder + @"\SmartTag\" + fileInfor.Name))
                            {
                                File.Copy(assemblyDirectoryName + @"\Data\SmartTag\" + fileInfor.Name,
                                          localAppDataFolder + @"\SmartTag\" + fileInfor.Name,
                                          overwrite);
                            }
                        }
                    }

                    #endregion Smart Tags
                }
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.Message);
                Log.Error(Ex.Message);
                _telemetry.Write(module, "Exception", "Smart Tags - " + Ex.Message);
            }

            try
            {
                if (!string.IsNullOrEmpty(assemblyDirectoryName))
                {
                    #region User Settings

                    if (!File.Exists(localAppDataFolder + @"\User Setting.xml"))
                    {
                        File.Copy(assemblyDirectoryName + @"\User Setting.xml",
                                  localAppDataFolder + @"\User Setting.xml",
                                  overwrite);
                    }
                    else
                    {
                        XmlDataDocument existingUserSettings = new XmlDataDocument();
                        XmlDataDocument latestUserSettings = new XmlDataDocument();

                        existingUserSettings.Load(localAppDataFolder + @"\User Setting.xml");
                        if (File.Exists(assemblyDirectoryName + @"\User Setting.xml"))
                        {
                            latestUserSettings.Load(assemblyDirectoryName + @"\User Setting.xml");
                        }

                        if (existingUserSettings != null && latestUserSettings != null)
                        {
                            XmlNode existingUserSettingsNode = existingUserSettings.SelectSingleNode("/userSetting");
                            XmlNode lastestUserSettingsNode = latestUserSettings.SelectSingleNode("/userSetting");

                            if (existingUserSettingsNode != null && existingUserSettingsNode.HasChildNodes &&
                                lastestUserSettingsNode != null && lastestUserSettingsNode.HasChildNodes)
                            {
                                bool areEqual = CompareUserSettingsFile(existingUserSettingsNode, "/userSetting",
                                                                        lastestUserSettingsNode);

                                if (!areEqual)
                                {
                                    File.Copy(assemblyDirectoryName + @"\User Setting.xml",
                                              localAppDataFolder + @"\User Setting.xml",
                                              overwrite);
                                }
                            }
                        }
                    }

                    #endregion User Settings
                }
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.Message);
                Log.Error(Ex.Message);
                _telemetry.Write(module, "Exception", "User Settings - " + Ex.Message);
            }
        }

        /// <summary>
        ///   Compares the schema of two xml nodes.
        /// </summary>
        /// <param name = "parentNode">Node to compare</param>
        /// <param name = "referenceXPath">Reference Xpath of the root element of the parentNode</param>
        /// <param name = "nodeToCompareAgainst">Node to compare against parentNode</param>
        /// <returns></returns>
        private bool CompareUserSettingsFile(XmlNode parentNode, string referenceXPath, XmlNode nodeToCompareAgainst)
        {
            bool areEqual = true;
            if (!nodeToCompareAgainst.ChildNodes.Count.Equals(parentNode.ChildNodes.Count))
            {
                return false;
            }
            else if (!parentNode.Attributes.Count.Equals(nodeToCompareAgainst.Attributes.Count))
            {
                return false;
            }
            else
            {
                XmlNode tempNode = null;
                foreach (XmlNode node in parentNode.ChildNodes)
                {
                    tempNode =
                        nodeToCompareAgainst.SelectSingleNode(string.Format("/{0}/{1}", referenceXPath, node.Name));
                    if (tempNode == null)
                    {
                        areEqual = false;
                        break;
                    }

                    if (node.HasChildNodes && tempNode.HasChildNodes)
                    {
                        areEqual = areEqual &&
                                   CompareUserSettingsFile(node, string.Format("{0}/{1}", referenceXPath, node.Name),
                                                           tempNode);
                        if (!areEqual)
                        {
                            break;
                        }
                    }
                    else if ((node.HasChildNodes && !tempNode.HasChildNodes) ||
                             (!node.HasChildNodes && tempNode.HasChildNodes))
                    {
                        areEqual = false;
                        break;
                    }
                }
            }

            return areEqual;
        }

        /// <summary>
        ///   Helper function to fire Event if Chemistry Zone was selected.
        /// </summary>
        /// <param name = "selectionState">Indicate True or False if Chemistry Zone was Selected or not.</param>
        private void ContentControlSelection(bool selectionState)
        {
            // Fire event when Content Control Selection state changed
            if (ContentControlSelectionChanged != null)
            {
                ContentControlSelectionChanged(
                    this,
                    new ContentControlEventArgs
                        {
                            Action = selectionState
                        });
            }
        }

        /// <summary>
        ///   Adds a new context object to the document - creates a ChemZone of the relevant type
        /// </summary>
        /// <param name = "range"></param>
        /// <param name = "contextObject"></param>
        /// <param name = "chemistryZoneProperties"></param>
        public IChemistryZone AddNewContextObjectToDocument(Range range, ContextObject contextObject,
                                                            ChemistryZoneProperties chemistryZoneProperties)
        {
            string module = "CoreClass.AddNewContextObjectToDocument()";
            WriteTelemetry(module, "Information", "Called");
            var documentDepictionOption = DepictionOption.CreateDepictionOption(contextObject.Cml,
                                                                                chemistryZoneProperties.
                                                                                    DocumentDepictionOptionXPath);
            if (documentDepictionOption == null)
            {
                throw new ArgumentException("documentDepictionOption",
                                            "Chem4Word cannot add this chemistry to the document because no depiction has been found for the document");
            }
            ContentControl control;
            range.Select();
            object missing = Type.Missing;

            // remove the current text from the selection before adding the picture
            range.Text = string.Empty;

            ActiveChemistryDocument.EventTurnOn = false;

            if (Depiction.Is2D(documentDepictionOption))
            {
                if (WordVersion > 2007)
                {
                    control = wordApp.ActiveDocument.ContentControls.Add(
                        WdContentControlType.wdContentControlRichText, ref missing);
                    control.SetPlaceholderText(Text: " ");

                    try
                    {
                        DateTime started = DateTime.Now;

                        CmlMolecule mol = new CmlMolecule((XElement)documentDepictionOption.MachineUnderstandableOption);
                        WriteTelemetry(module, "Information", "Atoms: " + mol.GetAllAtoms().Count());
                        WriteTelemetry(module, "Information", "Bonds: " + mol.GetAllBonds().Count());

                        OoXmlOptions options = new OoXmlOptions();
                        options.ColouredAtoms = Setting.RenderAtomsInColour;
                        options.ShowHydrogens = Setting.RenderImplicitHydrogens;
                        options.TopLeft = WordTopLeft;

                        string guidString = Guid.NewGuid().ToString("N");
                        string bookmarkName = "C4W_" + guidString;
                        OoXmlFile ooXmlFile = new OoXmlFile(_telemetry);
                        string tempfileName = ooXmlFile.CreateFromCml(contextObject.Cml.ToString(), guidString, options);

                        DateTime started2 = DateTime.Now;
                        control.Range.InsertFile(tempfileName, bookmarkName);
                        TimeSpan ts2 = DateTime.Now - started2;
                        //WriteTelemetry(module, "Information", "Range.InsertFile took " + ts2.TotalMilliseconds.ToString("#,##0.0") + "ms");

                        if (wordApp.ActiveDocument.Bookmarks.Exists(bookmarkName))
                        {
                            wordApp.ActiveDocument.Bookmarks[bookmarkName].Delete();
                        }
                        File.Delete(tempfileName);

                        TimeSpan ts = DateTime.Now - started;
                        WriteTelemetry(module, "Information", "Rendering OOXML took " + ts.TotalMilliseconds.ToString("#,##0.0") + "ms");
                    }
                    catch (Exception e)
                    {
                        _telemetry.Write(module, "Exception", e.Message);
                        //MessageBox.Show(e.Message + "\n\n" + e.Source + "\n\n" + e.StackTrace,
                        //                Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                        //                MessageBoxImage.Stop);
                        new ErrorReport(_telemetry, WordTopLeft, module, e.Message).ShowDialog();
                    }
                }
                else
                {
                    // Existing code goes here

                    control = wordApp.ActiveDocument.ContentControls.Add(
                        WdContentControlType.wdContentControlPicture, ref missing);
                    control.SetPlaceholderText(Text: " ");

                    try
                    {
                        CanvasContainer container = new CanvasContainer(contextObject,
                                                                        new CmlMolecule((XElement)
                                                                            documentDepictionOption.MachineUnderstandableOption).CloneMolecule(1.54, false));
                        container.GeneratePng(true);
                        control.Range.InlineShapes.AddPicture(container.PngFileOutput, ref missing, ref missing, ref missing);
                        // Delete the png file
                        File.Delete(container.PngFileOutput);
                        container = null;
                    }
                    catch (Exception e)
                    {
                        _telemetry.Write(module, "Exception", e.Message);
                        //MessageBox.Show(e.Message + "\n\n" + e.Source + "\n\n" + e.StackTrace,
                        //                Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                        //                MessageBoxImage.Stop);
                        new ErrorReport(_telemetry, WordTopLeft, module, e.Message).ShowDialog();
                    }
                }
            }
            else
            {
                control = wordApp.ActiveDocument.ContentControls.Add(
                    WdContentControlType.wdContentControlRichText, ref missing);
                control.SetPlaceholderText(Text: " ");
                CreateOneDZoneContent(ref control, documentDepictionOption);
            }
            control.Title = Properties.Resources.ChemistryZoneAlias;

            // Invoke the ContentControlAfterAdd event to Active Document
            IChemistryZone addedChemZone = ActiveChemistryDocument.DocumentContentControlAfterAdd(control,
                                                                                                  contextObject.Cml,
                                                                                                  chemistryZoneProperties);
            wordApp.Selection.SetRange(addedChemZone.ContentControl.Range.End + 1,
                                       addedChemZone.ContentControl.Range.End + 1);
            addedChemZone.Lock();

            ActiveChemistryDocument.EventTurnOn = false;

            return addedChemZone;
        }

        /// <summary>
        ///   Creates an inline control within the Word document with the content from the CML file
        /// </summary>
        /// <param name = "fileName">CML file</param>
        public void CreateInlineControl(string fileName)
        {
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
            m.Start(fileName);

            if (m.Worked())
            {
                CmlMolecule cmlMolecule = CmlUtils.GetFirstDescendentMolecule(m.GetContextObject().Cml.Root);

                DocumentHostControl control = new DocumentHostControl();
                control.InnerCanvasContainer.SetData(m.GetContextObject(), cmlMolecule);

                object missing = Type.Missing;
                CustomXMLPart part =
                    wordApp.ActiveDocument.CustomXMLParts.Add(m.GetContextObject().Cml.ToString(), missing);
                control.CustomXMLId = part.Id;
                control.CustomXMLId = part.Id;

                object horizontal = false;
                object vertical = true;
                Microsoft.Office.Tools.Word.Document doc =
                    Microsoft.Office.Tools.Word.Document.GetVstoObject(wordApp.ActiveWindow.Document);
                doc.Controls.AddControl(control, wordApp.Selection.Range,
                                        wordApp.PixelsToPoints(control.Width, ref horizontal),
                                        wordApp.PixelsToPoints(control.Height, ref vertical),
                                        string.Format(CultureInfo.InvariantCulture, "Chemistry:{0}", Guid.NewGuid()));
            }
        }

        /// <summary>
        ///   Prompts the user to select a depiction of the chemistry.
        /// </summary>
        /// <param name = "contextObject">The contextObject containing the chemistry</param>
        /// <param name = "reason">the reason why the user is being prompted for this information</param>
        /// <returns>The selected depiction option or null</returns>
        public DepictionOption SelectDepictionOption(ContextObject contextObject, string reason)
        {
            return SelectDepictionOption(contextObject, contextObject.Cml.Root, reason);
        }

        /// <summary>
        ///   Prompts the user to select a depiction of the chemistry.
        /// </summary>
        /// <param name = "contextObject">The contextObject containing the chemistry</param>
        /// <param name = "eldestElement">The eldest element to look for depictions under</param>
        /// <param name = "reason">the reason why the user is being prompted for this information</param>
        /// <returns>The selected depiction option or null</returns>
        public DepictionOption SelectDepictionOption(ContextObject contextObject, XElement eldestElement, string reason)
        {
            try
            {
                ManageViewOption manageViewOpt =
                    new ManageViewOption(contextObject, "");
                if (manageViewOpt.ShowDialog() == true)
                {
                    if (manageViewOpt.SelectedDepiction != null)
                    {
                        return manageViewOpt.SelectedDepiction;
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                throw new NumboException("Chemistry Object was broken:\n Cannot find custom CML.", e);
            }
            catch (Exception e)
            {
                throw new NumboException("Error Switching Depictions:\n" + e.Message, e);
            }
            return null;
        }

        /// <summary>
        ///   Change depiction option of the selected Chemistry Zone.
        /// </summary>
        public void SwitchViewOptions()
        {
            try
            {
                var selectedZone = ActiveChemistryDocument.SelectedChemistryZone;
                var contextObject = selectedZone.AsContextObject();
                var newDocumentDepictionOption = SelectDepictionOption(contextObject,
                                                                       "Please select how you would like to display this chemistry in the document");
                if (newDocumentDepictionOption != null)
                {
                    ChangeExistingDocumentDepiction(selectedZone,
                                                    newDocumentDepictionOption, true);
                }
            }
            catch (ArgumentNullException e)
            {
                throw new NumboException("Chemistry Object was broken:\n Cannot find custom CML.", e);
            }
            catch (Exception e)
            {
                throw new NumboException("Error Switching Depictions:\n" + e.StackTrace + "\n" + e.Message, e);
            }
        }

        /// <summary>
        ///   Tweak 2D by showing ChemDoodle Editor, so that user can alter Chemistry diagram.
        ///   This function could be called after user selected one Chemistry Zone.
        ///   This function will go away once the chemistry canvas are inlined
        /// </summary>
        public void TweakDoodle2D(IChemistryZone selectedZone, bool newStructure)
        {
            string module = "CoreClass.TweakDoodle2d()";
            try
            {
                _telemetry.Write(module, "Information", "NewStructure: " + newStructure);

                #region Fire up ChemDoodle editor

                ChemDoodleEditorForm tcd = new ChemDoodleEditorForm();

                tcd.UserOptions = new OoXmlOptions();
                tcd.UserOptions.ShowHydrogens = Setting.RenderImplicitHydrogens;
                tcd.UserOptions.ColouredAtoms = Setting.RenderAtomsInColour;
                tcd.TopLeft = WordTopLeft;

                tcd.Telemetry = _telemetry;

                #region Convert cml to JSON

                Log.Debug("Converting CML to JSON");
                tcd.Before_CML = selectedZone.Cml.ToString();
                string normal = Chem4Word.UI.Converters.Cml.ToJson(selectedZone.Cml.ToString());
                if (WordVersion > 2007)
                {
                    tcd.Before_JSON = normal;
                }
                else
                {
                    tcd.Before_JSON = Chem4Word.UI.Converters.Json.InvertY(normal);
                }

                #endregion Convert cml to JSON

                Log.Debug("Opening Editor");
                System.Windows.Forms.DialogResult chemEditorResult = tcd.ShowDialog();

                #endregion Fire up ChemDoodle editor

                if (chemEditorResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (tcd.UserOptions.Changed)
                    {
                        Setting.RenderAtomsInColour = tcd.UserOptions.ColouredAtoms;
                        Setting.RenderImplicitHydrogens = tcd.UserOptions.ShowHydrogens;
                        Setting.SaveSettings(localAppDataFolder + @"\User Setting.xml");
                    }

                    #region Convert JSON to cml

                    Log.Debug("Converting JSON to CML");
                    if (WordVersion > 2007)
                    {
                        normal = tcd.After_JSON;
                    }
                    else
                    {
                        normal = Chem4Word.UI.Converters.Json.InvertY(tcd.After_JSON);
                    }
                    tcd.After_CML = Chem4Word.UI.Converters.Json.ToCML(normal);

                    #endregion Convert JSON to cml

                    #region Copy labels to new cml

                    Log.Debug("Copy labels to new CML");
                    XmlDocument docBefore = new XmlDocument();
                    docBefore.LoadXml(selectedZone.Cml.ToString());
                    XmlNamespaceManager nsmgr1 = new XmlNamespaceManager(docBefore.NameTable);
                    nsmgr1.AddNamespace("cml", "http://www.xml-cml.org/schema");

                    XmlDocument docAfter = new XmlDocument();
                    docAfter.LoadXml(tcd.After_CML);
                    XmlNamespaceManager nsmgr2 = new XmlNamespaceManager(docAfter.NameTable);
                    nsmgr2.AddNamespace("cml", "http://www.xml-cml.org/schema");

                    System.Xml.XmlNode beforeMolecule = docBefore.SelectSingleNode("//cml:molecule", nsmgr1);
                    System.Xml.XmlNode afterMolecule = docAfter.SelectSingleNode("//cml:molecule", nsmgr2);

                    XDocument doc = XDocument.Parse(docAfter.InnerXml);
                    string beforeConsiseFormula = tcd.Before_Formula;
                    string afterConciseFormula = tcd.After_Formula;
                    string beforeInchiKey = "";
                    string afterInchiKey = "";
                    string beforeSynonym = "Unknown";

                    // Copy molecule attributes
                    foreach (System.Xml.XmlAttribute att in beforeMolecule.Attributes)
                    {
                        if (!att.Name.Equals(CmlAttribute.ID))
                        {
                            System.Xml.XmlElement newMolecule = afterMolecule as System.Xml.XmlElement;
                            newMolecule.SetAttribute(att.Name, att.Value);
                        }
                    }

                    System.Xml.XmlElement inchiKeyElement = null;
                    System.Xml.XmlElement chemSpiderSynonymElement = null;
                    // Copy other elements
                    foreach (System.Xml.XmlNode node in beforeMolecule)
                    {
                        if (!((node.Name.EndsWith(CmlAtomArray.Tag)) || (node.Name.EndsWith(CmlBondArray.Tag))))
                        {
                            //System.Diagnostics.Debug.WriteLine("Element " + node.Name);
                            if (node.Name.EndsWith(CmlName.Tag)
                                && node.Attributes[0].Name.Equals(CmlAttribute.DictRef)
                                && node.Attributes[0].Value.Equals("nameDict:inchikey"))
                            {
                                inchiKeyElement = docAfter.CreateElement(node.Name, "http://www.xml-cml.org/schema");
                                inchiKeyElement.SetAttribute(CmlAttribute.DictRef, "nameDict:inchikey");
                            }
                            else if (node.Name.EndsWith(CmlName.Tag)
                                && node.Attributes[0].Name.Equals(CmlAttribute.DictRef)
                                && node.Attributes[0].Value.Equals("nameDict:chemspider"))
                            {
                                chemSpiderSynonymElement = docAfter.CreateElement(node.Name, "http://www.xml-cml.org/schema");
                                chemSpiderSynonymElement.SetAttribute(CmlAttribute.DictRef, "nameDict:chemspider");
                                beforeSynonym = node.InnerText;
                            }
                            else
                            {
                                System.Xml.XmlElement e = docAfter.CreateElement(node.Name, "http://www.xml-cml.org/schema");
                                e.InnerText = node.InnerText;
                                foreach (System.Xml.XmlAttribute att in node.Attributes)
                                {
                                    if (node.Name.EndsWith(CmlFormula.Tag) && att.Name.Equals(CmlAttribute.Concise))
                                    {
                                        beforeConsiseFormula = att.Value;
                                        Log.Debug("Before Concise Formula: " + beforeConsiseFormula);
                                        e.SetAttribute(att.Name, afterConciseFormula);
                                        Log.Debug("After Concise Formula: " + afterConciseFormula);
                                    }
                                    else
                                    {
                                        e.SetAttribute(att.Name, att.Value);
                                    }
                                }
                                afterMolecule.AppendChild(e);
                            }
                        }
                    }

                    #endregion Copy labels to new cml

                    ProgressBar pb = new ProgressBar();
                    pb.Value = 0;
                    pb.Maximum = 3;
                    pb.TopLeft = WordTopLeft;

                    #region Get Inchi-Keys from Chem Spider

                    Log.Debug("Get Inchi-Keys from Chem Spider");
                    if (string.IsNullOrEmpty(beforeInchiKey))
                    {
                        pb.Show();
                        pb.Message = "Obtaining InchiKey (before) from ChemSpider";
                        pb.Increment(1);

                        beforeInchiKey = GetInchiKey(tcd.Before_MolFile);
                    }

                    pb.Show();
                    pb.Message = "Obtaining InchiKey (after) from ChemSpider";
                    pb.Increment(1);

                    afterInchiKey = GetInchiKey(tcd.After_MolFile);

                    #endregion Get Inchi-Keys from Chem Spider

                    pb.Show();
                    pb.Message = "Obtaining Synonym from ChemSpider";
                    pb.Increment(1);

                    #region Get Synonym from ChemSpider

                    Log.Debug("Get Synonym from ChemSpider");
                    string afterSynonym = GetSynonymFromChemSpider(afterInchiKey);

                    #endregion Get Synonym from ChemSpider

                    pb.Value = 0;
                    pb.Hide();
                    pb.Close();

                    #region Save New Inchi-Key (if found)

                    if (afterInchiKey != null)
                    {
                        if (inchiKeyElement == null)
                        {
                            inchiKeyElement = docAfter.CreateElement(CmlName.Tag, "http://www.xml-cml.org/schema");
                            inchiKeyElement.SetAttribute(CmlAttribute.DictRef, "nameDict:inchikey");
                        }
                        inchiKeyElement.InnerText = afterInchiKey;
                        afterMolecule.AppendChild(inchiKeyElement);
                    }

                    #endregion Save New Inchi-Key (if found)

                    #region Save New ChemSpider Synonym (if found)

                    if (chemSpiderSynonymElement == null)
                    {
                        chemSpiderSynonymElement = docAfter.CreateElement(CmlName.Tag, "http://www.xml-cml.org/schema");
                        chemSpiderSynonymElement.SetAttribute(CmlAttribute.DictRef, "nameDict:chemspider");
                    }
                    if (!string.IsNullOrEmpty(afterSynonym))
                    {
                        chemSpiderSynonymElement.InnerText = afterSynonym;
                    }
                    else
                    {
                        chemSpiderSynonymElement.InnerText = "Not known to ChemSpider";
                    }
                    afterMolecule.AppendChild(chemSpiderSynonymElement);

                    #endregion Save New ChemSpider Synonym (if found)

                    // Detect if molecule has changed and we need to show Label editor
                    bool showLabelEditor = newStructure;
                    if (!beforeConsiseFormula.Equals(afterConciseFormula))
                    {
                        // Consise formula has changed
                        showLabelEditor = true;
                    }

                    if (beforeInchiKey != null && afterInchiKey != null)
                    {
                        if (!beforeInchiKey.Equals(afterInchiKey))
                        {
                            // Inchi-Key has changed;
                            showLabelEditor = true;
                        }
                    }

                    if (showLabelEditor)
                    {
                        #region Show Label Editor

                        // Generate ContextObject for label editor
                        ContextObject contextObject = new ContextObject(XDocument.Parse(docAfter.InnerXml));

                        // Edit labels
                        ICollection<IChemistryZone> commonBindingZones =
                            GetAllChemistryZonesBindingToTheCmlInThis(selectedZone);
                        Dictionary<DepictionOption, ICollection<IChemistryZone>> documentDepictionOptionsInUse =
                            GetDocumentDepictionOptionsInUse(contextObject, commonBindingZones);
                        Dictionary<DepictionOption, ICollection<IChemistryZone>> navigatorDepictionOptionsInUse =
                            GetNavigatorDepictionOptionsInUse(contextObject, commonBindingZones);

                        ContextObjectAndDialogResultHolder resultHolder = EditLabels(contextObject,
                                                                                     documentDepictionOptionsInUse,
                                                                                     navigatorDepictionOptionsInUse,
                                                                                     true);

                        bool labelEditResult = resultHolder.GetDialogResult();

                        #endregion Show Label Editor

                        #region Save the data with new labels

                        contextObject = resultHolder.GetContextObject();
                        var chemZoneProperties = selectedZone.Properties;
                        chemZoneProperties.ViewBox = contextObject.ViewBoxDimensions;
                        selectedZone.Properties = chemZoneProperties;

                        if (labelEditResult)
                        {
                            selectedZone.Cml = contextObject.Cml;
                        }
                        else
                        {
                            // No Label changes, simply overwrite Cml which causes refresh of chemistry zones
                            selectedZone.Cml = XDocument.Parse(docAfter.InnerXml);
                        }

                        #endregion Save the data with new labels
                    }
                    else
                    {
                        // No Label changes, simply overwrite Cml which causes refresh of chemistry zones
                        selectedZone.Cml = XDocument.Parse(docAfter.InnerXml);
                    }

                    XmlNode xmlNode = docAfter.SelectSingleNode("//cml:molecule", nsmgr1);
                    XElement xElement = XElement.Parse(xmlNode.OuterXml);
                    CmlMolecule mol = new CmlMolecule(xElement);
                    if (mol.GetAllBonds().Count > 0)
                    {
                        _telemetry.Write(module, "Information", "Bond Lengths; Median: "
                                                                +
                                                                SafeDouble.AsString(mol.GetMedianBondLength(), "#0.000")
                                                                + " Average: " +
                                                                SafeDouble.AsString(mol.GetAverageBondLength(), "#0.000"));
                    }
                }
                else
                {
                    // User Cancelled edit
                    if (newStructure)
                    {
                        // ToDo - MAW - How to delete the unedited structure
                        selectedZone.ContentControl.Delete(true);
                    }
                }

                tcd.Close();
            }
            catch (Exception ex)
            {
                Log.Error("Error in TweakDoodle2D", ex);
                _telemetry.Write(module, "Exception", ex.Message);
                new ErrorReport(_telemetry, WordTopLeft, module, ex.Message).ShowDialog();
            }
            finally
            {
                // Sometimes the the open state of the file is not update quickly enough,
                // So that we need to invoke GC to refresh the environment states.
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public void WriteTelemetry(string operation, string level, string message)
        {
            _telemetry.Write(operation, level, message);
        }

        private string GetSynonymFromChemSpider(string afterInchiKey)
        {
            string module = "CoreClass.GetSynonymFromChemSpider()";
            string result = null;
            DateTime started = DateTime.Now;

            if (!string.IsNullOrEmpty(afterInchiKey))
            {
                try
                {
                    Log.Debug("Getting Chemspider RDF Page");
                    _telemetry.Write(module, "Information", "Calling WebService");
                    string url = Properties.Resources.ChemSpiderRdfServiceUri + afterInchiKey;
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    request.Timeout = 5000;
                    request.UserAgent = "Chem4Word";
                    HttpWebResponse response;

                    response = (HttpWebResponse)request.GetResponse();
                    if (HttpStatusCode.OK.Equals(response.StatusCode))
                    {
                        result = "Unknown";
                        using (Stream resStream = response.GetResponseStream())
                        {
                            XmlTextReader reader = new XmlTextReader(resStream);
                            bool inSynonym = false;
                            while (reader.Read())
                            {
                                //System.Diagnostics.Debug.WriteLine(reader.Name);
                                if (reader.Name.Equals("chemdomain:Synonym"))
                                {
                                    inSynonym = !inSynonym;
                                }
                                if (inSynonym)
                                {
                                    if (reader.Name.Equals("chemdomain:hasValue"))
                                    {
                                        result = reader.ReadInnerXml();
                                        Debug.WriteLine("Found Synonym: " + result);
                                        Log.Debug("Found Synonym: " + result);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        _telemetry.Write(module, "Error", "Http Status: " + response.StatusCode);
                        Log.Debug("Chemspider RDF Page - Error - Status code: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Chemspider RDF Page - Exception - " + ex.Message);
                    if (ex.Message.Contains("404"))
                    {
                        result = "Not Found";
                    }
                    else
                    {
                        _telemetry.Write(module, "Exception", ex.Message);
                    }
                }
            }

            TimeSpan ts = DateTime.Now - started;
            _telemetry.Write(module, "Information", "Duration " + ts.TotalMilliseconds.ToString("##,###.0") + "ms");

            return result;
        }

        private string GetInchiKey(string molfile)
        {
            string module = "CoreClass.GetInchiKey()";
            string result = null;
            DateTime started = DateTime.Now;

            try
            {
                Log.Debug("Calling ChemSpider WebService");
                _telemetry.Write(module, "Information", "Calling WebService");
                com.chemspider.www.InChI i = new com.chemspider.www.InChI();
                i.Url = Properties.Resources.ChemSpiderWebServiceUri + "InChI.asmx";
                i.UserAgent = "Chem4Word";
                i.Timeout = 5000;
                result = i.MolToInChIKey(molfile);
                Debug.WriteLine("ChemSpider Result: " + result);
                Log.Debug("ChemSpider Result: " + result);
            }
            catch (Exception ex)
            {
                _telemetry.Write(module, "Exception", ex.Message);
                if (!ex.Message.ToLower().Contains("timed out"))
                {
                    _telemetry.Write(module, "Exception(Data)", molfile);
                }
                Log.Error("GetInchiKey() - Exception - " + ex.Message);
            }

            TimeSpan ts = DateTime.Now - started;
            _telemetry.Write(module, "Information", "Duration " + ts.TotalMilliseconds.ToString("##,###.0") + "ms");

            return result;
        }

        /// <summary>
        ///   Tweak 2D by showing 2D Editor, so that user can alter Chemistry diagram.
        ///   This function could be called after user selected one Chemistry Zone.
        ///   This function will go away once the chemistry canvas are inlined
        /// </summary>
        public void Tweak2D(IChemistryZone selectedZone)
        {
            var co = selectedZone.AsContextObject();
            var originalContextObject = co.Clone();
            try
            {
                DepictionOption depictionOption = DepictionOption.CreateDepictionOption(co.Cml,
                                                                                        selectedZone.Properties.
                                                                                        DocumentDepictionOptionXPath);
                DepictionOption associatedTwoDDepictionOption = depictionOption.GetAssociatedTwoDDepictionOption();
                var dialog2DEditing = new NewEditorWindow(selectedZone, co,
                                                                      associatedTwoDDepictionOption.
                                                                          MachineUnderstandableOption);
                string xpathToTwoDDepictionOption = associatedTwoDDepictionOption.AbsoluteXPathToDepictionOption;
                associatedTwoDDepictionOption = null;
                depictionOption = null;
                bool? dialogResult = dialog2DEditing.ShowDialog();

                if (dialogResult == true)
                {
                    var continueProcessing = true;
                    // get the updated contextObject from the 2d editor
                    // var contextObject = new ContextObject(dialog2DEditing.CmlOutput);
                    var contextObject = dialog2DEditing.ContextObject;

                    associatedTwoDDepictionOption = DepictionOption.CreateDepictionOption(contextObject.Cml,
                                                                                          xpathToTwoDDepictionOption);
                    var outdated =
                        (bool)
                        ((XElement)associatedTwoDDepictionOption.MachineUnderstandableOption).XPathEvaluate(
                            "count(.//@*[local-name()='" + CmlConstants.Outdated + "' and namespace-uri()='" +
                            CmlConstants.CmlxNS + "']) > 0");

                    if (outdated)
                    {
                        ICollection<IChemistryZone> commonBindingZones =
                            GetAllChemistryZonesBindingToTheCmlInThis(selectedZone);
                        Dictionary<DepictionOption, ICollection<IChemistryZone>> documentDepictionOptionsInUse =
                            GetDocumentDepictionOptionsInUse(contextObject, commonBindingZones);
                        Dictionary<DepictionOption, ICollection<IChemistryZone>> navigatorDepictionOptionsInUse =
                            GetNavigatorDepictionOptionsInUse(contextObject, commonBindingZones);

                        ContextObjectAndDialogResultHolder resultHolder = EditLabels(contextObject,
                                                                                     documentDepictionOptionsInUse,
                                                                                     navigatorDepictionOptionsInUse,
                                                                                     false);

                        if (resultHolder.GetDialogResult())
                        {
                            contextObject = resultHolder.GetContextObject();
                        }
                        else
                        {
                            continueProcessing = false;
                            contextObject = originalContextObject;
                        }
                    }

                    if (continueProcessing)
                    {
                        var chemZoneProperties = selectedZone.Properties;
                        chemZoneProperties.ViewBox = contextObject.ViewBoxDimensions;
                        selectedZone.Properties = chemZoneProperties;
                        selectedZone.Cml = contextObject.Cml;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NumboException("Error in Tweak2D:\n" + ex.Message, ex);
            }
            finally
            {
                // Sometimes the the open state of the file is not update quickly enough,
                // So that we need to invoke GC to refresh the environment states.
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        ///   Save selected Chemistry Zone into Gallery dictionary.
        ///   This function could be called after user selected one Chemistry Zone.
        /// </summary>
        /// <param name = "galleryName">Name of the gallery instance.</param>
        /// <exception cref = "Exception">If there is not exactly one chemistry zone selected</exception>
        public void SaveSelectionIntoGallery(string galleryName)
        {
            string module = "CoreClass.SaveSelectionIntoGallery()";
            _telemetry.Write(module, "Information", "'" + galleryName + "' saved into gallery");
            Microsoft.Office.Tools.Word.Document wordDoc =
                Microsoft.Office.Tools.Word.Document.GetVstoObject(wordApp.ActiveWindow.Document);
            if (wordApp.ActiveWindow.Panes.Count > 0)
            {
                Pane pane = wordApp.ActiveWindow.Panes[1];

                // For this mile stone we support user to to save only one Chemistry Zone each time.
                int selectedCcCount = pane.Selection.ContentControls.Count;
                if (selectedCcCount == 1)
                {
                    string cmlString = ActiveChemistryDocument.SelectedChemistryZone.Cml.ToString();
                    string tag = Guid.NewGuid().ToString();
                    // store cml string into CML File Manager
                    galleryDictionaryManager.Add(tag, cmlString);
                    ActiveChemistryDocument.SelectedChemistryZone.ContentControl.Tag = tag;

                    // prepare to store selection into gallery
                    Template template = wordDoc.AttachedTemplate as Template;
                    if (template != null)
                    {
                        object description = string.Empty;

                        // store selection into gallery
                        template.BuildingBlockEntries.Add(galleryName,
                                                          WdBuildingBlockTypes.wdTypeCustom5,
                                                          Properties.Resources.ChemistryZoneAlias,
                                                          pane.Selection.Range, ref description,
                                                          WdDocPartInsertOptions.wdInsertContent
                            );

                        // Update the Chem4Word.dotx template file silently
                        if (!template.Saved)
                        {
                            template.Save();
                        }
                    }

                    // Update Local SmartTag
                    if (!smartTag.IsTermExists(galleryName))
                    {
                        smartTag.AddTerm(galleryName, cmlString);
                    }
                    // galleryName is already in SmartTag dictionary
                    else
                    {
                        MessageBox.Show(
                            string.Format(CultureInfo.InvariantCulture, "Warning: \"{0}\" was already in the SmartTag Dictionary.",
                                          galleryName),
                            Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
                // TOLA NOTE is this possible - surely the save to gallery option is not
                // active if a ChemZone has not been selected?
                else if (selectedCcCount == 0)
                {
                    // Alert message to user when there is non Chemistry Zone were selected.
                    throw new NumboException("You have not correctly selected the Chemistry Zone.");
                }
                else
                {
                    // Alert message to user when there are more than one Chemistry Zone were selected.
                    throw new NumboException("Sorry you can save only one Chemistry Zone at a time.");
                }
            }
        }

        /// <summary>
        ///   Invoke  user setting.
        /// </summary>
        public void InvokeUserSetting()
        {
            UserSettingOption userSettingOption =
                new UserSettingOption(localAppDataFolder + @"\User Setting.xml");
            bool? dialogResult = userSettingOption.ShowDialog();
        }

        /// <summary>
        /// </summary>
        /// <param name = "contextObject"></param>
        /// <param name = "documentDepictionOptionsInUse"></param>
        /// <param name = "navigatorDepictionOptionsInUse"></param>
        /// <param name = "showEvaluate"></param>
        /// <returns></returns>
        private static ContextObjectAndDialogResultHolder EditLabels(ContextObject contextObject,
                                                                     Dictionary
                                                                         <DepictionOption, ICollection<IChemistryZone>>
                                                                         documentDepictionOptionsInUse,
                                                                     Dictionary
                                                                         <DepictionOption, ICollection<IChemistryZone>>
                                                                         navigatorDepictionOptionsInUse,
                                                                    bool showEvaluate)
        {
            var returnContextObject = contextObject.Clone();
            var result = false;
            if (contextObject == null)
            {
                throw new NumboException("contextObject is null");
            }
            if (documentDepictionOptionsInUse == null)
            {
                throw new NumboException("documentDepictionOptionsInUse is null");
            }
            if (documentDepictionOptionsInUse.Count == 0)
            {
                throw new NumboException("documentDepictionOptionsInUse is empty");
            }
            if (navigatorDepictionOptionsInUse == null)
            {
                throw new NumboException("navigatorDepictionOptionsInUse is null");
            }
            if (navigatorDepictionOptionsInUse.Count == 0)
            {
                throw new NumboException("navigatorDepictionOptionsInUse is empty");
            }
            try
            {
                EditLabels editLabels = new EditLabels(contextObject,
                                            documentDepictionOptionsInUse,
                                            navigatorDepictionOptionsInUse,
                                            showEvaluate);
                if (editLabels.ShowDialog() == true)
                {
                    returnContextObject = editLabels.ContextObject;
                    result = true;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new NumboException("Chemistry Object was broken:\nCannot find custom CML.", e);
            }
            catch (Exception e)
            {
                throw new NumboException("Error Editing Label:\n" + e.Message, e);
            }
            return new ContextObjectAndDialogResultHolder(returnContextObject, result);
        }

        private ICollection<IChemistryZone> GetAllChemistryZonesBindingToTheCmlInThis(IChemistryZone chemistryZone)
        {
            ICollection<IChemistryZone> commonBindingZones =
                ActiveChemistryDocument.GetOtherCommonBindingZones(chemistryZone);
            commonBindingZones.Add(chemistryZone);
            return commonBindingZones;
        }

        /// <summary>
        ///   Edit label of the specified Chemistry Zone
        /// </summary>
        /// <param name = "chemistryZone">The chemistry zone to edit label for</param>
        /// <exception cref = "NumboException">If there is a problem with the chemistry zone</exception>
        public void EditLabels(IChemistryZone chemistryZone)
        {
            ICollection<IChemistryZone> commonBindingZones = GetAllChemistryZonesBindingToTheCmlInThis(chemistryZone);
            ContextObject contextObject = chemistryZone.AsContextObject();
            Dictionary<DepictionOption, ICollection<IChemistryZone>> documentDepictionOptionsInUse =
                GetDocumentDepictionOptionsInUse(contextObject, commonBindingZones);
            Dictionary<DepictionOption, ICollection<IChemistryZone>> navigatorDepictionOptionsInUse =
                GetNavigatorDepictionOptionsInUse(contextObject, commonBindingZones);

            ContextObjectAndDialogResultHolder resultHolder = EditLabels(contextObject,
                                                                         documentDepictionOptionsInUse,
                                                                         navigatorDepictionOptionsInUse,
                                                                         false);
            if (resultHolder.GetDialogResult())
            {
                chemistryZone.Cml = resultHolder.GetContextObject().Cml;
            }
        }

        ///<summary>
        ///  Show a dialog window (modal) which contains the CML of the current zone
        ///</summary>
        ///<exception cref = "NumboException">if the chemistry object was broken</exception>
        ///<exception cref = "Exception"></exception>
        public void ViewCml(IChemistryZone chemistryZone)
        {
            new Viewer(chemistryZone.Cml).ShowDialog();
        }

        /// <summary>
        ///   Create chemistry from the currently selected text in the document.
        /// </summary>

        public void AddInlineChemText()
        {
            ChemistryZoneMatch selectedMatch = null;
            _chemistryZoneNames = new List<string>();
            _zoneMatches = new List<ChemistryZoneMatch>();

            Range range = wordApp.ActiveDocument.ActiveWindow.Selection.Range;
            string latex = TextTools.ConvertWordMarkupToLatexStyle(range);

            //Load dictionary
            _termDictionary = new TermDictionaryManager(assemblyDirectoryName + @"\SmartTag");
            _termDictionary.LoadLocalDictionary(localAppDataFolder + @"\SmartTag");

            Recognize(latex, 0, 0, true);
            selectedMatch = _zoneMatches.Find(match => match.Text == latex);
            if (string.IsNullOrEmpty(Convert.ToString(selectedMatch)))
            {
                ContextObject contextObject = Cid.CreateChemistryFromUnknownOrigin(latex);
                DepictionOption onlyDepictionOption = Depiction.PossibleDepictionOptions(contextObject).FirstOrDefault();
                ChemistryZoneProperties chemistryZoneProperties = new ChemistryZoneProperties(onlyDepictionOption,
                                                                                              onlyDepictionOption, true);
                //AddNewContextObjectToDocument(range, contextObject, onlyDepictionOption,
                //                              onlyDepictionOption);
                AddNewContextObjectToDocument(range, contextObject, chemistryZoneProperties);
            }
            else
            {
                MarkAsRecognizedChemistryZone(latex, selectedMatch, range);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="selectedText"></param>
        /// <param name="selectionRange"></param>

        private void MarkAsRecognizedChemistryZone(string id, ChemistryZoneMatch selectedText,
                                                         Range selectionRange)
        {
            XDocument cml = XDocument.Parse(_termDictionary.GetValue(selectedText.MoleculeId));
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
                        string.Compare(option.GetAsLatexFormattedString(), selectedText.Text,
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

                DepictionOption navigatorDepictionOption =
                    CoreClass.GetPreferedNavigatorDepiction(m.GetContextObject(),
                                                            Setting.NavigatorPreferedDepiction);

                //_core.AddNewContextObjectToDocument(selectionRange, m.GetContextObject(), documentDepictionOption,
                //                                    navigatorDepictionOption);

                var chemistryZoneProperties = new ChemistryZoneProperties(documentDepictionOption,
                                                                                 navigatorDepictionOption, true);
                //_core.AddNewContextObjectToDocument(selectionRange, m.GetContextObject(), documentDepictionOption,
                //                                    navigatorDepictionOption);
                AddNewContextObjectToDocument(selectionRange, m.GetContextObject(), chemistryZoneProperties);
            }
        }

        private static void AddToChemistryZoneList(string selectionText, string moleculeId, int startIndex, int endIndex)
        {
            ChemistryZoneMatch zoneMatch = new ChemistryZoneMatch
            {
                Text = selectionText,
                MoleculeId = (moleculeId == null) ? String.Empty : moleculeId,
                ContentId = Guid.NewGuid().ToString(),
                StartIndex = startIndex,
                EndIndex = endIndex
            };

            // to avoid duplicates entry
            if (!_chemistryZoneNames.Contains(selectionText.Trim()))
            {
                _zoneMatches.Add(zoneMatch);
                _chemistryZoneNames.Add(selectionText);
            }
        }

        private void Recognize(string paragraphText, int selectionRangeStart, int cursorPosition,
                                    bool isSelection)
        {
            string module = "CoreClass.Recognize()";
            paragraphText = paragraphText.Trim();

            if (!String.IsNullOrEmpty(paragraphText))
            {
                try
                {
                    foreach (Term term in _termDictionary)
                    {
                        Regex r = new Regex("(?i)\\b" + Regex.Escape(term.Value) + "\\b");
                        Match m = r.Match(paragraphText);
                        while (m.Success)
                        {
                            int startIndex = selectionRangeStart + m.Index;
                            int endIndex = selectionRangeStart + m.Index + m.Length;

                            //SetIndex(m, ref startIndex, ref endIndex);

                            if ((startIndex <= cursorPosition) && (endIndex >= cursorPosition) || isSelection)
                            {
                                AddToChemistryZoneList(m.Value, term.MoleculeId, startIndex, endIndex);
                            }

                            m = m.NextMatch();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _telemetry.Write(module, "Exception", ex.Message);
                    //MessageBox.Show(ex.Message,
                    //                UI.Properties.Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                    //                MessageBoxImage.Error);
                    new ErrorReport(_telemetry, WordTopLeft, module, ex.Message).ShowDialog();
                }
            }
        }

        /// <summary>
        ///   Gets the DepictionOption for the document or propmts the user for the information
        /// </summary>
        /// <param name = "contextObject"></param>
        /// <param name = "docPreferedDepiction">the type of depiction option to return</param>
        /// <returns></returns>
        internal static DepictionOption GetPreferedDocumentDepiction(ContextObject contextObject,
                                                                     DocPreferedDepiction
                                                                         docPreferedDepiction)
        {
            IEnumerable<DepictionOption> depictionOptions = Depiction.PossibleDepictionOptions(contextObject);
            DepictionOption documentDepictionOption = null;
            switch (docPreferedDepiction)
            {
                case DocPreferedDepiction.TwoD:
                    documentDepictionOption = depictionOptions.FirstOrDefault(Depiction.Is2D);
                    break;

                case DocPreferedDepiction.BoldNumber:
                    documentDepictionOption = depictionOptions.FirstOrDefault(Depiction.IsMoleculeBoldNumberLabel);
                    break;

                case DocPreferedDepiction.ChemicalName:
                    documentDepictionOption = depictionOptions.FirstOrDefault(Depiction.IsName);
                    break;

                case DocPreferedDepiction.ConciseFormula:
                    documentDepictionOption = depictionOptions.FirstOrDefault(Depiction.IsConciseFormula);
                    break;

                case DocPreferedDepiction.InlineFormula:
                    documentDepictionOption = depictionOptions.FirstOrDefault(Depiction.IsInlineFormula);
                    break;
            }
            if (documentDepictionOption == null)
            {
                // TODO JAT
                // NOTE temp - just chose something
                // we should actually be throwing up UI to ask the user
                documentDepictionOption = depictionOptions.FirstOrDefault();
            }
            return documentDepictionOption;
        }

        internal static DepictionOption GetPreferedNavigatorDepiction(ContextObject contextObject,
                                                                      NavPreferedDepiction
                                                                          navPreferedDepiction)
        {
            IEnumerable<DepictionOption> depictionOptions = Depiction.PossibleDepictionOptions(contextObject);
            DepictionOption navigatorDepictionOption = null;
            switch (navPreferedDepiction)
            {
                case NavPreferedDepiction.BoldNumber:
                    navigatorDepictionOption = depictionOptions.FirstOrDefault(Depiction.IsMoleculeBoldNumberLabel);
                    break;

                case NavPreferedDepiction.ChemicalName:
                    navigatorDepictionOption = depictionOptions.FirstOrDefault(Depiction.IsName);
                    break;

                case NavPreferedDepiction.ConciseFormula:
                    navigatorDepictionOption = depictionOptions.FirstOrDefault(Depiction.IsConciseFormula);
                    break;

                case NavPreferedDepiction.InlineFormula:
                    navigatorDepictionOption = depictionOptions.FirstOrDefault(Depiction.IsInlineFormula);
                    break;
            }
            if (navigatorDepictionOption == null)
            {
                // TODO JAT
                // we should actually be throwing up UI to ask the user
                navigatorDepictionOption =
                    Depiction.PossibleDepictionOptions(contextObject).SkipWhile(Depiction.Is2D).FirstOrDefault();
            }
            return navigatorDepictionOption;
        }

        private static Dictionary<DepictionOption, ICollection<IChemistryZone>> GetDocumentDepictionOptionsInUse(
            ContextObject contextObject,
            IEnumerable<IChemistryZone>
                chemistryZonesPointingToSameCml)
        {
            // it is vital that all the depiction options are build usign the same CML XDocument
            // and this is also the same one passed to the edit labels dialog because the
            // copmarison of the depictionOptions requires that they have the ReferenceEqual
            // source xdocument
            var dictionary = new Dictionary<DepictionOption, ICollection<IChemistryZone>>();
            foreach (var zone in chemistryZonesPointingToSameCml)
            {
                var depictionOption = DepictionOption.CreateDepictionOption(contextObject.Cml,
                                                                            zone.Properties.
                                                                                DocumentDepictionOptionXPath);
                ICollection<IChemistryZone> chemzones;
                if (dictionary.TryGetValue(depictionOption, out chemzones))
                {
                    chemzones.Add(zone);
                }
                else
                {
                    chemzones = new List<IChemistryZone> { zone };
                    dictionary.Add(depictionOption, chemzones);
                }
            }
            return dictionary;
        }

        private static Dictionary<DepictionOption, ICollection<IChemistryZone>> GetNavigatorDepictionOptionsInUse(
            ContextObject contextObject,
            IEnumerable<IChemistryZone>
                chemistryZonesPointingToSameCml)
        {
            // it is vital that all the depiction options are build usign the same CML XDocument
            // and this is also the same one passed to the edit labels dialog because the
            // copmarison of the depictionOptions requires that they have the ReferenceEqual
            // source xdocument
            var dictionary = new Dictionary<DepictionOption, ICollection<IChemistryZone>>();
            foreach (var zone in chemistryZonesPointingToSameCml)
            {
                var depictionOption = DepictionOption.CreateDepictionOption(contextObject.Cml,
                                                                            zone.Properties.
                                                                                NavigatorDepictionOptionXPath);
                ICollection<IChemistryZone> chemzones;
                if (dictionary.TryGetValue(depictionOption, out chemzones))
                {
                    chemzones.Add(zone);
                }
                else
                {
                    chemzones = new List<IChemistryZone> { zone };
                    dictionary.Add(depictionOption, chemzones);
                }
            }
            return dictionary;
        }

        ///<summary>
        ///  Change the existing document representation to the specified new one and update the
        ///  backing data to reflect this change. After the change the zone is selected
        ///</summary>
        ///<param name = "chemistryZone">The chemistry zone currently displayed</param>
        ///<param name = "newDocumentDepictionOption">The new depiction to use</param>
        ///<param name = "selectZoneAfterModification">If true the zone is selected after modification, if false moves the cursor to the end of the zone</param>
        public void ChangeExistingDocumentDepiction(IChemistryZone chemistryZone,
                                                    DepictionOption newDocumentDepictionOption,
                                                    bool selectZoneAfterModification)
        {
            var navigatorDepictionOption = DepictionOption.CreateDepictionOption(chemistryZone.Cml,
                                                                                 chemistryZone.Properties.
                                                                                     NavigatorDepictionOptionXPath);

            //var newChemistryZone = ActiveChemistryDocument.RebindDocumentContentControl(chemistryZone,
            //                                                                                       new ChemistryZoneProperties
            //                                                                                           (newDocumentDepictionOption,
            //                                                                                            navigatorDepictionOption));

            ChemistryZoneProperties newChemistryZoneProperties = chemistryZone.Properties.Clone();
            newChemistryZoneProperties.SetDocumentDepictionOption(newDocumentDepictionOption);
            var newChemistryZone = ActiveChemistryDocument.RebindDocumentContentControl(chemistryZone,
                                                                                        newChemistryZoneProperties);

            var contentControl = newChemistryZone.ContentControl;
            if (selectZoneAfterModification)
            {
                // Select the Chemistry Zone.
                // wordApp.Selection.SetRange(contentControl.Range.Start, contentControl.Range.End);
                newChemistryZone.Choose();
            }
            else
            {
                wordApp.Selection.SetRange(contentControl.Range.Start + 1, contentControl.Range.End + 1);
            }
        }

        /// <summary>
        ///   Handle event when Word document is opened.
        /// </summary>
        /// <param name = "doc">Document object of the opened document. <see cref = "Microsoft.Office.Interop.Word.Document" /></param>
        private void WordAppDocumentOpen(Document doc)
        {
            try
            {
                // handle inline controls
                // Load the persisted state of dynamic controls.
                ControlProperties[] savedControls = ControlsStorage.Load(doc);

                // JAT TODO - we should have serialised the depiction options to the
                // context object so we can deserialize them - and use them to reinstantiate
                // the controls etc. also we should not need to reimport anything?
                if (savedControls != null && savedControls.Length > 0)
                {
                    Microsoft.Office.Tools.Word.Document vstoDoc = doc.GetVstoObject();

                    // Recreate controls according to persisted state.
                    foreach (ControlProperties controlInfo in savedControls)
                    {
                        // We create Windows Forms controls by instantiating their type.
                        // For host controls (which do not have a default constructor), we call
                        // specific methods, e.g. AddBookmark().
                        if (ControlProperties.ChemistryControlType.DocumentHostControl.Equals(controlInfo.ControlType))
                        {
                            DocumentHostControl control = new DocumentHostControl();

                            CustomXMLPart part =
                                wordApp.ActiveDocument.CustomXMLParts.SelectByID(controlInfo.CustomXmlId);

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
                            m.Start(XDocument.Parse(part.DocumentElement.XML));

                            if (m.Worked())
                            {
                                XElement molecule =
                                    m.GetContextObject().Cml.Root.Element(
                                        CmlConstants.CmlxNamespace + CmlMolecule.Tag);
                                CmlMolecule cmlMolecule = new CmlMolecule(molecule);
                                control.InnerCanvasContainer.SetData(m.GetContextObject(), cmlMolecule);

                                // Restore the name.
                                control.Name = controlInfo.ControlName;

                                object horizontal = false;
                                object vertical = true;
                                float width = wordApp.PixelsToPoints(controlInfo.ControlWidth, ref horizontal);
                                float height = wordApp.PixelsToPoints(controlInfo.ControlHeight, ref vertical);
                                if (controlInfo.IsInline)
                                {
                                    // Add inline control
                                    object start = (int)controlInfo.ControlX;
                                    // We use a zero-length range for positioning.
                                    // The control's width is determined by the width parameter of AddControl.
                                    object end = (int)controlInfo.ControlX;

                                    Range r = vstoDoc.Range(ref start, ref end);
                                    vstoDoc.Controls.AddControl(
                                        control,
                                        r,
                                        width,
                                        height,
                                        controlInfo.ControlName);
                                }
                                else
                                {
                                    // Add floating control
                                    vstoDoc.Controls.AddControl(
                                        control,
                                        controlInfo.ControlX,
                                        controlInfo.ControlY,
                                        width,
                                        height,
                                        controlInfo.ControlName);
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                ActiveChemistryDocument = new ChemistryDocument(wordApp.ActiveDocument, this);
                documentDictionary.Add(wordApp.ActiveDocument, ActiveChemistryDocument);

                //Register Events
                wordApp.ActiveDocument.ContentControlAfterAdd += ActiveDocumentContentControlAfterAdd;
                wordApp.ActiveDocument.BuildingBlockInsert += ActiveDocumentBuildingBlockInsert;
            }
        }

        private void ActiveDocumentBuildingBlockInsert(Range wordRange, string name, string category, string blockType, string template)
        {
            //category "chemistry"
            //blockType "Custom 5"

            string module = "CoreClass.ActiveDocumentBuildingBlockInsert()";

            if (category.ToLower().Equals("chemistry") && blockType.ToLower().Equals("custom 5"))
            {
                // Only handle our stuff
                _telemetry.Write(module, "Information", "Inserted '" + name + "' from gallery");

                // Start timer to delete flagged Content Control(s) hopefully only one!
                _purgeTimer = new Timer(333);
                _purgeTimer.Elapsed += new ElapsedEventHandler(PurgeTimer_Elapsed);
                _purgeTimer.Start();
                _telemetry.Write(module, "Debug", "Purge Timer Started");
            }
        }

        /// <summary>
        /// </summary>
        /// <param name = "sel"></param>
        private void WordAppWindowSelectionChange(Selection sel)
        {
            switch (sel.ContentControls.Count)
            {
                case 0:
                    ContentControlSelection(false);
                    break;

                case 1:
                    {
                        // get the first content control from selection
                        object firstIndex = 1;
                        ContentControl cc = sel.ContentControls.get_Item(ref firstIndex);

                        // Check if the added content control is Chemistry Zone
                        if (Properties.Resources.ChemistryZoneAlias.Equals(cc.Title))
                        {
                            ContentControlSelection(true);
                        }
                        else
                        {
                            ContentControlSelection(false);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name = "doc"></param>
        /// <param name = "wn"></param>
        private void ApplicationWindowActivate(Document doc,
                                               Window wn)
        {
            string module = "CoreClass.ApplicationWindowActivate()";
            DateTime started = DateTime.Now;

            Debug.WriteLine("Started " + module);

            Microsoft.Office.Tools.Word.Document wordDoc =
                Microsoft.Office.Tools.Word.Document.GetVstoObject(wordApp.ActiveWindow.Document);
            if (wordDoc != null)
            {
                if (Setting.UseGallery)
                {
                    // Save state of Saved flag
                    bool docWasSaved = wordDoc.Saved;
                    string templateFileName = "Chem4Word2010.dotx";
                    if (WordVersion == 2007)
                    {
                        templateFileName = "Chem4Word2007.dotx";
                    }
                    wordDoc.AttachedTemplate = localAppDataFolder + @"\Chemistry Gallery\" + templateFileName;
                    if (docWasSaved)
                    {
                        // Re-instate state of Saved flag
                        wordDoc.Saved = true;
                    }
                }
            }

            // Switch ActiveChemistryDocument every time user switch document in Word.
            if (documentDictionary.ContainsKey(wordApp.ActiveDocument))
            {
                ActiveChemistryDocument = documentDictionary[wordApp.ActiveDocument];
            }
            else
            {
                ActiveChemistryDocument = new ChemistryDocument(doc, this);
                documentDictionary.Add(wordApp.ActiveDocument, ActiveChemistryDocument);
                doc.ContentControlAfterAdd += ActiveDocumentContentControlAfterAdd;
                doc.BuildingBlockInsert += ActiveDocumentBuildingBlockInsert;
            }

            // Fire Event
            if (WindowActivate != null)
            {
                WindowActivate(
                    this,
                    new ChemistryDocumentEventArgs
                        {
                            ChemistryDocument = ActiveChemistryDocument,
                            CurrentWindow = wn
                        });
            }
            TimeSpan ts = DateTime.Now - started;
            Debug.WriteLine("Executing " + module + " took " + ts.TotalMilliseconds.ToString("#,##0") + "ms");
        }

        private void ApplicationWindowDeactivate(Document document, Window window)
        {
            string module = "CoreClass.ApplicationWindowDeactivate()";
            DateTime started = DateTime.Now;

            Debug.WriteLine("Started " + module);

            if (WindowDeactivateEvent != null)
            {
                //if (wordApp.Documents.Count == 0)
                //{
                //    Debug.WriteLine("No Documents loaded");
                //}
                //else
                //{
                //    Debug.WriteLine("Document Count: " + wordApp.Documents.Count);
                //}

                WindowDeactivateEvent(this, new ChemistryDocumentEventArgs
                                                {
                                                    CurrentDocument = document,
                                                    CurrentWindow = window
                                                });
            }
            TimeSpan ts = DateTime.Now - started;
            Debug.WriteLine("Executing " + module + " took " + ts.TotalMilliseconds.ToString("#,##0") + "ms");
        }

        /// <summary>
        ///   Called whenever user creates a content control in the document -
        ///   regardless of whether or not it is a chemzone.
        ///
        ///   If it *is* a ChemZone then we handle it.
        ///   (it is only a chemzone if the title is set to "Chemistry"
        ///   which is how molecules from the Gallery get added.
        ///
        ///   When we create content controls (ie for new 2D or 1D zone) we
        ///   don't already have the title set so this method does not
        ///   handles them.
        /// </summary>
        /// <param name = "newContentControl"></param>
        /// <param name = "inUndoRedo"></param>
        private void ActiveDocumentContentControlAfterAdd(ContentControl newContentControl, bool inUndoRedo)
        {
            string module = "CoreClass.ActiveDocumentContentControlAfterAdd()";

            if (inUndoRedo)
            {
                // TODO JAT only show this message if high level alerts are turned on
                MessageBox.Show("You have now unbound the backing CML from the on screen representation",
                                Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK, MessageBoxImage.Stop);
                newContentControl.Title = string.Empty;
                newContentControl.Tag = null;
                newContentControl.XMLMapping.CustomXMLNode.Delete();
            }
            try
            {
                // Check if the added content control is Chemistry Zone
                if (Properties.Resources.ChemistryZoneAlias.Equals(newContentControl.Title))
                {
                    // Get the CML associated with the GUID of the Content saved in the Gallery (the tag)
                    XDocument cml = null;
                    string cmlString = galleryDictionaryManager.GetCmlString(newContentControl.Tag + string.Empty);
                    if (!string.IsNullOrEmpty(cmlString))
                    {
                        if (cmlString.Trim() != string.Empty)
                        {
                            cml = XDocument.Parse(cmlString);
                            newContentControl.XMLMapping.Delete();
                        }
                    }

                    // Clear Tag value - indicates that this is a chemistry zone in its own right rather than
                    // being a gallery item still
                    newContentControl.Tag = string.Empty;

                    //Flag the old Content Control for delete
                    newContentControl.Title = "CONTENTCONTROL_FLAGGED_FOR_DELETE";
                    newContentControl.LockContents = false;
                    if (WordVersion > 2007)
                    {
                        newContentControl.Range.Text = " ";
                        newContentControl.SetPlaceholderText(Text: " ");
                    }

                    if (cml != null)
                    {
                        ContextObject contextObject = new ContextObject(cml);
                        DepictionOption documentDepictionOption = GetPreferedDocumentDepiction(contextObject,
                            Setting.
                                DocumentPreferedDepiction);
                        DepictionOption navigatorDepictionOption = GetPreferedNavigatorDepiction(contextObject,
                            Setting.
                                NavigatorPreferedDepiction);
                        ChemistryZoneProperties chemistryZoneProperties =
                            new ChemistryZoneProperties(documentDepictionOption, navigatorDepictionOption,
                                Setting.CollapseNavigatorDepiction);

                        // because the imported content control is a picture we now need to change the depictions to the preferred setting
                        Range range = wordApp.ActiveDocument.ActiveWindow.Selection.Range;

                        _telemetry.Write(module, "Debug", "Inserting New Context Object");
                        // Disable Document events while adding New Contect Object
                        ActiveChemistryDocument.EventTurnOn = false;
                        AddNewContextObjectToDocument(range, contextObject, chemistryZoneProperties);
                        ActiveChemistryDocument.EventTurnOn = true;
                        _telemetry.Write(module, "Debug", "Inserted New Context Object");
                    }
                    else
                    {
                        _telemetry.Write(module, "Debug", "Copy/Paste or Drag/Drop of chemistry content control without cml detected");
                        MessageBox.Show("Copy/Paste or Drag/Drop of chemistry content control without cml detected." +
                            Environment.NewLine + "Please use Chemistry Navigator if you wish to make a copy.",
                            Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                                MessageBoxImage.Stop);
                        // Start timer to delete flagged Content Control(s) hopefully only one!
                        _purgeTimer = new Timer(333);
                        _purgeTimer.Elapsed += new ElapsedEventHandler(PurgeTimer_Elapsed);
                        _purgeTimer.Start();
                        _telemetry.Write(module, "Debug", "Purge Timer Started");
                    }
                }
            }
            catch (Exception ex)
            {
                _telemetry.Write(module, "Exception", ex.Message);
                //MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                //                MessageBoxImage.Stop);
                new ErrorReport(_telemetry, WordTopLeft, module, ex.Message).ShowDialog();
            }
        }

        private void PurgeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine("PurgeTimer_Elapsed()");
            try
            {
                if (wordApp != null && wordApp.ActiveDocument != null)
                {
                    foreach (ContentControl item in wordApp.ActiveDocument.ContentControls)
                    {
                        if (item.Title.CompareTo("CONTENTCONTROL_FLAGGED_FOR_DELETE") == 0)
                        {
                            Debug.WriteLine("Deleting flagged Content Control " + item.ID);
                            _telemetry.Write("CoreClass.PurgeTimer_Elapsed()", "Debug", "Deleting flagged Content Control " + item.ID);
                            item.Delete(true);
                        }
                    }
                }
                _purgeTimer.Elapsed -= PurgeTimer_Elapsed;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in PurgeTimer_Elapsed() - " + ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name = "doc"></param>
        /// <param name = "cancel"></param>
        private void WordAppDocumentBeforeClose(Document doc, ref bool cancel)
        {
            foreach (KeyValuePair<Document, IChemistryDocument> keyValue in documentDictionary)
            {
                if (keyValue.Value.WordDocument == doc)
                {
                    // Fire Event
                    if (DocumentBeforeClose != null)
                    {
                        DocumentBeforeClose(
                            this,
                            new ChemistryDocumentEventArgs
                                {
                                    CurrentDocument = keyValue.Key,
                                    ChemistryDocument = keyValue.Value
                                });
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IChemistryZone OpsinLookUpClick()
        {
            //string module = "CoreClass.OpsinLookUpClick()";

            //_telemetry.Write(module, "Information", "");
            string searchTerm = string.Empty;
            if (wordApp.Selection.ContentControls.Count == 0 &&
                wordApp.Selection.Range.Start != wordApp.Selection.Range.End)
            {
                searchTerm = wordApp.Selection.Text;
            }
            return OpsinLookUpSearch(searchTerm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public IChemistryZone OpsinLookUpSearch(string searchTerm)
        {
            string module = "CoreClass.OpsinLookUpSearch()";

            string searchResult = string.Empty;
            IChemistryZone chemistryZone = null;

            try
            {
                var opsinLookUp = new OpsinLookUp { SearchTerm = searchTerm };
                if (opsinLookUp.ShowDialog() == true)
                {
                    _telemetry.Write(module, "Information", "Search Term: '" + opsinLookUp.SearchTerm + "'");
                    searchResult = opsinLookUp.ResultDocument.ToString();

                    ImportMediator importMediator = new ImportMediator();
                    importMediator.Start(opsinLookUp.ResultDocument);
                    if (importMediator.Worked())
                    {
                        ContextObject contextObject = Cid.RemoveMoleculeBoldNumberLabels(importMediator.GetContextObject(),
                                                                                         importMediator.GetContextObject().
                                                                                             Cml.
                                                                                             Root);

                        DepictionOption documentDepictionOption = GetPreferedDocumentDepiction(contextObject,
                                                                                               Setting.
                                                                                                   DocumentPreferedDepiction);
                        DepictionOption navigatorDepictionOption = GetPreferedNavigatorDepiction(contextObject,
                                                                                                 Setting.
                                                                                                     NavigatorPreferedDepiction);
                        var chemistryZoneProperties = new ChemistryZoneProperties(documentDepictionOption,
                                                                                  navigatorDepictionOption,
                                                                                  Setting.CollapseNavigatorDepiction);

                        Range range = wordApp.ActiveDocument.ActiveWindow.Selection.Range;

                        CmlMolecule mol = new CmlMolecule((XElement)documentDepictionOption.MachineUnderstandableOption);
                        double? averageBondLength = mol.GetAverageBondLength();
                        if (WordVersion > 2007 && averageBondLength < 5)
                        {
                            mol.ScaleToAverageBondLength(20);
                            _telemetry.Write(module, "Information",
                                "Changed average bond length from "
                                + SafeDouble.AsString(averageBondLength, "#0.000") + " to 20");
                        }

                        chemistryZone = AddNewContextObjectToDocument(range, contextObject,
                                                                      chemistryZoneProperties);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                _telemetry.Write(module, "Exception", ex.Message);
                _telemetry.Write(module, "Exception(Data)", searchResult);
                //MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                //                MessageBoxImage.Stop);
                new ErrorReport(_telemetry, WordTopLeft, module, ex.Message).ShowDialog();
            }
            return chemistryZone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public IChemistryZone PubChemLookUpSearch(string searchTerm)
        {
            string module = "CoreClass.PubChemLookUpSearch()";

            string searchResult = string.Empty;
            IChemistryZone chemistryZone = null;

            try
            {
                var pubChemLookUp = new PubChemSearch { SearchTerm = searchTerm };
                if (pubChemLookUp.ShowDialog() == true)
                {
                    _telemetry.Write(module, "Information", "Search Term: '" + pubChemLookUp.SearchTerm + "'");
                    searchResult = pubChemLookUp.ResultDocument.ToString();

                    ImportMediator importMediator = new ImportMediator();
                    importMediator.Start(pubChemLookUp.ResultDocument);
                    if (importMediator.Worked())
                    {
                        ContextObject contextObject = Cid.RemoveMoleculeBoldNumberLabels(importMediator.GetContextObject(),
                                                                                         importMediator.GetContextObject().
                                                                                             Cml.
                                                                                             Root);

                        DepictionOption documentDepictionOption = GetPreferedDocumentDepiction(contextObject,
                                                                                               Setting.
                                                                                                   DocumentPreferedDepiction);
                        DepictionOption navigatorDepictionOption = GetPreferedNavigatorDepiction(contextObject,
                                                                                                 Setting.
                                                                                                     NavigatorPreferedDepiction);
                        var chemistryZoneProperties = new ChemistryZoneProperties(documentDepictionOption,
                                                                                  navigatorDepictionOption,
                                                                                  Setting.CollapseNavigatorDepiction);

                        Range range = wordApp.ActiveDocument.ActiveWindow.Selection.Range;

                        CmlMolecule mol = new CmlMolecule((XElement)documentDepictionOption.MachineUnderstandableOption);
                        double? averageBondLength = mol.GetAverageBondLength();
                        if (WordVersion > 2007 && averageBondLength < 5)
                        {
                            mol.ScaleToAverageBondLength(20);
                            _telemetry.Write(module, "Information",
                                "Changed average bond length from "
                                + SafeDouble.AsString(averageBondLength, "#0.000") + " to 20");
                        }

                        chemistryZone = AddNewContextObjectToDocument(range, contextObject,
                                                                      chemistryZoneProperties);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                _telemetry.Write(module, "Exception", ex.Message);
                _telemetry.Write(module, "Exception(Data)", searchResult);
                //MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                //                MessageBoxImage.Stop);
                new ErrorReport(_telemetry, WordTopLeft, module, ex.Message).ShowDialog();
            }

            return chemistryZone;
        }

        /// <summary>
        /// </summary>
        public IChemistryZone PubChemLookUpClick()
        {
            //string module = "CoreClass.PubChemLookUpClick()";
            //_telemetry.Write(module, "Information", "");
            string searchTerm = string.Empty;
            if (wordApp.Selection.ContentControls.Count == 0 &&
                wordApp.Selection.Range.Start != wordApp.Selection.Range.End)
            {
                searchTerm = wordApp.Selection.Text;
            }
            return PubChemLookUpSearch(searchTerm);
        }

        ///<summary>
        ///</summary>
        ///<param name = "searchTerm"></param>
        public void DocumentSearch(string searchTerm)
        {
            foreach (IChemistryZone chemistryZone in from chemistryZone in ActiveChemistryDocument
                                                     let depictionOptions =
                                                         Depiction.PossibleDepictionOptions(
                                                             chemistryZone.AsContextObject())
                                                     where
                                                         depictionOptions.Any(
                                                             depictionOption =>
                                                             depictionOption.GetAsLatexFormattedString().Contains(
                                                                 searchTerm))
                                                     select chemistryZone)
            {
                chemistryZone.Choose();
                return;
            }
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        ///<exception cref = "Exception"></exception>
        public DepictionOption AddNewLabel()
        {
            try
            {
                IChemistryZone chemistryZone = ActiveChemistryDocument.SelectedChemistryZone;
                ICollection<IChemistryZone> commonBindingZones = GetAllChemistryZonesBindingToTheCmlInThis(chemistryZone);
                ContextObject contextObject = chemistryZone.AsContextObject();

                Dictionary<DepictionOption, ICollection<IChemistryZone>> documentDepictionOptionsInUse =
                    GetDocumentDepictionOptionsInUse(contextObject, commonBindingZones);
                Dictionary<DepictionOption, ICollection<IChemistryZone>> navigatorDepictionOptionsInUse =
                    GetNavigatorDepictionOptionsInUse(contextObject, commonBindingZones);

                IEnumerable<DepictionOption> currentDepictionOptions = Depiction.PossibleDepictionOptions(contextObject);

                EditLabels editLabels = new EditLabels(contextObject, documentDepictionOptionsInUse,
                                                       navigatorDepictionOptionsInUse, false);
                editLabels.AddNewLabel();
                if (editLabels.ShowDialog() == true)
                {
                    chemistryZone.Cml = editLabels.ContextObject.Cml;
                    IEnumerable<DepictionOption> newDepictionOptions =
                        Depiction.PossibleDepictionOptions(editLabels.ContextObject);
                    foreach (DepictionOption newDepictionOption in
                        newDepictionOptions.Where(
                            newDepictionOption => !currentDepictionOptions.Contains(newDepictionOption)))
                    {
                        return newDepictionOption;
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception("Chemistry Object was broken:\n Cannot find custom CML.", e);
            }
            catch (Exception e)
            {
                throw new Exception("Error Editing Label:\n" + e.Message, e);
            }
            return null;
        }
    }

    internal class ContextObjectAndDialogResultHolder
    {
        private readonly ContextObject contextObject;
        private readonly bool dialogResult;

        internal ContextObjectAndDialogResultHolder(ContextObject contextObject, bool dialogResult)
        {
            this.contextObject = contextObject;
            this.dialogResult = dialogResult;
        }

        internal ContextObject GetContextObject()
        {
            return contextObject;
        }

        internal bool GetDialogResult()
        {
            return dialogResult;
        }
    }
}