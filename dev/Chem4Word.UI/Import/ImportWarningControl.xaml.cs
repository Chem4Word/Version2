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

namespace Chem4Word.UI.Import
{
    /// <summary>
    /// Interaction logic for ImportWarningControl.xaml
    /// </summary>
    public partial class ImportWarningControl
    {
        private readonly List<ImportWarning> selectedWarnings;

        #region constructor

        /// <summary>
        /// Initializes a new instance of the ImportWarningContorl class with
        /// which will contain the supplied warnings.
        /// </summary>
        /// <param name="importWarnings">the warnings to display</param>
        public ImportWarningControl(IEnumerable<ImportWarning> importWarnings)
        {
            InitializeComponent();
            selectedWarnings = new List<ImportWarning>();
            bool tic = true;
            foreach (ImportWarning iw in importWarnings)
            {
                Brush color = tic ? Brushes.White : Brushes.WhiteSmoke;
                tic = !tic;
                CheckBox checkBox = CreateListViewItem(iw, color);
                WarningsStack.Children.Add(checkBox);
                checkBox.Checked += CheckBoxChecked;
                checkBox.Unchecked += CheckBoxUnchecked;
            }
            // Register Events
            okButton.Click += OkButtonClick;
            cancelButton.Click += CancelButtonClick;
        }

        #endregion

        #region properties

        /// <summary>
        /// The warnings which the user has selected to fix.
        /// </summary>
        public List<ImportWarning> SelectedImportWarnings
        {
            get { return selectedWarnings; }
        }

        #endregion

        #region event handlers

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

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Visibility = Visibility.Hidden;
        }

        private void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox) sender;
            selectedWarnings.Add(checkBox.Tag as ImportWarning);
        }

        private void CheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox) sender;
            selectedWarnings.Remove(checkBox.Tag as ImportWarning);
        }

        private void SelectDeselectAllCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox checkBox in WarningsStack.Children)
            {
                checkBox.IsChecked = true;
            }
        }

        private void SelectDeselectAllCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox checkBox in WarningsStack.Children)
            {
                checkBox.IsChecked = false;
            }
        }

        private void InvertSelectionButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox checkBox in WarningsStack.Children)
            {
                checkBox.IsChecked = !checkBox.IsChecked;
            }
        }

        #endregion

        #region private methods

        /// <summary>
        /// Generate an instance of radio button to represent the given DepictionOption.
        /// </summary>
        /// <param name="importWarning"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        private static CheckBox CreateListViewItem(ImportWarning importWarning, Brush background)
        {
            CheckBox checkBox = new CheckBox {Tag = importWarning};
            Grid grid = new Grid {Width = 293, Background = background};
            ColumnDefinition colm1 = new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)};
            ColumnDefinition colm2 = new ColumnDefinition {Width = new GridLength(109, GridUnitType.Pixel)};
            grid.ColumnDefinitions.Add(colm1);
            grid.ColumnDefinitions.Add(colm2);
            RowDefinition row1 = new RowDefinition();
            grid.RowDefinitions.Add(row1);
            TextBlock textBlk = new TextBlock
                                    {
                                        Text = importWarning.Message,
                                        TextAlignment = TextAlignment.Left,
                                        TextWrapping = TextWrapping.Wrap
                                    };
            grid.Children.Add(textBlk);
            Grid.SetColumn(textBlk, 0);
            checkBox.Content = grid;
            checkBox.ToolTip = importWarning.Message;
            return checkBox;
        }

        #endregion
    }
}