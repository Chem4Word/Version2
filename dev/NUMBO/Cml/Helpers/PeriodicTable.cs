// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Numbo.Cml.Helpers
{
    public static class PeriodicTable
    {
        #region Element enum

        /// <summary>
        /// enumeration of chemical elements in symbolic form.
        /// </summary>
        public enum Element
        {
            R,
            Du,
            H,
            He,
            Li,
            Be,
            B,
            C,
            N,
            O,
            F,
            Ne,
            Na,
            Mg,
            Al,
            Si,
            P,
            S,
            Cl,
            Ar,
            K,
            Ca,
            Sc,
            Ti,
            V,
            Cr,
            Mn,
            Fe,
            Co,
            Ni,
            Cu,
            Zn,
            Ga,
            Ge,
            As,
            Se,
            Br,
            Kr,
            Rb,
            Sr,
            Y,
            Zr,
            Nb,
            Mo,
            Tc,
            Ru,
            Rh,
            Pd,
            Ag,
            Cd,
            In,
            Sn,
            Sb,
            Te,
            I,
            Xe,
            Cs,
            Ba,
            La,
            Ce,
            Pr,
            Nd,
            Pm,
            Sm,
            Eu,
            Gd,
            Tb,
            Dy,
            Ho,
            Er,
            Tm,
            Yb,
            Lu,
            Hf,
            Ta,
            W,
            Re,
            Os,
            Ir,
            Pt,
            Au,
            Hg,
            Tl,
            Pb,
            Bi,
            Po,
            At,
            Rn,
            Fr,
            Ra,
            Ac,
            Th,
            Pa,
            U,
            Np,
            Pu,
            Am,
            Cm,
            Bk,
            Cf,
            Es,
            Fm,
            Md,
            No,
            Lr,
            Rf,
            Db,
            Sg,
            Bh,
            Hs,
            Mt,
            Ds,
            Rg,
            Uub,
            Uut,
            Uuq,
            Uup,
            Uuh,
            Uus,
            Null
        } ;

        #endregion

        #region Type enum

        public enum Type
        {
            /// <summary>
            /// transition metal
            /// </summary>
            TransitionMetal,
            /// <summary>
            /// pblock top right
            /// </summary>
            PBlock,
            /// <summary>
            /// metal any sort
            /// </summary>
            Metal,
            /// <summary>
            /// metal any sort
            /// </summary>
            MetalNotSemiMetal,
            /// <summary>
            /// group A (number to be given separately)
            /// </summary>
            GroupA,
            /// <summary>
            /// group B (number to be given separately)
            /// </summary>
            GroupB,
            /// <summary>
            /// row (number to be given separately)
            /// </summary>
            Row,
            /// <summary>
            /// lanthanide
            /// </summary>
            Lanthanide,
            /// <summary>
            /// actinide
            /// </summary>
            Actinide,
            /// <summary>
            /// non-metal
            /// </summary>
            NonMetal,
            /// <summary>
            /// semi-metal
            /// </summary>
            SemiMetal,
            /// <summary>
            /// halogen
            /// </summary>
            Halogen
        }

        #endregion

        /// <summary>
        /// Hashtable of elements, indexed by atomicSymbol.
        /// </summary>
        public static Dictionary<Element, ChemicalElement> PTable;

        ///<summary>
        ///</summary>
        ///<param name="elementType"></param>
        ///<returns></returns>
        ///<exception cref="ArgumentNullException">if elementType is null</exception>
        ///<exception cref="ArgumentException">if elementType is not a valid Element</exception>
        public static Element GetElement(string elementType)
        {
            return (Element) Enum.Parse(typeof (Element), elementType, false);
        }

        ///<summary>
        /// Converts the string representation of a Chemcial Element to its PeridoicTable.Element equivalent. 
        /// A return value indicates whether the conversion succeeded or failed. 
        ///</summary>
        ///<param name="s">A string containing a Chemical Element to convert.</param>
        ///<param name="element">When this method returns, contains a PeridoicTable.Element equivalent to the symbol contained in s, 
        /// if the conversion succeeded, or Element.Null if the conversion failed. The conversion fails if the s parameter is null, 
        /// is not a valid Chemical Element. This parameter is passed uninitialized.</param>
        ///<returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParseElement(string s, out Element element)
        {
            bool ok = false;
            try
            {
                element = (Element) Enum.Parse(typeof (Element), s, false);
                ok = true;
            }
            catch (ArgumentException)
            {
                element = Element.Null;
            }
            return ok;
        }

        ///<summary>
        /// DEWISOTT
        ///</summary>
        ///<returns></returns>
        public static Dictionary<Element, ChemicalElement> GetPeriodicTableIndexedByElement()
        {
            if (PTable == null)
            {
                PTable = new Dictionary<Element, ChemicalElement>
                             {
                                 {Element.R, new ChemicalElement("R", 0, 0.0)},
                                 {Element.Du, new ChemicalElement("Du", 0, 0.0)},
                                 {Element.H, new ChemicalElement("H", 1, 1.00794)},
                                 {Element.He, new ChemicalElement("He", 2, 4.002602)},
                                 {Element.Li, new ChemicalElement("Li", 3, 6.941)},
                                 {Element.Be, new ChemicalElement("Be", 4, 9.012182)},
                                 {Element.B, new ChemicalElement("B", 5, 10.811)},
                                 {Element.C, new ChemicalElement("C", 6, 12.0107)},
                                 {Element.N, new ChemicalElement("N", 7, 14.0067)},
                                 {Element.O, new ChemicalElement("O", 8, 15.9994)},
                                 {Element.F, new ChemicalElement("F", 9, 18.9984032)},
                                 {Element.Ne, new ChemicalElement("Ne", 10, 20.1797)},
                                 {Element.Na, new ChemicalElement("Na", 11, 22.98977)},
                                 {Element.Mg, new ChemicalElement("Mg", 12, 24.305)},
                                 {Element.Al, new ChemicalElement("Al", 13, 26.981538)},
                                 {Element.Si, new ChemicalElement("Si", 14, 28.0855)},
                                 {Element.P, new ChemicalElement("P", 15, 30.973761)},
                                 {Element.S, new ChemicalElement("S", 16, 32.065)},
                                 {Element.Cl, new ChemicalElement("Cl", 17, 35.453)},
                                 {Element.Ar, new ChemicalElement("Ar", 18, 39.948)},
                                 {Element.K, new ChemicalElement("K", 19, 39.0983)},
                                 {Element.Ca, new ChemicalElement("Ca", 20, 40.078)},
                                 {Element.Sc, new ChemicalElement("Sc", 21, 44.95591)},
                                 {Element.Ti, new ChemicalElement("Ti", 22, 47.867)},
                                 {Element.V, new ChemicalElement("V", 23, 50.9415)},
                                 {Element.Cr, new ChemicalElement("Cr", 24, 51.9961)},
                                 {Element.Mn, new ChemicalElement("Mn", 25, 54.938049)},
                                 {Element.Fe, new ChemicalElement("Fe", 26, 55.845)},
                                 {Element.Co, new ChemicalElement("Co", 27, 58.9332)},
                                 {Element.Ni, new ChemicalElement("Ni", 28, 58.6934)},
                                 {Element.Cu, new ChemicalElement("Cu", 29, 63.546)},
                                 {Element.Zn, new ChemicalElement("Zn", 30, 65.409)},
                                 {Element.Ga, new ChemicalElement("Ga", 31, 69.723)},
                                 {Element.Ge, new ChemicalElement("Ge", 32, 72.64)},
                                 {Element.As, new ChemicalElement("As", 33, 74.9216)},
                                 {Element.Se, new ChemicalElement("Se", 34, 78.96)},
                                 {Element.Br, new ChemicalElement("Br", 35, 79.904)},
                                 {Element.Kr, new ChemicalElement("Kr", 36, 83.798)},
                                 {Element.Rb, new ChemicalElement("Rb", 37, 85.4678)},
                                 {Element.Sr, new ChemicalElement("Sr", 38, 87.62)},
                                 {Element.Y, new ChemicalElement("Y", 39, 88.90585)},
                                 {Element.Zr, new ChemicalElement("Zr", 40, 91.224)},
                                 {Element.Nb, new ChemicalElement("Nb", 41, 92.90638)},
                                 {Element.Mo, new ChemicalElement("Mo", 42, 95.94)},
                                 {Element.Tc, new ChemicalElement("Tc", 43, 98.0)},
                                 {Element.Ru, new ChemicalElement("Ru", 44, 101.07)},
                                 {Element.Rh, new ChemicalElement("Rh", 45, 102.9055)},
                                 {Element.Pd, new ChemicalElement("Pd", 46, 106.42)},
                                 {Element.Ag, new ChemicalElement("Ag", 47, 107.8682)},
                                 {Element.Cd, new ChemicalElement("Cd", 48, 112.411)},
                                 {Element.In, new ChemicalElement("In", 49, 114.818)},
                                 {Element.Sn, new ChemicalElement("Sn", 50, 118.71)},
                                 {Element.Sb, new ChemicalElement("Sb", 51, 121.76)},
                                 {Element.Te, new ChemicalElement("Te", 52, 127.6)},
                                 {Element.I, new ChemicalElement("I", 53, 126.90447)},
                                 {Element.Xe, new ChemicalElement("Xe", 54, 131.293)},
                                 {Element.Cs, new ChemicalElement("Cs", 55, 132.90545)},
                                 {Element.Ba, new ChemicalElement("Ba", 56, 137.327)},
                                 {Element.La, new ChemicalElement("La", 57, 138.9055)},
                                 {Element.Ce, new ChemicalElement("Ce", 58, 140.116)},
                                 {Element.Pr, new ChemicalElement("Pr", 59, 140.90765)},
                                 {Element.Nd, new ChemicalElement("Nd", 60, 144.24)},
                                 {Element.Pm, new ChemicalElement("Pm", 61, 145.0)},
                                 {Element.Sm, new ChemicalElement("Sm", 62, 150.36)},
                                 {Element.Eu, new ChemicalElement("Eu", 63, 151.964)},
                                 {Element.Gd, new ChemicalElement("Gd", 64, 157.25)},
                                 {Element.Tb, new ChemicalElement("Tb", 65, 158.92534)},
                                 {Element.Dy, new ChemicalElement("Dy", 66, 162.5)},
                                 {Element.Ho, new ChemicalElement("Ho", 67, 164.93032)},
                                 {Element.Er, new ChemicalElement("Er", 68, 167.259)},
                                 {Element.Tm, new ChemicalElement("Tm", 69, 168.93421)},
                                 {Element.Yb, new ChemicalElement("Yb", 70, 173.04)},
                                 {Element.Lu, new ChemicalElement("Lu", 71, 174.967)},
                                 {Element.Hf, new ChemicalElement("Hf", 72, 178.49)},
                                 {Element.Ta, new ChemicalElement("Ta", 73, 180.9479)},
                                 {Element.W, new ChemicalElement("W", 74, 183.84)},
                                 {Element.Re, new ChemicalElement("Re", 75, 186.207)},
                                 {Element.Os, new ChemicalElement("Os", 76, 190.23)},
                                 {Element.Ir, new ChemicalElement("Ir", 77, 192.217)},
                                 {Element.Pt, new ChemicalElement("Pt", 78, 195.078)},
                                 {Element.Au, new ChemicalElement("Au", 79, 196.96655)},
                                 {Element.Hg, new ChemicalElement("Hg", 80, 200.59)},
                                 {Element.Tl, new ChemicalElement("Tl", 81, 204.3833)},
                                 {Element.Pb, new ChemicalElement("Pb", 82, 207.2)},
                                 {Element.Bi, new ChemicalElement("Bi", 83, 208.98038)},
                                 {Element.Po, new ChemicalElement("Po", 84, 209.0)},
                                 {Element.At, new ChemicalElement("At", 85, 210.0)},
                                 {Element.Rn, new ChemicalElement("Rn", 86, 222.0)},
                                 {Element.Fr, new ChemicalElement("Fr", 87, 223.0)},
                                 {Element.Ra, new ChemicalElement("Ra", 88, 226.0)},
                                 {Element.Ac, new ChemicalElement("Ac", 89, 227.0)},
                                 {Element.Th, new ChemicalElement("Th", 90, 232.0381)},
                                 {Element.Pa, new ChemicalElement("Pa", 91, 231.03588)},
                                 {Element.U, new ChemicalElement("U", 92, 238.02891)},
                                 {Element.Np, new ChemicalElement("Np", 93, 237.0)},
                                 {Element.Pu, new ChemicalElement("Pu", 94, 244.0)},
                                 {Element.Am, new ChemicalElement("Am", 95, 243.0)},
                                 {Element.Cm, new ChemicalElement("Cm", 96, 247.0)},
                                 {Element.Bk, new ChemicalElement("Bk", 97, 247.0)},
                                 {Element.Cf, new ChemicalElement("Cf", 98, 251.0)},
                                 {Element.Es, new ChemicalElement("Es", 99, 252.0)},
                                 {Element.Fm, new ChemicalElement("Fm", 100, 257.0)},
                                 {Element.Md, new ChemicalElement("Md", 101, 258.0)},
                                 {Element.No, new ChemicalElement("No", 102, 259.0)},
                                 {Element.Lr, new ChemicalElement("Lr", 103, 262.0)},
                                 {Element.Rf, new ChemicalElement("Rf", 104, 261.0)},
                                 {Element.Db, new ChemicalElement("Db", 105, 262.0)},
                                 {Element.Sg, new ChemicalElement("Sg", 106, 266.0)},
                                 {Element.Bh, new ChemicalElement("Bh", 107, 264.0)},
                                 {Element.Hs, new ChemicalElement("Hs", 108, 269.0)},
                                 {Element.Mt, new ChemicalElement("Mt", 109, 268.0)},
                                 {Element.Ds, new ChemicalElement("Ds", 110, 271.0)},
                                 {Element.Rg, new ChemicalElement("Rg", 111, 272.0)},
                                 {Element.Uub, new ChemicalElement("Uub", 112, 285.0)},
                                 {Element.Uut, new ChemicalElement("Uut", 113, 284.0)},
                                 {Element.Uuq, new ChemicalElement("Uuq", 114, 289.0)},
                                 {Element.Uup, new ChemicalElement("Uup", 115, 288.0)},
                                 {Element.Uuh, new ChemicalElement("Uuh", 116, 292.0)},
                                 {Element.Uus, new ChemicalElement("Uus", 117, 294.0)}
                             };
                AddIsotopes();
            }
            return PTable;
        }

        private static void AddIsotopes()
        {
            AddIsotopes(Element.H, new[] {1, 2, 3});
            AddIsotopes(Element.He, new[] {3, 4});

            AddIsotopes(Element.B, new[] {9, 11});
            AddIsotopes(Element.C, new[] {11, 12, 13, 14});
            AddIsotopes(Element.N, new[] {14, 15});
            AddIsotopes(Element.O, new[] {16, 17, 18});
            AddIsotopes(Element.F, new[] {19});

            AddIsotopes(Element.Cl, new[] {35, 37});
        }

        private static void AddIsotopes(Element el, int[] masses)
        {
            ChemicalElement chemicalElement;
            PTable.TryGetValue(el, out chemicalElement);
            chemicalElement.IsotopeMasses = masses;
        }

        /// <summary>
        /// GET Group
        /// 
        /// not all elements are done yet (lacking lanthanides and actinides and 104 +)
        /// </summary>
        /// <param name="element"></param>
        /// <returns>-1 if not in list</returns>
        public static int GetGroup(Element element)
        {
            int group;
            switch (element)
            {
                case (Element.H):
                    group = 1;
                    break;
                case (Element.He):
                    group = 18;
                    break;
                case (Element.Li):
                    group = 1;
                    break;
                case (Element.Be):
                    group = 2;
                    break;
                case (Element.B):
                    group = 13;
                    break;
                case (Element.C):
                    group = 14;
                    break;
                case (Element.N):
                    group = 15;
                    break;
                case (Element.O):
                    group = 16;
                    break;
                case (Element.F):
                    group = 17;
                    break;
                case (Element.Ne):
                    group = 18;
                    break;
                case (Element.Na):
                    group = 1;
                    break;
                case (Element.Mg):
                    group = 2;
                    break;
                case (Element.Al):
                    group = 13;
                    break;
                case (Element.Si):
                    group = 14;
                    break;
                case (Element.P):
                    group = 15;
                    break;
                case (Element.S):
                    group = 16;
                    break;
                case (Element.Cl):
                    group = 17;
                    break;
                case (Element.Ar):
                    group = 18;
                    break;
                case (Element.K):
                    group = 1;
                    break;
                case (Element.Ca):
                    group = 2;
                    break;
                case (Element.Sc):
                    group = 3;
                    break;
                case (Element.Ti):
                    group = 4;
                    break;
                case (Element.V):
                    group = 5;
                    break;
                case (Element.Cr):
                    group = 6;
                    break;
                case (Element.Mn):
                    group = 7;
                    break;
                case (Element.Fe):
                    group = 8;
                    break;
                case (Element.Co):
                    group = 9;
                    break;
                case (Element.Ni):
                    group = 10;
                    break;
                case (Element.Cu):
                    group = 11;
                    break;
                case (Element.Zn):
                    group = 12;
                    break;
                case (Element.Ga):
                    group = 13;
                    break;
                case (Element.Ge):
                    group = 14;
                    break;
                case (Element.As):
                    group = 15;
                    break;
                case (Element.Se):
                    group = 16;
                    break;
                case (Element.Br):
                    group = 17;
                    break;
                case (Element.Kr):
                    group = 18;
                    break;
                case (Element.Rb):
                    group = 1;
                    break;
                case (Element.Sr):
                    group = 2;
                    break;
                case (Element.Y):
                    group = 3;
                    break;
                case (Element.Zr):
                    group = 4;
                    break;
                case (Element.Nb):
                    group = 5;
                    break;
                case (Element.Mo):
                    group = 6;
                    break;
                case (Element.Tc):
                    group = 7;
                    break;
                case (Element.Ru):
                    group = 8;
                    break;
                case (Element.Rh):
                    group = 9;
                    break;
                case (Element.Pd):
                    group = 10;
                    break;
                case (Element.Ag):
                    group = 11;
                    break;
                case (Element.Cd):
                    group = 12;
                    break;
                case (Element.In):
                    group = 13;
                    break;
                case (Element.Sn):
                    group = 14;
                    break;
                case (Element.Sb):
                    group = 15;
                    break;
                case (Element.Te):
                    group = 16;
                    break;
                case (Element.I):
                    group = 17;
                    break;
                case (Element.Xe):
                    group = 18;
                    break;
                case (Element.Cs):
                    group = 1;
                    break;
                case (Element.Ba):
                    group = 2;
                    break;
                case (Element.La):
                    group = 3;
                    break;
                case (Element.Hf):
                    group = 4;
                    break;
                case (Element.Ta):
                    group = 5;
                    break;
                case (Element.W):
                    group = 6;
                    break;
                case (Element.Re):
                    group = 7;
                    break;
                case (Element.Os):
                    group = 8;
                    break;
                case (Element.Ir):
                    group = 9;
                    break;
                case (Element.Pt):
                    group = 10;
                    break;
                case (Element.Au):
                    group = 11;
                    break;
                case (Element.Hg):
                    group = 12;
                    break;
                case (Element.Tl):
                    group = 13;
                    break;
                case (Element.Pb):
                    group = 14;
                    break;
                case (Element.Bi):
                    group = 15;
                    break;
                case (Element.Po):
                    group = 16;
                    break;
                case (Element.At):
                    group = 17;
                    break;
                case (Element.Rn):
                    group = 18;
                    break;
                case (Element.Fr):
                    group = 1;
                    break;
                case (Element.Ra):
                    group = 2;
                    break;
                case (Element.Ac):
                    group = 3;
                    break;
                default:
                    group = -1;
                    break;
            }
            return group;
        }

        /// <summary>
        /// GET Row (indexed from 0)
        /// H, He = 0
        /// C, N ... are 1
        /// 
        /// not all elements are done yet (lacking lanthanides and actinides and 104 +)
        /// </summary>
        /// <param name="element"></param>
        /// <returns>-1 for non-selected elements</returns>
        public static int GetRow(Element element)
        {
            int row;
            switch (element)
            {
                case (Element.H):
                    row = 0;
                    break;
                case (Element.He):
                    row = 0;
                    break;
                case (Element.Li):
                    row = 1;
                    break;
                case (Element.Be):
                    row = 1;
                    break;
                case (Element.B):
                    row = 1;
                    break;
                case (Element.C):
                    row = 1;
                    break;
                case (Element.N):
                    row = 1;
                    break;
                case (Element.O):
                    row = 1;
                    break;
                case (Element.F):
                    row = 1;
                    break;
                case (Element.Ne):
                    row = 1;
                    break;
                case (Element.Na):
                    row = 2;
                    break;
                case (Element.Mg):
                    row = 2;
                    break;
                case (Element.Al):
                    row = 2;
                    break;
                case (Element.Si):
                    row = 2;
                    break;
                case (Element.P):
                    row = 2;
                    break;
                case (Element.S):
                    row = 2;
                    break;
                case (Element.Cl):
                    row = 2;
                    break;
                case (Element.Ar):
                    row = 2;
                    break;
                case (Element.K):
                    row = 3;
                    break;
                case (Element.Ca):
                    row = 3;
                    break;
                case (Element.Sc):
                    row = 3;
                    break;
                case (Element.Ti):
                    row = 3;
                    break;
                case (Element.V):
                    row = 3;
                    break;
                case (Element.Cr):
                    row = 3;
                    break;
                case (Element.Mn):
                    row = 3;
                    break;
                case (Element.Fe):
                    row = 3;
                    break;
                case (Element.Co):
                    row = 3;
                    break;
                case (Element.Ni):
                    row = 3;
                    break;
                case (Element.Cu):
                    row = 3;
                    break;
                case (Element.Zn):
                    row = 3;
                    break;
                case (Element.Ga):
                    row = 3;
                    break;
                case (Element.Ge):
                    row = 3;
                    break;
                case (Element.As):
                    row = 3;
                    break;
                case (Element.Se):
                    row = 3;
                    break;
                case (Element.Br):
                    row = 3;
                    break;
                case (Element.Kr):
                    row = 3;
                    break;
                case (Element.Rb):
                    row = 4;
                    break;
                case (Element.Sr):
                    row = 4;
                    break;
                case (Element.Y):
                    row = 4;
                    break;
                case (Element.Zr):
                    row = 4;
                    break;
                case (Element.Nb):
                    row = 4;
                    break;
                case (Element.Mo):
                    row = 4;
                    break;
                case (Element.Tc):
                    row = 4;
                    break;
                case (Element.Ru):
                    row = 4;
                    break;
                case (Element.Rh):
                    row = 4;
                    break;
                case (Element.Pd):
                    row = 4;
                    break;
                case (Element.Ag):
                    row = 4;
                    break;
                case (Element.Cd):
                    row = 4;
                    break;
                case (Element.In):
                    row = 4;
                    break;
                case (Element.Sn):
                    row = 4;
                    break;
                case (Element.Sb):
                    row = 4;
                    break;
                case (Element.Te):
                    row = 4;
                    break;
                case (Element.I):
                    row = 4;
                    break;
                case (Element.Xe):
                    row = 4;
                    break;
                case (Element.Cs):
                    row = 5;
                    break;
                case (Element.Ba):
                    row = 5;
                    break;
                case (Element.La):
                    row = 5;
                    break;
                case (Element.Hf):
                    row = 5;
                    break;
                case (Element.Ta):
                    row = 5;
                    break;
                case (Element.W):
                    row = 5;
                    break;
                case (Element.Re):
                    row = 5;
                    break;
                case (Element.Os):
                    row = 5;
                    break;
                case (Element.Ir):
                    row = 5;
                    break;
                case (Element.Pt):
                    row = 5;
                    break;
                case (Element.Au):
                    row = 5;
                    break;
                case (Element.Hg):
                    row = 5;
                    break;
                case (Element.Tl):
                    row = 5;
                    break;
                case (Element.Pb):
                    row = 5;
                    break;
                case (Element.Bi):
                    row = 5;
                    break;
                case (Element.Po):
                    row = 5;
                    break;
                case (Element.At):
                    row = 5;
                    break;
                case (Element.Rn):
                    row = 5;
                    break;
                case (Element.Fr):
                    row = 6;
                    break;
                case (Element.Ra):
                    row = 6;
                    break;
                case (Element.Ac):
                    row = 6;
                    break;
                default:
                    row = -1;
                    break;
            }
            return row;
        }

        /// <summary>
        /// GET valence electrons
        /// only for selected elements
        /// H, B, C, Si, Ge, N, P, As, O, S, Se, F, Cl, Br, I,
        /// </summary>
        /// <param name="element"></param>
        /// <returns>-1 for non-selected elements</returns>
        public static int GetValenceElectronCount(Element element)
        {
            int count;
            switch (element)
            {
                case (Element.H):
                    count = 1;
                    break;
                case (Element.B):
                    count = 3;
                    break;
                case (Element.C):
                    count = 4;
                    break;
                case (Element.Si):
                    count = 4;
                    break;
                case (Element.Ge):
                    count = 4;
                    break;
                case (Element.N):
                    count = 5;
                    break;
                case (Element.P):
                    count = 5;
                    break;
                case (Element.As):
                    count = 5;
                    break;
                case (Element.O):
                    count = 6;
                    break;
                case (Element.S):
                    count = 6;
                    break;
                case (Element.Se):
                    count = 6;
                    break;
                case (Element.F):
                    count = 7;
                    break;
                case (Element.Cl):
                    count = 7;
                    break;
                case (Element.Br):
                    count = 7;
                    break;
                case (Element.I):
                    count = 7;
                    break;
                default:
                    count = -1;
                    break;
            }
            return count;
        }

      
        
      public static int CalculateCommonGroupDifference(Element thisElement, Element newElement)
        {
            int thisGroup = GetGroup(thisElement);
            // H is special
            if (Element.H.Equals(thisElement))
            {
                thisGroup = 17;
            }
            int newGroup = GetGroup(newElement);
            // H is special
            if (Element.H.Equals(newElement))
            {
                newGroup = 17;
            }
            return newGroup - thisGroup;
        }

        private static readonly int[] V1 = new[] { 1 };
        private static readonly int[] V2 = new[] { 2 };
        private static readonly int[] V3 = new[] { 3 };
        private static readonly int[] V4 = new[] { 4 };
        //private static readonly int[] V35 = new[] { 3,5 };
        //private static readonly int[] V246 = new[] { 2,4,6 };
        //private static readonly int[] V012 = new[] { 0,1,2 };
        
        public static IEnumerable<int> GetAllowedValencies(Element element) {
            int[] valencies;
            switch (element) {
                case Element.C:
                case Element.Si:
                    valencies = V4;
                    break;
                case Element.P:
                    valencies = V3; // V35; // Make same as ChemDoodle
                    break;
                case Element.N:
                    valencies = V3; // V35; // Make same as ChemDoodle
                    break;
                case Element.B:
                case Element.As:
                    valencies = V3;
                    break;
                case Element.O:
                case Element.Se:
                case Element.Te:
                    valencies = V2;
                    break;
                case Element.S:
                    valencies = V2; // V246; // Make same as ChemDoodle
                    break;
                case Element.Du:
                    valencies = V1; // V012;  // Can't draw using ChemDoodle !
                    break;
                default:
                    valencies = V1;
                    break;
            }
            return valencies;
        } 

        public static int GetValence(Element element, int sumOfChargeAndBondOrder) {
            var allowedValencies = GetAllowedValencies(element);
            foreach (var valency in allowedValencies.Where(valency => valency >= sumOfChargeAndBondOrder)) {
                return valency;
            }
            return -1;
        }
    }
}