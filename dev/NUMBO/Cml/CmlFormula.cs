// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Chem4Word.Common.Utilities;
using Numbo.Cml.Helpers;

namespace Numbo.Cml
{
    /// <summary>
    /// supports CML formula element
    /// </summary>
    public class CmlFormula : CmlElement
    {
        /// <summary>
        /// tag
        /// </summary>
        public const string Tag = "formula";

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlFormula()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlFormula(XElement element)
            : base(element)
        {}

        public string Concise
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.Concise) != null)
                           ? DelegateElement.Attribute(CmlAttribute.Concise).Value
                           : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Concise, value); }
        }

        public string Convention
        {
            get { return DelegateElement.Attribute(CmlAttribute.Convention).Value; }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Convention, value); }
        }

        public Nullable<double> Count
        {
            get
            {
                XAttribute countAttribute = DelegateElement.Attribute(CmlAttribute.Count);
                return (countAttribute == null || countAttribute.Value == null ||
                        string.Empty.Equals(countAttribute.Value))
                           ?
                               (double?) null
                           : SafeDouble.Parse(countAttribute.Value);
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Count, value.ToString()); }
        }

        public string Inline
        {
            get
            {
                return (DelegateElement.Attribute(CmlAttribute.Inline) != null)
                           ? DelegateElement.Attribute(CmlAttribute.Inline).Value
                           : null;
            }
            set { DelegateElement.SetAttributeValue(CmlAttribute.Inline, value); }
        }

        public Nullable<int> FormalCharge { get; set; }

        /// <summary>
        /// tag
        /// </summary>
        /// <returns></returns>
        public override string GetTag()
        {
            return Tag;
        }

        /// <summary>
        /// get child formaul elements
        /// </summary>
        /// <returns>zero length list if none</returns>
        public ICollection<CmlFormula> GetChildFormulas()
        {
            List<CmlFormula> childFormulaList = new List<CmlFormula>();
            IEnumerable<XElement> formulasX = this.DelegateElement.Elements(CmlConstants.CmlxNamespace + Tag);
            foreach (XElement formulaX in formulasX)
            {
                childFormulaList.Add(new CmlFormula(formulaX));
            }
            return childFormulaList;
        }

        /// <summary>
        /// Determines whether the argument is a CMLLite formula
        /// 
        /// at the moment this only test the name and namespace - in future
        /// this will check more
        /// </summary>
        /// <param name="element">the XElement to test</param>
        /// <returns>true if element is a CMLFormula otherwise false</returns>
        public static bool IsFormula(XElement element)
        {
            XName xName = CmlConstants.CmlxNamespace + Tag;
            return xName.Equals(element.Name);
        }

        /// <summary>
        /// returns @concise value
        /// if this is not present, calculates formula from atomic composition
        /// </summary>
        /// <param name="molecule"></param>
        /// <returns>null if no concise and no atoms</returns>
        public static string GetOrCalculateConciseFormula(CmlMolecule molecule)
        {
            // is there a child?
            CmlFormula formula = molecule.GetChildFormula();
            string formulaS = (formula == null) ? null : formula.Concise;

            // no child formula or no concise
            return formulaS ?? CalculateConciseFormula(molecule);
        }

        public void MultiplyBy(double d)
        {
            ConciseFormula conciseFormula = ConciseFormula.GetConciseFormula(this);
            if (conciseFormula != null)
            {
                conciseFormula.MultiplyBy(d);
                this.Concise = conciseFormula.GetSortedString();
            }
        }

        public void Add(CmlFormula formula)
        {
            ConciseFormula conciseFormula = ConciseFormula.GetConciseFormula(this);
            if (conciseFormula != null)
            {
                conciseFormula.Add((formula), 1.0);
                this.Concise = conciseFormula.GetSortedString();
            }
        }

        public bool EqualsConcise(CmlFormula formula)
        {
            return this.Concise != null && this.Concise.Equals(formula.Concise);
        }

        /// <summary>
        /// calculates formula from atomic composition
        /// </summary>
        /// <param name="molecule"></param>
        /// <returns>null if no atoms</returns>
        public static string CalculateConciseFormula(CmlMolecule molecule)
        {
            // child molecules? iterate and sum results
            IEnumerable<CmlMolecule> childMolecules = molecule.GetChildMoleculeList();
            if (childMolecules.Count() > 0)
            {
                ConciseFormula aggregateConciseFormula = null;
                foreach (CmlMolecule childMolecule in childMolecules)
                {
                    double? count = childMolecule.Count;
                    if (null == count)
                    {
                        throw new NumboException("Count attribute must be present on a submolecule");
                    }
                    string conciseS = GetOrCalculateConciseFormula(childMolecule);
                    if (conciseS != null)
                    {
                        ConciseFormula nextConciseFormula = new ConciseFormula(conciseS);
                        nextConciseFormula.MultiplyBy((double) count);
                        if (aggregateConciseFormula == null)
                        {
                            aggregateConciseFormula = nextConciseFormula;
                        }
                        else
                        {
                            aggregateConciseFormula.Add(nextConciseFormula, 1.0);
                        }
                    }
                }
                if (aggregateConciseFormula != null)
                {
                    return aggregateConciseFormula.GetSortedString();
                }
            }
            ConciseFormula conciseFormula = new ConciseFormula(molecule);
            return conciseFormula.GetSortedString().Equals(string.Empty) ? null : conciseFormula.GetSortedString();
        }

        /// <summary>
        /// calculates the concise formula from atomic composition
        /// and adds it to the specified molecule and can add it to all
        /// child molecules
        /// </summary>
        /// <param name="molecule"></param>
        /// <param name="recurse">should formulae be added to child molecules</param>
        public static void AddConciseFormulas(CmlMolecule molecule, bool recurse)
        {
            // child molecules? iterate and sum results
            IEnumerable<CmlMolecule> childMolecules = molecule.GetChildMoleculeList();
            if (childMolecules.Count() > 0)
            {
                ConciseFormula aggregateConciseFormula = null;
                foreach (CmlMolecule childMolecule in childMolecules)
                {
                    double? count = childMolecule.Count;
                    if (null == count)
                    {
                        throw new NumboException("Count attribute must be present on a submolecule");
                    }
                    string conciseS = GetOrCalculateConciseFormula(childMolecule);
                    if (conciseS != null)
                    {
                        ConciseFormula nextConciseFormula = new ConciseFormula(conciseS);
                        CmlFormula childFormula = new CmlFormula {Concise = nextConciseFormula.GetSortedString()};
                        if (recurse)
                        {
                            CmlUtils.Add(childMolecule, childFormula);
                        }
                        nextConciseFormula.MultiplyBy((double) count);
                        if (aggregateConciseFormula == null)
                        {
                            aggregateConciseFormula = nextConciseFormula;
                        }
                        else
                        {
                            aggregateConciseFormula.Add(nextConciseFormula, 1.0);
                        }
                    }
                }
                CmlFormula formula = new CmlFormula {Concise = aggregateConciseFormula.GetSortedString()};
                CmlUtils.Add(molecule, formula);
            }
            else
            {
                ConciseFormula conciseFormula = new ConciseFormula(molecule);
                CmlFormula formula = new CmlFormula {Concise = conciseFormula.GetSortedString()};
                CmlUtils.Add(molecule, formula);
            }
        }

        public static bool IsValidInlineValue(string text)
        {
            bool ok = false;
            if (text != null)
            {
                // no spaces ... now check that there is actually some content
                Regex contentRegex = new Regex(".+");
                ok = contentRegex.IsMatch(text);
            }
            return ok;
        }

        public void RemoveInline()
        {
            XAttribute attribute = DelegateElement.Attribute(CmlAttribute.Inline);
            if (attribute != null)
            {
                attribute.Remove();
            }
            else
            {
                /// the inline is already not present
            }
        }

        public void AnnotateOutdated()
        {
            CmlUtils.AnnotateOutdated(this);
        }

        public void RemoveOutdated()
        {
            CmlUtils.RemoveOutdated(this);
        }

        public bool IsOutdated()
        {
            return CmlUtils.IsOutdated(this);
        }
    }

    /// <summary>
    /// helper class, not used externally
    /// </summary>
    internal class ConciseFormula
    {
        private readonly Dictionary<string, double> _atomCounts = new Dictionary<string, double>();
        private int _charge;

        /// <summary>
        /// create formula from string
        /// </summary>
        /// <param name="concise">well-formed concise string</param>
        /// <exception cref="ArgumentException">if concise contains unparsable tokens</exception>
        public ConciseFormula(string concise)
        {
            concise = concise.Trim();
            string[] tokens = concise.Split(' ');
            int nTokens = tokens.Count();
            int nelem = nTokens/2;
            for (int i = 0; i < nelem; i++)
            {
                int itok = i*2;
                string elem = tokens[itok++];
                double count;
                if (!Double.TryParse(tokens[itok], out count))
                {
                    throw new ArgumentException("Cannot parse token (" + tokens[itok] + ") at (" + itok + ") in " +
                                                concise);
                }
                itok++;
                _atomCounts[elem] = count;
            }
            if (nTokens%2 == 1)
            {
                string lastToken = tokens[nTokens - 1];
                if (!int.TryParse(lastToken, out _charge))
                {
                    throw new ArgumentException("cannot parse last token (" + lastToken + ") as charge: " + concise);
                }
            }
        }

        /// <summary>
        /// create from molecule
        /// </summary>
        /// <param name="molecule">molecule containing atoms</param>
        public ConciseFormula(CmlMolecule molecule)
        {
            IEnumerable<CmlAtom> atoms = molecule.GetAtoms();
            _charge = 0;
            foreach (CmlAtom atom in atoms)
            {
                string elementType = atom.ElementType;
                _charge += (atom.FormalCharge.HasValue) ? atom.FormalCharge.Value : 0;
                double count = (atom.Count.HasValue) ? atom.Count.Value : 1.0D;
                if (!_atomCounts.ContainsKey(elementType))
                {
                    _atomCounts[elementType] = 0.0D;
                }
                _atomCounts[elementType] += count;
            }
        }

        internal static ConciseFormula GetConciseFormula(CmlFormula formula)
        {
            ConciseFormula conciseFormula = null;
            string concise = formula.Concise;
            if (concise != null)
            {
                conciseFormula = new ConciseFormula(concise);
            }
            return conciseFormula;
        }

        /// <summary>
        /// allow scaling of formula
        /// </summary>
        /// <param name="f">multiplier (should be positive)</param>
        public void MultiplyBy(double f)
        {
            IEnumerable<string> keys = _atomCounts.Keys;
            List<string> keyList = new List<string>();
            foreach (string key in keys)
            {
                keyList.Add(key);
            }
            foreach (string k in keyList)
            {
                double d = _atomCounts[k];
                _atomCounts[k] = f*d;
            }
            _charge *= (int) f;
        }

        /// <summary>
        /// add two formulae elementwise
        /// </summary>
        /// <param name="formula">formula to add</param>
        /// <param name="count">result of addition</param>
        public void Add(CmlFormula formula, double count)
        {
            string conciseS = formula.Concise;
            if (conciseS != null)
            {
                ConciseFormula conciseFormula = new ConciseFormula(conciseS);
                Add(conciseFormula, count);
            }
        }

        /// <summary>
        /// add formulae elementwise after applying count
        /// </summary>
        /// <param name="conciseFormula">formula to add</param>
        /// <param name="count">factor to scale it by</param>
        public void Add(ConciseFormula conciseFormula, double count)
        {
            List<string> elemList = new List<string>();
            foreach (string key in conciseFormula._atomCounts.Keys)
            {
                elemList.Add(key);
            }

            foreach (string elem in elemList)
            {
                if (!_atomCounts.ContainsKey(elem))
                {
                    _atomCounts[elem] = 0.0D;
                }
                _atomCounts[elem] += count*conciseFormula._atomCounts[elem];
            }
            _charge += (int) (count*conciseFormula._charge);
        }

        /// <summary>
        /// sort as C H alphabetic order
        /// </summary>
        /// <returns>sorted string</returns>
        public string GetSortedString()
        {
            String formulaS = "";
            formulaS = Add(formulaS, PeriodicTable.Element.C.ToString());
            formulaS = Add(formulaS, PeriodicTable.Element.H.ToString());
            IEnumerable<string> keys = _atomCounts.Keys;
            List<string> keyList = new List<string>();
            foreach (string key in keys)
            {
                keyList.Add(key);
            }
            keyList.Sort();
            foreach (string key in keyList)
            {
                if (!key.Equals(PeriodicTable.Element.C.ToString()) && !key.Equals(PeriodicTable.Element.H.ToString()))
                {
                    formulaS = Add(formulaS, key);
                }
            }
            if (_charge != 0)
            {
                formulaS += " " + _charge;
            }
            return formulaS;
        }

        private string Add(string formulaS, string elementType)
        {
            if (_atomCounts.ContainsKey(elementType))
            {
                double dd = _atomCounts[elementType];
                dd = Math.Round(dd, 4);
                if (!string.Empty.Equals(formulaS))
                {
                    formulaS += " ";
                }
                formulaS += elementType + " " + CmlUtils.TruncateTrailingZeros(dd);
            }
            return formulaS;
        }
    }
}