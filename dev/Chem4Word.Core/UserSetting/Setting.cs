// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
namespace Chem4Word.Core.UserSetting
{
    ///<summary>
    ///</summary>
    public class Setting
    {
        /// <summary>
        /// Private constructor for fxcop fix.
        /// </summary>
        private Setting()
        {}

        public static ImportSetting Import { get; set; }

        public static DocPreferedDepiction DocumentPreferedDepiction { get; set; }

        public static NavPreferedDepiction NavigatorPreferedDepiction { get; set; }

        public static bool CollapseNavigatorDepiction { get; set; }

        public static bool RenderAtomsInColour { get; set; }

        public static bool RenderImplicitHydrogens { get; set; }
    }

    public enum ImportSetting
    {
        Auto,
        Prompt,
        StrictFail
    }

    public enum DocPreferedDepiction
    {
        TwoD,
        ConciseFormula,
        InlineFormula,
        ChemicalName,
        BoldNumber
    }

    public enum NavPreferedDepiction
    {
        ConciseFormula,
        InlineFormula,
        ChemicalName,
        BoldNumber
    }
}