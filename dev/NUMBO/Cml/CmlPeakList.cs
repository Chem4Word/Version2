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
    /// supports CML peakList element
    /// </summary>
    public class CmlPeakList : CmlElement
    {
        public const string Tag = "peakList";

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlPeakList()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlPeakList(XElement element) : base(element)
        {}

        public string XUnits
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.XUnits) != null)
                           ? DelegateElement.Attribute(CmlAttribute.XUnits).Value
                           : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.XUnits, value); }
        }

        public string YUnits
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.YUnits) != null)
                           ? DelegateElement.Attribute(CmlAttribute.YUnits).Value
                           : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.YUnits, value); }
        }

        public override string GetTag()
        {
            return Tag;
        }
    }
}