// Created by Mike Williams - 19/10/2015
// 
// -----------------------------------------------------------------------
//   Copyright (c) 2015, The Outercurve Foundation.  
//   This software is released under the Apache License, Version 2.0. 
//   The license and further copyright text can be found in the file LICENSE.TXT at
//   the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private const int CP_NOCLOSE_BUTTON = 0x200;

        public System.Windows.Point TopLeft { get; set; }

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
            //Debug.WriteLine(progressBar1.Value + "/" + progressBar1.Maximum);
            Thread.Sleep(5);
        }

        // Disable Close Button
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void ProgressBar_Load(object sender, EventArgs e)
        {
            if (TopLeft.X != 0 && TopLeft.Y != 0)
            {
                Left = (int)TopLeft.X;
                Top = (int)TopLeft.Y;
            }
        }
    }
}
