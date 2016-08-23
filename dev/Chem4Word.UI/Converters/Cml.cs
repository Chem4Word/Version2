using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Chem4Word.Common.Utilities;
using Newtonsoft.Json.Linq;

namespace Chem4Word.UI.Converters
{
    public static class Cml
    {
        public static string ToJson(string p_CmlIn)
        {
            //Debug.WriteLine("CML.ToJson()");

            string result = p_CmlIn;

            if (p_CmlIn.StartsWith("<"))
            {
                // Need to store atomIds in dictionary as id of 1st atom not always a0
                Dictionary<String, int> atomIds = new Dictionary<string, int>();
                JObject jsonOut = new JObject();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(p_CmlIn);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("cml", "http://www.xml-cml.org/schema");

                XmlNodeList atoms = doc.SelectNodes("//cml:atom", nsmgr);
                //Debug.WriteLine("Found " + atoms.Count + " atoms");
                // Handle case when cml namespace fails to get atoms
                if (atoms.Count == 0)
                {
                    atoms = doc.SelectNodes("//atom");
                    //Debug.WriteLine("Found " + atoms.Count + " atoms");
                }
                if (atoms.Count > 0)
                {
                    JArray jAtomsArray = new JArray();
                    JProperty jAtomsProperty = new JProperty("a", jAtomsArray);
                    jsonOut.Add(jAtomsProperty);

                    int atomCount = 0;
                    foreach (XmlNode atom in atoms)
                    {
                        string atomId = GetAttribute(atom, "id");
                        if (string.IsNullOrEmpty(atomId))
                        {
                            atomId = "a" + atomCount;
                        }
                        atomIds.Add(atomId, atomCount);

                        string atomLabel = GetAttribute(atom, "elementType");
                        // 2D co-ordinates (if present)
                        string x2 = GetAttribute(atom, "x2");
                        string y2 = GetAttribute(atom, "y2");
                        // 3D co-ordinates (if present)
                        string x3 = GetAttribute(atom, "x3");
                        string y3 = GetAttribute(atom, "y3");
                        string z3 = GetAttribute(atom, "z3");

                        JObject jAtom = new JObject();
                        jAtom.Add(new JProperty("i", atomId));
                        if (string.IsNullOrEmpty(z3))
                        {
                            // 2D co-ordinates supplied
                            jAtom.Add(new JProperty("x", SafeDouble.Parse(x2)));
                            jAtom.Add(new JProperty("y", SafeDouble.Parse(y2)));
                        }
                        else
                        {
                            // 3D co-ordinates supplied
                            jAtom.Add(new JProperty("x", SafeDouble.Parse(x3)));
                            jAtom.Add(new JProperty("y", SafeDouble.Parse(y3)));
                            jAtom.Add(new JProperty("z", SafeDouble.Parse(z3)));
                        }

                        if (!atomLabel.Equals("C"))
                        {
                            jAtom.Add(new JProperty("l", atomLabel));
                        }

                        string charge = GetAttribute(atom, "formalCharge");
                        if (!string.IsNullOrEmpty(charge))
                        {
                            jAtom.Add(new JProperty("c", Int32.Parse(charge)));
                        }

                        jAtomsArray.Add(jAtom);
                        atomCount++;
                    }
                }

                XmlNodeList bonds = doc.SelectNodes("//cml:bond", nsmgr);
                //Debug.WriteLine("Found " + bonds.Count + " bonds");
                // Handle case when cml namespace fails to get bonds
                if (bonds.Count == 0)
                {
                    bonds = doc.SelectNodes("//bond");
                    //Debug.WriteLine("Found " + bonds.Count + " bonds");
                }
                if (bonds.Count > 0)
                {
                    JArray jBondsArray = new JArray();
                    JProperty jBondsProperty = new JProperty("b", jBondsArray);
                    jsonOut.Add(jBondsProperty);

                    int bondCount = 0;
                    foreach (XmlNode bond in bonds)
                    {
                        string atomRefs2 = GetAttribute(bond, "atomRefs2");
                        string[] bondStartEnd = atomRefs2.Split(' ');
                        string bondOrder = GetAttribute(bond, "order");
                        string bondStereo = "";

                        if (bond.ChildNodes.Count > 0)
                        {
                            XmlNode stereoNode = bond.FirstChild;
                            bondStereo = stereoNode.InnerText;
                        }

                        JObject jBond = new JObject();

                        jBond.Add(new JProperty("i", "b" + bondCount));

                        int atomId0 = atomIds[bondStartEnd[0]];
                        int atomId1 = atomIds[bondStartEnd[1]];
                        jBond.Add(new JProperty("b", atomId0));
                        jBond.Add(new JProperty("e", atomId1));

                        if (!string.IsNullOrEmpty(bondOrder))
                        {
                            Single order = OrderToNumber(bondOrder);
                            if (order != 1)
                            {
                                jBond.Add(new JProperty("o", order));
                            }
                        }

                        if (!string.IsNullOrEmpty(bondStereo))
                        {
                            jBond.Add(new JProperty("s", StereoToString(bondStereo)));
                        }

                        jBondsArray.Add(jBond);
                        bondCount++;
                    }
                }

                result = jsonOut.ToString();
            }

            return result;
        }
        
        private static string StereoToString(string p_stereo)
        {
            string result = "";
            switch (p_stereo)
            {
                case "W":
                    result = "protruding";
                    break;

                case "H":
                    result = "recessed";
                    break;

                default:
                    break;
            }
            return result;
        }

        private static Single OrderToNumber(string p_order)
        {
            string temp = "0";
            switch (p_order)
            {
                case "1":
                case "S":
                    temp = "1";
                    break;

                case "1.5":
                case "A":
                    temp = "1.5";
                    break;

                case "2":
                case "D":
                    temp = "2";
                    break;

                case "3":
                case "T":
                    temp = "3";
                    break;

                default:
                    break;
            }
            return Single.Parse(temp);
        }

        private static string GetAttribute(XmlNode p_node, string p_name)
        {
            string result = "";
            try
            {
                for (int i = 0; i < p_node.Attributes.Count; i++)
                {
                    if (p_node.Attributes[i].Name.Equals(p_name))
                    {
                        result = p_node.Attributes[p_name].InnerText;
                        break;
                    }
                }
            }
            catch { }
            return result;
        }
    }
}