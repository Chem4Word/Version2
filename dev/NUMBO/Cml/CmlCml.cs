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
    /// supports CML cml element
    /// </summary>
    public class CmlCml : CmlElement
    {
        public const string Tag = "cml";

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlCml()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  copy constructor
        /// </summary>
        public CmlCml(XElement element) : base(element)
        {}

        /// <summary>
        /// version
        /// </summary>
        public string Version
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.Version) != null)
                           ? DelegateElement.Attribute(CmlAttribute.Version).Value
                           : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Version, value); }
        }

        /// <summary>
        /// convention
        /// </summary>
        public string Convention
        {
            get { return DelegateElement.Attribute(CmlAttribute.Convention).Value; }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Convention, value); }
        }

        public override string GetTag()
        {
            return Tag;
        }

        public static bool IsCml(XElement element)
        {
            XName xName = CmlConstants.CmlxNamespace + Tag;
            return xName.Equals(element.Name);
        }
    }
}