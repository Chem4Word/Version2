// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Chem4Word.Api;
using Chem4Word.Api.Core;
using Chem4Word.Api.Events;
using Chem4Word.Common;
using Chem4Word.Core.Properties;
using Chem4Word.Core.UserSetting;
using Chem4Word.UI.Converters;
using Chem4Word.UI.OOXML;
using Chem4Word.UI.TwoD;
using log4net;
using Microsoft.Office.Interop.Word;
using Numbo.Cml;
using Numbo.Coa;
using Office = Microsoft.Office.Core;

namespace Chem4Word.Core
{
    /// <summary>
    /// This class representation of collection of Chemistry Zone of a single Word Document.
    /// </summary>
    public class ChemistryDocument : IChemistryDocument
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (ChemistryDocument));
        private readonly Dictionary<string, IChemistryZone> chemistryZones;
        private readonly CoreClass core;
        private readonly Document document;
        private readonly XmlMappingManager xmlMappingManager;
        private int num;

        public Telemetry GetTelemetry()
        {
            return core.GetTelemety();
        }

        /// <summary>  
        /// Initializes a new instance of the Document class. <see cref="ChemistryDocument"/>
        /// </summary>
        /// <param name="document">Word doucment. <see cref="Microsoft.Office.Interop.Word.Document"/></param>
        /// <param name="chem4WordCore">Chem4Word core. <see cref="Chem4Word.Core.CoreClass"/></param>
        internal ChemistryDocument(Document document, CoreClass chem4WordCore)
        {
            EventTurnOn = true;
            chemistryZones = new Dictionary<string, IChemistryZone>();
            this.document = document;
            this.core = chem4WordCore;
            this.num = 1;

            // Initialize Chemistry Zone Dictionary.
            ContentControls contentControls =
                this.document.SelectContentControlsByTitle(Resources.ChemistryZoneAlias);
            if (contentControls != null)
            {
                foreach (ContentControl contentControl in contentControls)
                {
                    IChemistryZone chemZone = new ChemistryZone(contentControl, this)
                                                  {Name = Resources.ChemistryZoneNamePrefix + num};
                    num++;
                    chemistryZones.Add(contentControl.ID, chemZone);
                }
            }

            xmlMappingManager = new XmlMappingManager(this);

            // Register Events
            this.document.ContentControlBeforeDelete += DocumentContentControlBeforeDelete;
            this.document.ContentControlOnEnter += DocumentContentControlOnEnter;
            this.document.ContentControlOnExit += DocumentContentControlOnExit;
        }

        /// <summary>
        /// Gets Xml mapping manager.
        /// </summary>
        internal XmlMappingManager XmlMappingManager
        {
            get { return xmlMappingManager; }
        }

        /// <summary>
        /// Gets Word doucment object. <see cref="Microsoft.Office.Interop.Word.Document"/>
        /// </summary>
        public Document WordDocument
        {
            get { return document; }
        }

        /// <summary>
        /// Gets Chem4Word core object. <see cref="Chem4Word.Core.CoreClass"/>
        /// </summary>
        public ICoreClass Core
        {
            get { return core; }
        }

        public void WriteTelemetry(string operation, string level, string message)
        {
            core.WriteTelemetry(operation, level, message);
        }

        public int WordVersion
        {
            get { return core.WordVersion; }
        }

        /// <summary>
        /// Gets number of Chemistry Zone in side this document.
        /// </summary>
        public int Count
        {
            get { return chemistryZones.Count; }
        }

        /// <summary>
        /// Select the Chemistry Zone.
        /// </summary>
        public IChemistryZone SelectedChemistryZone { get; private set; }

        IEnumerator<IChemistryZone> IEnumerable<IChemistryZone>.GetEnumerator()
        {
            // Sort Temp Enumerator base on Rage of Chemistry Zone
            // And return the sorted dictionary.
            SortedDictionary<int, IChemistryZone> result = new SortedDictionary<int, IChemistryZone>();
            foreach (IChemistryZone chemZone in chemistryZones.Values)
            {
                result.Add(chemZone.ContentControl.Range.Start, chemZone);
            }
            return result.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // Sort Temp Enumerator base on Rage of Chemistry Zone
            // And return the sorted dictionary.
            SortedDictionary<int, IChemistryZone> result = new SortedDictionary<int, IChemistryZone>();
            foreach (IChemistryZone chemZone in chemistryZones.Values)
            {
                result.Add(chemZone.ContentControl.Range.Start, chemZone);
            }
            return result.Values.GetEnumerator();
        }

        public ICollection<IChemistryZone> GetOtherCommonBindingZones(IChemistryZone chemistryZone)
        {
            return XmlMappingManager.GetOtherCommonBindingZones(chemistryZone);
        }

        public IChemistryZone RebindDocumentContentControl(IChemistryZone chemistryZone,
                                                           ChemistryZoneProperties newChemistryZoneProperties)
        {
            string module = "ChemistryDocument.RebindDocumentContentControl(b)";
            WriteTelemetry(module, "Information", "Called");
            ContentControl contentControl = null;
            string refVal = XmlMappingManager.GetCmlRefValueByZone(chemistryZone);
            DepictionOption newDocumentDepictionOption = DepictionOption.CreateDepictionOption(chemistryZone.Cml,
                                                                                               newChemistryZoneProperties.DocumentDepictionOptionXPath);
            // before the current content control is deleted we must get the CML out of it
            // (the deletion causes the links to the CML to be broken)
            ContextObject contextObject = chemistryZone.AsContextObject();

            // the new depiction requires a *new* picture content control or is a text content control 
            // which means we have to remove whatever is currently in the existing content control
            EventTurnOn = false;
            chemistryZone.ContentControl.Delete(true);
            EventTurnOn = true;
            if (Depiction.Is2D(newDocumentDepictionOption))
            {
                object missing = Type.Missing;

                if (WordVersion > 2007)
                {
                    DateTime started = DateTime.Now;

                    CmlMolecule mol = new CmlMolecule((XElement)newDocumentDepictionOption.MachineUnderstandableOption);
                    core.WriteTelemetry(module, "Information", "Atoms: " + mol.GetAllAtoms().Count());
                    core.WriteTelemetry(module, "Information", "Bonds: " + mol.GetAllBonds().Count());

                    C4wOptions options = new C4wOptions();
                    options.ColouredAtoms = Setting.RenderAtomsInColour;
                    options.ShowHydrogens = Setting.RenderImplicitHydrogens;
                    string guidString = Guid.NewGuid().ToString("N");
                    string bookmarkName = "C4W_" + guidString;
                    OoXmlFile ooXmlFile = new OoXmlFile(core.GetTelemety());
                    string tempfileName = ooXmlFile.CreateFromCml(contextObject.Cml.ToString(), guidString, options);

                    contentControl =
                        WordDocument.ContentControls.Add(WdContentControlType.wdContentControlRichText,
                            ref missing);
                    DateTime started2 = DateTime.Now;
                    contentControl.Range.InsertFile(tempfileName, bookmarkName);
                    TimeSpan ts2 = DateTime.Now - started2;
                    core.WriteTelemetry(module, "Information", "Range.InsertFile took " + ts2.TotalMilliseconds.ToString("#,##0.0") + "ms");

                    if (WordDocument.Bookmarks.Exists(bookmarkName))
                    {
                        WordDocument.Bookmarks[bookmarkName].Delete();
                    }
                    File.Delete(tempfileName);

                    TimeSpan ts = DateTime.Now - started;
                    core.WriteTelemetry(module, "Information", "Rendering OOXML took " + ts.TotalMilliseconds.ToString("#,##0.0") + "ms");
                }
                else
                {
                    // Existing code goes here
                    CmlMolecule cmlMolecule =
                        new CmlMolecule((XElement)newDocumentDepictionOption.MachineUnderstandableOption);
                    CanvasContainer editor = new CanvasContainer(contextObject, cmlMolecule.CloneMolecule(1.54));
                    editor.GeneratePng(false);

                    contentControl = WordDocument.ContentControls.Add(WdContentControlType.wdContentControlPicture,
                                                                           ref missing);

                    contentControl.Range.InlineShapes.AddPicture(editor.PngFileOutput, ref missing, ref missing,
                                                                 ref missing);
                    // Sometime the the open state of the file is not update quickly enough,
                    // So that we need to invoke GC to refresh the environment states.
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    // Delete the temporary png file
                    try
                    {
                        File.Delete(editor.PngFileOutput);
                    }
                    catch
                    {
                        Log.Debug("couldn't delete: " + editor.PngFileOutput);
                    }
                }

            }
            else
            {
                object range = WordDocument.Application.Selection.Range;
                contentControl = WordDocument.ContentControls.Add(WdContentControlType.wdContentControlRichText,
                                                                       ref range);
                core.CreateOneDZoneContent(ref contentControl, newDocumentDepictionOption);
            }
            // Set Alias = "chemistry"
            contentControl.Title = Resources.ChemistryZoneAlias;
            return RebindDocumentContentControl(contentControl, refVal, newChemistryZoneProperties);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ContentControlEventArgs> ContentControlAfterAdd;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ContentControlEventArgs> ContentControlBeforeDelete;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ContentControlEventArgs> ContentControlAfterDelete;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ContentControlEventArgs> ContentControlOnEnter;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ContentControlEventArgs> ContentControlOnExit;

        /// <summary>
        /// Retrieve Chemistry Zone by id.
        /// </summary>
        /// <param name="id">String value specify id of Chemistry Zone.</param>
        /// <returns>Return Chemistry Zone object.</returns>
        public IChemistryZone GetChemistryZone(string id)
        {
            return chemistryZones[id];
        }

        ///<summary>
        ///</summary>
        public bool EventTurnOn { get; set; }

        /// <summary>
        /// Create Linked Chemistry Zone.
        /// </summary>
        /// <param name="newContentControl">The created content control.</param>
        /// <param name="newChemistryZoneProperties"></param>
        /// <param name="sourceChemZone">Source Chemistry Zone</param>
        public IChemistryZone DocumentContentControlAfterAdd(ContentControl newContentControl,
                                                             ChemistryZoneProperties newChemistryZoneProperties,
                                                             IChemistryZone sourceChemZone)
        {
            IChemistryZone newChemistryZone = null;
            // Check if the Content Control is Chemistry Zone
            if (Resources.ChemistryZoneAlias.Equals(newContentControl.Title))
            {
                // Add NewContentControl into Dictionary
                newChemistryZone = new ChemistryZone(newContentControl, this)
                                       {Name = Resources.ChemistryZoneNamePrefix + num};
                num++;

                ChemistryZoneProperties properties = newChemistryZoneProperties;
                DepictionOption documentDepictionOption =
                    DepictionOption.CreateDepictionOption(sourceChemZone.Cml, properties.DocumentDepictionOptionXPath);

                DepictionOption navigatorDepictionOption =
                    DepictionOption.CreateDepictionOption(sourceChemZone.Cml, properties.NavigatorDepictionOptionXPath);

//                newChemistryZone.Properties = new ChemistryZoneProperties(documentDepictionOption,
//                                                                          navigatorDepictionOption, newChemistryZoneProperties.CollapseNavigatorDepiction);
                newChemistryZone.Properties = newChemistryZoneProperties;

                string refVal = xmlMappingManager.GetCmlRefValueByZone(sourceChemZone);
                xmlMappingManager.UpdateCmlRefByZone(newChemistryZone, refVal);

                chemistryZones.Add(newContentControl.ID, newChemistryZone);

                // Fire event After Add Content Control
                if (ContentControlAfterAdd != null)
                {
                    ContentControlEventArgs eventArgs = new ContentControlEventArgs
                                                            {
                                                                ContentChemistryZone = newChemistryZone
                                                            };
                    ContentControlAfterAdd(this, eventArgs);
                    newChemistryZone = eventArgs.ContentChemistryZone;
                }
                newChemistryZone.Lock();
            }
            return newChemistryZone;
        }

        /// <summary>
        /// Event handle when new Chemistry Zone was added into Word document 
        /// and we know what the properties are.
        /// </summary>
        /// <param name="newContentControl">The created content control.</param>
        /// <param name="cml">Cml that is going to be in the Chemistry Zone.</param>
        /// <param name="properties">Properties bag associate with Chemistry Zone.</param>
        public IChemistryZone DocumentContentControlAfterAdd(
            ContentControl newContentControl, XDocument cml,
            ChemistryZoneProperties properties)
        {
            // Add NewContentControl into Dictionary
            IChemistryZone chemZone = new ChemistryZone(newContentControl, this) {Name = Resources.ChemistryZoneNamePrefix + num};
            num++;

            chemZone.Cml = cml;
            chemZone.Properties = properties;

            chemistryZones.Add(newContentControl.ID, chemZone);

            // Fire event After Add Content Control
            if (ContentControlAfterAdd != null)
            {
                ContentControlEventArgs eventArgs = new ContentControlEventArgs
                                                        {
                                                            ContentChemistryZone = chemZone
                                                        };

                ContentControlAfterAdd(this, eventArgs);
                chemZone = eventArgs.ContentChemistryZone;
            }
            chemZone.Lock();
            return chemZone;
        }

        internal IChemistryZone SelectChemistryZoneById(string id)
        {
            foreach (IChemistryZone chemZone in chemistryZones.Values)
            {
                if (chemZone.ID == id)
                {
                    return chemZone;
                }
            }
            return null;
        }

        /// <summary>
        /// Rebind Document Content Control to new Cml file.
        /// </summary>
        /// <param name="existingContentControl"></param>
        /// <param name="refVal">the id of the customXML containing the relevant CML</param>
        /// <param name="properties"></param>
        private IChemistryZone RebindDocumentContentControl(
            ContentControl existingContentControl,
            string refVal,
            ChemistryZoneProperties properties)
        {
            string module = "ChemistryDocument.RebindDocumentContentControl(a)";
            WriteTelemetry(module, "Information", "Called");

            // Check if the Content Control is Chemistry Zone
            if (Resources.ChemistryZoneAlias.Equals(existingContentControl.Title))
            {
                // Add NewContentControl into Dictionary
                IChemistryZone chemZone = new ChemistryZone(existingContentControl, this)
                                              {Name = Resources.ChemistryZoneNamePrefix + num};
                num++;

                xmlMappingManager.UpdateCmlRefByZone(chemZone, refVal);
                chemZone.Properties = properties;

                chemistryZones.Add(existingContentControl.ID, chemZone);

                // Fire event After Add Content Control
                if (ContentControlAfterAdd != null)
                {
                    ContentControlEventArgs eventArgs = new ContentControlEventArgs
                                                            {
                                                                ContentChemistryZone = chemZone
                                                            };
                    ContentControlAfterAdd(this, eventArgs);
                    chemZone = eventArgs.ContentChemistryZone;
                }

                return chemZone;
            }
            return null;
        }

        /// <summary>
        /// Event handler when content control is being delete from Word document.
        /// </summary>
        /// <param name="oldContentControl">The Deleted content control.</param>
        /// <param name="inUndoRedo">True or false if the action is cause by undo or not-undo.</param>
        private void DocumentContentControlBeforeDelete(ContentControl oldContentControl,
                                                        bool inUndoRedo)
        {
            // Check if the Content Control is Chemistry Zone
            if (Resources.ChemistryZoneAlias.Equals(oldContentControl.Title))
            {
                IChemistryZone chemZone = chemistryZones[oldContentControl.ID];

                // Fire event Before Delete Content Control
                if (EventTurnOn && ContentControlBeforeDelete != null)
                {
                    ContentControlEventArgs eventArgs = new ContentControlEventArgs
                                                            {
                                                                ContentChemistryZone = chemZone,
                                                                Action = inUndoRedo
                                                            };

                    this.ContentControlBeforeDelete(this, eventArgs);
                }

                // Remove OldContentControl from Dictionary
                chemistryZones.Remove(oldContentControl.ID);

                // Fire event After Delete Content Control
                if (EventTurnOn && ContentControlAfterDelete != null)
                {
                    this.ContentControlAfterDelete(
                        this,
                        new ContentControlEventArgs
                            {
                                ContentChemistryZone = chemZone,
                                Action = inUndoRedo
                            });
                }

                // Remove Chemistry Zone Property Custom XML
                xmlMappingManager.RemoveChemPropXpart(oldContentControl.ID);
            }
        }

        /// <summary>
        /// Event handler when cursor enter content control.
        /// </summary>
        /// <param name="contentControl">The content control is being entered.</param>
        internal void DocumentContentControlOnEnter(ContentControl contentControl)
        {
            // Check if the Content Control is Chemistry Zone
            if (Resources.ChemistryZoneAlias.Equals(contentControl.Title))
            {
                SelectedChemistryZone = chemistryZones[contentControl.ID];

                if (ContentControlOnEnter != null)
                {
                    IChemistryZone chemZone = chemistryZones[contentControl.ID];
                    this.ContentControlOnEnter(
                        this,
                        new ContentControlEventArgs
                            {
                                ContentChemistryZone = chemZone
                            });
                }

                document.Application.Selection.SetRange(contentControl.Range.Start - 1,
                                                        contentControl.Range.End + 1);
            }
        }

        /// <summary>
        /// Event handler when cursor left content control
        /// </summary>
        /// <param name="contentControl">The content control is being left.</param>
        /// <param name="cancel">Set this cancel parameter to true, if we want to cancel the action.</param>
        private void DocumentContentControlOnExit(ContentControl contentControl,
                                                  ref bool cancel)
        {
            // Check if the Content Control is Chemistry Zone   
            if (Resources.ChemistryZoneAlias.Equals(contentControl.Title))
            {
                SelectedChemistryZone = null;

                // Fire event when cursor exit Chemistry Zone
                if (ContentControlOnExit != null)
                {
                    IChemistryZone chemZone = chemistryZones[contentControl.ID];
                    ContentControlEventArgs eventArgs = new ContentControlEventArgs
                                                            {
                                                                ContentChemistryZone = chemZone,
                                                                Action = cancel
                                                            };

                    this.ContentControlOnExit(this, eventArgs);
                    cancel = eventArgs.Action;
                }
            }
        }
    }
}