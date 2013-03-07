// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Globalization;
using System.Xml.Linq;

namespace Numbo.Cml
{
    /// <summary>
    /// Supports all attributes and types in dotNUMBO
    /// mainly static methods
    /// </summary>
    public abstract class CmlAttribute
    {
        public const string AtomRefs = "atomRefs";
        public const string AtomRefs2 = "atomRefs2";
        public const string AtomRefs4 = "atomRefs4";
        public const string BondRefs = "bondRefs";
        public const string Chirality = "chirality";

        public const string Concise = "concise";
        public const string Convention = "convention";

        public const string Count = "count";
        public const string DateType = "dataType";
        public const string DictRef = "dictRef";
        public const string ElementType = "elementType";

        public const string FormalCharge = "formalCharge";

        public const string ID = "id";
        public const string Inline = "inline";
        public const string IsotopeNumber = "isotopeNumber";
        public const string Max = "max";
        public const string Min = "min";
        public const string Namespace = "namespace";
        public const string Order = "order";
        public const string PeakMultiplicity = "peakMultiplicity";
        public const string PeakShape = "peakShape";
        public const string Ref = "ref";

        public const string SpinMultiplicity = "spinMultiplicity";
        public const string Term = "term";
        public const string Title = "title";
        public const string Units = "units";

        public const string Value = "value";
        public const string Version = "version";
        public const string X2 = "x2";
        public const string X3 = "x3";
        public const string XMax = "xMax";
        public const string XMin = "xMin";
        public const string XUnits = "xUnits";
        public const string XValue = "xValue";
        public const string Y2 = "y2";
        public const string Y3 = "y3";
        public const string YUnits = "yUnits";
        public const string YValue = "yValue";
        public const string Z3 = "y3";
        // ... not complete

        ///<summary>
        ///</summary>
        ///<param name="delegateElement"></param>
        ///<param name="value"></param>
        public static void SetFormalCharge(XElement delegateElement, int? value)
        {
            if (value == null)
            {
                // remove attribute
                XAttribute attribute = delegateElement.Attribute(FormalCharge);
                if (attribute != null)
                {
                    attribute.Remove();
                }
            }
            else
            {
                delegateElement.SetAttributeValue(FormalCharge, value.Value);
            }
        }

        public static int? GetFormalCharge(XElement delegateElement)
        {
            XAttribute chargeAttribute = delegateElement.Attribute(FormalCharge);
            int? ch = null;
            if (chargeAttribute != null && chargeAttribute.Value != null)
            {
                string v = chargeAttribute.Value;
                ch = (string.Empty.Equals(v)) ? 0 : int.Parse(v, CultureInfo.InvariantCulture);
            }
            return ch;
        }
    }
}