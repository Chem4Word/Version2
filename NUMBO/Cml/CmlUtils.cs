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
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using Euclid;
using log4net;
using Numbo.Cml.Helpers;

namespace Numbo.Cml
{
    /// <summary>
    /// static methods providing utilities mainly for CML objects
    /// </summary>
    /// <author>pm286 started this</author>
    public static class CmlUtils
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (CmlUtils));

        /// <summary>
        /// iterates through list of atoms to find first H one
        /// </summary>
        /// <param name="atoms">list of atoms</param>
        /// <returns>null if none</returns>
        public static CmlAtom GetFirstH(this CmlAtom[] atoms)
        {
            CmlAtom firstAtom = null;
            if (atoms != null)
            {
                foreach (CmlAtom atom in atoms)
                {
                    if (PeriodicTable.Element.H.Equals(PeriodicTable.GetElement(atom.ElementType)))
                    {
                        firstAtom = atom;
                        break;
                    }
                }
            }
            return firstAtom;
        }

        /// <summary>
        /// iterates through list of atoms to find first non-H one
        /// </summary>
        /// <param name="atoms">list of atoms</param>
        /// <returns>null if none</returns>
        public static CmlAtom GetFirstNonH(this CmlAtom[] atoms)
        {
            CmlAtom firstAtom = null;
            if (atoms != null)
            {
                foreach (CmlAtom atom in atoms)
                {
                    if (!PeriodicTable.Element.H.Equals(PeriodicTable.GetElement(atom.ElementType)))
                    {
                        firstAtom = atom;
                        break;
                    }
                }
            }
            return firstAtom;
        }

        /// <summary>
        /// checks that all atoms are H
        /// </summary>
        /// <param name="atoms">list of atoms</param>
        /// <returns>true if list is non-null and contains no H (could be zero-length)</returns>
        public static bool AreAllH(this CmlAtom[] atoms)
        {
            bool allH = false;
            if (atoms != null)
            {
                allH = true;
                foreach (CmlAtom atom in atoms)
                {
                    if (!PeriodicTable.Element.H.Equals(PeriodicTable.GetElement(atom.ElementType)))
                    {
                        allH = false;
                        break;
                    }
                }
            }
            return allH;
        }

        /// <summary>
        /// checks that no atoms are H
        /// </summary>
        /// <param name="atoms">list of atoms</param>
        /// <returns>true if list is non-null and contains only H (could be zero-length)</returns>
        public static bool ContainNoH(this CmlAtom[] atoms)
        {
            bool noH = false;
            if (atoms != null)
            {
                noH = true;
                foreach (CmlAtom atom in atoms)
                {
                    if (PeriodicTable.Element.H.Equals(PeriodicTable.GetElement(atom.ElementType)))
                    {
                        noH = false;
                        break;
                    }
                }
            }
            return noH;
        }

        /// <summary>
        /// gets number of H atoms
        /// </summary>
        /// <param name="atoms">array of atoms</param>
        /// <returns>0 if null array; else count</returns>
        public static int GetHCount(this CmlAtom[] atoms)
        {
            int count = 0;
            if (atoms != null)
            {
                foreach (CmlAtom atom in atoms)
                {
                    if (PeriodicTable.Element.H.Equals(PeriodicTable.GetElement(atom.ElementType)))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// gets number of non-H atoms
        /// </summary>
        /// <param name="atoms">array of atoms</param>
        /// <returns>0 if null array; else count</returns>
        public static int GetNonHCount(this CmlAtom[] atoms)
        {
            int count = 0;

            foreach (CmlAtom atom in atoms)
            {
                if (!PeriodicTable.Element.H.Equals(PeriodicTable.GetElement(atom.ElementType)))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// concatenates values in array with single whitespace
        /// leaves result trimmed.
        /// </summary>
        /// <returns>concatenated string</returns>
        public static string Concatenate(IEnumerable<string> strings)
        {
            if (strings == null)
            {
                throw new ArgumentNullException("strings");
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < strings.Count(); i++)
            {
                if (i > 0)
                {
                    builder.Append(" ");
                }
                builder.Append(strings.ElementAt(i));
            }
            return builder.ToString();
        }

        /// <summary>
        /// Gets the first CMLMolecule at or below the container passed in or null if no CMLMolecules are found.
        /// </summary>
        /// <author>jat45</author>
        /// <param name="container">The XDocument or XElement to search in/below for a CMLMolecule</param>
        /// <returns>The first CMLMolecule below or at the XContainer or null</returns>
        /// <exception cref="ArgumentNullException">if container is null</exception>
        public static CmlMolecule GetFirstDescendentMolecule(XContainer container)
        {
            if (null == container)
            {
                throw new ArgumentNullException("container");
            }
            XElement element;
            if (container is XDocument)
            {
                element = ((XDocument) container).Root;
            }
            else
            {
                // container must be XElement
                element = container as XElement;
            }

            XElement moleculeXElement =
                element.DescendantsAndSelf(CmlConstants.CmlxNamespace + CmlMolecule.Tag).FirstOrDefault();

            return (moleculeXElement == null) ? null : new CmlMolecule(moleculeXElement);
        }

        /// <summary>
        /// Get the first ancestor molecule of this element or null if it do not exist
        /// </summary>
        /// <param name="xElement">the object to get the ancestor molecule of</param>
        /// <returns>the first ancestor molecule or null if xElement == null or ancestor does not exist</returns>
        /// <exception cref="ArgumentNullException">if xElement is null</exception>
        public static CmlMolecule GetFirstAncestorMolecule(XNode xElement)
        {
            CmlMolecule molecule = null;
            XElement moleculeXElement =
                xElement.XPathSelectElement("./ancestor::*[local-name()='" + CmlMolecule.Tag +
                                            "' and namespace-uri()='" + CmlConstants.Cmlns + "'][1]");
            if (moleculeXElement != null)
            {
                molecule = new CmlMolecule(moleculeXElement);
            }
            else
            {
                // no parent
                Log.Debug("No ancestor molecule Delegate");
            }
            return molecule;
        }

        /// <summary>
        /// Get the first ancestor molecule of this element or null if it do not exist
        /// </summary>
        /// <param name="xElements">the object to get the ancestor molecule of</param>
        /// <returns>the first ancestor molecule or null if xElement == null or ancestor does not exist</returns>
        /// <exception cref="ArgumentNullException">if xElement is null</exception>
        public static CmlMolecule GetFirstCommonAncestorMolecule(IEnumerable<XElement> xElements)
        {
            if (xElements == null)
            {
                return null;
            }
            if (xElements.Count() == 0)
            {
                return null;
            }
            HashSet<XElement> moleculeParents = new HashSet<XElement>();
            foreach (XElement xElement in xElements)
            {
                XElement moleculeXElement =
                    xElement.XPathSelectElement("./ancestor::*[local-name()='" + CmlMolecule.Tag +
                                                "' and namespace-uri()='" + CmlConstants.Cmlns + "'][1]");
                if (moleculeXElement == null)
                {
                    // no parent
                    Log.Debug("No ancestor molecule Delegate");
                    return null;
                }
                moleculeParents.Add(moleculeXElement);
            }
            return (moleculeParents.Count() == 1)
                       ? new CmlMolecule(moleculeParents.First())
                       : GetFirstCommonAncestorMolecule(moleculeParents);
        }

        /// <summary>
        /// Get the first ancestor molecule of this element or null if it does not exist
        /// </summary>
        /// <param name="cmlElement">the object to get the ancestor molecule of</param>
        /// <returns>the first ancestor molecule or null if cmlElement == null or ancestor does not exist</returns>
        public static CmlMolecule GetFirstAncestorMolecule(CmlElement cmlElement)
        {
            CmlMolecule molecule = null;
            if (cmlElement != null)
            {
                molecule = GetFirstAncestorMolecule(cmlElement.DelegateElement);
            }
            return molecule;
        }

        /// <summary>
        /// Remove trailing zeros from a double containing decimal point
        /// and will remove decimal point if all characters following it are noughts.
        /// 
        /// 2.00000 becomes 2
        /// 2.10000 beceoms 2.1
        /// 2.01000 becomes 2.01
        /// </summary>
        /// <param name="d">the double to truncate</param>
        /// <returns>a string without the trailing zeros</returns>
        public static string TruncateTrailingZeros(double d)
        {
            string s = d.ToString(CultureInfo.InvariantCulture);
            while (s.EndsWith("0", StringComparison.OrdinalIgnoreCase) && s.Contains("."))
            {
                int l = s.Length;
                s = s.Substring(0, l - 1);
            }
            if (s.EndsWith(".", StringComparison.OrdinalIgnoreCase))
            {
                int l = s.Length;
                s = s.Substring(0, l - 1);
            }
            return s;
        }

        /// <summary>
        /// always adds child convenience method
        /// throws exception for adding atoms or bonds to anything
        /// </summary>
        /// <param name="cmlElement">the element to add the child to</param>
        /// <param name="child">the element to add</param>
        public static void Add(CmlElement cmlElement, CmlElement child)
        {
            if (child is CmlAtom)
            {
                throw new ArgumentException("cannot use add(atom); use molecule.AddAtom(atom)", "child");
            }
            if (child is CmlBond)
            {
                throw new ArgumentException("cannot use add(bond); use molecule.AddBond(bond)", "child");
            }
            Remove(child); // to be safe
            cmlElement.DelegateElement.Add(child.DelegateElement);
        }

        /// <summary>
        /// always adds child convenience method
        /// removes any existing children of any type
        /// throws exception for adding atoms or bonds to anything
        /// </summary>
        /// <param name="cmlElement">the element to add the child to</param>
        /// <param name="child">the element to add</param>
        public static void AddOnlyChild(CmlElement cmlElement, CmlElement child)
        {
            IEnumerable<XElement> childElements = cmlElement.DelegateElement.Elements();
            foreach (XElement childElement in childElements)
            {
                childElement.Remove();
            }
            Add(cmlElement, child);
        }

        /// <summary>
        /// adds single child
        /// convenience method
        /// </summary>
        /// <param name="cmlElement"></param>
        /// <param name="child"></param>
        /// <param name="overwrite">if true removes all content; if false and not empty is silent no-op</param>
        public static void AddOnlyChild(CmlElement cmlElement, CmlElement child, bool overwrite)
        {
            IEnumerable<XElement> childElements = cmlElement.DelegateElement.Elements(child.DelegateElement.Name);
            if (overwrite)
            {
                foreach (XElement xe in childElements)
                {
                    xe.Remove();
                }
                cmlElement.DelegateElement.Add(child.DelegateElement);
            }
            else if (childElements.Count() == 0)
            {
                cmlElement.DelegateElement.Add(child.DelegateElement);
            }
        }

        /// <summary>
        /// Adds a namespace declaration to cmlElement and binds the uri provided to the given prefix
        /// </summary>
        /// <param name="cmlElement"></param>
        /// <param name="prefix"></param>
        /// <param name="uri"></param>
        /// <exception cref="ArgumentNullException">if prefix is null or empty or if uri is null</exception>
        /// <exception cref="NullReferenceException">if cmlElement does not have a delegate element</exception>
        public static void AddNamespaceDeclaration(CmlElement cmlElement, string prefix, Uri uri)
        {
            XAttribute xAttribute = new XAttribute(XNamespace.Xmlns + prefix, uri);
            cmlElement.DelegateElement.Add(xAttribute);
        }

        /// <summary>
        /// Adds a namespace declaration to cmlElement and binds the uri to the given prefix
        /// </summary>
        /// <param name="cmlElement">the element to add the namespace declaration to</param>
        /// <param name="prefix">The prefix to bind to the uri. This should not be null or the empty string</param>
        /// <param name="uri">The uri to bind to the prefix</param>
        /// <exception cref="UriFormatException">If the uri is not valid</exception>
        public static void AddNamespaceDeclaration(CmlElement cmlElement, string prefix, string uri)
        {
            Uri uri1 = new Uri(uri);
            AddNamespaceDeclaration(cmlElement, prefix, uri1);
        }

        /// <summary>
        /// removes delegate from its parent delegate
        /// convenience method
        /// user is responsible for keeping track 
        /// </summary>
        /// <param name="cmlElement">the element to remove</param>
        /// <exception cref="ArgumentNullException">if cmlElement is null</exception>
        public static void Remove(CmlElement cmlElement)
        {
            if (cmlElement == null)
            {
                throw new ArgumentNullException("cmlElement");
            }
            if (cmlElement.DelegateElement.Parent != null)
            {
                cmlElement.DelegateElement.Remove();
            }
        }

        /// <summary>
        /// gets centroid of subcentroids
        /// </summary>
        /// <param name="subCentroids"></param>
        /// <returns>null if list is null or any subcentroids are null</returns>
        public static Point2 GetCentroid(ICollection<IHasCentroid2> subCentroids)
        {
            Point2 centroid = null;
            if (subCentroids != null && subCentroids.Count > 0)
            {
                centroid = new Point2(0.0D, 0.0D);
                for (int i = 0; i < subCentroids.Count; i++)
                {
                    IHasCentroid2 subCentroid = subCentroids.ElementAt(i);
                    if (subCentroid == null)
                    {
                        centroid = null;
                        break;
                    }
                    Point2 point = subCentroid.GetCentroid();
                    if (point == null)
                    {
                        centroid = null;
                        break;
                    }
                    centroid = centroid.Add(point);
                }
            }
            if (centroid != null)
            {
                centroid = centroid.MultiplyBy(1.0/subCentroids.Count);
            }
            return centroid;
        }

        public static CmlCml GetEldestCmlCmlElement(XNode xNode)
        {
            XElement xElement =
                xNode.XPathSelectElement("//*[local-name()='" + CmlCml.Tag + "' and namespace-uri()='" +
                                         CmlConstants.Cmlns + "'][1]");
            return CmlCml.IsCml(xElement) ? new CmlCml(xElement) : null;
        }

        /// <summary>
        /// gets all descendant molecules whose parents are not molecules
        /// molecules can be nested if there are intervening containing elements
        /// </summary>
        /// <param name="pointer"></param>
        /// <returns>list of molecules</returns>
        internal static IEnumerable<CmlMolecule> GetAllToplevelDescendentMolecules(XElement pointer)
        {
            List<CmlMolecule> moleculeList = new List<CmlMolecule>();
            if (pointer != null)
            {
                // self == molecule
                if (pointer.Name.LocalName.Equals(CmlMolecule.Tag) &&
                    pointer.Name.Namespace.NamespaceName.Equals(CmlConstants.Cmlns))
                {
                    moleculeList.Add(new CmlMolecule(pointer));
                }
                else
                {
                    IEnumerable<XElement> descendants = pointer.XPathSelectElements(
                        ".//*[local-name()='" + CmlMolecule.Tag + "' and namespace-uri()='" + CmlConstants.Cmlns +
                        "' and  not(parent::*[local-name()='" + CmlMolecule.Tag + "' and namespace-uri()='" +
                        CmlConstants.Cmlns + "'])]");
                    foreach (XElement element in descendants)
                    {
                        moleculeList.Add(new CmlMolecule(element));
                    }
                }
            }
            return moleculeList;
        }

        /// <summary>
        /// gets fully qualified name of element in relation to root
        /// </summary>
        /// <param name="xelement"></param>
        /// <returns></returns>
        public static string GenerateFullPath(XElement xelement)
        {
            string name = "*[local-name()='" + xelement.Name.LocalName + "' and namespace-uri()='" +
                          xelement.Name.NamespaceName + "']";
            int position = xelement.ElementsBeforeSelf(xelement.Name.Namespace + xelement.Name.LocalName).Count();
            if (xelement.Document.Root == xelement)
            {
                return "/" + name + "[" + (1 + position) + "]";
            }
            return GenerateFullPath(xelement.Parent) + "/" + name + "[" + (1 + position) + "]";
        }

        public static void AnnotateOutdated(CmlElement cmlElement)
        {
            if (cmlElement == null)
            {
                throw new ArgumentNullException("cmlElement");
            }
            if (!IsOutdated(cmlElement))
            {
                string prefix = cmlElement.DelegateElement.GetPrefixOfNamespace(CmlConstants.CmlxNS);
                if (prefix == null)
                {
                    AddNamespaceDeclaration(cmlElement, CmlConstants.CmlxPrefix, CmlConstants.CmlxNS);
                }
                cmlElement.DelegateElement.Add(new XAttribute(CmlConstants.CmlxXNamespace + CmlConstants.Outdated,
                                                              "true"));
            }
        }

        public static void RemoveOutdated(CmlElement cmlElement)
        {
            if (cmlElement == null)
            {
                throw new ArgumentNullException("cmlElement");
            }
            XAttribute outdated =
                cmlElement.DelegateElement.Attribute(CmlConstants.CmlxXNamespace + CmlConstants.Outdated);
            if (outdated != null)
            {
                outdated.Remove();
            }
        }

        public static bool IsOutdated(CmlElement cmlElement)
        {
            XAttribute outdated =
                cmlElement.DelegateElement.Attribute(CmlConstants.CmlxXNamespace + CmlConstants.Outdated);
            return outdated != null;
        }

        /// <summary>
        /// gets prefix from qname
        /// </summary>
        /// <param name="qname">of form p:l or l where p is prefix and l is label</param>
        /// <returns>null if no colon else p</returns>
        public static string GetPrefix(string qname)
        {
            string prefix = null;
            if (qname != null)
            {
                int idx = qname.IndexOf(CmlConstants.SColon, StringComparison.OrdinalIgnoreCase);
                if (idx >= 0)
                {
                    prefix = qname.Substring(0, idx);
                }
            }
            return prefix;
        }

        /// <summary>
        /// gets localname from qname
        /// </summary>
        /// <param name="qname">of form p:l or l where p is prefix and l is label</param>
        /// <returns>l</returns>
        public static string GetLocalName(string qname)
        {
            string localname = qname;
            if (qname != null)
            {
                int idx = qname.IndexOf(CmlConstants.SColon, StringComparison.OrdinalIgnoreCase);
                if (idx >= 0)
                {
                    localname = qname.Substring(idx + 1);
                }
            }
            return localname;
        }

        internal static bool DoPointersFormImmediateMolecule(IEnumerable<XElement> atomPointers)
        {
            bool immediateMolecule = false;
            if (atomPointers != null)
            {
                if (atomPointers.Count() > 1)
                {
                    CmlMolecule molecule = GetFirstCommonAncestorMolecule(atomPointers);
                    if (molecule != null)
                    {
                        atomPointers =
                            CmlAtom.GetAtomPointers(CmlMolecule.AddBondedHydrogensToGroup(CmlAtom.GetAtoms(atomPointers)));
                        HashSet<XElement> atomPointerSet = new HashSet<XElement>();

                        foreach (XElement atomPointer in atomPointers)
                        {
                            atomPointerSet.Add(atomPointer);
                        }
                        IEnumerable<CmlAtom> atoms = molecule.GetAllAtoms();
                        if (atomPointerSet.Count() == atoms.Count())
                        {
                            immediateMolecule = true;
                            foreach (CmlAtom atom in atoms)
                            {
                                if (!atomPointerSet.Contains(atom.DelegateElement))
                                {
                                    immediateMolecule = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return immediateMolecule;
        }

        ///// <summary>
        ///// reads a resource into an XMLDocument
        ///// the Resource denotes a byte[].
        ///// so typical call is ReadDocumentFromResource(MyAssembly.Properties.Resources.aminoacids)
        ///// </summary>
        ///// <param name="bytes"></param>
        ///// <returns></returns>
        //public static XDocument ReadDocumentFromResource(byte[] bytes)
        //{
        //    String ss = new StreamReader(new MemoryStream(bytes), true).ReadToEnd();
        //    XDocument document = null;
        //    try
        //    {
        //        document = XDocument.Parse(ss);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Cannot parse XML file:" + ss + ":", e);
        //    }
        //    return document;
        //}

        /// <summary>
        ///  remove all child labels
        /// </summary>
        /// <param name="element"></param>
        internal static void RemoveAllChildLabels(CmlElement element)
        {
            IEnumerable<XElement> labels = GetChildLabels(element.DelegateElement);
            foreach (XElement label in labels)
            {
                label.Remove();
            }
        }

        internal static IEnumerable<XElement> GetChildLabels(XElement pointer)
        {
            IEnumerable<XElement> labels =
                pointer.XPathSelectElements("./*[local-name()='" + CmlLabel.Tag +
                                            "' and namespace-uri()='" + CmlConstants.Cmlns + "']");
            return labels;
        }

        /// <summary>
        /// gets ordered list of atom coordinates
        /// </summary>
        /// <param name="atoms"></param>
        /// <returns>never null; null coordinates for atoms without them</returns>
        public static IEnumerable<Point2> GetCoordinates2D(IEnumerable<CmlAtom> atoms)
        {
            List<Point2> points = new List<Point2>();
            if (atoms != null)
            {
                foreach (CmlAtom atom in atoms)
                {
                    points.Add(atom.Point2);
                }
            }
            return points;
        }

        /// <summary>
        /// gets ordered list of coordinates of atoms in bonds
        /// (p1, p2) ... (p1, p2) where points are at ends of bonds
        /// </summary>
        /// <param name="bonds"></param>
        /// <returns>never null; null coordinates for atoms without them</returns>
        public static IEnumerable<Point2> GetCoordinates2D(IEnumerable<CmlBond> bonds)
        {
            List<Point2> points = new List<Point2>();
            if (bonds != null)
            {
                foreach (CmlBond bond in bonds)
                {
                    CmlAtom atom0 = bond.GetAtoms().ElementAt(0);
                    points.Add(atom0.Point2);
                    CmlAtom atom1 = bond.GetAtoms().ElementAt(1);
                    points.Add(atom1.Point2);
                }
            }
            return points;
        }
    }
}