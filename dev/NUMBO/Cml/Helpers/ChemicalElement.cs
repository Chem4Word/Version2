// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;

namespace Numbo.Cml.Helpers
{
    /// <summary>
    /// ChemicalElement
    /// </summary>
    public class ChemicalElement
    {
        public enum RadiusType
        {
            /** dewisott */
            Atomic,
            /** dewisott */
            Covalent,
            /** dewisott */
            Ionic,
            /** dewisott */
            Vdw
        }
       
        public ChemicalElement(String symbol, int atomicNumber, double atomicWeight)
        {
            this.Symbol = symbol;
            this.AtomicNumber = atomicNumber;
            this.AtomicWeight = atomicWeight;
        }

        /// <summary>
        /// </summary>
        public ChemicalElement()
        {}

        public double AtomicWeight { get; set; }

        public int AtomicNumber { get; set; }

        public string Symbol { get; set; }

        public int[] IsotopeMasses { get; set; }

    }
}