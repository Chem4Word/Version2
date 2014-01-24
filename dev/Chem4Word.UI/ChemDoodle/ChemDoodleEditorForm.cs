using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Reflection;
using System.Resources;

using Newtonsoft.Json.Linq;
using Ionic.Zip;

namespace Chem4Word.UI.ChemDoodle
{
    public partial class ChemDoodleEditorForm : Form
    {
        private string ms_AppTitle = "Chem4Word Editor Powered By ChemDoodle Web V";

        public string Before_CML { get; set; }
        public string Before_MolFile { get; set; }
        public string Before_JSON { get; set; }

        public string After_CML { get; set; }
        public string After_MolFile { get; set; }
        public string After_JSON { get; set; }

        public ChemDoodleEditorForm()
        {
            InitializeComponent();
        }

        private void TweakChemDoodle_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string temp = Path.GetTempPath();
            //temp = @"C:\Temp";
            string cssfile = Properties.Resources.Chem4Word_css;

            //string htmlfile = Properties.Resources.HotLink_V523_html;
            //string htmlfile = Properties.Resources.HotLink_V600_html;
            string htmlfile = Properties.Resources.Offline_html;

            File.WriteAllText(Path.Combine(temp, "Chem4Word.css"), cssfile);
            File.WriteAllText(Path.Combine(temp, "Editor.html"), htmlfile);

            Byte[] bytes = Properties.Resources.ChemDoodleWeb_zip;
            Stream stream = new MemoryStream(bytes);

            using (ZipFile zip = ZipFile.Read(stream))
            {
                zip.ExtractAll(temp, ExtractExistingFileAction.OverwriteSilently);
            }

            browser.Navigate(Path.Combine(temp, "Editor.html"));

            //DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            this.Text = ms_AppTitle + ExecuteJavaScript("GetVersion");

            ExecuteJavaScript("SetJSON", Before_JSON, 25);

            object mol = null;
            string temp = null;

            mol = ExecuteJavaScript("GetMolFile");
            temp = mol.ToString();
            Before_MolFile = temp;
        }

        private object ExecuteJavaScript(string p_FunctionName, params object[] p_Args)
        {
            return browser.Document.InvokeScript(p_FunctionName, p_Args);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            object mol = null;
            string temp = null;

            // Get molfile first as GetJSON set scale
            mol = ExecuteJavaScript("GetMolFile");
            temp = mol.ToString();
            After_MolFile = temp;

            mol = ExecuteJavaScript("GetJSON");
            temp = mol.ToString();
            JToken molJson = JObject.Parse(temp);
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
    }
}
