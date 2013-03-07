// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Windows;

namespace Chem4Word.UI.Import
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ImportFailedControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ImportFailedControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets brief description of the message window.
        /// </summary>
        public string BriefDescription
        {
            get { return this.briefDescription.Text; }
            set { briefDescription.Text = value; }
        }

        /// <summary>
        /// Sets long description of the message window.
        /// </summary>
        public string LongDescription
        {
            get { return this.longDescription.Text; }
            set { longDescription.Text = value; }
        }

        /// <summary>
        /// Handle event click of the OK button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}