// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using Chem4Word.UI.Properties;

namespace Chem4Word.Core.SmartTag
{
    /// <summary>
    /// This class function as Chemical terms dictionary.
    /// It controls how to save and read chemical term.
    /// </summary>
    internal class TermDictionaryManager : IEnumerable<Term>
    {
        protected List<Term> localDictionary;
        protected string localSmartTagDirectory;
        protected string smartTagDirectory;
        protected List<Term> termsDictionary;

        /// <summary>
        /// Initializes a new instance of the TermDictionaryManager class.
        /// </summary>
        /// <param name="smartTagDirectory">Directory path of the dictionary.</param>
        public TermDictionaryManager(string smartTagDirectory)
        {
            try
            {
                this.smartTagDirectory = smartTagDirectory;

                this.termsDictionary = new List<Term>();
                this.localDictionary = new List<Term>();

                XDocument dictionary = XDocument.Load(this.smartTagDirectory + @"\smart-tag-dict.xml");
                IEnumerable<XElement> termElements = dictionary.Root.Descendants("Term");
                foreach (XElement term in termElements)
                {
                    Term t = new Term(term.Attribute("Value").Value.ToLower(CultureInfo.InvariantCulture),
                                      term.Attribute("MoleculeID").Value);
                    this.termsDictionary.Add(t);
                }
                this.termsDictionary.Sort(new TermComparer());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE);
            }
        }

        public void LoadLocalDictionary(string localDictionaryDirectory)
        {
            try
            {
                this.localSmartTagDirectory = localDictionaryDirectory;

                if (Directory.Exists(this.localSmartTagDirectory))
                {
                    if (File.Exists(localDictionaryDirectory + @"\smart-tag-dict.xml"))
                    {
                        this.localDictionary = new List<Term>();
                        XDocument dictionary = XDocument.Load(this.localSmartTagDirectory + @"\smart-tag-dict.xml");
                        IEnumerable<XElement> termElements = dictionary.Root.Descendants("Term");
                        foreach (XElement term in termElements)
                        {
                            Term t = new Term(term.Attribute("Value").Value.ToLower(CultureInfo.InvariantCulture),
                                              term.Attribute("MoleculeID").Value);
                            this.localDictionary.Add(t);
                        }
                        this.localDictionary.Sort(new TermComparer());
                    }
                }
                else
                {
                    Directory.CreateDirectory(this.localSmartTagDirectory);
                    XElement root = new XElement("SmartTagDictionary");
                    XDocument dictionary = new XDocument(root);
                    StreamWriter writer = new StreamWriter(this.localSmartTagDirectory + @"\smart-tag-dict.xml");
                    writer.Write(dictionary.ToString());
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.CHEM_4_WORD_MESSAGE_BOX_TITLE);
            }
        }

        public void AddTerm(string value, string cml)
        {
            string moleculeID = Guid.NewGuid().ToString();

            /// Create Cml file into local Smart Tag dictionary
            StreamWriter writer =
                new StreamWriter(localSmartTagDirectory +
                                 String.Format(CultureInfo.InvariantCulture, @"\{0}.cml", moleculeID));
            writer.Write(cml);
            writer.Close();

            /// Update Local Smart Tag dictionary file
            XDocument dictionary = XDocument.Load(this.localSmartTagDirectory + @"\smart-tag-dict.xml");
            XElement term = new XElement("Term");
            term.Add(new XAttribute("Value", value), new XAttribute("MoleculeID", moleculeID));
            dictionary.Root.Add(term);
            writer = new StreamWriter(this.localSmartTagDirectory + @"\smart-tag-dict.xml");
            writer.Write(dictionary.ToString());
            writer.Close();

            /// Reload Local Smart Tag dictionary
            this.LoadLocalDictionary(this.localSmartTagDirectory);
        }

        /// <summary>
        /// Check id the chemical term id exist in dictionary.
        /// </summary>
        /// <param name="id">Term id.</param>
        /// <returns>Return true or false, if the term exists or not.</returns>
        public bool Contain(string id)
        {
            bool value = File.Exists(this.smartTagDirectory + @"\" + id + @".cml");
            return (value || File.Exists(this.localSmartTagDirectory + @"\" + id + @".cml"));
        }

        public bool IsTermExists(string value)
        {
            string lowerValue = value.ToLower(CultureInfo.InvariantCulture);
            foreach (Term term in this.localDictionary)
            {
                if (term.Value.ToLower(CultureInfo.InvariantCulture) == lowerValue)
                {
                    return true;
                }
            }

            foreach (Term term in this.termsDictionary)
            {
                if (term.Value.ToLower(CultureInfo.InvariantCulture) == lowerValue)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Read Cml content from dictionary by id.
        /// </summary>
        /// <param name="id">Term id.</param>
        /// <returns>Return Cml content.</returns>
        public string GetValue(string id)
        {
            StreamReader reader = null;
            if (File.Exists(this.smartTagDirectory + @"\" + id + @".cml"))
            {
                reader = new StreamReader(this.smartTagDirectory + @"\" + id + @".cml");
            }
            else if (File.Exists(this.localSmartTagDirectory + @"\" + id + @".cml"))
            {
                reader = new StreamReader(this.localSmartTagDirectory + @"\" + id + @".cml");
            }

            return reader != null ? reader.ReadToEnd() : string.Empty;
        }

        /// <summary>
        /// Helper class for sorting Chemical term.
        /// </summary>
        private class TermComparer : IComparer<Term>
        {
            #region IComparer<string> Members

            public int Compare(Term x, Term y)
            {
                return (x.Value.Length - y.Value.Length)*-1;
            }

            #endregion
        }

        #region IEnumerable<Term> Members

        public IEnumerator<Term> GetEnumerator()
        {
            List<Term> enumerateTerm = new List<Term>(this.termsDictionary);
            enumerateTerm.AddRange(this.localDictionary);

            return enumerateTerm.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            List<Term> enumerateTerm = new List<Term>(this.termsDictionary);
            enumerateTerm.AddRange(this.localDictionary);

            return enumerateTerm.GetEnumerator();
        }

        #endregion
    }
}