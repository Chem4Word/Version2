// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Windows;

namespace Euclid
{
    public class Vector2
    {
        private Vector vector;

        private Vector2(Vector vector)
        {
            this.vector = vector;
        }

        public Vector2(double x, double y)
        {
            this.vector = new Vector(x, y);
        }

        public Vector2(Vector2 vector)
        {
            if (vector != null)
            {
                this.vector = new Vector(vector.X, vector.Y);
            }
        }

        public double X
        {
            get { return vector.X; }
            set { vector.X = value; }
        }

        public double Y
        {
            get { return vector.Y; }
            set { vector.Y = value; }
        }

        public double Length
        {
            get { return Math.Sqrt(Math.Pow(X, 2.0) + Math.Pow(Y, 2.0)); }
        }

        private static Vector2 CreateVector2(Vector vector)
        {
            return new Vector2(vector);
        }

        /// <summary>
        /// equality
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool Equals(Vector2 vector, double epsilon)
        {
            return Equals(this, vector, epsilon);
        }

        /// <summary>
        /// equality
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        private static bool Equals(Vector2 vector1, Vector2 vector2, double epsilon)
        {
            return (Math.Abs(vector1.X - vector2.X) <= epsilon
                    && Math.Abs(vector1.Y - vector2.Y) <= epsilon);
        }

        /// <summary>
        /// gets ANTICLOCKWISE angle from this to v2.
        /// thus V(1., 0.).AngleTo(V(0., 1.0) => Math.PI
        /// </summary>
        /// <param name="vector">Vector to rotate this onto</param>
        /// <returns>signed angle in radians</returns>
        public Angle AngleTo(Vector2 vector)

        {
            return (vector == null)
                       ? null
                       :
                           new Angle(Math.Atan2(vector.Y, vector.X) - Math.Atan2(this.Y, this.X), Angle.Unit.Radians);
        }

        /// <summary>
        /// length of vector
        /// </summary>
        /// <returns>null if vector not set</returns>
        public Nullable<double> GetLength()
        {
            return vector.Length;
        }

        /// <summary>
        /// normalizes to unit length
        /// </summary>
        /// <returns>null if vector not set</returns>
        public void Normalize()
        {
            this.vector.Normalize();
        }

        /// <summary>
        /// get unit vector
        /// does not alter this
        /// </summary>
        /// <returns>null if vector not set</returns>
        public Vector2 GetUnitVector()
        {
            Vector2 unitVector = new Vector2(this);
            unitVector.Normalize();
            return unitVector;
        }

        /// <summary>
        /// multiply by scalar
        /// </summary>
        public void MultiplyBy(double scalar)
        {
            this.vector.X *= scalar;
            this.vector.Y *= scalar;
        }

        /// <summary>
        /// add vectors
        /// </summary>
        /// <param name="vector">the vector to add</param>
        /// <exception cref="ArgumentNullException">If vector is null</exception>
        public void Add(Vector2 vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException("vector");
            }
            this.vector.X += vector.X;
            this.vector.Y += vector.Y;
        }
    }
}