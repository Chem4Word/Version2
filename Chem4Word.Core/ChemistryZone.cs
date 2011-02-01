// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.IO;
using System.Xml.Linq;
using Chem4Word.Api;
using Chem4Word.UI.TwoD;
using log4net;
using Microsoft.Office.Interop.Word;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.Core
{
    /// <summary>
    /// This class funcation as representation of 1D or 2D Chemistry Zone.
    /// </summary>
    internal class ChemistryZone : IChemistryZone
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (ChemistryZone));
        private readonly ChemistryDocument document;
        private readonly string id;
        private ContentControl contentControl;

        /// <summary>
        /// Initializes a new instance of the ChemistryZone class.
        /// </summary>
        /// <param name="contentControl"></param>
        /// <param name="parentDocument">Parents document object of this Chemistry Zone</param>
        internal ChemistryZone(ContentControl contentControl,
                               ChemistryDocument parentDocument)
        {
            id = contentControl.ID;
            Name = string.Empty;
            this.contentControl = contentControl;
            document = parentDocument;
            Log.Debug("creating a new chemistry zone");
        }

        public event EventHandler<EventArgs> ChemistryZoneUpdated;

        public event EventHandler<EventArgs> ChemistryZonePropertiesUpdated;

        public void Unlock()
        {
            contentControl.LockContents = false;
        }

        public void Lock()
        {
            contentControl.LockContents = true;
        }

        /// <summary>
        /// gets id of this Chemistry Zone.
        /// </summary>
        string IChemistryZone.ID
        {
            get { return this.id; }
        }

        /// <summary>
        /// Gets name this Chemistry Zone.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Select this Chemistry Zone.
        /// </summary>
        public void Choose()
        {
            // Set cursor to selected Chemistry Zone
            contentControl.Range.Select();

            // Navigate page to cursor.
            contentControl.Application.Selection.GoToNext(WdGoToItem.wdGoToObject);

            // Update Content Control Selection state to parent document
            document.DocumentContentControlOnEnter(contentControl);
        }

        public void Refresh()
        {
            Unlock();
            DepictionOption documentDepictionOption = DepictionOption.CreateDepictionOption(Cml,
                                                                                            Properties.
                                                                                                DocumentDepictionOptionXPath);
            if (Depiction.Is2D(documentDepictionOption))
            {
                ContextObject contexObject = new ContextObject(Cml);
                CmlMolecule cmlMolecule = new CmlMolecule((XElement) documentDepictionOption.MachineUnderstandableOption);
                CanvasContainer editor = new CanvasContainer(contexObject, cmlMolecule);
                editor.GeneratePng(false);

                object missing = Type.Missing;
                contentControl.Range.InlineShapes[1].Delete();
                contentControl.Range.InlineShapes.AddPicture(editor.PngFileOutput, ref missing, ref missing, ref missing);
                // Delete the png file
                File.Delete(editor.PngFileOutput);
            }
            else
            {
                /// the document depiction is a 1D zone
                document.Core.CreateOneDZoneContent(ref contentControl, documentDepictionOption);
            }
            if (this.ChemistryZoneUpdated != null)
            {
                this.ChemistryZoneUpdated(this, null);
            }
            Lock();
        }

        /// <summary>
        /// Internal content control. <see cref="Microsoft.Office.Interop.Word.ContentControl"/>
        /// </summary>
        ContentControl IChemistryZone.ContentControl
        {
            get { return this.contentControl; }
        }

        /// <summary>
        /// Gets or sets back-end Cml. 
        /// <see cref="System.Xml.Linq.XDocument" />
        /// </summary>
        public XDocument Cml
        {
            get
            {
                XDocument cml = this.document.XmlMappingManager.ReadCmlByZone(this);
                return cml;
            }
            set
            {
                this.document.XmlMappingManager.UpdateCmlByZone(this, value);

                if (this.ChemistryZoneUpdated != null)
                {
                    this.ChemistryZoneUpdated(this, null);
                }
            }
        }

        public ChemistryZoneProperties Properties
        {
            get
            {
                ChemistryZoneProperties properties = this.document.XmlMappingManager.ReadZonePropertiesByZone(this);
                return properties;
            }
            set { this.document.XmlMappingManager.UpdateZoneProperties(this, value);
            if (this.ChemistryZonePropertiesUpdated != null)
            {
                this.ChemistryZonePropertiesUpdated(this, null);
            }
            }
        }

        /// <summary>
        /// Return a string representation of this Chemistry Zone.
        /// </summary>
        /// <returns>Return a string representation of this Chemistry Zone.</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}