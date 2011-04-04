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
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using Euclid;
using log4net;
using Numbo.Cml;
using Numbo.Cml.Helpers;

[assembly: InternalsVisibleTo("COATest")]

namespace Numbo.Coa
{
    /// <summary>
    /// Chemistry Interface Definition
    /// Includes methods which allow modification of chemistry - whether it be 
    /// geometry or adding atoms / bonds
    /// </summary>
    public class Cid
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (Cid));

        /// <summary>
        /// private constructor to ensure that the CID class cannot be instantiated
        /// </summary>
        private Cid()
        {
            // private constructor to ensure that the CID class cannot be instantiated
        }

        /// <summary>
        /// force substitution of elementType
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="pointers"></param>
        /// <param name="elementType"></param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        /// <exception cref="ArgumentException">bad element</exception>
        /// <exception cref="ArgumentException">pointer is not atom</exception>
        public static ContextObject ChangeElementType(ContextObject contextObject, IEnumerable<XElement> pointers,
                                                      string elementType)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers == null)
            {
                throw new ArgumentNullException("pointers");
            }
            if (elementType == null)
            {
                throw new ArgumentNullException("elementType");
            }
            PeriodicTable.Element el;
            if (!PeriodicTable.TryParseElement(elementType, out el))
            {
                throw new ArgumentException("invalid element: " + elementType, "elementType");
            }
            HashSet<CmlMolecule> molecules = new HashSet<CmlMolecule>();
            foreach (XElement pointer in pointers)
            {
                CmlAtom atom = new CmlAtom(pointer);
                atom.ForceSubstituteElement(el);
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atom);
                molecules.Add(molecule);
                CidLogger record = new CidLogger("ChangeElementType");
                record.Add("pointers", pointers);
                record.Add("elementType", elementType);
                record.AddTo(molecule);
                Log.Debug(molecule.DelegateElement.ToString());
                AnnotateOutdated(pointer);
            }
            foreach (CmlMolecule molecule in molecules)
            {
                molecule.CheckAndFixStereochemistry();
                molecule.NormalizeConciseFormulas();
                AnnotateOutdated(molecule.DelegateElement);
            }
            return contextObject;
        }

        /// <summary>
        /// given a string which has been asserted to be chemistry this creates the 
        /// appropriate CML and new context object 
        /// TODO improve this
        /// </summary>
        /// <param name="chemistry">the text which is to be chemistry</param>
        /// <returns>a new context object containing the chemistry</returns>
        public static ContextObject CreateChemistryFromUnknownOrigin(string chemistry)
        {
            CmlCml cml = new CmlCml();
            CmlUtils.AddNamespaceDeclaration(cml, CmlConstants.NameDictPrefix, CmlConstants.NameDictNS);
            CmlName name = new CmlName();
            CmlUtils.Add(cml, name);
            name.DictRef = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", CmlConstants.NameDictPrefix, "unknown");
            name.SetValue(chemistry);
            XDocument xDocument = new XDocument();
            xDocument.Add(cml.DelegateElement);
            ContextObject contextObject = new ContextObject(xDocument);
            return contextObject;
        }

        /// <summary>
        /// TODO FIXME
        /// assume the pointers are to atoms
        /// TODO PMR
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="pointers"></param>
        /// <param name="elementType">the elementType (must correspond to PeriodicTable)</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static ContextObject ChangeElementTypeAndFixChemistry(
            ContextObject contextObject, IEnumerable<XElement> pointers,
            string elementType)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (elementType == null)
            {
                throw new ArgumentNullException("elementType");
            }
            if (ChemicalIntelligence.CanChangeElementType(contextObject, pointers))
            {
                foreach (XElement pointer in pointers)
                {
                    CmlAtom atom = new CmlAtom(pointer);
                    atom.ChangeElementTypeAndFixHydrogensForCommonElements(elementType);
                }
                CidLogger record = new CidLogger("ChangeElementTypeAndFixChemistry");
                record.Add("pointers", pointers);
                record.Add("elementType", elementType);
                CmlMolecule molecule = CmlUtils.GetFirstCommonAncestorMolecule(pointers);
                record.AddTo(molecule);
                Log.Debug(molecule);
                CheckAndFixStereochemistry(pointers);
                molecule.NormalizeConciseFormulas();
                AnnotateOutdated(molecule.DelegateElement);
            }
            return contextObject;
        }

        /// <summary>
        /// Deletes the selected atoms (and all bonds to them) then fixes the valencies by adding hydrogen
        /// If a carbon atom is deleted then so are all (non bridging) attached hydrogen atoms.
        /// </summary>
        /// <param name="contextObject">the contextObject containing the atoms</param>
        /// <param name="atomPointers">the pointers to the atoms to remove</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static ContextObject DeleteAtomsAndFixChemistry(ContextObject contextObject,
                                                               IEnumerable<XElement> atomPointers)
        {
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
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atomPointers.ElementAt(0));
                molecule.DeleteAtomsAndCapWithHydrogenForCommonElements(atomPointers);
                CidLogger record = new CidLogger("DeleteAtomsAndFixChemistry");
                record.Add("atomPointers", atomPointers);
                record.AddTo(molecule);
                molecule.CheckAndFixStereochemistry();
                molecule.NormalizeConciseFormulas();
                AnnotateOutdated(molecule.DelegateElement);
            }
            return contextObject;
        }

        /// <summary>
        /// Deletes the selected bonds then fixes the valencies of those atoms which have
        /// lost a bond by adding hydrogen
        /// </summary>
        /// <param name="contextObject">the contextObject containing the atoms</param>
        /// <param name="bondPointers">the pointers to the bonds to remove</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static ContextObject DeleteBondsAndFixChemistry(ContextObject contextObject,
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
            if (bondPointers.Count() > 0)
            {
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(bondPointers.ElementAt(0));
                molecule.SplitBondsAndFixChemistry(bondPointers, contextObject.StubScaleFactor);
                CidLogger record = new CidLogger("DeleteAtomsAndFixChemistry");
                record.Add("bondPointers", bondPointers);
                record.AddTo(molecule);
                molecule.CheckAndFixStereochemistry();
                molecule.NormalizeConciseFormulas();
                AnnotateOutdated(molecule.DelegateElement);
            }
            return contextObject;
        }

        public static ContextObject RemoveMoleculeBoldNumberLabels(ContextObject contextObject, XElement pointer)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }
            IEnumerable<DepictionOption> depictionOptions = Depiction.PossibleDepictionOptions(contextObject, pointer);
            foreach (DepictionOption depictionOption in depictionOptions)
            {
                if (Depiction.IsMoleculeBoldNumberLabel(depictionOption))
                {
                    CmlLabel label = new CmlLabel((XElement) depictionOption.MachineUnderstandableOption);
                    CmlUtils.Remove(label);
                }
            }
            return contextObject;
        }

        /// <summary>
        /// NormalizeOnImport the CMLLite below (and including) the pointer
        /// Called from import pipeline
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointer">the eldest element to normalise</param>
        /// <returns>the ContextObject</returns>
        public static ContextObject NormalizeOnImport(ContextObject contextObject, XElement pointer)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }
            IEnumerable<CmlMolecule> molecules = CmlUtils.GetAllToplevelDescendentMolecules(pointer);
            ContextObject co = contextObject;
            foreach (CmlMolecule molecule in molecules)
            {
                molecule.CalculateAndSetCyclicIndicators();
                molecule.NormaliseBondOrdersOnImport();
                molecule.RecursivelyNormaliseCharge();
                CidLogger record = new CidLogger("NormalizeOnImport");
                record.AddTo(molecule);
                molecule.RecursivelyNormaliseSpinMultiplity();
                molecule.CheckAndFixStereochemistryOnImport();
            }
            co = NormalizeConciseFormulaOnImport(co, pointer);
            return co;
        }

        /// <summary>
        /// Checks and then sets the value of a bold number label
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointer">pointer to the bold number label to set</param>
        /// <param name="value">the value of the bold number label to set</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        /// <exception cref="ArgumentException">if the value is not appropriate</exception>
        public static ContextObject EditMoleculeBoldNumberLabelValue(ContextObject contextObject, XElement pointer,
                                                                     string value)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            CmlLabel label = new CmlLabel(pointer);
            if (!CmlLabel.IsLabelValueValid(value))
            {
                throw new ArgumentException("the value of a label must not contain whitespace", value);
            }
            label.Value = value;
            return contextObject;
        }

        /// <summary>
        /// Creates a new bold number label as a child of the pointer 
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointer">pointer to the parent of the bold number label</param>
        /// <param name="value">the value of the bold number label to set</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        /// <exception cref="ArgumentException">if the pointer or value is not appropriate</exception>
        public static ContextObject AddMoleculeBoldNumberLabel(ContextObject contextObject, XElement pointer,
                                                               string value)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            CmlLabel label = new CmlLabel();

            XNamespace xns = CmlConstants.CmlDictNS;
            string prefix = pointer.GetPrefixOfNamespace(xns);
            if (prefix == null)
            {
                label.DelegateElement.Add(new XAttribute(xns + CmlConstants.CmlDictPrefix, xns));
            }
            label.DictRef = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", CmlConstants.CmlDictPrefix,
                                          CmlLabel.MoleculeBoldNumber);
            if (!CmlLabel.IsLabelValueValid(value))
            {
                throw new ArgumentException("the value of a label must not contain whitespace", value);
            }
            label.Value = value;
            // TODO check if label can be child of pointer
            pointer.Add(label.DelegateElement);
            return contextObject;
        }

        /// <summary>
        /// Checks and then sets the value of a label
        /// not (necessarily) a bold number label
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointer">pointer to the label to set</param>
        /// <param name="value">the value of the label to set</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        /// <exception cref="ArgumentException">if the value is not appropriate</exception>
        public static ContextObject EditOrSetLabelValue(ContextObject contextObject, XElement pointer, string value)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            CmlLabel label = new CmlLabel(pointer);
            if (!CmlLabel.IsLabelValueValid(value))
            {
                throw new ArgumentException("the value of a label must not contain whitespace", value);
            }
            label.Value = value;
            return contextObject;
        }

        /// <summary>
        /// Creates a new label as a child of the pointer - this will overwrite any existing label
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointers">pointers to the parents of the label</param>
        /// <param name="value">the value of the label to set</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        /// <exception cref="ArgumentException">if the pointer or value is not appropriate</exception>
        public static ContextObject AddLabel(ContextObject contextObject, IEnumerable<XElement> pointers,
                                             string value)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers == null)
            {
                throw new ArgumentNullException("pointers");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            foreach (XElement pointer in pointers)
            {
                CmlElement element = null;
                if (CmlMolecule.IsMolecule(pointer))
                {
                    element = new CmlMolecule(pointer);
                }
                else if (CmlAtom.IsAtom(pointer))
                {
                    element = new CmlAtom(pointer);
                }
                else if (CmlBond.IsBond(pointer))
                {
                    element = new CmlBond(pointer);
                }
                if (element != null)
                {
                    CmlLabel label = new CmlLabel();
                    XNamespace xns = CmlConstants.CmlDictNS;
                    string prefix = pointer.GetPrefixOfNamespace(xns);
                    if (prefix == null)
                    {
                        label.DelegateElement.Add(new XAttribute(xns + CmlConstants.CmlDictPrefix, xns));
                    }
                    label.DictRef = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", CmlConstants.CmlDictPrefix,
                                                  "unknown");
                    if (!CmlLabel.IsLabelValueValid(value))
                    {
                        throw new ArgumentException("the value of a label must not contain whitespace", value);
                    }
                    label.Value = value;
                    CmlUtils.AddOnlyChild(element, label, true);
                }
            }
            return contextObject;
        }

        /// <summary>
        /// Removes any label child of the pointer
        /// currently only works for atoms
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointers">pointers to the parents of the label</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        /// <exception cref="ArgumentException">if the pointer or value is not appropriate</exception>
        public static ContextObject RemoveLabel(ContextObject contextObject, IEnumerable<XElement> pointers)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers == null)
            {
                throw new ArgumentNullException("pointers");
            }

            foreach (XElement pointer in pointers)
            {
                // TODO check logic with Joe
                CmlElement element = null;
                if (CmlMolecule.IsMolecule(pointer))
                {
                    // molecule cannot be selected
//                    element = new CMLMolecule(pointer);
                }
                else if (CmlAtom.IsAtom(pointer))
                {
                    element = new CmlAtom(pointer);
                }
                else if (CmlBond.IsBond(pointer))
                {
                    // bonds do not have labels
                    element = new CmlBond(pointer);
                }
                if (element != null)
                {
                    IEnumerable<XElement> labelChilds =
                        element.DelegateElement.Elements(CmlConstants.CmlxNamespace + CmlLabel.Tag);
                    foreach (XElement labelChild in labelChilds)
                    {
                        // use CMLUtils incase we need to trap any action
                        CmlUtils.Remove(new CmlLabel(labelChild));
                    }
                }
            }
            return contextObject;
        }

        /// <summary>
        /// Sets the value of the inline formula
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="formulaElementPointer">pointer to the formula element on which the inline formula will be set or edited</param>
        /// <param name="value">the value of the inline formula to set</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        /// <exception cref="ArgumentException">if the pointer or value is not appropriate</exception>
        public static ContextObject EditOrSetInlineFormulaValue(ContextObject contextObject,
                                                                XElement formulaElementPointer, string value)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (formulaElementPointer == null)
            {
                throw new ArgumentNullException("formulaElementPointer");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (!CmlFormula.IsFormula(formulaElementPointer))
            {
                throw new ArgumentException("formulaElementPointer must point to a formula");
            }
            new CmlFormula(formulaElementPointer) {Inline = value};
            return contextObject;
        }

        /// <summary>
        /// Add an inline formula
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointer">pointer to the parent of the inline formula</param>
        /// <param name="value">the value of the inline formula to set</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        /// <exception cref="ArgumentException">if the pointer or value is not appropriate</exception>
        public static ContextObject AddInlineFormula(ContextObject contextObject, XObject pointer, string value)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            XElement xElement = pointer as XElement;
            if (xElement == null)
            {
                throw new ArgumentException("pointer must be able to accept child elements");
            }
            CmlFormula formula = new CmlFormula {Inline = value};
            xElement.Add(formula.DelegateElement);
            return FixMissingConciseFormula(contextObject, formula.DelegateElement);
        }

        /// <summary>
        /// Sets the value of the name
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointer">pointer to the the name</param>
        /// <param name="value">the value of the name to set</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        /// <exception cref="ArgumentException">if the pointer or value is not appropriate</exception>
        public static ContextObject EditNameValue(ContextObject contextObject, XObject pointer, string value)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            XElement xElement = pointer as XElement;
            if (xElement == null)
            {
                throw new ArgumentException("pointer must point to the name");
            }
            new CmlName(xElement) {DelegateElement = {Value = value}};
            return contextObject;
        }

        /// <summary>
        /// Add an inline formula
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointer">pointer to the parent of the name</param>
        /// <param name="value">the value of the name to add</param>
        /// <param name="type">the type of name which is being added</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        /// <exception cref="ArgumentException">if the pointer or value is not appropriate</exception>
        public static ContextObject AddName(ContextObject contextObject, XObject pointer, string value,
                                            string dictRefNamespace, string type)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            XElement xElement = pointer as XElement;
            if (xElement == null)
            {
                throw new ArgumentException("pointer must be able to accept child elements");
            }
            CmlName name = new CmlName();
            string prefix = xElement.GetPrefixOfNamespace(dictRefNamespace);
            XNamespace xns = dictRefNamespace;
            if (prefix == null)
            {
                int i = 0;
                while (xElement.GetPrefixOfNamespace("p" + i) != null)
                {
                    i++;
                }
                prefix = "p" + i;
                name.DelegateElement.Add(new XAttribute(xns + prefix, xns));
            }
            name.DelegateElement.Value = value;
            name.DictRef = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", prefix, type);
            xElement.Add(name.DelegateElement);
            return contextObject;
        }

        /// <summary>
        /// Remove the pointer (and any children)
        /// PMR: NOT FINISHED and low priority
        /// FIXME
        /// is this used anywhere?
        /// if pointer is element and has no namespace or non-CML one, remove
        /// if pointer is attribute and namespace is non-null and non-CML remove
        /// if pointer is CML element reroute
        /// if pointer is CML attribute reroute
        /// if pointer does not fall into special cases, ignore
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointer">the XObject to remove</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception
        public static ContextObject Remove(ContextObject contextObject, XObject pointer)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }
            CmlMolecule alteredMolecule = null;
            if (pointer is XAttribute)
            {
                XAttribute attribute = (XAttribute) pointer;
                XName xName = attribute.Name;

                if (CmlConstants.Cmlns.Equals(xName.Namespace))
                {
                    /// CML attribute shouldn't exist
                    throw new NumboException("Shouldn't be attributes with CML namespace");
                }
                if (!string.Empty.Equals(xName.Namespace))
                {
                    /// foreign attribute
                    /// why are we calling this method to remove?
                    attribute.Remove();
                }
                else
                {
                    // default attributes
                    if (CmlAttribute.IsotopeNumber.Equals(xName))
                    {
                        new CmlAtom(attribute.Parent).RemoveIsotope();
                    }
                    else if (CmlAttribute.Title.Equals(xName))
                    {
                        attribute.Remove();
                    }
                    else if (CmlAttribute.Inline.Equals(xName))
                    {
                        attribute.Remove();
                    }
                        // add others as we think of them
                    else
                    {
                        throw new NotImplementedException("cannot remove attribute: " + xName + " with Remove() yet");
                    }
                }
            }
            else if (pointer is XElement)
            {
                XElement element = (XElement) pointer;
                if (CmlConstants.Cmlns.Equals(element.Name.Namespace))
                {
                    if (CmlMolecule.IsMolecule(element))
                    {
                        alteredMolecule = CmlUtils.GetFirstAncestorMolecule(element);
                        element.Remove();
                    }
                    else if (CmlAtom.IsAtom(element))
                    {
                        alteredMolecule = CmlUtils.GetFirstAncestorMolecule(element);
                        new CmlAtom(element).DeleteSimple(true);
                    }
                    else if (CmlBond.IsBond(element))
                    {
                        alteredMolecule = CmlUtils.GetFirstAncestorMolecule(element);
                        new CmlBond(element).DeleteSimple(true);
                    }
                    else if (CmlFormula.IsFormula(element))
                    {
                        alteredMolecule = CmlUtils.GetFirstAncestorMolecule(element);
                        element.Remove();
                    }
                    else
                    {
                        alteredMolecule = CmlUtils.GetFirstAncestorMolecule(element);
                        element.Remove();
                    }
                }
                else
                {
                    // not CML
                    element.Remove();
                }
            }
            else
            {
                throw new ArgumentException("pointer must be element or attribute");
            }
            if (alteredMolecule != null)
            {
                alteredMolecule.NormalizeConciseFormulas();
            }
            return contextObject;
        }

        /// <summary>
        /// Removes all child CMLLite elements from this molecule and all child molecules which 
        /// rely on the connection table (such as name, spectrum, inline formula) except those which can be 
        /// updated (eg concise formula) which are added/updated as necessary.
        /// PMR: Needs more work
        /// </summary>
        /// <param name="contextObject">The context object containing the CML and ChemSS</param>
        /// <param name="moleculePointer">the molecule from which values should be removed or updated</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static ContextObject MarkOutdatedOrUpdateDerivedValues(ContextObject contextObject,
                                                                      XObject moleculePointer)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (moleculePointer == null)
            {
                throw new ArgumentNullException("moleculePointer");
            }
            XElement xMolecule = moleculePointer as XElement;
            if (xMolecule == null)
            {
                throw new ArgumentException("moleculePointer must point to an element");
            }
            IEnumerable<XElement> nameElements =
                xMolecule.XPathSelectElements(".//*[local-name()='" + CmlName.Tag + "' and namespace-uri()='" +
                                              CmlConstants.Cmlns + "']");
            foreach (XElement element in nameElements)
            {
                if (CmlName.IsName(element))
                {
                    CmlName name = new CmlName(element);
                    name.AnnotateOutdated();
                }
            }
            IEnumerable<XElement> formulaElements =
                xMolecule.XPathSelectElements(".//*[local-name()='" + CmlFormula.Tag + "' and namespace-uri()='" +
                                              CmlConstants.Cmlns + "']");
            foreach (XElement element in formulaElements)
            {
                if (CmlFormula.IsFormula(element))
                {
                    CmlFormula formula = new CmlFormula(element);
                    formula.AnnotateOutdated();
                }
            }

            // remove all the formulae below this molecule
            xMolecule.Descendants(CmlConstants.CmlxNamespace + CmlFormula.Tag).Remove();

            // add concise formula to this molecule and any child molecules
            CmlMolecule molecule = new CmlMolecule(xMolecule);
            CmlFormula.AddConciseFormulas(molecule, true);
            return contextObject;
        }

        /// <summary>
        /// If concise formula is missing then it is calculated (if possible) and added to the formula element 
        /// </summary>
        /// <param name="contextObject">The context object containing the CML and ChemSS</param>
        /// <param name="formulaPointer">the molecule from which values should be removed or updated</param>
        /// <returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static ContextObject FixMissingConciseFormula(ContextObject contextObject, XElement formulaPointer)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (formulaPointer == null)
            {
                throw new ArgumentNullException("formulaPointer");
            }
            // add concise formula to this formula if possible
            CmlFormula formula = new CmlFormula(formulaPointer);
            CmlMolecule parentMolecule = CmlUtils.GetFirstAncestorMolecule(formula);
            string concise = CmlFormula.CalculateConciseFormula(parentMolecule);
            if (concise != null)
            {
                formula.Concise = concise;
            }
            return contextObject;
        }

        ///<summary>
        ///Flips all the atoms in a molecule (and all child molecules) about a vector through a point
        ///</summary>
        ///<param name="contextObject">the ContextObject</param>
        ///<param name="moleculePointer">The pointer to the molecule which contains atoms to flip</param>
        ///<param name="vector">The vector about which to flip</param>
        ///<param name="point">The point where the vector is positioned</param>
        ///<returns>the ContextObject</returns>
        /// <exception cref="ArgumentNullException">If contextObject, vector or point are null</exception>
        /// <exception cref="ArgumentException">if moleculePointer is not to a molecule</exception>
        public static ContextObject FlipMoleculeAboutVectorThroughPoint(ContextObject contextObject,
                                                                        XElement moleculePointer, Vector2 vector,
                                                                        Point2 point)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (moleculePointer == null)
            {
                throw new ArgumentNullException("moleculePointer");
            }
            if (!CmlMolecule.IsMolecule(moleculePointer))
            {
                throw new ArgumentException("molecule pointer does not point to a molecule","moleculePointer");
            }
            if (point == null)
            {
                throw new ArgumentNullException("point");
            }
            if (vector == null)
            {
                throw new ArgumentNullException("vector");
            }
            CmlMolecule molecule = new CmlMolecule(moleculePointer);
            ICollection<CmlAtom> atoms = molecule.GetAllAtoms();
            HashSet<string> atomIDs = new HashSet<string>();
            foreach (CmlAtom atom in atoms)
            {
                atomIDs.Add(atom.Id);
            }
            Transform2 flipTransform = Transform2.FlipAboutVectorThroughPoint(vector, point);
            molecule.Transform2D(flipTransform);
            CidLogger record = new CidLogger("FlipMoleculeAboutVectorThroughPoint");
            record.Add("moleculePointer", moleculePointer);
            record.Add("vector", vector);
            record.Add("point", point);
            record.AddTo(molecule);
            molecule.CheckAndFixStereochemistry();
            return contextObject;
        }

        /// <summary>
        /// flip atoms about unique connecting bond
        /// if a solitary bond to non-Hydrogen atom exists the
        /// group can be flipped
        /// </summary>
        /// <param name="contextObject">ContextObject</param>
        /// <param name="atomPointers">atomPointers (at least 2)</param>
        /// <returns>ContextObject</returns>
        public static ContextObject FlipAtomsAbontSolitaryExternalBond(ContextObject contextObject,
                                                                       IEnumerable<XElement> atomPointers)
        {
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
                IEnumerable<CmlAtom> flippableAtoms =
                    CmlMolecule.AddBondedHydrogensToGroup(CmlAtom.GetAtoms(atomPointers));
                CmlBond bond = ChemicalIntelligence.GetFlippableBond(contextObject,
                                                                     CmlAtom.GetAtomPointers(flippableAtoms));
                if (bond != null)
                {
                    CmlAtom pivotAtom = bond.GetAtomInAtomPointersInBond(atomPointers);
                    CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(pivotAtom);
                    if (pivotAtom != null)
                    {
                        CmlAtom otherAtom = bond.GetAtomAtOtherEnd(pivotAtom);
                        Vector2 vector = otherAtom.GetVectorTo(pivotAtom);
                        IEnumerable<XElement> flippableAtomPointers = CmlAtom.GetXElements(flippableAtoms);
                        contextObject = FlipAboutVectorThroughPoint(contextObject, flippableAtomPointers, vector,
                                                                    pivotAtom.Point2);
                    }
                    CidLogger record = new CidLogger("FlipAtomsAbontSolitaryExternalBond");
                    record.Add("atomPointers", atomPointers);
                    record.AddTo(molecule);
                }
                CheckAndFixStereochemistry(atomPointers);
            }
            return contextObject;
        }

        ///<summary>
        /// TODO how do we initiate this? We need a gesture
        ///</summary>
        ///<param name="contextObject">the ContextObject</param>
        ///<param name="atomPointers"></param>
        ///<param name="vector"></param>
        ///<param name="point"></param>
        ///<returns>the ContextObject</returns>
        public static ContextObject FlipAboutVectorThroughPoint(ContextObject contextObject,
                                                                IEnumerable<XElement> atomPointers, Vector2 vector,
                                                                Point2 point)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (atomPointers == null)
            {
                throw new ArgumentNullException("atomPointers");
            }
            if (vector == null)
            {
                throw new ArgumentNullException("vector");
            }
            if (point == null)
            {
                throw new ArgumentNullException("point");
            }
            if (atomPointers.Count() > 0)
            {
                Transform2 flipTransform = Transform2.FlipAboutVectorThroughPoint(vector, point);
                foreach (XElement atomPointer in atomPointers)
                {
                    CmlAtom atom = new CmlAtom(atomPointer);
                    atom.Transform2D(flipTransform);
                }
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atomPointers.ElementAt(0));
                CidLogger record = new CidLogger("FlipAboutVectorThroughPoint");
                record.Add("atomPointers", atomPointers);
                record.Add("vector", vector);
                record.Add("point", point);
                record.AddTo(molecule);
                molecule.RemoveAllWedgeHatchBondStereos();
                CheckAndFixStereochemistry(atomPointers);
            }
            return contextObject;
        }

        /// <summary>
        /// Rotate a set of Objects which have both x2 and y2 attributes 
        //// through an angle of theta (degrees) about the Point p
        /// </summary>
        /// <remarks>
        /// The rotation is defined to be anticlockwise for positive theta. Negative values of 
        /// theta will produce a clockwise rotation.
        /// </remarks>
        /// <param name="contextObject">The context object containing the CML and ChemSS</param>
        /// <param name="pointers">the set of XElements which are to be transformed</param>
        /// <param name="point">the point about which rotation will occur</param>
        /// <param name="theta">the angle of rotation anticlockwise in degrees</param>
        /// <returns>The ContextObject which contains the modified CML</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static ContextObject RotateAtomSet2D(ContextObject contextObject, IEnumerable<XElement> pointers,
                                                    Angle theta, Point2 point)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers == null)
            {
                throw new ArgumentNullException("pointers");
            }
            if (theta == null)
            {
                throw new ArgumentNullException("theta");
            }
            if (point == null)
            {
                throw new ArgumentNullException("point");
            }
            if (pointers.Count() > 0)
            {
                if (CheckNamespace(pointers, "RotateAtomSet2D may only be applied to CML namespaced elements."))
                {
                    Transform2 transform = Transform2.CreateRotationMatrixAboutPoint(theta, point);
                    Transform2D(contextObject, transform, pointers);
                    CheckAndFixStereochemistry(pointers);
                }
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(pointers.ElementAt(0));
                if (molecule != null)
                {
                    CidLogger record = new CidLogger("FlipAboutVectorThroughPoint");
                    record.Add("pointers", pointers);
                    record.Add("theta", theta);
                    record.Add("point", point);
                    record.AddTo(molecule);
                }
            }
            return contextObject;
        }

        /// <summary>
        /// Translate a set of Objects which have both x2 and y2 attributes 
        /// by the vector 
        /// </summary>
        /// <param name="contextObject">The context object containing the CML and ChemSS</param>
        /// <param name="pointers">the set of XElements which are to be transformed</param>
        /// <param name="vector"></param>
        /// <returns>The ContextObject which contains the modified CML</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static ContextObject TranslateAtomSet2D(ContextObject contextObject, IEnumerable<XElement> pointers,
                                                       Vector2 vector)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointers == null)
            {
                throw new ArgumentNullException("pointers");
            }
            if (vector == null)
            {
                throw new ArgumentNullException("vector");
            }

            if (pointers.Count() > 0)
            {
                if (CheckNamespace(pointers, "TranslateAtomSet2D may only be applied to CML namespaced elements."))
                {
                    Transform2 transform = Transform2.CreateTranslation(vector);
                    ContextObject newContextObject = Transform2D(contextObject, transform, pointers);
                    CidLogger record = new CidLogger("TranslateAtomSet2D");
                    record.Add("pointers", pointers);
                    record.Add("vector", vector);
                    record.AddTo(CmlUtils.GetFirstAncestorMolecule(pointers.ElementAt(0)));
                    CheckAndFixStereochemistry(pointers);
                    return newContextObject;
                }
            }
            return contextObject;
        }

        /// <summary>
        /// set charge on atom or molecule
        /// do not adjust hydrogens
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="pointer"></param>
        /// <param name="charge"></param>
        /// <returns>the ContextObject</returns>
        public static ContextObject EditOrSetCharge(ContextObject contextObject, XElement pointer, int charge)
        {
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
                CmlAtom atom = new CmlAtom(pointer) {FormalCharge = charge};
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atom);
                molecule.NormalizeConciseFormulas();
                CidLogger record = new CidLogger("EditOrSetCharge");
                record.Add("pointer", pointer);
                record.Add("charge", charge);
                record.AddTo(molecule);
                CheckAndFixStereochemistry(pointer);
                AnnotateOutdated(pointer);
            }
            else if (CmlMolecule.IsMolecule(pointer))
            {
                CmlMolecule molecule = new CmlMolecule(pointer) {FormalCharge = charge};
                molecule.NormalizeConciseFormulas();
                CidLogger record = new CidLogger("EditOrSetCharge");
                record.Add("pointer", pointer);
                record.Add("charge", charge);
                record.AddTo(molecule);
                molecule.CheckAndFixStereochemistry();
                AnnotateOutdated(pointer);
            }
            return contextObject;
        }

        /// <summary>
        /// remove charge from atom
        /// the final state may not be chemically consistent
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="atomPointers">atom to remove from; must have single pointer</param>
        /// <returns>contextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static ContextObject UnsetCharge(
            ContextObject contextObject, IEnumerable<XElement> atomPointers)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (atomPointers == null)
            {
                throw new ArgumentNullException("atomPointers");
            }
            if (atomPointers.Count() == 1)
            {
                XElement atomPointer = atomPointers.ElementAt(0);
                if (CmlAtom.IsAtom(atomPointer))
                {
                    new CmlAtom(atomPointer) {FormalCharge = 0};
                    AnnotateOutdated(atomPointer);
                }
                CidLogger record = new CidLogger("UnsetCharge");
                record.Add("atomPointers", atomPointers);
                record.AddTo(CmlUtils.GetFirstAncestorMolecule(atomPointers.ElementAt(0)));
                CheckAndFixStereochemistry(atomPointers);
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atomPointer);
                molecule.NormalizeConciseFormulas();
                AnnotateOutdated(molecule.DelegateElement);
            }
            return contextObject;
        }

        /// <summary>
        /// set bond stereo
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="bondPointer"></param>
        /// <param name="value">Order</param>
        /// <returns>the ContextObject</returns>
        public static ContextObject SetBondStereo(ContextObject contextObject, XElement bondPointer, string value)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (bondPointer == null)
            {
                throw new ArgumentNullException("bondPointer");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (CmlBond.IsBond(bondPointer))
            {
                CmlBond bond = new CmlBond(bondPointer);
                bond.SetStereo(value);
                AnnotateOutdated(bondPointer);
                CidLogger record = new CidLogger("SetBondStereo");
                record.Add("bondPointer", bondPointer);
                record.AddTo(CmlUtils.GetFirstAncestorMolecule(bond));
            }
            return contextObject;
        }

        /// <summary>
        /// set bond order and do not adjust chemistry
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="bondPointer"></param>
        /// <param name="value">Order</param>
        /// <returns>the ContextObject</returns>
        public static ContextObject SetBondOrder(ContextObject contextObject, XElement bondPointer, string value)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (bondPointer == null)
            {
                throw new ArgumentNullException("bondPointer");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (CmlBond.IsBond(bondPointer))
            {
                BondOrder bondOrder = BondOrder.GetBondOrderFromCmlOrder(value);
                if (bondOrder.IntegerBondOrder != null)
                {
                    CmlBond bond = new CmlBond(bondPointer);
                    bond.ForceSetOrder(value);
                }
                CidLogger record = new CidLogger("SetBondOrder");
                record.Add("bondPointer", bondPointer);
                record.Add("value", value);
                record.AddTo(CmlUtils.GetFirstAncestorMolecule(bondPointer));
                CheckAndFixStereochemistry(bondPointer);
                AnnotateOutdated(bondPointer);
            }
            return contextObject;
        }

        /// <summary>
        /// set bond order
        /// if S,D,T tries to adjust connectivity and hydrogen else punt
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="bondPointer"></param>
        /// <param name="value">Order</param>
        /// <returns>the ContextObject</returns>
        public static ContextObject SetBondOrderAndFixChemistry(ContextObject contextObject,
                                                                XElement bondPointer, string value)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (bondPointer == null)
            {
                throw new ArgumentNullException("bondPointer");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (CmlBond.IsBond(bondPointer))
            {
                BondOrder bondOrder = BondOrder.GetBondOrderFromCmlOrder(value);
                if (bondOrder.IntegerBondOrder != null)
                {
                    CmlBond bond = new CmlBond(bondPointer);
                    bond.SetOrderAdjustChemistry(value);
                    CmlMolecule molecule = bond.GetFirstAncestorMolecule();
                    molecule.NormalizeConciseFormulas();
                    CidLogger record = new CidLogger("SetBondOrderAndFixChemistry");
                    record.Add("bondPointer", bondPointer);
                    record.Add("value", value);
                    record.AddTo(CmlUtils.GetFirstAncestorMolecule(bond));
                    CheckAndFixStereochemistry(bondPointer);
                    AnnotateOutdated(bondPointer);
                }
            }
            return contextObject;
        }

        /// <summary>
        /// set bond order
        /// if S,D,T tries to adjust connectivity and hydrogen else punt
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="bondPointers"></param>
        /// <param name="value">Order</param>
        /// <returns>the ContextObject</returns>
        public static ContextObject EditOrSetBondOrder(ContextObject contextObject,
                                                       IEnumerable<XElement> bondPointers, string value)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (bondPointers == null)
            {
                throw new ArgumentNullException("bondPointers");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (bondPointers.Count() > 0)
            {
                foreach (XElement bondPointer in bondPointers)
                {
                    SetBondOrder(contextObject, bondPointer, value);
                }
                CidLogger record = new CidLogger("EditOrSetBondOrder");
                record.Add("bondPointers", bondPointers);
                record.Add("value", value);
                record.AddTo(CmlUtils.GetFirstAncestorMolecule(bondPointers.ElementAt(0)));
                CheckAndFixStereochemistry(bondPointers);
                AnnotateOutdated(bondPointers.ElementAt(0));
            }
            return contextObject;
        }

        /// <summary>
        /// remove electron but do not adjust hydrogens
        /// same as IncreaseCharge
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="pointer">molecule or atom</param>
        /// <returns>the ContextObject</returns>
        public static ContextObject RemoveElectron(ContextObject contextObject, XElement pointer)
        {
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
                atom.RemoveElectron();
                CidLogger record = new CidLogger("RemoveElectron");
                record.Add("pointer", pointer);
                record.AddTo(CmlUtils.GetFirstAncestorMolecule(pointer));
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atom);
                molecule.NormalizeConciseFormulas();
                CheckAndFixStereochemistry(pointer);
                AnnotateOutdated(pointer);
            }
            else if (CmlMolecule.IsMolecule(pointer))
            {
                CmlMolecule molecule = new CmlMolecule(pointer);
                molecule.RemoveElectron();
                molecule.NormalizeConciseFormulas();
                CidLogger record = new CidLogger("RemoveElectron");
                record.Add("pointer", pointer);
                record.AddTo(molecule);
                AnnotateOutdated(pointer);
            }
            return contextObject;
        }

        /// <summary>
        /// electron but do not adjust hydrogens
        /// same as DecreaseCharge
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="pointer">molecule or atom</param>
        /// <returns>the ContextObject</returns>
        public static ContextObject AddElectron(ContextObject contextObject, XElement pointer)
        {
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
                atom.AddElectron();
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atom);
                molecule.NormalizeConciseFormulas();
                CidLogger record = new CidLogger("AddElectron");
                record.Add("pointer", pointer);
                record.AddTo(CmlUtils.GetFirstAncestorMolecule(pointer));
                CheckAndFixStereochemistry(pointer);
                AnnotateOutdated(pointer);
            }
            else if (CmlMolecule.IsMolecule(pointer))
            {
                CmlMolecule molecule = new CmlMolecule(pointer);
                molecule.AddElectron();
                molecule.NormalizeConciseFormulas();
                CidLogger record = new CidLogger("AddElectron");
                record.Add("pointer", pointer);
                record.AddTo(molecule);
                AnnotateOutdated(pointer);
            }
            return contextObject;
        }

        /// <summary>
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="pointer"></param>
        /// <param name="spinMultiplicity"></param>
        /// <returns>the ContextObject</returns>
        public static ContextObject EditOrSetSpinMultiplicity(ContextObject contextObject, XElement pointer,
                                                              int spinMultiplicity)
        {
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
                CmlAtom atom = new CmlAtom(pointer) {SpinMultiplicity = spinMultiplicity};
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atom);
                CidLogger record = new CidLogger("EditOrSetSpinMultiplicity");
                record.Add("pointer", pointer);
                record.Add("spinMultiplicity", spinMultiplicity);
                record.AddTo(molecule);
                CheckAndFixStereochemistry(pointer);
                molecule.NormalizeConciseFormulas();
                AnnotateOutdated(pointer);
            }
            else if (CmlMolecule.IsMolecule(pointer))
            {
                CmlMolecule molecule = new CmlMolecule(pointer) {SpinMultiplicity = spinMultiplicity};
                CidLogger record = new CidLogger("EditOrSetSpinMultiplicity");
                record.Add("pointer", pointer);
                record.Add("spinMultiplicity", spinMultiplicity);
                record.AddTo(molecule);
                molecule.NormalizeConciseFormulas();
                AnnotateOutdated(pointer);
            }
            CheckAndFixStereochemistry(pointer);
            return contextObject;
        }

        /// <summary>
        /// force set of isotopeNumber
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="atomPointer">atom to set Isotope on</param>
        /// <param name="isotopeNumber">isotopeNumber (> 0 but otherwise no check)</param>
        /// <returns>contextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static ContextObject EditOrSetIsotopeNumber(
            ContextObject contextObject, XElement atomPointer, int isotopeNumber)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (atomPointer == null)
            {
                throw new ArgumentNullException("atomPointer");
            }
            if (isotopeNumber <= 0)
            {
                throw new ArgumentException("Non-positive isotopeNumber: " + isotopeNumber);
            }
            if (CmlAtom.IsAtom(atomPointer))
            {
                new CmlAtom(atomPointer) {IsotopeNumber = isotopeNumber};
                CidLogger record = new CidLogger("EditOrSetIsotopeNumber");
                record.Add("atomPointer", atomPointer);
                record.Add("isotopeNumber", isotopeNumber);
                record.AddTo(CmlUtils.GetFirstAncestorMolecule(atomPointer));
                CheckAndFixStereochemistry(atomPointer);
                AnnotateOutdated(atomPointer);
            }
            return contextObject;
        }

        /// <summary>
        /// remove isotope information from atom
        /// this restores the default - no information on isotope
        /// (Note: for Carbon isotope=12 is different from no isotope information)
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="atomPointers"></param>
        /// <returns>contextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static ContextObject UnsetIsotopeNumber(
            ContextObject contextObject, IEnumerable<XElement> atomPointers)
        {
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
                CmlAtom atom = null;
                foreach (XElement atomPointer in atomPointers)
                {
                    if (CmlAtom.IsAtom(atomPointer))
                    {
                        atom = new CmlAtom(atomPointer);
                        if (atom.IsotopeNumber != null)
                        {
                            atom.RemoveIsotope();
                        }
                        AnnotateOutdated(atomPointer);
                    }
                }
                if (atom != null)
                {
                    CidLogger record = new CidLogger("UnsetIsotopeNumber");
                    record.Add("atomPointers", atomPointers);
                    record.AddTo(CmlUtils.GetFirstAncestorMolecule(atom));
                }
            }
            CheckAndFixStereochemistry(atomPointers);
            return contextObject;
        }

        /// <summary>
        /// adds a proton (H+) to an atom
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="atomPointer">atom</param>
        /// <returns>the ContextObject</returns>
        public static ContextObject AddProton(
            ContextObject contextObject, XElement atomPointer)
        {
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
                atom.AddProtonAndAdjustCoordinates();
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atom);
                if (molecule != null)
                {
                    molecule.NormalizeConciseFormulas();
                }
                CidLogger record = new CidLogger("AddProton");
                record.Add("atomPointer", atomPointer);
                record.AddTo(CmlUtils.GetFirstAncestorMolecule(atomPointer));
                AnnotateOutdated(atomPointer);
            }
            return contextObject;
        }

        /// <summary>
        /// adds a proton (H+) to an atom
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="atomPointer">atom</param>
        /// <returns>the ContextObject</returns>
        public static ContextObject RemoveProton(
            ContextObject contextObject, XElement atomPointer)
        {
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
                atom.RemoveProtonAndAdjustCoordinates();
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atom);
                if (molecule != null)
                {
                    molecule.NormalizeConciseFormulas();
                }
                CidLogger record = new CidLogger("RemoveProton");
                record.Add("atomPointer", atomPointer);
                record.AddTo(CmlUtils.GetFirstAncestorMolecule(atomPointer));
                AnnotateOutdated(atomPointer);
            }
            return contextObject;
        }

        /// <summary>
        /// adds a hydrogen (H.) to an atom
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="atomPointer">atom</param>
        /// <returns>the ContextObject</returns>
        public static ContextObject AddHydrogenDot(
            ContextObject contextObject, XElement atomPointer)
        {
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
                atom.AddHydrogenDotAndAdjustCoordinates();
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atom);
                if (molecule != null)
                {
                    molecule.NormalizeConciseFormulas();
                }
                CidLogger record = new CidLogger("AddHydrogenDot");
                record.Add("atomPointer", atomPointer);
                record.AddTo(CmlUtils.GetFirstAncestorMolecule(atomPointer));
                AnnotateOutdated(atomPointer);
            }
            return contextObject;
        }

        /// <summary>
        /// removers a hydrogen (H.) from an atom
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="atomPointer">atom</param>
        /// <returns>the ContextObject</returns>
        public static ContextObject RemoveHydrogenDot(
            ContextObject contextObject, XElement atomPointer)
        {
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
                atom.RemoveHydrogenDotAndAdjustCoordinates();
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atom);
                if (molecule != null)
                {
                    molecule.NormalizeConciseFormulas();
                }
                CidLogger record = new CidLogger("RemoveHydrogenDot");
                record.Add("atomPointer", atomPointer);
                record.AddTo(CmlUtils.GetFirstAncestorMolecule(atomPointer));
                AnnotateOutdated(atomPointer);
            }
            return contextObject;
        }

        /// <summary>
        /// replaces atom by group
        /// 
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="atomPointer">atom to replace by group</param>
        /// <param name="groupAbbreviation">abbreviation of replacing group</param>
        /// <returns>contextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static ContextObject SubstituteAtomByGroup(ContextObject contextObject, XElement atomPointer,
                                                          string groupAbbreviation)
        {
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
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(atomPointer);
                if (molecule != null)
                {
                    CmlAtom atom = new CmlAtom(atomPointer);
                    atom.SubstituteAtomAndHydrogensWithGroup(groupAbbreviation);
                    CidLogger record = new CidLogger("SubstituteAtomByGroup");
                    record.Add("atomPointer", atomPointer);
                    record.Add("groupAbbreviation", groupAbbreviation);

                    CheckAndFixStereochemistry(atomPointer);
                    molecule.NormalizeConciseFormulas();
                    AnnotateOutdated(molecule.DelegateElement);
                }
            }
            return contextObject;
        }

        /// <summary>
        /// replaces bond by group
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="bondPointer">bond to replace by group</param>
        /// <param name="groupAbbreviation">abbreviation of replacing group</param>
        /// <returns>contextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        public static ContextObject SubstituteBondByGroup(ContextObject contextObject, XElement bondPointer,
                                                          string groupAbbreviation)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (bondPointer == null)
            {
                throw new ArgumentNullException("bondPointer");
            }
            if (CmlBond.IsBond(bondPointer))
            {
                CmlBond bond = new CmlBond(bondPointer);
                CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(bondPointer);
                bond.SubstituteBondAndHydrogensWithGroup(groupAbbreviation);
                if (molecule != null)
                {
                    molecule.NormalizeConciseFormulas();
                }
                CmlUtils.RemoveAllChildLabels(bond);
                CidLogger record = new CidLogger("SubstituteBondByGroup");
                record.Add("bondPointer", bondPointer);
                record.AddTo(molecule);
                AnnotateOutdated(bondPointer);
            }
            return contextObject;
        }

        /// <summary>
        /// Checks the semantics of the chemistry below and including the pointer
        /// and marks (cmlx:warning="isotopeNumber") for example.
        /// </summary>
        /// <param name="contextObject">the ContextObject containing the pointer</param>
        /// <param name="pointer">a pointer to the eldest element which should be validated</param>
        /// <returns>the context object annotated with possible chemical rule violations</returns>
        public static ContextObject AnnotateViolationsOfCurrentChemicalRules(
            ContextObject contextObject, XElement pointer)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }
            if (CmlMolecule.IsMolecule(pointer))
            {
                CmlMolecule molecule = new CmlMolecule(pointer);
                molecule.AnnotateViolationsOfCurrentChemicalRules();
            }
            return contextObject;
        }

        /// <summary>
        /// Convenience method which applies the 2D transform to each of the elements. 
        /// This should check that the elements are from the contextObject supplied, that each has 2D coordinates 
        /// (maybe IHas2DCoordinates?).
        /// Only when all the points are successfully transformed should the CML in the contextObject be updated
        /// to ensure that no half completed transformations occur.
        /// </summary>
        /// <param name="contextObject">The ContextObject containing the elements to transform</param>
        /// <param name="transform">The Transform2 to apply to the elements</param>
        /// <param name="elements">The elements to apply the transformation to</param>
        /// <returns>The updated ContextObject</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        private static ContextObject Transform2D(ContextObject contextObject, Transform2 transform,
                                                 IEnumerable<XElement> elements)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (transform == null)
            {
                throw new ArgumentNullException("transform");
            }
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }
            Dictionary<XElement, Point2> map = new Dictionary<XElement, Point2>();

            foreach (XElement e in elements)
            {
                /// TODO eventally this should not look like this - it should use 
                /// a programmatic accessor for example Point2 p = e as IHas2DCoords
                /// if (p != null) continue
                XAttribute x2 = e.Attribute("x2");
                XAttribute y2 = e.Attribute("y2");

                if ((x2 == null) || (y2 == null))
                {
                    throw new ArgumentException(
                        "Transform may only be applied to XElements with x2 and y2 attributes.");
                }

                /// TODO hack - schema specifies that x2 and y2 must be doubles 
                /// ideally the Linq system should automatically convert from string
                /// this may mean defining how CMLElements should serialise and deserialize to Linq
                double xval = Double.Parse(x2.Value, CultureInfo.InvariantCulture);
                double yval = Double.Parse(y2.Value, CultureInfo.InvariantCulture);
                if ((xval.Equals(double.NaN)) || (yval.Equals(double.NaN)))
                {
                    throw new ArgumentException("Element has coordinate that is not a number.");
                }
                Point2 point = new Point2(xval, yval);
                Point2 transformedPoint = transform.Transform(point);
                map.Add(e, transformedPoint);
            }

            foreach (KeyValuePair<XElement, Point2> kvp in map)
            {
                XAttribute x2 = kvp.Key.Attribute("x2");
                XAttribute y2 = kvp.Key.Attribute("y2");
                x2.Value = kvp.Value.X.ToString(CultureInfo.InvariantCulture);
                y2.Value = kvp.Value.Y.ToString(CultureInfo.InvariantCulture);
            }
            return contextObject;
        }

        /// <summary>
        /// Check that all the elements are from the CML namesapce 
        /// </summary>
        /// <param name="elements">the XElements to check</param>
        /// <param name="exceptionMessage">the message an exception will throw</param>
        /// <returns>true if no exceptions thrown</returns>
        /// <exception cref="ArgumentNullException">if any of the arguments are null</exception>
        private static bool CheckNamespace(IEnumerable<XElement> elements, string exceptionMessage)
        {
            foreach (XElement e in elements)
            {
                string nameSpace = e.Name.Namespace.NamespaceName;
                if (nameSpace != CmlConstants.Cmlns)
                {
                    throw new ArgumentException(exceptionMessage);
                }
            }
            return true;
        }

        private static void CheckAndFixStereochemistry(IEnumerable<XElement> pointers)
        {
            if (pointers.Count() > 0)
            {
                CheckAndFixStereochemistry(pointers.ElementAt(0));
            }
        }

        private static void CheckAndFixStereochemistry(XElement pointer)
        {
            if (pointer != null)
            {
                if (CmlAtom.IsAtom(pointer))
                {
                    CmlMolecule molecule = CmlUtils.GetFirstAncestorMolecule(new CmlAtom(pointer));
                    if (molecule != null)
                    {
                        molecule.CheckAndFixStereochemistry();
                    }
                }
                else if (CmlMolecule.IsMolecule(pointer))
                {
                    new CmlMolecule(pointer).CheckAndFixStereochemistry();
                }
            }
        }

        /// <summary>
        /// add outdated attribute to any element that might be inconsistent after chemistry
        /// </summary>
        /// <param name="pointer"></param>
        private static void AnnotateOutdated(XElement pointer)
        {
            CmlMolecule molecule = CmlMolecule.IsMolecule(pointer)
                                       ? new CmlMolecule(pointer)
                                       : CmlUtils.GetFirstAncestorMolecule(pointer);
            if (molecule != null)
            {
                molecule.AnnotateOutdated();
            }
        }

        /// <summary>
        /// 
        /// current only processes toplevel molecules
        /// </summary>
        /// <param name="contextObject">the ContextObject</param>
        /// <param name="pointer"> </param>
        /// <returns>the ContextObject</returns>
        private static ContextObject NormalizeConciseFormulaOnImport(ContextObject contextObject, XElement pointer)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }
            IEnumerable<CmlMolecule> molecules = CmlUtils.GetAllToplevelDescendentMolecules(pointer);
            foreach (CmlMolecule molecule in molecules)
            {
                molecule.NormalizeConciseFormulasOnImport();
            }
            return contextObject;
        }
    }

    internal class CidLogger
    {
        private readonly ICollection<Arg> args;
        private readonly string instruction;

        internal CidLogger(string instruction)
        {
            this.instruction = instruction;
            args = new List<Arg>();
        }

        internal void Add(string name, IEnumerable<XElement> pointers)
        {
            args.Add(new Arg(name, pointers));
        }

        internal void Add(string name, IEnumerable<string> ss)
        {
            args.Add(new Arg(name, ss));
        }

        internal void Add(string name, XElement pointer)
        {
            args.Add(new Arg(name, pointer));
        }

        internal void Add(string name, string s)
        {
            args.Add(new Arg(name, s));
        }

        internal void Add(string name, Vector2 v)
        {
            args.Add(new Arg(name, v));
        }

        internal void Add(string name, Point2 p)
        {
            args.Add(new Arg(name, p));
        }

        internal void Add(string name, Angle a)
        {
            args.Add(new Arg(name, a));
        }

        internal void Add(string name, int i)
        {
            args.Add(new Arg(name, i));
        }

        internal void AddTo(CmlMolecule molecule)
        {
            XElement argListX = new XElement(CmlConstants.CmlxXNamespace + "cidCommand");
            argListX.Add(new XAttribute("instruction", instruction));
            foreach (Arg arg in args)
            {
                XElement argX = new XElement(CmlConstants.CmlxXNamespace + "arg");
                argX.Add(new XAttribute("class", arg.Classname));
                argX.Add(new XAttribute("name", arg.Name));
                argX.Value = arg.Value;
                argListX.Add(argX);
            }
            molecule.DelegateElement.Add(argListX);
        }
    }

    internal class Arg
    {
        public Arg(string name, int i)
        {
            this.Name = name;
            Classname = "integer";
            this.Value = "" + i;
        }

        public Arg(string name, double d)
        {
            this.Name = name;
            Classname = "double";
            this.Value = "" + d;
        }

        public Arg(string name, string s)
        {
            this.Name = name;
            Classname = "string";
            this.Value = s;
        }

        public Arg(string name, IEnumerable<string> ss)
        {
            this.Name = name;
            Classname = "string[]";
            StringBuilder sb = new StringBuilder();
            foreach (string s in ss)
            {
                sb.Append("{" + s + "}");
            }
            this.Value = sb.ToString();
        }

        public Arg(string name, Angle a)
        {
            this.Name = name;
            Classname = "Angle";
            this.Value = "" + a.Radians;
        }

        public Arg(string name, XElement element)
        {
            this.Name = name;
            Classname = element.Name.LocalName;
            XAttribute idAttribute = element.Attribute("id");
            this.Value = (idAttribute == null) ? "missingId" : idAttribute.Value;
        }

        public Arg(string name, Vector2 v)
        {
            this.Name = name;
            Classname = "Vector2";
            this.Value = "{" + v.X + "," + v.Y + "}";
        }

        public Arg(string name, Point2 p)
        {
            this.Name = name;
            Classname = "Point2";
            this.Value = "{" + p.X + "," + p.Y + "}";
        }

        public Arg(string name, IEnumerable<XElement> pointers)
        {
            this.Name = name;
            Classname = "element[]";
            StringBuilder sb = new StringBuilder();
            foreach (XElement pointer in pointers)
            {
                sb.Append("{[" + pointer.Name.LocalName + "]" + pointer.Attribute("id").Value + "}");
            }
            this.Value = sb.ToString();
        }

        public string Name { get; private set; }
        public string Classname { get; private set; }
        public string Value { get; private set; }
    }
}