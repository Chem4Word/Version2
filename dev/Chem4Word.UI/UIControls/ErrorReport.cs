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

        public ErrorReport(Telemetry telemetry, string operation, string exceptionMessage)
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
