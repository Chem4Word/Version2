// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using Chem4Word.Api;
using Chem4Word.Api.Events;
using Numbo.Coa;
using MessageBox=System.Windows.Forms.MessageBox;

namespace Chem4Word.UI.Navigator
{
    /// <summary>
    /// Interaction logic for ChemistryNavigator.xaml
    /// </summary>
    public partial class ChemistryNavigator
    {
        private IChemistryZone selectedItem;

        public ChemistryNavigator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the ChemistryNavigator class.
        /// </summary>
        public ChemistryNavigator(IChemistryDocument document) : this()
        {
            ChemistryDocument = document;
            // Register Events
            document.ContentControlAfterAdd += DocumentContentControlAfterAdd;
            document.ContentControlAfterDelete += DocumentContentControlAfterDelete;
            document.ContentControlOnEnter += DocumentContentControlOnEnter;
            document.ContentControlOnExit += DocumentContentControlOnExit;
            document.Core.ContentControlSelectionChanged += DocumentContentControlSelectionChanged;
            RefreshItems();
        }

        public IChemistryDocument ChemistryDocument { get; private set; }

        /// <summary>
        /// Gets or Sets a selected navigator item.
        /// </summary>
        private IChemistryZone SelectedItem
        {
            get { return selectedItem; }
            set
            {
                try
                {
                    if (selectedItem != value)
                    {
                        selectedItem = value;
                        DeselectAll();
                        foreach (NavigatorItem item in DepictionsStack.Children)
                        {
                            if (item.Source == value)
                            {
                                item.IsSelected = true;
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SelectedItem\n" + ex.Message);
                }
            }
        }

        private void DocumentContentControlSelectionChanged(object sender, ContentControlEventArgs e)
        {
            // Check if the current document is the active document, so that it interested to react the Selection_Change event
            if (ChemistryDocument == ChemistryDocument.Core.ActiveChemistryDocument)
            {
                // if a Chemistry Zone was selected,
                // e.Action = True, otherwise False
                SelectedItem = e.Action ? ChemistryDocument.SelectedChemistryZone : null;
            }
        }

        private void DocumentContentControlOnExit(object sender, ContentControlEventArgs e)
        {
            selectedItem = null;
            DeselectAll();
        }

        private void DocumentContentControlOnEnter(object sender, ContentControlEventArgs e)
        {
            SelectedItem = e.ContentChemistryZone;
        }

        private void DocumentContentControlAfterDelete(object sender, ContentControlEventArgs e)
        {
            RefreshItems();
        }

        private void DocumentContentControlAfterAdd(object sender, ContentControlEventArgs e)
        {
            RefreshItems();
        }

        private void NavigatorItemSelectedChanged(object sender, RoutedEventArgs e)
        {
            NavigatorItem item = sender as NavigatorItem;
            if (item != null)
            {
                SelectedItem = item.Source;
                item.Source.Choose();
            }
        }

        /// <summary>
        /// Clear all navigator items.
        /// </summary>
        private void Clear()
        {
            DepictionsStack.Children.Clear();
        }

        private void RefreshItems()
        {
            Clear();
            foreach (IChemistryZone chemZone in ChemistryDocument)
            {
                if (chemZone.Cml != null)
                {
                    MemberAdd(chemZone);
                }
            }
        }

        /// <summary>
        /// Add a new item to the Navigator Custom Control
        /// </summary>
        /// <param name="member"> IChemistryZone to be added</param>
        private void MemberAdd(IChemistryZone member)
        {
            NavigatorItem item = new NavigatorItem(member) {Width = 235};
            DepictionsStack.Children.Add(item);

            item.SelectedChanged += NavigatorItemSelectedChanged;
            item.InsertLinkAs += ItemInsertLinkAs;
            item.InsertCopyAs += ItemInsertCopyAs;
        }

        private void ItemInsertCopyAs(object sender, DepictionOptionEventArgs e)
        {
            NavigatorItem item = sender as NavigatorItem;
            if (item != null)
            {
                ChemistryZoneProperties chemistryZoneProperties = item.ChemistryZone.Properties;
                chemistryZoneProperties.SetDocumentDepictionOption(e.NewDepictionOption);
                ChemistryDocument.Core.CreateCopy(item.Source, chemistryZoneProperties);
            }
        }

        private void ItemInsertLinkAs(object sender, DepictionOptionEventArgs e)
        {
            NavigatorItem item = sender as NavigatorItem;
            if (item != null)
            {
                ChemistryZoneProperties chemistryZoneProperties = item.ChemistryZone.Properties;
                chemistryZoneProperties.SetDocumentDepictionOption(e.NewDepictionOption);
                ChemistryDocument.Core.CreateLink(item.Source, chemistryZoneProperties);
            }
        }

        private void DeselectAll()
        {
            foreach (NavigatorItem item in DepictionsStack.Children)
            {
                item.IsSelected = false;
            }
        }

        private void ExpandCollapseAllNavigatorDepictionsClick(object sender, RoutedEventArgs e)
        {
            if (DepictionsStack != null) {
                var expander = sender as Expander;
                if (expander != null) {
                    var collapase = !expander.IsExpanded;
                    foreach (NavigatorItem item in DepictionsStack.Children)
                    {
                        item.CollapseNavigatorDepiction = collapase;
                    }    
                }
            }
        }
    }
}