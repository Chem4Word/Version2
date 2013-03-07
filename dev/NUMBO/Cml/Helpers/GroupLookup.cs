// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace Numbo.Cml.Helpers
{
    public class GroupLookup
    {
        private readonly Dictionary<string, CmlMolecule> dictionary;

        public GroupLookup()
        {
            dictionary = new Dictionary<string, CmlMolecule>();
        }

        public int Count
        {
            get { return dictionary.Keys.Count; }
        }

        internal void AddGroup(CmlMolecule group)
        {
            string abbrev =
                group.DelegateElement.XPathSelectElement(".//*[@" + CmlAttribute.DictRef + "='nameDict:abbreviation']").
                    Value;
            if (abbrev == null)
            {
                throw new NumboException("molecule is not of group form");
            }
            try
            {
                dictionary.Add(abbrev, group);
            }
            catch (Exception e)
            {
                throw new NumboException(e + " ... " + abbrev);
            }
        }

        /// <summary>
        /// gets group by abbreviation
        /// </summary>
        /// <param name="abbreviation">to get group by</param>
        /// <returns>group or null</returns>
        public CmlMolecule GetGroup(string abbreviation)
        {
            CmlMolecule group;
            dictionary.TryGetValue(abbreviation, out group);
            return group;
        }

        public IEnumerable<string> Keys()
        {
            return dictionary.Keys;
        }
    }
}