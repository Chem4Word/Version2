// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Office.Interop.Word;

namespace Chem4Word.UI.Tools
{
    public static class TextTools
    {
        private const char Backslash = '\u005C';
        private const char Closebracket = '}';
        private const char Openbracket = '{';

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
        public static readonly string LatexIota = @"\Iota{}";
        public static readonly string LatexIotaLower = @"\iota{}";
        public static readonly string LatexKappa = @"\Kappa{}";
        public static readonly string LatexKappaLower = @"\kappa{}";
        public static readonly string LatexLambda = @"\Lambda{}";
        public static readonly string LatexLambdaLower = @"\lambda{}";
        public static readonly string LatexMinus = @"\minus{}";
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
        public static readonly string LatexPI = @"\Pi{}";
        public static readonly string LatexPILower = @"\pi{}";
        public static readonly string LatexPsi = @"\Psi{}";
        public static readonly string LatexPsiLower = @"\psi{}";
        public static readonly string LatexRho = @"\Rho{}";
        public static readonly string LatexRhoLower = @"\rho{}";
        public static readonly string LatexSigma = @"\Sigma{}";
        public static readonly string LatexSigmaf = @"\sigmaf{}";
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
        public static readonly string Minus = "\u2212";
        public static readonly string Mu = "\u039C";
        public static readonly string MuLower = "\u03BC";
        public static readonly string Nu = "\u039D";
        public static readonly string NuLower = "\u03BD";
        public static readonly string Omega = "\u03A9";
        public static readonly string OmegaLower = "\u03C9";
        public static readonly string OmicornLower = "\u03BF";
        public static readonly string Omicron = "\u039F";
        public static readonly string Phi = "\u03A6";
        public static readonly string PhiLower = "\u03C6";
        public static readonly string Pi = "\u03A0";
        public static readonly string PiLower = "\u03C0";
        public static readonly string Psi = "\u03A8";
        public static readonly string PsiLower = "\u03C8";
        public static readonly string Rho = "\u03A1";
        public static readonly string RhoLower = "\u03C1";
        public static readonly string Sigma = "\u03A3";
        public static readonly string Sigmaf = "\u03C2";
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


        public static readonly Dictionary<string, string> LatexGreekToUnicode;
        public static readonly Dictionary<string, string> LatexMiscToUnicode;
        private static readonly string TextBoxFont = "Calibri";

        static TextTools()
        {
            LatexGreekToUnicode = new Dictionary<string, string>
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
                                          {LatexPI, Pi},
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
                                          {LatexOmicronLower, OmicornLower},
                                          {LatexPILower, PiLower},
                                          {LatexRhoLower, RhoLower},
                                          {LatexSigmaf, Sigmaf},
                                          {LatexSigmaLower, SigmaLower},
                                          {LatexTauLower, TauLower},
                                          {LatexUpsilonLower, UpsilonLower},
                                          {LatexPhiLower, PhiLower},
                                          {LatexChiLower, ChiLower},
                                          {LatexPsiLower, PsiLower},
                                          {LatexOmegaLower, OmegaLower},
                                      };

            LatexMiscToUnicode = new Dictionary<string, string>
                                     {
                                         {LatexBullet, Bullet},
                                         {LatexDagger, Dagger},
                                         {LatexDoubleDagger, DoubleDagger},
                                         {LatexMinus, Minus},
                                     };
        }

        private static string ReplaceMisc(string incoming)
        {
            StringBuilder builder = new StringBuilder(incoming);
            foreach (KeyValuePair<string, string> nameToUnicode in LatexMiscToUnicode)
            {
                builder.Replace(nameToUnicode.Key, nameToUnicode.Value);
            }
            return builder.ToString();
        }

        private static string ReplaceGreek(string incoming)
        {
            StringBuilder builder = new StringBuilder(incoming);
            foreach (KeyValuePair<string, string> nameToUnicode in LatexGreekToUnicode)
            {
                builder.Replace(nameToUnicode.Key, nameToUnicode.Value);
            }
            return builder.ToString();
        }

        /// <summary>
        /// this method will also set the font so that it will display
        /// </summary>
        /// <param name="textBlock"></param>
        /// <param name="latex"></param>
        public static void ConvertLatexFormattedStringToTextBlock(ref TextBlock textBlock, string latex)
        {
            textBlock.FontFamily = new FontFamily(TextBoxFont);
            char[] chars = ReplaceMisc(ReplaceGreek(latex)).ToCharArray();
            bool backSlash = false;
            int length = chars.Length;
            StringBuilder builder = new StringBuilder(length);
            Run currentRun = new Run();
            for (int pos = 0; pos < length; pos++)
            {
                if (backSlash)
                {
                    // whatever follows a backslash should be put straight into the string 
                    builder.Append(chars[pos]);
                    backSlash = false;
                }
                else
                {
                    switch (chars[pos])
                    {
                        case Backslash:
                            backSlash = true;
                            break;
                        case '^':
                            if (pos < length - 1 && Openbracket.Equals(chars[pos + 1]))
                            {
                                currentRun.Text = builder.ToString();
                                textBlock.Inlines.Add(currentRun);
                                builder = new StringBuilder(length);
                                currentRun = new Run();
                                currentRun.Typography.Variants = FontVariants.Superscript;
                                pos ++;
                            }
                            else
                            {
                                builder.Append(chars[pos]);
                            }
                            break;
                        case '_':
                            if (pos < length - 1 && Openbracket.Equals(chars[pos + 1]))
                            {
                                currentRun.Text = builder.ToString();
                                textBlock.Inlines.Add(currentRun);
                                builder = new StringBuilder(length);
                                currentRun = new Run();
                                currentRun.Typography.Variants = FontVariants.Subscript;
                                pos ++;
                            }
                            else
                            {
                                builder.Append(chars[pos]);
                            }
                            break;
                        case Closebracket:
                            currentRun.Text = builder.ToString();
                            textBlock.Inlines.Add(currentRun);
                            builder = new StringBuilder(length);
                            currentRun = new Run();
                            break;
                        case Openbracket:
                            // we skip these 
                            // if they were \{ then we have dealt with them
                            // if they were _{ or ^{ we have dealt with them
                            break;
                        default:
                            builder.Append(chars[pos]);
                            break;
                    }
                }
                if (pos == length - 1 && builder.Length > 0)
                {
                    currentRun.Text = builder.ToString();
                    textBlock.Inlines.Add(currentRun);
                }
            }
        }

        /// <summary>
        /// Converts the text in the Range to the appropriate CML Latex like markup 
        /// for inclusion in a CML file (either as name, label, inline formula).
        /// 
        /// NOTE
        /// Currently only superscript and subscript formatting is maintained.
        /// </summary>
        /// <param name="range">the Range containing the text to convert</param>
        /// <returns>the string representing the text in the range converting Word formatting to CML Latex style</returns>
        public static string ConvertWordMarkupToLatexStyle(Range range)
        {
            StringBuilder builder = new StringBuilder();
            const string superStart = "^{";
            const string subStart = "_{";
            const string end = "}";
            bool super = false;
            bool sub = false;
            int length = range.Characters.Count;
            bool include = true;
            for (int i = 1; i <= length; i++)
            {
                string curr = range.Characters[i].Text;

                if (i == length)
                {
                    if (string.Equals("\r", curr, StringComparison.Ordinal))
                    {
                        include = false;
                    }
                }
                else
                {
                    include = true;
                }

                if (include)
                {
                    var bracket = (string.Equals("}", curr, StringComparison.OrdinalIgnoreCase) || string.Equals("{", curr, StringComparison.Ordinal));
                    if (range.Characters[i].Font.Subscript == 0 && range.Characters[i].Font.Superscript == 0)
                    {
                        // neither superscript nor supscript so must be normal
                        if (sub || super)
                        {
                            // then previous character must have been sub or super
                            // so we need to close that bracket before adding this character 
                            builder.Append(end);
                            sub = false;
                            super = false;
                        }
                        if (bracket)
                        {
                            builder.Append(@"\");
                        }
                        builder.Append(curr);
                    }
                    else if (range.Characters[i].Font.Superscript != 0)
                    {
                        // the current character is superscripted
                        if (super)
                        {
                            // then the previous character was also superscripted
                            sub = false;
                        }
                        else
                        {
                            // previous character was not super 
                            if (sub)
                            {
                                // previous character was sub - 
                                // so we need to close that bracket before we open the super
                                builder.Append(end);
                            }
                            builder.Append(superStart);
                        }
                        super = true;
                        if (bracket)
                        {
                            builder.Append(@"\");
                        }
                        builder.Append(range.Characters[i].Text);
                    }
                    else if (range.Characters[i].Font.Subscript != 0)
                    {
                        // the current character is subscripted
                        if (sub)
                        {
                            // then the previous character was also subscripted
                            super = false;
                        }
                        else
                        {
                            // previous character was not sub 
                            if (super)
                            {
                                // previous character was super - 
                                // so we need to close that bracket before we open the super
                                builder.Append(end);
                            }
                            builder.Append(subStart);
                        }
                        sub = true;
                        if (bracket)
                        {
                            builder.Append(@"\");
                        }
                        builder.Append(range.Characters[i].Text);
                    }
                }
            }
            // now finished ... but there may still be a bracket open (if we finished with super or sub)
            if (super || sub)
            {
                builder.Append(end);
            }
            return builder.ToString();
        }
    }
}