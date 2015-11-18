// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using Chem4Word.Api;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.UI.ManageView
{
    /// <summary>
    /// Interaction logic for EditLabels.xaml
    /// </summary>
    public partial class EditLabels
    {
        private readonly List<LabelItem> deletedItems;
        private readonly Dictionary<DepictionOption, ICollection<IChemistryZone>> documentDepictionOptionsInUse;
        private readonly Dictionary<DepictionOption, ICollection<IChemistryZone>> navigatorDepictionOptionsInUse;

        /// <summary>
        /// The context object associated with this edit labels
        /// </summary>
        /// <see cref="Numbo.Coa.ContextObject"/>
        private ContextObject contextObject;

        /// <summary>
        /// default constructore - mandatory for WPF controls so they 
        /// can be used in XAML
        /// </summary>
        public EditLabels()
        {
            InitializeComponent();
        }

        ///<summary>
        ///</summary>
        /// <param name="contextObject"></param>
        /// <param name="documentDepictionOptionsInUse"></param>
        /// <param name="navigatorDepictionOptionsInUse"></param>
        /// /// <param name="showEvaluate"></param>
        public EditLabels(ContextObject contextObject,
                          Dictionary<DepictionOption, ICollection<IChemistryZone>> documentDepictionOptionsInUse,
                          Dictionary<DepictionOption, ICollection<IChemistryZone>> navigatorDepictionOptionsInUse,
                          bool showEvaluate)
            : this()
        {
            this.documentDepictionOptionsInUse = documentDepictionOptionsInUse;
            this.navigatorDepictionOptionsInUse = navigatorDepictionOptionsInUse;
            deletedItems = new List<LabelItem>();

            if (!showEvaluate)
            {
                ShowEvaluateText.Visibility = System.Windows.Visibility.Hidden;
            }

            if (contextObject == null)
            {
                CmlCml cml = new CmlCml();
//                cml.DelegateElement.Add();
                XDocument cmlXDocument = new XDocument(cml.DelegateElement);
                contextObject = new ContextObject(cmlXDocument);
            }

            ContextObject = contextObject;

            IEnumerable<DepictionOption> depictions = Depiction.PossibleDepictionOptions(this.contextObject);
            foreach (DepictionOption depictionOption in depictions)
            {
                if (Depiction.Is2D(depictionOption) || Depiction.IsConciseFormula(depictionOption))
                {
                    // we don't show 2D formula at the moment
                    if (Depiction.IsConciseFormula(depictionOption))
                    {
                        // Show (last if ore than one) concise formula in window title
                        this.Title = "Edit labels for '" + depictionOption.HumanUnderstandableOption + "'";
                    }
                }
                else
                {
                    bool inUse;

                    if (this.documentDepictionOptionsInUse == null || this.navigatorDepictionOptionsInUse == null)
                    {
                        inUse = true;
                    }
                    else
                    {
                        inUse = (this.documentDepictionOptionsInUse.ContainsKey(depictionOption) ||
                                 this.navigatorDepictionOptionsInUse.ContainsKey(depictionOption));
                    }
                    var item = new LabelItem(ContextObject, depictionOption, inUse);
                    item.PostDelete += LabelItemAfterDelete;
                    item.PostUpdate += ItemAfterUpdate;
                    item.Click += ItemClick;
                    item.DepictionOptionChanged += DepictionOptionChanged;
                    labelsStack.Children.Add(item);
                }
            }
        }

        public ContextObject ContextObject
        {
            get { return contextObject; }
            private set { contextObject = value; }
        }

        /// <summary>
        /// Handles the ItemUpdate event - runs through the 
        /// LabelItems and calculates whether or not the OK
        /// button should enabled (only enabled when all 
        /// the items are valid)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemAfterUpdate(object sender, EventArgs e)
        {
            bool valid = true;
            foreach (LabelItem item in labelsStack.Children)
            {
                if (!item.IsValid)
                {
                    valid = false;
                    break;
                }
            }
            ok.IsEnabled = valid;
        }

        private void LabelItemAfterDelete(object sender, EventArgs e)
        {
            var labelItem = (LabelItem) sender;
            deletedItems.Add(labelItem);
            labelsStack.Children.Remove(labelItem);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Hide();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            AddNewLabel();
        }

        public void AddNewLabel()
        {
            var item = new LabelItem(ContextObject, null, false);
            item.PostDelete += LabelItemAfterDelete;
            item.PostUpdate += ItemAfterUpdate;
            item.Click += ItemClick;
            item.DepictionOptionChanged += DepictionOptionChanged;
            labelsStack.Children.Add(item);
            ok.IsEnabled = false;
            item.Editing = true;
        }

        private void RemoveDeletedItems()
        {
            foreach (LabelItem labelItem in deletedItems)
            {
                DepictionOption depictionOption = labelItem.DepictionOption;
                if (Depiction.IsFormula(depictionOption))
                {
                    ((XAttribute) depictionOption.MachineUnderstandableOption).Remove();
                }
                else if (Depiction.IsLabel(depictionOption))
                {
                    ((XElement) depictionOption.MachineUnderstandableOption).Remove();
                }
                else if (Depiction.IsName(depictionOption))
                {
                    ((XElement) depictionOption.MachineUnderstandableOption).Remove();
                }
            }
        }

        private void MarkItemsAsNotOutdated()
        {
            foreach (LabelItem item in labelsStack.Children)
            {
                DepictionOption depictionOption = item.DepictionOption;
                if (Depiction.IsFormula(depictionOption))
                {
                    CmlFormula formula = new CmlFormula(depictionOption.MachineUnderstandableOption.Parent);
                    if (CmlFormula.IsValidInlineValue(item.textBox.Text))
                    {
                        formula.Inline = item.textBox.Text;
                    }
                    formula.RemoveOutdated();
                }
                else if (Depiction.IsLabel(depictionOption))
                {
                    CmlLabel label = new CmlLabel((XElement) depictionOption.MachineUnderstandableOption);
                    if (CmlLabel.IsLabelValueValid(item.textBox.Text))
                    {
                        label.Value = item.textBox.Text;
                    }
                    label.RemoveOutdated();
                }
                else if (Depiction.IsName(depictionOption))
                {
                    var name = new CmlName((XElement) depictionOption.MachineUnderstandableOption);
                    if (CmlName.IsValidNameValue(item.textBox.Text))
                    {
                        name.SetValue(item.textBox.Text);
                    }
                    name.RemoveOutdated();
                }
            }
        }

        private void UpdateInUseDocumentDepictionOptions()
        {
            // run through all the in use document depiction options and update them
            foreach (var keyValuePair in documentDepictionOptionsInUse)
            {
                var depictionOption = keyValuePair.Key;
                depictionOption.RecalculateAbsoluteXPath();

                foreach (var chemistryZone in keyValuePair.Value)
                {
                    var newChemistryZoneProperties = chemistryZone.Properties.Clone();
                    newChemistryZoneProperties.SetDocumentDepictionOption(depictionOption);
                    chemistryZone.Properties = newChemistryZoneProperties;
                }
            }
        }

        private void UpdateInUseNavigatorDepictionOptions()
        {
            // run through all the in use navigator depiction options and update them
            foreach (var keyValuePair in navigatorDepictionOptionsInUse)
            {
                var depictionOption = keyValuePair.Key;
                depictionOption.RecalculateAbsoluteXPath();
                foreach (var chemistryZone in keyValuePair.Value)
                {
                    var newChemistryZoneProperties = chemistryZone.Properties.Clone();
                    newChemistryZoneProperties.SetNavigatorDepictionOption(depictionOption);
                    chemistryZone.Properties = newChemistryZoneProperties;
                }
            }
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            RemoveDeletedItems();
            MarkItemsAsNotOutdated();
            if (documentDepictionOptionsInUse == null || navigatorDepictionOptionsInUse == null)
            {
                // do nothing as we can't go through the in use depictions                
            }
            else
            {
                UpdateInUseDocumentDepictionOptions();
                UpdateInUseNavigatorDepictionOptions();
            }
            DialogResult = true;
            Hide();
        }

        /// <summary>
        /// Handles the event of the mouse clicking anywhere off the labels 
        /// this means that the labels are no longer being edited so iterate through
        /// all the labels and make them inactive (Mode.Normal)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (LabelItem item in labelsStack.Children)
            {
                item.Editing = false;
            }
        }

        /// <summary>
        /// Handles the event caused by clicking on a label item (anywhere)
        /// 
        /// Goes through all the other LabelItems and sets them to being inactive
        /// (Mode.Normal) (only one LabelItem can be edited at any one time).
        /// </summary>
        /// <param name="sender">the LabelItem clicked on</param>
        /// <param name="e"></param>
        private void ItemClick(object sender, EventArgs e)
        {
            LabelItem senderItem = (LabelItem) sender;
            foreach (LabelItem item in labelsStack.Children)
            {
                if (item != senderItem)
                {
                    item.Editing = false;
                }
            }
        }

        private void DepictionOptionChanged(object sender, DepictionOptionEventArgs e)
        {
            if (e.OldDepictionOption != null)
            {
                ICollection<IChemistryZone> chemistryZones;
                if (documentDepictionOptionsInUse.TryGetValue(e.OldDepictionOption, out chemistryZones))
                {
                    documentDepictionOptionsInUse.Remove(e.OldDepictionOption);
                    documentDepictionOptionsInUse.Add(e.NewDepictionOption, chemistryZones);
                }
                if (navigatorDepictionOptionsInUse.TryGetValue(e.OldDepictionOption, out chemistryZones))
                {
                    navigatorDepictionOptionsInUse.Remove(e.OldDepictionOption);
                    navigatorDepictionOptionsInUse.Add(e.NewDepictionOption, chemistryZones);
                }
            }
        }
    }
}