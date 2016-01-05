// Created by Mike Williams - 22/09/2015
// 
// -----------------------------------------------------------------------
//   Copyright (c) 2015, The Outercurve Foundation.  
//   This software is released under the Apache License, Version 2.0. 
//   The license and further copyright text can be found in the file LICENSE.TXT at
//   the root directory of the distribution.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Resources;
using Chem4Word.Common;
using Chem4Word.Common.Utilities;
using Chem4Word.UI.Converters;
using Chem4Word.UI.OOXML;
using Newtonsoft.Json.Linq;
using Ionic.Zip;

namespace Chem4Word.UI.ChemDoodle
{
    public partial class ChemDoodleEditorForm : Form, IMessageFilter
    {
        private string ms_AppTitle = "Chem4Word Structure Editor - Powered By ChemDoodle Web V";

        private const int WM_KEYDOWN = 0x0100;

        public OoXmlOptions UserOptions { get; set; }
        public string Before_CML { get; set; }
        public string Before_MolFile { get; set; }
        public string Before_JSON { get; set; }
        public string Before_Formula { get; set; }

        public string After_CML { get; set; }
        public string After_MolFile { get; set; }
        public string After_JSON { get; set; }
        public string After_Formula { get; set; }

        public Telemetry Telemetry { get; set; }

        public ChemDoodleEditorForm()
        {
            InitializeComponent();

            Application.AddMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {
            bool handled = false;

            if (m.Msg == WM_KEYDOWN)
            {
                //Debug.WriteLine("WM_KEYDOWN");
                if ((int)Control.ModifierKeys == (int)Keys.Control)
                {
                    //Debug.WriteLine("Contol Pressed");
                    Keys key = (Keys)(int)m.WParam & Keys.KeyCode;
                    switch (key)
                    {
                        #region Keys we are handling
                        case Keys.O: // File Open
                            btnOpen_Click(null, null);
                            handled = true;
                            break;
                        case Keys.S: // File Save
                            btnSaveAs_Click(null, null);
                            handled = true;
                            break;
                        #endregion

                        #region Keys we are supressing
                        case Keys.P:
                            handled = true;
                            break;
                        #endregion

                        // Pass the rest through
                        default:
                            break;
                    }
                }
            }

            return handled;
        }

        private void TweakChemDoodle_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            this.Show();
            Application.DoEvents();

            string temp = Path.GetTempPath();

            string markerFileName = Path.Combine(temp, "C4W-Version-2010-Beta-6-CDW-702.txt");
            if (!File.Exists(markerFileName))
            {
                string markerfile = Properties.Resources.C4W_MarkerFile_txt;
                File.WriteAllText(markerFileName, markerfile);

                string cssfile = Properties.Resources.Chem4Word_css;
                File.WriteAllText(Path.Combine(temp, "Chem4Word.css"), cssfile);

                string jsfile = Properties.Resources.Chem4Word_js;
                File.WriteAllText(Path.Combine(temp, "Chem4Word.js"), jsfile);

                string htmlfile = Properties.Resources.Offline_html;
                File.WriteAllText(Path.Combine(temp, "Editor.html"), htmlfile);

                Byte[] bytes = Properties.Resources.ChemDoodleWeb_702_zip;
                Stream stream = new MemoryStream(bytes);

                // NB: Top level of zip file must be the folder ChemDoodleWeb
                using (ZipFile zip = ZipFile.Read(stream))
                {
                    zip.ExtractAll(temp, ExtractExistingFileAction.OverwriteSilently);
                }
            }

            browser.Navigate(Path.Combine(temp, "Editor.html"));
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            this.Text = ms_AppTitle + ExecuteJavaScript("GetVersion");

            if (UserOptions.ColouredAtoms)
            {
                chkColouredAtoms.Checked = true;
            }
            else
            {
                chkColouredAtoms.Checked = false;
            }
            if (UserOptions.ShowHydrogens)
            {
                chkToggleImplicitHydrogens.Checked = true;
            }
            else
            {
                chkToggleImplicitHydrogens.Checked = false;
            }

            ExecuteJavaScript("InitialiseSketcherOptions", UserOptions.ColouredAtoms, UserOptions.ShowHydrogens);

            // Send JSON to ChemDoodle
            ExecuteJavaScript("SetJSON", Before_JSON);

            object obj = null;

            obj = ExecuteJavaScript("GetMolFile");
            Before_MolFile = obj.ToString();

            obj = ExecuteJavaScript("GetFormula");
            Before_Formula = obj.ToString();
            
            obj = ExecuteJavaScript("GetAverageBondLength");
            
            if (obj != null)
            {
                double averageBondLength = SafeDouble.Parse(obj.ToString());

                if (averageBondLength < 5)
                {
                    nudBondLength.Value = 20;
                    ExecuteJavaScript("ReScale", nudBondLength.Value);
                }
                else
                {
                    double newAverageBondLength = Math.Round(averageBondLength / 5.0) * 5;
                    nudBondLength.Value = (int)newAverageBondLength;
                }
            }
        }

        private object ExecuteJavaScript(string p_FunctionName, params object[] p_Args)
        {
            return browser.Document.InvokeScript(p_FunctionName, p_Args);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            object obj = null;

            obj = ExecuteJavaScript("GetFormula");
            After_Formula = obj.ToString();

            // Get molfile first as GetJSON sets scale
            obj = ExecuteJavaScript("GetMolFile");
            After_MolFile = obj.ToString();

            obj = ExecuteJavaScript("GetJSON");
            JToken molJson = JObject.Parse(obj.ToString());
            After_JSON = molJson.ToString();

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void TweakChemDoodle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != System.Windows.Forms.DialogResult.OK)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void chkColouredAtoms_CheckedChanged(object sender, EventArgs e)
        {
            UserOptions.Changed = true;
            if (chkColouredAtoms.Checked)
            {
                ExecuteJavaScript("AtomsInColour", true);
                UserOptions.ColouredAtoms = true;
            }
            else
            {
                ExecuteJavaScript("AtomsInColour", false);
                UserOptions.ColouredAtoms = false;
            }
        }

        private void chkToggleImplicitHydrogens_CheckedChanged(object sender, EventArgs e)
        {
            UserOptions.Changed = true;
            if (chkToggleImplicitHydrogens.Checked)
            {
                ExecuteJavaScript("ShowImplicitHCount");
                UserOptions.ShowHydrogens = true;
            }
            else
            {
                ExecuteJavaScript("HideImplicitHCount");
                UserOptions.ShowHydrogens = false;
            }
        }

        private void btnAddExplicitHydrogens_Click(object sender, EventArgs e)
        {
            ExecuteJavaScript("AddExplicitHydrogens");
        }

        private void btnRemoveExplicitHydrogens_Click(object sender, EventArgs e)
        {
            ExecuteJavaScript("RemoveHydrogens");
        }

        private void nudBondLength_ValueChanged(object sender, EventArgs e)
        {
            ExecuteJavaScript("ReScale", nudBondLength.Value);
        }

        private void btnFlip_Click(object sender, EventArgs e)
        {
            object obj = ExecuteJavaScript("GetJSON");
            string oldJson = obj.ToString();
            string newJson = Json.InvertY(oldJson);
            ExecuteJavaScript("SetJSON", newJson);
        }

        private void btnMirror_Click(object sender, EventArgs e)
        {
            object obj = ExecuteJavaScript("GetJSON");
            string oldJson = obj.ToString();
            string newJson = Json.InvertX(oldJson);
            ExecuteJavaScript("SetJSON", newJson);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("All Molecule Files (*.mol, *.cml)|*.mol;*.cml");
            sb.Append("|");
            sb.Append("CML Molecule Files (*.cml)|*.cml");
            sb.Append("|");
            sb.Append("MDL Molecule Files (*.mol)|*.mol");
            openFile.Filter = sb.ToString();
            openFile.FilterIndex = 1;

            DialogResult result = openFile.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string fileType = Path.GetExtension(openFile.FileName).ToLower();
                string fileContent = "";
                System.IO.StreamReader myFile = null;
                bool validFileType = false;

                switch (fileType)
                {
                    case ".mol":
                        Telemetry.Write("ChemDoodleEditorForm.btnOpen_Click()", "Information", "Opening " + fileType + " file");
                        myFile = new System.IO.StreamReader(openFile.FileName);
                        fileContent = myFile.ReadToEnd();
                        myFile.Close();
                        ExecuteJavaScript("SetMolFile", fileContent);
                        validFileType = true;
                        break;
                    case ".cml":
                        Telemetry.Write("ChemDoodleEditorForm.btnOpen_Click()", "Information", "Opening " + fileType + " file");
                        myFile = new System.IO.StreamReader(openFile.FileName);
                        fileContent = myFile.ReadToEnd();
                        myFile.Close();
                        ExecuteJavaScript("SetJSON", Cml.ToJson(fileContent));
                        validFileType = true;
                        break;
                    default:
                        break;
                }

                if (validFileType)
                {
                    object obj = ExecuteJavaScript("GetAverageBondLength");
                    double averageBondLength = SafeDouble.Parse(obj.ToString());

                    if (averageBondLength < 5)
                    {
                        nudBondLength.Value = 20;
                        ExecuteJavaScript("ReScale", nudBondLength.Value);
                    }
                    else
                    {
                        double newAverageBondLength = Math.Round(averageBondLength / 5.0) * 5;
                        nudBondLength.Value = (int)newAverageBondLength;
                    }
                }

            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CML Molecule Files (*.cml)|*.cml");
            sb.Append("|");
            sb.Append("MDL Molecule Files (*.mol)|*.mol");
            saveFile.FileName = "Untitled.cml";
            saveFile.Filter = sb.ToString();

            DialogResult result = saveFile.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                object obj = null;

                string fileType = Path.GetExtension(saveFile.FileName).ToLower();
                string fileContent = "";

                switch (fileType)
                {
                    case ".mol":
                        obj = ExecuteJavaScript("GetMolFile");
                        fileContent = obj.ToString();
                        break;
                    case ".cml":
                        obj = ExecuteJavaScript("GetJSON");
                        JToken molJson = JObject.Parse(obj.ToString());
                        fileContent = Json.ToCML(molJson.ToString());
                        break;
                    default:
                        break;
                }

                File.WriteAllText(saveFile.FileName, fileContent.Replace("utf-16", "utf-8"));
                Telemetry.Write("ChemDoodleEditorForm.btnSaveAs_Click()", "Information", "Saved " + fileType + " file");
            }
        }
    }
}
