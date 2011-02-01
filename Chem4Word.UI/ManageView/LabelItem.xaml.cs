// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using Chem4Word.UI.Tools;
using log4net;
using Numbo;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.UI.ManageView
{
    /// <summary>
    /// Interaction logic for LabelItem.xaml
    /// </summary>
    public partial class LabelItem : INotifyPropertyChanged
    {
        private const string DefaultDisplayText = "Chemical label...";
        private static readonly ILog Log = LogManager.GetLogger(typeof(LabelItem));

        private bool editing;
        private bool inUse;
        private bool isOutdated;
        private string labelText;
        private bool mouseDown;
        private ComboBoxItem oldComboBoxItem;
        private bool valid;

        /// <summary>
        /// Default constructor
        /// </summary>
        public LabelItem()
        {
            InitializeComponent();
        }

        ///<summary>
        ///</summary>
        ///<param name="contextObject"></param>
        ///<param name="depictionOption"></param>
        ///<param name="inUse"></param>
        public LabelItem(ContextObject contextObject, DepictionOption depictionOption, bool inUse)
            : this()
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (Depiction.Is2D(depictionOption))
            {
                throw new ArgumentException("Label items cannot be constructed from 2D depiction options",
                                            "depictionOption");
            }
            if (Depiction.IsConciseFormula(depictionOption))
            {
                throw new ArgumentException(
                    "Label items should not be constructed from concise formula depiction options",
                    "depictionOption");
            }
            ContextObject = contextObject;
            DepictionOption = depictionOption;
            InUse = inUse;
            oldComboBoxItem = null;
            IsValid = true;
            if (depictionOption != null)
            {
                textBlock.Inlines.Clear();
                TextTools.ConvertLatexFormattedStringToTextBlock(ref textBlock,
                                                                 depictionOption.GetAsLatexFormattedString());
                LabelText = depictionOption.GetAsLatexFormattedString();
                if (Depiction.IsFormula(depictionOption))
                {
                    comboBox.SelectedItem = FormulaInline;
                    CmlFormula formula = new CmlFormula(depictionOption.MachineUnderstandableOption.Parent);
                    IsOutdated = formula.IsOutdated();
                }
                else if (Depiction.IsName(depictionOption))
                {
                    CmlName name = new CmlName((XElement)depictionOption.MachineUnderstandableOption);
                    string nameDictPrefix = name.DelegateElement.GetPrefixOfNamespace(CmlConstants.NameDictNS);
                    if (
                        string.Format(CultureInfo.InvariantCulture, "{0}:{1}", nameDictPrefix, "abbreviation").Equals(
                            name.DictRef))
                    {
                        comboBox.SelectedItem = NameAbbreviation;
                    }
                    else if (
                        string.Format(CultureInfo.InvariantCulture, "{0}:{1}", nameDictPrefix, "iupac").Equals(
                            name.DictRef))
                    {
                        comboBox.SelectedItem = NameIupac;
                    }
                    else if (
                        string.Format(CultureInfo.InvariantCulture, "{0}:{1}", nameDictPrefix, "systematic").
                            Equals(name.DictRef))
                    {
                        comboBox.SelectedItem = NameSystematic;
                    }
                    else if (
                        string.Format(CultureInfo.InvariantCulture, "{0}:{1}", nameDictPrefix, "trivial").
                            Equals(name.DictRef))
                    {
                        comboBox.SelectedItem = NameTrivial;
                    }
                    else if (
                        string.Format(CultureInfo.InvariantCulture, "{0}:{1}", nameDictPrefix, "cas").
                            Equals(name.DictRef))
                    {
                        comboBox.SelectedItem = NameCas;
                    }
                    else
                    {
                        /// an unknown name type
                        comboBox.SelectedItem = NameUnknown;
                    }
                    IsOutdated = name.IsOutdated();
                }
                else if (Depiction.IsMoleculeBoldNumberLabel(depictionOption))
                {
                    comboBox.SelectedItem = LabelMoleculeBold;
                    CmlLabel label = new CmlLabel((XElement)depictionOption.MachineUnderstandableOption);
                    IsOutdated = label.IsOutdated();
                }

                mouseDown = false;
                editing = false;
            }

            else
            {
                LabelText = string.Empty;
                comboBox.SelectedItem = Prompt;
                IsValid = false;
            }
            SetDisplayText();
        }

        internal DepictionOption DepictionOption { get; private set; }
        public ContextObject ContextObject { get; private set; }

        public bool InUse
        {
            get { return inUse; }
            private set
            {
                if (inUse != value)
                {
                    inUse = value;
                    OnPropertyChanged("InUse");
                }
            }
        }

        ///<summary>
        ///</summary>
        public bool Editing
        {
            get { return editing; }
            internal set
            {
                if (editing != value)
                {
                    editing = value;
                    if (editing)
                    {
                        FocusHelper.FocusTextBoxCursorSelectAll(textBox);
                        /// we are about to start editing
                        if (Click != null)
                        {
                            Click(this, null);
                        }
                    }
                    else
                    {
                        /// we were editing and are now stopping
                        if (IsValid)
                        {
                            CalculateAndCallCorrectSetMethodIfPossible();
                        }
                        if (PostUpdate != null)
                        {
                            PostUpdate(this, null);
                        }
                        mouseDown = false;
                    }
                    OnPropertyChanged("Editing");
                }
            }
        }

        public bool IsValid
        {
            get { return valid; }
            set
            {
                if (valid != value)
                {
                    valid = value;
                    OnPropertyChanged("IsValid");
                    if (PostUpdate != null)
                    {
                        PostUpdate(this, null);
                    }
                }
            }
        }

        public bool IsOutdated
        {
            get { return isOutdated; }
            set
            {
                if (isOutdated != value)
                {
                    isOutdated = value;
                    OnPropertyChanged("IsOutdated");
                }
            }
        }

        public string LabelText
        {
            get { return labelText; }
            set
            {
                if (labelText != value)
                {
                    labelText = value;
                    SetDisplayText();
                    CalculateAndSetValidity();
                    OnPropertyChanged("LabelText");
                }
            }
        }

        ///<summary>
        /// <see cref="INotifyPropertyChanged"/>
        ///</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Event which is raised before the LabelItem is deleted
        /// </summary>
        public event EventHandler<EventArgs> PostDelete;

        /// <summary>
        /// Event raised after the LabelItem has been updated
        /// </summary>
        public event EventHandler<EventArgs> PostUpdate;

        /// <summary>
        /// Event raised when the LabelItem has been clicked on
        /// </summary>
        public event EventHandler<EventArgs> Click;

        /// <summary>
        /// Event raised after the depiction option is changed
        /// </summary>
        public event EventHandler<DepictionOptionEventArgs> DepictionOptionChanged;

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            if (PostDelete != null)
            {
                PostDelete(this, null);
            }
        }

        private void mouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseDown = true;
        }

        private void mouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (mouseDown)
            {
                Editing = true;
                if (Click != null)
                {
                    Click(this, null);
                }
            }
        }

        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateAndSetValidity();
            CalculateAndCallCorrectSetMethodIfPossible();
            if (PostUpdate != null)
            {
                PostUpdate(this, null);
            }
            if (Click != null)
            {
                Click(this, null);
            }
        }

        private void TextBoxGotFocus(object sender, KeyboardFocusChangedEventArgs keyboardFocusChangedEventArgs)
        {
            if (!Editing)
            {
                ((TextBox)sender).SelectAll();
            }
            Editing = true;
        }

        private void TextBoxLostFocus(object sender, KeyboardFocusChangedEventArgs keyboardFocusChangedEventArgs)
        {
            Editing = false;
        }

        private void txtEditLabel_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateAndSetValidity();
            if (Key.Return.Equals(e.Key))
            {
                Editing = false;
                ((TextBox)sender).MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                e.Handled = true;
            }
        }

        private void SetDisplayText()
        {
            textBlock.Inlines.Clear();
            if (labelText.Length == 0)
            {
                textBlock.Foreground = Brushes.LightSteelBlue;
                textBlock.Inlines.Add(new Run(DefaultDisplayText) { FontStyle = FontStyles.Italic });
            }
            else
            {
                textBlock.Foreground = Brushes.Black;
                TextTools.ConvertLatexFormattedStringToTextBlock(ref textBlock, labelText);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void OnDepictionOptionChanged(DepictionOption oldDepictionOption, DepictionOption newDepictionOption)
        {
            if (DepictionOptionChanged != null)
            {
                DepictionOptionEventArgs eventArgs = new DepictionOptionEventArgs
                {
                    OldDepictionOption = oldDepictionOption,
                    NewDepictionOption = newDepictionOption
                };

                DepictionOptionChanged(this, eventArgs);
            }
        }

        private void CalculateAndSetValidity()
        {
            if (Prompt.IsSelected)
            {
                IsValid = false;
            }
            else if (FormulaInline.IsSelected)
            {
                IsValid = CmlFormula.IsValidInlineValue(LabelText);
            }
            else if (LabelMoleculeBold.IsSelected)
            {
                IsValid = CmlLabel.IsLabelValueValid(LabelText);
            }
            else
            {
                /// must be a name
                IsValid = CmlName.IsValidNameValue(LabelText);
            }
        }

        private void CalculateAndCallCorrectSetMethodIfPossible()
        {
            DepictionOption oldDepictionOption = this.DepictionOption;
            if (Prompt.IsSelected)
            {
                oldComboBoxItem = Prompt;
            }
            else if (FormulaInline.IsSelected)
            {
                if (oldComboBoxItem == null)
                {
                    /// this is just the initial set
                    oldComboBoxItem = FormulaInline;
                }
                else
                {
                    if (IsValid && !FormulaInline.Equals(oldComboBoxItem))
                    {
                        if (Prompt.Equals(oldComboBoxItem))
                        {
                            ChangePromptToInlineFormula();
                        }
                        else if (LabelMoleculeBold.Equals(oldComboBoxItem))
                        {
                            ChangeMoleculeBoldNumberLabelToInlineFormula();
                        }
                        else
                        {
                            /// OldComboBox must be a name
                            ChangeNameToInlineFormula();
                        }
                    }
                }
            }
            else if (LabelMoleculeBold.IsSelected)
            {
                if (oldComboBoxItem == null)
                {
                    /// this is just the initial set
                    oldComboBoxItem = LabelMoleculeBold;
                }
                else
                {
                    if (IsValid && !LabelMoleculeBold.Equals(oldComboBoxItem))
                    {
                        if (Prompt.Equals(oldComboBoxItem))
                        {
                            ChangePromptToMoleculeBoldNumberLabel();
                        }
                        else if (FormulaInline.Equals(oldComboBoxItem))
                        {
                            ChangeInlineFormulaToMoleculeBoldNumberLabel();
                        }
                        else
                        {
                            /// OldComboBox must be a name
                            ChangeNameToMoleculeBoldNumberLabel();
                        }
                    }
                }
            }
            else
            {
                /// must be a name selected

                if (oldComboBoxItem == null)
                {
                    /// this is just the initial set
                    if (NameAbbreviation.IsSelected)
                    {
                        oldComboBoxItem = NameAbbreviation;
                    }
                    else if (NameCas.IsSelected)
                    {
                        oldComboBoxItem = NameCas;
                    }
                    else if (NameIupac.IsSelected)
                    {
                        oldComboBoxItem = NameIupac;
                    }
                    else if (NameSystematic.IsSelected)
                    {
                        oldComboBoxItem = NameSystematic;
                    }
                    else if (NameTrivial.IsSelected)
                    {
                        oldComboBoxItem = NameTrivial;
                    }
                    else
                    {
                        oldComboBoxItem = NameUnknown;
                    }
                }
                else
                {
                    if (IsValid)
                    {
                        if (Prompt.Equals(oldComboBoxItem))
                        {
                            ChangePromptToName();
                        }
                        else if (FormulaInline.Equals(oldComboBoxItem))
                        {
                            ChangeInlineFormulaToName();
                        }
                        else if (LabelMoleculeBold.Equals(oldComboBoxItem))
                        {
                            ChangeMoleculeBoldNumberLabelToName();
                        }
                        else
                        {
                            /// OldComboBox must be a name
                            ChangeNameToName();
                        }
                    }
                }
            }
            if (oldDepictionOption != DepictionOption)
            {
                OnDepictionOptionChanged(oldDepictionOption, DepictionOption);
            }
        }

        #region private methods which actually do the label changing

        /// <summary>
        /// adds a new inline formula to the eldest CMLMolecule encountered in the CML
        /// ... if no CMLMolecule is found then one is added
        /// </summary>
        private void ChangePromptToInlineFormula()
        {
            CmlMolecule molecule = CmlUtils.GetFirstDescendentMolecule(ContextObject.Cml);
            if (molecule == null)
            {
                molecule = new CmlMolecule { Id = "m1" };
                CmlElement cml = CmlUtils.GetEldestCmlCmlElement(ContextObject.Cml);
                if (cml == null)
                {
                    throw new NumboException("The context object does not contain any CMLLite");
                }
                CmlUtils.Add(cml, molecule);
            }
            CmlFormula formula = new CmlFormula { Inline = LabelText };
            CmlUtils.Add(molecule, formula);
            XAttribute inline = formula.DelegateElement.Attribute(CmlAttribute.Inline);
            if (oldComboBoxItem != Prompt)
            {
                Cid.Remove(ContextObject, DepictionOption.MachineUnderstandableOption);
            }
            DepictionOption = new DepictionOption(inline);
            oldComboBoxItem = FormulaInline;
            formula.RemoveOutdated();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ChangePromptToMoleculeBoldNumberLabel()
        {
            CmlMolecule molecule = CmlUtils.GetFirstDescendentMolecule(ContextObject.Cml);
            if (molecule == null)
            {
                molecule = new CmlMolecule { Id = "m1" };
                CmlElement cml = CmlUtils.GetEldestCmlCmlElement(ContextObject.Cml);
                if (cml == null)
                {
                    throw new NumboException("The context object does not contain any CMLLite");
                }
                CmlUtils.Add(cml, molecule);
            }
            string prefix = molecule.DelegateElement.GetPrefixOfNamespace(CmlConstants.CmlDictNS);
            if (prefix == null)
            {
                prefix = CmlConstants.CmlDictPrefix;
                CmlUtils.AddNamespaceDeclaration(molecule, prefix, CmlConstants.CmlDictNS);
            }
            CmlLabel label = new CmlLabel
            {
                Value = LabelText,
                DictRef =
                    string.Format(CultureInfo.InvariantCulture, "{0}:{1}", prefix,
                                  CmlLabel.MoleculeBoldNumber)
            };

            CmlUtils.Add(molecule, label);
            DepictionOption = new DepictionOption(label.DelegateElement);
            oldComboBoxItem = LabelMoleculeBold;
            label.RemoveOutdated();
        }

        /// <summary>
        /// adds a new name to the eldest CMLMolecule encountered in the CML
        /// ... if no CMLMolecule is found then one is added
        /// </summary>
        private void ChangePromptToName()
        {
            CmlMolecule molecule = CmlUtils.GetFirstDescendentMolecule(ContextObject.Cml);
            if (molecule == null)
            {
                molecule = new CmlMolecule { Id = "m1" };
                CmlElement cml = CmlUtils.GetEldestCmlCmlElement(ContextObject.Cml);
                if (cml == null)
                {
                    throw new NumboException("The context object does not contain any CMLLite");
                }
                CmlUtils.Add(cml, molecule);
            }
            CmlName name = new CmlName();
            name.SetValue(LabelText);
            string dictRef;
            if (NameAbbreviation.IsSelected)
            {
                dictRef = "abbreviation";
                oldComboBoxItem = NameAbbreviation;
            }
            else if (NameCas.IsSelected)
            {
                dictRef = "cas";
                oldComboBoxItem = NameCas;
            }
            else if (NameIupac.IsSelected)
            {
                dictRef = "iupac";
                oldComboBoxItem = NameIupac;
            }
            else if (NameSystematic.IsSelected)
            {
                dictRef = "systematic";
                oldComboBoxItem = NameSystematic;
            }
            else if (NameTrivial.IsSelected)
            {
                dictRef = "trivial";
                oldComboBoxItem = NameTrivial;
            }
            else
            {
                /// must be unknown
                dictRef = "unknown";
                oldComboBoxItem = NameUnknown;
            }
            string prefix = molecule.DelegateElement.GetPrefixOfNamespace(CmlConstants.NameDictNS);
            if (prefix == null)
            {
                prefix = CmlConstants.NameDictPrefix;
                CmlUtils.AddNamespaceDeclaration(molecule, prefix, CmlConstants.NameDictNS);
            }
            name.DictRef = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", prefix, dictRef);
            CmlUtils.Add(molecule, name);
            DepictionOption = new DepictionOption(name.DelegateElement);
            name.RemoveOutdated();
        }

        private void ChangeInlineFormulaToMoleculeBoldNumberLabel()
        {
            CmlFormula formula = new CmlFormula(DepictionOption.MachineUnderstandableOption.Parent);
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(formula);
            formula.RemoveInline();
            if (molecule == null)
            {
                molecule = new CmlMolecule { Id = "m1" };
                CmlElement cml = CmlUtils.GetEldestCmlCmlElement(ContextObject.Cml);
                if (cml == null)
                {
                    throw new NumboException("The context object does not contain any CMLLite");
                }
                CmlUtils.Add(cml, molecule);
            }
            string prefix = molecule.DelegateElement.GetPrefixOfNamespace(CmlConstants.CmlDictNS);
            if (prefix == null)
            {
                prefix = CmlConstants.CmlDictPrefix;
                CmlUtils.AddNamespaceDeclaration(molecule, prefix, CmlConstants.CmlDictNS);
            }
            CmlLabel label = new CmlLabel
            {
                Value = LabelText,
                DictRef =
                    string.Format(CultureInfo.InvariantCulture, "{0}:{1}", prefix,
                                  CmlLabel.MoleculeBoldNumber)
            };

            CmlUtils.Add(molecule, label);
            DepictionOption = new DepictionOption(label.DelegateElement);
            oldComboBoxItem = LabelMoleculeBold;
            label.RemoveOutdated();
        }

        /// <summary>
        /// removes the inline formula attribute and adds a new 
        /// CMLName to the parent molecule of the formula element
        /// </summary>
        private void ChangeInlineFormulaToName()
        {
            CmlFormula formula = new CmlFormula(DepictionOption.MachineUnderstandableOption.Parent);
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(formula);
            formula.RemoveInline();
            CmlName name = new CmlName();
            name.SetValue(LabelText);
            string dictRef;
            if (NameAbbreviation.IsSelected)
            {
                dictRef = "abbreviation";
                oldComboBoxItem = NameAbbreviation;
            }
            else if (NameCas.IsSelected)
            {
                dictRef = "cas";
                oldComboBoxItem = NameCas;
            }
            else if (NameIupac.IsSelected)
            {
                dictRef = "iupac";
                oldComboBoxItem = NameIupac;
            }
            else if (NameSystematic.IsSelected)
            {
                dictRef = "systematic";
                oldComboBoxItem = NameSystematic;
            }
            else if (NameTrivial.IsSelected)
            {
                dictRef = "trivial";
                oldComboBoxItem = NameTrivial;
            }
            else
            {
                /// must be unknown
                dictRef = "unknown";
                oldComboBoxItem = NameUnknown;
            }
            string prefix = molecule.DelegateElement.GetPrefixOfNamespace(CmlConstants.NameDictNS);
            if (prefix == null)
            {
                prefix = CmlConstants.NameDictPrefix;
                CmlUtils.AddNamespaceDeclaration(molecule, prefix, CmlConstants.NameDictNS);
            }
            name.DictRef = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", prefix, dictRef);
            CmlUtils.Add(molecule, name);
            DepictionOption = new DepictionOption(name.DelegateElement);
            name.RemoveOutdated();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ChangeMoleculeBoldNumberLabelToInlineFormula()
        {
            CmlLabel label = new CmlLabel((XElement)DepictionOption.MachineUnderstandableOption);
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(label.DelegateElement);
            label.DelegateElement.Remove();
            if (molecule == null)
            {
                molecule = new CmlMolecule { Id = "m1" };
                CmlElement cml = CmlUtils.GetEldestCmlCmlElement(ContextObject.Cml);
                if (cml == null)
                {
                    throw new NumboException("The context object does not contain any CMLLite");
                }
                CmlUtils.Add(cml, molecule);
            }
            CmlFormula formula = new CmlFormula { Inline = LabelText };
            CmlUtils.Add(molecule, formula);
            XAttribute inline = formula.DelegateElement.Attribute(CmlAttribute.Inline);
            DepictionOption = new DepictionOption(inline);
            oldComboBoxItem = FormulaInline;
            formula.RemoveOutdated();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ChangeMoleculeBoldNumberLabelToName()
        {
            CmlLabel label = new CmlLabel((XElement)DepictionOption.MachineUnderstandableOption);
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(label);
            label.DelegateElement.Remove();
            CmlName name = new CmlName();
            name.SetValue(LabelText);
            string dictRef;
            if (NameAbbreviation.IsSelected)
            {
                dictRef = "abbreviation";
                oldComboBoxItem = NameAbbreviation;
            }
            else if (NameCas.IsSelected)
            {
                dictRef = "cas";
                oldComboBoxItem = NameCas;
            }
            else if (NameIupac.IsSelected)
            {
                dictRef = "iupac";
                oldComboBoxItem = NameIupac;
            }
            else if (NameSystematic.IsSelected)
            {
                dictRef = "systematic";
                oldComboBoxItem = NameSystematic;
            }
            else if (NameTrivial.IsSelected)
            {
                dictRef = "trivial";
                oldComboBoxItem = NameTrivial;
            }
            else
            {
                /// must be unknown
                dictRef = "unknown";
                oldComboBoxItem = NameUnknown;
            }
            string prefix = molecule.DelegateElement.GetPrefixOfNamespace(CmlConstants.NameDictNS);
            if (prefix == null)
            {
                prefix = CmlConstants.NameDictPrefix;
                CmlUtils.AddNamespaceDeclaration(molecule, prefix, CmlConstants.NameDictNS);
            }
            name.DictRef = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", prefix, dictRef);
            CmlUtils.Add(molecule, name);
            DepictionOption = new DepictionOption(name.DelegateElement);
            label.RemoveOutdated();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ChangeNameToInlineFormula()
        {
            CmlName name = new CmlName((XElement)DepictionOption.MachineUnderstandableOption);
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(name.DelegateElement);
            name.DelegateElement.Remove();
            if (molecule == null)
            {
                molecule = new CmlMolecule { Id = "m1" };
                CmlElement cml = CmlUtils.GetEldestCmlCmlElement(ContextObject.Cml);
                if (cml == null)
                {
                    throw new NumboException("The context object does not contain any CMLLite");
                }
                CmlUtils.Add(cml, molecule);
            }
            CmlFormula formula = new CmlFormula { Inline = LabelText };
            CmlUtils.Add(molecule, formula);
            XAttribute inline = formula.DelegateElement.Attribute(CmlAttribute.Inline);
            DepictionOption = new DepictionOption(inline);
            oldComboBoxItem = FormulaInline;
            formula.RemoveOutdated();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ChangeNameToMoleculeBoldNumberLabel()
        {
            CmlName name = new CmlName((XElement)DepictionOption.MachineUnderstandableOption);
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(name.DelegateElement);
            name.DelegateElement.Remove();
            if (molecule == null)
            {
                molecule = new CmlMolecule { Id = "m1" };
                CmlElement cml = CmlUtils.GetEldestCmlCmlElement(ContextObject.Cml);
                if (cml == null)
                {
                    throw new NumboException("The context object does not contain any CMLLite");
                }
                CmlUtils.Add(cml, molecule);
            }
            string prefix = molecule.DelegateElement.GetPrefixOfNamespace(CmlConstants.CmlDictNS);
            if (prefix == null)
            {
                prefix = CmlConstants.CmlDictPrefix;
                CmlUtils.AddNamespaceDeclaration(molecule, prefix, CmlConstants.CmlDictNS);
            }
            CmlLabel label = new CmlLabel
            {
                Value = LabelText,
                DictRef =
                    string.Format(CultureInfo.InvariantCulture, "{0}:{1}", prefix,
                                  CmlLabel.MoleculeBoldNumber)
            };

            CmlUtils.Add(molecule, label);
            DepictionOption = new DepictionOption(label.DelegateElement);
            oldComboBoxItem = LabelMoleculeBold;
            label.RemoveOutdated();
        }

        /// <summary>
        /// Effectively just changes the dictRef on the name
        /// </summary>
        private void ChangeNameToName()
        {
            CmlName name = new CmlName((XElement)DepictionOption.MachineUnderstandableOption);
            name.SetValue(LabelText);
            string prefix = name.DelegateElement.GetPrefixOfNamespace(CmlConstants.NameDictNS);
            if (prefix == null)
            {
                prefix = CmlConstants.NameDictPrefix;
                CmlUtils.AddNamespaceDeclaration(name, prefix, CmlConstants.NameDictNS);
            }
            string dictRef;
            if (NameAbbreviation.IsSelected)
            {
                dictRef = "abbreviation";
                oldComboBoxItem = NameAbbreviation;
            }
            else if (NameCas.IsSelected)
            {
                dictRef = "cas";
                oldComboBoxItem = NameCas;
            }
            else if (NameIupac.IsSelected)
            {
                dictRef = "iupac";
                oldComboBoxItem = NameIupac;
            }
            else if (NameSystematic.IsSelected)
            {
                dictRef = "systematic";
                oldComboBoxItem = NameSystematic;
            }
            else if (NameTrivial.IsSelected)
            {
                dictRef = "trivial";
                oldComboBoxItem = NameTrivial;
            }
            else
            {
                /// must be unknown
                dictRef = "unknown";
                oldComboBoxItem = NameUnknown;
            }
            name.DictRef = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", prefix, dictRef);
            DepictionOption = new DepictionOption(name.DelegateElement);
            name.RemoveOutdated();
        }

        #endregion private methods which actually do the label changing
    }
}