// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EuclidTest")]

namespace Euclid
{
    /// <summary>
    /// Holds an angle radians but allows import/export of degrees
    /// </summary>
    public class Angle : IComparable
    {
        #region Range enum

        ///<summary>
        /// Specifiy how the limits of the angle should be managed
        ///</summary>
        public enum Range
        {
            ///<summary>
            /// angle does not reset every full circle
            ///</summary>
            Unlimited,
            ///<summary>
            /// goes between 0 and 360 (degrees) or 0 to 2*PI (radians)
            ///</summary>
            Unsigned,
            ///<summary>
            /// goes from +180 to -180 (degrees) or +PI to -PI (radians)
            ///</summary>
            Signed
        } ;

        #endregion

        #region Unit enum

        ///<summary>
        ///</summary>
        public enum Unit
        {
            ///<summary>
            /// Use Radians 
            ///</summary>
            Radians,
            ///<summary>
            /// Use degrees 
            ///</summary>
            Degrees
        } ;

        #endregion

        private const double DegreesToRadiansMultiplier = Math.PI/180.0;
        private const double TwoPI = 2.0*Math.PI;

        private double radians;
        private Range range = Range.Unlimited;

        /// <summary>
        /// Create an Angle with the specified angle and units
        /// </summary>
        /// <param name="angle">the angle which the Angle should represent in the specified units</param>
        /// <param name="unit">the units used to specify the angle</param>
        /// <exception cref="ArgumentOutOfRangeException">if angle is not a number</exception>
        public Angle(double angle, Unit unit)
        {
            /// neither angle nor units can ever be null
            if (Double.IsNaN(angle))
            {
                throw new ArgumentOutOfRangeException("angle", "angle passed into the creator is not a number");
            }
            radians = (Unit.Degrees == unit) ? angle*DegreesToRadiansMultiplier : angle;
            this.range = Range.Unlimited;
        }

        /// <summary>
        /// Create an Angle with the specified angle and units
        /// </summary>
        /// <param name="angle">the angle which the Angle should represent in the specified units</param>
        /// <param name="unit">the units used to specify the angle</param>
        /// <param name="range">the range through which the angle is defined</param>
        /// <exception cref="ArgumentException">if angle is not a number</exception>
        public Angle(double angle, Unit unit, Range range)
            : this(angle, unit)
        {
            this.range = range;
            Normalize();
        }

        public Angle(Angle angle)
        {
            this.range = angle.range;
            this.radians = angle.radians;
        }

        /// <summary>
        /// default Unlimited
        /// </summary>
        public Range AngleRange
        {
            get { return range; }
            set { range = value; }
        }

        /// <summary>
        /// Accessor for the angle in radians
        /// </summary>
        public double Radians
        {
            get { return radians; }
            set { radians = value; }
        }

        /// <summary>
        /// Accessor for the angle in degrees
        /// </summary>
        public double Degrees
        {
            get { return radians/DegreesToRadiansMultiplier; }
            set { radians = value*DegreesToRadiansMultiplier; }
        }

        /// <summary>
        /// compares against angle
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ArgumentException">if the argument is not an Angle</exception>
        /// <returns> -1, 0 1 according to lt, equal gt this</returns>
        public int CompareTo(object obj)
        {
            Angle angle = obj as Angle;
            if (angle == null)
            {
                throw new ArgumentException("the argument should be of type Angle", "obj");
            }
            return this.CompareTo(angle);
        }

        /// <summary>
        /// gets absolute value of angle
        /// </summary>
        /// <returns>absolute value</returns>
        public Angle GetAbsoluteValue()
        {
            Angle abs = new Angle(this);
            int sign = this.Sign() ?? 0;
            if (sign < 0)
            {
                abs = this.MultiplyBy(-1);
            }
            return abs;
        }

        /// <summary>
        /// is this less than angle
        /// note: the caller should probably normalize() 
        /// </summary>
        /// <param name="angle">angle must not be null, or an ArgumentNullException</param>
        /// <returns> bool </returns>
        /// <exception cref="ArgumentNullException">if angle is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">if angles have differenct ranges</exception>
        public bool LessThan(Angle angle)
        {
            if (angle == null)
            {
                throw new ArgumentNullException("angle");
            }
            if (angle.range != this.range)
            {
                throw new ArgumentOutOfRangeException("angle", "both angles must have the same Range");
            }
            this.Normalize();
            angle.Normalize();
            return radians.CompareTo(angle.radians) < 0;
        }

        /// <summary>
        /// is this less than angle
        /// note: the caller should probably normalize() 
        /// </summary>
        /// <param name="angle">angle must not be null, or an ArgumentNullException</param>
        /// <returns> bool </returns>
        /// <exception cref="ArgumentNullException">if angle is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">if angles have differenct ranges</exception>
        public bool GreaterThan(Angle angle)
        {
            if (angle == null)
            {
                throw new ArgumentNullException("angle");
            }
            if (angle.range != this.range)
            {
                throw new ArgumentOutOfRangeException("angle", "both angles must have the same Range");
            }
            this.Normalize();
            angle.Normalize();
            return (radians).CompareTo(angle.radians) > 0;
        }

        /// <summary>
        /// explicitly set range criterion (overriding anything previously there)
        /// </summary>
        /// <param name="range"></param>
        public void Normalize(Range range)
        {
            this.range = range;
            Normalize();
        }

        /// <summary>
        /// normalizes according to Range
        /// </summary>
        public void Normalize()
        {
            switch (range)
            {
                case Range.Unlimited:
                    break;
                case Range.Unsigned:
                    while (radians >= TwoPI)
                    {
                        radians -= TwoPI;
                    }
                    while (radians < 0)
                    {
                        radians += TwoPI;
                    }
                    break;
                case Range.Signed:
                    while (radians > Math.PI)
                    {
                        radians -= TwoPI;
                    }
                    while (radians <= -Math.PI)
                    {
                        radians += TwoPI;
                    }
                    break;
            }
        }

        /// <summary>
        /// if angle test is within epsilon of this angle, it is considered equal
        /// </summary>
        /// <param name="test">the angle to compare to this one</param>
        /// <param name="epsilon">the tolerance to check the angles must be equal</param>
        /// <returns>true if test angle is within epsilon of this angle otherwise false</returns>
        public bool Equals(Angle test, double epsilon)
        {
            if (test == null)
            {
                return false;
            }
            if (Double.IsNaN(epsilon))
            {
                return false;
            }
            return Math.Abs(Radians - test.Radians) <= epsilon;
        }

        /// <summary>
        /// Subtracts angle from this does not alter this
        /// the resultant angle is normalised according to 
        /// the range in this
        /// </summary>
        /// <param name="angle">the angle to subtract from this</param>
        /// <returns>difference or null if angle is null</returns>
        public Angle Subtract(Angle angle)
        {
            Angle result = null;
            if (angle != null)
            {
                if (angle.AngleRange != this.AngleRange)
                {
                    angle = new Angle(angle) {AngleRange = this.AngleRange};
                    angle.Normalize();
                }
                result = new Angle(this.radians - angle.radians, Unit.Radians, this.range);
                result.Normalize();
            }
            return result;
        }

        /// <summary>
        /// multiplies angle by scalar
        /// alters this
        /// </summary>
        /// <param name="factor">the factor to multiply this angle by</param>
        /// <returns>multiplied angle</returns>
        /// <exception cref="ArgumentException">if factor is not a number</exception>
        public Angle MultiplyBy(double factor)
        {
            if (Double.IsNaN(factor))
            {
                throw new ArgumentException("multiplier passed into MultiplyBy is not a number");
            }
            Radians *= factor;
            Normalize();
            return this;
        }

        /// <summary>
        /// returns angle between this (signed) and +- PI 
        /// if this == PI*3/4 , return PI/4
        /// if this == -PI*3/4 , return -PI/4
        /// </summary>
        /// <returns>signed differnce from PI or null if this is not an angle</returns>
        public Angle GetDeviationFromLinearity()
        {
            if (Double.IsNaN(radians))
            {
                return null;
            }
            Angle this1 = new Angle(this) {AngleRange = Range.Signed};
            Angle result = null;
            if (this1.LessThan(new Angle(0.0, Unit.Radians, Range.Signed)))
            {
                result = new Angle(-Math.PI, Unit.Radians, Range.Signed).Subtract(this1);
            }
            else
            {
                result = new Angle(Math.PI, Unit.Radians, Range.Signed).Subtract(this1);
            }
            return result;
        }

        /// <summary>
        /// finds sign of signed angle 
        /// </summary>
        /// <returns>-1 if less than, 0 if within epsilon, 1 greater than zero</returns>
        /// <exception cref="ArgumentNullException">if epsilon is null</exception>
        public int? Sign()
        {
            int? result = null;
            if (range == Range.Signed || range == Range.Unlimited)
            {
                if (radians < 0)
                {
                    result = -1;
                }
                else if (radians > 0)
                {
                    result = 1;
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }

        public override string ToString()
        {
            return this.radians + " (radians) " + AngleRange;
        }

        /// <summary>
        /// Compares this instance to a specified angle and returns an integer that indicates whether the value of this instance is 
        /// less than, equal to, or greater than the value of the specified angle.
        /// </summary>
        /// <param name="angle">the angle to compare against</param>
        /// <exception cref="ArgumentNullException">if angle is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">if angles have differenct ranges</exception>
        /// <returns>-1, 0, 1 </returns>
        public int CompareTo(Angle angle)
        {
            int result = 0;
            if (this.LessThan(angle))
            {
                result = -1;
            }
            if (this.GreaterThan(angle))
            {
                result = 1;
            }
            return result;
        }
    }
}