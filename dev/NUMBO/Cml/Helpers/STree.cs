// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Numbo.Cml.Helpers
{
    /// <summary>
    ///  spanning tree for a single disjoint subgraph (moiety)
    /// </summary>
    public class STree
    {
        /// <summary>
        ///  bonds to explore; if null explores all bonds
        /// </summary>
        internal HashSet<CmlBond> bondSet;

        /// <summary>
        /// create a single spanningTree starting at given atom
        /// include a subset of the bonds; only they will be used
        /// useful for analyzing rings and rings nuclei
        /// </summary>
        /// <param name="molecule">molecule to analyze (may be larger than final tree)</param>
        /// <param name="startAtomID">start atomId; thouws exception if null or not in molecule</param>
        /// <param name="bondSet">set of bonds to include (others are ignored)</param>
        public STree(CmlMolecule molecule, string startAtomID, HashSet<CmlBond> bondSet)
        {
            CreateTree(molecule, startAtomID, bondSet);
        }

        /// <summary>
        /// create a single spanningTree starting at given atom
        /// include all bonds
        /// </summary>
        /// <param name="molecule">molecule to analyze (may be larger than final tree)</param>
        /// <param name="startAtomID">start atomId; thouws exception if null or not in molecule</param>
        public STree(CmlMolecule molecule, string startAtomID)
        {
            CreateTree(molecule, startAtomID, null);
        }

        internal CmlMolecule Molecule { get; set; }
        internal List<string> LeafNodeIds { get; set; }
        internal STreeNode StNode { get; set; }
        internal Dictionary<string, XElement> AtomIdsVisited { get; set; }

        /// <summary>
        /// only public because it needed testing
        /// </summary>
        public HashSet<RingClosure> RingClosures { get; set; }

        private void CreateTree(CmlMolecule molecule, string startAtomID, HashSet<CmlBond> bondSet)
        {
            this.AtomIdsVisited = new Dictionary<string, XElement>();
            this.RingClosures = new HashSet<RingClosure>();
            this.LeafNodeIds = new List<string>();
            this.Molecule = molecule;
            this.bondSet = bondSet;
            StNode = new STreeNode(this, startAtomID);
            StNode.DepthFirstTraversal();
        }

        /// <summary>
        /// returns tree as XML, rooted at started node
        /// uses nested < node >s to indicate child nodes
        /// and < edge > for ring closures
        /// node identifiers are atom ids
        /// </summary>
        /// <returns>tree rooted at start node</returns>
        public XElement GetTreeAsXml()
        {
            return StNode.NodeElem;
        }

        /// <summary>
        /// create HashSet of ringClosures (only one for each clisong bond)
        /// </summary>
        /// <param name="sTree"></param>
        /// <param name="nodeElem"></param>
        /// <param name="bondId"></param>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        internal void AddRingClosure(STree sTree, XElement nodeElem, string bondId, string fromId, string toId)
        {
            // only add ringClosure once
            bool found = false;
            foreach (RingClosure ringClosure in sTree.RingClosures)
            {
                if ((ringClosure.FromId == fromId && ringClosure.ToId == toId) ||
                    (ringClosure.FromId == toId && ringClosure.ToId == fromId))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                XElement edge = new XElement(STreeNode.Edge);
                edge.SetAttributeValue(STreeNode.From, fromId);
                edge.SetAttributeValue(STreeNode.To, toId);
                nodeElem.Add(edge);
                RingClosures.Add(new RingClosure(bondId, fromId, toId));
            }
        }

        /// <summary>
        /// get cyclic bonds sorted lexically by Id
        /// </summary>
        /// <returns>list of ids, zero-length if none; NOT Null</returns>
        public List<string> GetSortedCyclicBondIds()
        {
            HashSet<string> cyclicBondIdSet = new HashSet<string>();
            foreach (RingClosure ringClosure in RingClosures)
            {
                XElement from = AtomIdsVisited[ringClosure.FromId];
                XElement to = AtomIdsVisited[ringClosure.ToId];
                HashSet<string> cyclicBondIds = FindCyclicBondsInAncestorsToRoot(from, to);
                foreach (string id in cyclicBondIds)
                {
                    cyclicBondIdSet.Add(id);
                }
            }
            List<string> cyclicBondIdList = cyclicBondIdSet.ToList();
            cyclicBondIdList.Sort();
            return cyclicBondIdList;
        }

        internal HashSet<string> FindCyclicBondsInAncestorsToRoot(XElement from, XElement to)
        {
            HashSet<string> cyclicBondIdSet = new HashSet<string>();
            HashSet<string> fromAncestorBondIds = GetAncestorBondIds(from.AncestorsAndSelf());
            HashSet<string> toAncestorBondIds = GetAncestorBondIds(to.AncestorsAndSelf());
            fromAncestorBondIds.SymmetricExceptWith(toAncestorBondIds);
            foreach (string bondId in fromAncestorBondIds)
            {
                cyclicBondIdSet.Add(bondId);
            }
            CmlBond bond = Molecule.GetBondByAtomRefs2(
                from.Attribute(STreeNode.Id).Value, to.Attribute(STreeNode.Id).Value);
            cyclicBondIdSet.Add(bond.Id);
            return cyclicBondIdSet;
        }

        private HashSet<string> GetAncestorBondIds(IEnumerable<XElement> ancestors)
        {
            HashSet<string> bondIds = new HashSet<string>();
            for (int i = 1; i < ancestors.Count(); i++)
            {
                CmlBond bond = Molecule.GetBondByAtomRefs2(
                    ancestors.ElementAt(i - 1).Attribute(CmlAttribute.ID).Value,
                    ancestors.ElementAt(i).Attribute(CmlAttribute.ID).Value);
                bondIds.Add(bond.Id);
            }
            return bondIds;
        }
    }

    /// <summary>
    /// holds information on ring closures
    /// probably not required for general exposure but helps testing
    /// </summary>
    public class RingClosure
    {
        /// <summary>
        /// ids for the ring closure link
        /// </summary>
        /// <param name="bondId">id for the closure bond</param>
        /// <param name="fromId">start of bond</param>
        /// <param name="toId">end of bond</param>
        public RingClosure(string bondId, string fromId, string toId)
        {
            BondId = bondId;
            FromId = fromId;
            ToId = toId;
        }

        // public because needed for testing
        public string BondId { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
    }
}