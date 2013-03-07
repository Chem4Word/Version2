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
    /// supports CML entry element
    /// </summary>
    public class CmlEntry : CmlElement
    {
        public const string Tag = "entry";

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlEntry()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlEntry(XElement element)
            : base(element)
        {}

        public string Term
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.Term) != null)
                           ? DelegateElement.Attribute(CmlAttribute.Term).Value
                           : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Term, value); }
        }

        public override string GetTag()
        {
            return Tag;
        }
    }
}