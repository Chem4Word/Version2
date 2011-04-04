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
    /// supports CML peakStructure element
    /// </summary>
    public class CmlPeakStructure : CmlElement
    {
        public const string Tag = "peakStructure";

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlPeakStructure()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlPeakStructure(XElement element)
            : base(element)
        {}

        public string PeakShape
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.PeakShape) != null)
                           ? DelegateElement.Attribute(CmlAttribute.PeakShape).Value
                           : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.PeakShape, value); }
        }

        public override string GetTag()
        {
            return Tag;
        }
    }
}