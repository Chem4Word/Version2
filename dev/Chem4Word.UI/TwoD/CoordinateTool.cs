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
using System.Windows;
using Euclid;
using log4net;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.UI.TwoD
{
    /// <summary>
    /// Contains methods to calculate bounding boxes from molecules.
    /// TODO This must be modified to AbstractAtomContainer
    /// </summary>
    internal class CoordinateTool
    {
        private static ILog Log = LogManager.GetLogger(typeof (CoordinateTool));

        /// <summary>
        /// Calculate the bounding box from a set of CMLAtoms (should be anything with 2D coords)
        /// Calculation is performed in model coordinates.
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="atoms"></param>
        /// <returns></returns>
        public static Rect GetBounds2D(ContextObject contextObject, IEnumerable<CmlAtom> atoms)
        {
            if (!atoms.Any())
            {
                return new Rect(0, 0, 0, 0);
            }

            double xmax = Double.NegativeInfinity;
            double ymax = Double.NegativeInfinity;
            double xmin = Double.PositiveInfinity;
            double ymin = Double.PositiveInfinity;
            foreach (CmlAtom atom in atoms)
            {
                if (ChemicalIntelligence.ShouldAtomBeDrawnInTwoD(contextObject, atom.DelegateElement))
                {
                    Point2 p = atom.Point2;
                    xmax = Math.Max(xmax, p.X);
                    xmin = Math.Min(xmin, p.X);
                    ymax = Math.Max(ymax, p.Y);
                    ymin = Math.Min(ymin, p.Y);
                }
            }
            Log.Debug(string.Format(CultureInfo.InvariantCulture, "ymax {0} ymin {1}", ymax, ymin));
            return new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
        }

        public static Rect GetBounds2D(ContextObject contextObject, IEnumerable<CmlBond> bonds)
        {
            SortedDictionary<string, CmlAtom> atoms = new SortedDictionary<string, CmlAtom>();
            foreach (CmlBond bond in bonds)
            {
                foreach (CmlAtom atom in bond.GetAtoms())
                {
                    if (! atoms.ContainsKey(atom.Id))
                    {
                        atoms.Add(atom.Id, atom);
                    }
                }
            }
            return GetBounds2D(contextObject, atoms.Values);
        }
    }
}