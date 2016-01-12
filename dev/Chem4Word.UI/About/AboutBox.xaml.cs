// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Chem4Word.UI.About
{
    /// <summary>
    /// Interaction logic for AboutBox.xaml
    /// </summary>
    public sealed partial class AboutBox
    {
        private string productCopyright;
        private string productVersion;

        public AboutBox()
        {
            InitializeComponent();
        }

        public string GetVersion
        {
            get { return productVersion; }
            set { productVersion = value; }
        }

        public string Getcopyright
        {
            get { return productCopyright; }
            set { productCopyright = value; }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            txt1.Foreground = new SolidColorBrush(Color.FromRgb(102, 102, 102));
            txt2.Foreground = new SolidColorBrush(Color.FromRgb(102, 102, 102));
            txt3.Foreground = new SolidColorBrush(Color.FromRgb(102, 102, 102));
            txtBetalabel.Foreground = new SolidColorBrush(Color.FromRgb(255, 102, 0));
            labelVersion.Text = "V " + productVersion;
            labelCopyright.Text = "©" + productCopyright + " Outercurve Foundation. All rights reserved.";
        }

        private void HyperlinkClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://research.microsoft.com/en-us/projects/chem4word/");
        }

        private void HyperlinkClick1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://wwmm.ch.cam.ac.uk/wikis/wwmm/index.php/Main_Page");
        }

        private void HyperlinkClick2(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.outercurve.org/");
        }

        private void HyperlinkClick3(object sender, RoutedEventArgs e) {
            Process.Start("http://chem4word.codeplex.com/");
        }

        private void HyperlinkClick4(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.facebook.com/home.php?sk=group_186300551397797");
        }

        private void HyperlinkClick5(object sender, RoutedEventArgs e)
        {
            Process.Start("http://web.chemdoodle.com");
        }
        private void HyperlinkClick6(object sender, RoutedEventArgs e)
        {
            Process.Start("http://chem4word.co.uk/");
        }
        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}