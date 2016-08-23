// -----------------------------------------------------------------------
//  Copyright (c) Microsoft and the University of Cambridge. All rights reserved. 
//  This code is licensed under the Apache License, Version 2.0.
//  THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, 
//  INCLUDING ANY IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// -----------------------------------------------------------------------

namespace Chem4Word.AddIn.Navigator
{
    partial class ChemistryNavigatorHostControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.innerUI = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // innerUI
            // 
            this.innerUI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.innerUI.Location = new System.Drawing.Point(0, 0);
            this.innerUI.Name = "innerUI";
            this.innerUI.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.innerUI.Size = new System.Drawing.Size(250, 216);
            this.innerUI.TabIndex = 0;
            this.innerUI.Text = "elementHost1";
            this.innerUI.Child = null;
            // 
            // ChemistryNavigatorHostControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.innerUI);
            this.MinimumSize = new System.Drawing.Size(250, 0);
            this.Name = "ChemistryNavigatorHostControl";
            this.Size = new System.Drawing.Size(250, 216);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Integration.ElementHost innerUI;
    }
}
