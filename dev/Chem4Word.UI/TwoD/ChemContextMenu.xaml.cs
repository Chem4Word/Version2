// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Numbo.Cml.Helpers;

namespace Chem4Word.UI.TwoD
{
    /// <summary>
    /// Interaction logic for ChemContextMenu.xaml
    /// </summary>
    public partial class ChemContextMenu
    {
        private readonly string More = "More...";
        private bool isPeriodicTableShown;
        private int groupStartIndex = int.MaxValue;

        public ChemContextMenu()
        {
            InitializeComponent();
        }

        public ChemCanvas ParentCanvas { get; set; }

        public bool IsPeriodicTableShown
        {
            get { return isPeriodicTableShown; }
            set
            {
                isPeriodicTableShown = value;
                periodicTable.IsHitTestVisible = value;

                if (isPeriodicTableShown)
                {
                    DoubleAnimation da = new DoubleAnimation(1.0, new Duration(TimeSpan.FromMilliseconds(300)));
                    periodicTable.BeginAnimation(OpacityProperty, da);
                }
                else
                {
                    DoubleAnimation da = new DoubleAnimation(0.0, new Duration(TimeSpan.FromMilliseconds(300)));
                    periodicTable.BeginAnimation(OpacityProperty, da);
                }
            }
        }

        public event AtomTypeSelectedEventHandler AtomTypeSelected;
        public event AtomTypeSelectedEventHandler CustomAtomTypeSelected;
        public event GroupTypeSelectedEventHandler GroupSelected;
        public event ChargeSelectedEventHandler ChargeSelected;
        public event ChargeSelectedEventHandler CustomChargeSelected;
        public event IsotopeSelectedEventHandler IsotopeSelected;
        public event IsotopeSelectedEventHandler CustomIsotopeSelected;

        public void SetAtomOptions(HashSet<PeriodicTable.Element> options, IEnumerable<string> groups)
        {
            comboAtomType.Items.Clear();
            comboAtomType.IsDropDownOpen = false;

            comboAtomType.IsEnabled = false;

            comboAtomType.Items.Add("Change Atom Type");

            if (options != null)
            {
                comboAtomType.IsEnabled = true;
                foreach (PeriodicTable.Element set in options)
                {
                    comboAtomType.Items.Add(set.ToString());
                }
            }

            comboAtomType.Items.Add("-----");
            comboAtomType.Items.Add(More);

            if (groups != null)
            {
                comboAtomType.Items.Add("-----");

                groupStartIndex = comboAtomType.Items.Add("Select a group");

                foreach (string s in groups)
                {
                    comboAtomType.Items.Add(s);
                }
            }
            else
            {
                groupStartIndex = int.MaxValue;
            }

            comboAtomType.SelectedItem = "Change Atom Type";
        }

        public void SetIsotopeOptions(IEnumerable<int> isotopes)
        {
            comboIsotope.Items.Clear();
            comboIsotope.IsDropDownOpen = false;

            comboIsotope.IsEnabled = false;

            comboIsotope.Items.Add("Set Isotope");

            if (isotopes != null)
            {
                comboIsotope.IsEnabled = true;
                foreach (int i in isotopes)
                {
                    comboIsotope.Items.Add(i);
                }
            }

            comboIsotope.Items.Add("-----");
            comboIsotope.Items.Add("Custom");

            comboIsotope.SelectedItem = "Set Isotope";
        }

        private void comboAtomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AtomTypeSelected != null)
            {
                var s = comboAtomType.SelectedItem as string;

                if (comboAtomType.SelectedIndex > groupStartIndex && GroupSelected != null)
                {
                    GroupSelected(s);
                    return;
                }

                if (s != "Change Atom Type" && s != "Select a group" && s != "-----" && s != More &&
                    !string.IsNullOrEmpty(s))
                {
                    IsPeriodicTableShown = false;
                    AtomTypeSelected(s);
                    comboAtomType.SelectedIndex = 0;
                }
                else if (s == More)
                {
                    IsPeriodicTableShown = true;
                }
                else if (s == "-----")
                {
                    comboAtomType.SelectedIndex = 0;

                    IsPeriodicTableShown = false;
                }
            }
        }

        private void comboIsotope_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsotopeSelected != null)
            {
                if (comboIsotope.SelectedItem is string)
                {
                    if (comboIsotope.SelectedItem.ToString() == "Custom")
                    {
                        AddLabelWindow dlg = new AddLabelWindow
                                                 {
                                                     Title = "Set Isotope",
                                                     Owner = Window.GetWindow(this)
                                                 };

                        dlg.ShowDialog();

                        if (dlg.DialogResult == true)
                        {
                            int result;
                            bool isValid = int.TryParse(dlg.LabelText, out result);

                            if (isValid)
                            {
                                CustomIsotopeSelected(result);
                            }
                        }
                    }
                }
                else if (comboIsotope.SelectedItem != null)
                {
                    int result = (int) (comboIsotope.SelectedItem);

                    IsotopeSelected(result);
                }
            }
        }

        private void PeriodicTableElementSelected(PeriodicTable.Element el)
        {
            if (CustomAtomTypeSelected != null)
            {
                CustomAtomTypeSelected(el.ToString());
            }
        }
    }

    public delegate void AtomTypeSelectedEventHandler(string atomType);

    public delegate void GroupTypeSelectedEventHandler(string groupType);

    public delegate void ChargeSelectedEventHandler(int charge);

    public delegate void IsotopeSelectedEventHandler(int isotope);
}