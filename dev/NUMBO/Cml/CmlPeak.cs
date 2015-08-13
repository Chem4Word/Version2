// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Globalization;
using System.Xml.Linq;
using Chem4Word.Common.Utilities;

namespace Numbo.Cml
{
    /// <summary>
    /// supports CML peak element
    /// </summary>
    public class CmlPeak : CmlElement
    {
        public const string Tag = "peak";

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlPeak()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlPeak(XElement element)
            : base(element)
        {}

        public double XValue
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.XValue) != null)
                           ? SafeDoubleParser.Parse(DelegateElement.Attribute(CmlAttribute.XValue).Value)
                           : Double.NaN;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.XValue, value.ToString(CultureInfo.InvariantCulture)); }
        }

        public double YValue
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.YValue) != null)
                           ? SafeDoubleParser.Parse(DelegateElement.Attribute(CmlAttribute.YValue).Value)
                           : Double.NaN;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.YValue, value.ToString(CultureInfo.InvariantCulture)); }
        }

        public double XMax
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.XMax) != null)
                           ? SafeDoubleParser.Parse(DelegateElement.Attribute(CmlAttribute.XMax).Value)
                           : Double.NaN;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.XMax, value.ToString(CultureInfo.InvariantCulture)); }
        }

        public double XMin
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.XMin) != null)
                           ? SafeDoubleParser.Parse(DelegateElement.Attribute(CmlAttribute.XMin).Value)
                           : Double.NaN;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.XMin, value.ToString(CultureInfo.InvariantCulture)); }
        }

        public string PeakMultiplicity
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.PeakMultiplicity) != null)
                           ? DelegateElement.Attribute(CmlAttribute.PeakMultiplicity).Value
                           : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.PeakMultiplicity, value); }
        }

        public override string GetTag()
        {
            return Tag;
        }
    }
}