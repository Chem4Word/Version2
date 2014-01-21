// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Euclid
{
    public class Transform2
    {
        private readonly GeneralTransform generalTransformField;

        /// <summary>
        /// Private constructor to ensure creation of Transform2 is done by static factory methods
        /// Allows delegation of functionality
        /// </summary>
        /// <param name="generalTransform">the transform</param>
        private Transform2(GeneralTransform generalTransform)
        {
            this.generalTransformField = generalTransform;
        }

        public GeneralTransform GeneralTransform
        {
            get { return generalTransformField; }
            set { GeneralTransform = value; }
        }

        /// <summary>
        /// Convenience method to create matrix for anticlockwise rotation by theta degress about (0, 0)
        /// </summary>
        /// <param name="theta">the angle through which rotation will occur (anticlockwise)</param>
        /// <returns>the Transform2 object which represents this rotation</returns>
        public static Transform2 CreateRotationMatrixAboutOrigin(Angle theta)
        {
            if (null == theta)
            {
                throw new ArgumentNullException("theta", "Cannot create rotation from null Angle");
            }
            GeneralTransform transform = new RotateTransform(theta.Degrees);
            return new Transform2(transform);
        }

        /// <summary>
        /// Create matrix for rotation by theta degress about any point. Positive values of theta 
        /// produce an anticlockwise rotation, negative values produce a clockwise rotation.
        /// </summary>
        /// <param name="theta">the angle through which rotation will occur (anticlockwise)</param>
        /// <param name="point">the point about which the rotation will occur</param>
        /// <returns>the Transform2 object which represents this rotation</returns>
        public static Transform2 CreateRotationMatrixAboutPoint(Angle theta, Point2 point)
        {
            if (null == theta)
            {
                throw new ArgumentNullException("theta", "Cannot create rotation from null Angle");
            }
            if (null == point)
            {
                throw new ArgumentNullException("point", "Cannot create rotation about point from null Point2");
            }
            GeneralTransform transform = new RotateTransform(theta.Degrees, point.X, point.Y);
            return new Transform2(transform);
        }

        /// <summary>
        /// Create matrix for translation by a vector 
        /// </summary>
        /// <param name="vector">the vector to translate by</param>
        /// <returns>the Transform2 object which represents this translation</returns>
        public static Transform2 CreateTranslation(Vector2 vector)
        {
            if (null == vector)
            {
                throw new ArgumentNullException("vector", "Cannot create translation from null vector");
            }
            GeneralTransform transform = new TranslateTransform(vector.X, vector.Y);
            return new Transform2(transform);
        }

        /// <summary>
        /// create transform to flip About a vector
        /// </summary>
        /// TODO not tested
        /// <param name="vector">to flip about</param>
        /// <param name="point">which remains invariant</param>
        /// <returns>transform (or null if bad args)</returns>
        public static Transform2 FlipAboutVectorThroughPoint(Vector2 vector, Point2 point)
        {
            GeneralTransform transform = null;
            if (vector != null && point != null)
            {
                TransformGroup tg = new TransformGroup();
                TranslateTransform tminus = new TranslateTransform(-point.X, -point.Y);
                tg.Children.Add(tminus);
                double angle = - Math.Atan2(vector.Y, vector.X)*180.0/Math.PI;
                RotateTransform aplus = new RotateTransform(angle);
                tg.Children.Add(aplus);
                ScaleTransform flip = new ScaleTransform(1.0, -1.0);
                tg.Children.Add(flip);
                RotateTransform aminus = new RotateTransform(-angle);
                tg.Children.Add(aminus);
                TranslateTransform tplus = new TranslateTransform(point.X, point.Y);
                tg.Children.Add(tplus);
                transform = tg;
            }
            return new Transform2(transform);
        }

        /// <summary>
        /// Apply the currently defined transformation to the point
        /// </summary>
        /// <param name="point">the point to apply the transformation to</param>
        /// <returns>The new Point2 which results from the currently defined transformation on the point given</returns>
        public Point2 Transform(Point2 point)
        {
            if (null == point)
            {
                throw new ArgumentNullException("point", "Transform2.Transform requires Point2 not null");
            }
            Point p = new Point(point.X, point.Y);
            Point newP = generalTransformField.Transform(p);
            return Point2.CreatePoint2(newP);
        }

        public IEnumerable<Point2> Transform(IEnumerable<Point2> points)
        {
            if (null == points)
            {
                throw new ArgumentNullException("points", "Transform2.Transform requires IEnumerable<Point2> not null");
            }

            List<Point2> newPoints = new List<Point2>();

            foreach (Point2 point in points)
            {
                newPoints.Add(Transform(point));
            }
            return newPoints;
        }

        public ICollection<Point2> T(ICollection<Point2> points)
        {
            List<Point2> newPoints = new List<Point2>(points.Count);
            foreach (Point2 point in points)
            {
                newPoints.Add(point);
            }
            return newPoints;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return generalTransformField.ToString();
        }
    }
}