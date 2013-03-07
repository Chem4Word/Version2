// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Xml.Linq;
using Chem4Word.UI.Converters;

namespace Chem4Word.UI.CmlViewer
{
    /// <summary>
    /// Interaction logic for Viewer.xaml
    /// </summary>
    public partial class Viewer
    {
        private readonly PrettyXmlConverter converter = new PrettyXmlConverter();

        public Viewer()
        {
            InitializeComponent();
        }

        public Viewer(XDocument xDocument) : this()
        {
            reader.Document = converter.Render(xDocument.Root);
        }
    }
}