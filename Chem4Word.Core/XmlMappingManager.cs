// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Chem4Word.Api;
using Chem4Word.Api.Events;
using Microsoft.Office.Core;
using Office = Microsoft.Office.Core;

namespace Chem4Word.Core
{
    /// <summary>
    /// 
    /// </summary>
    internal class XmlMappingManager
    {
        private const string ChemistryZonePropertiesRootElementName = "ChemistryZone";
        private const string DocumentDepictionOptionXPathElementName = "DocumentDepictionOptionXPath";
        private const string NavigatorDepictionOptionXPathElementName = "NavigatorDepictionOptionXPath";
        private readonly ChemistryDocument chemistryDocument;

        /// <summary>
        /// Initializes a new instance of the Document class. <see cref="Chem4Word.Core.XmlMappingManager"/>
        /// </summary>
        /// <param name="chemistryDocument"></param>
        internal XmlMappingManager(ChemistryDocument chemistryDocument)
        {
            this.chemistryDocument = chemistryDocument;
            //Register Events
            this.chemistryDocument.ContentControlBeforeDelete += DocumentContentControlBeforeDelete;
        }

        private void DocumentContentControlBeforeDelete(object sender, ContentControlEventArgs e)
        {
            CustomXMLPart xPart;
            IChemistryZone oldZone = e.ContentChemistryZone;
            // If the deleted Chemistry Zone is the last control which bind to Custom Xml Part,
            // Remove the Custom Xml Part.
            if (GetOtherCommonBindingZones(oldZone).Count == 0)
            {
                string refValue = GetCmlRefValueByZone(oldZone);
                xPart = chemistryDocument.WordDocument.CustomXMLParts.SelectByID(refValue);
                if (xPart != null)
                {
                    xPart.Delete();
                }
            }
            // now remove the customXML which holds the chem zone properties
            xPart = GetChemZoneXPart(oldZone);
            if (xPart != null)
            {
                xPart.Delete();
            }
        }

        /// <summary>
        /// Called when the CML is set for a chemistry zone. 
        /// If this is the first time the CML is being set then there will not be any 
        /// properties set (ie document and navigator depiciton options will be null) 
        /// 
        /// </summary>
        /// <param name="chemZone"></param>
        /// <param name="cml"></param>
        internal void UpdateCmlByZone(IChemistryZone chemZone, XDocument cml)
        {
            string refValue = GetCmlRefValueByZone(chemZone);

            if (refValue == null)
            {
                UpdateZoneProperties(chemZone, null);
                UpdateCmlByZone(chemZone, cml);
            }
            else
            {
                if (refValue == string.Empty)
                {
                    object missing = Type.Missing;
                    CustomXMLPart xPart = chemistryDocument.WordDocument.CustomXMLParts.Add(cml.ToString(),
                                                                                            missing);
                    UpdateCmlRefByZone(chemZone, xPart.Id);
                }
                else
                {
                    CustomXMLPart xPart = chemistryDocument.WordDocument.CustomXMLParts.SelectByID(refValue);
                    if (xPart != null)
                    {
                        xPart.Delete();
                    }
                    object missing = Type.Missing;
                    xPart = chemistryDocument.WordDocument.CustomXMLParts.Add(cml.ToString(), missing);
                    // Collect all other Chemistry Zone that bind to the same CML
                    ICollection<IChemistryZone> otherChemZones = GetOtherCommonBindingZones(chemZone);
                    otherChemZones.Add(chemZone);
                    // Loop: rebind the collected Chemistry Zone to the CML
                    foreach (IChemistryZone chemZoneCursor in otherChemZones)
                    {
                        UpdateCmlRefByZone(chemZoneCursor, xPart.Id);
                        chemZoneCursor.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chemZone"></param>
        /// <param name="chemZoneProperties"></param>
        internal void UpdateZoneProperties(IChemistryZone chemZone, ChemistryZoneProperties chemZoneProperties)
        {
            if (chemZoneProperties == null)
            {
                // Construct Empty Chemistry Zone Xml Mapping
                XDocument newChemZonePropertiesDocument =
                    CreateChemZonePropertiesDocument(string.Empty, string.Empty, true, string.Empty,
                                                     chemZone.ContentControl.ID);

                CustomXMLPart xPart = GetChemZoneXPart(chemZone);
                if (xPart != null)
                {
                    xPart.Delete();
                }
                object missing = Type.Missing;
                chemistryDocument.WordDocument.CustomXMLParts.Add(newChemZonePropertiesDocument.ToString(), missing);
            }
            else
            {
                string refValue = GetCmlRefValueByZone(chemZone) + string.Empty;

                // Construct new Chemistry Zone Xml Mapping
                XDocument newChemZonePropertiesDocument =
                    CreateChemZonePropertiesDocument(chemZoneProperties.DocumentDepictionOptionXPath,
                                                     chemZoneProperties.NavigatorDepictionOptionXPath, chemZoneProperties.CollapseNavigatorDepiction, refValue,
                                                     chemZone.ContentControl.ID);

                CustomXMLPart xPart = GetChemZoneXPart(chemZone);
                if (xPart != null)
                {
                    xPart.Delete();
                }
                object missing = Type.Missing;
                chemistryDocument.WordDocument.CustomXMLParts.Add(newChemZonePropertiesDocument.ToString(), missing);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chemZone"></param>
        /// <returns></returns>
        internal ChemistryZoneProperties ReadZonePropertiesByZone(IChemistryZone chemZone)
        {
            string documentDepictionOptionXPath = string.Empty;
            string navigatorDepictionOptionXPath = string.Empty;
            bool collapseNavigatorDepition = false;
            CustomXMLPart xPart = GetChemZoneXPart(chemZone);
            if (xPart != null)
            {
                string xml = xPart.XML + string.Empty;
                XDocument xmlMapping = XDocument.Parse(xml);
                foreach (XElement element in xmlMapping.Root.Elements())
                {
                    switch (element.Name.LocalName)
                    {
                        case DocumentDepictionOptionXPathElementName:
                            documentDepictionOptionXPath = element.Attribute("value").Value;
                            break;
                        case NavigatorDepictionOptionXPathElementName:
                            navigatorDepictionOptionXPath = element.Attribute("value").Value;
                            bool.TryParse(element.Attribute("collapsed").Value, out collapseNavigatorDepition);
                            break;
                    }
                }
            }

            return new ChemistryZoneProperties(documentDepictionOptionXPath, navigatorDepictionOptionXPath, collapseNavigatorDepition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chemZone"></param>
        /// <returns></returns>
        internal XDocument ReadCmlByZone(IChemistryZone chemZone)
        {
            string refValue = GetCmlRefValueByZone(chemZone);

            if (refValue != string.Empty)
            {
                CustomXMLPart part = chemistryDocument.WordDocument.CustomXMLParts.SelectByID(refValue);
                return XDocument.Parse(part.XML);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chemZone"></param>
        /// <param name="refValue"></param>
        internal void UpdateCmlRefByZone(IChemistryZone chemZone, string refValue)
        {
            ChemistryZoneProperties chemZoneProperties = ReadZonePropertiesByZone(chemZone);
            // Construct new Chemistry Zone Xml Mapping
            XDocument newChemZonePropertiesDocument =
                CreateChemZonePropertiesDocument(chemZoneProperties.DocumentDepictionOptionXPath,
                                                 chemZoneProperties.NavigatorDepictionOptionXPath, chemZoneProperties.CollapseNavigatorDepiction, refValue,
                                                 chemZone.ContentControl.ID);

            CustomXMLPart xPart = GetChemZoneXPart(chemZone);
            if (xPart != null)
            {
                xPart.Delete();
            }
            object missing = Type.Missing;
            chemistryDocument.WordDocument.CustomXMLParts.Add(newChemZonePropertiesDocument.ToString(), missing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentDepictionOptionXPath"></param>
        /// <param name="navigatorDepictionOptionXPath"></param>
        /// <param name="collapseNavigatorDepiction">Should the image part of the navigator depcition be collapsed</param>
        /// <param name="refValue"></param>
        /// <param name="contentControlID"></param>
        /// <returns></returns>
        private XDocument CreateChemZonePropertiesDocument(string documentDepictionOptionXPath,
                                                           string navigatorDepictionOptionXPath, 
                                                           bool collapseNavigatorDepiction, string refValue,
                                                           string contentControlID)
        {
            XElement chemZonePropertiesElement = new XElement(ChemistryZonePropertiesRootElementName);
            XDocument chemZonePropertiesDocument = new XDocument(chemZonePropertiesElement);
            XElement documentDepictionXElement = new XElement(DocumentDepictionOptionXPathElementName);
            documentDepictionXElement.Add(new XAttribute("value", documentDepictionOptionXPath));
            chemZonePropertiesElement.Add(documentDepictionXElement);
            XElement navigatorDepictionXElement = new XElement(NavigatorDepictionOptionXPathElementName);
            navigatorDepictionXElement.Add(new XAttribute("value", navigatorDepictionOptionXPath));
            navigatorDepictionXElement.Add(new XAttribute("collapsed", collapseNavigatorDepiction.ToString()));
            chemZonePropertiesElement.Add(navigatorDepictionXElement);
            XElement refElement = new XElement("ref",
                                               new XAttribute("cml", refValue), new XAttribute("cc", contentControlID));
            chemZonePropertiesElement.Add(refElement);
            return chemZonePropertiesDocument;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chemZone"></param>
        /// <returns></returns>
        internal ICollection<IChemistryZone> GetOtherCommonBindingZones(IChemistryZone chemZone)
        {
            string refValue = GetCmlRefValueByZone(chemZone);

            // Collect all other Chemistry Zone that bind to the same CML
            List<IChemistryZone> otherChemZones = new List<IChemistryZone>();
            foreach (IChemistryZone chemZoneCursor in chemistryDocument)
            {
                CustomXMLPart xPart = GetChemZoneXPart(chemZoneCursor);
                CustomXMLNode xNode = xPart.SelectSingleNode("/ChemistryZone/ref/@cml");
                if (xNode != null)
                {
                    if (xNode.NodeValue == refValue && chemZoneCursor.ID != chemZone.ID)
                    {
                        otherChemZones.Add(chemZoneCursor);
                    }
                }
            }
            return otherChemZones;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chemZone"></param>
        /// <returns></returns>
        internal string GetCmlRefValueByZone(IChemistryZone chemZone)
        {
            string refVal = null;
            CustomXMLPart xPart = GetChemZoneXPart(chemZone);
            if (xPart != null)
            {
                CustomXMLNode xNode = xPart.SelectSingleNode("/ChemistryZone/ref/@cml");
                if (xNode != null)
                {
                    refVal = xNode.NodeValue;
                }
            }
            return refVal;
        }

        /// <summary>
        /// Get Custom XML which contain Chemistry Zone Properties.
        /// </summary>
        /// <param name="chemZone"></param>
        /// <returns></returns>
        private CustomXMLPart GetChemZoneXPart(IChemistryZone chemZone)
        {
            foreach (CustomXMLPart xPart in chemistryDocument.WordDocument.CustomXMLParts)
            {
                CustomXMLNode xNode = xPart.SelectSingleNode("/ChemistryZone/ref/@cc");
                if (xNode != null)
                {
                    if (xNode.NodeValue == chemZone.ContentControl.ID)
                    {
                        return xPart;
                    }
                }
            }
            return null;
        }

        internal void RemoveChemPropXpart(string controlId)
        {
            CustomXMLPart part = null;
            foreach (CustomXMLPart xPart in chemistryDocument.WordDocument.CustomXMLParts)
            {
                CustomXMLNode xNode = xPart.SelectSingleNode("/ChemistryZone/ref/@cc");
                if (xNode != null)
                {
                    if (xNode.NodeValue == controlId)
                    {
                        part = xPart;
                        break;
                    }
                }
            }

            if (part != null)
            {
                part.Delete();
            }
        }
    }
}