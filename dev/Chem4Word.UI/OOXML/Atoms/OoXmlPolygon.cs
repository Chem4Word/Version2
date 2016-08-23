using System.Collections.Generic;
using System.Windows;

namespace Chem4Word.UI.OOXML.Atoms
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
