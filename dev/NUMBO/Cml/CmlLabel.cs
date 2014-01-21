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
    /// supports CML label element
    /// typical examples are atom labels and bold label for molecule
    /// </summary>
    public class CmlLabel : CmlElement
    {
        public const string MoleculeBoldNumber = "moleculeBoldNo";
        public const string Tag = "label";

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlLabel()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlLabel(XElement element) : base(element)
        {}

        ///<summary>
        /// The dictRef property on Label
        ///</summary>
        public string DictRef
        {
            get
            {
                XAttribute a = DelegateElement.Attribute(CmlAttribute.DictRef);
                return (a != null) ? a.Value : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.DictRef, value); }
        }

        /// <summary>
        /// The value property on Label
        /// </summary>
        public string Value
        {
            get { return DelegateElement.Attribute(CmlAttribute.Value).Value; }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Value, value); }
        }

        public override string GetTag()
        {
            return Tag;
        }

        /// <summary>
        /// gets groupType as prefix of label.Value with dictRef = cmlx:group
        /// </summary>
        /// <returns></returns>
        public string GetDictionaryType()
        {
            return CmlUtils.GetPrefix(Value);
        }

        /// <summary>
        /// gets groupValue as localname of label.Value with dictRef = cmlx:group
        /// </summary>
        /// <returns>group name or null</returns>
        public string GetGroupName()
        {
            return CmlUtils.GetLocalName(Value);
        }

        /// <summary>
        /// Determines whether the argument is a CMLLite label
        /// </summary>
        /// <param name="element">the XElement to test</param>
        /// <returns>true if element is a CMLLabel otherwise false</returns>
        public static bool IsLabel(XElement element)
        {
            XName cmlLabelXName = CmlConstants.CmlxNamespace + Tag;
            return cmlLabelXName.Equals(element.Name);
        }

        /// <summary>
        /// Checks the value of a label to determine if it is valid.
        /// labels may not contain whitespace
        /// </summary>
        /// <param name="value">the value to check</param>
        /// <returns>true if valid, false otherwise</returns>
        public static bool IsLabelValueValid(string value)
        {
            bool ok = false;
            if (value != null)
            {
                Regex spaceRegex = new Regex("\\s+");

                if (spaceRegex.IsMatch(value))
                {
                    ok = false;
                }
                else
                {
                    // no spaces ... now check that there is actually some content
                    Regex contentRegex = new Regex(".+");
                    ok = contentRegex.IsMatch(value);
                }
            }
            return ok;
        }

        public bool IsOutdated()
        {
            return CmlUtils.IsOutdated(this);
        }

        public void AnnotateOutdated()
        {
            CmlUtils.AnnotateOutdated(this);
        }

        public void RemoveOutdated()
        {
            CmlUtils.RemoveOutdated(this);
        }
    }
}