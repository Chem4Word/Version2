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
using System.Xml.XPath;
using log4net;

namespace Numbo.Cml
{
    /// <summary>
    /// superclass of all CML element classes
    /// this class delegates to an XElement
    /// </summary>
    public abstract class CmlElement
    {
        // TODO change these to cmlx later
        private const string Deleted = "deleted";
        private const string True = "true";
        protected const string XsdDouble = "xsd:double";

        ///<summary>
        ///</summary>
        protected const string XsdInteger = "xsd:integer";

        protected const string XsdString = "xsd:string";
        private static readonly ILog Log = LogManager.GetLogger(typeof (CmlElement));

        /// <summary>
        ///  formed from XElement reference
        /// </summary>)
        /// <exception cref="ArgumentNullException">if element is null</exception>
        /// <exception cref="ArgumentException">if element name does not match tag</exception>
        protected CmlElement(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            DelegateElement = element;
            if (!CmlConstants.CmlxNamespace.Equals(DelegateElement.Name.Namespace))
            {
                throw new ArgumentException("must be from CML namespace");
            }
            if (!DelegateElement.Name.LocalName.Equals(GetTag()))
            {
                throw new ArgumentException("inconsistent local name: " + DelegateElement.Name.LocalName +
                                            " != " +
                                            GetTag());
            }
        }

        /// <summary>
        ///  copy constructor
        /// </summary>)
        /// <exception cref="ArgumentNullException">if element is null</exception>
        /// <exception cref="ArgumentException">if element name does not match tag</exception>
        protected CmlElement(CmlElement cmlElement)
        {
            DelegateElement = new XElement(cmlElement.DelegateElement);
        }

        public XElement DelegateElement { get; private set; }

        /// <summary>
        /// id is here beacuse it could be on everything
        /// </summary>
        public string Id
        {
            get
            {
                XAttribute att = DelegateElement.Attribute(CmlAttribute.ID);
                return (att == null) ? null : att.Value;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentOutOfRangeException("value", "the value of an id attribute must not be null or empty");
                }
                DelegateElement.SetAttributeValue(CmlAttribute.ID, value);
            }
        }
        /// <summary>
        /// get first child of given name convenience method
        /// </summary>
        /// <param name="localName"></param>
        /// <returns>CMLElement of correct class</returns>
        public CmlElement GetFirstCmlChild(string localName)
        {
            IEnumerable<XElement> elems =
                from elem in DelegateElement.Descendants(CmlConstants.CmlxNamespace + localName)
                select elem;
            return elems.Count() == 0
                       ? null
                       : CreateElement(
                             DelegateElement.Elements(CmlConstants.CmlxNamespace + localName).First());
        }


        /// <summary>
        /// Gets the name of the cml element (this is the local name)
        /// </summary>
        /// <returns>the (local) name of the cml element</returns>
        public abstract string GetTag();

        /// <summary>
        /// creates element of appropriate subclass
        /// for CML namespace creates CMLFoo, for others XElement by default
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static CmlElement CreateElement(XElement element)
        {
            CmlElement cmlElement = null;
            string elementName = element.Name.LocalName;
            if (CmlAtom.Tag.Equals(elementName))
            {
                cmlElement = new CmlAtom(element);
            }
            else if (CmlAtomArray.Tag.Equals(elementName))
            {
                cmlElement = new CmlAtomArray(element);
            }
            else if (CmlAtomParity.Tag.Equals(elementName))
            {
                cmlElement = new CmlAtomParity(element);
            }
            else if (CmlBond.Tag.Equals(elementName))
            {
                cmlElement = new CmlBond(element);
            }
            else if (CmlBondArray.Tag.Equals(elementName))
            {
                cmlElement = new CmlBondArray(element);
            }
            else if (CmlBondStereo.Tag.Equals(elementName))
            {
                cmlElement = new CmlBondStereo(element);
            }
            else if (CmlCml.Tag.Equals(elementName))
            {
                cmlElement = new CmlCml(element);
            }
            else if (CmlDictionary.Tag.Equals(elementName))
            {
                cmlElement = new CmlDictionary(element);
            }
            else if (CmlEntry.Tag.Equals(elementName))
            {
                cmlElement = new CmlEntry(element);
            }
            else if (CmlFormula.Tag.Equals(elementName))
            {
                cmlElement = new CmlFormula(element);
            }
            else if (CmlLabel.Tag.Equals(elementName))
            {
                cmlElement = new CmlLabel(element);
            }
            else if (CmlMolecule.Tag.Equals(elementName))
            {
                cmlElement = new CmlMolecule(element);
            }
            else if (CmlName.Tag.Equals(elementName))
            {
                cmlElement = new CmlName(element);
            }
            else if (CmlPeak.Tag.Equals(elementName))
            {
                cmlElement = new CmlPeak(element);
            }
            else if (CmlPeakList.Tag.Equals(elementName))
            {
                cmlElement = new CmlPeakList(element);
            }
            else if (CmlPeakStructure.Tag.Equals(elementName))
            {
                cmlElement = new CmlPeakStructure(element);
            }
            else if (CmlProperty.Tag.Equals(elementName))
            {
                cmlElement = new CmlProperty(element);
            }
            else if (CmlScalar.Tag.Equals(elementName))
            {
                cmlElement = new CmlScalar(element);
            }
            else if (CmlSpectrum.Tag.Equals(elementName))
            {
                cmlElement = new CmlSpectrum(element);
            }
            else
            {
                throw new ArgumentOutOfRangeException("element", "Unknown CML element: " + elementName);
            }

            return cmlElement;
        }

        /// <summary>
        /// use XElement.equals to compare delegates
        /// </summary>
        /// <param name="cmlElement"></param>
        /// <returns></returns>
        public bool Equals(CmlElement cmlElement)
        {
            return this.DelegateElement.Equals(cmlElement.DelegateElement);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return DelegateElement.GetHashCode();
        }

        /// <summary>
        ///  removes delegateElementVariable from parent and may mark as deleted
        ///  this is a bad thing to do for atoms and bonds unless you know
        ///  what you are doing. We may make it private later
        /// </summary>
        public void DeleteSimple(bool mark)
        {
            if (IsDeleted())
            {
                Log.Debug("WARN: Trying to delete previously deleted CMLElement " + GetTag());
            }
            else
            {
                if (this.DelegateElement.Parent != null)
                {
                    this.DelegateElement.Remove();
                    if (mark)
                    {
                        MarkAsDeleted(this.DelegateElement);
                    }
                }
            }
        }

        public void CleanDescendantDeletedAttributes()
        {
            CleanDeletedAttribute(this.DelegateElement);
            IEnumerable<XElement> descendants = this.DelegateElement.XPathSelectElements(".//*");
            foreach (XElement descendant in descendants)
            {
                CleanDeletedAttribute(descendant);
            }
        }

        /// <summary>
        /// adds attribute of form cmlx:name="value"
        /// </summary>
        /// <param name="localName"></param>
        /// <param name="value"></param>
        internal void AddCmlxAttribute(string localName, string value)
        {
            XAttribute att = this.DelegateElement.Attribute(CmlConstants.CmlxXNamespace + localName);
            if (att != null)
            {
                att.Remove();
            }
            DelegateElement.Add(new XAttribute(CmlConstants.CmlxXNamespace + localName, value));
        }

        private void CleanDeletedAttribute(XElement element)
        {
            XAttribute att = element.Attribute(Deleted);
            if (att != null)
            {
                att.Remove();
            }
        }

        private void MarkAsDeleted(XElement delegateElement)
        {
            delegateElement.Add(new XAttribute(Deleted, True));
        }

        private bool IsDeleted()
        {
            bool deleted = false;
            XAttribute att = this.DelegateElement.Attribute(Deleted);
            if (att != null)
            {
                deleted = True.Equals(att.Value);
            }
            return deleted;
        }

        /// <summary>
        /// Returns the indented XML of the delegate element.
        /// </summary>
        /// <returns>The indented XML for this node</returns>
        public override string ToString()
        {
            return DelegateElement.ToString();
        }
    }
}