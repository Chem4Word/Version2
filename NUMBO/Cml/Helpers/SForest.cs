// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Numbo.Cml.Helpers
{
    /// <summary>
    /// a collection of spanning trees for a molecule
    /// if molecule contains more than one disjoint subgraph (moiety)
    /// then there will be a corresponding set of spanning trees
    /// only applies to a toplevel molecule (i.e. nested molecules each have
    /// their own spanning trees)
    /// </summary>
    public class SForest
    {
        /// <summary>
        ///  set of bonds to explore; if null explores all bonds
        /// </summary>
        private HashSet<CmlBond> bondSet;

        /// <summary>
        /// default start at first atom in molecule.GetAtomIds
        /// </summary>
        /// <param name="molecule">molecule to analyze</param>
        public SForest(CmlMolecule molecule)
        {
            if (molecule == null)
            {
                throw new ArgumentNullException("molecule");
            }
            Init();
            IEnumerable<string> atomIds = molecule.GetAtomIds();
            if (atomIds.Count() > 0)
            {
                CreateTrees(molecule, atomIds.ElementAt(0));
            }
        }

        /// <summary>
        /// create spanningForest for molecule using a subset of the bonds
        /// typical use is for discovering ring nucleus list from cyclic bonds
        /// starting at given atom
        /// start at given atom. If not in molecule throws exception
        /// after first tree is analyzed moves to next one based on order of 
        /// atomIds.
        /// result should be reproducible, but not trivially predictable
        /// start at bondIds[0].GetAtomRefs2()[0]
        /// </summary>
        /// <param name="molecule">molecule to analyze</param>
        /// <param name="bondIds">bonds to limit the spanning tree</param>
        public SForest(CmlMolecule molecule, IEnumerable<string> bondIds)
        {
            if (bondIds != null && bondIds.Count() > 0)
            {
                CmlBond startBond = molecule.GetBondById(bondIds.ElementAt(0));
                Init();
                SetBondSet(molecule, bondIds);
                CreateTrees(molecule, startBond.GetAtomRefs2()[0]);
            }
        }

        /// <summary>
        /// create spanningForest for molecule using a subset of the bonds
        /// typical use is for discovering ring nucleus list from cyclic bonds
        /// starting at given atom
        /// start at given atom. If not in molecule throws exception
        /// after first tree is analyzed moves to next one based on order of 
        /// atomIds.
        /// result should be reproducible, but not trivially predictable
        /// </summary>
        /// <param name="molecule">molecule to analyze</param>
        /// <param name="bondIds">bonds to limit the spanning tree</param>
        /// <param name="rootId">atomId to start at</param>
        public SForest(CmlMolecule molecule, IEnumerable<string> bondIds, string rootId)
        {
            Init();
            SetBondSet(molecule, bondIds);
            CreateTrees(molecule, rootId);
        }

        /// <summary>
        /// create spanningForest for molecule starting at given atom
        /// start at given atom. If not in molecule throws exception
        /// after first tree is analyzed moves to next one based on order of 
        /// atomIds.
        /// result should be reproducible, but not trivially predictable
        /// </summary>
        /// <param name="molecule">molecule to analyze</param>
        /// <param name="rootId">atomId to start at</param>
        public SForest(CmlMolecule molecule, string rootId)
        {
            Init();
            CreateTrees(molecule, rootId);
        }

        /// <summary>
        /// list of spanning trees, one per moiety
        /// </summary>
        public List<STree> Trees { get; set; }

        private HashSet<string> UnusedAtomIds { get; set; }

        /// <summary>
        /// initial root of spanning forest and so root of first tree
        /// </summary>
        public string RootId { get; set; }

        private void SetBondSet(CmlMolecule molecule, IEnumerable<string> bondIds)
        {
            if (bondIds != null)
            {
                bondSet = new HashSet<CmlBond>();
                foreach (string bondId in bondIds)
                {
                    bondSet.Add(molecule.GetBondById(bondId));
                }
            }
        }

        private void Init()
        {
            this.Trees = new List<STree>();
        }

        private void CreateTrees(CmlMolecule molecule, string rootId)
        {
            if (molecule == null)
            {
                throw new ArgumentNullException("molecule");
            }
            if (molecule.GetAtomById(rootId) == null)
            {
                throw new ArgumentException("molecule does not contain root: " + rootId);
            }
            IEnumerable<CmlAtom> atomList = molecule.GetAtoms();
            RootId = rootId;
            UnusedAtomIds = new HashSet<string>();
            foreach (CmlAtom atom in atomList)
            {
                UnusedAtomIds.Add(atom.Id);
            }
            AddTree(molecule, rootId);
            while (UnusedAtomIds.Count() > 0)
            {
                AddTree(molecule, UnusedAtomIds.First());
            }
        }

        private void AddTree(CmlMolecule molecule, string rootId)
        {
            STree tree = new STree(molecule, rootId, this.bondSet);

            // if bondSet is not null, neglect single element trees
            if (bondSet == null || tree.StNode.NodeElem.Elements().Count() > 0)
            {
                this.Trees.Add(tree);
            }
            foreach (string id in tree.AtomIdsVisited.Keys)
            {
                UnusedAtomIds.Remove(id);
            }
        }
    }
}