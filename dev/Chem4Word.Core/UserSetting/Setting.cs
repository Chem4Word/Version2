// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.
//  This software is released under the Apache License, Version 2.0.
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
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
        /// Enable the gallery
        /// </summary>
        public static bool UseGallery { get; set; }

        /// <summary>
        /// Load Settings from file
        /// </summary>
        /// <param name="settingsFile"></param>
        public static void LoadSettings(string settingsFile)
        {
            //string module = "Setting.LoadSettings()";

            if (File.Exists(settingsFile))
            {
                XDocument userSetting = XDocument.Load(settingsFile);
                bool saveRequired = false;
                try
                {
                    string importOption = userSetting.Root.Element("importOption").Attribute("value").Value;
                    Import = (ImportSetting)Enum.Parse(typeof(ImportSetting), importOption, true);
                }
                catch
                {
                    Import = ImportSetting.Auto;
                    saveRequired = true;
                }

                try
                {
                    bool useGallery;
                    bool.TryParse(userSetting.Root.Element("useGallery").Attribute("value").Value,
                                  out useGallery);
                    UseGallery = useGallery;
                }
                catch
                {
                    UseGallery = true;
                    saveRequired = true;
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
                    saveRequired = true;
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
                    saveRequired = true;
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
                    saveRequired = true;
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
                    saveRequired = true;
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
                    saveRequired = true;
                }

                if (saveRequired)
                {
                    SaveSettings(settingsFile);
                }
            }
            else
            {
                SetDefaultsAndSave(settingsFile);
            }
        }

        private static void SetDefaultsAndSave(string settingsFile)
        {
            // File does not exist; Set defaults
            Import = ImportSetting.Auto;
            DocumentPreferedDepiction = DocPreferedDepiction.TwoD;
            NavigatorPreferedDepiction = NavPreferedDepiction.ConciseFormula;
            UseGallery = true;
            CollapseNavigatorDepiction = true;
            RenderAtomsInColour = true;
            RenderImplicitHydrogens = true;

            // Save file
            SaveSettings(settingsFile);
        }

        /// <summary>
        /// Save Settings to file
        /// </summary>
        /// <param name="settingsFile"></param>
        public static void SaveSettings(string settingsFile)
        {
            //string module = "Setting.SaveSettings()";

            using (XmlWriter xw = XmlWriter.Create(settingsFile))
            {
                XDocument userSetting = new XDocument(
                    new XElement("userSetting",
                        new XElement("importOption",
                                    new XAttribute("value", Import.ToString())),
                        new XElement("documentPreferedDepiction",
                                    new XAttribute("value", DocumentPreferedDepiction.ToString())),
                        new XElement("useGallery",
                                    new XAttribute("value", UseGallery.ToString())),
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

    /// <summary>
    /// 
    /// </summary>
    public enum ImportSetting
    {
        /// <summary>
        /// 
        /// </summary>
        Auto,
        /// <summary>
        /// 
        /// </summary>
        Prompt,
        /// <summary>
        /// 
        /// </summary>
        StrictFail
    }

    /// <summary>
    /// 
    /// </summary>
    public enum DocPreferedDepiction
    {
        /// <summary>
        /// 
        /// </summary>
        TwoD,
        /// <summary>
        /// 
        /// </summary>
        ConciseFormula,
        /// <summary>
        /// 
        /// </summary>
        InlineFormula,
        /// <summary>
        /// 
        /// </summary>
        ChemicalName,
        /// <summary>
        /// 
        /// </summary>
        BoldNumber
    }

    /// <summary>
    /// 
    /// </summary>
    public enum NavPreferedDepiction
    {
        /// <summary>
        /// 
        /// </summary>
        ConciseFormula,
        /// <summary>
        /// 
        /// </summary>
        InlineFormula,
        /// <summary>
        /// 
        /// </summary>
        ChemicalName,
        /// <summary>
        /// 
        /// </summary>
        BoldNumber
    }
}