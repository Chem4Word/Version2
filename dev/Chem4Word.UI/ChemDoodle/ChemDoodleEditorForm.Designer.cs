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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChemDoodleEditorForm));
            this.browser = new System.Windows.Forms.WebBrowser();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBoxExplicit = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRemoveExplicitHydrogens = new System.Windows.Forms.Button();
            this.btnAddExplicitHydrogens = new System.Windows.Forms.Button();
            this.groupBoxImplicit = new System.Windows.Forms.GroupBox();
            this.chkColouredAtoms = new System.Windows.Forms.CheckBox();
            this.chkToggleImplicitHydrogens = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.nudBondLength = new System.Windows.Forms.NumericUpDown();
            this.btnFlip = new System.Windows.Forms.Button();
            this.btnMirror = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.groupBoxBondLength = new System.Windows.Forms.GroupBox();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxExplicit.SuspendLayout();
            this.groupBoxImplicit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBondLength)).BeginInit();
            this.groupBoxBondLength.SuspendLayout();
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
            this.browser.Size = new System.Drawing.Size(679, 465);
            this.browser.TabIndex = 0;
            this.browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.browser_DocumentCompleted);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(596, 530);
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
            this.btnOk.Location = new System.Drawing.Point(597, 502);
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
            this.groupBoxExplicit.Controls.Add(this.label2);
            this.groupBoxExplicit.Controls.Add(this.label1);
            this.groupBoxExplicit.Controls.Add(this.btnRemoveExplicitHydrogens);
            this.groupBoxExplicit.Controls.Add(this.btnAddExplicitHydrogens);
            this.groupBoxExplicit.Location = new System.Drawing.Point(6, 471);
            this.groupBoxExplicit.Name = "groupBoxExplicit";
            this.groupBoxExplicit.Size = new System.Drawing.Size(114, 83);
            this.groupBoxExplicit.TabIndex = 3;
            this.groupBoxExplicit.TabStop = false;
            this.groupBoxExplicit.Text = "Explicit Hydrogens";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Remove";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Add";
            // 
            // btnRemoveExplicitHydrogens
            // 
            this.btnRemoveExplicitHydrogens.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveExplicitHydrogens.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveExplicitHydrogens.Image")));
            this.btnRemoveExplicitHydrogens.Location = new System.Drawing.Point(14, 51);
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
            this.btnAddExplicitHydrogens.Image = ((System.Drawing.Image)(resources.GetObject("btnAddExplicitHydrogens.Image")));
            this.btnAddExplicitHydrogens.Location = new System.Drawing.Point(14, 18);
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
            this.groupBoxImplicit.Controls.Add(this.chkColouredAtoms);
            this.groupBoxImplicit.Controls.Add(this.chkToggleImplicitHydrogens);
            this.groupBoxImplicit.Location = new System.Drawing.Point(125, 471);
            this.groupBoxImplicit.Name = "groupBoxImplicit";
            this.groupBoxImplicit.Size = new System.Drawing.Size(122, 83);
            this.groupBoxImplicit.TabIndex = 4;
            this.groupBoxImplicit.TabStop = false;
            this.groupBoxImplicit.Text = "Rendering Options";
            // 
            // chkColouredAtoms
            // 
            this.chkColouredAtoms.AutoSize = true;
            this.chkColouredAtoms.Checked = true;
            this.chkColouredAtoms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkColouredAtoms.Location = new System.Drawing.Point(14, 57);
            this.chkColouredAtoms.Name = "chkColouredAtoms";
            this.chkColouredAtoms.Size = new System.Drawing.Size(88, 17);
            this.chkColouredAtoms.TabIndex = 0;
            this.chkColouredAtoms.Text = "Colour Atoms";
            this.toolTip1.SetToolTip(this.chkColouredAtoms, "Check to show atom labels in colour");
            this.chkColouredAtoms.UseVisualStyleBackColor = true;
            this.chkColouredAtoms.CheckedChanged += new System.EventHandler(this.chkColouredAtoms_CheckedChanged);
            // 
            // chkToggleImplicitHydrogens
            // 
            this.chkToggleImplicitHydrogens.AutoSize = true;
            this.chkToggleImplicitHydrogens.Checked = true;
            this.chkToggleImplicitHydrogens.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkToggleImplicitHydrogens.Location = new System.Drawing.Point(14, 25);
            this.chkToggleImplicitHydrogens.Name = "chkToggleImplicitHydrogens";
            this.chkToggleImplicitHydrogens.Size = new System.Drawing.Size(99, 17);
            this.chkToggleImplicitHydrogens.TabIndex = 0;
            this.chkToggleImplicitHydrogens.Text = "Show Implicit H";
            this.toolTip1.SetToolTip(this.chkToggleImplicitHydrogens, "Check to show implicit Hydrogens");
            this.chkToggleImplicitHydrogens.UseVisualStyleBackColor = true;
            this.chkToggleImplicitHydrogens.CheckedChanged += new System.EventHandler(this.chkToggleImplicitHydrogens_CheckedChanged);
            // 
            // nudBondLength
            // 
            this.nudBondLength.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudBondLength.Location = new System.Drawing.Point(15, 23);
            this.nudBondLength.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudBondLength.Name = "nudBondLength";
            this.nudBondLength.Size = new System.Drawing.Size(44, 20);
            this.nudBondLength.TabIndex = 15;
            this.toolTip1.SetToolTip(this.nudBondLength, "Change size of drawing (in Word)");
            this.nudBondLength.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudBondLength.ValueChanged += new System.EventHandler(this.nudBondLength_ValueChanged);
            // 
            // btnFlip
            // 
            this.btnFlip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFlip.Image = ((System.Drawing.Image)(resources.GetObject("btnFlip.Image")));
            this.btnFlip.Location = new System.Drawing.Point(365, 513);
            this.btnFlip.Name = "btnFlip";
            this.btnFlip.Size = new System.Drawing.Size(40, 40);
            this.btnFlip.TabIndex = 16;
            this.toolTip1.SetToolTip(this.btnFlip, "Flip drawing");
            this.btnFlip.UseVisualStyleBackColor = true;
            this.btnFlip.Click += new System.EventHandler(this.btnFlip_Click);
            // 
            // btnMirror
            // 
            this.btnMirror.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMirror.Image = ((System.Drawing.Image)(resources.GetObject("btnMirror.Image")));
            this.btnMirror.Location = new System.Drawing.Point(411, 513);
            this.btnMirror.Name = "btnMirror";
            this.btnMirror.Size = new System.Drawing.Size(40, 40);
            this.btnMirror.TabIndex = 18;
            this.toolTip1.SetToolTip(this.btnMirror, "Mirror drawing");
            this.btnMirror.UseVisualStyleBackColor = true;
            this.btnMirror.Click += new System.EventHandler(this.btnMirror_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAs.Image")));
            this.btnSaveAs.Location = new System.Drawing.Point(513, 513);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(40, 40);
            this.btnSaveAs.TabIndex = 19;
            this.toolTip1.SetToolTip(this.btnSaveAs, "Export File");
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.Location = new System.Drawing.Point(467, 513);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(40, 40);
            this.btnOpen.TabIndex = 20;
            this.toolTip1.SetToolTip(this.btnOpen, "Import File");
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // groupBoxBondLength
            // 
            this.groupBoxBondLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxBondLength.Controls.Add(this.nudBondLength);
            this.groupBoxBondLength.Location = new System.Drawing.Point(253, 471);
            this.groupBoxBondLength.Name = "groupBoxBondLength";
            this.groupBoxBondLength.Size = new System.Drawing.Size(84, 81);
            this.groupBoxBondLength.TabIndex = 17;
            this.groupBoxBondLength.TabStop = false;
            this.groupBoxBondLength.Text = "Bond Length";
            // 
            // ChemDoodleEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 562);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnMirror);
            this.Controls.Add(this.btnFlip);
            this.Controls.Add(this.groupBoxBondLength);
            this.Controls.Add(this.groupBoxExplicit);
            this.Controls.Add(this.groupBoxImplicit);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.browser);
            this.MinimumSize = new System.Drawing.Size(616, 478);
            this.Name = "ChemDoodleEditorForm";
            this.Text = "Chem4Word Structure Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TweakChemDoodle_FormClosing);
            this.Load += new System.EventHandler(this.TweakChemDoodle_Load);
            this.groupBoxExplicit.ResumeLayout(false);
            this.groupBoxExplicit.PerformLayout();
            this.groupBoxImplicit.ResumeLayout(false);
            this.groupBoxImplicit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBondLength)).EndInit();
            this.groupBoxBondLength.ResumeLayout(false);
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
        private System.Windows.Forms.NumericUpDown nudBondLength;
        private System.Windows.Forms.GroupBox groupBoxBondLength;
        private System.Windows.Forms.Button btnFlip;
        private System.Windows.Forms.Button btnMirror;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private System.Windows.Forms.CheckBox chkColouredAtoms;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}