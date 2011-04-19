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
using Euclid;
using log4net;
using Numbo.Cml.Helpers;

namespace Numbo.Cml
{
    /// <summary>
    /// supports CML atom element
    /// </summary>
    public class CmlAtom : CmlElement, IHasCentroid2, IEquatable<CmlAtom>
    {
        public const string CmlxGroup = CmlConstants.CmlxPrefix + ":" + Group;
        public const string CmlxGroupPointer = CmlConstants.CmlxPrefix + ":" + GroupPointer;
        public const string Group = "group";
        public const string GroupPointer = Group + "Pointer";
        public const string IDPrefix = "a";
        public const string Tag = "atom";
        private static readonly ILog Log = LogManager.GetLogger(typeof (CmlAtom));

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// restricted access; use moleucle.CreateAndAddAtomWithId instead
        /// </summary>
        internal CmlAtom()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlAtom(XElement element)
            : base(element)
        {}

        /// <summary>
        /// sets elementType
        /// </summary>
        public string ElementType
        {
            get
            {
                XAttribute att = DelegateElement.Attribute(CmlAttribute.ElementType);
                return (att == null) ? null : att.Value;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.ElementType, value); }
        }

        /// <summary>
        /// count
        /// </summary>
        public Nullable<double> Count
        {
            get
            {
                XAttribute countAttribute = DelegateElement.Attribute(CmlAttribute.Count);
                return (countAttribute == null || countAttribute.Value == null ||
                        string.Empty.Equals(countAttribute.Value))
                           ?
                               (double?) null
                           : double.Parse(countAttribute.Value, CultureInfo.InvariantCulture);
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
                    delta = -charge;
                }
                CmlMolecule parent = CmlUtils.GetFirstAncestorMolecule(this);
                if (parent != null)
                {
                    parent.ChangeFormalChargeBy(delta);
                }
            }
        }

        /// <summary>
        /// get/ set X2 coordinate
        /// </summary>
        public double? X2
        {
            get { return GetCoord(CmlAttribute.X2); }
            set { SetCoord(CmlAttribute.X2, (double) value); }
        }

        /// <summary>
        /// get/ set Y2 coordinate
        /// </summary>
        public double? Y2
        {
            get { return GetCoord(CmlAttribute.Y2); }
            set { SetCoord(CmlAttribute.Y2, (double) value); }
        }

        /// <summary>
        /// get/ set X3 coordinate
        /// </summary>
        public double? X3
        {
            get { return GetCoord(CmlAttribute.X3); }
            set { SetCoord(CmlAttribute.X3, (double) value); }
        }

        /// <summary>
        /// get/ set Y3 coordinate
        /// </summary>
        public double? Y3
        {
            get { return GetCoord(CmlAttribute.Y3); }
            set { SetCoord(CmlAttribute.Y3, (double) value); }
        }

        /// <summary>
        /// get/ set Y3 coordinate
        /// </summary>
        public double? Z3
        {
            get { return GetCoord(CmlAttribute.Z3); }
            set { SetCoord(CmlAttribute.Z3, (double) value); }
        }

        /// <summary>
        /// isotopeNumber 
        /// </summary>
        public Nullable<int> IsotopeNumber
        {
            get
            {
                XAttribute att = DelegateElement.Attribute(CmlAttribute.IsotopeNumber);
                return (att == null || att.Value == null || string.Empty.Equals(att.Value))
                           ?
                               (int?) null
                           : int.Parse(att.Value, CultureInfo.InvariantCulture);
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.IsotopeNumber, value); }
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
        /// X2 / Y2 pair
        /// </summary>
        public Point2 Point2
        {
            get
            {
                return (this.X2 != null && this.Y2 != null)
                           ?
                               new Point2((double) this.X2, (double) this.Y2)
                           : null;
            }
            set
            {
                this.X2 = value.X;
                this.Y2 = value.Y;
            }
        }

        public bool Equals(CmlAtom other)
        {
            return this.DelegateElement.Equals(other.DelegateElement);
        }

        /// <summary>
        /// returns coordinate of atom
        /// </summary>
        /// <returns>null if no coordinate</returns>
        public Point2 GetCentroid()
        {
            return this.Point2;
        }

        /// <summary>
        /// returns null
        /// </summary>
        /// currently atoms have no structure
        /// <returns></returns>
        public ICollection<IHasCentroid2> GetSubCentroids()
        {
            return null;
        }

        public override string GetTag()
        {
            return Tag;
        }

        private double? GetCoord(string attName)
        {
            XAttribute att = DelegateElement.Attribute(attName);
            return (att == null || att.Value == null || string.Empty.Equals(att.Value))
                       ?
                           (double?) null
                       : double.Parse(att.Value, CultureInfo.InvariantCulture);
        }

        private void SetCoord(string attName, double value)
        {
            DelegateElement.SetAttributeValue(attName, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Gets the (first) atom parity child of this element - or null if none is defined
        /// </summary>
        /// <returns>the first CMLAtomParity child of this atom or null</returns>
        public CmlAtomParity GetAtomParity()
        {
            CmlAtomParity atomParity = null;
            XElement xElement =
                DelegateElement.XPathSelectElement(".//*[local-name()='" + CmlAtomParity.Tag +
                                                                "' and namespace-uri()='" + CmlConstants.Cmlns + "'][1]");
            if (xElement != null)
            {
                atomParity = new CmlAtomParity(xElement);
            }
            return atomParity;
        }

        /// <summary>
        /// 2D distance between 2 atoms
        /// </summary>
        /// <param name="atom"></param>
        /// <returns></returns>
        public double GetDistance(CmlAtom atom)
        {
            if (atom == null)
            {
                throw new ArgumentNullException("atom", "null atom in GetDistance");
            }
            return Point2.GetDistance(this.Point2, atom.Point2);
        }

        /// <summary>
        /// get angle between 3 atoms
        /// </summary>
        /// <exception cref="ArgumentNullException">if atoms are null</exception>
        /// <exception cref="ArgumentException">if any two atoms are identical</exception>
        /// <param name="atom0"></param>
        /// <param name="atom1"></param>
        /// <param name="atom2"></param>
        /// <param name="epsilon"></param>
        /// <returns>null if no coordinates</returns>
        public static Angle GetAngle(CmlAtom atom0, CmlAtom atom1, CmlAtom atom2, double epsilon)
        {
            if (atom0 == null)
            {
                throw new ArgumentNullException("atom0", "null atom(s) in GetAngle");
            }
            if (atom1 == null)
            {
                throw new ArgumentNullException("atom1", "null atom(s) in GetAngle");
            }
            if (atom2 == null)
            {
                throw new ArgumentNullException("atom2", "null atom(s) in GetAngle");
            }
            if (atom0 == atom1)
            {
                throw new ArgumentException("atom0 = atom1 in GetAngle");
            }
            if (atom1 == atom2)
            {
                throw new ArgumentException("atom2 = atom1 in GetAngle");
            }
            return Point2.GetAngle(atom0.Point2, atom1.Point2, atom2.Point2, epsilon);
        }

        /// <summary>
        /// get list of ligand ids in the order in which the bonds occur.
        /// lexical value of id is irrelevant
        /// PMR: changed 2009-06-15
        /// </summary>
        /// <returns>if no ligands, empty ICollection, not null</returns>
        public ICollection<string> GetLigandIds()
        {
            ICollection<CmlAtom> atoms = GetLigands();
            ICollection<string> ids = new List<string>(atoms.Count());
            foreach (CmlAtom atom in atoms)
            {
                ids.Add(atom.Id);
            }
            return ids;
        }

        /// <summary>
        /// get bond-order sum
        /// assumes all bonds are S D T A
        /// if not tries anyway to get a reasonable int
        /// </summary>
        /// <returns>sum of bonds</returns>
        public Nullable<int> GetBondOrderSum()
        {
            ICollection<CmlBond> bonds = GetLigandBonds();
            Nullable<int> sum = 0;
            double sumD = 0.0;
            foreach (CmlBond bond in bonds)
            {
                if (bond.BondOrder == null || bond.BondOrder.NumericBondOrder == null)
                {
                    sum = null;
                    break;
                }
                sumD += (double) bond.BondOrder.NumericBondOrder;
            }
            if (sum == null)
            {}
            else if (sumD < 0.25)
            {
                sum = 0;
            }
            else if (sumD < 1.25)
            {
                sum = 1;
            }
            else if (sumD < 2.25)
            {
                sum = 2;
            }
            else if (sumD < 3.25)
            {
                sum = 3;
            }
            else if (sumD < 4.6)
            {
                sum = 4;
            }
            else if (sumD < 5.2)
            {
                sum = 5;
            }

            return sum;
        }

        // TODO       getStereoHydrogens();

        // TODO        checkBondStereoAgainstGeometry();

        /// <summary>
        /// get list of ligands in the order in which the bonds occur.
        /// </summary>
        /// <returns>if no ligands, empty List, not null</returns>
        public ICollection<CmlAtom> GetHydrogenLigands()
        {
            ICollection<CmlAtom> ligands = this.GetLigands();
            ICollection<CmlAtom> hydrogenLigands = new List<CmlAtom>(ligands.Count);
            foreach (CmlAtom atom in ligands)
            {
                if (PeriodicTable.Element.H.ToString().Equals(atom.ElementType))
                {
                    hydrogenLigands.Add(atom);
                }
            }
            return hydrogenLigands;
        }

        /// <summary>
        /// get list of non-hydrogen ligands in the order in which the bonds occur.
        /// </summary>
        /// <returns>if no ligands, empty List, not null</returns>
        public ICollection<CmlAtom> GetNonHydrogenLigands()
        {
            ICollection<CmlAtom> ligands = this.GetLigands();
            ICollection<CmlAtom> nonHydrogenLigands = new List<CmlAtom>(ligands.Count);
            foreach (CmlAtom atom in ligands)
            {
                if (!(PeriodicTable.Element.H.ToString().Equals(atom.ElementType)))
                {
                    nonHydrogenLigands.Add(atom);
                }
            }
            return nonHydrogenLigands;
        }

        /// <summary>
        /// get list of bonds to non-hydrogen ligands in the order in which the bonds occur.
        /// </summary>
        /// <returns>if no ligands, empty List, not null</returns>
        public ICollection<CmlBond> GetNonHydrogenLigandBonds()
        {
            ICollection<CmlBond> ligandBonds = this.GetLigandBonds();
            ICollection<CmlBond> nonHydrogenLigandBonds = new List<CmlBond>(ligandBonds.Count);
            foreach (CmlBond ligandBond in ligandBonds)
            {
                CmlAtom otherAtom = ligandBond.GetAtomAtOtherEnd(this);
                if (!PeriodicTable.Element.H.ToString().Equals(otherAtom.ElementType))
                {
                    nonHydrogenLigandBonds.Add(ligandBond);
                }
            }
            return nonHydrogenLigandBonds;
        }

        /// <summary>
        /// get list of ligands in the order in which the bonds occur.
        /// </summary>
        /// <returns>if no ligands, empty List, not null</returns>
        public ICollection<CmlBond> GetHydrogenLigandBonds()
        {
            ICollection<CmlBond> ligandBonds = this.GetLigandBonds();
            ICollection<CmlBond> hydrogenLigandBonds = new List<CmlBond>(ligandBonds.Count);
            foreach (CmlBond ligandBond in ligandBonds)
            {
                CmlAtom otherAtom = ligandBond.GetAtomAtOtherEnd(this);
                if (PeriodicTable.Element.H.ToString().Equals(otherAtom.ElementType))
                {
                    hydrogenLigandBonds.Add(ligandBond);
                }
            }
            return hydrogenLigandBonds;
        }

        /// <summary>
        /// get list of ligands in the order in which the bonds occur.
        /// </summary>
        /// <returns>if no ligands, empty List, not null</returns>
        public ICollection<CmlAtom> GetLigands()
        {
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
            if (molecule == null)
            {
                throw new NumboException("No ancestor molecule: " + this.DelegateElement);
            }
            ICollection<CmlBond> bonds = molecule.GetBonds();
            HashSet<CmlAtom> ligands = new HashSet<CmlAtom>();
            foreach (CmlBond bond in bonds)
            {
                if (bond.GetAtomRefs2().Contains(Id))
                {
                    CmlAtom atomAtOtherEnd = bond.GetAtomAtOtherEnd(this);
                    if (atomAtOtherEnd == null)
                    {
                        throw new NumboException("No atom at other end: " + bond.DelegateElement);
                    }
                    ligands.Add(atomAtOtherEnd);
                }
            }
            return ligands.ToList();
        }

        /// <summary>
        /// get list of bonds to ligands in the order in which the bonds occur.
        /// PMR: 2009-06-17 not yet tested
        /// </summary>
        /// <returns>if no ligands, empty List, not null</returns>
        public ICollection<CmlBond> GetLigandBonds()
        {
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
            if (molecule == null)
            {
                throw new NumboException("No ancestor molecule: " + this.DelegateElement);
            }
            ICollection<CmlAtom> ligands = this.GetLigands();
            ICollection<CmlBond> ligandBonds = new List<CmlBond>(ligands.Count);
            foreach (CmlBond bond in molecule.GetAllBonds())
            {
                if (bond.GetAtomRefs2().Contains(this.Id))
                {
                    ligandBonds.Add(bond);
                }
            }

            return ligandBonds;
        }

        /// <summary>
        /// get list of bond ids to ligands in the order in which the bonds occur.
        /// lexical value of id is irrelevant
        /// </summary>
        /// <returns>if no ligands, empty string[], not null</returns>
        public ICollection<string> GetLigandBondIds()
        {
            ICollection<CmlBond> bonds = GetLigandBonds();
            ICollection<string> ids = new List<string>(bonds.Count);
            foreach (CmlBond bond in bonds)
            {
                ids.Add(bond.Id);
            }
            return ids;
        }

        /// <summary>
        /// transforms a IEnumerable of pointers into atoms
        /// </summary>
        /// <param name="atomPointers">atom pointers</param>
        /// <returns>IEnumerable of CMLAtoms</returns>
        public static IEnumerable<CmlAtom> GetAtoms(IEnumerable<XElement> atomPointers)
        {
            List<CmlAtom> atoms = null;
            if (atomPointers != null)
            {
                atoms = new List<CmlAtom>();
                foreach (XElement atomPointer in atomPointers)
                {
                    atoms.Add(new CmlAtom(atomPointer));
                }
            }
            return atoms;
        }

        /// <summary>
        /// transforms a IEnumerable of atoms into pointers
        /// </summary>
        /// <param name="atoms">atoms</param>
        /// <returns>IEnumerable of XElement atomPointers</returns>
        public static IEnumerable<XElement> GetAtomPointers(IEnumerable<CmlAtom> atoms)
        {
            List<XElement> atomPointers = null;
            if (atoms != null)
            {
                atomPointers = new List<XElement>();
                foreach (CmlAtom atom in atoms)
                {
                    atomPointers.Add(atom.DelegateElement);
                }
            }
            return atomPointers;
        }

        /// <summary>
        ///  deletes delegate atom and removes hanging bonds
        ///  leaves ligand atoms in a mess
        /// </summary>
        internal void DeleteAndRemoveHangingBonds()
        {
            ICollection<CmlBond> ligands = this.GetLigandBonds();
            foreach (CmlBond ligandBond in ligands)
            {
                ligandBond.DeleteSimple(true);
            }
            this.DeleteSimple(true);
        }

        public void ForceSubstituteElement(PeriodicTable.Element newElement)
        {
            ElementType = newElement.ToString();
        }

        private void DeleteHydrogenLigandsAndBonds()
        {
            ICollection<CmlBond> ligandBonds = this.GetHydrogenLigandBonds();
            ICollection<CmlAtom> ligands = this.GetHydrogenLigands();
            foreach (CmlBond ligandBond in ligandBonds)
            {
                ligandBond.DeleteSimple(true);
            }
            foreach (CmlAtom ligand in ligands)
            {
                ligand.DeleteSimple(true);
            }
        }

        /// <summary>
        /// get vector from this to atom.
        /// i.e. atom.Poinmt2.subtract(this.Point2)
        /// </summary>
        /// <param name="atom"></param>
        /// <returns>null if atom==null or any Point2 is null</returns>
        public Vector2 GetVectorTo(CmlAtom atom)
        {
            Vector2 vector = null;
            Point2 thisPoint = this.Point2;
            Point2 atomPoint = (atom == null) ? null : atom.Point2;
            if (thisPoint != null)
            {
                vector = atomPoint.Subtract(thisPoint);
            }
            return vector;
        }

        /// <summary>
        /// gets ringNucleus in which atom appears
        /// </summary>
        /// TODO not tested on spiro atoms
        /// <returns>null if atom is null or not in any ringNucleus</returns>
        public RingNucleus GetRingNucleus()
        {
            RingNucleus ringNucleus = null;
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
            if (molecule != null)
            {
                IEnumerable<RingNucleus> ringNucleusList = RingNucleus.CreateRingNucleusList(molecule);
                foreach (RingNucleus rn in ringNucleusList)
                {
                    if (rn.Contains(this))
                    {
                        ringNucleus = rn;
                        break;
                    }
                }
            }
            return ringNucleus;
        }

        /// <summary>
        /// gets rings in which atom appears
        /// </summary>
        /// <returns>gets rings in which atom appears or empty list if not in any rings</returns>
        public IEnumerable<Ring> GetRingList()
        {
            List<Ring> ringList = new List<Ring>();
            RingNucleus ringNucleus = GetRingNucleus();
            if (ringNucleus != null)
            {
                ICollection<Ring> rList = ringNucleus.RingList;
                foreach (Ring ring in rList)
                {
                    if (ring.Contains(this))
                    {
                        ringList.Add(ring);
                    }
                }
            }
            return ringList;
        }

        /// <summary>
        /// transforms atoms
        /// 
        /// TODO this needs refactoring - what if the atoms do not have 2d coodinates?
        /// 
        /// TODO this needs moving - it shouldn't be in Atom.. maybe in Molecule but probably
        /// it can move up to CID - it does not require chemistry but can transform anything with 
        /// 2D coordinates 
        /// </summary>
        /// <param name="molecule">reference molecule</param>
        /// <param name="atomIds">atoms to transform (as ids)</param>
        /// <param name="transform">transform</param>
        public static void TransformAtoms(CmlMolecule molecule, IEnumerable<string> atomIds, Transform2 transform)
        {
            foreach (string atomId in atomIds)
            {
                CmlAtom atom = molecule.GetAtomById(atomId);
                atom.Transform2D(transform);
            }
        }

        /// <summary>
        /// transform coordinates
        /// skips if noo coordinates
        /// </summary>
        /// <param name="transform"></param>
        public void Transform2D(Transform2 transform)
        {
            Point2 point = this.Point2;
            if (point != null)
            {
                point = transform.Transform(point);
                this.Point2 = point;
            }
        }

        public void RemoveIsotope()
        {
            this.IsotopeNumber = null;
            //            this.GetMolecule().update();
        }

        /// <summary>
        /// calculate missing bondstereo from atomParity
        /// (else) check consistency of bondStereo and calculate missing atomParity
        /// (else) check the two are consistent
        /// </summary>
        public void CalculateOrEnsureBondStereoCompatibleWithAtomParity()
        {
            CmlAtomParity atomParity = this.GetAtomParity();
            IEnumerable<CmlBond> ligandList = this.GetLigandBonds();
            int nBondStereo = 0;
            List<CmlBondStereo> bondStereoList = new List<CmlBondStereo>();

            foreach (CmlBond ligand in ligandList)
            {
                CmlBondStereo bondStereo = ligand.GetBondStereo();
                if (bondStereo != null)
                {
                    nBondStereo++;
                }
                bondStereoList.Add(bondStereo);
            }
            if (nBondStereo == 0)
            {
                if (atomParity != null)
                {
                    this.AddFirstWedgeableBondValue(atomParity);
                }
                // else no-op
            }
            else if (atomParity == null)
            {
                ComputeParityFromBondStereoOfLigands();
            }
            else
            {
                bool areCompatible = CheckParityAndBondStereo(atomParity);
                if (!areCompatible)
                {
                    throw new NumboException("Inconsistent atomParity on atom (" + Id + ")");
                }
            }
        }

        private void AddFirstWedgeableBondValue(CmlAtomParity atomParity)
        {
            var wedgeableBond = GetFirstWedgeableBond();
            if (wedgeableBond != null)
            {
                var bondStereoIndicator = atomParity.CalculateBondStereoIndicator(wedgeableBond);
                wedgeableBond.SetSharpEndOfBondToAtom(this);
                var bondStereo = new CmlBondStereo();
                CmlUtils.AddOnlyChild(wedgeableBond, bondStereo);
                bondStereo.SetValue(bondStereoIndicator);
            }
        }

        public CmlAtomParity ComputeParityFromBondStereoOfLigands()
        {
            CmlAtomParity atomParity = null;
            var ligandBondList = this.GetLigandBonds();
            foreach (var bondStereo in
                ligandBondList.Select(ligandBond => ligandBond.GetBondStereo()).Where(bondStereo => bondStereo != null)) {
                atomParity = bondStereo.CreateNewAtomParityForAtomAtSharpEndWithAtomRefs4InLigandOrder();
                if (atomParity != null)
                {
                    break;
                }
            }
            if (atomParity != null)
            {
//                CheckParityAndBondStereo(atomParity);
            }
            return atomParity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="atomParity"></param>
        /// <returns></returns>
        private bool CheckParityAndBondStereo(CmlAtomParity atomParity)
        {
            var ok = true;
            var molecule = CmlUtils.GetFirstAncestorMolecule(this);
            var ligandBondList = GetLigandBonds();
            foreach (var ligandBond in ligandBondList)
            {
                Log.Debug("ligandBond " + ligandBond);
                var bondStereo = ligandBond.GetBondStereo();
                if (bondStereo != null && this.Equals(bondStereo.GetAtomAtSharpEnd()))
                {
                    var calculatedParity =
                        bondStereo.CreateNewAtomParityForAtomAtSharpEndWithAtomRefs4InLigandOrder();
                    Log.Debug(calculatedParity);
                    ok = (calculatedParity.GetSign() == atomParity.GetSign());
                    if (!ok)
                    {
                        Log.Debug(molecule);
                        Log.Debug("problem with ligandBond " + ligandBond);
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// iterates over ligand bonds until a suitable one for a wedge is found
        /// (a) looks for first acyclic bond to non-H not already used
        /// (else) looks for first acyclic bond to H not already used
        /// (else) takes first cyclic bond not already used
        /// (else) sits down and howls (actually return null)
        /// </summary>
        /// <returns>null if cannot find one</returns>
        /// could be private
        public CmlBond GetFirstWedgeableBond()
        {
            CmlBond wedgeableBond = null;
            ICollection<CmlBond> ligandBondList = this.GetLigandBonds();
            foreach (CmlBond ligandBond in ligandBondList)
            {
                // don't use cyclic bonds
                if (ligandBond.ForceGetCyclicIndicator().Equals(CmlBond.Cyclicity.Cyclic))
                {
                    continue;
                }
                CmlAtom otherAtom = ligandBond.GetAtomAtOtherEnd(this);
                PeriodicTable.Element elementType = PeriodicTable.GetElement(otherAtom.ElementType);
                if (PeriodicTable.Element.H.Equals(elementType))
                {
                    // remember latest H ligand
                    wedgeableBond = ligandBond;
                }
                else
                {
                    CmlBondStereo bondStereo = ligandBond.GetBondStereo();
                    // already used?
                    if (bondStereo != null)
                    {
                        continue;
                    }
                    wedgeableBond = ligandBond;
                    break;
                }
            }
            // force to first unused bond
            if (wedgeableBond == null)
            {
                foreach (CmlBond ligandBond in ligandBondList)
                {
                    CmlBondStereo bondStereo = ligandBond.GetBondStereo();
                    // already used?
                    if (bondStereo != null)
                    {
                        continue;
                    }
                    wedgeableBond = ligandBond;
                    break;
                }
            }
            return wedgeableBond;
        }

        internal void ChangeElementTypeAndFixHydrogensForCommonElements(string newElementType)
        {
            var newElement = PeriodicTable.GetElement(newElementType);
            var thisElement = PeriodicTable.GetElement(this.ElementType);
            // change within common set
            if (CmlMolecule.CommonElementSet.Contains(thisElement) &&
                CmlMolecule.CommonElementSet.Contains(newElement))
            {
                var nHyd = PeriodicTable.CalculateCommonGroupDifference(newElement, thisElement);
                if (nHyd > 0)
                {
                    AddNakedHydrogensAndAdjustCoordinates(nHyd);
                    ElementType = newElementType;
                }
                else if (GetHydrogenLigands().Count() >= -nHyd)
                {
                    DeleteHydrogen(-nHyd);
                    ElementType = newElementType;
                }
            }
                // change to R or NextCommon
            else if (PeriodicTable.Element.R.Equals(newElement) ||
                     CmlMolecule.NextCommonElementSet.Contains(newElement))
            {
                // clear everything
                ClearProperties();
                ElementType = newElementType;
            }
                // everything else
            else
            {
                // clear everything
                ClearProperties();
                ElementType = newElementType;
            }
        }

        private void ClearProperties()
        {
            DeleteHydrogenLigandsAndBonds();
            FormalCharge = 0;
            RemoveIsotope();
        }

        /// <summary>
        ///  could be private
        /// </summary>
        internal void CreateOrAdjustHydrogenCoordinates()
        {
            IEnumerable<CmlAtom> hydrogens = GetHydrogenLigands();
            IEnumerable<CmlAtom> nonHydrogens = GetNonHydrogenLigands();
            ComputeAndAdd2DCoordinates(nonHydrogens, hydrogens);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fixedAtoms"></param>
        /// <param name="movingAtoms"></param>
        /// could be private
        internal void ComputeAndAdd2DCoordinates(IEnumerable<CmlAtom> fixedAtoms,
                                                 IEnumerable<CmlAtom> movingAtoms)
        {
            if (movingAtoms.Count() == 0)
            {
                return; // nothing to do
            }
            if (fixedAtoms.Count() == 0)
            {
                Point2 p = this.CreatePointAtBondedDistance(new Vector2(0.0, 1.0), CmlBond.DefaultLength);
                movingAtoms.ElementAt(0).Point2 = p;
                List<CmlAtom> newFixedAtoms = new List<CmlAtom> {movingAtoms.ElementAt(0)};
                List<CmlAtom> newMovingAtoms = new List<CmlAtom>();
                for (int i = 1; i < movingAtoms.Count(); i++)
                {
                    newMovingAtoms.Add(movingAtoms.ElementAt(i));
                }
                ComputeAndAdd2DCoordinates(newFixedAtoms, newMovingAtoms);
            }
            else if (fixedAtoms.Count() == 1)
            {
                Angle theta = new Angle(2*Math.PI/((double) movingAtoms.Count() + 1), Angle.Unit.Radians);
                Point2 fPoint = new Point2(fixedAtoms.ElementAt(0).Point2);
                Transform2 t = Transform2.CreateRotationMatrixAboutPoint(theta, this.Point2);
                for (int i = 0; i < movingAtoms.Count(); i++)
                {
                    fPoint = t.Transform(fPoint);
                    movingAtoms.ElementAt(i).Point2 = new Point2(fPoint);
                }
            }
            else if (fixedAtoms.Count() == 2)
            {
                int start = 0;
                Angle theta = GetAngle(fixedAtoms.ElementAt(start), this, fixedAtoms.ElementAt(1 - start),
                                       0.0000001);
                if (theta.LessThan(new Angle(0.0, Angle.Unit.Radians)))
                {
                    start = 1;
                    theta = theta.MultiplyBy(-1.0);
                }
                Angle theta1 =
                    new Angle(2*Math.PI - theta.Radians, Angle.Unit.Radians).MultiplyBy(1.0/
                                                                                         ((double) movingAtoms.Count() +
                                                                                          1));
                Transform2 t = Transform2.CreateRotationMatrixAboutPoint(theta1, this.Point2);
                Point2 fPoint = new Point2(fixedAtoms.ElementAt(1 - start).Point2);
                for (int i = 0; i < movingAtoms.Count(); i++)
                {
                    fPoint = t.Transform(fPoint);
                    movingAtoms.ElementAt(i).Point2 = fPoint;
                }
            }
            else if (fixedAtoms.Count() == 3 && movingAtoms.Count() == 1)
            {
                // arbitrarily choose PI from first atom
                Vector2 v2 = fixedAtoms.ElementAt(0).GetVectorTo(this);
                movingAtoms.ElementAt(0).Point2 = this.Point2.Add(v2);
            }

            else if (movingAtoms.Count() == 1)
            {
                Vector2 v = fixedAtoms.ElementAt(0).Point2.Subtract(this.Point2);
                for (int i = 1; i < fixedAtoms.Count(); i++)
                {
                    v.Add(fixedAtoms.ElementAt(i).Point2.Subtract(this.Point2));
                }
                if (v.GetLength() < 0.001)
                {
                    v = new Vector2(this.Point2.Subtract(fixedAtoms.ElementAt(0).Point2));
                }
                movingAtoms.ElementAt(0).Point2 = this.CreatePointAtBondedDistance(v,
                                                                                   this.GetDistance(
                                                                                       fixedAtoms.ElementAt(0)));
            }
            else
            {
                throw new NumboException("Cannot add new atom positions; fixed: " + fixedAtoms.Count() + " moving: " +
                                         movingAtoms.Count());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        /// could be private
        internal Point2 CreatePointAtBondedDistance(Vector2 vector, double? distance)
        {
            if (vector == null)
            {
                throw new ArgumentNullException("vector");
            }
            if (distance == null)
            {
                throw new ArgumentNullException("distance", "No bonding distance set");
            }
            vector = new Vector2(vector);
            vector.Normalize();
            this.Point2.Add(vector);
            return Point2;
        }
       
        /// <summary>
        /// force delete hydrogens and their bonds
        /// H atoms are selected in unpredictable order
        /// </summary>
        /// <param name="count"></param>
        internal void DeleteHydrogen(int count)
        {
            ICollection<CmlAtom> hLigands = this.GetHydrogenLigands();
            if (hLigands.Count < count)
            {
                throw new NumboException("trying to remove too many H atoms");
            }
            IEnumerable<CmlBond> hLigandBonds = this.GetHydrogenLigandBonds();
            for (int i = 0; i < count; i++)
            {
                hLigands.ElementAt(i).DeleteSimple(true);
                hLigandBonds.ElementAt(i).DeleteSimple(true);
            }
        }

        /// <summary>
        /// creates hydrogens on this-atom
        /// adds ids and bonds but not coordinates
        /// </summary>
        /// <param name="count"></param>
        private void CreateAndAddHydrogen(int count)
        {
            for (int i = 0; i < count; i++)
            {
                CreateAndAddHydrogen();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateAndAddHydrogen()
        {
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
            CmlAtom atom = molecule.CreateAndAddAtomWithId(PeriodicTable.Element.H);
            molecule.CreateAndAddBondWithId(this, atom, CmlBond.Single);
        }

        /// <summary>
        /// Determines whether the argument is a CMLLite atom
        /// 
        /// at the moment this only test the name and namespace - in future
        /// this will check more
        /// </summary>
        /// <param name="element">the XElement to test</param>
        /// <returns>true if element is a CMLAtom otherwise false</returns>
        public static bool IsAtom(XElement element)
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
        /// can the atom be substituted by a group
        /// requires a single non-hydorgen ligand with acyclic bond
        /// </summary>
        /// <returns></returns>
        internal GroupLookup GetSubstitutionDictionary()
        {
            CmlMolecule.GroupType groupType = CmlMolecule.GroupType.Unknown;
            GroupLookup groupDictionary = null;
            IEnumerable<CmlAtom> nonHydrogenLigandList = this.GetNonHydrogenLigands();
            IEnumerable<CmlAtom> hydrogenLigandList = this.GetHydrogenLigands();
            GroupLookup elementDictionary = GroupLookupManager.Builtin.GetGroupDictionary(this.ElementType);

            CmlLabel label = GetFirstLabel(CmlxGroupPointer);
            if (label != null)
            {
                string dictionaryType = label.GetDictionaryType();
                groupDictionary = GroupLookupManager.Builtin.GetGroupDictionary(dictionaryType);
            }
                // does atom have bespoke dictionary?
            else if (elementDictionary != null)
            {
                groupDictionary = elementDictionary;
            }
                // R-X bond
            else if (nonHydrogenLigandList.Count() == 1)
            {
                groupType = CmlMolecule.GroupType.MonovalentSingle;
            }
                // C-H bond
            else if (hydrogenLigandList.Count() == 1 &&
                     PeriodicTable.Element.C.Equals(PeriodicTable.GetElement(this.ElementType)))
            {
                groupType = CmlMolecule.GroupType.MonovalentSingle;
            }
                // R-X-R' 
            else if (nonHydrogenLigandList.Count() == 2)
            {
                CmlBond bond0 = this.GetNonHydrogenLigandBonds().ElementAt(0);
                CmlBond bond1 = this.GetNonHydrogenLigandBonds().ElementAt(1);
                // requires 2 single bonds (could be cyclic)
                if (CmlBond.Single.Equals(bond0.Order) && CmlBond.Single.Equals(bond1.Order))
                {
                    groupType = CmlMolecule.GroupType.DivalentSingle;
                }
            }
            if (groupDictionary == null && !CmlMolecule.GroupType.Unknown.Equals(groupType))
            {
                groupDictionary = GroupLookupManager.Builtin.GetGroupDictionary(groupType.ToString());
            }
            return groupDictionary;
        }

        /// <summary>
        /// can only work if single non-H ligand
        /// this is required so we can work out which is the bond
        /// this.atom is destroyed
        /// </summary>
        /// <param name="groupAbbreviation"></param>
        internal void SubstituteAtomAndHydrogensWithGroup(string groupAbbreviation)
        {
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
            bool change = AddRemoveProtonElectronHDot(groupAbbreviation);
            if (!change)
            {
                GroupLookup groupDictionary = GetSubstitutionDictionary();
                if (groupDictionary != null)
                {
                    CmlMolecule groupMolecule = groupDictionary.GetGroup(groupAbbreviation);
                    if (groupMolecule == null)
                    {
                        throw new NumboException("Cannot find group: " + groupAbbreviation);
                    }
                    // if zerovalent replace with group
                    if (this.GetLigands().Count() == 0)
                    {
                        this.ReplaceIsolatedAtomWithGroup(groupMolecule);
                        change = true;
                    }
                        // if monovalent call replace unique bond
                    else if (this.GetNonHydrogenLigands().Count() == 1)
                    {
                        CmlBond bond = this.GetNonHydrogenLigandBonds().ElementAt(0);
                        bond.ReplaceGroupWithGroup(this, groupMolecule);
                        change = true;
                    }
                        // if a single C-H, substitute the H
                    else if (this.GetHydrogenLigands().Count() == 1 &&
                             PeriodicTable.Element.C.Equals(PeriodicTable.GetElement(this.ElementType)))
                    {
                        CmlAtom hAtom = this.GetHydrogenLigands().ElementAt(0);
                        CmlBond hBond = this.GetHydrogenLigandBonds().ElementAt(0);
                        hBond.ReplaceGroupWithGroup(hAtom, groupMolecule);
                        change = true;
                    }
                    else if (this.GetNonHydrogenLigands().Count() == 2)
                    {
                        this.ReplaceDivalentAtomWithGroup(groupMolecule);
                        change = true;
                    }
                }
                else if (groupAbbreviation == null)
                {
                    throw new ArgumentNullException("groupAbbreviation", "BUG: null abbreviation");
                }
                if (change)
                {
                    if (molecule == null)
                    {
                        throw new NumboException("null molecule");
                    }
                    molecule.Normalize();
                }
            }
        }

        private bool AddRemoveProtonElectronHDot(string groupAbbreviation)
        {
            bool ok = false;
            if (CmlConstants.AddElectron.Equals(groupAbbreviation))
            {
                this.AddElectron();
                ok = true;
            }
            else if (CmlConstants.RemoveElectron.Equals(groupAbbreviation))
            {
                this.RemoveElectron();
                ok = true;
            }
            else if (CmlConstants.AddProton.Equals(groupAbbreviation))
            {
                this.AddProtonAndAdjustCoordinates();
                ok = true;
            }
            else if (CmlConstants.RemoveProton.Equals(groupAbbreviation))
            {
                this.RemoveProtonAndAdjustCoordinates();
                ok = true;
            }
            else if (CmlConstants.AddHdot.Equals(groupAbbreviation))
            {
                this.AddHydrogenDotAndAdjustCoordinates();
                ok = true;
            }
            else if (CmlConstants.RemoveHdot.Equals(groupAbbreviation))
            {
                this.RemoveHydrogenDotAndAdjustCoordinates();
                ok = true;
            }
            return ok;
        }

        private void ReplaceIsolatedAtomWithGroup(CmlMolecule group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }

            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
            // clone group and renumber
            CmlMolecule groupClone = new CmlMolecule(group);
            groupClone.RenumberAtomsBonds(molecule);
            // has group got coordinates?
            Point2 groupCentroid = groupClone.GetCentroid();
            if (groupCentroid == null)
            {
                throw new NumboException("No coordinates in group");
            }

            Vector2 translationVector = this.Point2.Subtract(groupCentroid);
            // and transform to overlap
            Transform2 translationTransform = Transform2.CreateTranslation(translationVector);
            groupClone.Transform2D(translationTransform);
            // delete and clean
            this.DeleteAndRemoveHangingBonds();
            this.DeleteSimple(true);
            // copy groupClone
            molecule.TransferBondsAndAtomsToMolecule(groupClone);
        }

        /// <summary>
        /// replaces divalent group
        /// </summary>
        /// <param name="group"></param>
        internal void ReplaceDivalentAtomWithGroup(CmlMolecule group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
            // clone group and renumber
            CmlMolecule groupClone = new CmlMolecule(group);
            groupClone.RenumberAtomsBonds(molecule);
            // has group got coordinates?
            if (groupClone.GetCentroid() == null)
            {
                throw new ArgumentException("No coordinates in group");
            }

            // ================ this ===============
            IEnumerable<CmlAtom> nonHydrogenLigands = this.GetNonHydrogenLigands();
            IEnumerable<CmlBond> nonHydrogenLigandBonds = this.GetNonHydrogenLigandBonds();
            if (nonHydrogenLigandBonds.Count() != 2)
            {
                throw new NumboException("must have 2 non-H ligands");
            }

            this.RemoveAtomParityOnLigands();
            // remove all hydrogens
            IEnumerable<CmlAtom> hydrogenLigands = this.GetHydrogenLigands();
            foreach (CmlAtom hydrogenLigand in hydrogenLigands)
            {
                hydrogenLigand.DeleteAndRemoveHangingBonds();
            }
            CmlAtom nonHAtom0 = nonHydrogenLigands.ElementAt(0);
            CmlAtom nonHAtom1 = nonHydrogenLigands.ElementAt(1);
            Vector2 thisVector0 = this.GetVectorTo(nonHAtom0);
            Vector2 thisVector1 = this.GetVectorTo(nonHAtom1);
            Vector2 thisVector01 = new Vector2(thisVector0);
            thisVector01.Add(thisVector1);
            // if vectors are antiparallel make a perpendicular one
            if (thisVector01.GetLength() < 0.001)
            {
                thisVector01 = new Vector2(thisVector0.Y, -thisVector0.X);
            }

            // ================ group ================
            // check has 2 R groups 
            IEnumerable<CmlAtom> rGroups = groupClone.GetAtomsByElementType(PeriodicTable.Element.R);
            if (rGroups.Count() != 2)
            {
                throw new NumboException("group must have two R elements");
            }
            CmlAtom rAtom0 = rGroups.ElementAt(0);
            IEnumerable<CmlAtom> ligands0 = rAtom0.GetLigands();
            CmlAtom rAtom1 = rGroups.ElementAt(1);
            IEnumerable<CmlAtom> ligands1 = rAtom1.GetLigands();
            if (ligands0.Count() != 1 || ligands1.Count() != 1)
            {
                throw new NumboException("Can only join if both R groups have ONE ligand");
            }

            // get bonds and atoms in group
            CmlBond rBond0 = rAtom0.GetLigandBonds().ElementAt(0);
            //CmlBond rBond1 = rAtom1.GetLigandBonds().ElementAt(0);
            CmlAtom centralAtom = rBond0.GetAtomAtOtherEnd(rAtom0);

            // calculate geometry and transformations
            Vector2 rVector0 = centralAtom.GetVectorTo(rAtom0);
            Vector2 rVector1 = centralAtom.GetVectorTo(rAtom1);
            // bisector
            Vector2 rVector01 = new Vector2(rVector0);
            rVector01.Add(rVector1);

            // ======================= overlap ================
            Angle theta = rVector01.AngleTo(thisVector01);

            Transform2 rotationTransform = Transform2.CreateRotationMatrixAboutPoint(theta, centralAtom.Point2);
            groupClone.Transform2D(rotationTransform);

            Vector2 translationVector = centralAtom.GetVectorTo(this);
            // and transform to overlap
            Transform2 translationTransform = Transform2.CreateTranslation(translationVector);
            groupClone.Transform2D(translationTransform);
            // delete and clean
            this.DeleteAndRemoveHangingBonds();
            this.DeleteSimple(true);

            molecule.CreateAndAddBondWithId(nonHAtom0, centralAtom, CmlBond.Single);
            molecule.CreateAndAddBondWithId(nonHAtom1, centralAtom, CmlBond.Single);
            rAtom0.DeleteAndRemoveHangingBonds();
            rAtom1.DeleteAndRemoveHangingBonds();

            // copy groupClone
            molecule.TransferBondsAndAtomsToMolecule(groupClone);
        }

        public IEnumerable<CmlLabel> GetLabels()
        {
            IEnumerable<XElement> elements =
                DelegateElement.XPathSelectElements(".//*[local-name()='" + CmlLabel.Tag +
                                                            "' and namespace-uri()='" +
                                                            CmlConstants.Cmlns + "']");
            List<CmlLabel> labels = new List<CmlLabel>(elements.Count());
            foreach (XElement element in elements)
            {
                if (CmlLabel.IsLabel(element))
                {
                    labels.Add(new CmlLabel(element));
                }
            }
            return labels;
        }

        /// <summary>
        /// get first label with given dictRef
        /// </summary>
        /// <param name="dictRef"></param>
        /// <returns></returns>
        public CmlLabel GetFirstLabel(string dictRef)
        {
            CmlLabel label = null;
            IEnumerable<CmlLabel> labels = GetLabels();
            foreach (CmlLabel labelx in labels)
            {
                string dRef = labelx.DictRef;
                if (dictRef.Equals(dRef))
                {
                    label = labelx;
                    break;
                }
            }
            return label;
        }

        /// <summary>
        /// utility to translate a list of atoms into XElements
        /// </summary>
        /// <param name="atoms">List of atoms</param>
        /// <returns>list of their DelegateElements; null id atoms is null</returns>
        public static IEnumerable<XElement> GetXElements(IEnumerable<CmlAtom> atoms)
        {
            List<XElement> elements = null;
            if (atoms != null)
            {
                elements = atoms.Select(atom => atom.DelegateElement).ToList();
            }
            return elements;
        }

        public static bool IsValidIsotopeNumber(int isotopeNumber)
        {
            return (isotopeNumber >= 1);
        }

        /// <summary>
        ///  a small number of elements can accept protons (H+)
        ///  they must have an existing lone pair
        ///  C,N,O,S,F,Cl,Br,I
        /// </summary>
        /// <returns></returns>
        public bool CanAddProton()
        {
            bool ok = false;
            PeriodicTable.Element element = PeriodicTable.GetElement(this.ElementType);
            int formalCharge = this.FormalCharge ?? 0;
            int bondOrderSum = this.GetBondOrderSum() ?? 0;
            // C-
            if (PeriodicTable.Element.C.Equals(element))
            {
                if (bondOrderSum == 3 && formalCharge == -1)
                {
                    ok = true;
                }
            }
                // R-NR(-) or R-N-(R)R
            else if (PeriodicTable.Element.N.Equals(element))
            {
                if (formalCharge == -1)
                {
                    ok = true;
                }
                else if (bondOrderSum <= 3 && formalCharge <= 0)
                {
                    ok = true;
                }
            }
                // R-O- or R-O-R
            else if (PeriodicTable.Element.O.Equals(element))
            {
                if (formalCharge == -1)
                {
                    ok = true;
                }
                else if (bondOrderSum <= 2 && formalCharge <= 0)
                {
                    ok = true;
                }
            }
                // R-S (-)
            else if (PeriodicTable.Element.S.Equals(element))
            {
                if (formalCharge == -1)
                {
                    ok = true;
                }
            }
                // X- halide ions
            else if (PeriodicTable.Element.F.Equals(element) ||
                     PeriodicTable.Element.Cl.Equals(element) ||
                     PeriodicTable.Element.Br.Equals(element) ||
                     PeriodicTable.Element.I.Equals(element))
            {
                if (formalCharge == -1)
                {
                    ok = true;
                }
            }

            return ok;
        }

        /// <summary>
        ///  a small number of elements can lose protons (H+)
        ///  C,N,O,S
        ///  there must not be an existing negative charge
        /// </summary>
        /// <returns></returns>
        internal bool CanRemoveProton()
        {
            bool ok = false;
            PeriodicTable.Element element = PeriodicTable.GetElement(this.ElementType);
            int formalCharge = this.FormalCharge ?? 0;
            int nHyd = this.GetHydrogenLigands().Count();
            if (nHyd > 0 && formalCharge >= 0)
            {
                if (PeriodicTable.Element.C.Equals(element) ||
                    PeriodicTable.Element.N.Equals(element) ||
                    PeriodicTable.Element.O.Equals(element) ||
                    PeriodicTable.Element.S.Equals(element))
                {
                    ok = true;
                }
            }

            return ok;
        }

        /// <summary>
        ///  a small number of elements can accept hydrogens (H.)
        ///  they must be existing radicals
        ///  C,N,O,S,F,Cl,Br,I
        /// </summary>
        /// <returns></returns>
        internal bool CanAddHydrogenDot()
        {
            bool ok = false;
            PeriodicTable.Element element = PeriodicTable.GetElement(this.ElementType);
            int spin = this.SpinMultiplicity ?? 1;
            // C.
            if (spin == 2)
            {
                if (PeriodicTable.Element.C.Equals(element) ||
                    PeriodicTable.Element.N.Equals(element) ||
                    PeriodicTable.Element.O.Equals(element) ||
                    PeriodicTable.Element.S.Equals(element) ||
                    PeriodicTable.Element.F.Equals(element) ||
                    PeriodicTable.Element.Cl.Equals(element) ||
                    PeriodicTable.Element.Br.Equals(element) ||
                    PeriodicTable.Element.I.Equals(element))
                {
                    ok = true;
                }
            }
            return ok;
        }

        /// <summary>
        ///  a small number of element can lose hydrogen
        ///  C,N,O,S
        /// </summary>
        /// <returns></returns>
        internal bool CanRemoveHydrogenDot()
        {
            bool ok = false;
            PeriodicTable.Element element = PeriodicTable.GetElement(this.ElementType);
            int nHyd = this.GetHydrogenLigands().Count();
            if (nHyd > 0)
            {
                if (PeriodicTable.Element.C.Equals(element) ||
                    PeriodicTable.Element.N.Equals(element) ||
                    PeriodicTable.Element.O.Equals(element) ||
                    PeriodicTable.Element.S.Equals(element))
                {
                    ok = true;
                }
            }

            return ok;
        }

        /// <summary>
        /// at present simply checks for carbon and 4 ligands
        /// </summary>
        /// <returns>this == C && nLig == 4</returns>
        internal bool CanBeChiral()
        {
            return this.GetLigands().Count() == 4 && PeriodicTable.Element.C.Equals(
                                                         PeriodicTable.GetElement(this.ElementType));
        }

        /// <summary>
        /// removes a proton and decreases the charge on the atom
        /// adjusts coordinates of remaining hydrogens
        /// </summary>
        internal void RemoveProtonAndAdjustCoordinates()
        {
            int charge = this.FormalCharge ?? 0;
            if (this.GetHydrogenLigands().Count() > 0)
            {
                this.DeleteHydrogen(1);
                this.CreateOrAdjustHydrogenCoordinates();
                this.FormalCharge = charge - 1;
            }
        }

        /// <summary>
        /// adds an electron
        /// TODO needs chemical work
        /// always pairs electrons by default
        /// </summary>
        internal void AddElectron()
        {
            int? fc = this.FormalCharge;
            if (fc != null)
            {
                this.FormalCharge = fc - 1;
            }
            else
            {
                this.FormalCharge = -1;
            }
            int? sm = this.SpinMultiplicity;
            if (sm == null)
            {
                this.SpinMultiplicity = 2;
            }
            else if ((int) sm == 2)
            {
                this.SpinMultiplicity = 1;
            }
            else
            {
                this.SpinMultiplicity = (int) sm - 1;
            }
        }

        /// <summary>
        /// removes an electron
        /// TODO needs chemical work
        /// always pairs electrons by default
        /// </summary>
        internal void RemoveElectron()
        {
            int? fc = this.FormalCharge;
            if (fc != null)
            {
                this.FormalCharge = fc + 1;
            }
            else
            {
                this.FormalCharge = 1;
            }
            int? sm = this.SpinMultiplicity;
            if (sm == null)
            {
                this.SpinMultiplicity = 2;
            }
            else if ((int) sm == 2)
            {
                this.SpinMultiplicity = 1;
            }
            else
            {
                this.SpinMultiplicity = (int) sm - 1;
            }
        }

        /// <summary>
        /// adds a proton and increases the charge on the atom
        /// adjusts coordinates of remaining hydrogens
        /// </summary>
        internal void AddProtonAndAdjustCoordinates()
        {
            int charge = this.FormalCharge ?? 0;
            this.CreateAndAddHydrogen(1);
            this.CreateOrAdjustHydrogenCoordinates();
            this.FormalCharge = charge + 1;
        }

        /// <summary>
        /// adds a hydrogen atom 
        /// adjusts coordinates of remaining hydrogens
        /// </summary>
        internal void AddHydrogenDotAndAdjustCoordinates()
        {
            int spin = this.SpinMultiplicity ?? 1;
            if (spin >= 2)
            {
                this.SpinMultiplicity = spin - 1;
                this.CreateAndAddHydrogen(1);
                this.CreateOrAdjustHydrogenCoordinates();
            }
        }

        /// <summary>
        /// adds hydrogens
        /// </summary>
        /// <param name="nHyd">number of H to add (0 is no-op)</param>
        internal void AddNakedHydrogensAndAdjustCoordinates(int nHyd)
        {
            if (nHyd > 0)
            {
                this.CreateAndAddHydrogen(nHyd);
                this.CreateOrAdjustHydrogenCoordinates();
            }
        }

        /// <summary>
        /// removes a hydrogen atom 
        /// adjusts coordinates of remaining hydrogens
        /// </summary>
        internal void RemoveHydrogenDotAndAdjustCoordinates()
        {
            int spin = this.SpinMultiplicity ?? 1;
            if (spin == 1 && this.GetHydrogenLigands().Count() > 0)
            {
                this.SpinMultiplicity = 2;
                this.DeleteHydrogen(1);
                this.CreateOrAdjustHydrogenCoordinates();
            }
        }

        /// <summary>
        /// allows change if number of H to be deleted exist
        /// </summary>
        /// <param name="commonElement"></param>
        /// <returns></returns>
        internal bool HashEnoughHydrogensToDeleteOnGroupChange(PeriodicTable.Element commonElement)
        {
            bool ok = false;
            PeriodicTable.Element thisElement = PeriodicTable.GetElement(this.ElementType);
            int groupDiff = PeriodicTable.CalculateCommonGroupDifference(commonElement, thisElement);
            if (groupDiff >= 0 || -groupDiff <= this.GetHydrogenLigands().Count())
            {
                ok = true;
            }
            return ok;
        }

        /// <summary>
        /// current returns true as there are no rules forbidding it.
        /// </summary>
        /// <returns></returns>
        internal bool CanAddElectron()
        {
            return true;
        }

        internal bool CanRemoveElectron()
        {
            bool ok = false;
            PeriodicTable.Element el = PeriodicTable.GetElement(this.ElementType);
            if (PeriodicTable.Element.H.Equals(el))
            {
                ok = this.FormalCharge <= 0;
            }
            else
            {
                // TODO spend time counting electrons some wet day
                ok = true;
            }
            return ok;
        }

        internal List<int> SuggestPossibleIsotopeMasses()
        {
            List<int> numbers = new List<int>();
            PeriodicTable.Element el = PeriodicTable.GetElement(this.ElementType);
            ChemicalElement chemicalElement = null;
            PeriodicTable.GetPeriodicTableIndexedByElement().TryGetValue(el, out chemicalElement);
            int[] masses = chemicalElement.IsotopeMasses;
            // cannot find isotopes, make +- 5 round atomic Mass - this will be phased out!
            if (masses == null)
            {
                int atomicMass = (int) chemicalElement.AtomicWeight;
                for (int i = -5; i <= 5; i++)
                {
                    int mass = atomicMass + i;
                    if (i > 0)
                    {
                        numbers.Add(mass);
                    }
                }
            }
            else
            {
                foreach (int mass in masses)
                {
                    numbers.Add(mass);
                }
            }
            return numbers;
        }

        /// <summary>
        /// checks if atom has exactly 2 ligands and is within 
        /// epsilon of linearity
        /// </summary>
        /// <returns></returns>
        internal bool Has2LinearLigands(Angle epsilon)
        {
            epsilon.AngleRange = Angle.Range.Signed;
            epsilon = epsilon.GetAbsoluteValue();
            bool linear = false;
            IEnumerable<CmlAtom> nonHLigands = this.GetNonHydrogenLigands();
            if (nonHLigands.Count() == 2)
            {
                Angle angle = GetAngle(nonHLigands.ElementAt(0), this, nonHLigands.ElementAt(1), 0.0001);
                if (angle != null)
                {
                    Angle delta = angle.GetDeviationFromLinearity();
                    delta = delta.GetAbsoluteValue();
                    delta.AngleRange = Angle.Range.Signed;
                    delta.Normalize();
                    if (delta.LessThan(epsilon))
                    {
                        linear = true;
                    }
                }
            }
            return linear;
        }

        internal static string[] GetAtomIds(IEnumerable<CmlAtom> atoms)
        {
            string[] ids = new string[atoms.Count()];
            for (int i = 0; i < ids.Count(); i++)
            {
                ids[i] = atoms.ElementAt(i).Id;
            }
            return ids;
        }

        internal void RemoveAtomParityOnLigands()
        {
            IEnumerable<CmlAtom> ligands = this.GetLigands();
            foreach (CmlAtom ligand in ligands)
            {
                ligand.RemoveAtomParityChildren();
            }
        }

        internal void RemoveAtomParityChildren()
        {
            // should only be one at most, but just to be sure
            IEnumerable<XElement> atomParityChildren =
                this.DelegateElement.XPathSelectElements("./*[local-name()='" + CmlAtomParity.Tag +
                                                         "' and namespace-uri()='" + CmlConstants.Cmlns + "']");
            foreach (XElement atomParityChild in atomParityChildren)
            {
                atomParityChild.Remove();
            }
        }

        internal void RemoveBondStereoFromSharpLigandBonds()
        {
            IEnumerable<CmlBond> ligandBonds = this.GetLigandBonds();
            foreach (CmlBond ligandBond in ligandBonds)
            {
                ligandBond.DeleteAllWedgeHatchBondStereoChildrenWithSharpAtom(this);
            }
        }
    }
}