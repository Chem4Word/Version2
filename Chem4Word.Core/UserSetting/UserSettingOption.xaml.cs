// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;

namespace Chem4Word.Core.UserSetting
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UserSettingOption
    {
        private readonly string settingfile;

        /// <summary>
        /// Initializes a new instance of the UserSettingOption class.
        /// </summary>
        public UserSettingOption(string settingFile)
        {
            InitializeComponent();

            this.settingfile = settingFile;

            switch (Setting.Import)
            {
                case ImportSetting.Auto:
                    this.ImportOption.SelectedItem = this.importComboBoxItemAuto;
                    break;
                case ImportSetting.Prompt:
                    this.ImportOption.SelectedItem = this.importComboBoxItemPrompt;
                    break;
                case ImportSetting.StrictFail:
                    this.ImportOption.SelectedItem = this.importComboBoxItemFail;
                    break;
            }

            switch (Setting.DocumentPreferedDepiction)
            {
                case DocPreferedDepiction.TwoD:
                    this.documentPreferedDepiction.SelectedItem = this.documentPreferedDepiction2D;
                    break;
                case DocPreferedDepiction.InlineFormula:
                    this.documentPreferedDepiction.SelectedItem = this.documentPreferedDepictionInline;
                    break;
                case DocPreferedDepiction.ConciseFormula:
                    this.documentPreferedDepiction.SelectedItem = this.documentPreferedDepictionConcise;
                    break;
                case DocPreferedDepiction.ChemicalName:
                    this.documentPreferedDepiction.SelectedItem = this.documentPreferedDepictionName;
                    break;
                case DocPreferedDepiction.BoldNumber:
                    this.documentPreferedDepiction.SelectedItem = this.documentPreferedDepictionBold;
                    break;
            }

            switch (Setting.NavigatorPreferedDepiction)
            {
                case NavPreferedDepiction.BoldNumber:
                    this.NavigatorPreferedDepiction.SelectedItem = this.navigatorPreferedDepictionBold;
                    break;
                case NavPreferedDepiction.ChemicalName:
                    this.NavigatorPreferedDepiction.SelectedItem = this.navigatorPreferedDepictionName;
                    break;
                case NavPreferedDepiction.ConciseFormula:
                    this.NavigatorPreferedDepiction.SelectedItem = this.navigatorPreferedDepictionConcise;
                    break;
                case NavPreferedDepiction.InlineFormula:
                    this.NavigatorPreferedDepiction.SelectedItem = this.navigatorPreferedDepictionInline;
                    break;
            }
            this.checkBox1.IsChecked = Setting.CollapseNavigatorDepiction;
        }

        private void documentPreferedDepictionMouseMove(object sender, MouseEventArgs e)
        {
            // mouseover pop up text
            // show example?
        }

        private void documentPreferedDepictionConciseMouseMove(object sender, MouseEventArgs e)
        {}

        private void documentPreferedDepictionInlineMouseMove(object sender, MouseEventArgs e)
        {}

        private void documentPreferedDepictionNameMouseMove(object sender, MouseEventArgs e)
        {}

        private void documentPreferedDepictionBoldMouseMove(object sender, MouseEventArgs e)
        {}

        private void navigatorPreferedDepictionConciseMouseMove(object sender, MouseEventArgs e)
        {}

        private void navigatorPreferedDepictionInlineMouseMove(object sender, MouseEventArgs e)
        {}

        private void navigatorPreferedDepictionNameMouseMove(object sender, MouseEventArgs e)
        {}

        private void navigatorPreferedDepictionBoldMouseMove(object sender, MouseEventArgs e)
        {}

        private void importAutoMouseMove(object sender, MouseEventArgs e)
        {}

        private void importPromptMouseMove(object sender, MouseEventArgs e)
        {}

        private void importFailMouseMove(object sender, MouseEventArgs e)
        {}

        private void btnCancelClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }

        private void btnOKClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            /// set Import Option
            if (this.ImportOption.SelectedItem == this.importComboBoxItemAuto)
            {
                Setting.Import = ImportSetting.Auto;
            }
            else if (this.ImportOption.SelectedItem == this.importComboBoxItemPrompt)
            {
                Setting.Import = ImportSetting.Prompt;
            }
            else if (this.ImportOption.SelectedItem == this.importComboBoxItemFail)
            {
                Setting.Import = ImportSetting.StrictFail;
            }

            /// set Document Prefered Depiction
            if (this.documentPreferedDepiction.SelectedItem == this.documentPreferedDepiction2D)
            {
                Setting.DocumentPreferedDepiction = DocPreferedDepiction.TwoD;
            }
            else if (this.documentPreferedDepiction.SelectedItem == this.documentPreferedDepictionInline)
            {
                Setting.DocumentPreferedDepiction = DocPreferedDepiction.InlineFormula;
            }
            else if (this.documentPreferedDepiction.SelectedItem == this.documentPreferedDepictionConcise)
            {
                Setting.DocumentPreferedDepiction = DocPreferedDepiction.ConciseFormula;
            }
            else if (this.documentPreferedDepiction.SelectedItem == this.documentPreferedDepictionName)
            {
                Setting.DocumentPreferedDepiction = DocPreferedDepiction.ChemicalName;
            }
            else if (this.documentPreferedDepiction.SelectedItem == this.documentPreferedDepictionBold)
            {
                Setting.DocumentPreferedDepiction = DocPreferedDepiction.BoldNumber;
            }

            /// set Navigator Prefered Depiction
            if (this.NavigatorPreferedDepiction.SelectedItem == this.navigatorPreferedDepictionBold)
            {
                Setting.NavigatorPreferedDepiction = NavPreferedDepiction.BoldNumber;
            }
            else if (this.NavigatorPreferedDepiction.SelectedItem == this.navigatorPreferedDepictionName)
            {
                Setting.NavigatorPreferedDepiction = NavPreferedDepiction.ChemicalName;
            }
            else if (this.NavigatorPreferedDepiction.SelectedItem == this.navigatorPreferedDepictionConcise)
            {
                Setting.NavigatorPreferedDepiction = NavPreferedDepiction.ConciseFormula;
            }
            else if (this.NavigatorPreferedDepiction.SelectedItem == this.navigatorPreferedDepictionInline)
            {
                Setting.NavigatorPreferedDepiction = NavPreferedDepiction.InlineFormula;
            }

            // set collapse navigator option
            Setting.CollapseNavigatorDepiction = (checkBox1.IsChecked == true);

            /// Write to Setting File
            using (XmlWriter xw = XmlWriter.Create(settingfile))
            {
                XDocument userSetting = new XDocument(new XElement("userSetting",
                                                                   new XElement("importOption",
                                                                                new XAttribute("value",
                                                                                               Setting.
                                                                                                   Import.ToString())),
                                                                   new XElement("documentPreferedDepiction",
                                                                                new XAttribute("value",
                                                                                               Setting.
                                                                                                   DocumentPreferedDepiction
                                                                                                   .ToString())),
                                                                   new XElement("navigatorPreferedDepiction",
                                                                                new XAttribute("value",
                                                                                               Setting.
                                                                                                   NavigatorPreferedDepiction
                                                                                                   .ToString())),
                                                                                                   new XElement("collapseNavigatorDepiction",
                                                                                new XAttribute("value",
                                                                                               Setting.
                                                                                                   CollapseNavigatorDepiction
                                                                                                   .ToString()))

                                                          ));
                userSetting.WriteTo(xw);
            }

            this.Hide();
        }
    }
}