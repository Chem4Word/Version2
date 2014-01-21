// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using log4net;

namespace Numbo.Cml
{
    /// <summary>
    /// supports <atomParity></atomParity>
    /// </summary>
    public class CmlAtomParity : CmlElement
    {
        private const double Epsilon = 0.001;
        public const string Tag = "atomParity";
        public const int Undefined = -99;
        private ILog Log = LogManager.GetLogger(typeof (CmlAtomParity));

        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlAtomParity()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlAtomParity(XElement element) : base(element)
        {}

        /// <summary>
        /// set atomRefs4
        /// </summary>
        /// no check on values
        /// <param name="atomRefs4"></param>
        public void SetAtomRefs4(string[] atomRefs4)
        {
            DelegateElement.SetAttributeValue(CmlAttribute.AtomRefs4, CmlUtils.Concatenate(atomRefs4));
        }

        /// <summary>
        /// get atomRefs4
        /// </summary>
        /// no check on values
        /// <returns>null if no attribute else splits string at WS</returns>
        public string[] GetAtomRefs4()
        {
            XAttribute atomRefs4 = DelegateElement.Attribute(CmlAttribute.AtomRefs4);
            return (atomRefs4 == null) ? null : atomRefs4.Value.Split();
        }

        public override string GetTag()
        {
            return Tag;
        }

        private CmlAtom GetParentAtom()
        {
            XElement parent =
                DelegateElement.XPathSelectElement("ancestor::*[local-name()='" + CmlAtom.Tag +
                                                   "' and namespace-uri()='" + CmlConstants.Cmlns + "'][1]");
            return (parent == null) ? null : new CmlAtom(parent);
        }

        private static int CalculateNumberOfSwapsToSortIdsLexically(string[] atomRefs4)
        {
            int nswaps = 0;
            int delta = 0;
            do
            {
                delta = 0;
                for (int i = 0; i < atomRefs4.Count(); i++)
                {
                    for (int j = i + 1; j < atomRefs4.Count(); j++)
                    {
                        if (string.Compare(atomRefs4[i], atomRefs4[j], StringComparison.OrdinalIgnoreCase) > 0)
                        {
                            string swap = atomRefs4[i];
                            atomRefs4[i] = atomRefs4[j];
                            atomRefs4[j] = swap;
                            nswaps++;
                            delta++;
                        }
                    }
                }
            } while (delta != 0);
            return nswaps;
        }

        public static int CalculateSwapParityBetweenAtomRefs4Pair(string[] atomRefs4A, string[] atomRefs4B)
        {
            int referenceSwapParity = CalculateNumberOfSwapsToSortIdsLexically(atomRefs4A);
            int computedSwapParity = CalculateNumberOfSwapsToSortIdsLexically(atomRefs4B);
            int parityDifference = (referenceSwapParity - computedSwapParity)%2;
            return parityDifference;
        }

        internal string CalculateBondStereoIndicator(CmlBond bond)
        {
            CmlAtom parentAtom = GetParentAtom();
            if (parentAtom == null)
            {
                throw new NumboException("the parent atom of atomParity cannot be null");
            }

            if (!bond.GetAtoms().Contains(parentAtom))
            {
                throw new NumboException(string.Format(CultureInfo.InvariantCulture, "Atom parity parent ({0}) not in bond {1}", parentAtom.Id, bond));
            }
            return CalculateBondStereoIndicator(bond, parentAtom);
        }

        private string CalculateBondStereoIndicator(CmlBond bond, CmlAtom parentAtom)
        {
            string bondStereoIndicator = CmlBond.Hatch;
            int? paritySign = GetSign();
            if (paritySign != null)
            {
                CmlBondStereo bondStereo = new CmlBondStereo();
                bondStereo.SetValue(CmlBond.Hatch);
                CmlAtom broadAtom = bond.GetAtomAtOtherEnd(parentAtom);
                double d = bondStereo.CalculateDeterminant(broadAtom, parentAtom.GetLigands());
                bondStereoIndicator = (d*paritySign < 0) ? CmlBond.Wedge : CmlBond.Hatch;
            }
            return bondStereoIndicator;
        }

        public double? GetValue()
        {
            double value;
            if (double.TryParse(DelegateElement.Value, out value))
            {
                return value;
            }
            return null;
        }

        public void SetValue(double value)
        {
            DelegateElement.Value = value.ToString(CultureInfo.InvariantCulture);
        }

        public int? GetSign()
        {
            int? sign = null;
            double? dd = GetValue();
            if (dd.HasValue)
            {
                if (Math.Abs(dd.Value) > Epsilon)
                {
                    sign = Math.Sign(dd.Value);
                }
            }
            return sign;
        }
    }
}