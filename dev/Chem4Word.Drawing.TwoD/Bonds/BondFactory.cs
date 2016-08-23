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
using Numbo.Coa;

namespace Chem4Word.Drawing.TwoD.Bonds
{
    public class BondFactory : IBondFactory
    {
        private static BondFactory _bondFactory;
        public static BondFactory Instance
        {
            get
            {
                if(_bondFactory == null)
                    _bondFactory = new BondFactory();

                return _bondFactory;
            }
        }

        public IEdge CreateBond(CmlBond bond, IEdge edge, INode startNode, INode endNode, IChemCanvas canvas)
        {
            switch (bond.Order)
            {
                case CmlBond.Triple:
                    // always draw triple bonds
                    edge = new TripleEdgeControl(canvas.ContextObject, bond, startNode, endNode, canvas);
                    break;
                case CmlBond.Double:
                    // always draw double bonds
                    edge = new DoubleEdgeControl(canvas.ContextObject, bond, startNode, endNode, canvas);
                    break;

                #region case1

                case CmlBond.Single:
                    // don't draw if C-H unless also wedge/hatch
                    // if it is a C-H non-W/H bond then the H will not have been drawn
                    // ie either the StartNode or the EndNode will be null
                    if (startNode != null && endNode != null)
                    {
                        // a 'something' has been drawn at both ends of this bonds so 
                        // we must draw a bond
                        CmlBondStereo bondStereo = bond.GetBondStereo();
                        if (bondStereo == null)
                        {
                            // not a stereo bond so just show a plain single bond
                            edge = new BasicEdgeControl(canvas.ContextObject, bond, startNode, endNode, canvas);
                        }
                        else
                        {
                            // this  should actually use the bondStereo.Concise value too to ensure 
                            // that the W/H is being propertly interpretted
                            switch (bondStereo.DelegateElement.Value)
                            {
                                case "W":
                                    edge = new WedgeEdgeControl(canvas.ContextObject, bond, startNode, endNode, canvas);
                                    break;
                                case "H":
                                    edge = new HatchEdgeControl(canvas.ContextObject, bond, startNode, endNode, canvas);
                                    break;
                                default:
                                    edge = new BasicEdgeControl(canvas.ContextObject, bond, startNode, endNode, canvas);
                                    break;
                            }
                        }
                    }
                    break;

                // either the start or the end node has not been drawn onscreen so
                // there can't be a bond

                #endregion case1

                default:
                    // unrecognised bond type so just draw a dotted line to indicate association between the atoms
                    edge = new DottedEdgeControl(canvas.ContextObject, bond, startNode, endNode, canvas);
                    break;
            }
            return edge;
        }
    }
}