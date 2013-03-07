// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Numbo.Cml;

namespace Chem4Word.UI.TwoD
{
    /// <summary>
    /// Interaction logic for AddLabelWindow.xaml
    /// </summary>
    public partial class AddLabelWindow
    {
        public AddLabelWindow()
        {
            InitializeComponent();
        }

        #region LabelText

        /// <summary>
        /// LabelText Dependency Property
        /// </summary>
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof (string), typeof (AddLabelWindow),
                                        new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the LabelText property.  This dependency property 
        /// indicates ....
        /// </summary>
        public string LabelText
        {
            get { return (string) GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        public void SelectAll()
        {
            textBox.SelectAll();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Return)
            {
                e.Handled = true;
                if (!Validation.GetHasError(textBox))
                {
                    DialogResult = true;
                    Close();
                }
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            textBox.Focus();
            textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }

    public class LabelValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null && CmlLabel.IsLabelValueValid(value.ToString()))
            {
                return ValidationResult.ValidResult;
            }

            return new ValidationResult(false,
                                        "Label must not contain any spaces and its length must be greater than zero");
        }
    }
}