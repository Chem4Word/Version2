﻿namespace Chem4Word.UI.UIControls
{
    partial class AutomaticUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutomaticUpdate));
            this.btnUpdateNow = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnUpdateLater = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.linkCodeplexPage = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnUpdateNow
            // 
            this.btnUpdateNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateNow.Location = new System.Drawing.Point(557, 358);
            this.btnUpdateNow.Name = "btnUpdateNow";
            this.btnUpdateNow.Size = new System.Drawing.Size(86, 23);
            this.btnUpdateNow.TabIndex = 0;
            this.btnUpdateNow.Text = "Update Now";
            this.btnUpdateNow.UseVisualStyleBackColor = true;
            this.btnUpdateNow.Click += new System.EventHandler(this.btnUpdateNow_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 36);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(631, 294);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(103, 13);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "Update available";
            // 
            // btnUpdateLater
            // 
            this.btnUpdateLater.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateLater.Location = new System.Drawing.Point(465, 358);
            this.btnUpdateLater.Name = "btnUpdateLater";
            this.btnUpdateLater.Size = new System.Drawing.Size(86, 23);
            this.btnUpdateLater.TabIndex = 3;
            this.btnUpdateLater.Text = "Update Later";
            this.btnUpdateLater.UseVisualStyleBackColor = true;
            this.btnUpdateLater.Click += new System.EventHandler(this.btnUpdateLater_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 361);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(447, 19);
            this.progressBar1.TabIndex = 4;
            // 
            // linkCodeplexPage
            // 
            this.linkCodeplexPage.AutoSize = true;
            this.linkCodeplexPage.Location = new System.Drawing.Point(12, 342);
            this.linkCodeplexPage.Name = "linkCodeplexPage";
            this.linkCodeplexPage.Size = new System.Drawing.Size(360, 13);
            this.linkCodeplexPage.TabIndex = 5;
            this.linkCodeplexPage.TabStop = true;
            this.linkCodeplexPage.Text = "Click here to download from the CodePlex web site if automatic update fails";
            this.linkCodeplexPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkCodeplexPage_LinkClicked);
            // 
            // AutomaticUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 393);
            this.Controls.Add(this.linkCodeplexPage);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnUpdateLater);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnUpdateNow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutomaticUpdate";
            this.Text = "Update Available";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutomaticUpdate_FormClosing);
            this.Load += new System.EventHandler(this.AutomaticUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateNow;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnUpdateLater;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.LinkLabel linkCodeplexPage;
    }
}