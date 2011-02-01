// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Globalization;
using System.Xml.Linq;

namespace Numbo.Cml
{
    /// <summary>
    /// supports CML scalar element
    /// </summary>
    public class CmlScalar : CmlElement
    {
        public const string Tag = "scalar";

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlScalar()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlScalar(XElement element)
            : base(element)
        {}

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlScalar(int i)
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {
            this.DelegateElement.Value = "" + i;
            this.DelegateElement.SetAttributeValue(CmlAttribute.DateType, XsdInteger);
        }

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlScalar(double d)
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {
            this.DelegateElement.Value = "" + d;
            this.DelegateElement.SetAttributeValue(CmlAttribute.DateType, XsdDouble);
        }

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlScalar(string s)
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {
            this.DelegateElement.Value = s;
            this.DelegateElement.SetAttributeValue(CmlAttribute.DateType, XsdString);
        }

        public string DataType
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.DateType) != null)
                           ? DelegateElement.Attribute(CmlAttribute.DateType).Value
                           : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.DateType, value); }
        }

        public string Units
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.Units) != null)
                           ? DelegateElement.Attribute(CmlAttribute.Units).Value
                           : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Units, value); }
        }

        public double Max
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.Max) != null)
                           ? Double.Parse(DelegateElement.Attribute(CmlAttribute.Max).Value,
                                          CultureInfo.InvariantCulture)
                           : Double.NaN;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Max, value.ToString(CultureInfo.InvariantCulture)); }
        }

        public double Min
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.Min) != null)
                           ? Double.Parse(DelegateElement.Attribute(CmlAttribute.Min).Value,
                                          CultureInfo.InvariantCulture)
                           : Double.NaN;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Min, value.ToString(CultureInfo.InvariantCulture)); }
        }

        public override string GetTag()
        {
            return Tag;
        }
    }
}