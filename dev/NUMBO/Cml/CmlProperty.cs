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
    /// supports CML property element
    /// </summary>
    public class CmlProperty : CmlElement
    {
        public const string Tag = "property";

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlProperty()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlProperty(XElement element)
            : base(element)
        {}

        /// <summary>
        /// get/ set DictRef
        /// </summary>
        public string DictRef
        {
            get
            {
                XAttribute att = DelegateElement.Attribute(CmlAttribute.DictRef);
                return (att == null) ? null : att.Value;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.DictRef, value); }
        }

        public override string GetTag()
        {
            return Tag;
        }
    }
}