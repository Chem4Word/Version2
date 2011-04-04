// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Euclid
{
    /// <summary>
    /// class may have a defined centroid
    /// can be applied to an object of any number of points including 1
    /// 
    /// </summary>
    /// <author>pmr</author>
    public interface IHasCentroid2
    {
        /// <summary>
        /// if no points returns null
        /// if no coordinates for the points returns null
        /// if some points have no coordinates it is up to the class to announce its
        /// strategy.
        /// Centroid is not weighted by mass
        /// </summary>
        /// <returns>centroid</returns>
        Point2 GetCentroid();

        /// <summary>
        /// list of centroids of components of this
        /// </summary>
        /// allows a compound object to return those subcomponents (e.g. bonds in a ring
        /// if object is primitive (e.g. an atom) returns null;
        /// <returns>null if object has no subcentroids and no coordinates</returns>
        ICollection<IHasCentroid2> GetSubCentroids();
    }
}