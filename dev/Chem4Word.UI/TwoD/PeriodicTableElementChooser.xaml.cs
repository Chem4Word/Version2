// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Numbo.Cml.Helpers;
using CMLHelpers = Numbo.Cml.Helpers;

namespace Chem4Word.UI.TwoD
{
    /// <summary>
    /// Interaction logic for PeriodicTableElementChooser.xaml
    /// </summary>
    public partial class PeriodicTableElementChooser
    {
        /// <summary>
        /// Display a PeriodicTable so the user can choose any element
        /// </summary>
        public PeriodicTableElementChooser()
        {
            InitializeComponent();
            Dictionary<PeriodicTable.Element,
                ChemicalElement> dictionary = Numbo.Cml.Helpers.PeriodicTable.GetPeriodicTableIndexedByElement();
            foreach (KeyValuePair<PeriodicTable.Element, ChemicalElement> kvp in dictionary)
            {
                PeriodicTable.Element element = kvp.Key;
                ChemicalElement chemicalElement = kvp.Value;

                int gridRow;
                int gridCol;
                LinearGradientBrush brush;

                if (chemicalElement.AtomicNumber >= 58 && chemicalElement.AtomicNumber <= 71)
                {
                    /// lanthanide
                    gridRow = 7;
                    gridCol = chemicalElement.AtomicNumber - 58 + 3;
                    brush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.BlueViolet, 45) {Opacity = 0.5};
                }
                else if (chemicalElement.AtomicNumber >= 90 && chemicalElement.AtomicNumber <= 103)
                {
                    /// actinide
                    gridRow = 8;
                    gridCol = chemicalElement.AtomicNumber - 90 + 3;
                    brush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.Pink, 45) {Opacity = 0.5};
                }
                else if (Numbo.Cml.Helpers.PeriodicTable.Element.R.Equals(element))
                {
                    /// an R 
                    /// lets put this in the middle of a special row at the bottom
                    brush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.WhiteSmoke, 45)
                                {Opacity = 0.5};

                    gridCol = 0;
                    gridRow = 8;
                }
                else if (Numbo.Cml.Helpers.PeriodicTable.Element.Du.Equals(element))
                {
                    /// a Dummy
                    /// lets put this in the middle of a special row at the bottom
                    brush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.WhiteSmoke, 45)
                                {Opacity = 0.5};
                    gridCol = 1;
                    gridRow = 8;
                }
                else
                {
                    /// something else
                    int group = Numbo.Cml.Helpers.PeriodicTable.GetGroup(element);
                    int row = Numbo.Cml.Helpers.PeriodicTable.GetRow(element);
                    gridCol = group - 1;
                    gridRow = row;
                    brush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.WhiteSmoke, 45) {Opacity = 0.5};
                    if (row == 0 && group == 1)
                    {
                        /// hydrogen
                        brush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.LightGreen, 45) {Opacity = 0.5};
                    }
                    else if (group <= 2)
                    {
                        /// (earth) alkali metals
                        brush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.Purple, 45) {Opacity = 0.5};
                    }
                    else if (group == 18)
                    {
                        /// noble gases
                        brush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.Green, 45) {Opacity = 0.5};
                    }
                    else if (group >= 13 && group <= 17)
                    {
                        /// p block
                        if (row == 1 && group >= 15)
                        {
                            /// Nitrogen to Ne
                            brush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.Green, 45) {Opacity = 0.5};
                        }
                        else if (row == 2 && group >= 16)
                        {
                            /// Cl to Ar
                            brush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.Green, 45) {Opacity = 0.5};
                        }
                        else
                        {
                            brush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.LightBlue, 45) {Opacity = 0.5};
                        }
                    }
                    else if (group >= 3 && group <= 12)
                    {
                        /// d-block
                        brush = new LinearGradientBrush(Colors.WhiteSmoke, Colors.OrangeRed, 45) {Opacity = 0.5};
                    }
                    /// other wise 
                    /// not something we deal with so do nothing
                    
                }

                if (gridCol >= 0 && gridRow >= 0)
                {
                    Button button = new Button
                                        {
                                            Content = string.Format(CultureInfo.InvariantCulture, "{0}", element),
                                            FontSize = 12,
                                            FontWeight = FontWeights.Bold,
                                            Name = string.Format(CultureInfo.InvariantCulture, "{0}", element),
                                            Width = 30,
                                            Height = 30,
                                            Foreground = Brushes.Black,
                                        };
                    button.Click += ButtonMouseClick;
                    button.Background = brush;
                    PeriodicTable.Children.Add(button);
                    Grid.SetColumn(button, gridCol);
                    Grid.SetRow(button, gridRow);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PeriodicTable.Element Element { get; private set; }

        public event ElementSelectedEventHandler ElementSelected;

        private void ButtonMouseClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button) sender;
            Element = Numbo.Cml.Helpers.PeriodicTable.GetElement(b.Name);
            e.Handled = true;

            OnElementSelected();
        }

        private void OnElementSelected()
        {
            if (ElementSelected != null)
            {
                ElementSelected(Element);
            }
        }
    }

    public delegate void ElementSelectedEventHandler(PeriodicTable.Element element);
}