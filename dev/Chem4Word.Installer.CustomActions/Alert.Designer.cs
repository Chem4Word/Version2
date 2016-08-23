namespace CustomInstaller
{
    partial class Alert
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.applicationList = new System.Windows.Forms.ListBox();
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnAutoClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(28, 22);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(347, 13);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "The following applications should be closed before continuing the install:";
            // 
            // applicationList
            // 
            this.applicationList.FormattingEnabled = true;
            this.applicationList.HorizontalScrollbar = true;
            this.applicationList.Location = new System.Drawing.Point(31, 54);
            this.applicationList.Name = "applicationList";
            this.applicationList.Size = new System.Drawing.Size(344, 108);
            this.applicationList.TabIndex = 1;
            // 
            // btnRetry
            // 
            this.btnRetry.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnRetry.Location = new System.Drawing.Point(75, 177);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(114, 28);
            this.btnRetry.TabIndex = 2;
            this.btnRetry.Text = "&Try Again";
            this.btnRetry.UseVisualStyleBackColor = true;
            // 
            // btnAutoClose
            // 
            this.btnAutoClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAutoClose.Location = new System.Drawing.Point(211, 177);
            this.btnAutoClose.Name = "btnAutoClose";
            this.btnAutoClose.Size = new System.Drawing.Size(114, 28);
            this.btnAutoClose.TabIndex = 3;
            this.btnAutoClose.Text = "&Close without Save";
            this.btnAutoClose.UseVisualStyleBackColor = true;
            // 
            // Alert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 226);
            this.ControlBox = false;
            this.Controls.Add(this.btnAutoClose);
            this.Controls.Add(this.btnRetry);
            this.Controls.Add(this.applicationList);
            this.Controls.Add(this.lblMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Alert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chemistry Add-in for Word";
            this.Load += new System.EventHandler(this.Alert_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ListBox applicationList;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Button btnAutoClose;

    }
}