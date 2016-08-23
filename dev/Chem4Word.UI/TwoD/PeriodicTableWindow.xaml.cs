// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chem4Word.UI.TwoD
{
    /// <summary>
    /// Interaction logic for PeriodicTableWindow.xaml
    /// </summary>
    public partial class PeriodicTableWindow : Window
    {
        public PeriodicTableWindow()
        {
            InitializeComponent();
        }

        public Numbo.Cml.Helpers.PeriodicTable.Element SelectedElement
        {
            get; private set;
        }

        private void PeriodicTableElementChooser_ElementSelected(Numbo.Cml.Helpers.PeriodicTable.Element element)
        {
            SelectedElement = element;
            DialogResult = true;
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
