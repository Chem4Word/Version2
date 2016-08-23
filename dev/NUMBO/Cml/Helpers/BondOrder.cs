// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Globalization;
using System.Xml.Linq;

namespace Numbo.Cml.Helpers
{
    public class BondOrder
    {
        public static readonly BondOrder Aromatic = new BondOrder(CmlBond.Aromatic, 1.5);
        public static readonly BondOrder Double = new BondOrder(CmlBond.Double, 2.0);
        public static readonly BondOrder Other = new BondOrder(CmlBond.Other, 0.0);
        public static readonly BondOrder Single = new BondOrder(CmlBond.Single, 1.0);
        public static readonly BondOrder Triple = new BondOrder(CmlBond.Triple, 3.0);
        public static readonly BondOrder Unknown = new BondOrder(CmlBond.Unknown, 0.0);
        private readonly string cmlBondOrder;
        private readonly Order order;
        private Nullable<double> numericBondOrder;

        /// <summary>
        /// creates BondOrder
        /// if value is Unknown, order = Order.Unknown
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numericBondOrder"></param>
        private BondOrder(string value, double numericBondOrder)
        {
            this.numericBondOrder = numericBondOrder;
            cmlBondOrder = value;
            order = Order.Unknown;
            if (numericBondOrder == 1)
            {
                order = Order.S;
            }
            if (numericBondOrder == 2)
            {
                order = Order.D;
            }
            if (numericBondOrder == 3)
            {
                order = Order.T;
            }
            if (numericBondOrder == 1.5) {
                order = Order.A;
            }
        }

        public Nullable<int> IntegerBondOrder
        {
            get
            {
                int? o = null;
                if (numericBondOrder.HasValue)
                {
                    o = (int) numericBondOrder;
                    if (Math.Abs((double) numericBondOrder - (int) o) > 0.01)
                    {
                        o = null;
                    }
                }
                return o;
            }
        }

        public Nullable<double> NumericBondOrder
        {
            get { return numericBondOrder; }
        }

        public string CmlBondOrder
        {
            get { return cmlBondOrder; }
        }

        /// <summary>
        /// returns order in bond
        /// </summary>
        /// <param name="bond"></param>
        /// <returns>Single,Double,Triple,Aromatic or Unknown (not null)</returns>
        public static BondOrder GetBondOrder(CmlBond bond)
        {
            XAttribute att = bond.DelegateElement.Attribute(CmlAttribute.Order);
            string value = (att == null) ? null : att.Value;
            return (value == null) ? Unknown : GetBondOrderFromCmlOrder(value);
        }

        /// <summary>
        /// get BondOrder for numeric value
        /// if value = 1,2,3 returns BondOrder else null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BondOrder GetBondOrder(int value)
        {
            BondOrder bondOrder = null;
            switch (value)
            {
                case 1:
                    bondOrder = Single;
                    break;
                case 2:
                    bondOrder = Double;
                    break;
                case 3:
                    bondOrder = Triple;
                    break;
            }
            return bondOrder;
        }

        /// <summary>
        /// get BondOrder from integer as string
        /// convenience method
        /// </summary>
        /// <param name="value">"1", "2", "3"</param>
        /// <returns></returns>
        public static BondOrder GetBondOrderFromInteger(string value)
        {
            BondOrder bondOrder = Unknown;
            int intValue = 0;
            int.TryParse(value, out intValue);
            bondOrder = GetBondOrder(intValue);
            return bondOrder;
        }

        /// <summary>
        /// get BondOrder for string value
        /// if value = CMLBond.Single, Double, Triple, Aromatic returns BondOrder else Unknown
        /// </summary>
        /// normalizes deprecated values (1,2,3)
        /// <param name="value"></param>
        /// <returns></returns>
        public static BondOrder GetBondOrderFromCmlOrder(string value)
        {
            BondOrder bondOrder = Unknown;
            if (value == null)
            {}
            else if (value.Equals(CmlBond.Single) || value.Equals("1"))
            {
                bondOrder = Single;
            }
            else if (value.Equals(CmlBond.Double) || value.Equals("2"))
            {
                bondOrder = Double;
            }
            else if (value.Equals(CmlBond.Triple) || value.Equals("3"))
            {
                bondOrder = Triple;
            }
            else if (value.Equals(CmlBond.Aromatic) || value.Equals("1.5"))
            {
                bondOrder = Aromatic;
            }
            else if (value.Equals(CmlBond.Unknown))
            {
                bondOrder = Unknown;
            }
            else if (value.Equals(CmlBond.Other))
            {
                bondOrder = Other;
            }
            return bondOrder;
        }

        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture,"cml: {0} Order: {1} numeric: {2}", cmlBondOrder, order, numericBondOrder);
        }

        private enum Order
        {
            S,
            D,
            T,
            A,
            Unknown,
            Other
        }
    }
}