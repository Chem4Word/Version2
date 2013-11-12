using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chem4Word.UI.Converters
{
    public static class Json
    {
        public static string ToCML(string p_JsonIn)
        {
            Debug.WriteLine("JSON.ToCML()");
            string result = p_JsonIn;

            // http://www.xml-cml.org/convention/molecular
            if (p_JsonIn.StartsWith("{"))
            {
                XmlDocument doc = new XmlDocument();

                int atomCounter = 0;
                int bondCounter = 0;

                #region Construct empty CML file
                StringBuilder sb = new StringBuilder();
                sb.Append("<cml:cml");
                sb.Append(" convention='conventions:molecular'");
                sb.Append(" xmlns:conventions='http://www.xml-cml.org/convention/'");
                sb.Append(" xmlns:cml='http://www.xml-cml.org/schema'");
                sb.Append(" xmlns:cmlDict='http://www.xml-cml.org/dictionary/cml/'");
                sb.Append(" xmlns:nameDict='http://www.xml-cml.org/dictionary/cml/name/'");
                //sb.Append(" xmlns:p7='http://www.xml-cml.org/dictionary/cmlx/'");
                sb.Append(">");
                sb.Append("<cml:molecule id='m1'>");
                sb.Append("  <cml:atomArray>");
                sb.Append("  </cml:atomArray>");
                sb.Append("  <cml:bondArray>");
                sb.Append("  </cml:bondArray>");
                sb.Append("</cml:molecule>");
                sb.Append("</cml:cml>");
                doc.LoadXml(sb.ToString());
                #endregion

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("cml", "http://www.xml-cml.org/schema");

                XmlElement cmlAtoms = doc.SelectSingleNode("//cml:atomArray", nsmgr) as XmlElement;
                XmlElement cmlBonds = doc.SelectSingleNode("//cml:bondArray", nsmgr) as XmlElement;

                JToken molJson = JObject.Parse(p_JsonIn);

                JToken atoms = molJson.SelectToken("a");
                Debug.WriteLine("Found " + atoms.Count<JToken>() + " atoms");
                foreach (JToken atom in atoms)
                {
                    // Ignore incoming atom id
                    //string atomid = (string)atom.SelectToken("i");
                    string label = (string)atom.SelectToken("l");
                    double x = (double)atom.SelectToken("x");
                    double y = (double)atom.SelectToken("y");
                    string charge = (string)atom.SelectToken("c");

                    XmlElement cmlAtom = doc.CreateElement("cml:atom", "http://www.xml-cml.org/schema");
                    cmlAtom.SetAttribute("id", "a" + atomCounter.ToString());
                    if (label == null)
                    {
                        label = "C";
                    }
                    cmlAtom.SetAttribute("elementType", label);
                    cmlAtom.SetAttribute("x2", x.ToString());
                    cmlAtom.SetAttribute("y2", y.ToString());
                    if (charge != null)
                    {
                        cmlAtom.SetAttribute("formalCharge", charge);
                    }
                    cmlAtoms.AppendChild(cmlAtom);
                    atomCounter++;
                }

                JToken bonds = molJson.SelectToken("b");
                if (bonds != null)
                {
                    Debug.WriteLine("Found " + bonds.Count<JToken>() + " bonds");
                    foreach (JToken bond in bonds)
                    {
                        // Ignore incoming bond id
                        //string bondid = (string)bond.SelectToken("i");
                        string begin = (string)bond.SelectToken("b");
                        string end = (string)bond.SelectToken("e");
                        string order = (string)bond.SelectToken("o");
                        string stereo = (string)bond.SelectToken("s");

                        XmlElement cmlBond = doc.CreateElement("cml:bond", "http://www.xml-cml.org/schema");
                        cmlBond.SetAttribute("id", "b" + bondCounter);
                        cmlBond.SetAttribute("atomRefs2", "a" + begin + " a" + end);
                        cmlBond.SetAttribute("cyclic", "Acyclic");
                        cmlBond.SetAttribute("p7", "http://www.xml-cml.org/dictionary/cmlx");

                        if (order != null)
                        {
                            cmlBond.SetAttribute("order", OrderToString(order));
                        }
                        else
                        {
                            cmlBond.SetAttribute("order", "S");
                        }

                        if (stereo != null)
                        {
                            XmlElement cmlStereo = doc.CreateElement("cml:bondStereo", "http://www.xml-cml.org/schema");
                            cmlStereo.InnerText = StereoToString(stereo);
                            cmlBond.AppendChild(cmlStereo);
                        }
                        cmlBonds.AppendChild(cmlBond);
                        bondCounter++;
                    }
                }
                result = Beautify(doc);
            }
            return result;
        }

        private static string StereoToString(string p_stereo)
        {
            string result = "";
            switch (p_stereo)
            {
                case "protruding":
                    result = "W";
                    break;
                case "recessed":
                    result = "H";
                    break;
                case "ambiguous":
                    result = "S";
                    break;
            }
            return result;
        }

        private static string OrderToString(string p_order)
        {
            string result = "p_order";
            switch (p_order)
            {
                case "1":
                case "1.0":
                    result = "S";
                    break;
                case "2":
                case "2.0":
                    result = "D";
                    break;
                case "3":
                case "3.0":
                    result = "T";
                    break;
            }
            return result;
        }

        private static string Beautify(XmlDocument doc)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.NewLineChars = "\r\n";
            settings.NewLineHandling = NewLineHandling.Replace;
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }

    }
}
