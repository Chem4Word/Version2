// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using Chem4Word.UI.Tools;
using Numbo.Coa;

namespace Chem4Word.UI.ManageView
{
    /// <summary>
    /// Interaction logic for ManageDepictionOption.xaml
    /// </summary>
    public partial class ManageViewOption
    {
        private const string DefaultExplanation = "Please select from the following available depictions";
        private DepictionOption selectedDepiction;

        ///<summary>
        /// Creates the UI to allow the user to choose a DepictionOption based on 
        /// the contents of the contextObject with the specified reason
        ///</summary>
        ///<param name="contextObject">the ContextObject containing the CML to depict</param>
        ///<param name="reason">the reason why the user is being prompted for this view option (default is used if this is null or empty)</param>
        public ManageViewOption(ContextObject contextObject, string reason) :
            this(contextObject, contextObject.Cml.Root, reason)
        {
            // call through to main constructor
        }

        ///<summary>
        /// Creates the UI to allow the user to choose a DepictionOption based on 
        /// the contents of the contextObject below the eldestElement with the specified reason
        ///</summary>
        ///<param name="contextObject">the ContextObject containing the CML to depict</param>
        ///<param name="eldestElement">the eldest element to look for DepictionOptions below</param>
        ///<param name="reason">the reason why the user is being prompted for this view option (default is used if this is null or empty)</param>
        public ManageViewOption(ContextObject contextObject, XElement eldestElement, string reason)
        {
            InitializeComponent();
            if (reason != null)
            {
                if (string.Empty.Equals(reason))
                {
                    reason = DefaultExplanation;
                }
            }
            Explanation.Text = reason;
            IEnumerable<DepictionOption> depictionOptions = Depiction.PossibleDepictionOptions(contextObject,
                                                                                               eldestElement);
            bool tic = true;
            foreach (DepictionOption depiction in depictionOptions)
            {
                Brush color = tic ? Brushes.White : Brushes.WhiteSmoke;
                tic = !tic;
                RadioButton radio = CreateListViewItem(depiction, color);
                DepictionsStack.Children.Add(radio);
                radio.Checked += RadioChecked;
            }
            // Register Events
            okButton.Click += OkButtonClick;
        }

        /// <summary>
        /// Set/Get currently selected depiction option
        /// </summary>
        public DepictionOption SelectedDepiction
        {
            get { return selectedDepiction; }
            set
            {
                foreach (RadioButton radio in DepictionsStack.Children)
                {
                    if ((DepictionOption) radio.Tag == value)
                    {
                        radio.IsChecked = true;
                        break;
                    }
                }
                selectedDepiction = value;
            }
        }

        /// <summary>
        /// Handle event checked of radio button (Depiction Options).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioChecked(object sender, RoutedEventArgs e)
        {
            selectedDepiction = ((RadioButton) sender).Tag as DepictionOption;
        }

        /// <summary>
        /// Handle event click of OK button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Generate an instance of radio button to represent the given DepictionOption.
        /// </summary>
        /// <param name="depiction"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        private static RadioButton CreateListViewItem(DepictionOption depiction, Brush background)
        {
            RadioButton radioButton = new RadioButton
                                          {
                                              GroupName = "depictionGroup",
                                              Tag = depiction,
                                              ToolTip = depiction.DepictionOptionDescription
                                          };

            Grid grid = new Grid {Width = 310, Background = background};

            ColumnDefinition colm1 = new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)};
            ColumnDefinition colm2 = new ColumnDefinition {Width = new GridLength(159, GridUnitType.Pixel)};
            grid.ColumnDefinitions.Add(colm1);
            grid.ColumnDefinitions.Add(colm2);

            TextBlock textBlk = new TextBlock
                                    {
                                        TextAlignment = TextAlignment.Left,
                                        FontSize = 13,
                                    };

            string latex = Depiction.Is2D(depiction)
                               ? Properties.Resources.TWO_D_LABEL
                               : depiction.GetAsLatexFormattedString();
            TextTools.ConvertLatexFormattedStringToTextBlock(ref textBlk, latex);
            grid.Children.Add(textBlk);
            Grid.SetColumn(textBlk, 0);

            TextBlock textType = new TextBlock
                                     {
                                         Text = depiction.DepictionOptionDescription,
                                         Foreground = Brushes.LightBlue,
                                         FontFamily = new FontFamily("Calibri"),
                                         TextAlignment = TextAlignment.Right
                                     };

            grid.Children.Add(textType);
            Grid.SetColumn(textType, 1);

            radioButton.Content = grid;
            return radioButton;
        }
    }
}