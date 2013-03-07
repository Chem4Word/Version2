// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Numbo.Properties;

namespace Numbo.Cml.Helpers
{
    /// <summary>
    /// a dictionary of GroupDictionaries indexed by abbreviations
    /// </summary>
    public class GroupLookupManager
    {
        public static readonly GroupLookupManager Builtin;

        private readonly Dictionary<string, GroupLookup> groupDictionaryDictionaryByName =
            new Dictionary<string, GroupLookup>();

        static GroupLookupManager()
        {
            // named dictionaries
            Builtin = new GroupLookupManager();
            CreateNewDictionaryAndAddToBuiltin(Resources.aminoacids);
            CreateNewDictionaryAndAddToBuiltin(Resources.divalent);
            CreateNewDictionaryAndAddToBuiltin(Resources.monovalent);
            CreateNewDictionaryAndAddToBuiltin(Resources.olefins);
            CreateNewDictionaryAndAddToBuiltin(Resources.isolatedR);
            CreateNewDictionaryAndAddToBuiltin(Resources.copper);
            // groups
            CreateNewDictionaryAndAddToBuiltin(Resources.ane);
            CreateNewDictionaryAndAddToBuiltin(Resources.ene);
            CreateNewDictionaryAndAddToBuiltin(Resources.ketone);
        }

        private GroupLookupManager()
        {}

        public GroupLookup GetGroupDictionary(string dictionaryName)
        {
            GroupLookup groupDictionary = null;
            groupDictionaryDictionaryByName.TryGetValue(dictionaryName, out groupDictionary);
            return groupDictionary;
        }

        private static void CreateNewDictionaryAndAddToBuiltin(string xmlSource)
        {
            XDocument document = XDocument.Load(new StringReader(xmlSource));
            XElement cml = document.Root;
            string idValue = cml.Attribute(CmlAttribute.ID).Value;
            IEnumerable<XElement> cmlChilds = cml.Elements(CmlConstants.CmlxNamespace + CmlCml.Tag);
            GroupLookup groupDictionary = new GroupLookup();
            foreach (XElement cmlChild in cmlChilds)
            {
                CmlMolecule group = CmlUtils.GetFirstDescendentMolecule(cmlChild);
                groupDictionary.AddGroup(group);
            }
            Builtin.groupDictionaryDictionaryByName.Add(idValue, groupDictionary);
        }
    }
}