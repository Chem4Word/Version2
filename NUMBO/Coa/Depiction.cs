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
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using Numbo.Cml;

namespace Numbo.Coa
{
    /// <summary>
    /// Methods which work out how a molecule/fragment etc may be displayed to the user.
    /// </summary>
    public class Depiction
    {
        public static readonly string Alpha = "\u0391";
        public static readonly string AlphaLower = "\u03B1";
        public static readonly string Beta = "\u0392";
        public static readonly string BetaLower = "\u03B2";
        public static readonly string Bullet = "\u2022";
        public static readonly string Chi = "\u03A7";
        public static readonly string ChiLower = "\u03C7";
        public static readonly string Dagger = "\u2020";
        public static readonly string Delta = "\u0394";
        public static readonly string DeltaLower = "\u03B4";
        public static readonly string DoubleDagger = "\u2021";
        public static readonly string Epsilon = "\u0395";
        public static readonly string EpsilonLower = "\u03B5";
        public static readonly string Eta = "\u0397";
        public static readonly string EtaLower = "\u03B7";
        public static readonly string Gamma = "\u0393";
        public static readonly string GammaLower = "\u03B3";
        public static readonly string Iota = "\u0399";
        public static readonly string IotaLower = "\u03B9";
        public static readonly string Kappa = "\u039A";
        public static readonly string KappaLower = "\u03BA";
        public static readonly string Lambda = "\u039B";
        public static readonly string LambdaLower = "\u03BB";
        public static readonly string LatexAlpha = @"\Alpha{}";
        public static readonly string LatexAlphaLower = @"\alpha{}";
        public static readonly string LatexBeta = @"\Beta{}";
        public static readonly string LatexBetaLower = @"\beta{}";
        public static readonly string LatexBullet = @"\bullet{}";
        public static readonly string LatexChi = @"\Chi{}";
        public static readonly string LatexChiLower = @"\chi{}";
        public static readonly string LatexDagger = @"\dagger{}";
        public static readonly string LatexDelta = @"\Delta{}";
        public static readonly string LatexDeltaLower = @"\delta{}";
        public static readonly string LatexDoubleDagger = @"\ddagger{}";
        public static readonly string LatexEpsilon = @"\Epsilon{}";
        public static readonly string LatexEpsilonLower = @"\epsilon{}";
        public static readonly string LatexEta = @"\Eta{}";
        public static readonly string LatexEtaLower = @"\eta{}";
        public static readonly string LatexGamma = @"\Gamma{}";
        public static readonly string LatexGammaLower = @"\gamma{}";
        public static readonly Dictionary<string, string> LatexGreekToOMath;
        public static readonly string LatexHyphen = @"-";
        public static readonly string LatexIota = @"\Iota{}";
        public static readonly string LatexIotaLower = @"\iota{}";
        public static readonly string LatexKappa = @"\Kappa{}";
        public static readonly string LatexKappaLower = @"\kappa{}";
        public static readonly string LatexLambda = @"\Lambda{}";
        public static readonly string LatexLambdaLower = @"\lambda{}";
        public static readonly string LatexMu = @"\Mu{}";
        public static readonly string LatexMuLower = @"\mu{}";
        public static readonly string LatexNu = @"\Nu{}";
        public static readonly string LatexNuLower = @"\nu{}";
        public static readonly string LatexOmega = @"\Omega{}";
        public static readonly string LatexOmegaLower = @"\omega{}";
        public static readonly string LatexOmicron = @"\Omicron{}";
        public static readonly string LatexOmicronLower = @"\omicron{}";
        public static readonly string LatexPhi = @"\Phi{}";
        public static readonly string LatexPhiLower = @"\phi{}";
        public static readonly string LatexPi = @"\Pi{}";
        public static readonly string LatexPiLower = @"\pi{}";
        public static readonly string LatexPsi = @"\Psi{}";
        public static readonly string LatexPsiLower = @"\psi{}";
        public static readonly string LatexRho = @"\Rho{}";
        public static readonly string LatexRhoLower = @"\rho{}";
        public static readonly string LatexSigma = @"\Sigma{}";
        //public static readonly string LatexSigmafLower = @"\sigmaf{}";
        public static readonly string LatexSigmaLower = @"\sigma{}";
        public static readonly string LatexTau = @"\Tau{}";
        public static readonly string LatexTauLower = @"\tau{}";
        public static readonly string LatexTheta = @"\Theta{}";
        public static readonly string LatexThetaLower = @"\theta{}";
        public static readonly string LatexUpsilon = @"\Upsilon{}";
        public static readonly string LatexUpsilonLower = @"\upsilon{}";
        public static readonly string LatexXi = @"\Xi{}";
        public static readonly string LatexXiLower = @"\xi{}";
        public static readonly string LatexZeta = @"\Zeta{}";
        public static readonly string LatexZetaLower = @"\zeta{}";
        public static readonly string Mu = "\u039C";
        public static readonly string MuLower = "\u03BC";
        public static readonly string Nu = "\u039D";
        public static readonly string NuLower = "\u03BD";
        public static readonly string Omega = "\u03A9";
        public static readonly string OmegaLower = "\u03C9";
        public static readonly string Omicron = "\u039F";
        public static readonly string OmicronLower = "\u03BF";
        public static readonly string Phi = "\u03A6";
        public static readonly string PhiLower = "\u03C6";
        public static readonly string Pi = "\u03A0";
        public static readonly string PiLower = "\u03C0";
        public static readonly string Psi = "\u03A8";
        public static readonly string PsiLower = "\u03C8";
        public static readonly string Rho = "\u03A1";
        public static readonly string RhoLower = "\u03C1";
        public static readonly string Sigma = "\u03A3";
        //public static readonly string Sigmaf = "\u03C2";
        public static readonly string SigmaLower = "\u03C3";
        public static readonly string Tau = "\u03A4";
        public static readonly string TauLower = "\u03C4";
        public static readonly string Theta = "\u0398";
        public static readonly string ThetaLower = "\u03B8";
        public static readonly string Upsilon = "\u03A5";
        public static readonly string UpsilonLower = "\u03C5";
        public static readonly string Xi = "\u039E";
        public static readonly string XiLower = "\u03BE";
        public static readonly string Zeta = "\u0396";
        public static readonly string ZetaLower = "\u03B6";

        static Depiction()
        {
            LatexGreekToOMath = new Dictionary<string, string>
                                    {
                                        {LatexAlpha, Alpha},
                                        {LatexBeta, Beta},
                                        {LatexGamma, Gamma},
                                        {LatexDelta, Delta},
                                        {LatexEpsilon, Epsilon},
                                        {LatexZeta, Zeta},
                                        {LatexEta, Eta},
                                        {LatexTheta, Theta},
                                        {LatexIota, Iota},
                                        {LatexKappa, Kappa},
                                        {LatexLambda, Lambda},
                                        {LatexMu, Mu},
                                        {LatexNu, Nu},
                                        {LatexXi, Xi},
                                        {LatexOmicron, Omicron},
                                        {LatexPi, Pi},
                                        {LatexRho, Rho},
                                        {LatexSigma, Sigma},
                                        {LatexTau, Tau},
                                        {LatexUpsilon, Upsilon},
                                        {LatexPhi, Phi},
                                        {LatexChi, Chi},
                                        {LatexPsi, Psi},
                                        {LatexOmega, Omega},
                                        {LatexAlphaLower, AlphaLower},
                                        {LatexBetaLower, BetaLower},
                                        {LatexGammaLower, GammaLower},
                                        {LatexDeltaLower, DeltaLower},
                                        {LatexEpsilonLower, EpsilonLower},
                                        {LatexZetaLower, ZetaLower},
                                        {LatexEtaLower, EtaLower},
                                        {LatexThetaLower, ThetaLower},
                                        {LatexIotaLower, IotaLower},
                                        {LatexKappaLower, KappaLower},
                                        {LatexLambdaLower, LambdaLower},
                                        {LatexMuLower, MuLower},
                                        {LatexNuLower, NuLower},
                                        {LatexXiLower, XiLower},
                                        {LatexOmicronLower, OmicronLower},
                                        {LatexPiLower, PiLower},
                                        {LatexRhoLower, RhoLower},
                                        {LatexSigmaLower, SigmaLower},
                                        {LatexTauLower, TauLower},
                                        {LatexUpsilonLower, UpsilonLower},
                                        {LatexPhiLower, PhiLower},
                                        {LatexChiLower, ChiLower},
                                        {LatexPsiLower, PsiLower},
                                        {LatexOmegaLower, OmegaLower},
                                        {LatexHyphen, "\u002D"}
                                    };
        }

        /// <summary>
        /// Prevent instantiation of the class. All methods should be accessed statically.
        /// </summary>
        private Depiction()
        {}

        /// <summary>
        /// Calculate and return an ordered list of all the possible depictions of the CML in the contextObject.
        /// Convenience method of PossibleDepictionOptions(ContextObject, RootElementOfCML)
        /// 
        /// Possible DepictionOptions are 2D, chemical names, concise formula, inline formulae
        /// </summary>
        /// <param name="contextObject">The context object containing the CML and ChemSS</param>
        /// <returns></returns>
        public static IEnumerable<DepictionOption> PossibleDepictionOptions(ContextObject contextObject)
        {
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            return PossibleDepictionOptions(contextObject, contextObject.Cml.Root);
        }

        /// <summary>
        /// Calculate and return a list of all the possible depictions of the CML in the contextObject
        /// below the pointer given
        ///
        /// Possible DepictionOptions are chemical names, formulae, molecules and "bold numbers"
        /// </summary>
        /// <param name="contextObject">The context object containing the CML and ChemSS</param>
        /// <param name="pointer">The eldest element to consider as a possible depiction</param>
        /// <returns></returns>
        public static IEnumerable<DepictionOption> PossibleDepictionOptions(ContextObject contextObject,
                                                                            XElement pointer)
        {
            // Check null argument
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }
            if (pointer == null)
            {
                throw new ArgumentNullException("pointer");
            }

            // Check if the pointer is in the context object
            int count = 0;
            if (contextObject.Cml.Root != null)
            {
                count = (contextObject.Cml.Root.DescendantsAndSelf(pointer.Name).Where(
                    e => e.ToString() == pointer.ToString())).Count();
            }
            if (count == 0)
            {
                throw new ArgumentException("pointer is not from the CML from this context object");
            }

            // Manipulate 2D depiction
            var twoDDepictions =
                pointer.DescendantsAndSelf(CmlConstants.CmlxNamespace + CmlMolecule.Tag).Where(
                    e => (from atom in e.Descendants(CmlConstants.CmlxNamespace + CmlAtom.Tag)
                          where atom.Attribute("x2") != null && atom.Attribute("y2") != null
                          select atom).Count() > 0);

            var result = twoDDepictions.Select(e => new DepictionOption(e)).ToList();
            var labels =
                pointer.XPathSelectElements(".//*[local-name()='" + CmlLabel.Tag + "' and namespace-uri()='" +
                                            CmlConstants.Cmlns + "' ]");
            result.AddRange(from element in labels
                            where CmlLabel.IsLabel(element)
                            let label = new CmlLabel(element)
                            let cmlDictPrefix = element.GetPrefixOfNamespace(CmlConstants.CmlDictNS)
                            where string.Format(CultureInfo.InvariantCulture, "{0}:{1}", cmlDictPrefix, CmlLabel.MoleculeBoldNumber).Equals(label.DictRef)
                            select new DepictionOption(element));

            // Manipulate 1D depiction
            var oneDDepictions =
                from e in pointer.DescendantsAndSelf(CmlConstants.CmlxNamespace + CmlName.Tag)
                select e;
            result.AddRange(oneDDepictions.Select(e => new DepictionOption(e)));

            oneDDepictions = from e in pointer.DescendantsAndSelf(CmlConstants.CmlxNamespace + CmlFormula.Tag)
                             select e;

            foreach (var e in oneDDepictions)
            {
                // concise formula comes before inline for preference
                var concise = e.Attribute("concise");
                if (concise != null)
                {
                    var newDepictionOpt = new DepictionOption(concise);
                    if (!result.Contains<DepictionOption>(newDepictionOpt))
                    {
                        result.Add(newDepictionOpt);
                    }
                }
                var inline = e.Attribute("inline");
                if (inline != null)
                {
                    var newDepictionOpt = new DepictionOption(inline);
                    if (!result.Contains<DepictionOption>(newDepictionOpt))
                    {
                        result.Add(newDepictionOpt);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Determines whether the supplied depiction option is a name 
        /// </summary>
        /// <param name="depictionOption">the DepictionOption to test</param>
        /// <returns>true if depictionOption is a name otherwise false</returns>
        public static bool IsName(DepictionOption depictionOption)
        {
            if (depictionOption == null)
            {
                return false;
            }
            var xElement = depictionOption.MachineUnderstandableOption as XElement;
            if (xElement == null)
            {
                return false;
            }
            var xName = CmlConstants.CmlxNamespace + CmlName.Tag;
            return xName.Equals(xElement.Name);
        }

        /// <summary>
        /// Determines whether the supplied depiction option is a label
        /// </summary>
        /// <param name="depictionOption">the DepictionOption to test</param>
        /// <returns>true if depictionOption is a name otherwise false</returns>
        public static bool IsLabel(DepictionOption depictionOption)
        {
            if (depictionOption == null)
            {
                return false;
            }
            var xElement = depictionOption.MachineUnderstandableOption as XElement;
            if (xElement == null)
            {
                return false;
            }
            var xName = CmlConstants.CmlxNamespace + CmlLabel.Tag;
            return xName.Equals(xElement.Name);
        }

        /// <summary>
        /// Determines whether the supplied depiction option is a bold number label
        /// </summary>
        /// <param name="depictionOption">the DepictionOption to test</param>
        /// <returns>true if depictionOption is a a bold number label otherwise false</returns>
        public static bool IsMoleculeBoldNumberLabel(DepictionOption depictionOption)
        {
            if (IsLabel(depictionOption))
            {
                var xElement = (XElement) depictionOption.MachineUnderstandableOption;
                var prefix = xElement.GetPrefixOfNamespace(CmlConstants.CmlDictNS);
                if (prefix != null)
                {
                    var dictRefAttribute = xElement.Attribute(CmlAttribute.DictRef);
                    if (dictRefAttribute != null)
                    {
                        return (prefix + ":" + CmlLabel.MoleculeBoldNumber).Equals(dictRefAttribute.Value);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether the supplied depiction option is a formula
        /// </summary>
        /// <param name="depictionOption">the DepictionOption to test</param>
        /// <returns>true if depictionOption is a formula otherwise false</returns>
        public static bool IsFormula(DepictionOption depictionOption)
        {
            if (depictionOption == null)
            {
                return false;
            }
            var xAttribute = depictionOption.MachineUnderstandableOption as XAttribute;
            if (xAttribute == null)
            {
                return false;
            }
            var xElement = xAttribute.Parent;
            if (xElement == null)
            {
                return false;
            }
            var xName = CmlConstants.CmlxNamespace + CmlFormula.Tag;
            return xName.Equals(xElement.Name);
        }

        /// <summary>
        /// Determines whether the supplied depiction option is a concise formula
        /// </summary>
        /// <param name="depictionOption">the DepictionOption to test</param>
        /// <returns>true if depictionOption is a concise formula otherwise false</returns>
        public static bool IsConciseFormula(DepictionOption depictionOption)
        {
            if (IsFormula(depictionOption))
            {
                var xAttribute = depictionOption.MachineUnderstandableOption as XAttribute;
                XName xName = CmlAttribute.Concise;
                return xName.Equals(xAttribute.Name);
            }
            return false;
        }

        /// <summary>
        /// Determines whether the supplied depiction option is an inline formula
        /// </summary>
        /// <param name="depictionOption">the DepictionOption to test</param>
        /// <returns>true if depictionOption is an inline formula otherwise false</returns>
        public static bool IsInlineFormula(DepictionOption depictionOption)
        {
            if (IsFormula(depictionOption))
            {
                var xAttribute = depictionOption.MachineUnderstandableOption as XAttribute;
                XName xName = CmlAttribute.Inline;
                return xName.Equals(xAttribute.Name);
            }
            return false;
        }

        /// <summary>
        /// Determines whether the supplied depiction option is 2D
        /// </summary>
        /// <param name="depictionOption">the DepictionOption to test</param>
        /// <returns>true if depictionOption is a 2D otherwise false</returns>
        public static bool Is2D(DepictionOption depictionOption)
        {
            if (depictionOption == null)
            {
                return false;
            }
            var xElement = depictionOption.MachineUnderstandableOption as XElement;
            if (xElement == null)
            {
                return false;
            }
            var xName = CmlConstants.CmlxNamespace + CmlMolecule.Tag;
            return xName.Equals(xElement.Name);
        }

        public static string ReplaceGreekAsOMath(string incoming)
        {
            var builder = new StringBuilder(incoming);
            foreach (var nameToUnicode in LatexGreekToOMath)
            {
                builder.Replace(nameToUnicode.Key, nameToUnicode.Value);
            }
            return builder.ToString();
        }
    }
}