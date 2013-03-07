// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Globalization;
using System.Xml;

namespace Chem4Word.UI.Import
{
    /// <summary>
    /// Checks that the URI the user has specified exists
    /// </summary>
    internal class UriReaderValidator : IValidator
    {
        private readonly ImportMediator mediator;

        private string briefMessage = string.Empty;

        private string longMessage = string.Empty;

        private string uri;

        private XmlReader xmlReader;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">The mediator to control the data flow</param>
        internal UriReaderValidator(ImportMediator mediator)
        {
            this.mediator = mediator;
            this.mediator.RegisterUriReaderValidator(this);
        }

        /// <summary>
        /// Tests if it is possible to read from the uri.
        /// </summary>
        /// <returns>true if no problems found, false otherwise</returns>
        public bool Process()
        {
            Reset();
            try
            {
                xmlReader = XmlReader.Create(uri);
                return true;
            }
            catch (ArgumentException e)
            {
                briefMessage = string.Format(CultureInfo.CurrentUICulture, "Unable to read from this URI: {0}", uri);
                longMessage = string.Format(CultureInfo.CurrentUICulture, e.ToString());
            }
            catch (Exception e)
            {
                briefMessage = "Unexpected exception:\n\t" + e.Message;
                longMessage = e.ToString();
            }
            return false;
        }

        public void Start()
        {
            mediator.UriReaderValidate();
        }

        public string BriefMessage()
        {
            return briefMessage;
        }

        public string LongMessage()
        {
            return longMessage;
        }

        private void Reset()
        {
            briefMessage = string.Empty;
            longMessage = string.Empty;
        }

        public void SetInputUri(string uri)
        {
            this.uri = uri;
        }

        public XmlReader GetXmlReader()
        {
            return xmlReader;
        }
    }
}