// Created by Gergely István Oroszi - 2015.06.14.
// 
// -----------------------------------------------------------------------
//   Copyright (c) 2015, The Outercurve Foundation.  
//   This software is released under the Apache License, Version 2.0. 
//   The license and further copyright text can be found in the file LICENSE.TXT at
//   the root directory of the distribution.
// -----------------------------------------------------------------------

using Chem4Word.Drawing.TwoD.Common;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.Drawing.TwoD.Nodes
{
    public class NodeFactory : INodeFactory
    {
        private static NodeFactory _nodeFactory;
        public static NodeFactory Instance
        {
            get
            {
                if(_nodeFactory == null)
                    _nodeFactory = new NodeFactory();
                return _nodeFactory;
            }
        }

        public INode CreateNode(CmlAtom atom, ContextObject contextObject, IChemCanvas chemCanvas)
        {
            INode node;
            switch (atom.ElementType)
            {
                case "H":
                    node = new HAtomNodeControl(contextObject, atom, chemCanvas);
                    break;
                case "C":
                    if (atom.IsotopeNumber != null ||
                        ChemicalIntelligence.IsAtomLinearCarbon(contextObject, atom.DelegateElement))
                    {
                        node = new BasicNodeControl(contextObject, atom, chemCanvas);
                    }
                    else
                    {
                        node = new NoTextNodeControl(contextObject, atom, chemCanvas);
                    }
                    break;
                case "F":
                    // F atoms are drawn in green - just for to show we can do a different colour
                    node = new FAtomNodeControl(contextObject, atom, chemCanvas);
                    break;
                default:
                    node = new BasicNodeControl(contextObject, atom, chemCanvas);
                    break;
            }
            return node;
        }
    }
}