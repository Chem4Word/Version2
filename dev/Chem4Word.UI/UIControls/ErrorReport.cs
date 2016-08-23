using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Chem4Word.Common;

namespace Chem4Word.UI.UIControls
{
    public partial class ErrorReport : Form
    {

        private Telemetry _telemetry;
        private string _exceptionMessage = string.Empty;
        private string _operation = string.Empty;

        public System.Windows.Point TopLeft { get; set; }

        public ErrorReport(Telemetry telemetry, System.Windows.Point TopLeft, string operation, string exceptionMessage)
        {
            InitializeComponent();

            try
            {
                _telemetry = telemetry;
                _operation = operation;
                _exceptionMessage = exceptionMessage;
            }
            catch (Exception)
            {
                // Do Nothing
            }
        }

        private void ErrorReport_Load(object sender, EventArgs e)
        {
            if (TopLeft.X != 0 && TopLeft.Y != 0)
            {
                Left = (int)TopLeft.X;
                Top = (int)TopLeft.Y;
            }
            
            try
            {
                textBox1.Text = _exceptionMessage;

            }
            catch (Exception)
            {
                // Do Nothing
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string answer = richTextBox1.Text;

                if (!string.IsNullOrEmpty(answer))
                {
                    _telemetry.Write(_operation, "Exception(Data)", richTextBox1.Text);
                }
            }
            catch (Exception)
            {
                // Do Nothing
            }
            finally
            {
                Close();
            }
        }

    }
}
