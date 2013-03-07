// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml.XPath;
using Euclid;
using log4net;
using Numbo.Cml.Helpers;

namespace Numbo.Cml
{
    /// <summary>
    /// supports CML bond element
    /// </summary>
    public class CmlBond : CmlElement, IHasCentroid2
    {
        #region Cyclicity enum

        ///<summary>
        ///</summary>
        public enum Cyclicity
        {
            ///<summary>
            ///</summary>
            Cyclic,
            ///<summary>
            ///</summary>
            Acyclic,
            ///<summary>
            ///</summary>
            Unknown
        }

        #endregion

        #region Type enum

        public enum Type
        {
            Other,
            Unknown,
            Single,
            Double,
            Triple,
            Hatch,
            Wedge,
        }

        #endregion

        public const string Aromatic = "A";
        public const string CmlxCyclic = "cyclic";

        /* from the schema
        <xsd:restriction base="xsd:string">
          <xsd:enumeration value="S">
            <xsd:annotation>
              <xsd:documentation>
                <h:div class="summary">Single bond.</h:div>
                <h:div class="description">synonymous with "1.</h:div>
              </xsd:documentation>
            </xsd:annotation>
          </xsd:enumeration>
          <xsd:enumeration value="1">
            <xsd:annotation>
              <xsd:documentation>
                <h:div class="summary">Single bond.</h:div>
              </xsd:documentation>
            </xsd:annotation>
          </xsd:enumeration>
          <xsd:enumeration value="D">
            <xsd:annotation>
              <xsd:documentation>
                <h:div class="summary">Double bond.</h:div>
              </xsd:documentation>
            </xsd:annotation>
          </xsd:enumeration>
          <xsd:enumeration value="2">
            <xsd:annotation>
              <xsd:documentation>
                <h:div class="summary">Double bond.</h:div>
              </xsd:documentation>
            </xsd:annotation>
          </xsd:enumeration>
          <xsd:enumeration value="T">
            <xsd:annotation>
              <xsd:documentation>
                <h:div class="summary">Triple bond.</h:div>
              </xsd:documentation>
            </xsd:annotation>
          </xsd:enumeration>
          <xsd:enumeration value="3">
            <xsd:annotation>
              <xsd:documentation>
                <h:div class="summary">Triple bond.</h:div>
              </xsd:documentation>
            </xsd:annotation>
          </xsd:enumeration>
          <xsd:enumeration value="A">
            <xsd:annotation>
              <xsd:documentation>
                <h:div class="summary">Aromatic bond.</h:div>
              </xsd:documentation>
            </xsd:annotation>
          </xsd:enumeration>
          <xsd:enumeration value="unknown">
            <xsd:annotation>
              <xsd:documentation>
                <h:div class="summary">Unknown bond order.</h:div>
              </xsd:documentation>
            </xsd:annotation>
          </xsd:enumeration>
          <xsd:enumeration value="other">
            <xsd:annotation>
              <xsd:documentation>
                <h:div class="summary">Other bond order.</h:div>
                <h:div class="description">Further information can be given using dictRef</h:div>
              </xsd:documentation>
            </xsd:annotation>
          </xsd:enumeration>
        </xsd:restriction>
        */

        public const string Double = "D";
        public const string Hatch = "H";
        public const string HatchReverse = Hatch + Reverse;
        public const string Other = "other";
        //public const string HBond = "hbond";
        //public const string PARTIAL01 = "partial01";
        //public const string PARTIAL12 = "partial12";
        //public const string PARTIAL23 = "partial23";
        public const string Reverse = "-REV";
        public const string Single = "S";

        public const double StubScalefactor = 1.0/3.0;

        public const string Tag = "bond";
        public const string Triple = "T";
        public const string Unknown = "unknown";

        public const string Wedge = "W";
        public const string WedgeReverse = Wedge + Reverse;
        private static readonly ILog Log = LogManager.GetLogger(typeof (CmlBond));
        public static readonly double DefaultLength = 1.54;

        /// <summary>
        /// creates unattached bond
        /// use molecule.CreateAndAddBondWithId() where possible
        /// </summary>
        internal CmlBond()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlBond(XElement element)
            : base(element)
        {}

        /// <summary>
        ///  copy constructor
        /// </summary>
        public CmlBond(CmlBond bond)
            : base(bond.DelegateElement)
        {}

        public string Order
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.Order) != null)
                           ? DelegateElement.Attribute(CmlAttribute.Order).Value
                           : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Order, value); }
        }

        public BondOrder BondOrder
        {
            get { return BondOrder.GetBondOrder(this); }
        }

        /// <summary>
        /// get centroid of bond as mean of two atom positions
        /// </summary>
        /// <returns>null if no atoms or atoms do not both have coordinates</returns>
        public Point2 GetCentroid()
        {
            return CmlUtils.GetCentroid(GetSubCentroids());
        }

        /// <summary>
        /// returns centroids of atoms
        /// </summary>
        /// <returns>null if no atoms or atoms do not have centroids</returns>
        public ICollection<IHasCentroid2> GetSubCentroids()
        {
            List<IHasCentroid2> centroidList = null;
            ICollection<CmlAtom> atoms = GetAtoms();
            if (atoms != null && atoms.Count == 2)
            {
                centroidList = new List<IHasCentroid2>();
                foreach (CmlAtom atom in atoms)
                {
                    if (atom != null)
                    {
                        Point2 point = atom.GetCentroid();
                        if (point == null)
                        {
                            centroidList = null;
                            break;
                        }
                        centroidList.Add(point);
                    }
                }
            }
            return centroidList;
        }

        /// <summary>
        /// set atomRefs2 not checking uniqueness
        /// </summary>
        /// <param name="atomRefs2"></param>
        public void SetAtomRefs2(string[] atomRefs2)
        {
            DelegateElement.SetAttributeValue(CmlAttribute.AtomRefs2, CmlUtils.Concatenate(atomRefs2));
        }

        /// <summary>
        /// get atomRefs2 not checking uniqueness
        /// </summary>
        public string[] GetAtomRefs2()
        {
            XAttribute att = DelegateElement.Attribute(CmlAttribute.AtomRefs2);
            return (att == null) ? null : att.Value.Split();
        }

        /// <summary>
        /// adds attribute cmlx:cyclicity with controlled values
        /// </summary>
        /// <param name="cyclicity"></param>
        public void SetCyclicIndicator(Cyclicity cyclicity)
        {
            XAttribute cmlxCyclic = DelegateElement.Attribute(CmlConstants.CmlxXNamespace + CmlxCyclic);
            if (cmlxCyclic != null)
            {
                cmlxCyclic.Remove();
            }
            AddCmlxAttribute(CmlxCyclic, cyclicity.ToString());
        }

        /// <summary>
        /// if bond has cyclic indicator returns it, else forces computation for whole molecule
        /// </summary>
        /// <returns></returns>
        public Cyclicity ForceGetCyclicIndicator()
        {
            var cyclicAttribute = DelegateElement.Attribute(CmlConstants.CmlxXNamespace + CmlxCyclic);
            if (cyclicAttribute == null)
            {
                var molecule = GetFirstAncestorMolecule();
                molecule.CalculateAndSetCyclicIndicators();
                cyclicAttribute = DelegateElement.Attribute(CmlConstants.CmlxXNamespace + CmlxCyclic);
            }
            var cyclicity = Cyclicity.Unknown; 
            if (cyclicAttribute != null) {
                cyclicity = (Cyclicity) Enum.Parse(typeof (Cyclicity), cyclicAttribute.Value, false);
            }
            return cyclicity;
        }

        /// <summary>
        /// get atoms in atomRefs2 order
        /// </summary>
        /// <returns>null if no atomRefs2 or bad atomRefs2 or atoms not found</returns>
        public ICollection<CmlAtom> GetAtoms()
        {
            List<CmlAtom> atoms = null;
            string[] atomIds = GetAtomRefs2();
            if (atomIds != null && atomIds.Count() == 2)
            {
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
                if (molecule == null)
                {
                    throw new NumboException(string.Format(CultureInfo.InvariantCulture, "Null molecule: {0}", DelegateElement));
                }
                else
                {
                    atoms = new List<CmlAtom>();
                    foreach (string atomId in atomIds)
                    {
                        CmlAtom atom = molecule.GetAtomById(atomId);
                        if (atom == null)
                        {
                            atoms = null;
                            break;
                        }
                        atoms.Add(atom);
                    }
                }
            }
            return atoms;
        }

        /// <summary>
        /// Gets the (first) CMLBondStereo child of this element - or null if none is defined
        /// </summary>
        /// <returns>the first CMLBondStereo child of this bond or null</returns>
        public CmlBondStereo GetBondStereo()
        {
            CmlBondStereo bondStereo = null;
            XElement xElement =
                DelegateElement.XPathSelectElement(".//*[local-name()='" + CmlBondStereo.Tag +
                                                   "' and namespace-uri()='" + CmlConstants.Cmlns + "'][1]");
            if (xElement != null)
            {
                bondStereo = new CmlBondStereo(xElement);
            }
            return bondStereo;
        }

        /// <summary>
        /// get atomId at other end of a bond
        /// </summary>
        /// <param name="atomId">known atomId</param>
        /// <returns>null if atomId == null or not in bond</returns>
        public string GetAtomIdAtOtherEnd(string atomId)
        {
            string otherAtomId = null;
            if (atomId != null)
            {
                string[] atomIds = GetAtomRefs2();
                if (atomIds != null)
                {
                    if (string.Compare(atomIds[0], atomId, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        otherAtomId = atomIds[1];
                    }
                    else if (string.Compare(atomIds[1], atomId, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        otherAtomId = atomIds[0];
                    }
                }
            }
            return otherAtomId;
        }

        /// <summary>
        /// gets pointed end of wedgeHatch
        /// </summary>
        public CmlAtom GetAtomAtSharpEnd()
        {
            CmlAtom atom = null;
            CmlBondStereo bondStereo = GetBondStereo();
            if (bondStereo != null)
            {
                if (bondStereo.IsWedgeHatch())
                {
                    atom = GetAtoms().ElementAt(0);
                }
            }
            return atom;
        }

        /// <summary>
        /// get atom at other end of a bond
        /// </summary>
        /// <param name="atom">known atom</param>
        /// <returns>null if atom == null or not in bond</returns>
        public CmlAtom GetAtomAtOtherEnd(CmlAtom atom)
        {
            CmlAtom otherAtom = null;
            if (atom != null)
            {
                string otherId = GetAtomIdAtOtherEnd(atom.Id);
                if (otherId != null)
                {
                    CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
                    if (molecule != null)
                    {
                        otherAtom = molecule.GetAtomById(otherId);
                    }
                }
            }
            return otherAtom;
        }

        public override string GetTag()
        {
            return Tag;
        }

        /// <summary>
        /// sets atoms in bond
        /// checks uniqueness and non nullity
        /// also chacked that both atoms belong to same molecule
        /// </summary>
        /// <param name="atom1"></param>
        /// <param name="atom2"></param>
        public bool SetAtomRefs2(CmlAtom atom1, CmlAtom atom2)
        {
            bool ok = false;
            if (atom1 != null && atom2 != null && !atom1.Id.Equals(atom2.Id))
            {
                CmlMolecule molecule1 = CmlUtils.GetFirstAncestorMolecule(atom1);
                CmlMolecule molecule2 = CmlUtils.GetFirstAncestorMolecule(atom2);
                if (molecule1 == null || molecule2 == null)
                {
                    Log.Debug("null owner molecule");
                }
                    // not yet working
                    //                else if (!molecule1.Equals(molecule2))
                    //                {
                    //                    CMLUtils.debug("different owner molecules");
                    //                }
                else if (atom1.Id != null && atom2.Id != null)
                {
                    SetAtomRefs2(new[] {atom1.Id, atom2.Id});
                    ok = true;
                }
            }
            return ok;
        }

        /// <summary>
        /// returns grandparent molecule 
        /// </summary>
        /// <returns>null if none</returns>
        public CmlMolecule GetFirstAncestorMolecule()
        {
            XElement parent = DelegateElement.Parent;
            XElement grandParent = (parent == null) ? null : parent.Parent;
            return (grandParent == null) ? null : new CmlMolecule(grandParent);
        }

        /// <summary>
        /// transforms a IEnumerable of pointers into bonds
        /// </summary>
        /// <param name="bondPointers">bond pointers</param>
        /// <returns>IEnumerable of CMLBonds</returns>
        public static IEnumerable<CmlBond> GetBonds(IEnumerable<XElement> bondPointers)
        {
            List<CmlBond> bonds = null;
            if (bondPointers != null)
            {
                bonds = new List<CmlBond>();
                foreach (XElement bondPointer in bondPointers)
                {
                    bonds.Add(new CmlBond(bondPointer));
                }
            }
            return bonds;
        }

        /// <summary>
        /// transforms a IEnumerable of bonds into pointers
        /// </summary>
        /// <param name="bonds">bonds</param>
        /// <returns>IEnumerable of XElement bondPointers</returns>
        public static IEnumerable<XElement> GetBondPointers(IEnumerable<CmlBond> bonds)
        {
            List<XElement> bondPointers = null;
            if (bonds != null)
            {
                bondPointers = new List<XElement>();
                foreach (CmlBond bond in bonds)
                {
                    bondPointers.Add(bond.DelegateElement);
                }
            }
            return bondPointers;
        }

        /// <summary>
        ///  deletes delegate bond and adds stub bonds capped with Dummy to each atom 
        ///  the bond order is retained
        /// </summary>
        /// <returns>list of new dummy atoms</returns>
        public void DeleteAndAddBondsCappedWithHydrogen(CmlMolecule molecule, double stubScaleFactor)
        {
            List<CmlBond> dummyBonds = DeleteAndAddBondsCappedWithDummy(stubScaleFactor);
            molecule.RemoveDummyBondsAndProcessHydrogens(stubScaleFactor, dummyBonds);
        }

        /// <summary>
        ///  deletes delegate bond and adds stub bonds capped with Dummy to each atom 
        ///  the bond order is retained
        /// </summary>
        /// <returns>list of new  bonds to dummy atoms</returns>
        internal List<CmlBond> DeleteAndAddBondsCappedWithDummy(double stubScaleFactor)
        {
            List<CmlBond> dummyBondList = new List<CmlBond>();
            CmlMolecule molecule = GetFirstAncestorMolecule();
            IEnumerable<CmlAtom> atoms = GetAtoms();

            CmlAtom atom0 = atoms.ElementAt(0);
            CmlBond newBond = AddBondCappedWithDummy(atom0, stubScaleFactor, molecule);
            dummyBondList.Add(newBond);

            CmlAtom atom1 = GetAtoms().ElementAt(1);
            newBond = AddBondCappedWithDummy(atom1, stubScaleFactor, molecule);
            dummyBondList.Add(newBond);

            DeleteSimple(true);
            return dummyBondList;
        }

        private static void RemoveDummyBondAndProcessHydrogens(double stubScaleFactor, CmlBond dummyBond)
        {
            throw new NotImplementedException();
        }

        internal void RemoveDummyOrChangeToHForCommonAtoms(double stubScaleFactor)
        {
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
            if (molecule != null)
            {
                IEnumerable<CmlAtom> atoms = GetAtoms();
                CmlAtom atom0 = atoms.ElementAt(0);
                CmlAtom atom1 = atoms.ElementAt(1);
                int dummy = (PeriodicTable.Element.Du.Equals(PeriodicTable.GetElement(atom0.ElementType))) ? 0 : 1;
                int other = 1 - dummy;
                CmlAtom otherAtom = atoms.ElementAt(other);
                CmlAtom dummyAtom = atoms.ElementAt(dummy);
                PeriodicTable.Element otherElement = PeriodicTable.GetElement(otherAtom.ElementType);
                if (CmlMolecule.CommonElementSet.Contains(otherElement))
                {
                    ReplaceDummyByHydrogen(stubScaleFactor, molecule, otherAtom, dummyAtom);
                }
                else
                {
                    dummyAtom.DelegateElement.Remove();
                    DelegateElement.Remove();
                }
            }
        }

        private void ReplaceDummyByHydrogen(double stubScaleFactor, CmlMolecule molecule, CmlAtom otherAtom,
                                            CmlAtom dummyAtom)
        {
            dummyAtom.ElementType = PeriodicTable.Element.H.ToString();
            ScaleBond(otherAtom, dummyAtom, 1.0/stubScaleFactor);
            BondOrder bo = BondOrder.GetBondOrder(this);
            double? order = (bo == null) ? null : bo.NumericBondOrder;
            if (order != null)
            {
                double orderx = (double) order;
                Point2 point = otherAtom.Point2;
                if (Math.Abs(orderx - 2.0) < 0.1)
                {
                    SplitAndCapDoubleBondWithHydrogen(molecule, otherAtom);
                }
                if (Math.Abs(orderx - 3.0) < 0.1)
                {
                    SplitAndCapTripleBondWithHydrogen(molecule, otherAtom);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        private CmlBond AddBondCappedWithDummy(CmlAtom atom, double stubScaleFactor, CmlMolecule molecule)
        {
            if (atom == null)
            {
                throw new ArgumentNullException("atom", "null atom");
            }
            int serial = this.GetAtomSerial(atom.Id);
            if (serial == -1)
            {
                throw new ArgumentException("atom is not in bond");
            }
            CmlAtom dummy = molecule.CreateAndAddAtomWithId(PeriodicTable.Element.Du);
            CmlAtom atom1 = GetAtomAtOtherEnd(atom);
            dummy.Point2 = atom1.Point2;

            CmlBond bond = new CmlBond();
            bond.SetAtomRefs2(new[] {atom.Id, dummy.Id});
            bond.GenerateAndAddId();
            bond.Order = Order;
            molecule.AddBond(bond);
            bond.ScaleBond(atom, dummy, stubScaleFactor);
            return bond;
        }

        private void SplitAndCapDoubleBondWithHydrogen(CmlMolecule molecule, CmlAtom otherAtom)
        {
            Order = Single;
            CmlBond this1 = CloneAndAddDummyBondAndCapWithHydrogen(molecule, otherAtom);
            RotateBond(new Angle(Math.PI/4.0, Angle.Unit.Radians), otherAtom);
            this1.RotateBond(new Angle(-Math.PI/4.0, Angle.Unit.Radians), otherAtom);
        }

        private void SplitAndCapTripleBondWithHydrogen(CmlMolecule molecule, CmlAtom otherAtom)
        {
            Order = Single;
            CmlBond this1 = CloneAndAddDummyBondAndCapWithHydrogen(molecule, otherAtom);
            this1.RotateBond(new Angle(Math.PI/2.0, Angle.Unit.Radians), otherAtom);
            CmlBond this2 = CloneAndAddDummyBondAndCapWithHydrogen(molecule, otherAtom);
            this2.RotateBond(new Angle(-Math.PI/2.0, Angle.Unit.Radians), otherAtom);
        }

        private void RotateBond(Angle angle, CmlAtom otherAtom)
        {
            //            CMLMolecule molecule = this.GetFirstAncestorMolecule();
            Transform2 t2 = Transform2.CreateRotationMatrixAboutPoint(angle, otherAtom.Point2);
            CmlAtom dummyAtom = GetAtomAtOtherEnd(otherAtom);
            dummyAtom.Point2 = t2.Transform(dummyAtom.Point2);
        }

        private CmlBond CloneAndAddDummyBondAndCapWithHydrogen(
            CmlMolecule molecule, CmlAtom otherAtom)
        {
            CmlAtom dummyAtom = GetAtomAtOtherEnd(otherAtom);
            CmlAtom dummyAtom1 = molecule.CreateAndAddAtomWithId(PeriodicTable.Element.H);
            dummyAtom1.Point2 = new Point2(dummyAtom.Point2);

            // have to use this constructor as in different molecule
            CmlBond dummyBond1 = new CmlBond();
            dummyBond1.Order = Single;
            dummyBond1.SetAtomRefs2(new[] {otherAtom.Id, dummyAtom1.Id});
            dummyBond1.Id = otherAtom.Id + "_" + dummyAtom1.Id;
            molecule.AddBond(dummyBond1);
            return dummyBond1;
        }

        /// <summary>
        /// scale bond length by adjusting coords of this.GetOtherAtom(atom)
        /// </summary>
        /// <param name="atom0"></param>
        /// <param name="atom1"></param>
        /// <param name="scaleFactor">length of bond</param>
        private void ScaleBond(CmlAtom atom0, CmlAtom atom1, double scaleFactor)
        {
            if (atom0 == null)
            {
                throw new ArgumentNullException("atom0", "null atom");
            }
            if (atom1 == null)
            {
                throw new ArgumentNullException("atom1", "null atom");
            }
            Vector2 vector = GetVector2(atom0);
            if (vector == null)
            {
                throw new ArgumentException("no vector created");
            }
            vector.MultiplyBy(scaleFactor);
            Point2 point0 = atom0.Point2;
            Point2 point1 = point0.Add(vector);
            atom1.Point2 = point1;
        }

        /// <summary>
        /// called by CID
        /// </summary>
        /// <param name="groupAbbreviation"></param>
        public void SubstituteBondAndHydrogensWithGroup(string groupAbbreviation)
        {
            GroupLookup groupDictionary = GetSubstitutionDictionaryFromBondAtomsAndOrder();
            if (groupDictionary != null)
            {
                GetDictionaryEntryAndReplaceGroup(groupDictionary, groupAbbreviation);
                NormalizeAndValidate();
            }
        }

        private void NormalizeAndValidate()
        {
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
            if (molecule != null)
            {
                molecule.NormalizeConciseFormulas();
                molecule.CheckAndFixStereochemistry();
            }
        }

        public GroupLookup GetSubstitutionDictionaryFromBondAtomsAndOrder()
        {
            string abbreviation = GetAbbreviationFromBondAtomsAndOrder();
            GroupLookup groupDictionary = GroupLookupManager.Builtin.GetGroupDictionary(abbreviation);
            return groupDictionary;
        }

        public string GetAbbreviationFromBondAtomsAndOrder()
        {
            // TODO - generalize this with search, etc.
            string abbreviation = "unknown";
            CmlAtom atom0 = GetAtoms().ElementAt(0);
            CmlAtom atom1 = GetAtoms().ElementAt(1);
            PeriodicTable.Element elem0 = PeriodicTable.GetElement(atom0.ElementType);
            PeriodicTable.Element elem1 = PeriodicTable.GetElement(atom1.ElementType);
            BondOrder bondOrder = BondOrder.GetBondOrder(this);
            int bo = bondOrder.IntegerBondOrder ?? 0;
            // C...O
            if ((elem0.Equals(PeriodicTable.Element.O) && elem1.Equals(PeriodicTable.Element.C)) ||
                (elem0.Equals(PeriodicTable.Element.C) && elem1.Equals(PeriodicTable.Element.O)))
            {
                // make C atom0 to normalize ligands
                if (elem0.Equals(PeriodicTable.Element.O))
                {
                    CmlAtom temp = atom0;
                    atom0 = atom1;
                    atom1 = temp;
                }
                // get ligands except this bond
                ICollection<CmlAtom> cLigands = atom0.GetLigands();
                ICollection<CmlAtom> nonSelfLigands = new List<CmlAtom>();
                foreach (CmlAtom cLigand in cLigands)
                {
                    if (!cLigand.Equals(atom1))
                    {
                        nonSelfLigands.Add(cLigand);
                    }
                }
                if (bo == 2)
                {
                    // both LIGAND atoms C?
                    if (
                        PeriodicTable.Element.C.Equals(PeriodicTable.GetElement(nonSelfLigands.ElementAt(0).ElementType)) &&
                        PeriodicTable.Element.C.Equals(PeriodicTable.GetElement(nonSelfLigands.ElementAt(1).ElementType)))
                    {
                        abbreviation = "ketone";
                    }
                }
                else if (bo == 1)
                {
                    // both atoms C?
                    if (
                        PeriodicTable.Element.C.Equals(PeriodicTable.GetElement(nonSelfLigands.ElementAt(0).ElementType)) &&
                        PeriodicTable.Element.C.Equals(PeriodicTable.GetElement(nonSelfLigands.ElementAt(1).ElementType)))
                    {
                        abbreviation = "ol";
                    }
                }
            }
                // C...C
            else if (elem0.Equals(PeriodicTable.Element.C) && elem1.Equals(PeriodicTable.Element.C))
            {
                if (bo == 1)
                {
                    abbreviation = "ane";
                }
                else if (bo == 2)
                {
                    abbreviation = "ene";
                }
                else if (bo == 3)
                {
                    abbreviation = "yne";
                }
            }
            return abbreviation;
        }

        private void GetDictionaryEntryAndReplaceGroup(
            GroupLookup groupDictionary, string groupAbbreviation)
        {
            CmlAtom atom0 = GetAtoms().ElementAt(0);
            CmlAtom atom1 = GetAtoms().ElementAt(1);
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
            CmlMolecule groupMolecule = groupDictionary.GetGroup(groupAbbreviation);
            if (groupMolecule == null)
            {
                throw new ArgumentException("Cannot find group: " + groupAbbreviation);
            }
            // clone group
            CmlMolecule groupClone = new CmlMolecule(groupMolecule);
            Log.Debug(groupClone.DelegateElement.ToString());

            // the first atom must match the bond somewhere
            CmlAtom cloneAtom0 = groupClone.GetAtomByLabelValue("r1");
            CmlLabel label0 = new CmlLabel(CmlUtils.GetChildLabels(cloneAtom0.DelegateElement).First());
            string dictRef0 = label0.DictRef;
            PeriodicTable.Element el0 = PeriodicTable.GetElement(cloneAtom0.ElementType);
            CmlAtom cloneAtom1 = groupClone.GetAtomByLabelValue("r2");
            CmlLabel label1 = new CmlLabel(CmlUtils.GetChildLabels(cloneAtom1.DelegateElement).First());
            string dictRef1 = label1.DictRef;
            PeriodicTable.Element el1 = PeriodicTable.GetElement(cloneAtom1.ElementType);
            if (dictRef0 == null || dictRef1 == null)
            {
                Log.Debug("labels must have dictRef");
            }
            // align atoms with bond, swap if necessary
            if (atom1.ElementType.Equals(cloneAtom0.ElementType))
            {
                CmlAtom temp = atom0;
                atom0 = atom1;
                atom1 = temp;
            }
            // now remove labels
            CmlUtils.RemoveAllChildLabels(cloneAtom0);
            CmlUtils.RemoveAllChildLabels(cloneAtom1);

            if (PeriodicTable.Element.R.Equals(el0))
            {
                cloneAtom0.ElementType = atom0.ElementType;
                atom0.DeleteHydrogen(1);
            }
            if (PeriodicTable.Element.R.Equals(el1))
            {
                cloneAtom1.ElementType = atom1.ElementType;
                atom1.DeleteHydrogen(1);
            }

            groupClone.RenumberAtomsBonds(molecule);

            // orient bond vectors
            Vector2 thisVector = atom0.GetVectorTo(atom1);
            // does group require geometrical re-orientation?
            if (PeriodicTable.Element.R.Equals(el0) && PeriodicTable.Element.R.Equals(el1))
            {
                Vector2 prettyVector = GetPrettyDoubleBondVector();
                Angle angle0 = prettyVector.AngleTo(thisVector);
                if (angle0.Sign() > 0)
                {
                    Transform2 transform = Transform2.FlipAboutVectorThroughPoint(new Vector2(1.0, 0.0),
                                                                                  new Point2(0.0, 0.0));
                    groupClone.Transform2D(transform);
                }
            }
            Vector2 cloneVector = cloneAtom0.GetVectorTo(cloneAtom1);
            Angle angle = cloneVector.AngleTo(thisVector);
            Transform2 rotationTransform = Transform2.CreateRotationMatrixAboutOrigin(angle);
            groupClone.Transform2D(rotationTransform);

            // translate
            Point2 cloneCentroid = cloneAtom0.Point2.Add(cloneAtom1.Point2);
            cloneCentroid = cloneCentroid.MultiplyBy(0.5);
            Point2 thisCentroid = GetCentroid();
            Vector2 translationVector = thisCentroid.Subtract(cloneCentroid);
            Transform2 translationTransform = Transform2.CreateTranslation(translationVector);
            groupClone.Transform2D(translationTransform);

            // delete and clean
            MakeBondsToClone(atom0, cloneAtom0);
            MakeBondsToClone(atom1, cloneAtom1);
            atom0.DeleteSimple(true);
            atom1.DeleteSimple(true);
            DeleteSimple(true);
            // copy groupClone
            molecule.TransferBondsAndAtomsToMolecule(groupClone);
            //            molecule.RemoveCyclicIndicators();
            Log.Debug(molecule.DelegateElement.ToString());
        }

        private void MakeBondsToClone(CmlAtom atom, CmlAtom cloneAtom)
        {
            IEnumerable<CmlBond> ligandBonds = atom.GetLigandBonds();
            foreach (CmlBond ligandBond in ligandBonds)
            {
                CmlAtom ligandAtom = ligandBond.GetAtomAtOtherEnd(atom);
                // skip this
                if (ligandAtom.Equals(GetAtomAtOtherEnd(atom)))
                {
                    continue;
                }
                ligandBond.SetAtomRefs2(new[] {ligandAtom.Id, cloneAtom.Id});
            }
        }

        /// <summary>
        /// generate a unique ID for the bond
        /// uses atomRefs2 if present else Guid
        /// </summary>
        internal void GenerateAndAddId()
        {
            string[] atomRefs2 = GetAtomRefs2();
            if (atomRefs2 != null)
            {
                Id = atomRefs2[0] + "_" + atomRefs2[1];
            }
            else
            {
                Id = "b" + Guid.NewGuid();
            }
        }

        /// <summary>
        /// get vector corresponding to bond
        /// </summary>
        /// <param name="atom0">atom at root of vector</param>
        /// <returns></returns>
        public Vector2 GetVector2(CmlAtom atom0)
        {
            Vector2 vector = null;
            if (atom0 != null)
            {
                CmlAtom atom1 = GetAtomAtOtherEnd(atom0);
                Point2 point0 = atom0.Point2;
                Point2 point1 = atom1.Point2;
                if (point0 != null && point1 != null)
                {
                    vector = point1.Subtract(point0);
                }
            }
            return vector;
        }

        /// <summary>
        /// Determines whether the argument is a CMLLite bond
        /// 
        /// at the moment this only test the name and namespace - in future
        /// this will check more
        /// </summary>
        /// <param name="element">the XElement to test</param>
        /// <returns>true if element is a CMLBond otherwise false</returns>
        public static bool IsBond(XElement element)
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
        /// Returns true if the bond is definitely cyclic otherwise false
        /// 
        /// this method compares the value returned by ForceGetCyclicIndicator()
        /// to Cyclicity.Cyclic - other options are Cyclicity.Acyclic and Cyclicity.Unknown
        /// and may be more useful for the user.
        /// <see cref="ForceGetCyclicIndicator"/>
        /// </summary>
        /// <returns>true if the bond is definitely cyclic otherwise false</returns>
        public bool IsCyclic()
        {
            return Cyclicity.Cyclic.Equals(ForceGetCyclicIndicator());
        }

        /// <summary>
        /// gets serial number (0, 1) of atom in bond
        /// </summary>
        /// <param name="id"></param>
        /// <returns>-1 if not found</returns>
        public int GetAtomSerial(string id)
        {
            int serial = -1;
            if (id == null)
            {}
            else if (id.Equals(GetAtomRefs2().ElementAt(0)))
            {
                serial = 0;
            }
            else if (id.Equals(GetAtomRefs2().ElementAt(1)))
            {
                serial = 1;
            }
            return serial;
        }

        /// <summary>
        /// can the atom be substituted by a group
        /// requires a single non-hydorgen ligand with acyclic bond
        /// </summary>
        /// <returns></returns>
        public CmlMolecule.GroupType GetSubstitutionGroup()
        {
            CmlMolecule.GroupType groupType = CmlMolecule.GroupType.Unknown;
            Cyclicity cyclicity = ForceGetCyclicIndicator();
            if (BondOrder.Single.Equals(BondOrder))
            {
                if (Cyclicity.Acyclic.Equals(cyclicity))
                {
                    groupType = CmlMolecule.GroupType.AcyclicSingle;
                }
                else if (Cyclicity.Cyclic.Equals(cyclicity))
                {
                    groupType = CmlMolecule.GroupType.CyclicSingle;
                }
            }
            else if (BondOrder.Double.Equals(BondOrder))
            {
                if (Cyclicity.Acyclic.Equals(cyclicity))
                {
                    groupType = CmlMolecule.GroupType.AcyclicDouble;
                }
                else if (Cyclicity.Cyclic.Equals(cyclicity))
                {
                    groupType = CmlMolecule.GroupType.CyclicDouble;
                }
            }
            return groupType;
        }

        public Vector2 GetPrettyDoubleBondVector()
        {
            Vector2 vector = null;

            if (IsCyclic())
            {
                return GetPrettyCyclicDoubleBondVector();
            }

            // We're acyclic.

            CmlAtom atom1;
            string[] a1LigandIDs;
            GetAtomAndLigandIDs(0, out atom1, out a1LigandIDs);

            if (a1LigandIDs.Length == 0)
            {
                return null;
            }

            CmlAtom[] atom1Atoms = GetAtoms(atom1, a1LigandIDs);

            CmlAtom atom2;
            string[] a2LigandIDs;
            GetAtomAndLigandIDs(1, out atom2, out a2LigandIDs);

            if (a2LigandIDs.Length == 0)
            {
                return null;
            }

            CmlAtom[] atom2Atoms = GetAtoms(atom2, a2LigandIDs);

            if (atom1Atoms.Length > 2 || atom2Atoms.Length > 2)
            {
                return null;
            }

            if (atom1Atoms.AreAllH() && atom2Atoms.AreAllH())
            {
                return null;
            }

            if (atom1Atoms.ContainNoH() && atom2Atoms.ContainNoH())
            {
                return null;
            }

            if (atom1Atoms.GetHCount() == 1 && atom1Atoms.GetNonHCount() == 1)
            {
                if (atom2Atoms.Length == 2)
                {
                    if (atom2Atoms.GetHCount() == 2 || atom2Atoms.ContainNoH())
                    {
                        //Double sided bond on the side of the non H atom from atom1Atoms
                        //Elbow bond :¬)
                        //DrawElbowBond(atom1);
                        return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom1Atoms);
                    }
                    // Now must have 1 H and 1 !H
                    if (AtomsAreOnTheSameSide(atom1Atoms.GetFirstNonH(), atom2Atoms.GetFirstNonH())
                        /*if a2a H on the same side as a1a H*/)
                    {
                        //double bond on the side of non H
                        return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom1Atoms);
                    }

                    //Now must be a trans bond.
                    return null;
                }
                else
                {
                    //Count now 1
                    if (atom2Atoms.GetHCount() == 1)
                    {
                        //Double bond on the side of non H from atom1Atoms, bevel 1 end.
                        return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom1Atoms);
                    }

                    //Now !H
                    if (AtomsAreOnTheSameSide(atom1Atoms.GetFirstNonH(), atom2Atoms.GetFirstNonH())
                        /*atom2Atom's !H is on the same side as atom1Atom's !H*/)
                    {
                        //double bond on the side of !H from atom1Atoms, bevel both ends
                        return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom1Atoms);
                    }

                    //Now must be a trans bond.
                    return null;
                }
            }
            else if (atom1Atoms.AreAllH())
            {
                if (atom2Atoms.Length == 2)
                {
                    if (atom2Atoms.AreAllH())
                    {
                        //Already caught.
                        throw new ApplicationException("This scenario should already have been caught");
                    }
                    if (atom2Atoms.ContainNoH())
                    {
                        return null;
                    }

                    //Must now have 1 H and 1 !H
                    //double bond on the side of Atom2Atoms' !H, bevel 1 end only
                    return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom2Atoms);
                }
                //Count must now be 1

                if (atom2Atoms.AreAllH())
                {
                    //Already caught
                    throw new ApplicationException("This scenario should already have been caught");
                }

                // Must now be 1 !H
                // Double bond on the side of atom2Atoms' !H, bevel 1 end only.
                return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom2Atoms);
            }
            else if (atom1Atoms.GetHCount() == 0)
            {
                if (atom2Atoms.Length == 2)
                {
                    if (atom2Atoms.AreAllH())
                    {
                        return null;
                    }
                    if (atom2Atoms.ContainNoH())
                    {
                        return null;
                    }
                    // Now must have 1 H and 1 !H
                    //Double bond on the side of Atom2Atoms' !H, bevel both ends.
                    return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom2Atoms);
                }
                //Count is 1
                if (atom2Atoms.GetHCount() == 1)
                {
                    return null;
                }

                if (atom2Atoms.GetHCount() == 0)
                {
                    //double bond on the side of atom2Atoms' !H, bevel both ends.
                    return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom2Atoms);
                }
            }
                // atom1Atoms' count = 1

            else if (atom1Atoms.GetHCount() == 1)
            {
                if (atom2Atoms.Length == 2)
                {
                    if (atom2Atoms.AreAllH())
                    {
                        // Already caught.
                        throw new ApplicationException("This scenario should already have been caught");
                    }
                    if (atom2Atoms.ContainNoH())
                    {
                        return null;
                    }

                    //Now atom2Atoms contains 1 H and 1 !H
                    //double bond on side of atom2Atoms' !H, bevel 1 end
                    return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom2Atoms);
                }

                if (atom2Atoms.AreAllH())
                {
                    // Already caught.
                    throw new ApplicationException("This scenario should already have been caught");
                }
            }
            else if (atom1Atoms.GetHCount() == 0)
            {
                if (atom2Atoms.Length == 2)
                {
                    if (atom2Atoms.AreAllH())
                    {
                        //Double dbond on the side of atom1Atoms' !H, bevel one end.
                        return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom1Atoms);
                    }
                    if (atom2Atoms.ContainNoH())
                    {
                        //double bond on the side of atom1Atoms' !H, bevel both end.
                        return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom1Atoms);
                    }

                    //Now atom2Atoms contains 1 H and 1 !H
                    if (AtomsAreOnTheSameSide(atom1Atoms.GetFirstNonH(), atom2Atoms.GetFirstNonH())
                        /* atom2Atoms' !H is on the same side as atom1Atom's !H */)
                    {
                        //double bond on the side of atom1Atoms' !H, bevel both ends
                        return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom1Atoms);
                    }

                    // Must be trans
                    return null;
                }

                // atoms2Atoms length = 1
                if (atom2Atoms.GetHCount() == 1)
                {
                    //double bond on side of atom1Atoms' !H, bevel one end.
                    return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom1Atoms);
                }

                // atom2Atoms must contain 1 !H
                if (AtomsAreOnTheSameSide(atom1Atoms.GetFirstNonH(), atom2Atoms.GetFirstNonH())
                    /* atom2Atoms' !H is on same side as atom1Atoms' !H */)
                {
                    //double bond on side of atom1Atoms' !H, bevel both ends
                    return VectorOnSideOfNonHAtomFromAtom1Atoms(atom1, atom2, atom1Atoms);
                }

                //Must be trans
                return null;
            }

            return vector;
        }

        private bool AtomsAreOnTheSameSide(CmlAtom atom1, CmlAtom atom2)
        {
            Vector v = atom2.Point2.ToPoint() - atom1.Point2.ToPoint();
            Vector perp = new Vector(-v.Y, v.X);

            // Let's make a big rectangle. :)
            StreamGeometry geometry = new StreamGeometry();

            Point p1 = atom1.Point2.ToPoint() - v*10000;
            Point p2 = p1 + perp*10000;
            Point p3 = p2 + v*20000;
            Point p4 = p3 - perp*10000;

            using (StreamGeometryContext ctx = geometry.Open())
            {
                ctx.BeginFigure(p1, true, true);
                ctx.LineTo(p2, true, false);
                ctx.LineTo(p3, true, false);
                ctx.LineTo(p4, true, false);
            }

            Point point1 = atom1.Point2.ToPoint();
            Point point2 = atom2.Point2.ToPoint();

            bool containsPoint1 = geometry.FillContains(point1);
            bool containsPoint2 = geometry.FillContains(point2);

            Log.Debug("contains point 1 : " + containsPoint1);
            Log.Debug("contains point 2 : " + containsPoint2);

            return containsPoint1 == containsPoint2;
        }

        private Vector2 VectorOnSideOfNonHAtomFromAtom1Atoms(CmlAtom atom1, CmlAtom atom2, CmlAtom[] atom1Atoms)
        {
            Vector2 bondVector = atom2.Point2 - atom1.Point2;

            Vector2 posDisplacementVector = new Vector2(-bondVector.Y, bondVector.X);
            Vector2 negDisplacementVector = new Vector2(bondVector.Y, -bondVector.X);

            posDisplacementVector.Normalize();
            negDisplacementVector.Normalize();

            posDisplacementVector.MultiplyBy(3);
            negDisplacementVector.MultiplyBy(3);

            Point2 posEndPoint = atom2.Point2.Add(posDisplacementVector);
            Point2 negEndPoint = atom2.Point2.Add(negDisplacementVector);

            CmlAtom nonHAtom = atom1Atoms.GetFirstNonH();
            Point2 nonHAtomLoc = nonHAtom.Point2;

            double posDist = nonHAtomLoc.Subtract(posEndPoint).Length;
            double negDist = nonHAtomLoc.Subtract(negEndPoint).Length;

            bool posDisplacement = posDist < negDist;
            Vector2 displacementVector = posDisplacement ? posDisplacementVector : negDisplacementVector;

            return displacementVector;
        }

        private CmlAtom[] GetAtoms(CmlAtom atom, string[] ligandIDs)
        {
            CmlBond[] bonds = new CmlBond[ligandIDs.Length];
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atom);

            for (int i = 0; i < ligandIDs.Length; i++)
            {
                bonds[i] = molecule.GetBondById(ligandIDs[i]);
            }

            List<CmlAtom> childAtoms = new List<CmlAtom>();

            foreach (CmlBond b in bonds)
            {
                childAtoms.Add(b.GetAtomAtOtherEnd(atom));
            }

            return childAtoms.ToArray();
        }

        private void GetAtomAndLigandIDs(int atomIndex, out CmlAtom atom, out string[] ligandIDs)
        {
            atom = GetAtoms().ElementAt(atomIndex);
            ligandIDs = GetLigandIDs(atom);
        }

        private string[] GetLigandIDs(CmlAtom atom)
        {
            string[] ligandIDs;
            ICollection<string> tempLigandIDs = atom.GetLigandBondIds();
            StringCollection temp = new StringCollection();
            temp.AddRange(tempLigandIDs.ToArray());
            temp.Remove(Id);

            ligandIDs = new string[temp.Count];
            temp.CopyTo(ligandIDs, 0);
            return ligandIDs;
        }

        /// <summary>
        /// indicates which side of bond to draw subsidiary double bond
        /// </summary>
        /// if bond order is not 2, returns null
        /// if bond is in single ring, vector points to centre of ring
        /// if bond is in cisoid bond (excluding hydrogens) points to area
        /// including both bonds
        /// if bond is in 3 rings vector is null
        /// 
        /// this is not foolproof as we may need to make a benzene ring consistent
        /// <returns></returns>
        public Vector2 GetPrettyCyclicDoubleBondVector()
        {
            Vector2 vector = null;
            IEnumerable<Ring> ringList = GetRingList();
            Point2 midPoint = GetCentroid();
            if (ringList.Count() == 0)
            {
                vector = GetVector2(GetAtoms().ElementAt(0));
                vector = new Vector2(vector.Y, -vector.X);
            }
            else if (ringList.Count() == 1)
            {
                Ring ring = ringList.ElementAt(0);
                Point2 ringCentroid = ring.GetCentroid();
                if (ringCentroid != null && midPoint != null)
                {
                    vector = ringCentroid.Subtract(midPoint);
                }
            }
            else if (ringList.Count() == 2)
            {
                // which ring has double bonds?
                Ring ring0 = ringList.ElementAt(0);
                int size0 = ring0.BondIdSet.Count();
                int doubleBondCount0 = ring0.GetDoubleBondCount();
                Ring ring1 = ringList.ElementAt(1);
                int size1 = ring1.BondIdSet.Count();
                int doubleBondCount1 = ring1.GetDoubleBondCount();
                Ring theRing = null;
                // only one double per ring
                if (doubleBondCount0 == 1 && doubleBondCount1 == 1)
                {
                    // null
                }
                    // conjugated
                else if (size0 == 2*doubleBondCount0)
                {
                    theRing = ring0;
                }
                else if (size1 == 2*doubleBondCount1)
                {
                    theRing = ring1;
                }
                    // which ring has more doubles
                else if (doubleBondCount0 > doubleBondCount1)
                {
                    theRing = ring0;
                }
                else
                {
                    theRing = ring1;
                }
                Point2 ringCentroid = theRing.GetCentroid();
                if (ringCentroid != null && midPoint != null)
                {
                    vector = ringCentroid.Subtract(midPoint);
                }
            }
            return vector;
        }

        /// <summary>
        /// gets ringNucleus in which bond appears
        /// </summary>
        /// <param name="bond">bond</param>
        /// <returns>null if bond is null or not in any ringNucleus</returns>
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
        /// gets rings in which bond appears
        /// </summary>
        /// <returns>zero length list if not in any rings</returns>
        public ICollection<Ring> GetRingList()
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

        public Nullable<double> GetBondLength()
        {
            Nullable<double> dist = null;
            CmlAtom atom0 = GetAtoms().ElementAt(0);
            CmlAtom atom1 = GetAtoms().ElementAt(1);
            Point2 point0 = atom0.Point2;
            Point2 point1 = atom1.Point2;
            if (point0 != null && point1 != null)
            {
                Vector2 v = point1.Subtract(point0);
                dist = v.GetLength();
            }
            return dist;
        }

        /// <summary>
        /// get all atoms including and downstream of startId
        /// </summary>
        /// starts at startId and recurses down subtree. If 'this' is a cyclic
        /// bond will include the whole molecule
        /// <param name="startId">atom to start at</param>
        /// <returns>list of downstream atoms (must include startId)</returns>
        public IEnumerable<string> GetDownstreamAtomIds(string startId)
        {
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
            List<string> atomList = null;
            string otherAtomId = GetAtomIdAtOtherEnd(startId);
            if (otherAtomId != null)
            {
                Queue<string> queue = new Queue<string>();
                queue.Enqueue(startId);
                atomList = new List<string>();
                HashSet<string> usedSet = new HashSet<string>();
                usedSet.Add(otherAtomId);
                while (queue.Count > 0)
                {
                    string nextAtomId = queue.Dequeue();
                    CmlAtom atom = molecule.GetAtomById(nextAtomId);
                    usedSet.Add(nextAtomId);
                    IEnumerable<string> ligandIds = atom.GetLigandIds();
                    foreach (string ligandId in ligandIds)
                    {
                        if (!usedSet.Contains(ligandId))
                        {
                            queue.Enqueue(ligandId);
                        }
                    }
                }
                atomList = new List<string>();
                foreach (string atomId in usedSet)
                {
                    if (!atomId.Equals(otherAtomId))
                    {
                        atomList.Add(atomId);
                    }
                }
            }
            return atomList;
        }

        /// <summary>
        /// flip 2D coordinates about bond
        /// </summary>
        /// atoms downstream of startId are flipped about bond vector
        /// <param name="startId">atomId to start at</param>
        public bool FlipBond(string startId)
        {
            bool flip = false;
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(this);
            CmlAtom startAtom = molecule.GetAtomById(startId);
            CmlAtom otherAtom = GetAtomAtOtherEnd(startAtom);
            if (otherAtom != null)
            {
                // tests whether all atoms have coordinates
                Point2 centroid = molecule.GetCentroid();
                if (centroid != null && startAtom != null)
                {
                    IEnumerable<string> downstreamAtomIds = GetDownstreamAtomIds(startId);
                    Log.Debug("DOWNSTREAM " + CmlUtils.Concatenate(downstreamAtomIds));
                    if (downstreamAtomIds != null)
                    {
                        Vector2 bondVector = startAtom.Point2.Subtract(otherAtom.Point2);
                        Point2 otherPoint = otherAtom.Point2;
                        Transform2 flipTransform = Transform2.FlipAboutVectorThroughPoint(
                            bondVector, otherPoint);
                        CmlAtom.TransformAtoms(molecule, downstreamAtomIds, flipTransform);
                        flip = true;
                    }
                }
            }
            return flip;
        }

        /// <summary>
        /// convertes numeric orders (1,2,3) to S,D,T
        /// </summary>
        public void NormaliseOrderOnImport()
        {
            const string deprecatedSingle = "1";
            const string deprecatedDouble = "2";
            const string deprecatedTriple = "3";

            string order = Order;
            switch (order)
            {
                case deprecatedSingle:
                    Order = Single;
                    break;
                case deprecatedDouble:
                    Order = Double;
                    break;
                case deprecatedTriple:
                    Order = Triple;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// sets order to "numeric" value (S,D,T)
        /// then adjusts chemistry on atoms 
        /// </summary>
        /// <param name="value"></param>
        public void SetOrderAdjustChemistry(string value)
        {
            BondOrder bondOrder = BondOrder.GetBondOrderFromCmlOrder(value);
            SetOrderAndAdjustChemistry(bondOrder);
        }

        /// <summary>
        /// sets bond order (S,D,T) and then adjust chemistry of atoms
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool SetOrderAndAdjustChemistry(BondOrder newOrder)
        {
            bool ok = false;
            if (newOrder == null)
            {
                throw new ArgumentNullException("newOrder");
            }
            int? newIntOrder = newOrder.IntegerBondOrder;
            // not integer order - just set it
            if (newIntOrder == null)
            {
                SetOrder(newOrder.CmlBondOrder);
                AddCmlxAttribute("forcedChange", "bondOrder->" + newOrder.CmlBondOrder);
                ok = true;
            }
            else
            {
                // current order (if int)
                int? thisIntOrder = BondOrder.GetBondOrder(this).IntegerBondOrder;
                CmlAtom atom0 = GetAtoms().ElementAt(0);
                CmlAtom atom1 = GetAtoms().ElementAt(1);
                if (thisIntOrder != null)
                {
                    int deltaHydrogen = (int) thisIntOrder - (int) newIntOrder;
                    if (deltaHydrogen > 0)
                    {
                        atom0.AddNakedHydrogensAndAdjustCoordinates(deltaHydrogen);
                        atom1.AddNakedHydrogensAndAdjustCoordinates(deltaHydrogen);
                    }
                    else if (deltaHydrogen < 0)
                    {
                        atom0.DeleteHydrogen(-deltaHydrogen);
                        atom1.DeleteHydrogen(-deltaHydrogen);
                    }
                }
                else
                {
                    AddCmlxAttribute(CmlConstants.ForcedChange, "bondOrder->" + newOrder.CmlBondOrder);
                }
                SetOrder(newOrder.CmlBondOrder);
            }
            return ok;
        }

        private void AddNakedHydrogensToBond(int deltaHydrogen)
        {
            CmlAtom atom0 = GetAtoms().ElementAt(0);
            CmlAtom atom1 = GetAtoms().ElementAt(1);
            int nHyd0 = atom0.GetHydrogenLigands().Count();
            int nHyd1 = atom1.GetHydrogenLigands().Count();
            // there must be enough H to delete from each end
            if (deltaHydrogen < 0 && nHyd0 >= deltaHydrogen && nHyd1 >= deltaHydrogen)
            {
                atom0.DeleteHydrogen(-deltaHydrogen);
                atom1.DeleteHydrogen(-deltaHydrogen);
            }
            else if (deltaHydrogen > 0)
            {
                atom0.AddNakedHydrogensAndAdjustCoordinates(deltaHydrogen);
                atom1.AddNakedHydrogensAndAdjustCoordinates(deltaHydrogen);
            }
        }

        /// <summary>
        /// removes bondStereo child
        /// does not affect any atomParity
        /// </summary>
        public void ClearStereo()
        {
            CmlBondStereo bondStereo = GetBondStereo();
            if (bondStereo != null)
            {
                CmlUtils.Remove(bondStereo);
            }
        }

        /// <summary>
        /// replace atom and all atoms downstream with group
        /// </summary>
        /// <param name="atom"></param>
        public void ReplaceGroupWithGroup(CmlAtom replaceableAtom, CmlMolecule group)
        {
            if (replaceableAtom == null)
            {
                throw new ArgumentNullException("replaceableAtom");
            }
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }

            string groupPointerType = null;
            string groupPointerName = null;
            CmlLabel dictRefLabel = replaceableAtom.GetFirstLabel(CmlAtom.CmlxGroupPointer);
            if (dictRefLabel != null)
            {
                groupPointerType = dictRefLabel.GetDictionaryType();
                groupPointerName = dictRefLabel.GetGroupName();
            }

            CmlAtom fixedAtom = GetAtomAtOtherEnd(replaceableAtom);
            if (fixedAtom == null)
            {
                throw new ArgumentException("atom (" + replaceableAtom.Id + ")not in bond: " + Id);
            }
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(replaceableAtom);
            molecule.CalculateAndSetCyclicIndicators();
            Cyclicity cyclicity = ForceGetCyclicIndicator();
            if (cyclicity.Equals(Cyclicity.Cyclic))
            {
                throw new NumboException("Cannot set group on cyclicity bond");
            }
            // clone group and renumber
            CmlMolecule groupClone = new CmlMolecule(group);
            groupClone.RenumberAtomsBonds(molecule);
            // has group got coordinates?
            if (groupClone.GetCentroid() == null)
            {
                throw new ArgumentException("No coordinates in group");
            }
            // get atoms in this to delete
            IEnumerable<string> atomIdList = GetDownstreamAtomIds(replaceableAtom.Id);
            // remove all atoms except the pivot
            foreach (string Id in atomIdList)
            {
                if (!Id.Equals(replaceableAtom.Id))
                {
                    CmlAtom downstreamAtom = molecule.GetAtomById(Id);
                    downstreamAtom.DeleteAndRemoveHangingBonds();
                }
            }

            CmlAtom rAtom = null;
            CmlAtom rLigand = null;
            // check has one R group with one ligand
            // this is used for groups with labelled atoms
            // e.g. R----N----X, where N is labelled aa:N
            // the R will be lost and the bond made to the N
            // this selects the R bonded to the labelled atom
            IEnumerable<CmlAtom> rGroups = groupClone.GetAtomsByElementType(PeriodicTable.Element.R);
            if (groupPointerName != null)
            {
                foreach (CmlAtom r in rGroups)
                {
                    IEnumerable<CmlAtom> rLigands = r.GetLigands();
                    foreach (CmlAtom rLigand0 in rLigands)
                    {
                        CmlLabel dRefLabel = rLigand0.GetFirstLabel(CmlAtom.CmlxGroup);
                        if (dRefLabel != null)
                        {
                            // pointer of form aa:N
                            if ((groupPointerType + ":" + groupPointerName).Equals(dRefLabel.Value))
                            {
                                rLigand = rLigand0;
                                rAtom = r;
                                break;
                            }
                        }
                    }
                }
                if (rAtom == null)
                {
                    throw new NumboException("cannot find Rgroup: " + groupPointerType + ":" + groupPointerName);
                }
            }
            else if (rGroups.Count() != 1)
            {
                throw new NumboException("group must have one R element");
            }
            else
            {
                rAtom = rGroups.ElementAt(0);
            }
            if (rAtom == null)
            {
                throw new NumboException("cannot find Rgroup: ");
            }
            IEnumerable<CmlAtom> ligands = rAtom.GetLigands();
            if (ligands.Count() != 1)
            {
                throw new NumboException("Can only join if R has ONE ligand");
            }

            // get bonds and atoms in group
            CmlBond rBond = rAtom.GetLigandBonds().ElementAt(0);
            CmlAtom otherRAtom = rBond.GetAtomAtOtherEnd(rAtom);

            // calculate geometry and transformations
            Vector2 oldVector = fixedAtom.GetVectorTo(replaceableAtom);
            Vector2 newVector = rAtom.GetVectorTo(otherRAtom);
            Angle theta = newVector.AngleTo(oldVector);
            Transform2 rotationTransform = Transform2.CreateRotationMatrixAboutPoint(theta, fixedAtom.Point2);
            groupClone.Transform2D(rotationTransform);

            Vector2 translationVector = otherRAtom.GetVectorTo(replaceableAtom);
            // and transform to overlap
            Transform2 translationTransform = Transform2.CreateTranslation(translationVector);
            groupClone.Transform2D(translationTransform);

            replaceableAtom.RemoveAtomParityOnLigands();

            // delete and clean
            replaceableAtom.DeleteAndRemoveHangingBonds();
            replaceableAtom.DeleteSimple(true);
            CmlBond newBond = new CmlBond();
            newBond.SetAtomRefs2(new[] {fixedAtom.Id, otherRAtom.Id});
            newBond.GenerateAndAddId();
            newBond.Order = Single;
            molecule.AddBond(newBond);
            rAtom.DeleteSimple(true);
            rBond.DeleteSimple(true);
            // copy groupClone
            molecule.TransferBondsAndAtomsToMolecule(groupClone);
            //            molecule.RemoveCyclicIndicators();
            Log.Debug("MOL..." + molecule.DelegateElement);
        }

        public void RemoveCyclicIndicator()
        {
            XAttribute cmlxCyclic = DelegateElement.Attribute(CmlConstants.CmlxXNamespace + CmlxCyclic);
            if (cmlxCyclic != null)
            {
                cmlxCyclic.Remove();
            }
        }

        /// <summary>
        /// hash of atomIds for comparison of bonds
        /// sorts ids and returns amm_ann
        /// </summary>
        /// <returns></returns>
        public string AtomRefs2Hash()
        {
            string a1 = GetAtomRefs2().ElementAt(0);
            string a2 = GetAtomRefs2().ElementAt(1);
            return (string.Compare(a1, a2, StringComparison.OrdinalIgnoreCase) < 0) ? a1 + "_" + a2 : a2 + "_" + a1;
        }

        /// <summary>
        /// utility to translate a list of bonds into XElements
        /// </summary>
        /// <param name="atoms">List of bonds</param>
        /// <returns>list of their DelegateElements; null id bonds is null</returns>
        public static IEnumerable<XElement> GetXElements(IEnumerable<CmlBond> bonds)
        {
            List<XElement> elements = null;
            if (bonds != null)
            {
                elements = new List<XElement>();
                foreach (CmlBond bond in bonds)
                {
                    elements.Add(bond.DelegateElement);
                }
            }
            return elements;
        }

        internal void SetType(Type type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// currently allow changes:
        /// D->S
        /// T->D
        /// S->D
        /// D->T
        /// 
        /// S, U -> H,W 
        /// 
        /// any -> U
        /// U-> S
        /// 
        /// </summary>
        /// <param name="type">type to change to</param>
        /// <returns></returns>
        internal bool CanSetType(Type targetType)
        {
            Type thisType = GetBondType();
            bool ok = false;
            if (false)
            {}
                // any -> U
            else if (targetType.Equals(Type.Unknown))
            {
                ok = true;
            }
                // S, T -> D
            else if (targetType.Equals(Type.Double))
            {
                ok = thisType.Equals(Type.Single) || thisType.Equals(Type.Triple);
            }
                // D -> S, T
            else if (thisType.Equals(Type.Double))
            {
                ok = targetType.Equals(Type.Single) || targetType.Equals(Type.Triple);
            }
                // U, S -> W, H
            else if (targetType.Equals(Type.Hatch) || targetType.Equals(Type.Wedge))
            {
                if (thisType.Equals(Type.Single) || thisType.Equals(Type.Unknown))
                {
                    ok = GetAtoms().ElementAt(0).CanBeChiral() || GetAtoms().ElementAt(1).CanBeChiral();
                }
            }
            else if (thisType.Equals(Type.Hatch) || thisType.Equals(Type.Wedge))
            {
                ok = targetType.Equals(Type.Single) || targetType.Equals(Type.Unknown);
            }
            return ok;
        }

        public Type GetBondType()
        {
            Type type = Type.Unknown;
            BondOrder bondOrder = BondOrder.GetBondOrder(this);
            if (BondOrder.Single.Equals(bondOrder))
            {
                type = Type.Single;
            }
            else if (BondOrder.Double.Equals(bondOrder))
            {
                type = Type.Double;
            }
            else if (BondOrder.Triple.Equals(bondOrder))
            {
                type = Type.Triple;
            }
            else
            {
                CmlBondStereo bondStereo = GetBondStereo();
                if (bondStereo != null)
                {
                    if (Hatch.Equals(bondStereo.GetValue()))
                    {
                        type = Type.Hatch;
                    }
                    else if (Wedge.Equals(bondStereo.GetValue()))
                    {
                        type = Type.Wedge;
                    }
                }
            }

            return type;
        }

        /// <summary>
        ///        ///  PMR 2009-08-08
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<string> SuggestPossibleBondOrders()
        {
            List<string> orderList = new List<string>();
            CmlAtom atom0 = GetAtoms().ElementAt(0);
            PeriodicTable.Element element0 = PeriodicTable.GetElement(atom0.ElementType);
            CmlAtom atom1 = GetAtoms().ElementAt(1);
            PeriodicTable.Element element1 = PeriodicTable.GetElement(atom1.ElementType);
            if (CmlMolecule.CommonElementSet.Contains(element0) &&
                CmlMolecule.CommonElementSet.Contains(element1))
            {
                int hCount0 = atom0.GetHydrogenLigands().Count();
                int hCount1 = atom1.GetHydrogenLigands().Count();
                BondOrder bondOrder = BondOrder.GetBondOrder(this);
                int numericOrder = bondOrder.IntegerBondOrder ?? 0;
                if (numericOrder > 0)
                {
                    for (int newOrder = 1; newOrder <= 3; newOrder++)
                    {
                        int newIntOrder = 0;
                        int deltaOrder = newOrder - numericOrder;
                        // can we lose hydrogens in increasing the bond order?
                        if (deltaOrder > 0 && deltaOrder <= hCount0 && deltaOrder <= hCount1)
                        {
                            newIntOrder = newOrder;
                        }
                            // add hydrogens
                        else if (deltaOrder < 0)
                        {
                            newIntOrder = newOrder;
                        }
                        if (newIntOrder > 0)
                        {
                            BondOrder bondOrderx = BondOrder.GetBondOrder(newIntOrder);
                            orderList.Add(bondOrderx.CmlBondOrder);
                        }
                    }
                }
            }
            return orderList;
        }

        /// <summary>
        ///  assumes checks have been made
        ///  PMR 2009-08-08
        /// </summary>
        /// <param name="newOrder"></param>
        public void SetBondOrderAndAddDeleteNakedHydrogens(int newOrder)
        {
            BondOrder bondOrder = BondOrder.GetBondOrder(this);
            int numericOrder = bondOrder.IntegerBondOrder ?? 0;
            if (numericOrder > 0)
            {
                CmlAtom atom0 = GetAtoms().ElementAt(0);
                CmlAtom atom1 = GetAtoms().ElementAt(1);
                int hCount0 = atom0.GetHydrogenLigands().Count();
                int hCount1 = atom1.GetHydrogenLigands().Count();
                int deltaOrder = newOrder - numericOrder;
                // can we lose hydrogens in increasing the bond order?
                if (deltaOrder > 0 && deltaOrder <= hCount0 && deltaOrder <= hCount1)
                {
                    atom0.DeleteHydrogen(deltaOrder);
                    atom1.DeleteHydrogen(deltaOrder);
                }
                    // add hydrogens
                else
                {
                    atom0.AddNakedHydrogensAndAdjustCoordinates(-deltaOrder);
                    atom1.AddNakedHydrogensAndAdjustCoordinates(-deltaOrder);
                }
                BondOrder newBondOrder = BondOrder.GetBondOrder(newOrder);
                Order = newBondOrder.CmlBondOrder;
            }
        }

        internal IEnumerable<string> SuggestPossibleStereos()
        {
            List<string> stringList = new List<string>();
            CmlAtom atom0 = GetAtoms().ElementAt(0); // sharp end
            CmlAtom atom1 = GetAtoms().ElementAt(1);
            CmlBondStereo bondStereo = GetBondStereo();
            string bondStereoS = (bondStereo == null) ? Unknown : bondStereo.GetValue();
            if (atom0.CanBeChiral())
            {
                OfferWedgeHatchUnknown(stringList, bondStereoS);
                if (atom1.CanBeChiral())
                {
                    IfNotCurrentAddStereoToList(stringList, HatchReverse, bondStereoS);
                    IfNotCurrentAddStereoToList(stringList, WedgeReverse, bondStereoS);
                }
            }
            else if (atom1.CanBeChiral())
            {
                SwapAtomsInBond();
                OfferWedgeHatchUnknown(stringList, bondStereoS);
            }
            else
            {
                ; // empty list
            }
            return stringList;
        }

        private static void OfferWedgeHatchUnknown(List<string> stringList, string bondStereoS)
        {
            IfNotCurrentAddStereoToList(stringList, Hatch, bondStereoS);
            IfNotCurrentAddStereoToList(stringList, Wedge, bondStereoS);
            IfNotCurrentAddStereoToList(stringList, Unknown, bondStereoS);
        }

        /// <summary>
        /// at present returns group abbreviation
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<string> SuggestPossibleGroups()
        {
            List<string> stringList = new List<string>();

            GroupLookup groupDictionary = GetSubstitutionDictionaryFromBondAtomsAndOrder();
            if (groupDictionary != null)
            {
                foreach (string key in groupDictionary.Keys())
                {
                    stringList.Add(key);
                }
            }
            return stringList;
        }

        private static void IfNotCurrentAddStereoToList(List<string> stringList, string bondStereoS,
                                                        string bondStereoType)
        {
            if (!bondStereoType.Equals(bondStereoS))
            {
                stringList.Add(bondStereoS);
            }
        }

        /// <summary>
        /// is this string a wedge or hatch
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool IsStereoIndicator(string value)
        {
            bool stereo = false;
            if (value != null)
            {
                if (Wedge.Equals(value) || Hatch.Equals(value))
                {
                    stereo = true;
                }
            }
            return stereo;
        }

        internal void SetStereo(string value)
        {
            // the order of calls is important and the messy
            // repeated additon of bondStereo to bond
            CmlBondStereo bondStereo = new CmlBondStereo();
            CmlUtils.AddOnlyChild(this, bondStereo, true);
            if (IsReverseSharpEnd(value))
            {
                SwapAtomsInBond();
                value = Unreverse(value);
            }
            CmlAtom sharpAtom = GetAtomAtSharpEnd();
            if (sharpAtom != null)
            {
                sharpAtom.RemoveAtomParityChildren();
                sharpAtom.RemoveBondStereoFromSharpLigandBonds();
                sharpAtom.RemoveAtomParityChildren();
            }
            CmlUtils.AddOnlyChild(this, bondStereo, true);
            Log.Debug(this);
            bondStereo.SetValue(value);
            bondStereo.AddCmlxAttribute(CmlConstants.ForcedChange, "stereo->" + value);
            bondStereo.CreateNewAtomParityForAtomAtSharpEndWithAtomRefs4InLigandOrder();
        }

        private string Unreverse(string value)
        {
            return value.Substring(0, value.Length - Reverse.Length);
        }

        private bool IsReverseSharpEnd(string value)
        {
            return value.EndsWith(Reverse);
        }

        public void ForceSetOrder(string value)
        {
            SetOrder(value);
            AddCmlxAttribute(CmlConstants.ForcedChange, "order->" + value);
        }

        /// <summary>
        /// set order
        /// looks up value in enum; if not found sets to Order.unknown
        /// </summary>
        /// <param name="value"></param>
        internal void SetOrder(string value)
        {
            BondOrder bondOrder = BondOrder.GetBondOrderFromCmlOrder(value);
            if (bondOrder == null)
            {
                Order = Unknown;
            }
            else
            {
                Order = bondOrder.CmlBondOrder;
            }
        }

        /// <summary>
        /// gets atom belonging to pointers and also in external bond 
        /// </summary>
        /// <param name="atomPointers">atoms at one end of bond</param>
        /// <returns>null if not found (probable bug)</returns>
        internal CmlAtom GetAtomInAtomPointersInBond(IEnumerable<XElement> atomPointers)
        {
            CmlAtom pivotAtom = null;
            string atomId0 = GetAtoms().ElementAt(0).Id;
            string atomId1 = GetAtoms().ElementAt(1).Id;
            foreach (XElement atomPointer in atomPointers)
            {
                CmlAtom atom = new CmlAtom(atomPointer);
                string atomId = atom.Id;
                if (atomId0.Equals(atom.Id))
                {
                    pivotAtom = atom;
                }
                else if (atomId1.Equals(atom.Id))
                {
                    pivotAtom = atom;
                }
                if (pivotAtom != null)
                {
                    break;
                }
            }
            return pivotAtom;
        }

        internal void SetSharpEndOfBondToAtom(CmlAtom atom)
        {
            CmlAtom atom0 = GetAtoms().ElementAt(0);
            CmlAtom atom1 = GetAtoms().ElementAt(1);
            if (atom0.Equals(atom))
            {
                // no-op
            }
            else if (atom1.Equals(atom))
            {
                SwapAtomsInBond(atom0, atom1);
            }
            else
            {
                throw new ArgumentException("atom is not part of bond", "atom");
            }
        }

        private void SwapAtomsInBond()
        {
            string[] atomRefs2 = GetAtomRefs2();
            SetAtomRefs2(new[] {atomRefs2[1], atomRefs2[0]});
        }

        private void SwapAtomsInBond(CmlAtom atom0, CmlAtom atom1)
        {
            SetAtomRefs2(new[] {atom1.Id, atom0.Id});
        }

        public override string ToString()
        {
            IEnumerable<CmlAtom> atoms = GetAtoms();
            return atoms.ElementAt(0).Id + "..." + atoms.ElementAt(1).Id;
        }

        internal void DeleteAllWedgeHatchBondStereoChildrenWithSharpAtom(CmlAtom atom)
        {
            if (atom.Equals(GetAtoms().ElementAt(0)))
            {
                DeleteAllWedgeHatchBondStereoChildren();
            }
        }

        internal void DeleteAllWedgeHatchBondStereoChildren() {
            var bondStereo = GetBondStereo();
            while (bondStereo != null)
            {
                if (bondStereo.IsWedgeHatch())
                {
                    CmlUtils.Remove(bondStereo);
                }
            }
        }
    }
}