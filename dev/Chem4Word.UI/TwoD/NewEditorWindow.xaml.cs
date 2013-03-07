// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using Chem4Word.Api;
using Chem4Word.UI.Tools;
using Numbo.Cml;
using Numbo.Cml.Helpers;
using Numbo.Coa;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Chem4Word.UI.TwoD
{
    /// <summary>
    /// Interaction logic for NewEditorWindow.xaml
    /// </summary>
    public partial class NewEditorWindow
    {
        private const string More = "More...";
        private int groupStartIndex = int.MaxValue;
        private String mainDepictionXPath;
        private String formulaDepictionXPath;
        
        public NewEditorWindow()
        {
            this.InitializeComponent();
        }

        public NewEditorWindow(IChemistryZone chemistryZone, ContextObject contextObject, XObject moleculePointer)
            : this()
        {
            CmlMolecule molecule = (moleculePointer != null)
                                       ? new CmlMolecule((XElement) moleculePointer)
                                       : CmlUtils.GetFirstDescendentMolecule(contextObject.Cml);

            mainDepictionXPath = chemistryZone.Properties.NavigatorDepictionOptionXPath;

            var depictionOptions = Depiction.PossibleDepictionOptions(contextObject, molecule.DelegateElement);
            foreach (var depictionOption in
                depictionOptions.Where(Depiction.IsConciseFormula))
            {
                formulaDepictionXPath = depictionOption.AbsoluteXPathToDepictionOption;
                break;
            }
            _editor.Canvas.Refreshed += Canvas_Refreshed;
            _editor.SetData(contextObject, molecule);
            _editor.chemCanvas.AtomTypeOptionsUpdated += ChemCanvasAtomTypeOptionsUpdated;
            _editor.chemCanvas.IsotopeOptionsUpdated += chemCanvas_IsotopeOptionsUpdated;

        }

        void Canvas_Refreshed(object sender, EventArgs e)
        {
            if (mainDepictionXPath != null)
            {
                DepictionOption mainDepiction = DepictionOption.CreateDepictionOption(_editor.chemCanvas.ContextObject.Cml, mainDepictionXPath);
                MainText = mainDepiction.GetAsLatexFormattedString();
            }
            if (formulaDepictionXPath != null)
            {
                DepictionOption formulaDepiction = DepictionOption.CreateDepictionOption(_editor.chemCanvas.ContextObject.Cml, formulaDepictionXPath);
                FormulaText = formulaDepiction.GetAsLatexFormattedString();
            }
        }

       

        private void PeriodicTableElementSelected(PeriodicTable.Element el)
        {
           
        }

        void chemCanvas_IsotopeOptionsUpdated(System.Collections.Generic.IEnumerable<int> isotopes)
        {
            IsotopeOptionsCombo.Items.Clear();
            IsotopeOptionsCombo.Items.Clear();
            IsotopeOptionsCombo.IsDropDownOpen = false;

            IsotopeOptionsCombo.IsEnabled = false;

            IsotopeOptionsCombo.Items.Add("Set Isotope");

            if (isotopes != null)
            {
                IsotopeOptionsCombo.IsEnabled = true;
                foreach (int i in isotopes)
                {
                    IsotopeOptionsCombo.Items.Add(i);
                }
            }

            IsotopeOptionsCombo.Items.Add("-----");
            IsotopeOptionsCombo.Items.Add("Custom");

            IsotopeOptionsCombo.SelectedItem = "Set Isotope";
        }

        void ChemCanvasAtomTypeOptionsUpdated(System.Collections.Generic.HashSet<Numbo.Cml.Helpers.PeriodicTable.Element> options, System.Collections.Generic.IEnumerable<string> groups)
        {
            AtomTypeComboBox.Items.Clear();
            AtomTypeComboBox.IsDropDownOpen = false;

            AtomTypeComboBox.IsEnabled = false;

            AtomTypeComboBox.Items.Add("Change Atom Type");
            //AtomTypeComboBox.Items.Add(new RibbonComboBoxItem {Content = "Change Atom Type"});

            if (options != null)
            {
                AtomTypeComboBox.IsEnabled = true;
                foreach (var set in options)
                {
                    AtomTypeComboBox.Items.Add(set.ToString());
                    //AtomTypeComboBox.Items.Add(new RibbonComboBoxItem { Content = set.ToString() });
                }
            }

            AtomTypeComboBox.Items.Add("-----");
            //AtomTypeComboBox.Items.Add(new RibbonComboBoxItem { Content = "-----" });
            AtomTypeComboBox.Items.Add(More);
            //AtomTypeComboBox.Items.Add(new RibbonComboBoxItem { Content = More });

            if (groups != null)
            {
                AtomTypeComboBox.Items.Add("-----");
                //AtomTypeComboBox.Items.Add(new RibbonComboBoxItem { Content = "-----" });
                groupStartIndex = AtomTypeComboBox.Items.Add("Select a group");
                //groupStartIndex = AtomTypeComboBox.Items.Add(new RibbonComboBoxItem { Content = "Select a group" });

                foreach (string s in groups)
                {
                    AtomTypeComboBox.Items.Add(s);
                    //AtomTypeComboBox.Items.Add(new RibbonComboBoxItem { Content = s });
                }
            }
            else
            {
                groupStartIndex = int.MaxValue;
            }

            AtomTypeComboBox.SelectedIndex = 0;
        }

        #region Public Methods

        public CanvasContainer GetEditor()
        {
            return _editor;
        }


        ///<summary>
        ///</summary>
        public void CreatePng()
        {
            if (_editor != null)
            {
                _editor.GeneratePng(false);
            }
        }

        #endregion Public Methods

        #region Public Members

        /// <summary>
        /// Gets or Sets display text.
        /// </summary>
        private string MainText
        {
            get { return mainTextDepiciton.Text; }
            set
            {
                mainTextDepiciton.Inlines.Clear();
                TextTools.ConvertLatexFormattedStringToTextBlock(ref mainTextDepiciton, value);
            }
        }

        /// <summary>
        /// Gets or Sets display text.
        /// </summary>
        private string FormulaText
        {
            get { return formulaTextDepiction.Text; }
            set
            {
                formulaTextDepiction.Inlines.Clear();
                TextTools.ConvertLatexFormattedStringToTextBlock(ref formulaTextDepiction, value);
            }
        }

        /// <summary>
        /// Gets PNG file name
        /// </summary>
        public string PngFileName
        {
            get { return _editor != null ? _editor.PngFileOutput : string.Empty; }
        }

        /// <summary>
        /// Gets output CML, formatted in XDocument class
        /// </summary>
        public XDocument CmlOutput
        {
            get { return _editor != null ? _editor.CmlOutPut : null; }
        }

        public ContextObject ContextObject {
            get { return _editor != null ? _editor.ContextObject : null; }
        }

        ///<summary>
        ///</summary>
        public bool ConnectionTableChanged
        {
            get { return _editor.ConnectionTableChanged; }
        }

        #endregion Public Members

        #region Rotate Command

        internal void Rotate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.RotateExecuted(sender, e);
        }

        internal void RotateSelection_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.RotateSelectionCanExecute(sender, e);
            }
        }

        #endregion

        #region Delete Selection Command

        private void DeleteSelection_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.DeleteSelectionCanExecute(sender, e);
            }
        }

        private void DeleteSelection_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.DeleteSelectionExecuted(sender, e);
        }

        #endregion

        #region Flip Horizontal Command

        private void FlipHorizontal_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.FlipHorizontalCanExecute(sender, e);
            }
        }

        private void FlipHorizontal_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.FlipHorizontalExecuted(sender, e);
        }

        #endregion

        #region Flip Vertical Command

        private void FlipVertical_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.FlipVerticalExecuted(sender, e);
        }

        private void FlipVertical_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.FlipVerticalCanExecute(sender, e);
            }
        }

        #endregion

        #region Add and remove electron commands

        private void AddRemoveElectron_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.AddRemoveElectronCanExecute(sender, e);
            }
        }

        private void AddElectron_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.AddElectronExecuted(sender, e);
        }

        private void RemoveElectron_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.RemoveElectronExecuted(sender, e);
        }

        #endregion

        #region Add Label Command

        private void AddLabel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.AddLabelCanExecute(sender, e);
            }
        }

        private void AddLabel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.AddLabelExecuted(sender, e);
        }

        #endregion

        #region Remove Label Command

        private void RemoveLabel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.RemoveLabelCanExecute(sender, e);
            }
        }

        private void RemoveLabel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.RemoveLabelExecuted(sender, e);
        }

        #endregion

        #region Flip Selection Command

        /// <summary>
        /// PMR changed this to new signature
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlipSelection_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.FlipSelectionCanExecute(sender, e);
            }
        }

        private void FlipSelection_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.FlipSelectionExecuted(sender, e);
        }

        #endregion

        #region Remove Isotope Command

        private void RemoveIsotope_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.RemoveIsotopeCanExecute(sender, e);
            }
        }

        private void RemoveIsotope_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.RemoveIsotopeExecuted(sender, e);
        }

        #endregion

        #region Set Charge Command

        private void SetCharge_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.SetChargeCanExecute(sender, e);
            }
        }

        private void SetCharge_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.SetChargeExecuted(sender, e);
        }

        #endregion

        #region Remove Charge Command

        private void RemoveCharge_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.RemoveChargeCanExecute(sender, e);
            }
        }

        private void RemoveCharge_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.RemoveChargeExecuted(sender, e);
        }

        #endregion

        #region Add Proton Command

        private void AddProton_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.AddProtonCanExecute(sender, e);
            }
        }

        private void AddProton_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.AddProtonExecuted(sender, e);
        }

        #endregion

        #region Remove Proton

        private void RemoveProton_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.RemoveProtonCanExecute(sender, e);
            }
        }

        private void RemoveProton_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.RemoveProtonExecuted(sender, e);
        }

        #endregion

        #region Add Hydrogen Dot Command

        internal void AddHydrogenDot_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.AddHydrogenDotCanExecute(sender, e);
            }
        }

        internal void AddHydrogenDot_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.AddHydrogenDotExecuted(sender, e);
        }

        #endregion

        #region Remove Hydrogen Dot Command

        private void RemoveHydrogenDot_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.RemoveHydrogenDotCanExecute(sender, e);
            }
        }

        private void RemoveHydrogenDot_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.RemoveHydrogenDotExecuted(sender, e);
        }

        #endregion

        #region Select All Command

        private void SelectAll_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.SelectAllExecuted(sender, e);
        }

        #endregion

        #region Undo/Redo Commands

        private void Undo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.UndoCanExecute(sender, e);
            }
        }

        private void Undo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.UndoExecuted(sender, e);
        }

        private void Redo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_editor == null)
            {
                e.CanExecute = false;
            }
            else
            {
                _editor.Canvas.RedoCanExecute(sender, e);
            }
        }

        private void Redo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _editor.Canvas.RedoExecuted(sender, e);
        }

        #endregion

        #region Save Command

        private ContextObject lastSavedContext;

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _editor != null && _editor.chemCanvas.IsDirty;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lastSavedContext = _editor.Canvas.Save();
        }

        #endregion

        private void RibbonWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_editor.chemCanvas.IsDirty)
            {
                DialogResult result = MessageBox.Show("Would you like to save your changes?", "Save changes?",
                                                  MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                                  MessageBoxDefaultButton.Button3);

                switch (result)
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case System.Windows.Forms.DialogResult.Yes:
                        DialogResult = true;
                        Save_Executed(null, null);
                        break;
                    case System.Windows.Forms.DialogResult.No:
                        if (lastSavedContext != null)
                        {
                            _editor.Canvas.ContextObject = lastSavedContext;
                            _editor.Canvas.molecule = CmlUtils.GetFirstDescendentMolecule(lastSavedContext.Cml);
                            _editor.Canvas.Refresh();
                            _editor.Canvas.InvalidateAll();
                            DialogResult = true;
                        }
                        else
                        {
                            DialogResult = false;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                DialogResult = true;
            }
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lastSavedContext != null)
            {
                _editor.Canvas.ContextObject = lastSavedContext;
                _editor.Canvas.molecule = CmlUtils.GetFirstDescendentMolecule(lastSavedContext.Cml);
                _editor.Canvas.Refresh();
                _editor.Canvas.InvalidateAll();
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }

            Close();
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Save_Executed(sender, null);
            DialogResult = true;
            Close();
        }

        private void AtomTypeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (AtomTypeComboBox.SelectedItem == null)
            {
                return;
            }

            var s = AtomTypeComboBox.SelectedItem.ToString();
            //var s = item.Content as string;

            if (AtomTypeComboBox.SelectedIndex > groupStartIndex)
            {
                _editor.Canvas.ChemGroupSelected(s);
                return;
            }

            if (s != "Change Atom Type" && s != "Select a group" && s != "-----" && s != More &&
                !string.IsNullOrEmpty(s))
            {
                _editor.Canvas.ChemAtomTypeSelected(s);
                AtomTypeComboBox.SelectedIndex = 0;
            }
            else if (s == More)
            {
                //IsPeriodicTableShown = true;
                PeriodicTableWindow dlg = new PeriodicTableWindow();

                if (dlg.ShowDialog().Value == true)
                {
                    _editor.Canvas.ChemContextMenuCustomAtomTypeSelected(dlg.SelectedElement.ToString());
                }
                AtomTypeComboBox.SelectedIndex = 0;
            }
            else if (s == "-----")
            {
                AtomTypeComboBox.SelectedIndex = 0;
                //IsPeriodicTableShown = false;
            }
        }

        private void IsotopeOptionsCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (IsotopeOptionsCombo.SelectedItem != null)
            {
                if (IsotopeOptionsCombo.SelectedItem is string)
                {
                    if (IsotopeOptionsCombo.SelectedItem.ToString() == "Custom")
                    {
                        AddLabelWindow dlg = new AddLabelWindow
                        {
                            Title = "Set Isotope",
                            Owner = System.Windows.Window.GetWindow(this)
                        };

                        dlg.ShowDialog();

                        if (dlg.DialogResult == true)
                        {
                            int result;
                            bool isValid = int.TryParse(dlg.LabelText, out result);

                            if (isValid)
                            {
                                _editor.Canvas.ChemContextMenuIsotopeSelected(result);
                            }
                        }
                    }
                }
                else if (IsotopeOptionsCombo.SelectedItem != null)
                {
                    int result = (int)(IsotopeOptionsCombo.SelectedItem);

                    _editor.Canvas.ChemContextMenuIsotopeSelected(result);
                }
            }
        }


        //private bool _isPeriodicTableShown;

        //public bool IsPeriodicTableShown
        //{
        //    get { return _isPeriodicTableShown; }
        //    set
        //    {
        //        _isPeriodicTableShown = value;
        //        periodicTable.IsHitTestVisible = value;

        //        if (_isPeriodicTableShown)
        //        {
        //            DoubleAnimation da = new DoubleAnimation(1.0, new Duration(TimeSpan.FromMilliseconds(300)));
        //            periodicTable.BeginAnimation(PeriodicTableElementChooser.OpacityProperty, da);
        //        }
        //        else
        //        {
        //            DoubleAnimation da = new DoubleAnimation(0.0, new Duration(TimeSpan.FromMilliseconds(300)));
        //            periodicTable.BeginAnimation(PeriodicTableElementChooser.OpacityProperty, da);
        //        }
        //    }
        //}
    }
}