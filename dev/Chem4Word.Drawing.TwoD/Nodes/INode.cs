// Created by Gergely István Oroszi - 2015.06.10.
// 
// -----------------------------------------------------------------------
//   Copyright (c) 2015, The Outercurve Foundation.  
//   This software is released under the Apache License, Version 2.0. 
//   The license and further copyright text can be found in the file LICENSE.TXT at
//   the root directory of the distribution.
// -----------------------------------------------------------------------
//

using System;
using System.Windows;
using Chem4Word.Drawing.TwoD.Common;
using Numbo.Cml;

namespace Chem4Word.Drawing.TwoD.Nodes
{
    public interface INode : IVisual
    {
        Rect RetrieveBounds { get; }

        /// <summary>
        /// The point at which an Attachment (edge) should point at
        /// </summary>
        Point Attachment { get; }

        /// <summary>
        /// Is this atom currently selected
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// Is this the active atom
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// The CmlAtom which this NodeControl wraps
        /// </summary>
        CmlAtom CmlAtom { get; }

        /// <summary>
        /// Indicates the underlying CML has changed
        /// </summary>
        event EventHandler<CmlChangedEventArgs> CmlChangedEvent;

        void Invalidate();
        event EventHandler NodeMouseDown;
    }
}