// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Chem4Word.Api;
using Chem4Word.Common;
using Chem4Word.Core.Properties;
using Chem4Word.Core.UserSetting;
using Chem4Word.UI.OOXML;
using Chem4Word.UI.TwoD;
using log4net;
using Microsoft.Office.Interop.Word;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.Core {
    /// <summary>
    ///   This class funcation as representation of 1D or 2D Chemistry Zone.
    /// </summary>
    internal class ChemistryZone : IChemistryZone {
        private static readonly ILog Log = LogManager.GetLogger(typeof (ChemistryZone));
        private readonly ChemistryDocument document;
        private readonly string id;
        private ContentControl contentControl;

        /// <summary>
        ///   Initializes a new instance of the ChemistryZone class.
        /// </summary>
        /// <param name = "contentControl"></param>
        /// <param name = "parentDocument">Parents document object of this Chemistry Zone</param>
        internal ChemistryZone(ContentControl contentControl,
                               ChemistryDocument parentDocument) {
            id = contentControl.ID;
            Name = string.Empty;
            this.contentControl = contentControl;
            document = parentDocument;
            Log.Debug("creating a new chemistry zone");
        }

        #region IChemistryZone Members

        public int WordVersion {
            get
            {
                return document.WordVersion;
            }
        }
        public event EventHandler<EventArgs> ChemistryZoneUpdated;

        public event EventHandler<EventArgs> ChemistryZonePropertiesUpdated;

        public void Unlock() {
            contentControl.LockContents = false;
        }

        public void Lock() {
            contentControl.LockContents = true;
        }

        /// <summary>
        ///   gets id of this Chemistry Zone.
        /// </summary>
        string IChemistryZone.ID {
            get { return id; }
        }

        /// <summary>
        ///   Gets name this Chemistry Zone.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///   Select this Chemistry Zone.
        /// </summary>
        public void Choose() {
            // Set cursor to selected Chemistry Zone
            contentControl.Range.Select();

            // Navigate page to cursor.
            contentControl.Application.Selection.GoToNext(WdGoToItem.wdGoToObject);

            // Update Content Control Selection state to parent document
            document.DocumentContentControlOnEnter(contentControl);
        }

        public ContextObject AsContextObject() {
            var contextObject = new ContextObject(Cml) {ViewBoxDimensions = Properties.ViewBox};
            return contextObject;
        }

        public void Refresh()
        {
            string module = "ChemistryZone.Refresh()";
            document.WriteTelemetry(module,"Information","Called");
            Unlock();
            DepictionOption documentDepictionOption = DepictionOption.CreateDepictionOption(Cml,
                                                                                            Properties.
                                                                                                DocumentDepictionOptionXPath);
            if (Depiction.Is2D(documentDepictionOption)) {

                if (document.WordVersion > 2007)
                {
                    DateTime started = DateTime.Now;

                    CmlMolecule mol = new CmlMolecule((XElement)documentDepictionOption.MachineUnderstandableOption);
                    document.WriteTelemetry(module, "Information", "Atoms: " + mol.GetAllAtoms().Count());
                    document.WriteTelemetry(module, "Information", "Bonds: " + mol.GetAllBonds().Count());

                    OoXmlOptions options = new OoXmlOptions();
                    options.ColouredAtoms = Setting.RenderAtomsInColour;
                    options.ShowHydrogens = Setting.RenderImplicitHydrogens;
                    string guidString = Guid.NewGuid().ToString("N");
                    string bookmarkName = "C4W_" + guidString;
                    OoXmlFile ooXmlFile = new OoXmlFile(document.GetTelemetry());
                    string tempfileName = ooXmlFile.CreateFromCml(Cml.ToString(), guidString, options);

                    var missing = Type.Missing;
                    contentControl.Range.Delete();
                    DateTime started2 = DateTime.Now;

                    contentControl.Range.InsertFile(tempfileName, bookmarkName);
                    TimeSpan ts2 = DateTime.Now - started2;
                    //document.WriteTelemetry(module, "Information", "Range.InsertFile took " + ts2.TotalMilliseconds.ToString("#,##0.0") + "ms");
                    contentControl.Title = Resources.ChemistryZoneAlias;

                    if (document.WordDocument.Bookmarks.Exists(bookmarkName))
                    {
                        document.WordDocument.Bookmarks[bookmarkName].Delete();
                    }
                    File.Delete(tempfileName);

                    TimeSpan ts = DateTime.Now - started;
                    document.WriteTelemetry(module, "Information", "Rendering OOXML took " + ts.TotalMilliseconds.ToString("#,##0.0") + "ms");
                }
                else
                {
                    // Existing code goes here

                    var contexObject = AsContextObject();
                    var cmlMolecule = new CmlMolecule((XElement)documentDepictionOption.MachineUnderstandableOption);
                    var editor = new CanvasContainer(contexObject, cmlMolecule.CloneMolecule(1.54, false));
                    editor.GeneratePng(false);

                    var missing = Type.Missing;
                    contentControl.Range.InlineShapes[1].Delete();
                    contentControl.Range.InlineShapes.AddPicture(editor.PngFileOutput, ref missing, ref missing, ref missing);

                    // Delete the png file
                    File.Delete(editor.PngFileOutput);
                }

            }
            else
            {
                // the document depiction is a 1D zone
                document.Core.CreateOneDZoneContent(ref contentControl, documentDepictionOption);
            }
            if (ChemistryZoneUpdated != null) {
                ChemistryZoneUpdated(this, null);
            }
            Lock();
        }

        /// <summary>
        ///   Internal content control. <see cref = "Microsoft.Office.Interop.Word.ContentControl" />
        /// </summary>
        ContentControl IChemistryZone.ContentControl {
            get { return contentControl; }
        }

        /// <summary>
        ///   Gets or sets back-end Cml. 
        ///   <see cref = "XDocument.Xml.Linq, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
        /// </summary>
        public XDocument Cml {
            get {
                var cml = document.XmlMappingManager.ReadCmlByZone(this);
                return cml;
            }
            set {
                document.XmlMappingManager.UpdateCmlByZone(this, value);

                if (ChemistryZoneUpdated != null) {
                    ChemistryZoneUpdated(this, null);
                }
            }
        }

        public ChemistryZoneProperties Properties {
            get {
                var properties = document.XmlMappingManager.ReadZonePropertiesByZone(this);
                return properties;
            }
            set {
                document.XmlMappingManager.UpdateZoneProperties(this, value);
                if (ChemistryZonePropertiesUpdated != null) {
                    ChemistryZonePropertiesUpdated(this, null);
                }
            }
        }

        #endregion

        /// <summary>
        ///   Return a string representation of this Chemistry Zone.
        /// </summary>
        /// <returns>Return a string representation of this Chemistry Zone.</returns>
        public override string ToString() {
            return Name;
        }
    }
}