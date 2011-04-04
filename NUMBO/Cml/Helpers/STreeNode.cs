// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Xml.Linq;

namespace Numbo.Cml.Helpers
{
    /// <summary>
    /// a node in a spanning tree
    /// </summary>
    public class STreeNode
    {
        /// <summary>
        /// xml vocabulary for tree
        /// </summary>
        public const string Edge = "edge";

        public const string From = "from";
        public const string Id = "id";
        public const string Node = "node";
        public const string To = "to";

        private readonly STree sTree;

        /// <summary>
        /// construct within a tree 
        /// </summary>
        /// <param name="sTree">spanningTree context</param>
        /// <param name="id">unique id for this node</param>
        public STreeNode(STree sTree, string id)
        {
            NodeElem = new XElement(Node);
            NodeElem.SetAttributeValue(Id, id);
            this.sTree = sTree;
            sTree.AtomIdsVisited[id] = this.NodeElem;
        }

        internal XElement NodeElem { get; set; }

        /// <summary>
        /// recursive traversal from this node
        /// order of child nodes same as in order of bonds
        /// </summary>
        internal void DepthFirstTraversal()
        {
            string thisId = NodeElem.Attribute(Id).Value;
            CmlAtom atom = sTree.Molecule.GetAtomById(thisId);
            string parentId = (NodeElem.Parent == null)
                                  ?
                                      null
                                  : NodeElem.Parent.Attribute(Id).Value;
            ICollection<string> ligandIds = atom.GetLigandIds();
            int newNodeCount = 0;
            foreach (string ligandId in ligandIds)
            {
                if (!sTree.AtomIdsVisited.ContainsKey(ligandId))
                {
                    if (CheckInBondSet(sTree.bondSet, thisId, ligandId))
                    {
                        STreeNode ligandNode = new STreeNode(sTree, ligandId);
                        NodeElem.Add(ligandNode.NodeElem);
                        ligandNode.DepthFirstTraversal();
                        newNodeCount++;
                    }
                }
                else
                {
                    if (!ligandId.Equals(parentId))
                    {
                        // ringClosure
                        // probably always in explored set, but check...
                        if (CheckInBondSet(sTree.bondSet, thisId, ligandId))
                        {
                            CmlBond bond = sTree.Molecule.GetBondByAtomRefs2(ligandId, thisId);
                            if (bond == null)
                            {
                                throw new NumboException("Null bond " + ligandId + " / " + thisId);
                            }
                            sTree.AddRingClosure(sTree, NodeElem, bond.Id, thisId, ligandId);
                        }
                    }
                }
            }
            if (newNodeCount > 0)
            {
                sTree.LeafNodeIds.Add(thisId);
            }
        }

        /// <summary>
        /// VERY crude; need to design better lookup for bonds
        /// </summary>
        /// <param name="bondSet"></param>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        /// <returns></returns>
        private bool CheckInBondSet(HashSet<CmlBond> bondSet, string fromId, string toId)
        {
            bool found = true;
            if (bondSet != null)
            {
                found = false;
                foreach (CmlBond bond in bondSet)
                {
                    string[] bb = bond.GetAtomRefs2();
                    if (bb[0].Equals(fromId) && bb[1].Equals(toId) ||
                        bb[1].Equals(fromId) && bb[0].Equals(toId))
                    {
                        found = true;
                        break;
                    }
                }
            }
            return found;
        }
    }
}