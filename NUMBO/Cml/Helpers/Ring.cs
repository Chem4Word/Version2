// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Euclid;
using log4net;

namespace Numbo.Cml.Helpers
{
    /// <summary>
    /// a Ring (single or part of a ring nucleus)
    /// defined by its bonds
    /// </summary>
    public class Ring : IHasCentroid2, IComparable<Ring>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (Ring));
        private readonly CmlMolecule molecule;

        private List<string> atomIdList;
        internal List<CmlBond> chainedBondList = new List<CmlBond>();

        /// <summary>
        /// create from set of bonds
        /// </summary>
        /// <param name="bonds">bonds should all be in same moiety</param>
        public Ring(IEnumerable<CmlBond> bonds)
        {
            CreateBondIdSetAndChainedBondList(bonds);
            this.molecule = BondIdSet.Count() > 0 ? CmlUtils.GetFirstAncestorMolecule(bonds.ElementAt(0)) : null;
        }

        /// <summary>
        /// create from molecule and ids
        /// </summary>
        /// <param name="molecule">to create from</param>
        /// <param name="cyclicBondIds">bond ids in molecule</param>
        public Ring(CmlMolecule molecule, IEnumerable<string> cyclicBondIds)
        {
            this.molecule = molecule;
            this.BondIdSet = new HashSet<string>();
            foreach (string id in cyclicBondIds)
            {
                BondIdSet.Add(id);
            }
        }

        internal HashSet<string> BondIdSet { get; set; }

        /// <summary>
        /// compare on ring sizes
        /// if equal, then compare on lexical value of concatenated sorted bondIds
        /// </summary>
        /// <param name="other">to compare with</param>
        /// <returns>-1, 0, 1 for this < = > other</returns>
        public int CompareTo(Ring other)
        {
            int result = 0;
            if (this.BondIdSet.Count() < other.BondIdSet.Count())
            {
                result = -1;
            }
            else if (this.BondIdSet.Count() > other.BondIdSet.Count())
            {
                result = 1;
            }
            if (result == 0)
            {
                string thisS = CmlUtils.Concatenate(this.SortedBondIds());
                string ringS = CmlUtils.Concatenate(other.SortedBondIds());
                return string.Compare(thisS, ringS, StringComparison.OrdinalIgnoreCase);
            }
            return result;
        }

        /// <summary>
        /// calculate centroid
        /// </summary>
        /// <returns>centroid; null if no coordinates</returns>
        public Point2 GetCentroid()
        {
            return CmlUtils.GetCentroid(this.GetSubCentroids());
        }

        /// <summary>
        /// returns centroids of atoms
        /// </summary>
        /// <returns>null if no atoms or atoms do not have centroids</returns>
        public ICollection<IHasCentroid2> GetSubCentroids()
        {
            List<IHasCentroid2> centroidList = null;
            if (BondIdSet != null && BondIdSet.Count > 0)
            {
                centroidList = new List<IHasCentroid2>();
                foreach (string bondId in BondIdSet)
                {
                    CmlBond bond = molecule.GetBondById(bondId);
                    centroidList.Add(bond);
                }
            }
            return centroidList;
        }

        private void CreateBondIdSetAndChainedBondList(IEnumerable<CmlBond> bonds)
        {
            if (bonds.Count() < 3)
            {
                throw new ArgumentException("ring must contain at least 3 bonds");
            }
            this.BondIdSet = new HashSet<string>();
            List<CmlBond> unusedBondList = new List<CmlBond>();
            foreach (CmlBond bondx in bonds)
            {
                this.BondIdSet.Add(bondx.Id);
                unusedBondList.Add(bondx);
            }
            chainedBondList = new List<CmlBond>();
            // add first bond to chain
            string atomId0 = unusedBondList.ElementAt(0).GetAtoms().ElementAt(0).Id;
            string atomId = unusedBondList.ElementAt(0).GetAtoms().ElementAt(1).Id;
            CmlBond bond = unusedBondList.ElementAt(0);
            chainedBondList.Add(bond);
            unusedBondList[0] = null;
            int count = 1;
            while (unusedBondList.Count() > count)
            {
                bool change = false;
                for (int i = 0; i < unusedBondList.Count(); i++)
                {
                    bond = unusedBondList.ElementAt(i);
                    if (bond != null)
                    {
                        int otherref = -1;
                        string[] atomRefs2 = bond.GetAtomRefs2();
                        if (atomRefs2[0].Equals(atomId))
                        {
                            otherref = 1;
                        }
                        else if (atomRefs2[1].Equals(atomId))
                        {
                            otherref = 0;
                        }
                        if (otherref != -1)
                        {
                            chainedBondList.Add(bond);
                            atomId = atomRefs2[otherref];
                            change = true;
                            count++;
                            Log.Debug("ADD " + bond.DelegateElement + " ... " + count);
                            unusedBondList[i] = null;
                            break;
                        }
                    }
                }
                if (!change)
                {
                    throw new NumboException("cannot form ring/chain to atom: " + atomId);
                }
            }
            if (!atomId0.Equals(atomId))
            {
                throw new NumboException("chain does not form ring (start: " + atomId0 + ", end: " + atomId + ")");
            }
        }

        /// <summary>
        /// are two rings equal?
        /// </summary>
        /// <param name="ring"></param>
        /// <returns></returns>
        public bool HasIdenticalBonds(Ring ring)
        {
            bool equals = false;
            if (ring != null)
            {
                if (ring.BondIdSet != null)
                {
                    equals = ring.BondIdSet.SetEquals(this.BondIdSet);
                }
            }
            return equals;
        }

        /// <summary>
        /// returns bondIds sorted lexically
        /// </summary>
        /// <returns>lexical list of ids</returns>
        public IEnumerable<string> SortedBondIds()
        {
            List<string> bondIdList = new List<string>();
            foreach (string bondId in BondIdSet)
            {
                bondIdList.Add(bondId);
            }
            bondIdList.Sort();
            return bondIdList;
        }

        /// <summary>
        /// list of bonds in order round the ring
        /// no special starting point
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CmlBond> GetChainedBondList()
        {
            return chainedBondList;
        }

        public int GetDoubleBondCount()
        {
            int count = 0;
            foreach (CmlBond bond in chainedBondList)
            {
                if (CmlBond.Double.Equals(bond.Order))
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// create new ring as intersection of two rings
        /// </summary>
        /// <param name="ring">to intersect with</param>
        /// <returns>bonds common to both rings</returns>
        public Ring Intersect(Ring ring)
        {
            HashSet<string> sdSet = new HashSet<string>();
            foreach (string bondId in this.BondIdSet)
            {
                sdSet.Add(bondId);
            }
            sdSet.Intersect(ring.BondIdSet);
            return new Ring(molecule, sdSet);
        }

        /// <summary>
        /// bonds found in eith ring but not both
        /// </summary>
        /// <param name="ring">to xor</param>
        /// <returns>list of bonds occuring in only one ring</returns>
        public Ring SymmetricExceptWith(Ring ring)
        {
            HashSet<string> sdSet = new HashSet<string>();
            foreach (string bondId in this.BondIdSet)
            {
                sdSet.Add(bondId);
            }
            sdSet.SymmetricExceptWith(ring.BondIdSet);
            return new Ring(molecule, sdSet);
        }

        /// <summary>
        /// all bonds common to two rings
        /// </summary>
        /// <param name="ring">other ring</param>
        /// <returns>list of bonds in eitehr ring or both</returns>
        public Ring Union(Ring ring)
        {
            HashSet<string> sdSet = new HashSet<string>();
            foreach (string bondId in this.BondIdSet)
            {
                sdSet.Add(bondId);
            }
            sdSet.Union(ring.BondIdSet);
            return new Ring(molecule, sdSet);
        }

        /// <summary>
        /// is ring contained in list of rings?
        /// </summary>
        /// <param name="ringList"></param>
        /// <returns></returns>
        public bool IsContainedIn(List<Ring> ringList)
        {
            bool found = false;
            foreach (Ring ring in ringList)
            {
                if (this.CompareTo(ring) == 0)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        /// <summary>
        /// returns a vector from centroid of bond to ring centroid
        /// </summary>
        /// bond must be in ring
        /// vector = ringCentroid.Subtract(bondCentroid);
        /// <param name="bond"></param>
        /// <returns>null if bond is null, not in ring or has no coordinates</returns>
        public Vector2 GetBondToCentroidVector(CmlBond bond)
        {
            Vector2 vector = null;
            if (this.Contains(bond))
            {
                Point2 bondCentroid = bond.GetCentroid();
                Point2 ringCentroid = this.GetCentroid();
                if (bondCentroid != null && ringCentroid != null)
                {
                    vector = ringCentroid.Subtract(bondCentroid);
                }
            }
            return vector;
        }

        /// <summary>
        /// does ring contains bond
        /// </summary>
        /// <param name="bond"></param>
        /// <returns>false if bond == null or bond not in ring</returns>
        public bool Contains(CmlBond bond)
        {
            bool b = false;
            if (bond != null)
            {
                if (BondIdSet != null)
                {
                    b = BondIdSet.Contains(bond.Id);
                }
            }
            return b;
        }

        public List<string> GetAtomIdList()
        {
            if (atomIdList == null)
            {
                atomIdList = new List<string>();
                foreach (string bondId in BondIdSet)
                {
                    CmlBond bond = molecule.GetBondById(bondId);
                    AddAtomId(atomIdList, bond.GetAtomRefs2()[0]);
                    AddAtomId(atomIdList, bond.GetAtomRefs2()[1]);
                }
            }
            return atomIdList;
        }

        private void AddAtomId(ICollection<string> atomIdList, string atomId)
        {
            if (atomId != null && !atomIdList.Contains(atomId))
            {
                atomIdList.Add(atomId);
            }
        }

        /// <summary>
        /// does ring contains atom
        /// </summary>
        /// <param name="atom">atom</param>
        /// <returns>false if atom == null or atom not in ring</returns>
        public bool Contains(CmlAtom atom)
        {
            bool b = false;
            if (atom != null)
            {
                GetAtomIdList();
                b = atomIdList.Contains(atom.Id);
            }
            return b;
        }

        private CmlMolecule GetAsMolecule()
        {
            CmlMolecule cmlMolecule = new CmlMolecule();
            HashSet<string> atomIdSet = new HashSet<string>();
            // add first bond and its atoms
            for (int i = 0; i < chainedBondList.Count(); i++)
            {
                AddBond(chainedBondList.ElementAt(i), atomIdSet, cmlMolecule);
            }
            return cmlMolecule;
        }

        private void AddBond(CmlBond bond, HashSet<string> atomIdSet, CmlMolecule molecule)
        {
            AddAtomIfNew(bond.GetAtoms().ElementAt(0), atomIdSet, molecule);
            AddAtomIfNew(bond.GetAtoms().ElementAt(1), atomIdSet, molecule);
            molecule.AddBond(bond);
        }

        private void AddAtomIfNew(CmlAtom atom, HashSet<string> atomIdSet, CmlMolecule molecule)
        {
            if (!atomIdSet.Contains(atom.Id))
            {
                atomIdSet.Add(atom.Id);
                molecule.AddAtom(atom);
            }
        }

        public static bool AreEqualLists(ICollection<Ring> expected, ICollection<Ring> actual)
        {
            bool equals = false;
            if (actual != null && expected != actual)
            {
                if (expected.Count() == actual.Count)
                {
                    equals = true;
                    for (int i = 0; i < expected.Count(); i++)
                    {
                        Ring expectedRing = expected.ElementAt(i);
                        Ring actualRing = actual.ElementAt(i);
                        if (expectedRing != null)
                        {
                            if (!expectedRing.SetEquals(actualRing))
                            {
                                equals = false;
                                break;
                            }
                        }
                    }
                }
            }
            return equals;
        }

        public bool SetEquals(Ring ring)
        {
            bool equals = this.BondIdSet.SetEquals(ring.BondIdSet);
            return equals;
        }

        public override string ToString()
        {
            return GetAsMolecule().DelegateElement.ToString();
        }
    }
}