// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Euclid;

namespace Numbo.Cml.Helpers
{
    /// <summary>
    /// a group of joined rings
    /// includes spiro
    /// </summary>
    public class RingNucleus : IHasCentroid2
    {
        /// <summary>
        /// create ring nucleus from spanning tree of cyclic bonds
        /// sptree normally comes from applying SForest to cyclic bonds
        /// </summary>
        /// apply SSSR by default
        /// <param name="molecule">molecule containg bonds</param>
        /// <param name="spanningTree">sp tree of cyclic bonds</param>
        private RingNucleus(CmlMolecule molecule, STree spanningTree)
        {
            if (molecule == null)
            {
                throw new ArgumentNullException("molecule");
            }
            RingList = new List<Ring>();
            IEnumerable<XElement> edges = spanningTree.StNode.NodeElem.Descendants(STreeNode.Edge);
            IEnumerable<XElement> nodes = spanningTree.StNode.NodeElem.DescendantsAndSelf(STreeNode.Node);
            foreach (XElement edge in edges)
            {
                XElement from = edge.Parent;
                string toS = edge.Attribute(STreeNode.To).Value;
                XElement to = null;
                foreach (XElement node in nodes)
                {
                    string id = node.Attribute(STreeNode.Id).Value;
                    if (toS.Equals(id))
                    {
                        to = node;
                        break;
                    }
                }
                if (to == null)
                {
                    throw new NumboException("Couldn't resolve 'to'");
                }
                HashSet<string> cyclicBondIds = spanningTree.FindCyclicBondsInAncestorsToRoot(
                    from, to);

                RingList.Add(new Ring(molecule, cyclicBondIds));
            }
            ApplySortedSetOfSmallestRings();
            SortRingList();
        }

        public ICollection<Ring> RingList { get; set; }

        /// <summary>
        /// calculate centroid of SSSR centroids (not the atoms or bonds)
        /// </summary>
        /// <returns>centroid; null if no coordinates</returns>
        public Point2 GetCentroid()
        {
            return CmlUtils.GetCentroid(this.GetSubCentroids());
        }

        /// <summary>
        /// returns centroids of SSSR (not atoms or bonds)
        /// </summary>
        /// <returns>null if no atoms or atoms do not have centroids</returns>
        public ICollection<IHasCentroid2> GetSubCentroids()
        {
            List<IHasCentroid2> centroidList = null;
            if (RingList != null && RingList.Count > 0)
            {
                centroidList = new List<IHasCentroid2>();
                foreach (Ring ring in RingList)
                {
                    centroidList.Add(ring);
                }
            }
            return centroidList;
        }

        /// <summary>
        /// create exhaustive list of RingNuclei in molecule
        /// </summary>
        /// <param name="molecule">the moelcule to analyze</param>
        /// <param name="startId">atom to start at (Must be in bond in bonds)</param>
        /// <returns>list of RingNucleus</returns>
        public static IEnumerable<RingNucleus> CreateRingNucleusList(CmlMolecule molecule, string startId)
        {
            IEnumerable<string> cyclicBondIds = molecule.GetCyclicBondIds();
            SForest spanningForest = new SForest(molecule, cyclicBondIds.ToArray(), startId);
            return CreateRingNucleusList(molecule, spanningForest);
        }

        /// <summary>
        /// create exhaustive list of RingNuclei in molecule
        /// </summary>
        /// <param name="molecule">the moelcule to analyze</param>
        /// <returns>list of RingNucleus</returns>
        public static IEnumerable<RingNucleus> CreateRingNucleusList(CmlMolecule molecule)
        {
            IEnumerable<string> cyclicBondIds = molecule.GetCyclicBondIds();
            SForest spanningForest = new SForest(molecule, cyclicBondIds);
            return CreateRingNucleusList(molecule, spanningForest);
        }

        private static IEnumerable<RingNucleus> CreateRingNucleusList(CmlMolecule molecule, SForest spanningForest)
        {
            List<RingNucleus> ringNucleusList = new List<RingNucleus>();
            IEnumerable<STree> spTreeList = spanningForest.Trees;
            if (spTreeList != null)
            {
                foreach (STree tree in spTreeList)
                {
                    ringNucleusList.Add(new RingNucleus(molecule, tree));
                }
            }
            return ringNucleusList;
        }

        /// <summary>
        /// apply set algebra to rings in nucleus to reduce sizes
        /// </summary>
        /// algorithm not verified but makes a reasonable effort
        public void ApplySortedSetOfSmallestRings()
        {
            bool change = true;
            while (change)
            {
                change = false;
                for (int i = 0; i < RingList.Count() - 1; i++)
                {
                    Ring ringi = RingList.ElementAt(i);
                    for (int j = i + 1; j < RingList.Count(); j++)
                    {
                        Ring ringj = RingList.ElementAt(j);
                        // no overlap, continue
                        Ring isect = ringi.Intersect(ringj);
                        if (isect.BondIdSet.Count == 0)
                        {
                            continue;
                        }
                        // does symmetric difference give a smaller ring?
                        Ring ringij = ringi.SymmetricExceptWith(ringj);
                        // is it the same as we already have?
                        if (RingList.Contains(ringij))
                        {
                            continue;
                        }
                        if (ringij.CompareTo(ringi) < 0)
                        {
                            RingList.ElementAt(i).BondIdSet = ringij.BondIdSet;
                            change = true;
                            break;
                        }
                        if (ringij.CompareTo(ringj) < 0)
                        {
                            RingList.ElementAt(j).BondIdSet = ringij.BondIdSet;
                            change = true;
                            break;
                        }
                    }
                }
            }
            SortRingList();
        }

        private void SortRingList()
        {
            Ring[] ringArray = RingList.ToArray();
            Array.Sort(ringArray);
            RingList = new List<Ring>();
            foreach (Ring ring in ringArray)
            {
                RingList.Add(ring);
            }
        }

        /// <summary>
        /// does ringNucleus contains bond
        /// </summary>
        /// <param name="bond"></param>
        /// <returns>false if bond == null or bond not in ringNucleus</returns>
        public bool Contains(CmlBond bond)
        {
            bool b = false;
            if (bond != null)
            {
                foreach (Ring ring in RingList)
                {
                    if (ring.Contains(bond))
                    {
                        b = true;
                        break;
                    }
                }
            }
            return b;
        }

        /// <summary>
        /// does ringNucleus contains atom
        /// </summary>
        /// <param name="atom">atom</param>
        /// <returns>false if atom == null or atom not in ringNucleus</returns>
        public bool Contains(CmlAtom atom)
        {
            bool b = false;
            if (atom != null)
            {
                foreach (Ring ring in RingList)
                {
                    if (ring.Contains(atom))
                    {
                        b = true;
                        break;
                    }
                }
            }
            return b;
        }
    }
}