// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Runtime.CompilerServices;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("NUMBOTest")]

namespace Numbo.Cml
{
    /// <summary>
    /// Commonly required constant values for CML
    /// </summary>
    public class CmlConstants
    {
        public const string AddElectron = "+e-";
        public const string AddHdot = "+H.";
        public const string AddProton = "+H+";

        ///<summary>
        /// The CML dictionary namespace.
        /// This is the base dictionary which nodes not contain many specific
        /// entries (ie it defines things like melting point and molecular mass)
        ///</summary>
        public const string CmlDictNS = "http://www.xml-cml.org/dictionary/cml/";

        /// <summary>
        /// The prefix associated with the CML dictionary.
        /// </summary>
        public const string CmlDictPrefix = "cmlDict";

        ///<summary>
        /// The CML namespace
        ///</summary>
        public const string Cmlns = "http://www.xml-cml.org/schema";

        ///<summary>
        /// The CMLX dictionary namespace.
        /// Uncontrolled (semi-controlled) vocabulary for CML extensions
        ///</summary>
        public const string CmlxNS = "http://www.xml-cml.org/dictionary/cmlx/";

        /// <summary>
        /// The prefix associated with the CMLX .
        /// </summary>
        public const string CmlxPrefix = "cmlx";

        /// <summary>
        /// The cmlx attribute indicating warnings
        /// </summary>
        public const string CmlxWarning = "warning";

        ///<summary>
        /// The tolerance to use for double comparison (1.0E-10)
        ///</summary>
        public const double Epsilon = 1.0E-10;

        /// <summary>
        /// cmlx attribute local name indicating change was forced rather than suggested
        /// </summary>
        public const string ForcedChange = "forcedChange";

        ///<summary>
        /// The Name dictionary namespace.
        ///</summary>
        public const string NameDictNS = "http://www.xml-cml.org/dictionary/cml/name/";

        /// <summary>
        /// The prefix associated with the name dictionary.
        /// </summary>
        public const string NameDictPrefix = "nameDict";

        public const string RemoveElectron = "-e-";
        public const string RemoveHdot = "-H.";
        public const string RemoveProton = "-H+";
        public const string SColon = ":";
        
        ///<summary>
        /// The CML namesapce as an XNamespace to use in LINQ
        ///</summary>
        public static readonly XNamespace CmlxNamespace = Cmlns;

        /// <summary>
        /// The CMLX Xnamespace for Linq
        /// </summary>
        public static readonly XNamespace CmlxXNamespace = CmlxNS;

        public static readonly string Outdated = "outdated";

        /// <summary>
        /// Dummy Constructor to avoid public constructor initialization.
        /// </summary>
        private CmlConstants()
        {}
    }
}