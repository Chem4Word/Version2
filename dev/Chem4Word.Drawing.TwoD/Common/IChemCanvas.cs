// Created by Gergely István Oroszi - 2015.06.14.
// 
// -----------------------------------------------------------------------
//   Copyright (c) 2015, The Outercurve Foundation.  
//   This software is released under the Apache License, Version 2.0. 
//   The license and further copyright text can be found in the file LICENSE.TXT at
//   the root directory of the distribution.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Numbo.Cml;
using Numbo.Cml.Helpers;
using Numbo.Coa;

namespace Chem4Word.Drawing.TwoD.Common
{
    public delegate void AtomTypeOptionsUpdatedHandler(
    HashSet<PeriodicTable.Element> options, IEnumerable<string> groups);

    public delegate void IsotopeOptionsUpdatedHandler(IEnumerable<int> isotopes);

    public interface IChemCanvas
    {
        event AtomTypeOptionsUpdatedHandler AtomTypeOptionsUpdated;
        event IsotopeOptionsUpdatedHandler IsotopeOptionsUpdated;

        /// <summary>
        ///   Gets or sets the ZoomFactor property.  This dependency property 
        ///   indicates ....
        /// </summary>
        double ZoomFactor { get; set; }

        SortedDictionary<string, CmlAtom> SelectedAtoms { get; set; }
        SortedDictionary<string, CmlBond> SelectedBonds { get; set; }
        ContextObject ContextObject { get; }

        /// <summary>
        ///   Get the canvas to which to add UIElements.
        /// </summary>
        Canvas Canvas { get; }

        bool IsCmlValid { get; }

        /// <summary>
        /// </summary>
        bool ConnectionTableChanged { get; }

        double StandardXOffset { get; }
        double StandardYOffset { get; }

        bool IsPerformingDirectionBondSelect { get; set; }
        bool IsDirty { get; }
        bool IsSaved { get; }
        DrawingMode DrawingMode { get; set; }

        void TakeSnapshotOfContextObject();

        void ChemContextMenuCustomAtomTypeSelected(string atomType);
        void SetData(ContextObject contextObject, CmlMolecule moleculePointer);

        /// <summary>
        ///   Convert the model x coordinate to screen x coodinate
        /// </summary>
        /// <param name = "x"></param>
        /// <returns></returns>
        double ToScreenX(double x);

        /// <summary>
        ///   Convert the screen x coordinate to model x coodinate
        /// </summary>
        /// <param name = "x"></param>
        /// <returns></returns>
        double FromScreenX(double x);

        /// <summary>
        ///   Convert the model y coordinate to screen y coodinate
        /// </summary>
        /// <param name = "y"></param>
        /// <returns></returns>
        double ToScreenY(double y);

        /// <summary>
        ///   Convert the screen y coordinate to model y coodinate
        /// </summary>
        /// <param name = "y"></param>
        /// <returns></returns>
        double FromScreenY(double y);

        /// <summary>
        ///   Method to maintain compatability with original design
        ///   Clear the canvas and redraw all the elements.
        ///   TODO should refactor calls to Refresh to include the bool
        /// </summary>
        void Refresh();
        
        event EventHandler Refreshed;
        void Flip();
        ContextObject Save();

        void DoDirectionalSelect(CmlBond bond, CmlAtom atomInSelectionDirection);
    }
}