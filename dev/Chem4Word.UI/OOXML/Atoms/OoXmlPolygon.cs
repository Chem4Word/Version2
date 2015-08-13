using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Word2010AddIn.OOXML.Atoms
{
    public class OoXmlPolygon
    {
        /// <summary>
        /// List of Points for the Polygon
        /// </summary>
        public List<Point> TTFPoints;

        public OoXmlPolygon()
        {
            TTFPoints = new List<Point>();
        }
    }
}
