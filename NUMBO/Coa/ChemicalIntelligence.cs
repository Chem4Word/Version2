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
using log4net;
using Numbo.Cml;
using Numbo.Cml.Helpers;

namespace Numbo.Coa
{
    /// <summary>
    /// Includes methods which allow the chemistry to queried
    /// </summary>
    public class ChemicalIntelligence
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (ChemicalIntelligence));

        /// <summary>
        /// private constructor to prevent instantiation
        /// </summary>
        private ChemicalIntelligence()
        {}

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to 
        /// edit or set bond order on the pointers.
        /// 
        /// Essentially if all the pointers are to bonds then yes, otherwise no.
        ///
        /// In future this could perhaps be more complex
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if SetBondOrder is possible on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanEditOrSetBondOrder(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                foreach (XElement element in pointers)
                {
                    if (CmlBond.IsBond(element))
                    {
                        ok = true;
                    }
                    else
                    {
                        ok = false;
                        break;
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to 
        /// edit or set charge on the pointers.
        /// 
        /// Essentially if there is only one pointer and it
        /// is to a molecule or atom then yes, otherwise no.
        ///
        /// In future this could perhaps take account of the atom/molecule 
        /// neighbourhood - or if it is already charged etc
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if EditOrSetCharge is possible on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanEditOrSetCharge(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                if (pointers.Count() == 1)
                {
                    XElement xElement = pointers.FirstOrDefault();
                    if (CmlMolecule.IsMolecule(xElement) || CmlAtom.IsAtom(xElement))
                    {
                        ok = true;
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to 
        /// remove charge on the pointers (molecule or atom)
        /// Only active for objects with charge set
        /// 
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if CanUnsetCharge is possible on at least one of the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanUnsetCharge(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                XElement xElement = pointers.FirstOrDefault();
                if (CmlMolecule.IsMolecule(xElement))
                {
                    if (new CmlMolecule(xElement).FormalCharge != null)
                    {
                        ok = true;
                    }
                }
                else
                {
                    foreach (XElement pointer in pointers)
                    {
                        if (CmlAtom.IsAtom(pointer))
                        {
                            if (new CmlAtom(pointer).FormalCharge != null)
                            {
                                ok = true;
                            }
                        }
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// if an carbon atom has two non-hydrogen ligands it requires graphical enhancement (dot?)
        /// to indicate the carbon
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointer"></param>
        /// <returns>true if exactly two ligands and angle within 5 degrees of 180</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool IsAtomLinearCarbon(ContextObject contextObject, XElement pointer)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer != null)
            {
                if (CmlAtom.IsAtom(pointer))
                {
                    CmlAtom atom = new CmlAtom(pointer);
                    if (PeriodicTable.Element.C.Equals(PeriodicTable.GetElement(atom.ElementType)))
                    {
                        if (atom.Has2LinearLigands(new Angle(15, Angle.Unit.Degrees)))
                        {
                            ok = true;
                        }
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to add a proton to the
        /// pointers
        /// 
        /// Essentially if there is only one pointer and it
        /// is to a proton-aware atom then yes, otherwise no.
        /// 
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if adding a proton is possible on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanAddProton(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                if (pointers.Count() == 1)
                {
                    XElement xElement = pointers.FirstOrDefault();
                    if (CmlAtom.IsAtom(xElement))
                    {
                        CmlAtom atom = new CmlAtom(xElement);
                        if (atom.CanAddProton())
                        {
                            ok = true;
                        }
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to remove a proton from the
        /// pointers
        /// 
        /// Essentially if there is only one pointer and it
        /// is to an IsProtonAdjustable atom with attached hydrogen then yes, otherwise no.
        /// 
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if IncreaseCharge is possible on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanRemoveProton(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                if (pointers.Count() == 1)
                {
                    XElement xElement = pointers.FirstOrDefault();
                    if (CmlAtom.IsAtom(xElement))
                    {
                        CmlAtom atom = new CmlAtom(xElement);
                        if (atom.CanRemoveProton())
                        {
                            ok = true;
                        }
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to remove an electron from the
        /// pointers
        /// 
        /// Essentially if there is only one pointer and it
        /// is to an atom with at least one electron then yes, otherwise no.
        /// 
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if canRemoveElectron is possible on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanRemoveElectron(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                if (pointers.Count() == 1)
                {
                    XElement xElement = pointers.FirstOrDefault();
                    if (CmlAtom.IsAtom(xElement))
                    {
                        CmlAtom atom = new CmlAtom(xElement);
                        if (atom.CanRemoveElectron())
                        {
                            ok = true;
                        }
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to add a hydrogen (H.) to the
        /// pointers
        /// 
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if adding a proton is possible on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanAddHydrogenDot(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                if (pointers.Count() == 1)
                {
                    XElement xElement = pointers.FirstOrDefault();
                    if (CmlAtom.IsAtom(xElement))
                    {
                        CmlAtom atom = new CmlAtom(xElement);
                        if (atom.CanAddHydrogenDot())
                        {
                            ok = true;
                        }
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to add an electron to the
        /// pointers
        /// 
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if adding an electron is possible on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanAddElectron(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                if (pointers.Count() == 1)
                {
                    XElement xElement = pointers.FirstOrDefault();
                    if (CmlAtom.IsAtom(xElement))
                    {
                        CmlAtom atom = new CmlAtom(xElement);
                        if (atom.CanAddElectron())
                        {
                            ok = true;
                        }
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to remove a proton from the
        /// pointers
        /// 
        /// Essentially if there is only one pointer and it
        /// is to an atom with attached hydrogen then yes, otherwise no.
        /// 
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if IncreaceCharge is possible on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanRemoveHydrogenDot(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                if (pointers.Count() == 1)
                {
                    XElement xElement = pointers.FirstOrDefault();
                    if (CmlAtom.IsAtom(xElement))
                    {
                        CmlAtom atom = new CmlAtom(xElement);
                        if (atom.CanRemoveHydrogenDot())
                        {
                            ok = true;
                        }
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to 
        /// edit or set isotope on the pointers.
        /// 
        /// Essentially if there is there is one pointer and it is to an atom
        /// 
        /// In future this could perhaps take account of the atom 
        /// neighbourhood?
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if EditOrSetIsotope is possible on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanEditOrSetIsotope(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                if (pointers.Count() == 1)
                {
                    if (CmlAtom.IsAtom(pointers.FirstOrDefault()))
                    {
                        ok = true;
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="atomPointer"></param>
        /// <returns></returns>
        public static IEnumerable<int> SuggestPossibleIsotopeNumbers(ContextObject contextObject, XElement atomPointer)
        {
            List<int> numbers = new List<int>();
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (atomPointer == null)
            {
                throw new ArgumentNullException("atomPointer");
            }
            if (CmlAtom.IsAtom(atomPointer))
            {
                CmlAtom atom = new CmlAtom(atomPointer);
                numbers = atom.SuggestPossibleIsotopeMasses();
            }
            return numbers;
        }

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to 
        /// make the current pointers wedge or hatch bonds.
        /// 
        /// Essentially if there is there is one pointer and it is to a Single bond
        /// 
        /// In future this could perhaps take account of the bond stereochemistry
        /// environment
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if EditOrSetIsotope is possible on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanEditOrSetWedgeOrHatch(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                if (pointers.Count() == 1)
                {
                    if (CmlBond.IsBond(pointers.FirstOrDefault()))
                    {
                        CmlBond bond = new CmlBond(pointers.FirstOrDefault());
                        if (BondOrder.GetBondOrder(bond).Equals(BondOrder.Single))
                        {
                            ok = true;
                        }
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to 
        /// edit or set a radical (spin multiplicity) on the pointers.
        /// 
        /// Essentially if there is only one pointer and it
        /// is to a molecule or atom then yes, otherwise no.
        ///
        /// In future this could perhaps take account of the atom/molecule 
        /// neighbourhood - or if it is already has unpaired electrons etc
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if EditOrSetSpinMultiplicty is possible on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanEditOrSetSpinMultiplicity(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                if (pointers.Count() == 1)
                {
                    XElement xElement = pointers.FirstOrDefault();
                    if (CmlMolecule.IsMolecule(xElement) || CmlAtom.IsAtom(xElement))
                    {
                        ok = true;
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Calculates whether or not it is reasonable to 
        /// add a label to the pointers.
        /// 
        /// This will be true if
        /// 1. there is only one pointer and it is to a molecule (bold number label)
        /// 2. there are one or more pointers and they are all to atoms  
        /// 3. there are one or more pointers and they are all to bonds
        ///
        /// In future this could perhaps take account of the atom/molecule 
        /// neighbourhood - or if it is already has unpaired electrons etc
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if AddLabel is possible on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanAddLabel(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }

            // V1 do we allow multiple labels? edit this as appropriate
            bool allowMultipleLabelEdits = true;
            if (pointers != null)
            {
                if (pointers.Count() == 1)
                {
                    if (CmlMolecule.IsMolecule(pointers.FirstOrDefault()) || CmlAtom.IsAtom(pointers.FirstOrDefault()) ||
                        CmlBond.IsBond((pointers.FirstOrDefault())))
                    {
                        ok = true;
                    }
                }
                else if (allowMultipleLabelEdits)
                {
                    foreach (XElement element in pointers)
                    {
                        if (CmlAtom.IsAtom(element))
                        {
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                            break;
                        }
                    }
                    if (!ok)
                    {
                        foreach (XElement element in pointers)
                        {
                            if (CmlBond.IsBond(element))
                            {
                                ok = true;
                            }
                            else
                            {
                                ok = false;
                                break;
                            }
                        }
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Calculates whether or not it is reasonable to 
        /// edit the label pointers.
        /// 
        /// Essentially if the pointers are only to labels then true
        /// otherwise no.
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if EditLabel is possible on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanEditLabel(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                foreach (XElement element in pointers)
                {
                    if (CmlLabel.IsLabel(element))
                    {
                        ok = true;
                    }
                    else
                    {
                        // not a label
                        ok = false;
                        break;
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to rotate
        /// the selection
        /// 
        /// This will be true if
        /// 1. there is only one pointer and it is to a molecule
        /// 2. there are multiple pointers and they are all to atoms  
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="pointers"></param>
        /// <returns>true if the pointer(s) can be rotated otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanRotate(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            bool ok = true;
            // BUG FIMXE
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                if (pointers.Count() == 1)
                {
                    if (!CmlMolecule.IsMolecule(pointers.FirstOrDefault()))
                    {
                        // not a molecule
                        ok = false;
                    }
                }
                else
                {
                    foreach (XElement element in pointers)
                    {
                        if (!CmlAtom.IsAtom(element))
                        {
                            // not an atom
                            ok = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                ok = false;
            }
            return ok;
        }

        /// <summary>
        /// can flip Horizontally
        /// </summary>
        /// <param name="contextObject">contextObject</param>
        /// <param name="atomPointers">atoms to flip, must forma complete molecule; 
        /// expanded to include hydrogens in case they are not selected</param>
        /// <returns>true if complete molecule</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static Vector2AndPoint2 CanFlipHorizontally(ContextObject contextObject,
                                                           IEnumerable<XElement> atomPointers)
        {
            Vector2AndPoint2 vp = null;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (atomPointers == null)
            {
                throw new ArgumentNullException("atomPointers");
            }
            if (atomPointers.Count() > 0)
            {
                if (CmlUtils.DoPointersFormImmediateMolecule(atomPointers))
                {
                    Vector2 v = new Vector2(1.0, 0.0);
                    vp = CreateVectorPoint(contextObject, atomPointers, vp, v);
                }
            }
            return vp;
        }

        /// <summary>
        /// can flip Vertically
        /// </summary>
        /// <param name="contextObject">contextObject</param>
        /// <param name="atomPointers">atoms to flip, must form a complete molecule; 
        /// expanded to include hydrogens in case they are not selected</param>
        /// <returns>true if complete molecule</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static Vector2AndPoint2 CanFlipVertically(ContextObject contextObject,
                                                         IEnumerable<XElement> atomPointers)
        {
            Vector2AndPoint2 vp = null;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (atomPointers == null)
            {
                throw new ArgumentNullException("atomPointers");
            }
            if (atomPointers.Count() > 0)
            {
                if (CmlUtils.DoPointersFormImmediateMolecule(atomPointers))
                {
                    Vector2 v = new Vector2(0.0, 1.0);
                    vp = CreateVectorPoint(contextObject, atomPointers, vp, v);
                }
            }
            return vp;
        }

        private static Vector2AndPoint2 CreateVectorPoint(ContextObject contextObject,
                                                          IEnumerable<XElement> atomPointers, Vector2AndPoint2 vp,
                                                          Vector2 v)
        {
            CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atomPointers.ElementAt(0));
            IEnumerable<XElement> expandedAtoms = CmlAtom.GetAtomPointers(molecule.GetAtoms());
            Point2 centroid = GetMoleculeFlipPoint(contextObject, expandedAtoms);
            vp = new Vector2AndPoint2(v, centroid);
            return vp;
        }

        /// <summary>
        /// searches for single bond about which the atomPointers can be flipped
        /// This will be true if there are multiple atomPointers (>=2) and they 
        /// form a connected network with only a single external bond extending out
        /// </summary>
        /// <param name="contextObject">contextObject</param>
        /// <param name="atomPointers">atoms to flip (>=2)</param>
        /// <returns>is there a bond about which the atoms can be flipped</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static Vector2AndPoint2 CanFlipAboutExternalAcyclicBond(ContextObject contextObject,
                                                                       IEnumerable<XElement> atomPointers)
        {
            Vector2AndPoint2 vp = null;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (atomPointers == null)
            {
                throw new ArgumentNullException("atomPointers");
            }
            CmlBond bond = GetFlippableBond(contextObject, atomPointers);
            if (bond != null)
            {
                CmlAtom atom = bond.GetAtoms().ElementAt(0);
                Vector2 v2 = bond.GetVector2(atom);
                vp = new Vector2AndPoint2(v2, atom.Point2);
            }
            return vp;
        }

        /// <summary>
        /// gets single acyclic bond about which the atomPointers can be flipped
        /// This will be true if there are multiple pointers and they 
        /// form a connected network with only a single edge extending out
        /// </summary>
        /// <param name="contextObject">contextObject</param>
        /// <param name="atomPointers">atoms to flip</param>
        /// <returns>bond pointer about which to flip; null if impossible</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static CmlBond GetFlippableBond(ContextObject contextObject, IEnumerable<XElement> atomPointers)
        {
            CmlBond flipBond = null;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (atomPointers == null)
            {
                throw new ArgumentNullException("atomPointers");
            }
            if (atomPointers.Count() > 1)
            {
                IEnumerable<CmlAtom> atoms = CmlAtom.GetAtoms(atomPointers);
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atoms.ElementAt(0));
                if (molecule != null)
                {
                    flipBond = molecule.GetSingleExternalAcyclicBond(atoms);
                }
                else
                {
                    Log.Debug("Null molecule in flip: ");
                }
            }
            return flipBond;
        }

        /// <summary>
        /// Calculates whether or not it is chemically reasonable to decrease 
        /// the charge by unity on the pointers.
        /// 
        /// Essentially if there is only one pointer and it
        /// is to a molecule or atom then yes, otherwise no.
        ///
        /// In future this could perhaps take account of the atom/molecule 
        /// neighbourhood - or if it is already charged etc
        /// </summary>
        ///<param name="contextObject"></param>
        ///<param name="pointers"></param>
        ///<returns>true if CID.ChangeElementType can be called on the pointers otherwise false</returns>
        /// <exception cref="ArgumentNullException">if contextObject is null</exception>
        public static bool CanChangeElementType(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            bool ok = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers != null)
            {
                foreach (XElement pointer in pointers)
                {
                    if (CmlAtom.IsAtom(pointer))
                    {
                        ok = true;
                    }
                    else
                    {
                        ok = false;
                        break;
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Suggests groups which might replace monovalent atoms/groups
        /// For example a -Cl or -CH3 could be substituted by -Ph or -NO2
        /// The strings are abbreviations which could be displayed. To retrieve the group use 
        /// CMLMolecule.GetGroupDictionary().TryGetValue(groupAbbreviation, out groupMolecule);
        /// </summary>
        /// <returns>ordered set of group abbreviations</returns>
        public static IEnumerable<string> SuggestPossibleGroups(ContextObject contextObject, XElement atomPointer)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (atomPointer == null)
            {
                throw new ArgumentNullException("atomPointer");
            }
            List<string> groupList = new List<string>();
            CmlAtom atom = new CmlAtom(atomPointer);

            GroupLookup dictionary = atom.GetSubstitutionDictionary();
            if (dictionary != null)
            {
                foreach (string s in dictionary.Keys())
                {
                    groupList.Add(s);
                }
                groupList.Sort();
            }
            CanModifyElectronProtonAndHDot(groupList, atom);
            return groupList;
        }

        private static void CanModifyElectronProtonAndHDot(ICollection<string> groupList, CmlAtom atom)
        {
            if (atom.CanAddElectron())
            {
                groupList.Add(CmlConstants.AddElectron);
            }
            if (atom.CanRemoveElectron())
            {
                groupList.Add(CmlConstants.RemoveElectron);
            }
            if (atom.CanAddProton())
            {
                groupList.Add(CmlConstants.AddProton);
            }
            if (atom.CanRemoveProton())
            {
                groupList.Add(CmlConstants.RemoveProton);
            }
            if (atom.CanAddHydrogenDot())
            {
                groupList.Add(CmlConstants.AddHdot);
            }
            if (atom.CanRemoveHydrogenDot())
            {
                groupList.Add(CmlConstants.RemoveHdot);
            }
        }

        /// <summary>
        /// Suggests elements (in future groups too) which it makes chemical sense to 
        /// change the element (pointer) to. A typical example would be carbon to 
        /// a nitrogen or the carbon in a methyl group to any single valent element 
        /// such as F or Cl. 
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointer">the delegate element of the CMLAtom</param>
        /// <returns></returns>
        public static HashSet<PeriodicTable.Element> SuggestPossibleElements(ContextObject contextObject,
                                                                             XElement pointer)
        {
            HashSet<PeriodicTable.Element> replacementSet = new HashSet<PeriodicTable.Element>();
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }
            if (CmlAtom.IsAtom(pointer))
            {
                CmlAtom atom = new CmlAtom(pointer);
                PeriodicTable.Element currentAtomElement = PeriodicTable.GetElement(atom.ElementType);
                if (CmlMolecule.CommonElementSet.Contains(currentAtomElement))
                {
                    foreach (PeriodicTable.Element commonElement in CmlMolecule.CommonElementSet)
                    {
                        if (atom.HashEnoughHydrogensToDeleteOnGroupChange(commonElement))
                        {
                            replacementSet.Add(commonElement);
                        }
                    }
                    replacementSet.Add(PeriodicTable.Element.R);
                }
                    // hydrogen
                else if (PeriodicTable.Element.H.Equals(currentAtomElement))
                {
                    foreach (PeriodicTable.Element commonElement in CmlMolecule.CommonElementSet)
                    {
                        replacementSet.Add(commonElement);
                    }
                    foreach (PeriodicTable.Element nextCommonElement in CmlMolecule.NextCommonElementSet)
                    {
                        replacementSet.Add(nextCommonElement);
                    }
                    replacementSet.Add(PeriodicTable.Element.R);
                }
                    // R
                else if (PeriodicTable.Element.R.Equals(currentAtomElement))
                {
                    foreach (PeriodicTable.Element commonElement in CmlMolecule.CommonElementSet)
                    {
                        replacementSet.Add(commonElement);
                    }
                    foreach (PeriodicTable.Element nextCommonElement in CmlMolecule.NextCommonElementSet)
                    {
                        replacementSet.Add(nextCommonElement);
                    }
                }
                    // B and second row
                else if (CmlMolecule.NextCommonElementSet.Contains(currentAtomElement))
                {
                    foreach (PeriodicTable.Element commonElement in CmlMolecule.CommonElementSet)
                    {
                        replacementSet.Add(commonElement);
                    }
                    foreach (PeriodicTable.Element nextCommonElement in CmlMolecule.NextCommonElementSet)
                    {
                        replacementSet.Add(nextCommonElement);
                    }
                    replacementSet.Add(PeriodicTable.Element.R);
                }
                // don't change to identical
                replacementSet.Remove(currentAtomElement);
            }
            return replacementSet;
        }

        /// <summary>
        /// Calculates and returns (in co-ordinate space) the position about which 
        /// rotation of the selection should occur.
        /// 
        /// If the selection is a terminal group (ie one which only has a single bond (of any order)
        /// extending out of the selection then the point of rotation is the co-ordinates of the atom 
        /// in the selection which the extruding bond is to.
        /// 
        /// Otherwise the rotation point is the centre of the rectangular box bounding the 
        /// selected atoms.
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="selectedAtomPointers">pointers to the atoms in the selection</param>
        /// <returns>The co-ordinates (in molecule space) of the point to rotate about</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static Point2 GetMoleculeFlipPoint(ContextObject contextObject,
                                                  IEnumerable<XElement> selectedAtomPointers)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (selectedAtomPointers == null)
            {
                throw new ArgumentNullException("selectedAtomPointers");
            }
            var atoms = new HashSet<CmlAtom>();
            foreach (XElement atomPointer in selectedAtomPointers)
            {
                CmlAtom atom = new CmlAtom(atomPointer);
                /// FIXME - this must also add the non stereo specific hydrogen ligans connected to this atom
                atoms.Add(atom);
            }
            CmlAtom cpoint = null;
            bool mayHavePoint = true;
            foreach (CmlAtom a in atoms)
            {
                List<CmlAtom> neighbours = a.GetLigands().ToList();
                neighbours.RemoveAll(o => o.ElementType == PeriodicTable.Element.H.ToString());
                neighbours.RemoveAll(atoms.Contains);

                if (mayHavePoint && neighbours.Count == 1)
                {
                    if (cpoint == null)
                    {
                        cpoint = a;
                    }
                    else
                    {
                        cpoint = null;
                        mayHavePoint = false;
                    }
                }
            }
            Point2 point;
            if (cpoint == null)
            {
                double xmax = Double.NegativeInfinity;
                double ymax = Double.NegativeInfinity;
                double xmin = Double.PositiveInfinity;
                double ymin = Double.PositiveInfinity;
                foreach (CmlAtom atom in atoms)
                {
                    Point2 p = atom.Point2;
                    xmax = Math.Max(xmax, p.X);
                    xmin = Math.Min(xmin, p.X);
                    ymax = Math.Max(ymax, p.Y);
                    ymin = Math.Min(ymin, p.Y);
                }
                double width = xmax - xmin;
                double height = ymax - ymin;

                point = new Point2(xmin + width/2, ymin + height/2);
            }
            else
            {
                point = cpoint.Point2;
            }
            return point;
        }

        /// <summary>
        /// Calculates and returns (in co-ordinate space) the position about which 
        /// rotation of the selection should occur.
        /// 
        /// If the selection is a terminal group (ie one which only has a single bond (of any order)
        /// extending out of the selection then the point of rotation is the co-ordinates of the atom 
        /// in the selection which the extruding bond is to.
        /// 
        /// Otherwise the rotation point is the centre of the rectangular box bounding the 
        /// selected atoms.
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="selectedAtomPointers">pointers to the atoms in the selection</param>
        /// <returns>The co-ordinates (in molecule space) of the point to rotate about</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static Point2 GetRotationPoint(ContextObject contextObject,
                                              IEnumerable<XElement> selectedAtomPointers)
        {
            Point2 point = null;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (selectedAtomPointers == null)
            {
                throw new ArgumentNullException("selectedAtomPointers");
            }
            if (selectedAtomPointers.Count() > 1)
            {
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(selectedAtomPointers.ElementAt(0));
                CmlAtom atom = molecule.GetEndOfSingleExternalAcyclicBond(selectedAtomPointers);
                // single acyclic bond?
                if (atom != null)
                {
                    point = atom.Point2;
                }
                else
                {
                    // no; get centroid of bounding box
                    IEnumerable<CmlAtom> atoms = CmlAtom.GetAtoms(selectedAtomPointers);
                    IEnumerable<Point2> points = CmlUtils.GetCoordinates2D(atoms);
                    IEnumerable<Point2> corners = Point2.Get4BoundingPoints(points);
                    if (corners != null)
                    {
                        point = Point2.GetCentroid(corners);
                    }
                }
            }
            return point;
        }

        /// <summary>
        /// Checks the semantics of the chemistry below and including the pointer.
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointer">a pointer to the eldest element which should be validated</param>
        /// <returns>true, if there are any chemical rules warnings</returns>
        public static bool AnnotateViolationsOfCurrentChemicalRules(ContextObject contextObject, XElement pointer)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }
            bool check = false;
            if (CmlMolecule.IsMolecule(pointer))
            {
                CmlMolecule molecule = new CmlMolecule(pointer);
                check = molecule.AnnotateViolationsOfCurrentChemicalRules();
            }
            return check;
        }

        /// <summary>
        /// Get a list of the atoms which are downstream from the current bond starting with atomPointer 
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="bondPointer"></param>
        /// <param name="atomPointer"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetDownstreamAtomIds(ContextObject contextObject, XElement bondPointer,
                                                               XElement atomPointer)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (bondPointer == null)
            {
                throw new ArgumentNullException("bondPointer");
            }
            if (atomPointer == null)
            {
                throw new ArgumentNullException("atomPointer");
            }
            var bond = new CmlBond(bondPointer);
            var atom = new CmlAtom(atomPointer);
            return bond.GetDownstreamAtomIds(atom.Id);
        }

        /// Checks the uniqness of the bold number(s) within scope.
        /// </summary>
        /// <param name="contextObject">The ContextObject containing the pointer(s)</param>
        /// <param name="pointers">The element(s) containing the current bold number(s) to check</param>
        /// <param name="otherContextObjects">The other context objects which are in scope</param>
        /// <returns>true if unique false otherwise</returns>
        public static bool IsBoldNumberUnique(ContextObject contextObject, IEnumerable<XElement> pointers,
                                              IEnumerable<ContextObject> otherContextObjects)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers == null)
            {
                throw new ArgumentNullException("pointers");
            }
            if (otherContextObjects == null)
            {
                throw new ArgumentNullException("otherContextObjects");
            }
            int count = otherContextObjects.Count();
            if (count == 0)
            {
                return true;
            }
            HashSet<string> boldNumbers = new HashSet<string>();
            foreach (XElement pointer in pointers)
            {
                DepictionOption depictionOption = new DepictionOption(pointer);
                if (Depiction.IsMoleculeBoldNumberLabel(depictionOption))
                {
                    CmlLabel label = new CmlLabel(pointer);
                    boldNumbers.Add(label.Value);
                }
                // otherwise not pointing to a bold number label
            }
            foreach (ContextObject otherContextObject in otherContextObjects)
            {
                IEnumerable<DepictionOption> depictionOptions = Depiction.PossibleDepictionOptions(otherContextObject);
                foreach (DepictionOption option in depictionOptions)
                {
                    if (Depiction.IsMoleculeBoldNumberLabel(option))
                    {
                        CmlLabel label = new CmlLabel((XElement) option.MachineUnderstandableOption);
                        if (!boldNumbers.Add(label.Value))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Can we reset the IsotopeNumber on the atoms to the default
        /// </summary>
        /// <param name="contextObject">The ContextObject containing the pointer(s)</param>
        /// <param name="pointers">The atom(s) whose isotopeNumbers are to be reset. Atoms without 
        /// isotopeNumbers are included as resettting them is idemPotent</param>
        /// <returns>true if resettable</returns>
        public static bool CanUnsetIsotopeNumber(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers == null)
            {
                throw new ArgumentNullException("pointers");
            }
            bool canEdit = false;
            if (pointers.Count() > 0)
            {
                canEdit = true;
                foreach (XElement pointer in pointers)
                {
                    if (!CmlAtom.IsAtom(pointer))
                    {
                        canEdit = false;
                        break;
                    }
                }
            }
            return canEdit;
        }

        /// <summary>
        /// Can we remove the child labels on atoms/bonds/molecules
        /// </summary>
        /// <param name="contextObject">The ContextObject containing the pointer(s)</param>
        /// <param name="pointers">The atom(s) bond(s) or molecule whose labels are to be removed.
        ///  Atoms / bonds / molceuls without  labels are not included</param>
        /// <returns>true if resettable</returns>
        public static bool CanRemoveLabels(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers == null)
            {
                throw new ArgumentNullException("pointers");
            }
            bool canRemove = false;
            if (pointers.Count() > 0)
            {
                canRemove = false;
                foreach (XElement pointer in pointers)
                {
                    if (CmlAtom.IsAtom(pointer) || CmlBond.IsBond(pointer) || CmlMolecule.IsMolecule(pointer))
                    {
                        if (CmlUtils.GetChildLabels(pointer).Count() > 0)
                        {
                            canRemove = true;
                            break;
                        }
                    }
                }
            }
            return canRemove;
        }

        /// <summary>
        /// Checks the semantics of the chemistry below and including the pointer
        /// and marks (cmlx:warning="isotopeNumber") for example.
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointer">a pointer to the eldest element which should be validated</param>
        /// <returns>the context object annotated with possible chemical rule violations</returns>
        public static bool AreThereWarningsOfCurrentChemicalRules(ContextObject contextObject, XElement pointer)
        {
            bool ok = true;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)

            {
                throw new ArgumentNullException("pointer");
            }
            IEnumerable<XElement> descendantsAndSelf = pointer.DescendantsAndSelf();
            foreach (XElement element in descendantsAndSelf)
            {
                IEnumerable<XAttribute> attributes =
                    element.Attributes(CmlConstants.CmlxXNamespace + CmlConstants.CmlxWarning);
                int count = attributes.Count();
                if (count != 0)
                {
                    ok = false;
                    break;
                }
            }
            return ok;
        }

        /// <summary>
        /// this will return a list of all bond order changes possible on a bond
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> SuggestPossibleBondOrders(ContextObject contextObject,
                                                                    IEnumerable<XElement> bondPointers)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (bondPointers == null)
            {
                throw new ArgumentNullException("bondPointers");
            }
            IEnumerable<string> optionList = new List<string>();
            if (bondPointers.Count() == 1)
            {
                if (CmlBond.IsBond(bondPointers.ElementAt(0)))
                {
                    CmlBond bond = new CmlBond(bondPointers.ElementAt(0));
                    optionList = bond.SuggestPossibleBondOrders();
                }
            }
            return optionList;
        }

        /// <summary>
        /// this will return a list of all stereos possible on a bond
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> SuggestPossibleBondStereos(ContextObject contextObject,
                                                                     IEnumerable<XElement> bondPointers)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (bondPointers == null)
            {
                throw new ArgumentNullException("bondPointers");
            }
            IEnumerable<string> optionList = new List<string>();
            if (bondPointers.Count() == 1)
            {
                if (CmlBond.IsBond(bondPointers.ElementAt(0)))
                {
                    CmlBond bond = new CmlBond(bondPointers.ElementAt(0));
                    optionList = bond.SuggestPossibleStereos();
                }
            }
            return optionList;
        }

        /// <summary>
        /// this will return a list of all operations possible on a bond
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> SuggestPossibleBondGroups(ContextObject contextObject,
                                                                    IEnumerable<XElement> bondPointers)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (bondPointers == null)
            {
                throw new ArgumentNullException("bondPointers");
            }
            IEnumerable<string> optionList = new List<string>();
            if (bondPointers.Count() == 1)
            {
                if (CmlBond.IsBond(bondPointers.ElementAt(0)))
                {
                    CmlBond bond = new CmlBond(bondPointers.ElementAt(0));
                    optionList = bond.SuggestPossibleGroups();
                }
            }
            return optionList;
        }

        /// <summary>
        /// is atom visible on screen?
        /// </summary>
        /// <param name="contextObject">The ContextObject containing the pointer</param>
        /// <param name="atomPointer">the atom to interrogate</param>
        /// <returns>true if atom should be shown on screen</returns>
        public static bool ShouldAtomBeDrawnInTwoD(ContextObject contextObject, XElement atomPointer)
        {
            bool draw = false;
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (atomPointer == null)
            {
                throw new ArgumentNullException("atomPointer");
            }
            if (!CmlAtom.IsAtom(atomPointer))
            {
                throw new ArgumentException("atomPointer is not pointing to an atom", "atomPointer");
            }
            CmlAtom atom = new CmlAtom(atomPointer);

            switch (atom.ElementType)
            {
                case "H":
                    // only draw hydrogen atoms if they are 
                    // 1. are involved in 0 or more than one bond

                    // 1. bonded to non-carbon atoms
                    // 3. or are C-H in Wedge/Hatch bonds
                    // 4. is not bonded to anything else
                    // 5. is involved in an unusual (ie unknown type) bond
                    // 6. is involved in a molecule which only contains CH3, CH2 or CH 
                    ICollection<CmlBond> bonds = atom.GetLigandBonds();
                    if (bonds.Count() == 0 || bonds.Count() > 1)
                    {
                        draw = true;
                    }
                    else
                    {
                        // only one bond - if the bond type is unknown then we must draw the H
                        CmlBond bond = bonds.ElementAt(0);
                        if (!CmlBond.Single.Equals(bond.Order))
                        {
                            draw = true;
                        }
                        else
                        {
                            // only one bond so check what is at the other end
                            CmlAtom atomAtOtherEnd = bond.GetAtomAtOtherEnd(atom);
                            switch (atomAtOtherEnd.ElementType)
                            {
                                case "C":
                                    // only show if bondStereo is W/H or if only H's connected to the carbon
                                    CmlBondStereo bondStereo = bond.GetBondStereo();
                                    if (bondStereo == null)
                                    {
                                        CmlAtom[] ligands = atomAtOtherEnd.GetLigands().ToArray();
                                        if (ligands.AreAllH())
                                        {
                                            draw = true;
                                        }
                                        break;
                                    }
                                    // this  should actually use the bondStereo.Concise value too to ensure 
                                    // that the W/H is being propertly interpretted
                                    switch (bondStereo.DelegateElement.Value)
                                    {
                                        case CmlBond.Wedge:
                                            draw = true;
                                            break;
                                        case CmlBond.Hatch:
                                            draw = true;
                                            break;
                                        default:
                                            draw = true;
                                            break;
                                    }
                                    break;

                                default:
                                    // atom at other end is not C
                                    draw = true;
                                    break;
                            }
                        }
                    }
                    break;
                default:
                    // non-hydrogen 
                    draw = true;
                    break;
            }
            return draw;
        }
    }
}