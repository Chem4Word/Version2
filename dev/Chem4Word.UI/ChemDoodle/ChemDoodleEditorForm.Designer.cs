namespace Chem4Word.UI.ChemDoodle
{
    partial class ChemDoodleEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBoxExplicit = new System.Windows.Forms.GroupBox();
            this.btnRemoveExplicitHydrogens = new System.Windows.Forms.Button();
            this.btnAddExplicitHydrogens = new System.Windows.Forms.Button();
            this.groupBoxImplicit = new System.Windows.Forms.GroupBox();
            this.chkToggleImplicitHydrogens = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxExplicit.SuspendLayout();
            this.groupBoxImplicit.SuspendLayout();
            this.SuspendLayout();
            // 
            // browser
            // 
            this.browser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browser.Location = new System.Drawing.Point(0, 0);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(446, 330);
            this.browser.TabIndex = 0;
            this.browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.browser_DocumentCompleted);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(358, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(277, 360);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBoxExplicit
            // 
            this.groupBoxExplicit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxExplicit.Controls.Add(this.btnRemoveExplicitHydrogens);
            this.groupBoxExplicit.Controls.Add(this.btnAddExplicitHydrogens);
            this.groupBoxExplicit.Location = new System.Drawing.Point(141, 336);
            this.groupBoxExplicit.Name = "groupBoxExplicit";
            this.groupBoxExplicit.Size = new System.Drawing.Size(123, 50);
            this.groupBoxExplicit.TabIndex = 3;
            this.groupBoxExplicit.TabStop = false;
            this.groupBoxExplicit.Text = "Explicit Hydrogens";
            // 
            // btnRemoveExplicitHydrogens
            // 
            this.btnRemoveExplicitHydrogens.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveExplicitHydrogens.Image = global::Chem4Word.UI.Properties.Resources.Minus;
            this.btnRemoveExplicitHydrogens.Location = new System.Drawing.Point(72, 18);
            this.btnRemoveExplicitHydrogens.Name = "btnRemoveExplicitHydrogens";
            this.btnRemoveExplicitHydrogens.Size = new System.Drawing.Size(26, 26);
            this.btnRemoveExplicitHydrogens.TabIndex = 1;
            this.btnRemoveExplicitHydrogens.Text = "-";
            this.toolTip1.SetToolTip(this.btnRemoveExplicitHydrogens, "Convert explicit Hydrogens to implicit");
            this.btnRemoveExplicitHydrogens.UseVisualStyleBackColor = true;
            this.btnRemoveExplicitHydrogens.Click += new System.EventHandler(this.btnRemoveExplicitHydrogens_Click);
            // 
            // btnAddExplicitHydrogens
            // 
            this.btnAddExplicitHydrogens.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddExplicitHydrogens.Image = global::Chem4Word.UI.Properties.Resources.Plus;
            this.btnAddExplicitHydrogens.Location = new System.Drawing.Point(22, 18);
            this.btnAddExplicitHydrogens.Name = "btnAddExplicitHydrogens";
            this.btnAddExplicitHydrogens.Size = new System.Drawing.Size(26, 26);
            this.btnAddExplicitHydrogens.TabIndex = 0;
            this.btnAddExplicitHydrogens.Text = "+";
            this.toolTip1.SetToolTip(this.btnAddExplicitHydrogens, "Convert implicit Hydrogens to explicit");
            this.btnAddExplicitHydrogens.UseVisualStyleBackColor = true;
            this.btnAddExplicitHydrogens.Click += new System.EventHandler(this.btnAddExplicitHydrogens_Click);
            // 
            // groupBoxImplicit
            // 
            this.groupBoxImplicit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxImplicit.Controls.Add(this.chkToggleImplicitHydrogens);
            this.groupBoxImplicit.Location = new System.Drawing.Point(12, 336);
            this.groupBoxImplicit.Name = "groupBoxImplicit";
            this.groupBoxImplicit.Size = new System.Drawing.Size(114, 50);
            this.groupBoxImplicit.TabIndex = 4;
            this.groupBoxImplicit.TabStop = false;
            this.groupBoxImplicit.Text = "Implicit Hydrogens";
            // 
            // chkToggleImplicitHydrogens
            // 
            this.chkToggleImplicitHydrogens.AutoSize = true;
            this.chkToggleImplicitHydrogens.Checked = true;
            this.chkToggleImplicitHydrogens.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkToggleImplicitHydrogens.Location = new System.Drawing.Point(14, 25);
            this.chkToggleImplicitHydrogens.Name = "chkToggleImplicitHydrogens";
            this.chkToggleImplicitHydrogens.Size = new System.Drawing.Size(53, 17);
            this.chkToggleImplicitHydrogens.TabIndex = 0;
            this.chkToggleImplicitHydrogens.Text = "Show";
            this.toolTip1.SetToolTip(this.chkToggleImplicitHydrogens, "Check to show implicit Hydrogens");
            this.chkToggleImplicitHydrogens.UseVisualStyleBackColor = true;
            this.chkToggleImplicitHydrogens.CheckedChanged += new System.EventHandler(this.chkToggleImplicitHydrogens_CheckedChanged);
            // 
            // ChemDoodleEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 395);
            this.Controls.Add(this.groupBoxImplicit);
            this.Controls.Add(this.groupBoxExplicit);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.browser);
            this.Name = "ChemDoodleEditorForm";
            this.Text = "TweakChemDoodle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TweakChemDoodle_FormClosing);
            this.Load += new System.EventHandler(this.TweakChemDoodle_Load);
            this.groupBoxExplicit.ResumeLayout(false);
            this.groupBoxImplicit.ResumeLayout(false);
            this.groupBoxImplicit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBoxExplicit;
        private System.Windows.Forms.Button btnRemoveExplicitHydrogens;
        private System.Windows.Forms.Button btnAddExplicitHydrogens;
        private System.Windows.Forms.GroupBox groupBoxImplicit;
        private System.Windows.Forms.CheckBox chkToggleImplicitHydrogens;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}