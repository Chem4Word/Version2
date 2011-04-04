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
    /// supports CML bondArray element
    /// </summary>
    public class CmlBondArray : CmlElement
    {
        public const string Tag = "bondArray";

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlBondArray()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlBondArray(XElement element) : base(element)
        {}

        public override string GetTag()
        {
            return Tag;
        }
    }
}