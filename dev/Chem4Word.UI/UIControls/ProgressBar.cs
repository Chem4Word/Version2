using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Chem4Word.UI.UIControls
{
    public partial class ProgressBar : Form
    {
        public int Value
        {
            get { return progressBar1.Value; }
            set
            {
                if (value >= progressBar1.Minimum && value <= progressBar1.Maximum)
                {
                    progressBar1.Value = value;
                }
            }
        }

        public int Minimum
        {
            get { return progressBar1.Minimum; }
            set
            {
                if (value >= 0)
                {
                    progressBar1.Minimum = value;
                }
            }
        }

        public int Maximum
        {
            get { return progressBar1.Maximum; }
            set
            {
                if (value > 0)
                {
                    progressBar1.Maximum = value;
                }
            }
        }

        public string Message
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public ProgressBar()
        {
            InitializeComponent();
        }

        public void Increment(int value)
        {
            progressBar1.Increment(value);
            progressBar1.Refresh();
            this.Refresh();
            Application.DoEvents();
            Debug.WriteLine(progressBar1.Value + "/" + progressBar1.Maximum);
            Thread.Sleep(5);
        }
    }
}
