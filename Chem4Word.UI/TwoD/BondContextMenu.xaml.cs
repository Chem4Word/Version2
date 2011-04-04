// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Chem4Word.UI.TwoD
{
    /// <summary>
    /// Interaction logic for BondContextMenu.xaml
    /// </summary>
    public partial class BondContextMenu
    {
        private const string Custom = "Custom";
        private const string Divider = "-------";
        private const string SelectBondGroup = "Select bond group";
        private const string SelectBondOrder = "Select bond order";
        private const string SelectBondStereo = "Select bond stereo";

        public BondContextMenu()
        {
            InitializeComponent();
        }

        public event BondOrderSelectedEventHandler BondOrderSelected;
        public event BondOrderSelectedEventHandler CustomBondOrderSelected;
        public event BondSteroSelectedEventHandler BondStereoSelected;
        public event BondGroupSelectedEventHandler BondGroupSelected;

        public void SetBondOptions(IEnumerable<string> bondOrders, IEnumerable<string> bondStereos,
                                   IEnumerable<string> bondGroups)
        {
            SetBondOrderOptions(bondOrders);
            SetBondStereoOptions(bondStereos);
            SetBondGroupOptions(bondGroups);
        }

        private void SetBondOrderOptions(IEnumerable<string> bondOrders)
        {
            bondOrderComboBox.Items.Clear();
            bondOrderComboBox.Items.Add(SelectBondOrder);

            foreach (string order in bondOrders)
            {
                bondOrderComboBox.Items.Add(order);
            }

            bondOrderComboBox.Items.Add(Divider);
            bondOrderComboBox.Items.Add(Custom);

            bondOrderComboBox.SelectedIndex = 0;
        }

        private void SetBondStereoOptions(IEnumerable<string> bondStereos)
        {
            bondStereoComboBox.Items.Clear();
            bondStereoComboBox.Items.Add(SelectBondStereo);

            foreach (string stereo in bondStereos)
            {
                bondStereoComboBox.Items.Add(stereo);
            }

            bondStereoComboBox.IsEnabled = bondStereoComboBox.Items.Count > 1;
            bondStereoComboBox.SelectedIndex = 0;
        }

        private void SetBondGroupOptions(IEnumerable<string> bondGroups)
        {
            bondGroupComboBox.Items.Clear();
            bondGroupComboBox.Items.Add(SelectBondGroup);

            foreach (string group in bondGroups)
            {
                bondGroupComboBox.Items.Add(group);
            }

            bondGroupComboBox.IsEnabled = bondGroupComboBox.Items.Count > 1;
            bondGroupComboBox.SelectedIndex = 0;
        }

        private void OnBondOrderSelected(string order)
        {
            if (BondOrderSelected != null)
            {
                BondOrderSelected(order);
            }
        }

        private void OnCustomBondOrderSelected(string order)
        {
            if (CustomBondOrderSelected != null)
            {
                CustomBondOrderSelected(order);
            }
        }

        private void OnBondStereoSelected(string stereo)
        {
            if (BondStereoSelected != null)
            {
                BondStereoSelected(stereo);
            }
        }

        private void OnBondGroupSelected(string group)
        {
            if (BondGroupSelected != null)
            {
                BondGroupSelected(group);
            }
        }

        private void bondOrderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bondOrderComboBox.SelectedItem == null || bondOrderComboBox.SelectedItem.Equals(SelectBondOrder))
            {
                return;
            }
            if (bondOrderComboBox.SelectedItem.Equals(Custom))
            {
                AddLabelWindow dlg = new AddLabelWindow {Title = "Set Bond Order", Owner = Window.GetWindow(this)};

                dlg.ShowDialog();

                if (dlg.DialogResult == true)
                {
                    OnCustomBondOrderSelected(dlg.LabelText);
                }

                return;
            }

            OnBondOrderSelected(bondOrderComboBox.SelectedItem.ToString());
        }

        private void bondStereoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bondStereoComboBox.SelectedItem == null || bondStereoComboBox.SelectedItem.Equals(SelectBondStereo))
            {
                return;
            }

            OnBondStereoSelected(bondStereoComboBox.SelectedItem.ToString());
        }

        private void bondGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bondGroupComboBox.SelectedItem == null || bondGroupComboBox.SelectedItem.Equals(SelectBondGroup))
            {
                return;
            }

            OnBondGroupSelected(bondGroupComboBox.SelectedItem.ToString());
        }
    }

    public delegate void BondOrderSelectedEventHandler(string order);

    public delegate void BondSteroSelectedEventHandler(string type);

    public delegate void BondGroupSelectedEventHandler(string group);
}