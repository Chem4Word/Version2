// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Numbo.Cml;

namespace Chem4Word.UI.BoldNumberOrganiser
{
    /// <summary>
    /// Interaction logic for EditSetRemoveBoldNumberLabelControl.xaml
    /// </summary>
    public partial class EditSetRemoveBoldNumberLabelControl : INotifyPropertyChanged
    {
        private readonly List<string> currentlyUsedLabelvalues;
        private string boldNumberLabel;
        private bool isValidLabelValue;
        private string oldLabelValue;

        public EditSetRemoveBoldNumberLabelControl()
        {
            InitializeComponent();
        }

        public EditSetRemoveBoldNumberLabelControl(string currentLabelValue, string suggestedNewLabelValue,
                                                   List<string> boldNumberLabelValuesInUse) : this()
        {
            InitializeComponent();
            OldLabelValue = currentLabelValue;
            BoldNumberLabel = suggestedNewLabelValue;
            currentlyUsedLabelvalues = boldNumberLabelValuesInUse ?? new List<string>(0);
        }

        public string OldLabelValue
        {
            get { return oldLabelValue; }
            private set
            {
                oldLabelValue = value;
                OnPropertyChanged("OldLabelValue");
            }
        }

        public string BoldNumberLabel
        {
            get { return boldNumberLabel; }
            private set
            {
                boldNumberLabel = value;
                OnPropertyChanged("BoldNumberLabel");
                IsValidLabelValue = (CmlLabel.IsLabelValueValid(BoldNumberLabel) &&
                                     !currentlyUsedLabelvalues.Contains(BoldNumberLabel));
            }
        }

        public bool IsValidLabelValue
        {
            get { return isValidLabelValue; }
            private set
            {
                isValidLabelValue = value;
                OnPropertyChanged("IsValidLabelValue");
            }
        }

        private void txtEditLabel_KeyUp(object sender, KeyEventArgs e)
        {
            IsValidLabelValue = (CmlLabel.IsLabelValueValid(BoldNumberLabel) &&
                                 !currentlyUsedLabelvalues.Contains(BoldNumberLabel));
            if (Key.Return.Equals(e.Key))
            {
                ((TextBox) sender).MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                e.Handled = true;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}