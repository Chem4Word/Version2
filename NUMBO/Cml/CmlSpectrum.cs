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
    /// supports CML spectrum element
    /// </summary>
    public class CmlSpectrum : CmlElement
    {
        public const string Tag = "spectrum";

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlSpectrum()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlSpectrum(XElement element)
            : base(element)
        {}

        public override string GetTag()
        {
            return Tag;
        }

        public CmlPeakList GetPeakList()
        {
            CmlPeakList peakList = null;
            return peakList;
        }
    }
}