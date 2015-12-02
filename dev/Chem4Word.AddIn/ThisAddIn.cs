// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.IO;
using System.Xml;
using Chem4Word.Core;
using log4net;
using log4net.Config;
using Microsoft.Office.Core;
using Microsoft.Win32;

namespace Chem4Word.AddIn {
    public partial class ThisAddIn {
        private const string ChemistryInstallPath = @"Chemistry Add-in for Word";
        private static readonly ILog Log = LogManager.GetLogger(typeof (ThisAddIn));

        private static readonly string AddInRegistryKeyPath =
            @"Software\Microsoft\Office\Word\Addins\Chemistry Add-in for Word";

        private static readonly string AddInManifestKeyName = @"Manifest";

        private void ThisAddInStartup(object sender, EventArgs e) {
            SetUpLogging();
        }

        private void ThisAddInShutdown(object sender, EventArgs e) {}

        /// <summary>
        ///   Sets local details of Chemistry Ribbon
        /// </summary>
        /// <param name = "serviceGuid"> system GUID</param>
        /// <returns>system object associated with new ChemistryRibbon</returns>
        protected override object RequestService(Guid serviceGuid) {
            if (serviceGuid == typeof (IRibbonExtensibility).GUID) {
                CoreClass chemistryCore = new CoreClass(Application);
                VstoSmartTags.Add(chemistryCore.SmartTag);
                return new ChemistryRibbon(chemistryCore, CustomTaskPanes, Application);
            }
            return base.RequestService(serviceGuid);
        }

        /// <summary>
        ///   Override the BeginInit method to load Chemistry Fonts.
        /// </summary>
        public override void BeginInit() {
            base.BeginInit();

            // Install 2 Chemisty fonts into current process.
            // Font name: Ms ChemSans, MS ChemSerif
            Chem4Word.Font.ChemistryFontManager.AddChemistryFontToProcess();
        }

        /// <summary>
        ///   Method to find the assembly directory name.
        /// </summary>
        /// <returns>
        ///   Returns the install path of the AddIn from the registry.
        /// </returns>
        private static string GetAssemblyDirectoryName() {
            string addInInstallPath = String.Empty;

            string codebase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codebase);
            string path = Uri.UnescapeDataString(uri.Path);
            addInInstallPath = Path.GetDirectoryName(path);

            return addInInstallPath;
        }

        private static void SetUpLogging() {
            string appDataFolder =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                             ChemistryInstallPath);

            string configFilePath = Path.Combine(appDataFolder, "Log4NetConfig.xml");

            if (File.Exists(configFilePath)) {
                try {
                    File.SetAttributes(configFilePath, FileAttributes.Normal);
                } catch {
                    //don't do anything, just let it go
                    Log.Debug("failed to configure logging");
                }
            }

            FileInfo configFile = new FileInfo(configFilePath);

            if (!configFile.Exists) {
                //if they don't already have a config file, go find it and copy it
                //string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                //                              ChemistryInstallPath+"\\Log4NetConfig.xml");
                string addInInstallPath = GetAssemblyDirectoryName();
                string logPath = Path.Combine(addInInstallPath, "Log4NetConfig.xml");
                if (File.Exists(logPath)) {
                    File.Copy(logPath, configFile.FullName);
                    XmlDocument document = new XmlDocument();
                    document.Load(configFile.FullName);

                    foreach (XmlNode node in document.GetElementsByTagName("file")) {
                        node.Attributes.GetNamedItem("value").Value =
                            Path.Combine(appDataFolder,
                                         "C4WLog.txt");
                        break;
                    }
                    document.Save(configFile.FullName);
                } else {
                    throw new Exception("Could not find logging configuration file.");
                }
            }
            XmlConfigurator.Configure(configFile);
        }

        /// <summary>
        ///   Required method for Designer support - do not modify
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup() {
            Startup += ThisAddInStartup;
            Shutdown += ThisAddInShutdown;
        }
    }
}