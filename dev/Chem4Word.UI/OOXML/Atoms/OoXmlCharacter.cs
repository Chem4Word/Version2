using System.Collections.Generic;
using System.Windows;

namespace Chem4Word.UI.OOXML.Atoms
{
    public class OoXmlCharacter
    {
        /// <summary>
        /// List of Polygons for the Character
        /// </summary>
        public List<OoXmlPolygon> OoXmlPolygons;

        /// <summary>
        /// The width of the smallest rectangle that completely encloses the glyph (its black box).
        /// </summary>
        public double BlackBoxX;

        /// <summary>
        /// The height of the smallest rectangle that completely encloses the glyph (its black box).
        /// </summary>
        public double BlackBoxY;

        /// <summary>
        /// The horizontal distance from the origin of the current character cell to the origin of the next character cell.
        /// </summary>
        public double CellIncX;

        /// <summary>
        /// The vertical distance from the origin of the current character cell to the origin of the next character cell.
        /// </summary>
        public double CellIncY;

        /// <summary>
        /// The x- and y-coordinates of the upper left corner of the smallest rectangle that completely encloses the glyph.
        /// </summary>
        public Point GlyphOrigin;

        /// <summary>
        /// Tweak X for this character
        /// </summary>
        public double OffsetX;

        /// <summary>
        /// Tweak Y for this character
        /// </summary>
        public double OffsetY;

        public OoXmlCharacter()
        {
            OoXmlPolygons = new List<OoXmlPolygon>();
        }
    }
}
