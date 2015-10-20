using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            get { return textBox1.Text; }
            set
            {
                textBox1.Text = value;
                textBox1.SelectionStart = 0;
                textBox1.SelectionLength = 0;
            }
        }

        public ProgressBar()
        {
            InitializeComponent();
        }

        public void Increment(int value)
        {
            if (progressBar1.Value + value < progressBar1.Maximum)
            {
                progressBar1.Increment(value);
                progressBar1.Refresh();
            }
            Application.DoEvents();
        }
    }
}
