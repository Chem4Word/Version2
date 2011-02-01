// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Windows.Forms;
using Chem4Word.UI.TwoD;

namespace Chem4Word.UI
{
    public partial class DocumentHostControl : UserControl
    {
        private string customXmlId;

        public DocumentHostControl()
        {
            InitializeComponent();

            // Control name have to be unique.
            this.Name = Guid.NewGuid().ToString();
        }

        public string CustomXMLId
        {
            get { return this.customXmlId; }
            set { this.customXmlId = value; }
        }

        public CanvasContainer InnerCanvasContainer
        {
            get { return this.chemistryCanvas1; }
        }
    }
}