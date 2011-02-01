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
using System.Xml.Linq;
using Euclid;

namespace Numbo.Cml
{
    /// <summary>
    /// supports CML bondStereo element
    /// </summary>
    public class CmlBondStereo : CmlElement
    {
        public const string Tag = "bondStereo";

        public static readonly string Wedge = "W";

        public static readonly string Hatch = "H";
        public static readonly string Cis = "C";
        public static readonly string Trans = "T";
        public static readonly string Undefined = "undefined";


        /// <summary>
        /// creates unattached CMLElement with unattached XElement
        /// </summary>
        public CmlBondStereo()
            : base(new XElement(CmlConstants.CmlxNamespace + Tag))
        {}

        /// <summary>
        ///  formed from XElement reference
        /// </summary>
        public CmlBondStereo(XElement element) : base(element)
        {}

        public override string GetTag()
        {
            return Tag;
        }

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

        public string GetValue()
        {
            return DelegateElement.Value;
        }

        public void SetValue(string value)
        {
            DelegateElement.Value = value;
        }

        internal CmlAtom GetAtomAtSharpEnd()
        {
            CmlBond bond = GetParentBond();
            return (bond == null) ? null : bond.GetAtoms().ElementAt(0);
        }

        internal CmlAtom GetAtomAtBroadEnd()
        {
            CmlBond bond = GetParentBond();
            return (bond == null) ? null : bond.GetAtoms().ElementAt(1);
        }

        internal CmlBond GetParentBond()
        {
            CmlBond bond = null;
            var bondPointer = DelegateElement.Parent;
            if (bondPointer != null)
            {
                bond = new CmlBond(bondPointer);
            }
            return bond;
        }

        internal CmlAtomParity CreateNewAtomParityForAtomAtSharpEndWithAtomRefs4InLigandOrder()
        {
            if (!IsWedgeHatch())
            {
                throw new Exception("This bond is not a Wedge or a Hatch bond.");
            }
            var atom = GetAtomAtSharpEnd();
            if (atom == null)
            {
                throw new Exception("no atom at sharp end: " + Id);
            }
            var bond = this.GetParentBond();
            var broadAtom = bond.GetAtomAtOtherEnd(atom);
            var ligandList = atom.GetLigands();
            if (ligandList.Count() != 4)
            {
                throw new Exception("must have 4 atoms for stereochemistry");
            }
            var d = this.CalculateDeterminant(broadAtom, ligandList);
            var atomParity = new CmlAtomParity();
            CmlUtils.AddOnlyChild(atom, atomParity);
            atomParity.SetAtomRefs4(CmlAtom.GetAtomIds(ligandList));
            atomParity.SetValue(d);

            return atomParity;
        }

        internal double CalculateDeterminant(CmlAtom broadAtom, ICollection<CmlAtom> ligandList)
        {
            string wedgeHatchValue = this.GetValue();
            double wedgeHatchZ = GetZFromWedgeHatch(wedgeHatchValue);
            double d = CalculateDeterminant(broadAtom, ligandList, wedgeHatchZ);
            return d;
        }

        private static double CalculateDeterminant(CmlAtom broadAtom, ICollection<CmlAtom> ligandList,
                                                   double wedgeHatchZ)
        {
            double[][] matrix = new double[4][];
            for (int i = 0; i < 4; i++)
            {
                matrix[i] = new double[4];
            }
            for (int j = 0; j < 4; j++)
            {
                CmlAtom ligand = ligandList.ElementAt(j);
                double z = (ligand.Equals(broadAtom)) ? wedgeHatchZ : 0.0;
                matrix[0][j] = 1.0;
                matrix[1][j] = (double) ligand.X2;
                matrix[2][j] = (double) ligand.Y2;
                matrix[3][j] = z;
            }
            RealSquareMatrix rsm = new RealSquareMatrix(matrix);
            return rsm.Determinant();
        }

        private static double GetZFromWedgeHatch(string wedgeHatchValue)
        {
            double z = 0.0;
            if (wedgeHatchValue == null)
            {
                throw new ArgumentNullException("wedgeHatchValue", "no WedgeHatch value");
            }
            if (wedgeHatchValue.Equals(CmlBond.Wedge))
            {
                z = 1.0;
            }
            else if (wedgeHatchValue.Equals(CmlBond.Hatch))
            {
                z = -1.0;
            }
            else
            {
                throw new Exception("bad stereo value :" + wedgeHatchValue + ":");
            }

            return z;
        }

        public bool IsWedgeHatch() {
            return Wedge.Equals(GetValue()) || Hatch.Equals(GetValue());
        }
    }
}