// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Windows.Input;

namespace Chem4Word.UI.Commands
{
    public static class ChemCommands
    {
        #region DeleteSelection

        /// <summary>
        /// The DeleteSelection command ....
        /// </summary>
        public static RoutedUICommand DeleteSelection
            = new RoutedUICommand("Delete selection", "DeleteSelection", typeof (ChemCommands));

        #endregion

        #region FlipHorizontal

        /// <summary>
        /// The FlipHorizontal command ....
        /// </summary>
        public static RoutedUICommand FlipHorizontal
            = new RoutedUICommand("Flip Horizontal", "FlipHorizontal", typeof (ChemCommands));

        #endregion

        #region FlipVertical

        /// <summary>
        /// The FlipVertical command ....
        /// </summary>
        public static RoutedUICommand FlipVertical
            = new RoutedUICommand("Flip Vertical", "FlipVertical", typeof (ChemCommands));

        #endregion

        #region RotateSelection

        /// <summary>
        /// The RotateSelection command ....
        /// </summary>
        public static RoutedUICommand RotateSelection
            = new RoutedUICommand("Rotate selection", "RotateSelection", typeof (ChemCommands));

        #endregion

        #region AddElectron

        /// <summary>
        /// The AddElectron command ....
        /// </summary>
        public static RoutedUICommand AddElectron
            = new RoutedUICommand("Add Electron", "AddElectron", typeof (ChemCommands));

        #endregion

        #region RemoveElectron

        /// <summary>
        /// The RemoveElectron command ....
        /// </summary>
        public static RoutedUICommand RemoveElectron
            = new RoutedUICommand("Remove Electron", "RemoveElectron", typeof (ChemCommands));

        #endregion

        #region AddLabel

        /// <summary>
        /// The AddLabel command ....
        /// </summary>
        public static RoutedUICommand AddLabel
            = new RoutedUICommand("Add Label", "AddLabel", typeof (ChemCommands));

        #endregion

        #region FlipSelection

        /// <summary>
        /// The FlipSelection command ....
        /// </summary>
        public static RoutedUICommand FlipSelection
            = new RoutedUICommand("Flip Selection", "FlipSelection", typeof (ChemCommands));

        #endregion

        #region AddIsotopeNumber

        /// <summary>
        /// The AddIsotopeNumber command ....
        /// </summary>
        public static RoutedUICommand AddIsotopeNumber
            = new RoutedUICommand("Add Isotope Number", "AddIsotopeNumber", typeof (ChemCommands));

        #endregion

        #region RemoveIsotopeNumber

        /// <summary>
        /// The RemoveIsotopeNumber command ....
        /// </summary>
        public static RoutedUICommand RemoveIsotopeNumber
            = new RoutedUICommand("Remove Isotope Number", "RemoveIsotopeNumber", typeof (ChemCommands));

        #endregion

        #region RemoveCharge

        /// <summary>
        /// The RemoveCharge command ....
        /// </summary>
        public static RoutedUICommand RemoveCharge
            = new RoutedUICommand("Remove Charge", "RemoveCharge", typeof (ChemCommands));

        #endregion

        #region SetCharge

        /// <summary>
        /// The SetCharge command ....
        /// </summary>
        public static RoutedUICommand SetCharge
            = new RoutedUICommand("Set Charge", "SetCharge", typeof (ChemCommands));

        #endregion

        #region RemoveLabel

        /// <summary>
        /// The RemoveLabel command ....
        /// </summary>
        public static RoutedUICommand RemoveLabel
            = new RoutedUICommand("Remove label", "RemoveLabel", typeof (ChemCommands));

        #endregion

        #region AddProton

        /// <summary>
        /// The IncreaseStamina command ....
        /// </summary>
        public static RoutedUICommand AddProton
            = new RoutedUICommand("Add Proton", "IncreaseStamina", typeof (ChemCommands));

        #endregion

        #region RemoveProton

        /// <summary>
        /// The RemoveProton command ....
        /// </summary>
        public static RoutedUICommand RemoveProton
            = new RoutedUICommand("Remove Proton", "RemoveProton", typeof (ChemCommands));

        #endregion

        #region AddHydrogenDot

        /// <summary>
        /// The AddHydrogenDot command ....
        /// </summary>
        public static RoutedUICommand AddHydrogenDot
            = new RoutedUICommand("Add Hydrogen Dot", "AddHydrogenDot", typeof (ChemCommands));

        #endregion

        #region RemoveHydrogenDot

        /// <summary>
        /// The RemoveHydrogenDot command ....
        /// </summary>
        public static RoutedUICommand RemoveHydrogenDot
            = new RoutedUICommand("Remove Hydrogen Dot", "RemoveHydrogenDot", typeof (ChemCommands));

        #endregion
    }
}