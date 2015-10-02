// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Chem4Word.Common.Utilities;
using Euclid;
using log4net;
using Numbo.Cml.Helpers;

namespace Numbo.Cml
{
    /// <summary>
    /// supports CML molecule element
    /// </summary>
    public class CmlMolecule : CmlElement, IHasCentroid2
    {
        #region GroupType enum

        /// <summary>
        /// these are used in dictionary files so do not change
        /// </summary>
        public enum GroupType
        {
            // atoms
            MonovalentSingle,
            RGroupMonovalentSingle,
            DivalentSingle,
            AminoAcid,
            // bonds
            AcyclicSingle,
            CyclicSingle,
            AcyclicDouble,
            CyclicDouble,
            Unknown
        }

        #endregion

        public const string Tag = "molecule";

        /// <summary>
        /// elements for which hydrogen manipulation is possible
        /// </summary>
        public static readonly HashSet<PeriodicTable.Element> CommonElementSet = new HashSet<PeriodicTable.Element>();

        private static readonly ILog Log = LogManager.GetLogger(typeof (CmlMolecule));

        /// <summary>
        /// elements which do not support H manipulation
        /// </summary>
        public static readonly HashSet<PeriodicTable.Element> NextCommonElementSet =
            new HashSet<PeriodicTable.Element>();

        static CmlMolecule()
        {
            CommonElementSet.Add(PeriodicTable.Element.C);
            CommonElementSet.Add(PeriodicTable.Element.N);
            CommonElementSet.Add(PeriodicTable.Element.O);
            CommonElementSet.Add(PeriodicTable.Element.F);
            CommonElementSet.Add(PeriodicTable.Element.Cl);
            CommonElementSet.Add(PeriodicTable.Element.Br);
            CommonElementSet.Add(PeriodicTable.Element.I);

            NextCommonElementSet.Add(PeriodicTable.Element.B);
            NextCommonElementSet.Add(PeriodicTable.Element.Si);
            NextCommonElementSet.Add(PeriodicTable.Element.P);
            NextCommonElementSet.Add(PeriodicTable.Element.S);
        }

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlMolecule()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlMolecule(XElement element)
            : base(element)
        {}

        public CmlMolecule(CmlMolecule molecule)
            : base(molecule)
        {}

        public string Chirality
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.Chirality) != null)
                           ? DelegateElement.Attribute(CmlAttribute.Chirality).Value
                           : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Chirality, value); }
        }

        public Nullable<double> Count
        {
            get
            {
                XAttribute countAttribute = DelegateElement.Attribute(CmlAttribute.Count);
                return (countAttribute == null || countAttribute.Value == null ||
                        string.Empty.Equals(countAttribute.Value))
                           ?
                               (Nullable<double>) null
                           : SafeDoubleParser.Parse(countAttribute.Value);
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Count, value.ToString()); }
        }

        /// <summary>
        /// formal charge
        /// </summary>
        public Nullable<int> FormalCharge
        {
            get { return CmlAttribute.GetFormalCharge(DelegateElement); }
            set
            {
                int? fc = CmlAttribute.GetFormalCharge(DelegateElement);
                int charge = (fc.HasValue) ? fc.Value : 0;
                int delta;
                if (value.HasValue)
                {
                    delta = value.Value - charge;
                    CmlAttribute.SetFormalCharge(DelegateElement, value);
                }
                else
                {
                    delta = - charge;
                }
                CmlMolecule parent = CmlUtils.GetFirstAncestorMolecule(this);
                if (parent != null)
                {
                    parent.ChangeFormalChargeBy(delta);
                }
            }
        }

        /// <summary>
        /// spinMultiplicity 
        /// </summary>
        public Nullable<int> SpinMultiplicity
        {
            get
            {
                XAttribute att = DelegateElement.Attribute(CmlAttribute.SpinMultiplicity);
                return (att == null || att.Value == null || string.Empty.Equals(att.Value))
                           ?
                               (int?) null
                           : int.Parse(att.Value, CultureInfo.InvariantCulture);
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.SpinMultiplicity, value); }
        }

        /// <summary>
        /// calculate centroid
        /// </summary>
        /// <returns>centroid; null if no coordinates</returns>
        public Point2 GetCentroid()
        {
            return CmlUtils.GetCentroid(GetSubCentroids());
        }

        /// </summary>
        /// <returns>null if no atoms or atoms do not have centroids</returns>
        public ICollection<IHasCentroid2> GetSubCentroids()
        {
            IEnumerable<CmlAtom> atoms = GetAtoms();
            List<IHasCentroid2> centroidList = null;
            if (atoms.Count() > 0)
            {
                centroidList = new List<IHasCentroid2>();
                foreach (CmlAtom atom in atoms)
                {
                    centroidList.Add(atom);
                }
            }
            return centroidList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CmlAtom GetAtomById(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id", "cannot get atom with null id");
            }
            IEnumerable<XElement> atoms =
                from atomX in DelegateElement.Descendants(CmlConstants.CmlxNamespace + CmlAtom.Tag)
                where atomX.Attribute(CmlAttribute.ID) != null && id == atomX.Attribute(CmlAttribute.ID).Value
                select atomX;
            CmlAtom atom = null;
            if (atoms.Count() == 0)
            {}
            else if (atoms.Count() > 1)
            {
                throw new NumboException("duplicate atom id");
            }
            else if (atoms.Count() == 1)
            {
                atom = new CmlAtom(atoms.First());
            }
            return atom;
        }

        /// <summary>
        /// finds first atom with first child label with given value
        /// </summary>
        /// <param name="labelValue">value of label (must be exact)</param>
        /// <returns>null if no label or value null or not found</returns>
        public CmlAtom GetAtomByLabelValue(string labelValue)
        {
            CmlAtom atom = null;
            if (labelValue != null)
            {
                XElement atomElement = DelegateElement.XPathSelectElement(
                    ".//*[local-name()='" + CmlAtom.Tag + "' and namespace-uri()='" + CmlConstants.Cmlns + "' and " +
                    "*[local-name()='" + CmlLabel.Tag + "' and namespace-uri()='" + CmlConstants.Cmlns +
                    "' and @value='" + labelValue + "']]");
                if (atomElement != null)
                {
                    atom = new CmlAtom(atomElement);
                }
            }
            return atom;
        }

        /// <summary>
        /// returns CMLAtomArray if exactly one, else null
        /// </summary>
        /// <returns></returns>
        public CmlAtomArray GetAtomArray()
        {
            IEnumerable<XElement> atomArrays =
                DelegateElement.Elements(CmlConstants.CmlxNamespace + CmlAtomArray.Tag);
            return (atomArrays.Count() == 1) ? new CmlAtomArray(atomArrays.First()) : null;
        }

        /// <summary>
        /// returns CMLBondArray if exactly one, else null
        /// </summary>
        /// <returns></returns>
        public CmlBondArray GetBondArray()
        {
            IEnumerable<XElement> bondArrays =
                DelegateElement.Elements(CmlConstants.CmlxNamespace + CmlBondArray.Tag);
            return (bondArrays.Count() == 1) ? new CmlBondArray(bondArrays.First()) : null;
        }

        /// <summary>
        /// gets all atoms in this molecule 
        /// does not recurse to nested molecules
        /// </summary>
        /// <returns>atoms</returns>
        public IEnumerable<CmlAtom> GetAtoms()
        {
            CmlAtomArray atomArray = GetAtomArray();
            List<CmlAtom> atomList = new List<CmlAtom>();
            if (atomArray != null)
            {
                IEnumerable<XElement> atomsX =
                    atomArray.DelegateElement.Elements(CmlConstants.CmlxNamespace + CmlAtom.Tag);
                atomList.AddRange(atomsX.Select(atomX => new CmlAtom(atomX)));
            }
            return atomList;
        }

        /// <summary>
        /// gets all atoms in this molecule including nested molecules
        /// </summary>
        /// <returns>atoms</returns>
        public IEnumerable<CmlAtom> GetAllAtoms()
        {
            var molecules = GetChildMoleculeList();
            return !molecules.Any() ? GetAtoms() : molecules.SelectMany(molecule => molecule.GetAtoms());
        }

        /// <summary>
        /// gets all atomIds in this molecule 
        /// does not recurse to nested molecules
        /// </summary>
        /// <returns>atom ids</returns>
        public IEnumerable<string> GetAtomIds()
        {
            CmlAtomArray atomArray = GetAtomArray();
            List<string> atomList = new List<string>();
            if (atomArray != null) {
                IEnumerable<XElement> atomsX =
                    atomArray.DelegateElement.Elements(CmlConstants.CmlxNamespace + CmlAtom.Tag);
                foreach (XAttribute attribute in atomsX.Select(atomX => atomX.Attribute(CmlAttribute.ID))) {
                    if (attribute == null)
                    {
                        throw new NumboException("null atom id");
                    }
                    atomList.Add(attribute.Value);
                }
            }
            return atomList;
        }

        /// <summary>
        /// gets all bonds in this molecule 
        /// does not recurse to nested molecules
        /// </summary>
        /// <returns>bonds</returns>
        public ICollection<CmlBond> GetBonds()
        {
            CmlBondArray bondArray = GetBondArray();
            List<CmlBond> bondList = new List<CmlBond>();
            if (bondArray != null) {
                IEnumerable<XElement> bondsX =
                    bondArray.DelegateElement.Elements(CmlConstants.CmlxNamespace + CmlBond.Tag);
                bondList.AddRange(bondsX.Select(bondX => new CmlBond(bondX)));
            }
            return bondList;
        }

        /// <summary>
        /// gets all bonds in this molecule including nested molecules
        /// </summary>
        /// <returns>bonds</returns>
        public ICollection<CmlBond> GetAllBonds()
        {
            ICollection<CmlMolecule> molecules = GetChildMoleculeList();
            if (molecules.Count() == 0)
            {
                return GetBonds();
            }
            return molecules.SelectMany(molecule => molecule.GetBonds()).ToList();
        }

        /// <summary>
        /// gets all bondIds in this molecule 
        /// does not recurse to nested molecules
        /// does not force generation of ids
        /// </summary>
        /// <returns>bond ids</returns>
        public IEnumerable<string> GetBondIds()
        {
            return GetBondIds(false);
        }

        /// <summary>
        /// gets all bondIds in this molecule 
        /// does not recurse to nested molecules
        /// </summary>
        /// <param name="generate">if true, forces generation of Ids</param>
        /// <returns>bond ids</returns>
        public IEnumerable<string> GetBondIds(bool generate)
        {
            CmlBondArray bondArray = GetBondArray();
            List<string> bondList = new List<string>();
            if (bondArray != null)
            {
                IEnumerable<XElement> bondsX =
                    bondArray.DelegateElement.Elements(CmlConstants.CmlxNamespace + CmlBond.Tag);
                foreach (XElement bondX in bondsX)
                {
                    XAttribute attribute = bondX.Attribute(CmlAttribute.ID);
                    if (attribute == null)
                    {
                        string atomRefs2 = bondX.Attribute(CmlAttribute.AtomRefs2).Value;
                        if (!generate)
                        {
                            throw new NumboException("bond has no id " + atomRefs2);
                        }
                        bondX.SetAttributeValue(CmlAttribute.ID, atomRefs2);
                    }
                    bondList.Add(attribute.Value);
                }
            }
            return bondList;
        }

        /// <summary>
        /// list of bonds corresponding to ids
        /// </summary>
        /// <param name="bondIds">list of ids; returns null if null</param>
        /// <returns>atoms; zero length array if none</returns>
        public IEnumerable<CmlBond> GetBonds(string[] bondIds)
        {
            if (bondIds == null)
            {
                throw new ArgumentNullException("bondIds");
            }
            List<CmlBond> bondList = new List<CmlBond>();
            foreach (string bondId in bondIds)
            {
                CmlBond bond = GetBondById(bondId);
                bondList.Add(bond);
            }
            return bondList;
        }

        /// <summary>
        /// gets bond by id
        /// </summary>
        /// <param name="id">id of bond</param>
        /// <returns>bond</returns>
        public CmlBond GetBondById(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            IEnumerable<XElement> bonds =
                from bondX in DelegateElement.Descendants(CmlConstants.CmlxNamespace + CmlBond.Tag)
                where bondX.Attribute(CmlAttribute.ID) != null && id == bondX.Attribute(CmlAttribute.ID).Value
                select bondX;
            CmlBond bond = null;
            switch (bonds.Count())
            {
                case 0:
                    // bond is null
                    break;
                case 1:
                    bond = new CmlBond(bonds.First());
                    break;
                default:
                    // there must be more that one bond with the same id
                    throw new NumboException("duplicate bond id");
            }
            return bond;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CmlBond GetBondByAtomRefs2(string id1, string id2)
        {
            if (id1 == null)
            {
                throw new ArgumentNullException("id1");
            }
            if (id2 == null)
            {
                throw new ArgumentNullException("id2");
            }
            IEnumerable<XElement> bonds =
                from bondX in DelegateElement.Descendants(CmlConstants.CmlxNamespace + CmlBond.Tag)
                where (bondX.Attribute(CmlAttribute.AtomRefs2).Value.Equals(id1 + " " + id2) ||
                       bondX.Attribute(CmlAttribute.AtomRefs2).Value.Equals(id2 + " " + id1))
                select bondX;
            CmlBond bond = null;
            switch (bonds.Count())
            {
                case 0:
                    // bond is null
                    break;
                case 1:
                    bond = new CmlBond(bonds.First());
                    break;
                default:
                    // there must be more that one bond with the same id
                    throw new NumboException("duplicate bond id");
            }
            return bond;
        }

        /// <summary>
        /// list of atoms corresponding to ids
        /// </summary>
        /// <param name="atomIds">list of ids; returns null if null</param>
        /// <returns>atoms; zero length array of none</returns>
        public IEnumerable<CmlAtom> GetAtoms(string[] atomIds)
        {
            if (atomIds == null)
            {
                throw new ArgumentNullException("atomIds");
            }
            List<CmlAtom> atomList = new List<CmlAtom>();
            foreach (string atomId in atomIds)
            {
                CmlAtom atom = GetAtomById(atomId);
                atomList.Add(atom);
            }
            return atomList;
        }

        /// <summary>
        /// gets Ids of cyclic bonds
        /// 
        /// </summary>
        /// <returns>if none returns empty array (not null)</returns>
        public List<string> GetCyclicBondIds()
        {
            List<string> cyclicBondIds = new List<string>();
            IEnumerable<CmlAtom> atoms = GetAtoms();
            if (atoms.Count() > 0)
            {
                CmlAtom atom = GetAtoms().First();
                SForest spanningForest = new SForest(this, atom.Id);
                List<STree> spanningTreeList = spanningForest.Trees;
                STree spanningTree = spanningTreeList[0];
                cyclicBondIds = spanningTree.GetSortedCyclicBondIds();
            }
            else
            {
                Log.Debug(this);
            }
            return cyclicBondIds;
        }

        /// <summary>
        /// sets all bonds to have this indicator
        /// normally used to set to Unknown or Acyclic (just before adding cyclic)
        /// </summary>
        /// <param name="cyclicityIndicator"></param>
        public void SetCyclicIndicators(CmlBond.Cyclicity cyclicityIndicator)
        {
            IEnumerable<CmlBond> bondList = GetBonds();
            foreach (CmlBond bond in bondList)
            {
                bond.SetCyclicIndicator(cyclicityIndicator);
            }
        }

        /// <summary>
        /// calculates whether all bonds are a/cyclic and sets the value
        /// </summary>
        public void CalculateAndSetCyclicIndicators()
        {
            SetCyclicIndicators(CmlBond.Cyclicity.Acyclic);
            IEnumerable<string> cyclicBondIds = GetCyclicBondIds();
            foreach (string cyclicBondId in cyclicBondIds)
            {
                CmlBond cyclicBond = GetBondById(cyclicBondId);
                cyclicBond.SetCyclicIndicator(CmlBond.Cyclicity.Cyclic);
            }
        }

        /// <summary>
        /// works out whether the atomGroup has bonds connecting to other atoms
        /// ignores hydrogens of any sort
        /// typical usage is to frind how many "valencies" the atomGroup has
        /// </summary>
        /// <param name="atomGroup"></param>
        /// <returns>list of bonds connecting to non-hydrogen atoms outside atomGroup</returns>
        public List<CmlBond> GetExternalBonds(IEnumerable<CmlAtom> atomGroup)
        {
            IEnumerable<CmlAtom> atoms = AddBondedHydrogensToGroup(atomGroup);
            HashSet<string> atomIdSet = new HashSet<string>();
            foreach (CmlAtom atom in atoms)
            {
                atomIdSet.Add(atom.Id);
            }
            List<CmlBond> bondList = new List<CmlBond>();
            foreach (CmlAtom atom in atoms)
            {
                ICollection<CmlAtom> ligandAtoms = atom.GetLigands();
                ICollection<CmlBond> ligandBonds = atom.GetLigandBonds();
                for (int i = 0; i < ligandAtoms.Count(); i++)
                {
                    if (!atomIdSet.Contains(ligandAtoms.ElementAt(i).Id))
                    {
                        bondList.Add(ligandBonds.ElementAt(i));
                    }
                }
            }
            return bondList;
        }

        /// <summary>
        /// adds any bonded hydrogens to group of atoms
        /// assumes all atoms are in same molecule
        /// </summary>
        /// <param name="atomGroup">set of atoms without constraints other than in same local molecule</param>
        /// <returns>set of original atoms and any bonded hydrogens not already included</returns>
        public static IEnumerable<CmlAtom> AddBondedHydrogensToGroup(IEnumerable<CmlAtom> atomGroup)
        {
            HashSet<CmlAtom> atoms = new HashSet<CmlAtom>();
            foreach (CmlAtom atom in atomGroup)
            {
                atoms.Add(atom);
                IEnumerable<CmlAtom> hatomLigands = atom.GetHydrogenLigands();
                foreach (CmlAtom ligand in hatomLigands)
                {
                    atoms.Add(ligand);
                }
            }
            return atoms;
        }

        /// <summary>
        /// unflattens molecule (i.e. creates new molecule for each spanning tree
        /// modifies this
        /// </summary>
        public void Unflatten()
        {
            // nested unflattening - unlikely but anyway...
            IEnumerable<CmlMolecule> childMolecules = GetChildMoleculeList();
            if (childMolecules.Count() > 0)
            {
                foreach (CmlMolecule childMolecule in childMolecules)
                {
                    childMolecule.Unflatten();
                }
            }
            else
            {
                IEnumerable<string> atomIds = GetAtomIds();
                if (atomIds.Count() > 0)
                {
                    SForest spanningForest = new SForest(this, atomIds.ElementAt(0));
                    List<STree> spanningTreeList = spanningForest.Trees;
                    // generate the unflattened
                    switch (spanningTreeList.Count())
                    {
                        case 0:
                            throw new NumboException("No atoms found");
                        case 1:
                            break;
                        default:
                            for (int i = 0; i < spanningTreeList.Count(); i++)
                            {
                                STree spanningTree = spanningTreeList.ElementAt(i);
                                CmlMolecule childMolecule = new CmlMolecule {Count = 1.0D};
                                CmlUtils.Add(this, childMolecule);
                                childMolecule.Id = Id + "_" + (i + 1);
                                IEnumerable<XElement> nodes =
                                    from e in spanningTree.StNode.NodeElem.DescendantsAndSelf(STreeNode.Node)
                                    where e.Attribute(CmlAttribute.ID) != null
                                    select e;
                                MoveAtoms(childMolecule, nodes);
                                MoveBonds(spanningTree, childMolecule, nodes);
                            }
                            CmlUtils.Remove(GetAtomArray());
                            CmlUtils.Remove(GetBondArray());
                            break;
                    }
                }
            }
        }

        private void MoveAtoms(CmlMolecule childMolecule, IEnumerable<XElement> nodes)
        {
            CmlAtomArray childAtomArray = new CmlAtomArray();
            CmlUtils.Add(childMolecule, childAtomArray);

            foreach (XElement node in nodes)
            {
                // remove atom from previous atomArray
                CmlAtom atom = GetAtomById(node.Attribute(CmlAttribute.ID).Value);
                CmlUtils.Remove(atom);
                childAtomArray.DelegateElement.Add(atom.DelegateElement);
            }
        }

        private void MoveBonds(STree spanningTree, CmlMolecule childMolecule, IEnumerable<XElement> nodes)
        {
            IEnumerable<XElement> edges = from e in spanningTree.StNode.NodeElem.DescendantsAndSelf(STreeNode.Edge)
                                          where e.Attribute(CmlAttribute.ID) != null
                                          select e;

            CmlBondArray childBondArray = new CmlBondArray();
            CmlUtils.Add(childMolecule, childBondArray);
            foreach (XElement node in nodes)
            {
                // remove bond from previous bondArray
                XElement parent = node.Parent;
                if (parent != null)
                {
                    string nodeId = node.Attribute(CmlAttribute.ID).Value;
                    CmlBond bond = GetBondByAtomRefs2(nodeId, parent.Attribute(CmlAttribute.ID).Value);
                    CmlUtils.Remove(bond);
                    childBondArray.DelegateElement.Add(bond.DelegateElement);
                }
            }
            foreach (XElement edge in edges)
            {
                // move bond from previous bondArray
                string from = edge.Attribute(STreeNode.From).Value;
                string to = edge.Attribute(STreeNode.To).Value;
                if (from == null || to == null)
                {
                    throw new NumboException("null to or from");
                }
                CmlBond bond = GetBondByAtomRefs2(from, to);
                CmlUtils.Add(childBondArray, bond);
            }
        }

        public override string GetTag()
        {
            return Tag;
        }

        /// <summary>
        /// gets list of child molecules in document order
        /// </summary>
        /// <returns>if none returns empty list not null</returns>
        public ICollection<CmlMolecule> GetChildMoleculeList()
        {
            List<CmlMolecule> children = new List<CmlMolecule>();

            IEnumerable<XElement> elems = from elem in DelegateElement.Elements(
                                              CmlConstants.CmlxNamespace + Tag)
                                          select elem;
            foreach (XElement e in elems)
            {
                children.Add(new CmlMolecule(e));
            }
            return children;
        }

        /// <summary>
        /// gets list of child formula in document order
        /// </summary>
        /// <returns>if none returns empty list not null</returns>
        public IEnumerable<CmlFormula> GetChildFormulaList()
        {
            List<CmlFormula> children = new List<CmlFormula>();

            IEnumerable<XElement> elems = from elem in DelegateElement.Elements(
                                              CmlConstants.CmlxNamespace + CmlFormula.Tag)
                                          select elem;
            foreach (XElement e in elems)
            {
                children.Add(new CmlFormula(e));
            }
            return children;
        }

        /// <summary>
        /// gets first child formula
        /// </summary>
        /// <returns>if none returns null</returns>
        public CmlFormula GetChildFormula()
        {
            IEnumerable<CmlFormula> children = GetChildFormulaList();
            return (children.Count() > 0) ? children.First() : null;
        }

        public void RemoveChildFormulae()
        {
            IEnumerable<CmlFormula> formulaList = GetChildFormulaList();
            foreach (CmlFormula formula in formulaList)
            {
                formula.DelegateElement.Remove();
            }
        }

        public void ScaleToAverageBondLength(double newLength)
        {
            double? averageBondLength = GetAverageBondLength();

            if (averageBondLength != 0 && newLength != 0)
            {
                double? scale = newLength / averageBondLength;

                IEnumerable<CmlAtom> atoms = GetAtoms();

                if (atoms.Count() > 0)
                {
                    foreach (CmlAtom atom in atoms)
                    {
                        atom.X2 *= scale;
                        atom.Y2 *= scale;
                    }
                }
            }
        }

        /// <summary>
        /// get average bond length
        /// </summary>
        /// <returns></returns>
        public Nullable<double> GetAverageBondLength()
        {
            Nullable<double> averageBondLength = null;
            IEnumerable<CmlBond> bonds = GetBonds();
            if (bonds.Count() > 0)
            {
                Nullable<double> total = 0;
                foreach (CmlBond bond in bonds)
                {
                    total += bond.GetBondLength();
                }
                averageBondLength = total/bonds.Count();
            }
            return averageBondLength;
        }

        /// <summary>
        /// get median bond length
        /// </summary>
        /// <returns>null if zero atoms or any bonded atoms have no coordinates</returns>
        public Nullable<double> GetMedianBondLength()
        {
            Nullable<double> medianLength = null;
            IEnumerable<CmlBond> bonds = GetBonds();
            if (bonds.Count() > 0)
            {
                double[] dList = new double[bonds.Count()];
                int i = 0;
                foreach (CmlBond bond in bonds)
                {
                    Nullable<double> length = bond.GetBondLength();
                    if (length == null)
                    {
                        medianLength = null;
                        break;
                    }
                    dList[i++] = (double) length;
                }
                medianLength = Real.GetMedian(dList);
            }
            return medianLength;
        }
        
        /// <summary>
        /// adds atom and ensures unique id
        /// checks for duplicates first
        /// </summary>
        /// <param name="atom"></param>
        /// <returns></returns>
        public CmlMolecule AddAtom(CmlAtom atom)
        {
            if (atom.Id != null)
            {
                if (GetAtomById(atom.Id) != null)
                {
                    throw new ArgumentException("cannot add atom with duplicate id: " + atom.Id);
                }
            }
            if (atom.Id == null)
            {
                atom.Id = GenerateUniqueAtomId();
            }
            CmlAtomArray atomArray = EnsureAtomArray();
            atomArray.DelegateElement.Add(atom.DelegateElement);

            return this;
        }

        private CmlAtomArray EnsureAtomArray()
        {
            XElement xAtomArray = DelegateElement.Element(CmlConstants.CmlxNamespace + CmlAtomArray.Tag);
            CmlAtomArray atomArray;
            if (xAtomArray != null)
            {
                atomArray = new CmlAtomArray(xAtomArray);
            }
            else
            {
                atomArray = new CmlAtomArray();
                CmlUtils.Add(this, atomArray);
            }
            return atomArray;
        }

        /// <summary>
        /// adds a bond; if no id, generates one
        /// </summary>
        /// <param name="bond"></param>
        /// <returns></returns>
        public CmlMolecule AddBond(CmlBond bond)
        {
            if (bond == null)
            {
                throw new ArgumentNullException("bond");
            }
            if (bond.GetAtomRefs2() == null)
            {
                throw new NumboException("atomRefs2 is null");
            }
            if (bond.Id == null)
            {
                bond.GenerateAndAddId();
            }
            CmlBondArray bondArray = EnsureBondArray();
            bondArray.DelegateElement.Add(bond.DelegateElement);

            return this;
        }

        private CmlBondArray EnsureBondArray()
        {
            XElement xBondArray = DelegateElement.Element(
                CmlConstants.CmlxNamespace + CmlBondArray.Tag);
            CmlBondArray bondArray;
            if (xBondArray != null)
            {
                bondArray = new CmlBondArray(xBondArray);
            }
            else
            {
                bondArray = new CmlBondArray();
                CmlUtils.Add(this, bondArray);
            }
            return bondArray;
        }

        /// <summary>
        /// gets unique identifier
        /// starts at "an" where n is current atomCount
        /// increases to a(n+1) ... etc and returns first unused
        /// </summary>
        /// <returns></returns>
        public string GenerateUniqueAtomId()
        {
            string id = null;
            IEnumerable<string> atomIds = GetAtomIds();
            int serial = atomIds.Count() + 1;
            while (true)
            {
                id = "a" + (serial++);
                if (GetAtomById(id) == null)
                {
                    break;
                }
            }
            return id;
        }

        /// <summary>
        /// gets unique identifier
        /// starts at "an" where n is current atomCount
        /// increases to a(n+1) ... etc and returns first unused
        /// </summary>
        /// <returns></returns>
        public string GenerateUniqueBondId()
        {
            string id = null;
            IEnumerable<string> bondIds = GetBondIds();
            int serial = bondIds.Count() + 1;
            while (true)
            {
                id = "b" + (serial++);
                if (GetBondById(id) == null)
                {
                    break;
                }
            }
            return id;
        }

        public void NormaliseBondOrdersOnImport()
        {
            ICollection<CmlBond> bonds = GetAllBonds();
            foreach (CmlBond bond in bonds)
            {
                bond.NormaliseOrderOnImport();
            }
        }

        public void NormalizeFormulas()
        {
            string concise = CmlFormula.GetOrCalculateConciseFormula(this);
            IEnumerable<XElement> formulaElements =
                DelegateElement.XPathSelectElements("./*[local-name()='formula' and namespace-uri()='" +
                                                    CmlConstants.Cmlns + "']");
            for (int i = 0; i < formulaElements.Count(); i++)
            {
                CmlFormula formulaElement = new CmlFormula(formulaElements.ElementAt(i));
                String conciseString = formulaElement.Concise;
                if (conciseString == null)
                {
                    formulaElement.Concise = concise;
                }
                else if (conciseString.Equals(concise))
                {
                    // no need to update
                }
                else
                {
                    throw new NumboException("incompatible concise formulae; was " +
                                             conciseString + "; now " + concise);
                }
                // remove any spurious atomArrays
                IEnumerable<XElement> atomArrays =
                    formulaElement.DelegateElement.XPathSelectElements(
                        "*[local-name()='atomArray' and namespace-uri()='" + CmlConstants.Cmlns + "']");
                for (int j = 0; j < atomArrays.Count(); j++)
                {
                    atomArrays.ElementAt(j).Remove();
                }
            }
        }

        public CmlFormula GetFirstConciseFormula()
        {
            XElement formula =
                DelegateElement.XPathSelectElement("./*[local-name()='" + CmlFormula.Tag +
                                                   "' and @concise and namespace-uri()='" + CmlConstants.Cmlns +
                                                   "'][1]");
            return (formula == null) ? null : new CmlFormula(formula);
        }

        /// <summary>
        /// normalize concise formulas recursively upwards
        ///    if concise formulas are not present, don't try and add them
        ///    if concise formulas are present overwrite them with calculated value
        ///    
        /// </summary>
        public void NormalizeConciseFormulas()
        {
            // recurse through nested molecules
            IEnumerable<CmlMolecule> childMolecules = GetChildMoleculeList();
            if (childMolecules.Count() > 0)
            {
                CmlFormula newFormula = new CmlFormula();
                foreach (CmlMolecule childMolecule in childMolecules)
                {
                    childMolecule.NormalizeConciseFormulas();
                    CmlFormula childFormula = childMolecule.GetFirstConciseFormula();
                    if (childFormula != null)
                    {
                        if (childMolecule.Count != null)
                        {
                            childFormula.MultiplyBy((double) childMolecule.Count);
                        }
                        newFormula.Add(childFormula);
                    }
                }
                CmlFormula thisFormula = GetFirstConciseFormula();
                // no formula?
                if (thisFormula == null)
                {
                    CmlFormula formula = new CmlFormula {Concise = newFormula.Concise};
                    CmlUtils.Add(this, formula);
                }
                else
                {
                    IEnumerable<XElement> childFormulas =
                        DelegateElement.XPathSelectElements("./*[local-name()='" + CmlFormula.Tag +
                                                            "' and namespace-uri()='" + CmlConstants.Cmlns +
                                                            "']");
                    foreach (XElement f in childFormulas)
                    {
                        new CmlFormula(f) {Concise = newFormula.Concise};
                    }
                }
            }
            else
            {
                // there are no child molecules 
                IEnumerable<CmlFormula> childFormulaList = GetChildFormulaList();
                string calculatedConcise = CmlFormula.CalculateConciseFormula(this);

                if (childFormulaList.Count() == 0 && calculatedConcise != null)
                {
                    CmlFormula formula = new CmlFormula();
                    CmlUtils.Add(this, formula);
                    formula.Concise = calculatedConcise;
                }
                // iterate through child formulas
                foreach (CmlFormula childFormula in childFormulaList)
                {
                    if (calculatedConcise == null)
                    {
                        // we can't calculate a concise for this molecule so we can't 
                        // do any thing to the current concise
                    }
                    else
                    {
                        // has a concise?
                        if (childFormula.Concise != null)
                        {
                            childFormula.Concise = calculatedConcise;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// normalize concise formulas recursively upwards when the molecule is imported
        ///    if concise formulas are not present, add them (where possible)
        ///    if concise formulas are present accept them anyway regardless of conflicts
        /// </summary>
        public void NormalizeConciseFormulasOnImport()
        {
            // recurse through nested molecules
            IEnumerable<CmlMolecule> childMolecules = GetChildMoleculeList();
            if (childMolecules.Count() > 0)
            {
                CmlFormula newFormula = new CmlFormula();
                foreach (CmlMolecule childMolecule in childMolecules)
                {
                    childMolecule.NormalizeConciseFormulasOnImport();
                    CmlFormula childFormula = childMolecule.GetFirstConciseFormula();
                    if (childFormula != null)
                    {
                        if (childMolecule.Count != null)
                        {
                            childFormula.MultiplyBy((double) childMolecule.Count);
                        }
                        newFormula.Add(childFormula);
                    }
                }
                CmlFormula thisFormula = GetFirstConciseFormula();
                // no concise formula?
                if (thisFormula == null)
                {
                    CmlFormula formula = new CmlFormula {Concise = newFormula.Concise};
                    CmlUtils.Add(this, formula);
                }
            }
            else
            {
                // no child molecules 
                IEnumerable<CmlFormula> childFormulaList = GetChildFormulaList();
                string calculatedConcise = CmlFormula.CalculateConciseFormula(this);
                if (calculatedConcise == null)
                {
                    // we can't calculate a concise for this molecule so we 
                    // do nothing - this is what the user specified on import
                }
                else
                {
                    if (childFormulaList.Count() == 0)
                    {
                        CmlFormula formula = new CmlFormula();
                        CmlUtils.Add(this, formula);
                        formula.Concise = calculatedConcise;
                    }
                    else
                    {
                        foreach (CmlFormula childFormula in childFormulaList)
                        {
                            childFormula.Concise = calculatedConcise;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// recursively normalilze formal charge
        /// </summary>
        public void RecursivelyNormaliseCharge()
        {
            IEnumerable<CmlMolecule> childMolecules = GetChildMoleculeList();
            if (childMolecules.Count() > 0)
            {
                int newCharge = 0;
                foreach (CmlMolecule childMolecule in childMolecules)
                {
                    childMolecule.RecursivelyNormaliseCharge();
                    if (childMolecule.FormalCharge != null)
                    {
                        newCharge += childMolecule.FormalCharge.Value;
                    }
                }
                if (FormalCharge != null)
                {
                    if (newCharge != FormalCharge.Value)
                    {
                        // inconsistency but schema allows this
                    }
                }
                else
                {
                    FormalCharge = newCharge;
                }
            }
            else
            {
                int newCharge = 0;
                IEnumerable<CmlAtom> atomList = GetAtoms();
                foreach (CmlAtom atom in atomList)
                {
                    if (atom.FormalCharge != null)
                    {
                        newCharge += atom.FormalCharge.Value;
                    }
                }
                if (FormalCharge != null)
                {
                    if (newCharge != FormalCharge.Value)
                    {
                        // inconsistency but schema allows this
                    }
                }
                else
                {
                    FormalCharge = newCharge;
                }
            }
        }

        /// <summary>
        /// uses atom values to normalize molecule
        /// </summary>
        public void NormalizeChargeAndSpinMultiplicityFromAtoms()
        {
            FormalCharge = null;
            RecursivelyNormaliseSpinMultiplity();
            RecursivelyNormaliseCharge();
            NormalizeConciseFormulas();
        }

        /// <summary>
        /// recursively normalize spinMultiplicity
        /// </summary>
        public void RecursivelyNormaliseSpinMultiplity()
        {
            IEnumerable<CmlMolecule> childMolecules = GetChildMoleculeList();
            // nested molecules
            if (childMolecules.Count() > 0)
            {
                int newSpinMultiplicity = 0;
                foreach (CmlMolecule childMolecule in childMolecules)
                {
                    childMolecule.RecursivelyNormaliseSpinMultiplity();
                    if (childMolecule.SpinMultiplicity != null)
                    {
                        newSpinMultiplicity += childMolecule.SpinMultiplicity.Value;
                    }
                }
                if (SpinMultiplicity != null)
                {
                    if (newSpinMultiplicity != SpinMultiplicity.Value)
                    {
                        // inconsistency but schema allows this
                    }
                }
                else
                {
                    SpinMultiplicity = newSpinMultiplicity;
                }
            }
            else
            {
                int newSpinMultiplicity = 0;
                IEnumerable<CmlAtom> atomList = GetAtoms();
                foreach (CmlAtom atom in atomList)
                {
                    if (atom.SpinMultiplicity != null)
                    {
                        newSpinMultiplicity += atom.SpinMultiplicity.Value - 1;
                    }
                }
                newSpinMultiplicity += 1;
                if (SpinMultiplicity != null)
                {
                    if (newSpinMultiplicity != SpinMultiplicity.Value)
                    {
                        // inconsistency but schema allows this
                    }
                }
                else
                {
                    SpinMultiplicity = newSpinMultiplicity;
                }
            }
        }

        /// <summary>
        /// Determines whether the argument is a CMLLite molecule
        /// 
        /// at the moment this only test the name and namespace - in future
        /// this will check more
        /// </summary>
        /// <param name="element">the XElement to test</param>
        /// <returns>true if element is a CMLMolecule otherwise false</returns>
        public static bool IsMolecule(XElement element)
        {
            bool ok = false;
            if (element != null)
            {
                XName xName = CmlConstants.CmlxNamespace + Tag;
                ok = xName.Equals(element.Name);
            }
            return ok;
        }

        /// <summary>
        /// transform atoms
        /// skips those without coordinates
        /// </summary>
        /// <param name="transform"></param>
        public void Transform2D(Transform2 transform)
        {
            IEnumerable<CmlAtom> atoms = GetAllAtoms();
            foreach (CmlAtom atom in atoms)
            {
                atom.Transform2D(transform);
            }
        }

        /// <summary>
        /// selecta atoms with given elementType
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        public IEnumerable<CmlAtom> GetAtomsByElementType(PeriodicTable.Element el)
        {
            IEnumerable<CmlAtom> atoms = GetAllAtoms();
            List<CmlAtom> atomList = new List<CmlAtom>();
            foreach (CmlAtom atom in atoms)
            {
                if (el.Equals(PeriodicTable.GetElement(atom.ElementType)))
                {
                    atomList.Add(atom);
                }
            }
            return atomList;
        }

        /// <summary>
        /// renumbers atoms and bonds so as not to clash with any atoms in molecule
        /// new ids are of the form a123 
        /// </summary>
        /// <param name="molecule"></param>
        public void RenumberAtomsBonds(CmlMolecule molecule)
        {
            List<CmlMolecule> moleculeList = new List<CmlMolecule> {molecule};
            RenumberAtomsBonds(moleculeList);
        }

        /// <summary>
        /// renumbers atoms and bonds so as not to clash with any atoms in moleculeList
        /// new ids are of the form a123 
        /// </summary>
        /// <param name="moleculeList"></param>
        public void RenumberAtomsBonds(List<CmlMolecule> moleculeList)
        {
            if (moleculeList == null)
            {
                throw new ArgumentNullException("moleculeList");
            }
            const string prefix = CmlAtom.IDPrefix;
            int maxId = FindMaxId(moleculeList, prefix);
            Dictionary<string, string> newToOldMap = MakeOldToNewMap(prefix, maxId);
            RenumberAtomParities(newToOldMap);
            RenumberBondStereos(newToOldMap);
            RenumberBonds(newToOldMap);
            RenumberAtoms(newToOldMap);
        }

        private static int FindMaxId(IEnumerable<CmlMolecule> moleculeList, string prefix)
        {
            int maxId = 0;
            foreach (CmlMolecule molecule in moleculeList)
            {
                IEnumerable<CmlAtom> atomList = molecule.GetAllAtoms();
                foreach (CmlAtom atom in atomList)
                {
                    string id = atom.Id;
                    if (id.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        int ii;
                        if (int.TryParse(id.Substring(prefix.Length), out ii))
                        {
                            if (ii > maxId)
                            {
                                maxId = ii;
                            }
                        }
                    }
                }
            }
            return maxId;
        }

        private Dictionary<string, string> MakeOldToNewMap(string prefix, int maxId)
        {
            Dictionary<string, string> oldToNewMap = new Dictionary<string, string>();
            IEnumerable<CmlAtom> atoms = GetAllAtoms();
            int inew = maxId + 1;
            foreach (CmlAtom atom in atoms)
            {
                oldToNewMap.Add(atom.Id, prefix + inew++);
            }
            return oldToNewMap;
        }

        private void RenumberAtomParities(Dictionary<string, string> newToOldMap)
        {
            IEnumerable<XElement> atomParities = DelegateElement.XPathSelectElements(
                ".//*[local-name()='" + CmlAtomParity.Tag + "' and namespace-uri()='" + CmlConstants.Cmlns + "']");
            foreach (XElement ap in atomParities)
            {
                CmlAtomParity atomParity = new CmlAtomParity(ap);
                string[] atomRefs4 = atomParity.GetAtomRefs4();
                TranslateMappedValues(newToOldMap, atomRefs4);
                atomParity.SetAtomRefs4(atomRefs4);
            }
        }

        private void RenumberAtoms(IDictionary<string, string> newToOldMap)
        {
            IEnumerable<XElement> atoms = DelegateElement.XPathSelectElements(
                ".//*[local-name()='" + CmlAtom.Tag + "' and namespace-uri()='" + CmlConstants.Cmlns + "']");
            foreach (XElement a in atoms)
            {
                CmlAtom atom = new CmlAtom(a);
                string value;
                if (newToOldMap.TryGetValue(atom.Id, out value))
                {
                    atom.Id = value;
                }
            }
        }

        private void RenumberBondStereos(Dictionary<string, string> newToOldMap)
        {
            IEnumerable<XElement> bondStereos = DelegateElement.XPathSelectElements(
                ".//*[local-name()='" + CmlBondStereo.Tag + "' and namespace-uri()='" + CmlConstants.Cmlns + "']");
            foreach (XElement bs in bondStereos)
            {
                CmlBondStereo bondStereo = new CmlBondStereo(bs);
                string[] atomRefs4 = bondStereo.GetAtomRefs4();
                TranslateMappedValues(newToOldMap, atomRefs4);
                bondStereo.SetAtomRefs4(atomRefs4);
            }
        }

        private void RenumberBonds(Dictionary<string, string> newToOldMap)
        {
            IEnumerable<XElement> bonds = DelegateElement.XPathSelectElements(
                ".//*[local-name()='" + CmlBond.Tag + "' and namespace-uri()='" + CmlConstants.Cmlns + "']");
            foreach (XElement b in bonds)
            {
                CmlBond bond = new CmlBond(b);
                string[] atomRefs2 = bond.GetAtomRefs2();
                TranslateMappedValues(newToOldMap, atomRefs2);
                bond.SetAtomRefs2(atomRefs2);
                bond.GenerateAndAddId();
            }
        }

        private static void TranslateMappedValues(IDictionary<string, string> newToOldMap, string[] atomRefs4)
        {
            int i = 0;
            foreach (string aref in atomRefs4)
            {
                string value;
                if (newToOldMap.TryGetValue(aref, out value))
                {
                    atomRefs4[i++] = value;
                }
            }
        }

        /// <summary>
        /// removes the attribute from bonds indicating a/cyclic
        /// </summary>
        public void RemoveCyclicIndicators()
        {
            IEnumerable<CmlBond> bonds = GetAllBonds();
            foreach (CmlBond bond in bonds)
            {
                bond.RemoveCyclicIndicator();
            }
        }

        /// <summary>
        /// transfers atoms and bonds from moleculeToCopy
        /// destroys moleculeToCopy
        /// moleculeToCopy should already have been renumbered to avoid clashes
        /// </summary>
        /// <param name="moleculeToCopy"></param>
        public void TransferBondsAndAtomsToMolecule(CmlMolecule moleculeToCopy)
        {
            IEnumerable<CmlAtom> atoms = moleculeToCopy.GetAllAtoms();
            foreach (CmlAtom atom in atoms)
            {
                atom.DeleteSimple(false);
                AddAtom(atom);
            }
            IEnumerable<CmlBond> bonds = moleculeToCopy.GetAllBonds();
            foreach (CmlBond bond in bonds)
            {
                bond.DeleteSimple(false);
                AddBond(bond);
            }
        }

        /// <summary>
        /// substitutes bond atomId0->atomId1 by groupMolecule
        /// requires bond to be acyclic
        /// </summary>
        /// <param name="groupMolecule"></param>
        /// <param name="atomId0">atom to be kept</param>
        /// <param name="atomId1">atom to be</param>
        public void SubstituteBondByGroup(CmlMolecule groupMolecule, String atomId0, String atomId1)
        {
            CmlBond bond = GetBondByAtomRefs2(atomId0, atomId1);
            if (bond == null)
            {
                throw new NumboException("null bond: " + atomId0 + " ... " + atomId1);
            }
            CmlAtom replaceableAtom = GetAtomById(atomId1);
            if (replaceableAtom == null)
            {
                throw new NumboException("null atom: " + atomId1);
            }
            bond.ReplaceGroupWithGroup(replaceableAtom, groupMolecule);
        }

        /// <summary>
        /// create new atom and add to molecule
        /// add unique Id
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        public CmlAtom CreateAndAddAtomWithId(PeriodicTable.Element el)
        {
            CmlAtom atom = new CmlAtom();
            AddAtom(atom); // ensures unique Id
            atom.ElementType = el.ToString();
            return atom;
        }

        /// <summary>
        /// creates new bond and adds to molecule
        /// atoms must be non-null and distinct and belong to molecule
        /// adds new unique id
        /// </summary>
        /// <param name="atom1"></param>
        /// <param name="atom2"></param>
        /// <returns></returns>
        public CmlBond CreateAndAddBondWithId(CmlAtom atom1, CmlAtom atom2)
        {
            if (atom1 == null)
            {
                throw new ArgumentNullException("atom1", "Cannot form bonds from null atoms");
            }
            if (atom2 == null)
            {
                throw new ArgumentNullException("atom2", "Cannot form bonds from null atoms");
            }
            if (atom1.Id == atom2.Id)
            {
                throw new ArgumentException("Cannot form bond from same atom");
            }
            if (GetBondByAtomRefs2(atom1.Id, atom2.Id) != null)
            {
                throw new NumboException("Cannot add duplicate bond");
            }
            CmlBond bond = new CmlBond();
            bond.SetAtomRefs2(atom1, atom2);
            AddBond(bond); // generates Id
            return bond;
        }

        public CmlBond CreateAndAddBondWithId(CmlAtom atom1, CmlAtom atom2, string order)
        {
            CmlBond bond = CreateAndAddBondWithId(atom1, atom2);
            if (order != null)
            {
                bond.Order = order;
            }
            return bond;
        }

        /// <summary>
        /// remove all hydrogens connected to Dummy
        /// if no other ligands remove dummy
        /// </summary>
        private void TidyDummyAtoms()
        {
            Log.Debug("TIDY" + this);
            IEnumerable<CmlAtom> atoms = GetAllAtoms();
            DeleteHydrogensOnDummy(atoms);
            DeleteNakedDummies(atoms);
        }

        private void DeleteNakedDummies(IEnumerable<CmlAtom> atoms)
        {
            foreach (CmlAtom atom in atoms)
            {
                PeriodicTable.Element el = PeriodicTable.GetElement(atom.ElementType);
                if (PeriodicTable.Element.Du.Equals(el))
                {
                    IEnumerable<CmlAtom> ligands = atom.GetLigands();
                    if (ligands.Count().Equals(0))
                    {
                        atom.DeleteSimple(true);
                    }
                }
            }
        }

        private void DeleteHydrogensOnDummy(IEnumerable<CmlAtom> atoms)
        {
            foreach (CmlAtom atom in atoms)
            {
                PeriodicTable.Element el = PeriodicTable.GetElement(atom.ElementType);
                if (PeriodicTable.Element.Du.Equals(el))
                {
                    IEnumerable<CmlAtom> hLigands = atom.GetHydrogenLigands();
                    IEnumerable<CmlBond> hLigandBonds = atom.GetHydrogenLigandBonds();
                    for (int i = 0; i < hLigandBonds.Count(); i++)
                    {
                        hLigands.ElementAt(i).DeleteSimple(true);
                        hLigandBonds.ElementAt(i).DeleteSimple(true);
                    }
                }
            }
        }

        public void RemoveDummiesOrChangeToHForCommonAtoms(double stubScaleFactor)
        {
            IEnumerable<CmlAtom> dummies = GetAtomsByElementType(PeriodicTable.Element.Du);
            foreach (CmlAtom dummy in dummies)
            {
                IEnumerable<CmlBond> ligandBonds = dummy.GetLigandBonds();
                foreach (CmlBond ligandBond in ligandBonds)
                {
                    ligandBond.RemoveDummyOrChangeToHForCommonAtoms(stubScaleFactor);
                }
            }
        }

        public void DeleteAtomsAndHydrogensAndSplitHangingBonds(IEnumerable<XElement> atomPointers)
        {
            IEnumerable<CmlAtom> atomList = AddBondedHydrogensToGroup(CmlAtom.GetAtoms(atomPointers));
            HashSet<string> atomIdSet = new HashSet<string>();
            // all atoms to be deleted
            foreach (CmlAtom atom in atomList)
            {
                atomIdSet.Add(atom.Id);
            }
            HashSet<string> bondIdSet = new HashSet<string>();
            foreach (string atomId in atomIdSet)
            {
                CmlAtom atom = GetAtomById(atomId);
                IEnumerable<CmlAtom> ligandList = atom.GetLigands();
                IEnumerable<CmlBond> ligandBondsList = atom.GetLigandBonds();
                for (int i = 0; i < ligandList.Count(); i++)
                {
                    if (atomIdSet.Contains(ligandList.ElementAt(i).Id))
                    {
                        bondIdSet.Add(ligandBondsList.ElementAt(i).Id);
                    }
                }
            }

            List<string> immediateAtoms = new List<string>();
            // delay deletion if atom has connections outside set
            List<string> delayedAtoms = new List<string>();
            // only delete atom if all ligands are in atomIdSet
            SortAtomsForDeletion(atomIdSet, immediateAtoms, delayedAtoms);
            // delete bonds first
            foreach (string bondId in bondIdSet)
            {
                CmlBond bond = GetBondById(bondId);
                bond.DeleteSimple(true);
            }
            // then immediate atoms
            foreach (string atomId in immediateAtoms)
            {
                CmlAtom atom = GetAtomById(atomId);
                atom.DeleteSimple(true);
            }
            // atoms with bonds outside set
            foreach (string atomId in delayedAtoms)
            {
                CmlAtom atom = GetAtomById(atomId);
                ICollection<CmlBond> ligandBonds = atom.GetLigandBonds();
                foreach (CmlBond ligandBond in ligandBonds)
                {
                    ligandBond.DeleteAndAddBondsCappedWithDummy(CmlBond.StubScalefactor);
                    Log.Debug("LIG DELETE " + ligandBond.DelegateElement);
                    ligandBond.DeleteSimple(true);
                }
                ligandBonds = atom.GetLigandBonds();
                ICollection<CmlAtom> ligands = atom.GetLigands();
                for (int i = 0; i < ligandBonds.Count(); i++)
                {
                    ligandBonds.ElementAt(i).DeleteSimple(true);
                    ligands.ElementAt(i).DeleteSimple(true);
                }
                Log.Debug("ATOM DELETE " + atom.DelegateElement);
                atom.DeleteSimple(true);
            }
        }

        private void SortAtomsForDeletion(HashSet<string> atomIdSet, List<string> atomsForImmediateDeletion,
                                          List<string> atomsForDelayedDeletion)
        {
            foreach (string atomId in atomIdSet)
            {
                CmlAtom atom = GetAtomById(atomId);
                bool delete = true;
                IEnumerable<CmlAtom> ligands = atom.GetLigands();
                foreach (CmlAtom ligand in ligands)
                {
                    // ignore atom if has ligands outside delete set
                    if (!atomIdSet.Contains(ligand.Id))
                    {
                        delete = false;
                        break;
                    }
                }
                if (delete)
                {
                    atomsForImmediateDeletion.Add(atomId);
                }
                else
                {
                    atomsForDelayedDeletion.Add(atomId);
                }
            }
        }

        internal void RemoveDummyBondsAndProcessHydrogens(double stubScaleFactor, List<CmlBond> dummyBondList)
        {
            foreach (CmlBond dummyBond in dummyBondList)
            {
                dummyBond.RemoveDummyOrChangeToHForCommonAtoms(stubScaleFactor);
            }
            TidyDummyAtoms();
        }

        internal void SplitBondsAndFixChemistry(IEnumerable<XElement> bondPointers, double stubScaleFactor)
        {
            List<CmlBond> dummyBondList = SplitBondsAndCapEndsWithDummy(bondPointers, stubScaleFactor);
            RemoveDummyBondsAndProcessHydrogens(stubScaleFactor, dummyBondList);
        }

        private List<CmlBond> SplitBondsAndCapEndsWithDummy(IEnumerable<XElement> bondPointers,
                                                            double stubScaleFactor)
        {
            List<CmlBond> dummyBondList = new List<CmlBond>();
            foreach (XElement bondPointer in bondPointers)
            {
                CmlBond bond = new CmlBond(bondPointer);
                List<CmlBond> dummyBonds = bond.DeleteAndAddBondsCappedWithDummy(stubScaleFactor);
                dummyBondList.AddRange(dummyBonds);
            }
            return dummyBondList;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CheckAndFixStereochemistryOnImport()
        {
            // first ensure that all atomParities enforce bondStereos where possible
            // calculate if not present
            CalculateOrEnsureBondStereoCompatibleWithAtomParitiesOnImport();

            List<CmlBondStereo> wedgeHatchBondStereos = GetBondStereosWithCmlWedgeHatchConvention();
            CalculateAndAddMissingAtomParitiesFromBondStereoOnImport(wedgeHatchBondStereos);
        }

        /// <summary>
        /// called after any operation which might invert the Chirality of molecule or atoms
        /// </summary>
        public void CheckAndFixStereochemistry()
        {
            // TODO check logic for NON import
            // first ensure that all atomParities enforce bondStereos where possible
            // calculate if not present
            CalculateOrEnsureBondStereoCompatibleWithAtomParitiesOnImport();

            List<CmlBondStereo> wedgeHatchBondStereos = GetBondStereosWithCmlWedgeHatchConvention();
            if (wedgeHatchBondStereos.Count() > 0)
            {
                CalculateAndAddMissingAtomParitiesFromBondStereoOnImport(wedgeHatchBondStereos);
            }
        }

        private List<CmlBondStereo> GetBondStereosWithCmlWedgeHatchConvention()
        {
            // now go through bondStereo and ONLY process those without atomParities
            IEnumerable<XElement> possibleWedgeHatchBondStereoPointers = this.DelegateElement.XPathSelectElements(
                ".//*[local-name()='" + CmlBondStereo.Tag + "' and namespace-uri()='" + CmlConstants.Cmlns +
                "' and (. = '"+CmlBondStereo.Wedge+"' or . = '"+CmlBondStereo.Hatch+"')]");
            List<CmlBondStereo> wedgeHatchBondStereos = new List<CmlBondStereo>();
            foreach (XElement bondStereoX in possibleWedgeHatchBondStereoPointers)
            {
                CmlBondStereo bondStereo = new CmlBondStereo(bondStereoX);
                wedgeHatchBondStereos.Add(bondStereo);
                
            }
            return wedgeHatchBondStereos;
        }

        private void CalculateOrEnsureBondStereoCompatibleWithAtomParitiesOnImport()
        {
            foreach (CmlAtom atom in GetAtoms())
            {
                CmlAtomParity atomParity = atom.GetAtomParity();
                if (atomParity != null)
                {
                    atom.CalculateOrEnsureBondStereoCompatibleWithAtomParity();
                }
            }
        }

        private void CalculateAndAddMissingAtomParitiesFromBondStereoOnImport(List<CmlBondStereo> wedgeHatchBondStereos)
        {
            foreach (CmlBondStereo bondStereo in wedgeHatchBondStereos)
            {
                CmlAtom atom = bondStereo.GetAtomAtSharpEnd();
                if (atom.GetAtomParity() == null)
                {
                    CmlAtomParity atomParity =
                        bondStereo.CreateNewAtomParityForAtomAtSharpEndWithAtomRefs4InLigandOrder();
//                    CMLAtomParity atomParity = atom.CreatNewAtomParityFromBondStereoWithAnticlockwiseLigands(bondStereo);
                    CmlUtils.Add(atom, atomParity);
                }
            }
        }

        /// <summary>
        /// delete group of atoms and cap any hanging bonds to common elements
        /// </summary>
        /// <param name="atomPointers"></param>
        public void DeleteAtomsAndCapWithHydrogenForCommonElements(IEnumerable<XElement> atomPointers)
        {
            DeleteAtomsAndHydrogensAndSplitHangingBonds(atomPointers);
            // remove dummies and cap with H
            RemoveDummiesOrChangeToHForCommonAtoms(CmlBond.StubScalefactor);
        }

        /// <summary>
        /// get single external bond
        /// </summary>
        /// <param name="atoms"></param>
        /// <returns></returns>
        public CmlBond GetSingleExternalAcyclicBond(IEnumerable<CmlAtom> atoms)
        {
            CmlBond externalBond = null;
            IEnumerable<CmlBond> externalBonds = GetExternalBonds(atoms);
            if (externalBonds.Count() == 1)
            {
                externalBond = externalBonds.ElementAt(0);
                if (!(CmlBond.Cyclicity.Acyclic.Equals(externalBond.ForceGetCyclicIndicator())))
                {
                    externalBond = null;
                }
            }
            return externalBond;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedAtomPointers"></param>
        /// <returns></returns>
        public CmlAtom GetEndOfSingleExternalAcyclicBond(IEnumerable<XElement> selectedAtomPointers)
        {
            CmlAtom atom = null;
            IEnumerable<CmlAtom> atomList = CmlAtom.GetAtoms(selectedAtomPointers);
            IEnumerable<CmlAtom> atoms = AddBondedHydrogensToGroup(atomList);
            CmlBond bond = GetSingleExternalAcyclicBond(atoms);
            if (bond != null)
            {
                atom = bond.GetAtoms().ElementAt(0);
                if (atoms.Contains(atom))
                {
                    atom = bond.GetAtomAtOtherEnd(atom);
                }
            }
            return atom;
        }

        public void AddElectron()
        {
            int fc = FormalCharge ?? 0;
            FormalCharge = fc - 1;
            int sm = SpinMultiplicity ?? 1;
            if (sm == 1)
            {
                SpinMultiplicity = 2;
            }
            else
            {
                SpinMultiplicity = sm - 1;
            }
        }

        public void RemoveElectron()
        {
            int fc = FormalCharge ?? 0;
            FormalCharge = fc + 1;
            int sm = SpinMultiplicity ?? 1;
            if (sm == 1)
            {
                SpinMultiplicity = 2;
            }
            else
            {
                SpinMultiplicity = sm - 1;
            }
        }

        internal void Normalize()
        {
            NormalizeConciseFormulas();
            NormalizeChargeAndSpinMultiplicityFromAtoms();
            CalculateAndSetCyclicIndicators();
            CheckAndFixStereochemistry();
        }

        /// <summary>
        /// annotates names and formulae
        /// </summary>
        internal void AnnotateOutdated()
        {
            IEnumerable<XElement> namesX = DelegateElement.XPathSelectElements(
                ".//*[local-name()='" + CmlName.Tag + "' and namespace-uri()='" + CmlConstants.Cmlns + "']");
            foreach (XElement nameX in namesX)
            {
                CmlName name = new CmlName(nameX);
                name.AnnotateOutdated();
            }
            // inline formulae
            IEnumerable<XElement> formulasX = DelegateElement.XPathSelectElements(
                ".//*[local-name()='" + CmlFormula.Tag + "' and namespace-uri()='" + CmlConstants.Cmlns +
                "' and @inline]");
            foreach (XElement formulaX in formulasX)
            {
                CmlFormula formula = new CmlFormula(formulaX);
                formula.AnnotateOutdated();
            }
        }

        /// <summary>
        /// gets list of atoms
        /// </summary>
        /// <param name="atomIds">ids - does not check uniqueness</param>
        /// <exception cref="NumboException">id not found</exception>
        /// <returns>list of atoms - could include duplicates</returns>
        internal IEnumerable<CmlAtom> GetAtomsById(string[] atomIds)
        {
            List<CmlAtom> atomList = new List<CmlAtom>();
            foreach (string atomId in atomIds)
            {
                CmlAtom atom = GetAtomById(atomId);
                if (atom == null)
                {
                    throw new NumboException("atom not found: " + atomId);
                }
                atomList.Add(atom);
            }
            return atomList;
        }

        /// <summary>
        /// gets list of bonds
        /// </summary>
        /// <param name="bondIds">ids - does not check uniqueness</param>
        /// <exception cref="NumboException">id not found</exception>
        /// <returns>list of bonds - could include duplicates</returns>
        internal IEnumerable<CmlBond> GetBondsById(string[] bondIds)
        {
            List<CmlBond> bondList = new List<CmlBond>();
            foreach (string bondId in bondIds)
            {
                CmlBond bond = GetBondById(bondId);
                if (bond == null)
                {
                    throw new NumboException("bond not found: " + bondId);
                }
                bondList.Add(bond);
            }
            return bondList;
        }

        internal void RemoveAllWedgeHatchBondStereos()
        {
            IEnumerable<CmlBond> bonds = this.GetBonds();
            foreach (CmlBond bond in bonds)
            {
                bond.DeleteAllWedgeHatchBondStereoChildren();
            }
        }

        public void ChangeFormalChargeBy(int delta)
        {
            int fc = (FormalCharge.HasValue) ? FormalCharge.Value : 0;
            FormalCharge = fc + delta;
        }
    }
}