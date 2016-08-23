using DocumentFormat.OpenXml;

namespace Chem4Word.UI.OOXML
{
    public static class Helper
    {
        // Margins are in CML Points
        public const double DRAWING_MARGIN = 5; // 5 is a good value to use (Use 0 to compare with AMC diagrams)
        public const double CHARACTER_CLIPPING_MARGIN = 1.25;

        // Percentage of average (median) bond length
        public const double MULTIPLE_BOND_OFFSET_PERCENTAGE = 0.2;

        public const double SUBSCRIPT_SCALE_FACTOR = 0.6;

        public const double SUBSCRIPT_DROP_FACTOR = 0.75;
        public const double SUPERSCRIPT_RAISE_FACTOR = 0.25;

        private const double EMUS_PER_TTF_POINT = 37.5;
        private const double EMUS_PER_CML_POINT = 9500;

        private const double EMUS_PER_TTF_POINT_SUBSCRIPT = EMUS_PER_TTF_POINT * SUBSCRIPT_SCALE_FACTOR;
        private const double TTF_TO_CML = EMUS_PER_CML_POINT / EMUS_PER_TTF_POINT;

        /// <summary>
        /// Scale a CML x or Y co-ordinate to WordML Units (EMU)
        /// </summary>
        /// <param name="XorY"></param>
        /// <returns></returns>
        public static Int64Value ScaleCmlForWord(double XorY)
        {
            double scaled = XorY * EMUS_PER_CML_POINT;
            return Int64Value.FromInt64((long)scaled);
        }
        public static double ScaleCmlToEmu(double XorY)
        {
            return XorY * EMUS_PER_CML_POINT;
        }
        public static double ScaleEmuToCml(double XorY)
        {
            return XorY / EMUS_PER_CML_POINT;
        }

        /// <summary>
        /// Scale a TTF x or Y co-ordinate to WordML Units (EMU)
        /// </summary>
        /// <param name="XorY"></param>
        /// <returns></returns>
        public static Int64Value ScaleTtfForWord(double XorY)
        {
            double scaled = XorY * EMUS_PER_TTF_POINT;
            return Int64Value.FromInt64((long)scaled);
        }
        public static double ScaleTtfToEmu(double XorY)
        {
            return XorY * EMUS_PER_TTF_POINT;
        }

        /// <summary>
        /// Scale a TTF SubScript x or Y co-ordinate to WordML Units (EMU)
        /// </summary>
        public static Int64Value ScaleTtfSubScriptForWord(double XorY)
        {
            double scaled = XorY * EMUS_PER_TTF_POINT_SUBSCRIPT;
            return Int64Value.FromInt64((long)scaled);
        }

        public static double ScaleTtfToCml(double XorY)
        {
            return XorY / TTF_TO_CML;
        }
    }
}
