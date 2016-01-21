// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml.Linq;
using Chem4Word.Api;
using Chem4Word.Core.Events;
using Chem4Word.Core.Properties;
using Chem4Word.Core.UserSetting;
using Chem4Word.UI.Import;
using Chem4Word.UI.UIControls;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Numbo.Coa;
using Application=Microsoft.Office.Interop.Word.Application;
using Document=Microsoft.Office.Tools.Word.Document;
using MSWord = Microsoft.Office.Interop.Word;

namespace Chem4Word.Core.SmartTag
{
    /// <summary>
    /// This class adds options to the Word Context Menus which convert words into Chemistry Zones.
    /// </summary>
    public class ChemistryContextMenuHelper
    {
        /// <summary>
        /// The control Tag added to CommandBarPopup controls for different CommandBar menus.
        /// </summary>
        private static readonly string _contextMenuTag = "2829AECC-061C-4dc5-8CC0-CAEC821B9127";

        /// <summary>
        /// List of Word CommandBars where "Convert to Chemistry Zone" option needs to be enabled.
        /// </summary>
        private static readonly string[] ChemistryWordContextMenus = new[]
                                                                         {"Text", "Spelling", "Grammar", "Grammar (2)"};

        /// <summary>
        /// Separator used while concatinating different IDs.
        /// </summary>
        private static readonly string separator = "||";

        /// <summary>
        /// Instance of the class ChemistryContextMenuHelper.
        /// </summary>
        private static ChemistryContextMenuHelper _chemistryContextMenuHelper;

        /// <summary>
        /// Saves the list of words that can be converted to Chemistry Zones based on the current selection.
        /// </summary>
        private static List<String> _chemistryZoneNames;

        /// <summary>
        /// Instance of class Chem4Word.Core.Core. Used to create chemistry zones.
        /// </summary>
        private static CoreClass _core;

        /// <summary>
        /// Instance of KeyboardEventHook class used to hook keyboard events.
        /// </summary>
        private static KeyboardEventHook _keyboardHook;

        /// <summary>
        /// The list of chemical terms.
        /// </summary>
        private static TermDictionaryManager _termDictionary;

        /// <summary>
        /// The Word application
        /// </summary>
        private static Application _wordApp;

        /// <summary>
        /// The current word document.
        /// </summary>
        private static Document _wordDoc;

        /// <summary>
        /// Stores the list of ChemistryZoneMatch objects in the current selection that can be converted to Chemistry Zones.
        /// </summary>
        private static List<ChemistryZoneMatch> _zoneMatches;

        /// <summary>
        /// Event handler for the user right mouse click event.
        /// </summary>
        public ApplicationEvents4_WindowBeforeRightClickEventHandler WindowBeforeRightClick =
            WordAppWindowBeforeRightClick;

        /// <summary>
        /// Private constructor for single instance of ChemistryContextMenuHelper.
        /// </summary>
        /// <param name="chem4WordCore">The Chem4Word.Core.Core instance.</param>
        /// <param name="wordApplication"></param>
        private ChemistryContextMenuHelper(CoreClass chem4WordCore, Application wordApplication)
        {
            _core = chem4WordCore;
            _wordApp = wordApplication;

            _core.WindowActivate += WindowActivate;
            _core.WindowDeactivateEvent += WindowDeactivate;

            _keyboardHook = new KeyboardEventHook();
            _keyboardHook.KeyboardRightClickEvent += OnKeyboardRightClickEvent;

            // Load Dictionary
            if (_core != null)
            {
                _termDictionary = new TermDictionaryManager(_core.assemblyDirectoryName + @"\SmartTag");
                _termDictionary.LoadLocalDictionary(_core.localAppDataFolder + @"\SmartTag");
            }
        }

        /// <summary>
        /// Method to handle keyboard event that triggers right mouse click functionality.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event information.</param>
        private static void OnKeyboardRightClickEvent(object sender, EventArgs e)
        {
            PopulateContextMenuOptions(_wordApp.ActiveDocument.ActiveWindow.Selection);
        }

        /// <summary>
        /// Gets an instance of the class ChemistryContextMenuHelper.
        /// </summary>
        /// <param name="chem4WordCore">An instance of Chem4Word.Core.Core class.</param>
        /// <param name="wordApplication">The Word application object.</param>
        /// <returns></returns>
        public static ChemistryContextMenuHelper Instance(CoreClass chem4WordCore, Application wordApplication)
        {
            if (_chemistryContextMenuHelper == null)
            {
                _chemistryContextMenuHelper = new ChemistryContextMenuHelper(chem4WordCore, wordApplication);
            }

            return _chemistryContextMenuHelper;
        }

        /// <summary>
        /// Called whenever user does a Right Mouse Click.
        /// </summary>
        /// <param name="selection">The selection below the mouse pointer when the right-click occurred.</param>
        /// <param name="cancel">
        /// False when the event occurs. 
        /// If the event procedure sets this argument to True, the default context menu does not appear when the procedure is finished.
        /// </param>
        private static void WordAppWindowBeforeRightClick(Selection selection, ref bool cancel)
        {
            PopulateContextMenuOptions(selection);
        }

        /// <summary>
        /// Method to populate the context menu options based on the current text selection.
        /// </summary>
        /// <param name="selection">The current text selected by the user.</param>
        private static void PopulateContextMenuOptions(Selection selection)
        {
            if (selection.Paragraphs.Count > 0 && !String.IsNullOrEmpty(selection.Paragraphs.First.Range.Text))
            {
                _chemistryZoneNames = new List<string>();
                _zoneMatches = new List<ChemistryZoneMatch>();

                // Check if the user has selected some text. If yes, only this text is added to the list.
                if (selection.Range != null && !String.IsNullOrEmpty(selection.Range.Text))
                {
                    string selectedText = selection.Range.Text;
                    Recognize(selectedText, selection.Range.Start, selection.Range.Start, true);

                    // If the selected text is not a Chemistry word, add it to the list by default.
                    if (!String.IsNullOrEmpty(selectedText) && !_chemistryZoneNames.Contains(selectedText))
                    {
                        AddToChemistryZoneList(selection.Range.Text, String.Empty, selection.Range.Start,
                                               selection.Range.End, 0);
                    }
                }
                else
                {
                    // If no text was selected, list is populated based on the cursor position.
                    Recognize(selection.Paragraphs.First.Range.Text, selection.Paragraphs.First.Range.Start,
                              selection.Range.Start, false);

                    if (selection.Words.First != null)
                    {
                        string selectedWord = selection.Words.First.Text.Trim();

                        // If the selected text is not a Chemistry word, add it to the list by default.
                        if (!String.IsNullOrEmpty(selectedWord) && !_chemistryZoneNames.Contains(selectedWord))
                        {
                            AddToChemistryZoneList(selectedWord, String.Empty, selection.Words.First.Start,
                                                   selection.Words.First.End, 0);
                        }
                    }
                }
            }

            AddChemistryContextMenus();
        }

        /// <summary>
        /// Creates the ChemistryZoneMatch object for the given selection, which is used to create chemistry zones and adds it to the list.
        /// </summary>
        /// <param name="selectionText">The selected text.</param>
        /// <param name="moleculeId">If selected text is a chemistry term, MoleculeId of the term.</param>
        /// <param name="startIndex">The start index of the selection. This is used to find the Range of the selection in the document.</param>
        /// <param name="endIndex">The end index of the selection. This is used to find the Range of the selection in the document.</param>
        private static void AddToChemistryZoneList(string selectionText, string moleculeId, int startIndex, int endIndex)
        {
            ChemistryZoneMatch zoneMatch = new ChemistryZoneMatch
                                               {
                                                   Text = selectionText,
                                                   MoleculeId = (moleculeId == null) ? String.Empty : moleculeId,
                                                   ContentId = Guid.NewGuid().ToString(),
                                                   StartIndex = startIndex,
                                                   EndIndex = endIndex
                                               };

            // to avoid duplicates entry
            if (!_chemistryZoneNames.Contains(selectionText.Trim()))
            {
                _zoneMatches.Add(zoneMatch);
                _chemistryZoneNames.Add(selectionText);
            }
        }

        /// <summary>
        /// Creates the ChemistryZoneMatch object for the given selection, which is used to create chemistry zones and adds it to the list at the specified position.
        /// </summary>
        /// <param name="selectionText">The selected text.</param>
        /// <param name="moleculeId">If selected text is a chemistry term, MoleculeId of the term.</param>
        /// <param name="startIndex">The start index of the selection. This is used to find the Range of the selection in the document.</param>
        /// <param name="endIndex">The end index of the selection. This is used to find the Range of the selection in the document.</param>
        /// <param name="position">
        /// Position to which the current selection is added. 
        /// Options show up on the Context menu based on their order.
        /// </param>
        private static void AddToChemistryZoneList(string selectionText, string moleculeId, int startIndex, int endIndex,
                                                   int position)
        {
            ChemistryZoneMatch zoneMatch = new ChemistryZoneMatch
                                               {
                                                   Text = selectionText,
                                                   MoleculeId = (moleculeId == null) ? String.Empty : moleculeId,
                                                   ContentId = Guid.NewGuid().ToString(),
                                                   StartIndex = startIndex,
                                                   EndIndex = endIndex
                                               };

            // to avoid duplicates entry
            if (!_chemistryZoneNames.Contains(selectionText.Trim()))
            {
                // Add the selected word at the specified position.
                _zoneMatches.Insert(position, zoneMatch);
                _chemistryZoneNames.Add(selectionText);
            }
        }

        /// <summary>
        /// Adds the Chemistry Context Menus to required Word Command Bars.
        /// </summary>
        private static void AddChemistryContextMenus()
        {
            // initialize  the active document object.
            _wordDoc = Document.GetVstoObject(_wordApp.ActiveWindow.Document);
            _wordApp.CustomizationContext = _wordDoc.AttachedTemplate;
            AddChemistryMenuPopup();
            // Save the context changes to addin custom template.
            ((Template) _wordDoc.AttachedTemplate).Saved = true;
        }

        /// <summary>
        /// Method to add the Chemistry option to the context menus.
        /// </summary>
        private static void AddChemistryMenuPopup()
        {
            ClearChemistryContextMenus();

            if (_zoneMatches.Count > 0)
            {
                foreach (string contextMenuName in ChemistryWordContextMenus)
                {
                    CommandBar contextMenu = _wordApp.CommandBars[contextMenuName];

                    if (contextMenu != null)
                    {
                        CommandBarPopup popupControl = (CommandBarPopup) contextMenu.Controls.Add(
                                                                             MsoControlType.msoControlPopup,
                                                                             Type.Missing, Type.Missing, Type.Missing,
                                                                             true);

                        if (popupControl != null)
                        {
                            popupControl.Caption = Resources.ChemistryContextMenuCaption;
                            popupControl.Tag = _contextMenuTag;
                            AddChemistryMenuSuggestions(popupControl,
                                                        contextMenu.Id.ToString(CultureInfo.InvariantCulture));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adds the list of suggested word to the chemistry context menu.
        /// User clicks on the required word to mark it as a Chemistry Zone.
        /// </summary>
        /// <param name="popupControl">The CommandBarPopup control to add to.</param>
        /// <param name="contextMenuId">Id of the CommandBar which is the parent of the CommandBarPopup.</param>
        private static void AddChemistryMenuSuggestions(CommandBarPopup popupControl, string contextMenuId)
        {
            foreach (ChemistryZoneMatch zoneMatch in _zoneMatches)
            {
                CommandBarButton buttonControl = (CommandBarButton) popupControl.Controls.Add(
                                                                        MsoControlType.msoControlButton, Type.Missing,
                                                                        Type.Missing, Type.Missing, true);

                buttonControl.Caption = zoneMatch.Text;
                buttonControl.Tag = zoneMatch.ContentId + separator + contextMenuId;
                buttonControl.Click += MarkAsChemistryZone;
            }
        }

        /// <summary>
        /// Invoked when user selects a word in the Context Menu to mark as Chemistry Zone.
        /// </summary>
        /// <param name="ctrl">The control which invoked this method.</param>
        /// <param name="cancelDefault"></param>
        private static void MarkAsChemistryZone(CommandBarButton ctrl, ref bool cancelDefault)
        {
            string module = "ChemistryContextMenuHelper.MarkAsChemistryZone()";
            string id = String.Empty;
            ChemistryZoneMatch selectedMatch = null;
            _core.WriteTelemetry(module, "Information", ctrl.Caption);

            if (!String.IsNullOrEmpty(ctrl.Tag))
            {
                string[] tagParts = ctrl.Tag.Split(new[] {separator}, StringSplitOptions.None);
                if (tagParts.Length == 2)
                {
                    selectedMatch = _zoneMatches.Find(
                        match => match.ContentId == tagParts[0]);
                }
            }

            if (selectedMatch != null)
            {
                id = selectedMatch.MoleculeId;

                if (_termDictionary.Contain(id))
                {
                    object start = selectedMatch.StartIndex;
                    object end = selectedMatch.EndIndex;
                    Range selectionRange = ((Application) ctrl.Application).ActiveDocument.Range(
                        ref start, ref end);
                    MarkAsRecognizedChemistryZone(id, selectedMatch, selectionRange);
                }
                else
                {
                    // Set the document selection to the selected text.
                    ((Application) ctrl.Application).ActiveDocument.ActiveWindow.Selection.SetRange(
                        selectedMatch.StartIndex, selectedMatch.EndIndex);
                    _core.AddInlineChemText();
                }
            }

            // Clear the context menu options after user selection.
            ClearChemistryContextMenus();
        }

        /// <summary>
        /// Marks the section as a chemistry term.
        /// </summary>
        /// <param name="id">MoleculeId of the selected term.</param>
        /// <param name="selectedMatch">The selected ChemistryZoneMatch object.</param>
        /// <param name="selectionRange">The range of the selected term.</param>
        private static void MarkAsRecognizedChemistryZone(string id, ChemistryZoneMatch selectedMatch,
                                                          Range selectionRange)
        {
            XDocument cml = XDocument.Parse(_termDictionary.GetValue(id));
            ImportMediator m = new ImportMediator();
            switch (Setting.Import)
            {
                case ImportSetting.StrictFail:
                    m.ParseSeverity = ImportMediator.Severity.Strict;
                    break;
                case ImportSetting.Prompt:
                    m.ParseSeverity = ImportMediator.Severity.Prompt;
                    break;
                case ImportSetting.Auto:
                    m.ParseSeverity = ImportMediator.Severity.Auto;
                    break;
            }
            m.Start(cml);

            if (m.Worked())
            {
                DepictionOption documentDepictionOption = null;
                IEnumerable<DepictionOption> possibleDepictionOptions =
                    Depiction.PossibleDepictionOptions(m.GetContextObject());
                foreach (DepictionOption option in possibleDepictionOptions)
                {
                    if (
                        string.Compare(option.GetAsLatexFormattedString(), selectedMatch.Text,
                                       StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        documentDepictionOption = option;
                        break;
                    }
                }
                if (documentDepictionOption == null)
                {
                    documentDepictionOption = CoreClass.GetPreferedDocumentDepiction(m.GetContextObject(),
                                                                                     Setting.
                                                                                         DocumentPreferedDepiction);
                }

                DepictionOption navigatorDepictionOption =
                    CoreClass.GetPreferedNavigatorDepiction(m.GetContextObject(),
                                                            Setting.NavigatorPreferedDepiction);

                //_core.AddNewContextObjectToDocument(selectionRange, m.GetContextObject(), documentDepictionOption,
                //                                    navigatorDepictionOption);

                var chemistryZoneProperties = new ChemistryZoneProperties(documentDepictionOption,
                                                                                 navigatorDepictionOption, true);
                //_core.AddNewContextObjectToDocument(selectionRange, m.GetContextObject(), documentDepictionOption,
                //                                    navigatorDepictionOption);
                _core.AddNewContextObjectToDocument(selectionRange, m.GetContextObject(), chemistryZoneProperties);
            }
        }

        /// <summary>
        /// Clears the existing controls from the Chemistry Context Menus.
        /// </summary>
        private static void ClearChemistryContextMenus()
        {
            foreach (string contextMenuName in ChemistryWordContextMenus)
            {
                CommandBar contextMenu = _wordApp.CommandBars[contextMenuName];

                if (contextMenu != null)
                {
                    CommandBarPopup popupControl = (CommandBarPopup) contextMenu.FindControl(
                                                                         MsoControlType.msoControlPopup, Type.Missing,
                                                                         _contextMenuTag, true, true);

                    if (popupControl != null)
                    {
                        popupControl.Delete(true);
                    }
                }
            }

            // Save the context changes to addin custom template.
            ((Template) _wordDoc.AttachedTemplate).Saved = true;
        }

        /// <summary>
        /// Function to recognize chemistry words in the dictionary.
        /// </summary>
        /// <param name="paragraphText">The current selected paragraph text.</param>
        /// <param name="selectionRangeStart"></param>
        /// <param name="cursorPosition"></param>
        /// <param name="isSelection"></param>
        private static void Recognize(string paragraphText, int selectionRangeStart, int cursorPosition,
                                      bool isSelection)
        {
            string module = "ChemistryContextMenuHelper.Recognize()";
            paragraphText = paragraphText.Trim();

            if (!String.IsNullOrEmpty(paragraphText))
            {
                try
                {
                    foreach (Term term in _termDictionary)
                    {
                        Regex r = new Regex("(?i)\\b" + Regex.Escape(term.Value) + "\\b");
                        Match m = r.Match(paragraphText);
                        while (m.Success)
                        {
                            int startIndex = selectionRangeStart + m.Index;
                            int endIndex = selectionRangeStart + m.Index + m.Length;

                            SetIndex(m, ref startIndex, ref endIndex);

                            if ((startIndex <= cursorPosition) && (endIndex >= cursorPosition) || isSelection)
                            {
                                AddToChemistryZoneList(m.Value, term.MoleculeId, startIndex, endIndex);
                            }

                            m = m.NextMatch();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _core.WriteTelemetry(module, "Exception", ex.Message);
                    //MessageBox.Show(ex.Message,
                    //                UI.Properties.Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE, MessageBoxButton.OK,
                    //                MessageBoxImage.Error);
                    new ErrorReport(_core.GetTelemetry(), module, ex.Message).ShowDialog();
                }
            }
        }

        private static void SetIndex(Match matchedTerm, ref int startIndex, ref int endIndex)
        {
            object start = startIndex;
            object end = endIndex;

            Range selectionRange = _wordApp.Application.ActiveDocument.Range(ref start, ref end);
            int maxCheck = 0;
            while (selectionRange.Text != matchedTerm.Value && maxCheck < matchedTerm.Value.Length)
            {
                if (!matchedTerm.Value.StartsWith(selectionRange.Text, StringComparison.OrdinalIgnoreCase))
                {
                    start = (int) start + 1;
                }

                end = (int) end + 1;
                selectionRange = _wordApp.Application.ActiveDocument.Range(ref start, ref end);
                maxCheck++;
            }

            startIndex = (int) start;
            endIndex = (int) end;
        }

        /// <summary>
        /// Handles the Window Activate event and set up the Keyboard hooks.
        /// </summary>
        private static void WindowActivate(object sender, ChemistryDocumentEventArgs e)
        {
            _keyboardHook.SetKeyBoardHook();
        }

        /// <summary>
        /// Handles the Window DeActivate event and releases the Keyboard hooks.
        /// </summary>
        private static void WindowDeactivate(object sender, ChemistryDocumentEventArgs e)
        {
            _keyboardHook.ClearKeyBoardHook();
        }
    }
}