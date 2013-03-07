// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
namespace Euclid
{
    public class Vector2AndPoint2
    {
        public Vector2AndPoint2(Vector2 v, Point2 p)
        {
            this.Vector = v;
            this.Point = p;
        }

        public Vector2 Vector { get; set; }
        public Point2 Point { get; set; }
    }
}