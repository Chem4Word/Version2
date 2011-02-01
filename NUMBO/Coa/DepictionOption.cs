// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using Numbo.Cml;

namespace Numbo.Coa {
    ///<summary>
    ///</summary>
    public class DepictionOption : IEquatable<DepictionOption> {
        /// <summary>
        /// </summary>
        /// <param name = "xObject">the XAttribute or XElement to which this depiction option refers</param>
        public DepictionOption(XObject xObject) {
            MachineUnderstandableOption = xObject;
            AbsoluteXPathToDepictionOption = GenerateFullPath(MachineUnderstandableOption);

            if (Depiction.Is2D(this)) {
                HumanUnderstandableOption = "2D";
                DepictionOptionDescription = "2D";
            } else if (Depiction.IsName(this)) {
                CmlName name = new CmlName((XElement) MachineUnderstandableOption);
                HumanUnderstandableOption = name.GetValue();
                DepictionOptionDescription = string.Format(CultureInfo.InvariantCulture, "name - {0}", name.DictRef);
            } else if (Depiction.IsMoleculeBoldNumberLabel(this)) {
                CmlLabel label = new CmlLabel((XElement) MachineUnderstandableOption);
                HumanUnderstandableOption = label.Value;
                DepictionOptionDescription = "molecule bold number";
            } else if (Depiction.IsLabel(this)) {
                CmlLabel label = new CmlLabel(MachineUnderstandableOption.Parent);
                HumanUnderstandableOption = label.Value;
                DepictionOptionDescription = string.Format(CultureInfo.InvariantCulture, "label - {0}", label.DictRef);
            } else if (Depiction.IsInlineFormula(this)) {
                CmlFormula formula = new CmlFormula(MachineUnderstandableOption.Parent);
                HumanUnderstandableOption = formula.Inline;
                DepictionOptionDescription = "formula - inline";
            } else if (Depiction.IsConciseFormula(this)) {
                CmlFormula formula = new CmlFormula(MachineUnderstandableOption.Parent);
                HumanUnderstandableOption = formula.Concise;
                DepictionOptionDescription = "formula - concise";
            } else {
                // unknown depiction type
                DepictionOptionDescription = "unknown";
                if (MachineUnderstandableOption is XElement) {
                    HumanUnderstandableOption = ((XElement) MachineUnderstandableOption).Value;
                } else {
                    // must be XAttribute
                    HumanUnderstandableOption = ((XAttribute) MachineUnderstandableOption).Value;
                }
            }
        }

        /// <summary>
        /// </summary>
        public string HumanUnderstandableOption { get; private set; }

        ///<summary>
        ///  A human readable explanation of what this depiction is: ie "name - nameDict:iupac"
        ///</summary>
        public string DepictionOptionDescription { get; private set; }

        ///<summary>
        ///</summary>
        public XObject MachineUnderstandableOption { get; private set; }

        ///<summary>
        ///</summary>
        public string AbsoluteXPathToDepictionOption { get; private set; }

        #region IEquatable<DepictionOption> Members

        /// <summary>
        ///   Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///   true if the current object is equal to the <paramref name = "other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name = "other">An object to compare with this object.
        /// </param>
        public bool Equals(DepictionOption other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return Equals(other.MachineUnderstandableOption, MachineUnderstandableOption);
        }

        #endregion

        /// <summary>
        ///   Event raised after the XPath mapping is changed.
        /// </summary>
        public event EventHandler<EventArgs> AbsoluteXPathMappingChanged;

        /// <summary>
        ///   Determines whether the specified <see cref = "T:System.Object" /> is equal to the current <see cref = "T:System.Object" />.
        /// </summary>
        /// <returns>
        ///   true if the specified <see cref = "T:System.Object" /> is equal to the current <see cref = "T:System.Object" />; otherwise, false.
        /// </returns>
        /// <param name = "obj">The <see cref = "T:System.Object" /> to compare with the current <see cref = "T:System.Object" />. 
        /// </param>
        /// <exception cref = "T:System.NullReferenceException">The <paramref name = "obj" /> parameter is null.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != typeof (DepictionOption)) {
                return false;
            }
            return Equals((DepictionOption) obj);
        }

        /// <summary>
        ///   Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        ///   A hash code for the current <see cref = "T:System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode() {
            return (MachineUnderstandableOption != null ? MachineUnderstandableOption.GetHashCode() : 0);
        }

        /// <summary>
        /// </summary>
        /// <param name = "left"></param>
        /// <param name = "right"></param>
        /// <returns></returns>
        public static bool operator ==(DepictionOption left, DepictionOption right) {
            return Equals(left, right);
        }

        /// <summary>
        /// </summary>
        /// <param name = "left"></param>
        /// <param name = "right"></param>
        /// <returns></returns>
        public static bool operator !=(DepictionOption left, DepictionOption right) {
            return !Equals(left, right);
        }

        /// <summary>
        ///   Allows you to deserialize the depiction option from the absolute xpath 
        ///   and the XDocument
        /// </summary>
        /// <param name = "xDocument"></param>
        /// <param name = "absoluteXPathToDepiction"></param>
        /// <returns></returns>
        public static DepictionOption CreateDepictionOption(XDocument xDocument, string absoluteXPathToDepiction) {
            if (xDocument == null) {
                return null;
            }
            var objs = (IEnumerable) xDocument.Root.XPathEvaluate(absoluteXPathToDepiction);
            var attList = objs.Cast<XObject>();
            return new DepictionOption(attList.FirstOrDefault());
        }

        private static string GenerateFullPath(XElement xelement) {
            var name = string.Format(CultureInfo.InvariantCulture, "*[local-name()='{0}' and namespace-uri()='{1}']", xelement.Name.LocalName, xelement.Name.NamespaceName);
            var position = 1 + xelement.ElementsBeforeSelf(xelement.Name.Namespace + xelement.Name.LocalName).Count();
            var nextBitOfPath = string.Format(CultureInfo.InvariantCulture, "/{0}[{1}]", name, (position));
            if (xelement.Document.Root == xelement) {
                return nextBitOfPath;
            }
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", GenerateFullPath(xelement.Parent),
                                 nextBitOfPath);
        }

        private static string GenerateFullPath(XObject xobject) {
            if (xobject is XAttribute) {
                var xattribute = xobject as XAttribute;
                return 
                   string.Format(CultureInfo.InvariantCulture, "{0}{1}", GenerateFullPath(xattribute.Parent),
                   string.Format(CultureInfo.InvariantCulture, "/@*[local-name()='{0}' and namespace-uri()='{1}']", xattribute.Name.LocalName, xattribute.Name.NamespaceName));
            }
            return GenerateFullPath(xobject as XElement);
        }

        /// <summary>
        ///   Converts the CML format for this depiction option to the appropriate OMath format
        /// </summary>
        /// <returns>the 1D depiction in OMath format</returns>
        /// <exception cref = "FormatException">If called on 2D depiction</exception>
        public string GetAsOMath() {
            if (Depiction.Is2D(this)) {
                throw new FormatException("2D depictions do not support this option");
            }
            return Depiction.IsConciseFormula(this)
                       ? FormatConciseAsOMath(((XAttribute) MachineUnderstandableOption).Value)
                       : FormatAsOMath(HumanUnderstandableOption);
        }

        public CmlMolecule GetParentMolecule() {
            var element = (MachineUnderstandableOption is XAttribute)
                                   ? MachineUnderstandableOption.Parent
                                   : (XElement) MachineUnderstandableOption;
            return CmlUtils.GetFirstAncestorMolecule(element);
        }

        /// <summary>
        /// </summary>
        /// <param name = "concise"></param>
        /// <returns></returns>
        private static string FormatConciseAsOMath(string concise) {
            if (concise == null) {
                throw new ArgumentNullException("concise");
            }
            var split = concise.Split(' ');

            var length = split.Length;
            var stringBuilder = new StringBuilder();
            var n = length/2;

            for (var j = 0; j < 2*n; j += 2) {
                stringBuilder.Append(split[j]);
                if (!string.Equals("1", split[j + 1], StringComparison.Ordinal)) {
                    stringBuilder.Append("_");
                    stringBuilder.Append(split[j + 1]);
                    if (j >= (2*n) - 2) {
                        if (length%2 == 0) {
                            stringBuilder.Append(" ");
                        }
                    } else {
                        stringBuilder.Append(" ");
                    }
                }
            }
            if (length%2 != 0) {
                // must be a charge
                stringBuilder.Append("^");
                stringBuilder.Append("(");
                var negative = split[length - 1].StartsWith("-", StringComparison.Ordinal);

                var count = negative ? split[length - 1].Substring(1) : split[length - 1];
                // count is defined to be int by the schema
                if (!string.Equals("1", count, StringComparison.Ordinal)) {
                    if (negative) {
                        stringBuilder.Append(count);
                    } else {
                        stringBuilder.Append(count);
                    }
                }
                stringBuilder.Append(negative ? "-" : "+");
                stringBuilder.Append(")");
                stringBuilder.Append(" ");
            }
            return stringBuilder.ToString();
        }

        private static string FormatAsOMath(string s) {
            s = Depiction.ReplaceGreekAsOMath(s);
            const string superpattern = "(" +
                                        "(((?'Open'(?<!\\\\)\\^\\{))(?'capture'[^(?:(?<!\\\\)\\^\\{)(?:(?<!\\\\)\\})]*))+" +
                                        "((?'Close-Open'(?<!\\\\)}))" +
                                        ")+" +
                                        "(?(Open)(?!))";

            s = Regex.Replace(s, superpattern, "^(${capture}) ");
            const string subpattern = "(" +
                                      "(((?'Open'(?<!\\\\)_\\{))(?'capture'[^(?:(?<!\\\\)_\\{)(?:(?<!\\\\)\\})]*))+" +
                                      "((?'Close-Open'(?<!\\\\)}))" +
                                      ")+" +
                                      "(?(Open)(?!))";
            s = Regex.Replace(s, subpattern, "_(${capture}) ");
            if (s.StartsWith("_(", StringComparison.OrdinalIgnoreCase) ||
                s.StartsWith("^(", StringComparison.OrdinalIgnoreCase)) {
                s = "\u200B" + s;
            }
            return s;
        }

        public string GetAsLatexFormattedString() {
            var s = string.Empty;
            if (!Depiction.Is2D(this)) {
                s = HumanUnderstandableOption;
                if (Depiction.IsConciseFormula(this)) {
                    var split = s.Split(' ');

                    var length = split.Length;
                    var stringBuilder = new StringBuilder();
                    var n = length/2;

                    for (var j = 0; j < 2*n; j += 2) {
                        stringBuilder.Append(split[j]);
                        if (!string.Equals("1", split[j + 1], StringComparison.Ordinal)) {
                            stringBuilder.Append("_{");
                            stringBuilder.Append(split[j + 1]);
                            stringBuilder.Append("}");
                        }
                    }
                    if (length%2 != 0) {
                        // must be a charge
                        stringBuilder.Append("^");
                        stringBuilder.Append("{");
                        var negative = split[length - 1].StartsWith("-", StringComparison.Ordinal);

                        var count = negative ? split[length - 1].Substring(1) : split[length - 1];
                        // count is defined to be int by the schema
                        if (!string.Equals("1", count, StringComparison.Ordinal)) {
                            if (negative) {
                                stringBuilder.Append(count);
                            } else {
                                stringBuilder.Append(count);
                            }
                        }
                        stringBuilder.Append(negative ? "-" : "+");
                        stringBuilder.Append("}");
                    }
                    s = stringBuilder.ToString();
                }
            }
            return s;
        }

        public void RecalculateAbsoluteXPath() {
            AbsoluteXPathToDepictionOption = GenerateFullPath(MachineUnderstandableOption);
            if (AbsoluteXPathMappingChanged != null) {
                AbsoluteXPathMappingChanged(this, null);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>the associated 2D depiction or null if none are found</returns>
        public DepictionOption GetAssociatedTwoDDepictionOption() {
            if (Depiction.Is2D(this)) {
                return this;
            }
            // must be a 1D depiction. so lets see if there a valid
            // two d depiction associated with it.
            var xElement = (MachineUnderstandableOption is XAttribute)
                                    ? MachineUnderstandableOption.Parent
                                    : (XElement) MachineUnderstandableOption;

            var cmlMolecule = CmlUtils.GetFirstAncestorMolecule(xElement);
            if (cmlMolecule == null) {
                return null;
            }
            var depictionOptions =
                Depiction.PossibleDepictionOptions(new ContextObject(MachineUnderstandableOption.Document),
                                                   cmlMolecule.DelegateElement);
            return (from option in depictionOptions
                    where Depiction.Is2D(option)
                    let moleculeXElement = option.MachineUnderstandableOption as XElement
                    where cmlMolecule.DelegateElement.Equals(moleculeXElement)
                    select option).FirstOrDefault();
        }

        public bool HasAssociatedTwoD() {
            return GetAssociatedTwoDDepictionOption() == null ? false : true;
        }
    }
}