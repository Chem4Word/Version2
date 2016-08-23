// Created by Gergely István Oroszi - 2015.06.14.
// 
// -----------------------------------------------------------------------
//   Copyright (c) 2015, The Outercurve Foundation.  
//   This software is released under the Apache License, Version 2.0. 
//   The license and further copyright text can be found in the file LICENSE.TXT at
//   the root directory of the distribution.
// -----------------------------------------------------------------------

using System;
using Chem4Word.Drawing.TwoD.Common;
using Chem4Word.Drawing.TwoD.Nodes;
using Numbo.Cml;

namespace Chem4Word.Drawing.TwoD.Bonds
{
    public interface IEdge : IVisual
    {
        /// <summary>
        /// Is this bond currently selected
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// Is this the active bond
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// The CMLBond which this EdgeControl wraps
        /// </summary>
        CmlBond Bond { get; }

        INode StartNode { get; }
        INode EndNode { get; }

        event EventHandler EdgeClicked;
        void Invalidate();
    }
}