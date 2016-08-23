// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Windows;
using System.Xml.Linq;
using log4net;

namespace Numbo.Coa
{
    /// <summary>
    /// A container for CML and ChemSS (global and ChemistryZone specific)
    /// </summary>
    public class ContextObject : ICloneable
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(ContextObject));

        ///<summary>
        ///</summary>
        ///<param name="cml"></param>
        public ContextObject(XDocument cml)
        {
            Cml = cml;
            StubScaleFactor = 1f/3f;
        }

        ///<summary>
        ///</summary>
        public XDocument Cml { get; private set; }

        /// TODO define ChemSS
        //ChemSS GlobalChemSS {get { return this.globalChemSS ; }}
        //ChemSS CZChemSS {get { return this.czChemSS ; }}
        /// <summary>
        /// TODO should come from ChemSS 
        /// </summary>
        public double StubScaleFactor { get; set; }

        //TODO: Just a stub. We need to be able to change this on a per chemzone basis.
        public bool DisplayHydrogenIsotopeNumber { get; set; }

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }

        public ContextObject Clone()
        {
            var newObj = new ContextObject(XDocument.Parse(Cml.ToString()))
                                       {
                                           DisplayHydrogenIsotopeNumber = DisplayHydrogenIsotopeNumber,
                                             ViewBoxDimensions = ViewBoxDimensions
                                       };
            return newObj;
        }

        public Rect ViewBoxDimensions { get; set; }
    }
}