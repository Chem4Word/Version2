// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using log4net;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;

namespace Chem4Word.Core
{
    ///<summary>
    ///</summary>
    public static class ControlsStorage
    {
        private const string ControlsNodeName = "Controls";
        private const string ControlsStorageNamespace = "urn:schemas-microsoft-com.VSTO2008Demos.ControlsStorage";
        private const string RootNodeName = "ControlsStorage";
        private static readonly ILog Log = LogManager.GetLogger(typeof (ControlsStorage));

        ///<summary>
        ///</summary>
        ///<param name="document"></param>
        ///<param name="controls"></param>
        public static void Store(_Document document, ControlProperties[] controls)
        {
            Log.Debug(string.Format(CultureInfo.InvariantCulture, "ControlsStorage - Store : controls count {0}", controls.Length));
            string xml = null;

            using (MemoryStream memStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memStream, controls);

                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement(RootNodeName, ControlsStorageNamespace);
                doc.AppendChild(root);

                XmlElement controlsDataNode = doc.CreateElement(ControlsNodeName, ControlsStorageNamespace);
                controlsDataNode.InnerXml = Convert.ToBase64String(memStream.GetBuffer(), 0, (int) memStream.Length);
                root.AppendChild(controlsDataNode);

                xml = doc.InnerXml;
            }

            CustomXMLParts parts = document.CustomXMLParts.SelectByNamespace(ControlsStorageNamespace);
            if (parts.Count > 0)
            {
                Debug.Assert(parts.Count == 1);
                parts[1].Delete();
            }

            document.CustomXMLParts.Add(xml, Type.Missing);
        }

        ///<summary>
        ///</summary>
        ///<param name="document"></param>
        ///<returns></returns>
        public static ControlProperties[] Load(_Document document)
        {
            Log.Debug(string.Format(CultureInfo.InvariantCulture, "ControlsStorage - Load : document {0}", document));

            ControlProperties[] controls = null;
            CustomXMLParts parts = document.CustomXMLParts.SelectByNamespace(ControlsStorageNamespace);

            if (parts != null && parts.Count > 0)
            {
                Debug.Assert(parts.Count == 1);
                CustomXMLPart part = parts[1];
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(part.XML);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("sc", ControlsStorageNamespace);

                XmlElement controlsElement =
                    doc.SelectSingleNode(String.Format(CultureInfo.CurrentUICulture, "//sc:{0}", ControlsNodeName),
                                         nsmgr) as XmlElement;
                if (controlsElement != null)
                {
                    byte[] data = Convert.FromBase64String(controlsElement.InnerXml);
                    BinaryFormatter formatter = new BinaryFormatter();
                    controls = (ControlProperties[]) formatter.Deserialize(new MemoryStream(data));
                }
            }

            return controls;
        }
    }
}