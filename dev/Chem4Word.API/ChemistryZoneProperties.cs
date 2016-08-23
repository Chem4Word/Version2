// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Windows;
using System.Xml.Linq;
using System.Xml.XPath;
using Chem4Word.Api.Properties;
using Numbo.Coa;

namespace Chem4Word.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ChemistryZoneProperties 
    {
        private const string ChemistryZonePropertiesRootElementName = "ChemistryZone";
        private const string DocumentDepictionOptionXPathElementName = "DocumentDepictionOptionXPath";
        private const string NavigatorDepictionOptionXPathElementName = "NavigatorDepictionOptionXPath";
        private const string ValueAttributeName = "value";
        private const string CollapsedAttributeName = "collapsed";
        private const string ViewBoxElementName = "ViewBox";
        private const string ViewBoxXAttriubteName = "x";
        private const string ViewBoxYAttriubteName = "y";
        private const string ViewBoxWidthAttriubteName = "width";
        private const string ViewBoxHeightAttriubteName = "height";


        public ChemistryZoneProperties()
        {
            // the default is to use a sans serif font
            OneDZoneFont = Resources.ChemistryFontSansSerif;
        }

        public ChemistryZoneProperties(XDocument storedProperties) :
            this() {
                if (storedProperties != null) {
                    SetupDocumentDepictionOptionXpath(storedProperties);
                    SetupNavigatorDepictionOptionXpath(storedProperties);
                    SetupCollapseNavigatorDepiction(storedProperties);
                    SetupViewBox(storedProperties);
                }
        }

        public ChemistryZoneProperties(string documentDepictionOptionXPath, string navigatorDepictionOptionXPath,
                                       bool collapseNavigatorDepiction)
            : this()
        {
            DocumentDepictionOptionXPath = documentDepictionOptionXPath;
            NavigatorDepictionOptionXPath = navigatorDepictionOptionXPath;
            CollapseNavigatorDepiction = collapseNavigatorDepiction;
        }


        
        public ChemistryZoneProperties(DepictionOption documentDepictionOption, DepictionOption navigatorDepictionOption, bool collapseNavigatorDepiction)
            : this()
        {
            DocumentDepictionOptionXPath = documentDepictionOption.AbsoluteXPathToDepictionOption;
            NavigatorDepictionOptionXPath = navigatorDepictionOption.AbsoluteXPathToDepictionOption;
            CollapseNavigatorDepiction = collapseNavigatorDepiction;
        }

        /// <summary>
        /// Gets _documentDepiction information.
        /// </summary>
        public string DocumentDepictionOptionXPath { get; private set; }

        /// <summary>
        /// Gets navigator _documentDepiction.
        /// </summary>
        public string NavigatorDepictionOptionXPath { get; private set; }

        public string OneDZoneFont { get; set; }
        public bool CollapseNavigatorDepiction { get; set; }

        public void SetDocumentDepictionOption(DepictionOption documentDepictionOption)
        {
            if (documentDepictionOption == null)
            {
                throw new ArgumentNullException("documentDepictionOption");
            }
            DocumentDepictionOptionXPath = documentDepictionOption.AbsoluteXPathToDepictionOption;
        }

        public void SetNavigatorDepictionOption(DepictionOption navigatorDepictionOption)
        {
            if (navigatorDepictionOption == null)
            {
                throw new ArgumentNullException("navigatorDepictionOption");
            }
            NavigatorDepictionOptionXPath = navigatorDepictionOption.AbsoluteXPathToDepictionOption;
        }

        public Rect ViewBox { get; set; } 

        public ChemistryZoneProperties Clone()
        {
            var cloned = new ChemistryZoneProperties
                                                 {
                                                     OneDZoneFont = OneDZoneFont,
                                                     DocumentDepictionOptionXPath = DocumentDepictionOptionXPath,
                                                     NavigatorDepictionOptionXPath = NavigatorDepictionOptionXPath,
                                                     CollapseNavigatorDepiction = CollapseNavigatorDepiction,
                                                     ViewBox = ViewBox
                                                 };
            return cloned;
        }

        public XDocument ToXDocument(string refValue, string contentControlId) {
            var chemZonePropertiesElement = new XElement(ChemistryZonePropertiesRootElementName);
            var chemZonePropertiesDocument = new XDocument(chemZonePropertiesElement);

            var documentDepictionXElement = new XElement(DocumentDepictionOptionXPathElementName);
            
           documentDepictionXElement.Add(new XAttribute(ValueAttributeName, string.IsNullOrEmpty(DocumentDepictionOptionXPath) ? string.Empty : DocumentDepictionOptionXPath));
           chemZonePropertiesElement.Add(documentDepictionXElement);
            var navigatorDepictionXElement = new XElement(NavigatorDepictionOptionXPathElementName);
            navigatorDepictionXElement.Add(new XAttribute(ValueAttributeName, string.IsNullOrEmpty(NavigatorDepictionOptionXPath) ? string.Empty : NavigatorDepictionOptionXPath));
            navigatorDepictionXElement.Add(new XAttribute(CollapsedAttributeName, CollapseNavigatorDepiction));
            chemZonePropertiesElement.Add(navigatorDepictionXElement);

            var viewBoxXElement = new XElement(ViewBoxElementName);
            viewBoxXElement.Add(new XAttribute(ViewBoxXAttriubteName, ViewBox.X));
            viewBoxXElement.Add(new XAttribute(ViewBoxYAttriubteName, ViewBox.Y));
            viewBoxXElement.Add(new XAttribute(ViewBoxWidthAttriubteName, ViewBox.Width));
            viewBoxXElement.Add(new XAttribute(ViewBoxHeightAttriubteName, ViewBox.Height));
            chemZonePropertiesElement.Add(viewBoxXElement);

            var refElement = new XElement("ref",
                                              new XAttribute("cml", refValue), new XAttribute("cc", contentControlId));
            chemZonePropertiesElement.Add(refElement);

            return chemZonePropertiesDocument;
        }


        private void SetupDocumentDepictionOptionXpath(XNode storedProperties)
        {
            var element = storedProperties.XPathSelectElement(
                (string.Format("//*[local-name()='{0}']", DocumentDepictionOptionXPathElementName)));
            if (element != null)
            {
                var attribute = element.Attribute(ValueAttributeName);
                if (attribute != null)
                {
                    DocumentDepictionOptionXPath = attribute.Value;
                }
            }
        }

        private void SetupNavigatorDepictionOptionXpath(XNode storedProperties)
        {
            var element = storedProperties.XPathSelectElement(
                (string.Format("//*[local-name()='{0}']", NavigatorDepictionOptionXPathElementName)));
            if (element != null)
            {
                var attribute = element.Attribute(ValueAttributeName);
                if (attribute != null)
                {
                    NavigatorDepictionOptionXPath = attribute.Value;
                }
            }
        }

        private void SetupCollapseNavigatorDepiction(XNode storedProperties) {
            CollapseNavigatorDepiction = false;
            var element = storedProperties.XPathSelectElement(
               (string.Format("//*[local-name()='{0}']", NavigatorDepictionOptionXPathElementName)));
            if (element != null)
            {
                var attribute = element.Attribute(CollapsedAttributeName);
                if (attribute != null) {
                    bool collapse;
                    if (Boolean.TryParse(attribute.Value, out collapse)) {
                        CollapseNavigatorDepiction = collapse;
                    } 
                }
            }
        }

        private void SetupViewBox(XNode storedProperties)
        {
            var element = storedProperties.XPathSelectElement(
               (string.Format("//*[local-name()='{0}']", ViewBoxElementName)));
            if (element != null)
            {
                var xAttribute = element.Attribute(ViewBoxXAttriubteName);
                var yAttribute = element.Attribute(ViewBoxYAttriubteName);
                var widthAttribute = element.Attribute(ViewBoxWidthAttriubteName);
                var heightAttribute = element.Attribute(ViewBoxHeightAttriubteName);
                
                if (xAttribute != null && yAttribute != null && widthAttribute != null && heightAttribute != null) {
                    double x, y, width, height;
                    if (double.TryParse(xAttribute.Value, out x) && double.TryParse(yAttribute.Value, out y) 
                        && double.TryParse(widthAttribute.Value, out width) && double.TryParse(heightAttribute.Value, out height)) {
                        ViewBox = new Rect(x, y, width, height);
                    }
                }
            }
        }
    }
}