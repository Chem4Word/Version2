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
    public partial class UpdateFailure : Form
    {
        public string WebPage { get; set; }

        public System.Windows.Point TopLeft { get; set; }

        public UpdateFailure()
        {
            InitializeComponent();
        }

        private void UpdateFailure_Load(object sender, EventArgs e)
        {
            if (TopLeft.X != 0 && TopLeft.Y != 0)
            {
                Left = (int)TopLeft.X;
                Top = (int)TopLeft.Y;
            }

            webBrowser1.DocumentText = WebPage;
        }
    }
}
