// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.
//  This software is released under the Apache License, Version 2.0.
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------

using System;
using System.Xml;
using System.Xml.Linq;

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
        {
        }

        /// <summary>
        /// Import cml error action
        /// </summary>
        public static ImportSetting Import { get; set; }

        /// <summary>
        /// Preferred document depiction
        /// </summary>
        public static DocPreferedDepiction DocumentPreferedDepiction { get; set; }

        /// <summary>
        /// Preferred navigator depiction
        /// </summary>
        public static NavPreferedDepiction NavigatorPreferedDepiction { get; set; }

        /// <summary>
        /// Navigator collapsed or expanded at start up
        /// </summary>
        public static bool CollapseNavigatorDepiction { get; set; }

        /// <summary>
        /// Render atoms in colour (OOXML)
        /// </summary>
        public static bool RenderAtomsInColour { get; set; }

        /// <summary>
        /// Render implicit hydrogens (OOXML)
        /// </summary>
        public static bool RenderImplicitHydrogens { get; set; }

        /// <summary>
        /// Load Settings from file
        /// </summary>
        /// <param name="settingsFile"></param>
        public static void LoadSettings(string settingsFile)
        {
            string module = "Setting.LoadSettings()";

            XDocument userSetting = XDocument.Load(settingsFile);

            try
            {
                string importOption = userSetting.Root.Element("importOption").Attribute("value").Value;
                Import = (ImportSetting)Enum.Parse(typeof(ImportSetting), importOption, true);
            }
            catch
            {
                Import = ImportSetting.Auto;
            }

            try
            {
                string documentPreferedDepiction =
                    userSetting.Root.Element("documentPreferedDepiction").Attribute("value").Value;
                DocumentPreferedDepiction = (DocPreferedDepiction)
                                                    Enum.Parse(typeof(DocPreferedDepiction),
                                                               documentPreferedDepiction, true);
            }
            catch
            {
                DocumentPreferedDepiction = DocPreferedDepiction.TwoD;
            }

            try
            {
                string navigatorPreferedDepiction =
                    userSetting.Root.Element("navigatorPreferedDepiction").Attribute("value").Value;
                NavigatorPreferedDepiction = (NavPreferedDepiction)
                                                     Enum.Parse(typeof(NavPreferedDepiction),
                                                                navigatorPreferedDepiction, true);
            }
            catch
            {
                NavigatorPreferedDepiction = NavPreferedDepiction.ConciseFormula;
            }

            try
            {
                bool collapseNavigatorDepiction;
                bool.TryParse(userSetting.Root.Element("collapseNavigatorDepiction").Attribute("value").Value,
                              out collapseNavigatorDepiction);
                CollapseNavigatorDepiction = collapseNavigatorDepiction;
            }
            catch
            {
                CollapseNavigatorDepiction = true;
            }

            try
            {
                bool ooXmlRenderAtomsInColour;
                bool.TryParse(userSetting.Root.Element("ooXmlRenderAtomsInColour").Attribute("value").Value,
                              out ooXmlRenderAtomsInColour);
                RenderAtomsInColour = ooXmlRenderAtomsInColour;
            }
            catch
            {
                RenderAtomsInColour = true;
            }

            try
            {
                bool ooXmlRenderImplicitHydrogens;
                bool.TryParse(userSetting.Root.Element("ooXmlRenderImplicitHydrogens").Attribute("value").Value,
                              out ooXmlRenderImplicitHydrogens);
                RenderImplicitHydrogens = ooXmlRenderImplicitHydrogens;
            }
            catch
            {
                RenderImplicitHydrogens = true;
            }
        }

        /// <summary>
        /// Save Settings to file
        /// </summary>
        /// <param name="settingsFile"></param>
        public static void SaveSettings(string settingsFile)
        {
            string module = "Setting.SaveSettings()";

            using (XmlWriter xw = XmlWriter.Create(settingsFile))
            {
                XDocument userSetting = new XDocument(
                    new XElement("userSetting",
                        new XElement("importOption",
                                    new XAttribute("value", Import.ToString())),
                        new XElement("documentPreferedDepiction",
                                    new XAttribute("value", DocumentPreferedDepiction.ToString())),
                        new XElement("navigatorPreferedDepiction",
                                    new XAttribute("value", NavigatorPreferedDepiction.ToString())),
                        new XElement("collapseNavigatorDepiction",
                                    new XAttribute("value", CollapseNavigatorDepiction.ToString())),
                        new XElement("ooXmlRenderAtomsInColour",
                                    new XAttribute("value", RenderAtomsInColour.ToString())),
                        new XElement("ooXmlRenderImplicitHydrogens",
                                    new XAttribute("value", RenderImplicitHydrogens.ToString()))
                ));

                userSetting.WriteTo(xw);
            }
        }
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