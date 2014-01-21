// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Windows.Forms;
using Chem4Word.Api;
using Chem4Word.UI.Navigator;

namespace Chem4Word.AddIn.Navigator {
    /// <summary>
    ///   Winform user control for hosting WPF Navigator control.
    /// </summary>
    public partial class ChemistryNavigatorHostControl : UserControl {
        /// <summary>
        ///   Initializes a new instance of the ChemistryNavigatorHostControl class.
        /// </summary>
        public ChemistryNavigatorHostControl(IChemistryDocument document) {
            InitializeComponent();

            innerUI.Child = new ChemistryNavigator(document);
        }
    }
}