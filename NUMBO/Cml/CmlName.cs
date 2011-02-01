// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Numbo.Cml
{
    /// <summary>
    /// supports CML name element
    /// </summary>
    public class CmlName : CmlElement
    {
        public const string Tag = "name";

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlName()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlName(XElement element) : base(element)
        {}

        public string DictRef
        {
            get
            {
                XAttribute dictRef = DelegateElement.Attribute(CmlAttribute.DictRef);
                return (dictRef == null) ? null : dictRef.Value;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.DictRef, value); }
        }

        /// <summary>
        /// get/ set Convention
        /// </summary>
        public string Convention
        {
            get
            {
                XAttribute att = DelegateElement.Attribute(CmlAttribute.Convention);
                return (att == null) ? null : att.Value;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Convention, value); }
        }

        public override string GetTag()
        {
            return Tag;
        }

        public static bool IsValidNameValue(string text)
        {
            bool ok = false;
            if (text != null)
            {
                // no spaces ... now check that there is actually some content
                Regex contentRegex = new Regex(".+");
                ok = contentRegex.IsMatch(text);
            }
            return ok;
        }

        public string GetValue()
        {
            return DelegateElement.Value;
        }

        public void SetValue(string value)
        {
            DelegateElement.Value = value;
        }

        /// <summary>
        /// Determines whether the argument is a CMLLite name
        /// 
        /// at the moment this only test the name and namespace - in future
        /// this will check more
        /// </summary>
        /// <param name="element">the XElement to test</param>
        /// <returns>true if element is a CMLName otherwise false</returns>
        public static bool IsName(XElement element)
        {
            XName xName = CmlConstants.CmlxNamespace + Tag;
            return xName.Equals(element.Name);
        }

        public void AnnotateOutdated()
        {
            CmlUtils.AnnotateOutdated(this);
        }

        public void RemoveOutdated()
        {
            CmlUtils.RemoveOutdated(this);
        }

        public bool IsOutdated()
        {
            return CmlUtils.IsOutdated(this);
        }
    }
}