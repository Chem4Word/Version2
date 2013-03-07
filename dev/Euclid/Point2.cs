// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace Euclid
{
    /// <summary>
    /// a 2D point
    /// </summary>
    public class Point2 : IHasCentroid2
    {
        private Point point;

        private Point2(Point point)
        {
            this.point = point;
        }

        /// <summary>
        /// new point
        /// </summary>
        /// <param name="x">x value</param>
        /// <param name="y">y value</param>
        public Point2(double x, double y)
        {
            this.point = new Point(x, y);
        }

        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="pt"></param>
        public Point2(Point2 pt)
        {
            this.point = new Point(pt.point.X, pt.point.Y);
        }

        public double X
        {
            get { return point.X; }
            set { point.X = value; }
        }

        public double Y
        {
            get { return point.Y; }
            set { point.Y = value; }
        }

        public double DistanceFromOrigin
        {
            get
            {
                if ((double.IsNaN(this.X)) || (double.IsNaN(this.Y)))
                {
                    return double.NaN;
                }
                if ((this.X == double.MaxValue) || (this.Y == double.MaxValue))
                {
                    return double.NaN;
                }
                if ((this.X == double.MinValue) || (this.Y == double.MinValue))
                {
                    return double.NaN;
                }
                return Math.Sqrt(this.X*this.X + this.Y*this.Y);
            }
        }

        /// <summary>
        /// so that point can be a IHasCentroid
        /// </summary>
        /// <returns>this</returns>
        public Point2 GetCentroid()
        {
            return this;
        }

        /// <summary>
        /// returns null
        /// </summary>
        /// <returns>null</returns>
        public ICollection<IHasCentroid2> GetSubCentroids()
        {
            return null;
        }

        public static Point2 CreatePoint2(Point point)
        {
            return new Point2(point);
        }

        public bool Equals(Point2 point, double epsilon)
        {
            if (null == point)
            {
                return false;
            }
            if ((double.IsNaN(this.X)) || (double.IsNaN(this.Y)) ||
                (double.IsNaN(point.X)) || (double.IsNaN(point.Y)))
            {
                return false;
            }
            double xDiff = Math.Abs(this.X - point.X);
            if (xDiff >= epsilon)
            {
                return false;
            }
            double yDiff = Math.Abs(this.Y - point.Y);
            if (yDiff >= epsilon)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// do not use epsilon when comparing the real numbers
        /// </summary>
        /// <param name="obj">the object to test equality with</param>
        /// <returns>true is obj is a Point2 with the same X and Y values as this</returns>
        public override bool Equals(object obj)
        {
            Point2 p = obj as Point2;
            if (null == p)
            {
                return false;
            }
            return (this.X == p.X) && (this.Y == p.Y);
        }

        public override int GetHashCode()
        {
            int result = 19;
            ulong xL = (ulong) X;
            result = 37*result + (int) (xL ^ (xL >> 32));
            ulong yL = (ulong) Y;
            result = 37*result + (int) (yL ^ (yL >> 32));
            return result;
        }

        /// <summary>
        /// add two points
        /// semantics are a bit hazy because it should really
        /// be Point + Vector
        /// </summary>
        /// <param name="point"></param>
        /// <returns>null if point = null</returns>
        public Point2 Add(Point2 point)
        {
            Point2 p = null;
            if (null != point)
            {
                if ((double.IsNaN(this.X)) || (double.IsNaN(this.Y)) ||
                    (double.IsNaN(point.X)) || (double.IsNaN(point.Y)))
                {
                    throw new ArithmeticException("Cannot perform arithmetic on double.NaN");
                }
                p = new Point2(this.X + point.X, this.Y + point.Y);
            }
            return p;
        }

        /// <summary>
        /// add a vector to a point
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>null if point = null</returns>
        public Point2 Add(Vector2 vector)
        {
            Point2 p = null;
            if (null != vector)
            {
                if ((double.IsNaN(this.X)) || (double.IsNaN(this.Y)) ||
                    (double.IsNaN(vector.X)) || (double.IsNaN(vector.Y)))
                {
                    throw new ArithmeticException("Cannot perform arithmetic on double.NaN");
                }
                p = new Point2(this.X + vector.X, this.Y + vector.Y);
            }
            return p;
        }

        public Vector2 Subtract(Point2 point)
        {
            if (null == point)
            {
                throw new ArgumentNullException("point", "Cannot subtract a null value from a Point2");
            }
            if ((double.IsNaN(this.X)) || (double.IsNaN(this.Y)) ||
                (double.IsNaN(point.X)) || (double.IsNaN(point.Y)))
            {
                throw new ArithmeticException("Cannot perform arithmetic on double.NaN");
            }
            return new Vector2(this.X - point.X, this.Y - point.Y);
        }

        public static Vector2 operator -(Point2 point1, Point2 point2)
        {
            return point1.Subtract(point2);
        }

        public static Point2 operator -(Point2 point1, Vector2 vector)
        {
            return new Point2(point1.X - vector.X, point1.Y - vector.Y);
        }

        public static Point2 operator +(Point2 point1, Vector2 vector)
        {
            return new Point2(point1.X + vector.X, point1.Y + vector.Y);
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        /// <summary>
        /// multiply by scalar
        /// does not alter this
        /// </summary>
        /// <param name="factor"></param>
        /// <returns>null if factor.IsNaN or overflow</returns>
        public Point2 MultiplyBy(double factor)
        {
            Point2 p = null;

            if (!Double.IsNaN(factor))
            {  
                double xx = this.X*factor;
                double yy = this.Y*factor;
                if (!Double.IsInfinity(xx) && !Double.IsInfinity(yy) &&
                    !Double.IsNaN(xx) && !Double.IsNaN(yy))
                {
                    p = new Point2(xx, yy);
                }
            }
            return p;
        }

        /// <summary>
        /// gets distance between two points 
        /// example:
        /// GetDistance(new Point2(1,0), new Point2(0,1)) => Math.sqrt(2.0)
        /// 
        /// </summary>
        /// <param name="point0">first point</param>
        /// <param name="point1">final point</param>
        /// <returns></returns>
        public static double GetDistance(Point2 point0, Point2 point1)
        {
            if (point0 == null)
            {
                throw new ArgumentNullException("point0");
            }
            if (point1 == null)
            {
                throw new ArgumentNullException("point1");
            }
            Vector2 from = point0.Subtract(point1);
            return (double) from.GetLength();
        }

        /// <summary>
        /// gets signed angle between three points 
        /// direction is anticlockwise
        /// example:
        /// GetAngle(new Point2(1,0), new Point2(0,0), new Point2(0,1)) => Math.PI/2
        /// GetAngle(new Point2(-1,0), new Point2(0,0), new Point2(0,1)) => -Math.PI/2
        /// GetAngle(new Point2(0,1), new Point2(0,0), new Point2(1,0)) => -Math.PI/2
        /// 
        /// </summary>
        /// <param name="point0">first point</param>
        /// <param name="point1">centre point</param>
        /// <param name="point2">final point</param>
        /// <param name="epsilon"></param>
        /// <exception cref="ArgumentException">if any atoms are coincident</exception>
        /// <returns>null if any points are null</returns>
        public static Angle GetAngle(Point2 point0, Point2 point1, Point2 point2, double epsilon)
        {
            Angle angle = null;
            if (point0 != null && point1 != null && point2 != null)
            {
                if (point0.Equals(point1, epsilon) || point2.Equals(point1, epsilon))
                {
                    throw new ArgumentException("coincident points in GetAngle");
                }
                Vector2 from = point0.Subtract(point1);
                Vector2 to = point2.Subtract(point1);
                angle = from.AngleTo(to);
            }
            return angle;
        }

        /// <summary>
        /// gets centroid of points
        /// if any point is null, return null
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point2 GetCentroid(HashSet<Point2> points)
        {
            Point2 centroid = null;
            if (points != null)
            {
                centroid = new Point2(0D, 0D);
                foreach (Point2 p in points)
                {
                    if (p == null)
                    {
                        centroid = null;
                        break;
                    }
                    centroid = centroid.Add(p);
                }
                if (centroid != null)
                {
                    centroid = centroid.MultiplyBy(1.0/points.Count);
                }
            }
            return centroid;
        }

        /// <summary>
        /// gets centroid of points
        /// if any point is null, return null
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point2 GetCentroid(IEnumerable<Point2> points)
        {
            Point2 centroid = null;
            if (points != null)
            {
                HashSet<Point2> pointSet = new HashSet<Point2>();
                foreach (Point2 point in points)
                {
                    if (point == null)
                    {
                        break;
                    }
                    pointSet.Add(point);
                }
                centroid = GetCentroid(pointSet);
            }
            return centroid;
        }

        /// <summary>
        /// gets 4 bounding points 
        /// </summary>
        /// <param name="points">list of points</param>
        /// <returns>(SW, NW, NE, SE; ie. minXminY, minXmaxY, maxXmaxY, maxXminY) points; null if no points or any are null</returns>
        public static IEnumerable<Point2> Get4BoundingPoints(IEnumerable<Point2> points)
        {
            double minX = Double.MaxValue;
            double maxX = -Double.MaxValue;
            double minY = Double.MaxValue;
            double maxY = -Double.MaxValue;
            List<Point2> points4 = null;
            if (points != null)
            {
                foreach (Point2 point in points)
                {
                    if (point == null)
                    {
                        points4 = null;
                        break;
                    }
                    if (points4 == null)
                    {
                        points4 = new List<Point2>();
                    }
                    if (point.X < minX)
                    {
                        minX = point.X;
                    }
                    else if (point.X > maxX)
                    {
                        maxX = point.X;
                    }
                    if (point.Y < minY)
                    {
                        minY = point.Y;
                    }
                    else if (point.Y > maxY)
                    {
                        maxY = point.Y;
                    }
                }
                if (points4 != null)
                {
                    points4.Add(new Point2(minX, minY));
                    points4.Add(new Point2(minX, maxY));
                    points4.Add(new Point2(maxX, maxY));
                    points4.Add(new Point2(maxX, minY));
                }
            }
            return points4;
        }

        /// <summary>
        /// length of vector
        /// </summary>
        /// <returns>null if vector not set</returns>
        public Nullable<double> GetDistanceTo(Point2 point)
        {
            Nullable<double> length = null;
            if (point != null)
            {
                Vector2 v = this.Subtract(point);
                length = v.GetLength();
            }
            return length;
        }

        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "({0}, {1})", X, Y);
        }
    }
}