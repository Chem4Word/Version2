// Created by Gergely István Oroszi - 2015.06.14.
// 
// -----------------------------------------------------------------------
//   Copyright (c) 2015, The Outercurve Foundation.  
//   This software is released under the Apache License, Version 2.0. 
//   The license and further copyright text can be found in the file LICENSE.TXT at
//   the root directory of the distribution.
// -----------------------------------------------------------------------

using Chem4Word.Drawing.TwoD.Common;
using Chem4Word.Drawing.TwoD.Nodes;
using Numbo.Cml;

namespace Chem4Word.Drawing.TwoD.Bonds
{
    public interface IBondFactory
    {
        IEdge CreateBond(CmlBond bond, IEdge edge, INode startNode, INode endNode, IChemCanvas canvas);
    }
}