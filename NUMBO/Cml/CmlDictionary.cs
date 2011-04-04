// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Xml.Linq;

namespace Numbo.Cml
{
    /// <summary>
    /// supports CML dictionary element
    /// </summary>
    public class CmlDictionary : CmlElement
    {
        public const string Tag = "dictionary";

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlDictionary()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlDictionary(XElement element) : base(element)
        {}

        public string Namespace
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.Namespace) != null)
                           ? DelegateElement.Attribute(CmlAttribute.Namespace).Value
                           : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Namespace, value); }
        }

        public override string GetTag()
        {
            return Tag;
        }
    }
}