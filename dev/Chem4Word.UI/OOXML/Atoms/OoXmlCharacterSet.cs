using System.Collections.Generic;
using System.Windows;

namespace Chem4Word.UI.OOXML.Atoms
{
    public class OoXmlCharacterSet
    {
        private Dictionary<char, OoXmlCharacter> m_OoXmlCharacters;

        public OoXmlCharacterSet()
        {
            m_OoXmlCharacters = new Dictionary<char, OoXmlCharacter>();
        }

        /// <summary>
        /// Gets an OoXml Character
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        public OoXmlCharacter GetOoXmlCharacter(char chr)
        {
            OoXmlCharacter x = null;
            if (!m_OoXmlCharacters.TryGetValue(chr, out x))
            {
                #region Character needs to be added

                switch (chr)
                {
                    #region Numbers and Symbols

                    case '0':
                        InsertNumber0();
                        break;

                    case '1':
                        InsertNumber1();
                        break;

                    case '2':
                        InsertNumber2();
                        break;

                    case '3':
                        InsertNumber3();
                        break;

                    case '4':
                        InsertNumber4();
                        break;

                    case '5':
                        InsertNumber5();
                        break;

                    case '6':
                        InsertNumber6();
                        break;

                    case '7':
                        InsertNumber7();
                        break;

                    case '8':
                        InsertNumber8();
                        break;

                    case '9':
                        InsertNumber9();
                        break;

                    case '.':
                        InsertSymbolDot();
                        break;

                    case '+':
                        InsertSymbolPlus();
                        break;

                    case '-':
                        InsertSymbolMinus();
                        break;

                    #endregion Numbers and Symbols

                    #region Lower Case

                    case 'a':
                        InsertLowerCaseA();
                        break;

                    case 'b':
                        InsertLowerCaseB();
                        break;

                    case 'c':
                        InsertLowerCaseC();
                        break;

                    case 'd':
                        InsertLowerCaseD();
                        break;

                    case 'e':
                        InsertLowerCaseE();
                        break;

                    case 'f':
                        InsertLowerCaseF();
                        break;

                    case 'g':
                        InsertLowerCaseG();
                        break;

                    case 'h':
                        InsertLowerCaseH();
                        break;

                    case 'i':
                        InsertLowerCaseI();
                        break;

                    case 'j':
                        InsertLowerCaseJ();
                        break;

                    case 'k':
                        InsertLowerCaseK();
                        break;

                    case 'l':
                        InsertLowerCaseL();
                        break;

                    case 'm':
                        InsertLowerCaseM();
                        break;

                    case 'n':
                        InsertLowerCaseN();
                        break;

                    case 'o':
                        InsertLowerCaseO();
                        break;

                    case 'p':
                        InsertLowerCaseP();
                        break;

                    case 'q':
                        InsertLowerCaseQ();
                        break;

                    case 'r':
                        InsertLowerCaseR();
                        break;

                    case 's':
                        InsertLowerCaseS();
                        break;

                    case 't':
                        InsertLowerCaseT();
                        break;

                    case 'u':
                        InsertLowerCaseU();
                        break;

                    case 'v':
                        InsertLowerCaseV();
                        break;

                    case 'w':
                        InsertLowerCaseW();
                        break;

                    case 'x':
                        InsertLowerCaseX();
                        break;

                    case 'y':
                        InsertLowerCaseY();
                        break;

                    case 'z':
                        InsertLowerCaseZ();
                        break;

                    #endregion Lower Case

                    #region Upper Case

                    case 'A':
                        InsertUpperCaseA();
                        break;

                    case 'B':
                        InsertUpperCaseB();
                        break;

                    case 'C':
                        InsertUpperCaseC();
                        break;

                    case 'D':
                        InsertUpperCaseD();
                        break;

                    case 'E':
                        InsertUpperCaseE();
                        break;

                    case 'F':
                        InsertUpperCaseF();
                        break;

                    case 'G':
                        InsertUpperCaseG();
                        break;

                    case 'H':
                        InsertUpperCaseH();
                        break;

                    case 'I':
                        InsertUpperCaseI();
                        break;

                    case 'J':
                        InsertUpperCaseJ();
                        break;

                    case 'K':
                        InsertUpperCaseK();
                        break;

                    case 'L':
                        InsertUpperCaseL();
                        break;

                    case 'M':
                        InsertUpperCaseM();
                        break;

                    case 'N':
                        InsertUpperCaseN();
                        break;

                    case 'O':
                        InsertUpperCaseO();
                        break;

                    case 'P':
                        InsertUpperCaseP();
                        break;

                    case 'Q':
                        InsertUpperCaseQ();
                        break;

                    case 'R':
                        InsertUpperCaseR();
                        break;

                    case 'S':
                        InsertUpperCaseS();
                        break;

                    case 'T':
                        InsertUpperCaseT();
                        break;

                    case 'U':
                        InsertUpperCaseU();
                        break;

                    case 'V':
                        InsertUpperCaseV();
                        break;

                    case 'W':
                        InsertUpperCaseW();
                        break;

                    case 'X':
                        InsertUpperCaseX();
                        break;

                    case 'Y':
                        InsertUpperCaseY();
                        break;

                    case 'Z':
                        InsertUpperCaseZ();
                        break;

                    #endregion Upper Case

                    default:
                        break;
                }

                #endregion Character needs to be added

                // This time it should not fail
                m_OoXmlCharacters.TryGetValue(chr, out x);
            };
            return x;
        }

        #region Numbers and Symbols

        private void InsertNumber0()
        {
            #region Character 0

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1554;
            c.BlackBoxY = 2433;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(138, -2392);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(138.125, -1174.875));
            p0.TTFPoints.Add(new Point(143.5586, -1375.816));
            p0.TTFPoints.Add(new Point(159.8594, -1556.141));
            p0.TTFPoints.Add(new Point(187.0273, -1715.848));
            p0.TTFPoints.Add(new Point(225.0625, -1854.938));
            p0.TTFPoints.Add(new Point(273.8125, -1976.355));
            p0.TTFPoints.Add(new Point(333.125, -2083.047));
            p0.TTFPoints.Add(new Point(403, -2175.012));
            p0.TTFPoints.Add(new Point(483.4375, -2252.25));
            p0.TTFPoints.Add(new Point(574.6914, -2313.391));
            p0.TTFPoints.Add(new Point(677.0156, -2357.063));
            p0.TTFPoints.Add(new Point(790.4102, -2383.266));
            p0.TTFPoints.Add(new Point(914.875, -2392));
            p0.TTFPoints.Add(new Point(1007.805, -2387.176));
            p0.TTFPoints.Add(new Point(1094.844, -2372.703));
            p0.TTFPoints.Add(new Point(1175.992, -2348.582));
            p0.TTFPoints.Add(new Point(1251.25, -2314.813));
            p0.TTFPoints.Add(new Point(1320.414, -2271.953));
            p0.TTFPoints.Add(new Point(1383.281, -2220.563));
            p0.TTFPoints.Add(new Point(1439.852, -2160.641));
            p0.TTFPoints.Add(new Point(1490.125, -2092.188));
            p0.TTFPoints.Add(new Point(1534.711, -2015.508));
            p0.TTFPoints.Add(new Point(1574.219, -1930.906));
            p0.TTFPoints.Add(new Point(1608.648, -1838.383));
            p0.TTFPoints.Add(new Point(1638, -1737.938));
            p0.TTFPoints.Add(new Point(1661.461, -1624.441));
            p0.TTFPoints.Add(new Point(1678.219, -1492.766));
            p0.TTFPoints.Add(new Point(1688.273, -1342.91));
            p0.TTFPoints.Add(new Point(1691.625, -1174.875));
            p0.TTFPoints.Add(new Point(1686.242, -975.3555));
            p0.TTFPoints.Add(new Point(1670.094, -796.0469));
            p0.TTFPoints.Add(new Point(1643.18, -636.9492));
            p0.TTFPoints.Add(new Point(1605.5, -498.0625));
            p0.TTFPoints.Add(new Point(1557.105, -376.5938));
            p0.TTFPoints.Add(new Point(1498.047, -269.75));
            p0.TTFPoints.Add(new Point(1428.324, -177.5313));
            p0.TTFPoints.Add(new Point(1347.938, -99.9375));
            p0.TTFPoints.Add(new Point(1256.582, -38.44141));
            p0.TTFPoints.Add(new Point(1153.953, 5.484375));
            p0.TTFPoints.Add(new Point(1040.051, 31.83984));
            p0.TTFPoints.Add(new Point(914.875, 40.625));
            p0.TTFPoints.Add(new Point(751.8672, 25.1875));
            p0.TTFPoints.Add(new Point(607.3438, -21.125));
            p0.TTFPoints.Add(new Point(481.3047, -98.3125));
            p0.TTFPoints.Add(new Point(373.75, -206.375));
            p0.TTFPoints.Add(new Point(270.6641, -378.4219));
            p0.TTFPoints.Add(new Point(197.0313, -597.1875));
            p0.TTFPoints.Add(new Point(152.8516, -862.6719));
            p0.TTFPoints.Add(new Point(138.125, -1174.875));
            p0.TTFPoints.Add(new Point(138.125, -1174.875));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(438.75, -1174.875));
            p1.TTFPoints.Add(new Point(447.332, -906.0898));
            p1.TTFPoints.Add(new Point(473.0781, -686.3594));
            p1.TTFPoints.Add(new Point(515.9883, -515.6836));
            p1.TTFPoints.Add(new Point(576.0625, -394.0625));
            p1.TTFPoints.Add(new Point(648.7305, -309.1055));
            p1.TTFPoints.Add(new Point(729.4219, -248.4219));
            p1.TTFPoints.Add(new Point(818.1367, -212.0117));
            p1.TTFPoints.Add(new Point(914.875, -199.875));
            p1.TTFPoints.Add(new Point(1011.613, -212.0625));
            p1.TTFPoints.Add(new Point(1100.328, -248.625));
            p1.TTFPoints.Add(new Point(1181.02, -309.5625));
            p1.TTFPoints.Add(new Point(1253.688, -394.875));
            p1.TTFPoints.Add(new Point(1313.762, -516.75));
            p1.TTFPoints.Add(new Point(1356.672, -687.375));
            p1.TTFPoints.Add(new Point(1382.418, -906.75));
            p1.TTFPoints.Add(new Point(1391, -1174.875));
            p1.TTFPoints.Add(new Point(1382.418, -1444.32));
            p1.TTFPoints.Add(new Point(1356.672, -1664.406));
            p1.TTFPoints.Add(new Point(1313.762, -1835.133));
            p1.TTFPoints.Add(new Point(1253.688, -1956.5));
            p1.TTFPoints.Add(new Point(1180.816, -2041.102));
            p1.TTFPoints.Add(new Point(1099.516, -2101.531));
            p1.TTFPoints.Add(new Point(1009.785, -2137.789));
            p1.TTFPoints.Add(new Point(911.625, -2149.875));
            p1.TTFPoints.Add(new Point(815.9531, -2139.211));
            p1.TTFPoints.Add(new Point(730.4375, -2107.219));
            p1.TTFPoints.Add(new Point(655.0781, -2053.898));
            p1.TTFPoints.Add(new Point(589.875, -1979.25));
            p1.TTFPoints.Add(new Point(523.7578, -1847.32));
            p1.TTFPoints.Add(new Point(476.5313, -1669.281));
            p1.TTFPoints.Add(new Point(448.1953, -1445.133));
            p1.TTFPoints.Add(new Point(438.75, -1174.875));
            p1.TTFPoints.Add(new Point(438.75, -1174.875));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('0', c);

            #endregion Character 0
        }

        private void InsertNumber1()
        {
            #region Character 1

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 878;
            c.BlackBoxY = 2392;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(362, -2392);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(1239.875, 0));
            p0.TTFPoints.Add(new Point(947.375, 0));
            p0.TTFPoints.Add(new Point(947.375, -1863.875));
            p0.TTFPoints.Add(new Point(890.4492, -1813.5));
            p0.TTFPoints.Add(new Point(825.2969, -1763.125));
            p0.TTFPoints.Add(new Point(751.918, -1712.75));
            p0.TTFPoints.Add(new Point(670.3125, -1662.375));
            p0.TTFPoints.Add(new Point(586.7773, -1615.148));
            p0.TTFPoints.Add(new Point(507.6094, -1574.219));
            p0.TTFPoints.Add(new Point(432.8086, -1539.586));
            p0.TTFPoints.Add(new Point(362.375, -1511.25));
            p0.TTFPoints.Add(new Point(362.375, -1794));
            p0.TTFPoints.Add(new Point(481.2031, -1854.734));
            p0.TTFPoints.Add(new Point(592.3125, -1921.563));
            p0.TTFPoints.Add(new Point(695.7031, -1994.484));
            p0.TTFPoints.Add(new Point(791.375, -2073.5));
            p0.TTFPoints.Add(new Point(876.4844, -2154.953));
            p0.TTFPoints.Add(new Point(948.1875, -2235.188));
            p0.TTFPoints.Add(new Point(1006.484, -2314.203));
            p0.TTFPoints.Add(new Point(1051.375, -2392));
            p0.TTFPoints.Add(new Point(1239.875, -2392));
            p0.TTFPoints.Add(new Point(1239.875, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('1', c);

            #endregion Character 1
        }

        private void InsertNumber2()
        {
            #region Character 2

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1578;
            c.BlackBoxY = 2392;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(97, -2392);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(1675.375, -281.125));
            p0.TTFPoints.Add(new Point(1675.375, 0));
            p0.TTFPoints.Add(new Point(100.75, 0));
            p0.TTFPoints.Add(new Point(101.6641, -52.30469));
            p0.TTFPoints.Add(new Point(107.6563, -103.5938));
            p0.TTFPoints.Add(new Point(118.7266, -153.8672));
            p0.TTFPoints.Add(new Point(134.875, -203.125));
            p0.TTFPoints.Add(new Point(169.457, -283.2578));
            p0.TTFPoints.Add(new Point(213.0781, -362.7813));
            p0.TTFPoints.Add(new Point(265.7383, -441.6953));
            p0.TTFPoints.Add(new Point(327.4375, -520));
            p0.TTFPoints.Add(new Point(401.0195, -601.0469));
            p0.TTFPoints.Add(new Point(489.3281, -688.1875));
            p0.TTFPoints.Add(new Point(592.3633, -781.4219));
            p0.TTFPoints.Add(new Point(710.125, -880.75));
            p0.TTFPoints.Add(new Point(888.5703, -1031.723));
            p0.TTFPoints.Add(new Point(1035.531, -1166.141));
            p0.TTFPoints.Add(new Point(1151.008, -1284.004));
            p0.TTFPoints.Add(new Point(1235, -1385.313));
            p0.TTFPoints.Add(new Point(1294.719, -1477.074));
            p0.TTFPoints.Add(new Point(1337.375, -1566.297));
            p0.TTFPoints.Add(new Point(1362.969, -1652.98));
            p0.TTFPoints.Add(new Point(1371.5, -1737.125));
            p0.TTFPoints.Add(new Point(1363.73, -1820.66));
            p0.TTFPoints.Add(new Point(1340.422, -1897.391));
            p0.TTFPoints.Add(new Point(1301.574, -1967.316));
            p0.TTFPoints.Add(new Point(1247.188, -2030.438));
            p0.TTFPoints.Add(new Point(1180.309, -2082.691));
            p0.TTFPoints.Add(new Point(1103.984, -2120.016));
            p0.TTFPoints.Add(new Point(1018.215, -2142.41));
            p0.TTFPoints.Add(new Point(923, -2149.875));
            p0.TTFPoints.Add(new Point(822.6563, -2141.953));
            p0.TTFPoints.Add(new Point(732.875, -2118.188));
            p0.TTFPoints.Add(new Point(653.6563, -2078.578));
            p0.TTFPoints.Add(new Point(585, -2023.125));
            p0.TTFPoints.Add(new Point(529.4453, -1953.656));
            p0.TTFPoints.Add(new Point(489.5313, -1872));
            p0.TTFPoints.Add(new Point(465.2578, -1778.156));
            p0.TTFPoints.Add(new Point(456.625, -1672.125));
            p0.TTFPoints.Add(new Point(156, -1703));
            p0.TTFPoints.Add(new Point(182.1016, -1861.184));
            p0.TTFPoints.Add(new Point(229.5313, -1999.359));
            p0.TTFPoints.Add(new Point(298.2891, -2117.527));
            p0.TTFPoints.Add(new Point(388.375, -2215.688));
            p0.TTFPoints.Add(new Point(497.7578, -2292.824));
            p0.TTFPoints.Add(new Point(624.4063, -2347.922));
            p0.TTFPoints.Add(new Point(768.3203, -2380.98));
            p0.TTFPoints.Add(new Point(929.5, -2392));
            p0.TTFPoints.Add(new Point(1092, -2380.117));
            p0.TTFPoints.Add(new Point(1236.625, -2344.469));
            p0.TTFPoints.Add(new Point(1363.375, -2285.055));
            p0.TTFPoints.Add(new Point(1472.25, -2201.875));
            p0.TTFPoints.Add(new Point(1559.695, -2101.125));
            p0.TTFPoints.Add(new Point(1622.156, -1989));
            p0.TTFPoints.Add(new Point(1659.633, -1865.5));
            p0.TTFPoints.Add(new Point(1672.125, -1730.625));
            p0.TTFPoints.Add(new Point(1668.469, -1659.43));
            p0.TTFPoints.Add(new Point(1657.5, -1588.844));
            p0.TTFPoints.Add(new Point(1639.219, -1518.867));
            p0.TTFPoints.Add(new Point(1613.625, -1449.5));
            p0.TTFPoints.Add(new Point(1579.551, -1379.523));
            p0.TTFPoints.Add(new Point(1535.828, -1307.719));
            p0.TTFPoints.Add(new Point(1482.457, -1234.086));
            p0.TTFPoints.Add(new Point(1419.438, -1158.625));
            p0.TTFPoints.Add(new Point(1340.371, -1075.141));
            p0.TTFPoints.Add(new Point(1238.859, -977.4375));
            p0.TTFPoints.Add(new Point(1114.902, -865.5156));
            p0.TTFPoints.Add(new Point(968.5, -739.375));
            p0.TTFPoints.Add(new Point(848.6563, -637.7617));
            p0.TTFPoints.Add(new Point(752.375, -553.9219));
            p0.TTFPoints.Add(new Point(679.6563, -487.8555));
            p0.TTFPoints.Add(new Point(630.5, -439.5625));
            p0.TTFPoints.Add(new Point(594.75, -400.1055));
            p0.TTFPoints.Add(new Point(562.25, -360.5469));
            p0.TTFPoints.Add(new Point(533, -320.8867));
            p0.TTFPoints.Add(new Point(507, -281.125));
            p0.TTFPoints.Add(new Point(1675.375, -281.125));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('2', c);

            #endregion Character 2
        }

        private void InsertNumber3()
        {
            #region Character 3

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1560;
            c.BlackBoxY = 2434;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(140, -2392);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(139.75, -628.875));
            p0.TTFPoints.Add(new Point(432.25, -667.875));
            p0.TTFPoints.Add(new Point(461.8555, -552.2461));
            p0.TTFPoints.Add(new Point(500.2969, -453.9844));
            p0.TTFPoints.Add(new Point(547.5742, -373.0898));
            p0.TTFPoints.Add(new Point(603.6875, -309.5625));
            p0.TTFPoints.Add(new Point(667.5195, -261.5742));
            p0.TTFPoints.Add(new Point(737.9531, -227.2969));
            p0.TTFPoints.Add(new Point(814.9883, -206.7305));
            p0.TTFPoints.Add(new Point(898.625, -199.875));
            p0.TTFPoints.Add(new Point(997.8008, -208.8125));
            p0.TTFPoints.Add(new Point(1088.953, -235.625));
            p0.TTFPoints.Add(new Point(1172.082, -280.3125));
            p0.TTFPoints.Add(new Point(1247.188, -342.875));
            p0.TTFPoints.Add(new Point(1309.395, -418.6406));
            p0.TTFPoints.Add(new Point(1353.828, -502.9375));
            p0.TTFPoints.Add(new Point(1380.488, -595.7656));
            p0.TTFPoints.Add(new Point(1389.375, -697.125));
            p0.TTFPoints.Add(new Point(1381.148, -793.457));
            p0.TTFPoints.Add(new Point(1356.469, -880.9531));
            p0.TTFPoints.Add(new Point(1315.336, -959.6133));
            p0.TTFPoints.Add(new Point(1257.75, -1029.438));
            p0.TTFPoints.Add(new Point(1187.469, -1086.668));
            p0.TTFPoints.Add(new Point(1108.25, -1127.547));
            p0.TTFPoints.Add(new Point(1020.094, -1152.074));
            p0.TTFPoints.Add(new Point(923, -1160.25));
            p0.TTFPoints.Add(new Point(879.0234, -1158.219));
            p0.TTFPoints.Add(new Point(829.9688, -1152.125));
            p0.TTFPoints.Add(new Point(775.8359, -1141.969));
            p0.TTFPoints.Add(new Point(716.625, -1127.75));
            p0.TTFPoints.Add(new Point(749.125, -1384.5));
            p0.TTFPoints.Add(new Point(763.0391, -1383.078));
            p0.TTFPoints.Add(new Point(775.5313, -1382.063));
            p0.TTFPoints.Add(new Point(786.6016, -1381.453));
            p0.TTFPoints.Add(new Point(796.25, -1381.25));
            p0.TTFPoints.Add(new Point(887.3516, -1387.344));
            p0.TTFPoints.Add(new Point(973.7813, -1405.625));
            p0.TTFPoints.Add(new Point(1055.539, -1436.094));
            p0.TTFPoints.Add(new Point(1132.625, -1478.75));
            p0.TTFPoints.Add(new Point(1198.031, -1534.102));
            p0.TTFPoints.Add(new Point(1244.75, -1602.656));
            p0.TTFPoints.Add(new Point(1272.781, -1684.414));
            p0.TTFPoints.Add(new Point(1282.125, -1779.375));
            p0.TTFPoints.Add(new Point(1275.32, -1856.359));
            p0.TTFPoints.Add(new Point(1254.906, -1926.438));
            p0.TTFPoints.Add(new Point(1220.883, -1989.609));
            p0.TTFPoints.Add(new Point(1173.25, -2045.875));
            p0.TTFPoints.Add(new Point(1114.852, -2092.086));
            p0.TTFPoints.Add(new Point(1048.531, -2125.094));
            p0.TTFPoints.Add(new Point(974.2891, -2144.898));
            p0.TTFPoints.Add(new Point(892.125, -2151.5));
            p0.TTFPoints.Add(new Point(810.3672, -2144.797));
            p0.TTFPoints.Add(new Point(735.7188, -2124.688));
            p0.TTFPoints.Add(new Point(668.1797, -2091.172));
            p0.TTFPoints.Add(new Point(607.75, -2044.25));
            p0.TTFPoints.Add(new Point(555.9531, -1983.922));
            p0.TTFPoints.Add(new Point(514.3125, -1910.188));
            p0.TTFPoints.Add(new Point(482.8281, -1823.047));
            p0.TTFPoints.Add(new Point(461.5, -1722.5));
            p0.TTFPoints.Add(new Point(169, -1774.5));
            p0.TTFPoints.Add(new Point(204.3438, -1913.285));
            p0.TTFPoints.Add(new Point(256.75, -2035.516));
            p0.TTFPoints.Add(new Point(326.2188, -2141.191));
            p0.TTFPoints.Add(new Point(412.75, -2230.313));
            p0.TTFPoints.Add(new Point(513.6016, -2301.051));
            p0.TTFPoints.Add(new Point(626.0313, -2351.578));
            p0.TTFPoints.Add(new Point(750.0391, -2381.895));
            p0.TTFPoints.Add(new Point(885.625, -2392));
            p0.TTFPoints.Add(new Point(981.1953, -2386.77));
            p0.TTFPoints.Add(new Point(1072.906, -2371.078));
            p0.TTFPoints.Add(new Point(1160.758, -2344.926));
            p0.TTFPoints.Add(new Point(1244.75, -2308.313));
            p0.TTFPoints.Add(new Point(1321.988, -2262.66));
            p0.TTFPoints.Add(new Point(1389.578, -2209.391));
            p0.TTFPoints.Add(new Point(1447.52, -2148.504));
            p0.TTFPoints.Add(new Point(1495.813, -2080));
            p0.TTFPoints.Add(new Point(1533.848, -2006.57));
            p0.TTFPoints.Add(new Point(1561.016, -1930.906));
            p0.TTFPoints.Add(new Point(1577.316, -1853.008));
            p0.TTFPoints.Add(new Point(1582.75, -1772.875));
            p0.TTFPoints.Add(new Point(1577.57, -1697.414));
            p0.TTFPoints.Add(new Point(1562.031, -1625.406));
            p0.TTFPoints.Add(new Point(1536.133, -1556.852));
            p0.TTFPoints.Add(new Point(1499.875, -1491.75));
            p0.TTFPoints.Add(new Point(1453.461, -1431.625));
            p0.TTFPoints.Add(new Point(1397.094, -1378));
            p0.TTFPoints.Add(new Point(1330.773, -1330.875));
            p0.TTFPoints.Add(new Point(1254.5, -1290.25));
            p0.TTFPoints.Add(new Point(1354.234, -1259.324));
            p0.TTFPoints.Add(new Point(1442.188, -1215.297));
            p0.TTFPoints.Add(new Point(1518.359, -1158.168));
            p0.TTFPoints.Add(new Point(1582.75, -1087.938));
            p0.TTFPoints.Add(new Point(1633.938, -1006.332));
            p0.TTFPoints.Add(new Point(1670.5, -915.0781));
            p0.TTFPoints.Add(new Point(1692.438, -814.1758));
            p0.TTFPoints.Add(new Point(1699.75, -703.625));
            p0.TTFPoints.Add(new Point(1685.531, -553.5664));
            p0.TTFPoints.Add(new Point(1642.875, -415.3906));
            p0.TTFPoints.Add(new Point(1571.781, -289.0977));
            p0.TTFPoints.Add(new Point(1472.25, -174.6875));
            p0.TTFPoints.Add(new Point(1350.984, -79.77734));
            p0.TTFPoints.Add(new Point(1214.688, -11.98438));
            p0.TTFPoints.Add(new Point(1063.359, 28.69141));
            p0.TTFPoints.Add(new Point(897, 42.25));
            p0.TTFPoints.Add(new Point(746.8398, 30.57031));
            p0.TTFPoints.Add(new Point(609.9844, -4.46875));
            p0.TTFPoints.Add(new Point(486.4336, -62.86719));
            p0.TTFPoints.Add(new Point(376.1875, -144.625));
            p0.TTFPoints.Add(new Point(283.7148, -244.9688));
            p0.TTFPoints.Add(new Point(213.4844, -359.125));
            p0.TTFPoints.Add(new Point(165.4961, -487.0938));
            p0.TTFPoints.Add(new Point(139.75, -628.875));
            p0.TTFPoints.Add(new Point(139.75, -628.875));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('3', c);

            #endregion Character 3
        }

        private void InsertNumber4()
        {
            #region Character 4

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1648;
            c.BlackBoxY = 2382;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(42, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(1075.75, 0));
            p0.TTFPoints.Add(new Point(1075.75, -570.375));
            p0.TTFPoints.Add(new Point(42.25, -570.375));
            p0.TTFPoints.Add(new Point(42.25, -838.5));
            p0.TTFPoints.Add(new Point(1129.375, -2382.25));
            p0.TTFPoints.Add(new Point(1368.25, -2382.25));
            p0.TTFPoints.Add(new Point(1368.25, -838.5));
            p0.TTFPoints.Add(new Point(1690, -838.5));
            p0.TTFPoints.Add(new Point(1690, -570.375));
            p0.TTFPoints.Add(new Point(1368.25, -570.375));
            p0.TTFPoints.Add(new Point(1368.25, 0));
            p0.TTFPoints.Add(new Point(1075.75, 0));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(1075.75, -838.5));
            p1.TTFPoints.Add(new Point(1075.75, -1912.625));
            p1.TTFPoints.Add(new Point(329.875, -838.5));
            p1.TTFPoints.Add(new Point(1075.75, -838.5));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('4', c);

            #endregion Character 4
        }

        private void InsertNumber5()
        {
            #region Character 5

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1580;
            c.BlackBoxY = 2391;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(138, -2350);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(138.125, -624));
            p0.TTFPoints.Add(new Point(445.25, -650));
            p0.TTFPoints.Add(new Point(467.9492, -544.832));
            p0.TTFPoints.Add(new Point(501.9219, -453.5781));
            p0.TTFPoints.Add(new Point(547.168, -376.2383));
            p0.TTFPoints.Add(new Point(603.6875, -312.8125));
            p0.TTFPoints.Add(new Point(669.043, -263.4023));
            p0.TTFPoints.Add(new Point(740.7969, -228.1094));
            p0.TTFPoints.Add(new Point(818.9492, -206.9336));
            p0.TTFPoints.Add(new Point(903.5, -199.875));
            p0.TTFPoints.Add(new Point(1005.063, -209.8281));
            p0.TTFPoints.Add(new Point(1098.5, -239.6875));
            p0.TTFPoints.Add(new Point(1183.813, -289.4531));
            p0.TTFPoints.Add(new Point(1261, -359.125));
            p0.TTFPoints.Add(new Point(1324.984, -445.25));
            p0.TTFPoints.Add(new Point(1370.688, -544.375));
            p0.TTFPoints.Add(new Point(1398.109, -656.5));
            p0.TTFPoints.Add(new Point(1407.25, -781.625));
            p0.TTFPoints.Add(new Point(1398.465, -900.1484));
            p0.TTFPoints.Add(new Point(1372.109, -1005.469));
            p0.TTFPoints.Add(new Point(1328.184, -1097.586));
            p0.TTFPoints.Add(new Point(1266.688, -1176.5));
            p0.TTFPoints.Add(new Point(1190.973, -1239.773));
            p0.TTFPoints.Add(new Point(1104.391, -1284.969));
            p0.TTFPoints.Add(new Point(1006.941, -1312.086));
            p0.TTFPoints.Add(new Point(898.625, -1321.125));
            p0.TTFPoints.Add(new Point(829.6641, -1317.113));
            p0.TTFPoints.Add(new Point(764.1563, -1305.078));
            p0.TTFPoints.Add(new Point(702.1016, -1285.02));
            p0.TTFPoints.Add(new Point(643.5, -1256.938));
            p0.TTFPoints.Add(new Point(589.6719, -1222.457));
            p0.TTFPoints.Add(new Point(541.9375, -1183.203));
            p0.TTFPoints.Add(new Point(500.2969, -1139.176));
            p0.TTFPoints.Add(new Point(464.75, -1090.375));
            p0.TTFPoints.Add(new Point(190.125, -1126.125));
            p0.TTFPoints.Add(new Point(420.875, -2349.75));
            p0.TTFPoints.Add(new Point(1605.5, -2349.75));
            p0.TTFPoints.Add(new Point(1605.5, -2070.25));
            p0.TTFPoints.Add(new Point(654.875, -2070.25));
            p0.TTFPoints.Add(new Point(526.5, -1430));
            p0.TTFPoints.Add(new Point(635.0703, -1495.406));
            p0.TTFPoints.Add(new Point(746.2813, -1542.125));
            p0.TTFPoints.Add(new Point(860.1328, -1570.156));
            p0.TTFPoints.Add(new Point(976.625, -1579.5));
            p0.TTFPoints.Add(new Point(1126.531, -1565.992));
            p0.TTFPoints.Add(new Point(1264.25, -1525.469));
            p0.TTFPoints.Add(new Point(1389.781, -1457.93));
            p0.TTFPoints.Add(new Point(1503.125, -1363.375));
            p0.TTFPoints.Add(new Point(1596.969, -1247.594));
            p0.TTFPoints.Add(new Point(1664, -1116.375));
            p0.TTFPoints.Add(new Point(1704.219, -969.7188));
            p0.TTFPoints.Add(new Point(1717.625, -807.625));
            p0.TTFPoints.Add(new Point(1705.844, -651.4219));
            p0.TTFPoints.Add(new Point(1670.5, -506.1875));
            p0.TTFPoints.Add(new Point(1611.594, -371.9219));
            p0.TTFPoints.Add(new Point(1529.125, -248.625));
            p0.TTFPoints.Add(new Point(1404.102, -122.0781));
            p0.TTFPoints.Add(new Point(1258.156, -31.6875));
            p0.TTFPoints.Add(new Point(1091.289, 22.54688));
            p0.TTFPoints.Add(new Point(903.5, 40.625));
            p0.TTFPoints.Add(new Point(748.4648, 29.25));
            p0.TTFPoints.Add(new Point(608.3594, -4.875));
            p0.TTFPoints.Add(new Point(483.1836, -61.75));
            p0.TTFPoints.Add(new Point(372.9375, -141.375));
            p0.TTFPoints.Add(new Point(281.1758, -239.7891));
            p0.TTFPoints.Add(new Point(211.4531, -353.0313));
            p0.TTFPoints.Add(new Point(163.7695, -481.1016));
            p0.TTFPoints.Add(new Point(138.125, -624));
            p0.TTFPoints.Add(new Point(138.125, -624));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('5', c);

            #endregion Character 5
        }

        private void InsertNumber6()
        {
            #region Character 6

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1573;
            c.BlackBoxY = 2433;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(125, -2392);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(1655.875, -1798.875));
            p0.TTFPoints.Add(new Point(1365, -1776.125));
            p0.TTFPoints.Add(new Point(1343.469, -1856.359));
            p0.TTFPoints.Add(new Point(1317.875, -1924.813));
            p0.TTFPoints.Add(new Point(1288.219, -1981.484));
            p0.TTFPoints.Add(new Point(1254.5, -2026.375));
            p0.TTFPoints.Add(new Point(1191.734, -2081.117));
            p0.TTFPoints.Add(new Point(1122.063, -2120.219));
            p0.TTFPoints.Add(new Point(1045.484, -2143.68));
            p0.TTFPoints.Add(new Point(962, -2151.5));
            p0.TTFPoints.Add(new Point(894.2578, -2146.625));
            p0.TTFPoints.Add(new Point(830.7813, -2132));
            p0.TTFPoints.Add(new Point(771.5703, -2107.625));
            p0.TTFPoints.Add(new Point(716.625, -2073.5));
            p0.TTFPoints.Add(new Point(651.2188, -2017.336));
            p0.TTFPoints.Add(new Point(593.125, -1949.594));
            p0.TTFPoints.Add(new Point(542.3438, -1870.273));
            p0.TTFPoints.Add(new Point(498.875, -1779.375));
            p0.TTFPoints.Add(new Point(463.8359, -1672.43));
            p0.TTFPoints.Add(new Point(438.3438, -1544.969));
            p0.TTFPoints.Add(new Point(422.3984, -1396.992));
            p0.TTFPoints.Add(new Point(416, -1228.5));
            p0.TTFPoints.Add(new Point(471.7578, -1303.758));
            p0.TTFPoints.Add(new Point(533.4063, -1368.656));
            p0.TTFPoints.Add(new Point(600.9453, -1423.195));
            p0.TTFPoints.Add(new Point(674.375, -1467.375));
            p0.TTFPoints.Add(new Point(751.6641, -1501.5));
            p0.TTFPoints.Add(new Point(830.7813, -1525.875));
            p0.TTFPoints.Add(new Point(911.7266, -1540.5));
            p0.TTFPoints.Add(new Point(994.5, -1545.375));
            p0.TTFPoints.Add(new Point(1135.316, -1531.918));
            p0.TTFPoints.Add(new Point(1265.266, -1491.547));
            p0.TTFPoints.Add(new Point(1384.348, -1424.262));
            p0.TTFPoints.Add(new Point(1492.563, -1330.063));
            p0.TTFPoints.Add(new Point(1582.496, -1214.535));
            p0.TTFPoints.Add(new Point(1646.734, -1083.266));
            p0.TTFPoints.Add(new Point(1685.277, -936.2539));
            p0.TTFPoints.Add(new Point(1698.125, -773.5));
            p0.TTFPoints.Add(new Point(1692.082, -663.3555));
            p0.TTFPoints.Add(new Point(1673.953, -557.1719));
            p0.TTFPoints.Add(new Point(1643.738, -454.9492));
            p0.TTFPoints.Add(new Point(1601.438, -356.6875));
            p0.TTFPoints.Add(new Point(1548.574, -266.043));
            p0.TTFPoints.Add(new Point(1486.672, -186.6719));
            p0.TTFPoints.Add(new Point(1415.73, -118.5742));
            p0.TTFPoints.Add(new Point(1335.75, -61.75));
            p0.TTFPoints.Add(new Point(1248.406, -16.96094));
            p0.TTFPoints.Add(new Point(1155.375, 15.03125));
            p0.TTFPoints.Add(new Point(1056.656, 34.22656));
            p0.TTFPoints.Add(new Point(952.25, 40.625));
            p0.TTFPoints.Add(new Point(777.8672, 23.81641));
            p0.TTFPoints.Add(new Point(620.3438, -26.60938));
            p0.TTFPoints.Add(new Point(479.6797, -110.6523));
            p0.TTFPoints.Add(new Point(355.875, -228.3125));
            p0.TTFPoints.Add(new Point(254.9219, -384.5664));
            p0.TTFPoints.Add(new Point(182.8125, -584.3906));
            p0.TTFPoints.Add(new Point(139.5469, -827.7852));
            p0.TTFPoints.Add(new Point(125.125, -1114.75));
            p0.TTFPoints.Add(new Point(141.0703, -1436.5));
            p0.TTFPoints.Add(new Point(188.9063, -1711.125));
            p0.TTFPoints.Add(new Point(268.6328, -1938.625));
            p0.TTFPoints.Add(new Point(380.25, -2119));
            p0.TTFPoints.Add(new Point(501.2109, -2238.438));
            p0.TTFPoints.Add(new Point(641.4688, -2323.75));
            p0.TTFPoints.Add(new Point(801.0234, -2374.938));
            p0.TTFPoints.Add(new Point(979.875, -2392));
            p0.TTFPoints.Add(new Point(1114.09, -2382.148));
            p0.TTFPoints.Add(new Point(1235.609, -2352.594));
            p0.TTFPoints.Add(new Point(1344.434, -2303.336));
            p0.TTFPoints.Add(new Point(1440.563, -2234.375));
            p0.TTFPoints.Add(new Point(1521.355, -2148.047));
            p0.TTFPoints.Add(new Point(1584.172, -2046.688));
            p0.TTFPoints.Add(new Point(1629.012, -1930.297));
            p0.TTFPoints.Add(new Point(1655.875, -1798.875));
            p0.TTFPoints.Add(new Point(1655.875, -1798.875));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(461.5, -771.875));
            p1.TTFPoints.Add(new Point(465.5117, -697.125));
            p1.TTFPoints.Add(new Point(477.5469, -624));
            p1.TTFPoints.Add(new Point(497.6055, -552.5));
            p1.TTFPoints.Add(new Point(525.6875, -482.625));
            p1.TTFPoints.Add(new Point(560.9805, -417.6758));
            p1.TTFPoints.Add(new Point(602.6719, -360.9531));
            p1.TTFPoints.Add(new Point(650.7617, -312.457));
            p1.TTFPoints.Add(new Point(705.25, -272.1875));
            p1.TTFPoints.Add(new Point(763.6484, -240.5508));
            p1.TTFPoints.Add(new Point(823.4688, -217.9531));
            p1.TTFPoints.Add(new Point(884.7109, -204.3945));
            p1.TTFPoints.Add(new Point(947.375, -199.875));
            p1.TTFPoints.Add(new Point(1036.75, -209.2188));
            p1.TTFPoints.Add(new Point(1119.625, -237.25));
            p1.TTFPoints.Add(new Point(1196, -283.9688));
            p1.TTFPoints.Add(new Point(1265.875, -349.375));
            p1.TTFPoints.Add(new Point(1324.172, -430.8281));
            p1.TTFPoints.Add(new Point(1365.813, -525.6875));
            p1.TTFPoints.Add(new Point(1390.797, -633.9531));
            p1.TTFPoints.Add(new Point(1399.125, -755.625));
            p1.TTFPoints.Add(new Point(1390.898, -872.5742));
            p1.TTFPoints.Add(new Point(1366.219, -976.4219));
            p1.TTFPoints.Add(new Point(1325.086, -1067.168));
            p1.TTFPoints.Add(new Point(1267.5, -1144.813));
            p1.TTFPoints.Add(new Point(1197.422, -1207.02));
            p1.TTFPoints.Add(new Point(1118.813, -1251.453));
            p1.TTFPoints.Add(new Point(1031.672, -1278.113));
            p1.TTFPoints.Add(new Point(936, -1287));
            p1.TTFPoints.Add(new Point(840.6328, -1278.113));
            p1.TTFPoints.Add(new Point(752.7813, -1251.453));
            p1.TTFPoints.Add(new Point(672.4453, -1207.02));
            p1.TTFPoints.Add(new Point(599.625, -1144.813));
            p1.TTFPoints.Add(new Point(539.1953, -1068.184));
            p1.TTFPoints.Add(new Point(496.0313, -980.4844));
            p1.TTFPoints.Add(new Point(470.1328, -881.7148));
            p1.TTFPoints.Add(new Point(461.5, -771.875));
            p1.TTFPoints.Add(new Point(461.5, -771.875));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('6', c);

            #endregion Character 6
        }

        private void InsertNumber7()
        {
            #region Character 7

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1542;
            c.BlackBoxY = 2351;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(158, -2351);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(157.625, -2070.25));
            p0.TTFPoints.Add(new Point(157.625, -2351.375));
            p0.TTFPoints.Add(new Point(1699.75, -2351.375));
            p0.TTFPoints.Add(new Point(1699.75, -2123.875));
            p0.TTFPoints.Add(new Point(1586.254, -1992.859));
            p0.TTFPoints.Add(new Point(1473.266, -1841.938));
            p0.TTFPoints.Add(new Point(1360.785, -1671.109));
            p0.TTFPoints.Add(new Point(1248.813, -1480.375));
            p0.TTFPoints.Add(new Point(1143.441, -1278.266));
            p0.TTFPoints.Add(new Point(1050.766, -1073.313));
            p0.TTFPoints.Add(new Point(970.7852, -865.5156));
            p0.TTFPoints.Add(new Point(903.5, -654.875));
            p0.TTFPoints.Add(new Point(863.5859, -501.8203));
            p0.TTFPoints.Add(new Point(831.5938, -341.6563));
            p0.TTFPoints.Add(new Point(807.5234, -174.3828));
            p0.TTFPoints.Add(new Point(791.375, 0));
            p0.TTFPoints.Add(new Point(490.75, 0));
            p0.TTFPoints.Add(new Point(499.4844, -147.875));
            p0.TTFPoints.Add(new Point(520.8125, -310.375));
            p0.TTFPoints.Add(new Point(554.7344, -487.5));
            p0.TTFPoints.Add(new Point(601.25, -679.25));
            p0.TTFPoints.Add(new Point(659.8008, -876.5352));
            p0.TTFPoints.Add(new Point(729.8281, -1070.266));
            p0.TTFPoints.Add(new Point(811.332, -1260.441));
            p0.TTFPoints.Add(new Point(904.3125, -1447.063));
            p0.TTFPoints.Add(new Point(1004.605, -1624.645));
            p0.TTFPoints.Add(new Point(1108.047, -1787.703));
            p0.TTFPoints.Add(new Point(1214.637, -1936.238));
            p0.TTFPoints.Add(new Point(1324.375, -2070.25));
            p0.TTFPoints.Add(new Point(157.625, -2070.25));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('7', c);

            #endregion Character 7
        }

        private void InsertNumber8()
        {
            #region Character 8

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1570;
            c.BlackBoxY = 2433;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(135, -2392);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(588.25, -1291.875));
            p0.TTFPoints.Add(new Point(503.1406, -1328.742));
            p0.TTFPoints.Add(new Point(429.8125, -1372.719));
            p0.TTFPoints.Add(new Point(368.2656, -1423.805));
            p0.TTFPoints.Add(new Point(318.5, -1482));
            p0.TTFPoints.Add(new Point(280.1094, -1546.797));
            p0.TTFPoints.Add(new Point(252.6875, -1617.688));
            p0.TTFPoints.Add(new Point(236.2344, -1694.672));
            p0.TTFPoints.Add(new Point(230.75, -1777.75));
            p0.TTFPoints.Add(new Point(242.4297, -1902.57));
            p0.TTFPoints.Add(new Point(277.4688, -2017.031));
            p0.TTFPoints.Add(new Point(335.8672, -2121.133));
            p0.TTFPoints.Add(new Point(417.625, -2214.875));
            p0.TTFPoints.Add(new Point(518.7813, -2292.367));
            p0.TTFPoints.Add(new Point(635.375, -2347.719));
            p0.TTFPoints.Add(new Point(767.4063, -2380.93));
            p0.TTFPoints.Add(new Point(914.875, -2392));
            p0.TTFPoints.Add(new Point(1063.258, -2380.676));
            p0.TTFPoints.Add(new Point(1196.406, -2346.703));
            p0.TTFPoints.Add(new Point(1314.32, -2290.082));
            p0.TTFPoints.Add(new Point(1417, -2210.813));
            p0.TTFPoints.Add(new Point(1500.18, -2115.293));
            p0.TTFPoints.Add(new Point(1559.594, -2009.922));
            p0.TTFPoints.Add(new Point(1595.242, -1894.699));
            p0.TTFPoints.Add(new Point(1607.125, -1769.625));
            p0.TTFPoints.Add(new Point(1601.691, -1689.441));
            p0.TTFPoints.Add(new Point(1585.391, -1614.641));
            p0.TTFPoints.Add(new Point(1558.223, -1545.223));
            p0.TTFPoints.Add(new Point(1520.188, -1481.188));
            p0.TTFPoints.Add(new Point(1471.082, -1423.348));
            p0.TTFPoints.Add(new Point(1410.703, -1372.516));
            p0.TTFPoints.Add(new Point(1339.051, -1328.691));
            p0.TTFPoints.Add(new Point(1256.125, -1291.875));
            p0.TTFPoints.Add(new Point(1359.262, -1250.641));
            p0.TTFPoints.Add(new Point(1449.297, -1198.438));
            p0.TTFPoints.Add(new Point(1526.23, -1135.266));
            p0.TTFPoints.Add(new Point(1590.063, -1061.125));
            p0.TTFPoints.Add(new Point(1640.184, -977.6406));
            p0.TTFPoints.Add(new Point(1675.984, -886.4375));
            p0.TTFPoints.Add(new Point(1697.465, -787.5156));
            p0.TTFPoints.Add(new Point(1704.625, -680.875));
            p0.TTFPoints.Add(new Point(1691.117, -534.2188));
            p0.TTFPoints.Add(new Point(1650.594, -399.75));
            p0.TTFPoints.Add(new Point(1583.055, -277.4688));
            p0.TTFPoints.Add(new Point(1488.5, -167.375));
            p0.TTFPoints.Add(new Point(1371.906, -76.375));
            p0.TTFPoints.Add(new Point(1238.25, -11.375));
            p0.TTFPoints.Add(new Point(1087.531, 27.625));
            p0.TTFPoints.Add(new Point(919.75, 40.625));
            p0.TTFPoints.Add(new Point(751.9688, 27.57422));
            p0.TTFPoints.Add(new Point(601.25, -11.57813));
            p0.TTFPoints.Add(new Point(467.5938, -76.83203));
            p0.TTFPoints.Add(new Point(351, -168.1875));
            p0.TTFPoints.Add(new Point(256.4453, -279.043));
            p0.TTFPoints.Add(new Point(188.9063, -402.7969));
            p0.TTFPoints.Add(new Point(148.3828, -539.4492));
            p0.TTFPoints.Add(new Point(134.875, -689));
            p0.TTFPoints.Add(new Point(142.2383, -800.4648));
            p0.TTFPoints.Add(new Point(164.3281, -902.4844));
            p0.TTFPoints.Add(new Point(201.1445, -995.0586));
            p0.TTFPoints.Add(new Point(252.6875, -1078.188));
            p0.TTFPoints.Add(new Point(317.8398, -1150.348));
            p0.TTFPoints.Add(new Point(395.4844, -1210.016));
            p0.TTFPoints.Add(new Point(485.6211, -1257.191));
            p0.TTFPoints.Add(new Point(588.25, -1291.875));
            p0.TTFPoints.Add(new Point(588.25, -1291.875));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(529.75, -1787.5));
            p1.TTFPoints.Add(new Point(536.5547, -1706.859));
            p1.TTFPoints.Add(new Point(556.9688, -1633.938));
            p1.TTFPoints.Add(new Point(590.9922, -1568.734));
            p1.TTFPoints.Add(new Point(638.625, -1511.25));
            p1.TTFPoints.Add(new Point(697.125, -1464.328));
            p1.TTFPoints.Add(new Point(763.75, -1430.813));
            p1.TTFPoints.Add(new Point(838.5, -1410.703));
            p1.TTFPoints.Add(new Point(921.375, -1404));
            p1.TTFPoints.Add(new Point(1002.066, -1410.652));
            p1.TTFPoints.Add(new Point(1075.141, -1430.609));
            p1.TTFPoints.Add(new Point(1140.598, -1463.871));
            p1.TTFPoints.Add(new Point(1198.438, -1510.438));
            p1.TTFPoints.Add(new Point(1245.715, -1566.652));
            p1.TTFPoints.Add(new Point(1279.484, -1628.859));
            p1.TTFPoints.Add(new Point(1299.746, -1697.059));
            p1.TTFPoints.Add(new Point(1306.5, -1771.25));
            p1.TTFPoints.Add(new Point(1299.543, -1848.488));
            p1.TTFPoints.Add(new Point(1278.672, -1919.328));
            p1.TTFPoints.Add(new Point(1243.887, -1983.77));
            p1.TTFPoints.Add(new Point(1195.188, -2041.813));
            p1.TTFPoints.Add(new Point(1136.129, -2089.801));
            p1.TTFPoints.Add(new Point(1070.266, -2124.078));
            p1.TTFPoints.Add(new Point(997.5977, -2144.645));
            p1.TTFPoints.Add(new Point(918.125, -2151.5));
            p1.TTFPoints.Add(new Point(837.9922, -2144.797));
            p1.TTFPoints.Add(new Point(764.9688, -2124.688));
            p1.TTFPoints.Add(new Point(699.0547, -2091.172));
            p1.TTFPoints.Add(new Point(640.25, -2044.25));
            p1.TTFPoints.Add(new Point(591.9063, -1987.984));
            p1.TTFPoints.Add(new Point(557.375, -1926.438));
            p1.TTFPoints.Add(new Point(536.6563, -1859.609));
            p1.TTFPoints.Add(new Point(529.75, -1787.5));
            p1.TTFPoints.Add(new Point(529.75, -1787.5));
            c.OoXmlPolygons.Add(p1);
            OoXmlPolygon p2 = new OoXmlPolygon();
            p2.TTFPoints.Add(new Point(435.5, -687.375));
            p2.TTFPoints.Add(new Point(439.207, -625.3203));
            p2.TTFPoints.Add(new Point(450.3281, -564.2813));
            p2.TTFPoints.Add(new Point(468.8633, -504.2578));
            p2.TTFPoints.Add(new Point(494.8125, -445.25));
            p2.TTFPoints.Add(new Point(528.0742, -390.0508));
            p2.TTFPoints.Add(new Point(568.5469, -341.4531));
            p2.TTFPoints.Add(new Point(616.2305, -299.457));
            p2.TTFPoints.Add(new Point(671.125, -264.0625));
            p2.TTFPoints.Add(new Point(730.7422, -235.9805));
            p2.TTFPoints.Add(new Point(792.5938, -215.9219));
            p2.TTFPoints.Add(new Point(856.6797, -203.8867));
            p2.TTFPoints.Add(new Point(923, -199.875));
            p2.TTFPoints.Add(new Point(1023.242, -208.3047));
            p2.TTFPoints.Add(new Point(1114.344, -233.5938));
            p2.TTFPoints.Add(new Point(1196.305, -275.7422));
            p2.TTFPoints.Add(new Point(1269.125, -334.75));
            p2.TTFPoints.Add(new Point(1328.844, -406.7578));
            p2.TTFPoints.Add(new Point(1371.5, -487.9063));
            p2.TTFPoints.Add(new Point(1397.094, -578.1953));
            p2.TTFPoints.Add(new Point(1405.625, -677.625));
            p2.TTFPoints.Add(new Point(1396.84, -778.6797));
            p2.TTFPoints.Add(new Point(1370.484, -870.5938));
            p2.TTFPoints.Add(new Point(1326.559, -953.3672));
            p2.TTFPoints.Add(new Point(1265.063, -1027));
            p2.TTFPoints.Add(new Point(1190.363, -1087.43));
            p2.TTFPoints.Add(new Point(1106.828, -1130.594));
            p2.TTFPoints.Add(new Point(1014.457, -1156.492));
            p2.TTFPoints.Add(new Point(913.25, -1165.125));
            p2.TTFPoints.Add(new Point(814.4805, -1156.594));
            p2.TTFPoints.Add(new Point(724.5469, -1131));
            p2.TTFPoints.Add(new Point(643.4492, -1088.344));
            p2.TTFPoints.Add(new Point(571.1875, -1028.625));
            p2.TTFPoints.Add(new Point(511.8242, -956.1094));
            p2.TTFPoints.Add(new Point(469.4219, -875.0625));
            p2.TTFPoints.Add(new Point(443.9805, -785.4844));
            p2.TTFPoints.Add(new Point(435.5, -687.375));
            p2.TTFPoints.Add(new Point(435.5, -687.375));
            c.OoXmlPolygons.Add(p2);
            m_OoXmlCharacters.Add('8', c);

            #endregion Character 8
        }

        private void InsertNumber9()
        {
            #region Character 9

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1567;
            c.BlackBoxY = 2433;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(138, -2392);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(182, -550.875));
            p0.TTFPoints.Add(new Point(463.125, -576.875));
            p0.TTFPoints.Add(new Point(485.0625, -484.5547));
            p0.TTFPoints.Add(new Point(515.125, -405.8438));
            p0.TTFPoints.Add(new Point(553.3125, -340.7422));
            p0.TTFPoints.Add(new Point(599.625, -289.25));
            p0.TTFPoints.Add(new Point(653.5547, -250.1484));
            p0.TTFPoints.Add(new Point(714.5938, -222.2188));
            p0.TTFPoints.Add(new Point(782.7422, -205.4609));
            p0.TTFPoints.Add(new Point(858, -199.875));
            p0.TTFPoints.Add(new Point(923.3555, -203.7344));
            p0.TTFPoints.Add(new Point(984.5469, -215.3125));
            p0.TTFPoints.Add(new Point(1041.574, -234.6094));
            p0.TTFPoints.Add(new Point(1094.438, -261.625));
            p0.TTFPoints.Add(new Point(1142.934, -295.0898));
            p0.TTFPoints.Add(new Point(1186.859, -333.7344));
            p0.TTFPoints.Add(new Point(1226.215, -377.5586));
            p0.TTFPoints.Add(new Point(1261, -426.5625));
            p0.TTFPoints.Add(new Point(1292.18, -482.6758));
            p0.TTFPoints.Add(new Point(1320.719, -547.8281));
            p0.TTFPoints.Add(new Point(1346.617, -622.0195));
            p0.TTFPoints.Add(new Point(1369.875, -705.25));
            p0.TTFPoints.Add(new Point(1389.07, -793.4063));
            p0.TTFPoints.Add(new Point(1402.781, -882.375));
            p0.TTFPoints.Add(new Point(1411.008, -972.1563));
            p0.TTFPoints.Add(new Point(1413.75, -1062.75));
            p0.TTFPoints.Add(new Point(1413.648, -1073.719));
            p0.TTFPoints.Add(new Point(1413.344, -1087.125));
            p0.TTFPoints.Add(new Point(1412.836, -1102.969));
            p0.TTFPoints.Add(new Point(1412.125, -1121.25));
            p0.TTFPoints.Add(new Point(1364.238, -1054.676));
            p0.TTFPoints.Add(new Point(1308.328, -994.7031));
            p0.TTFPoints.Add(new Point(1244.395, -941.332));
            p0.TTFPoints.Add(new Point(1172.438, -894.5625));
            p0.TTFPoints.Add(new Point(1094.895, -856.5273));
            p0.TTFPoints.Add(new Point(1014.203, -829.3594));
            p0.TTFPoints.Add(new Point(930.3633, -813.0586));
            p0.TTFPoints.Add(new Point(843.375, -807.625));
            p0.TTFPoints.Add(new Point(701.1875, -821.0313));
            p0.TTFPoints.Add(new Point(570.375, -861.25));
            p0.TTFPoints.Add(new Point(450.9375, -928.2813));
            p0.TTFPoints.Add(new Point(342.875, -1022.125));
            p0.TTFPoints.Add(new Point(253.2969, -1137.906));
            p0.TTFPoints.Add(new Point(189.3125, -1270.75));
            p0.TTFPoints.Add(new Point(150.9219, -1420.656));
            p0.TTFPoints.Add(new Point(138.125, -1587.625));
            p0.TTFPoints.Add(new Point(151.4805, -1759.977));
            p0.TTFPoints.Add(new Point(191.5469, -1914.656));
            p0.TTFPoints.Add(new Point(258.3242, -2051.664));
            p0.TTFPoints.Add(new Point(351.8125, -2171));
            p0.TTFPoints.Add(new Point(465.4102, -2267.688));
            p0.TTFPoints.Add(new Point(592.5156, -2336.75));
            p0.TTFPoints.Add(new Point(733.1289, -2378.188));
            p0.TTFPoints.Add(new Point(887.25, -2392));
            p0.TTFPoints.Add(new Point(1000.949, -2384.18));
            p0.TTFPoints.Add(new Point(1109.672, -2360.719));
            p0.TTFPoints.Add(new Point(1213.418, -2321.617));
            p0.TTFPoints.Add(new Point(1312.188, -2266.875));
            p0.TTFPoints.Add(new Point(1402.68, -2197.66));
            p0.TTFPoints.Add(new Point(1481.594, -2115.141));
            p0.TTFPoints.Add(new Point(1548.93, -2019.316));
            p0.TTFPoints.Add(new Point(1604.688, -1910.188));
            p0.TTFPoints.Add(new Point(1648.41, -1781.457));
            p0.TTFPoints.Add(new Point(1679.641, -1626.828));
            p0.TTFPoints.Add(new Point(1698.379, -1446.301));
            p0.TTFPoints.Add(new Point(1704.625, -1239.875));
            p0.TTFPoints.Add(new Point(1698.43, -1023.191));
            p0.TTFPoints.Add(new Point(1679.844, -829.7656));
            p0.TTFPoints.Add(new Point(1648.867, -659.5977));
            p0.TTFPoints.Add(new Point(1605.5, -512.6875));
            p0.TTFPoints.Add(new Point(1549.895, -385.4805));
            p0.TTFPoints.Add(new Point(1482.203, -274.4219));
            p0.TTFPoints.Add(new Point(1402.426, -179.5117));
            p0.TTFPoints.Add(new Point(1310.563, -100.75));
            p0.TTFPoints.Add(new Point(1208.441, -38.89844));
            p0.TTFPoints.Add(new Point(1097.891, 5.28125));
            p0.TTFPoints.Add(new Point(978.9102, 31.78906));
            p0.TTFPoints.Add(new Point(851.5, 40.625));
            p0.TTFPoints.Add(new Point(718.1484, 30.92578));
            p0.TTFPoints.Add(new Point(597.5938, 1.828125));
            p0.TTFPoints.Add(new Point(489.8359, -46.66797));
            p0.TTFPoints.Add(new Point(394.875, -114.5625));
            p0.TTFPoints.Add(new Point(315.1484, -200.0273));
            p0.TTFPoints.Add(new Point(253.0938, -301.2344));
            p0.TTFPoints.Add(new Point(208.7109, -418.1836));
            p0.TTFPoints.Add(new Point(182, -550.875));
            p0.TTFPoints.Add(new Point(182, -550.875));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(1379.625, -1602.25));
            p1.TTFPoints.Add(new Point(1371.246, -1721.688));
            p1.TTFPoints.Add(new Point(1346.109, -1828.125));
            p1.TTFPoints.Add(new Point(1304.215, -1921.563));
            p1.TTFPoints.Add(new Point(1245.563, -2002));
            p1.TTFPoints.Add(new Point(1175.129, -2066.695));
            p1.TTFPoints.Add(new Point(1097.891, -2112.906));
            p1.TTFPoints.Add(new Point(1013.848, -2140.633));
            p1.TTFPoints.Add(new Point(923, -2149.875));
            p1.TTFPoints.Add(new Point(828.6484, -2139.922));
            p1.TTFPoints.Add(new Point(740.5938, -2110.063));
            p1.TTFPoints.Add(new Point(658.8359, -2060.297));
            p1.TTFPoints.Add(new Point(583.375, -1990.625));
            p1.TTFPoints.Add(new Point(520.1016, -1905.109));
            p1.TTFPoints.Add(new Point(474.9063, -1807.813));
            p1.TTFPoints.Add(new Point(447.7891, -1698.734));
            p1.TTFPoints.Add(new Point(438.75, -1577.875));
            p1.TTFPoints.Add(new Point(447.332, -1469.457));
            p1.TTFPoints.Add(new Point(473.0781, -1371.703));
            p1.TTFPoints.Add(new Point(515.9883, -1284.613));
            p1.TTFPoints.Add(new Point(576.0625, -1208.188));
            p1.TTFPoints.Add(new Point(648.7305, -1145.98));
            p1.TTFPoints.Add(new Point(729.4219, -1101.547));
            p1.TTFPoints.Add(new Point(818.1367, -1074.887));
            p1.TTFPoints.Add(new Point(914.875, -1066));
            p1.TTFPoints.Add(new Point(1011.918, -1074.887));
            p1.TTFPoints.Add(new Point(1099.922, -1101.547));
            p1.TTFPoints.Add(new Point(1178.887, -1145.98));
            p1.TTFPoints.Add(new Point(1248.813, -1208.188));
            p1.TTFPoints.Add(new Point(1306.043, -1286.137));
            p1.TTFPoints.Add(new Point(1346.922, -1377.797));
            p1.TTFPoints.Add(new Point(1371.449, -1483.168));
            p1.TTFPoints.Add(new Point(1379.625, -1602.25));
            p1.TTFPoints.Add(new Point(1379.625, -1602.25));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('9', c);

            #endregion Character 9
        }

        private void InsertSymbolPlus()
        {
            #region Character +

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1573;
            c.BlackBoxY = 1575;
            c.CellIncX = 1944;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(185, -1960);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(833.625, -385.125));
            p0.TTFPoints.Add(new Point(833.625, -1038.375));
            p0.TTFPoints.Add(new Point(185.25, -1038.375));
            p0.TTFPoints.Add(new Point(185.25, -1311.375));
            p0.TTFPoints.Add(new Point(833.625, -1311.375));
            p0.TTFPoints.Add(new Point(833.625, -1959.75));
            p0.TTFPoints.Add(new Point(1109.875, -1959.75));
            p0.TTFPoints.Add(new Point(1109.875, -1311.375));
            p0.TTFPoints.Add(new Point(1758.25, -1311.375));
            p0.TTFPoints.Add(new Point(1758.25, -1038.375));
            p0.TTFPoints.Add(new Point(1109.875, -1038.375));
            p0.TTFPoints.Add(new Point(1109.875, -385.125));
            p0.TTFPoints.Add(new Point(833.625, -385.125));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('+', c);

            #endregion Character +
        }

        private void InsertSymbolMinus()
        {
            #region Character -

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 898;
            c.BlackBoxY = 294;
            c.CellIncX = 1108;
            c.CellIncY = 0;
            c.OffsetY = 900;
            c.GlyphOrigin = new Point(106, -1009);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(105.625, -715));
            p0.TTFPoints.Add(new Point(105.625, -1009.125));
            p0.TTFPoints.Add(new Point(1004.25, -1009.125));
            p0.TTFPoints.Add(new Point(1004.25, -715));
            p0.TTFPoints.Add(new Point(105.625, -715));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('-', c);

            #endregion Character -
        }

        private void InsertSymbolDot()
        {
            #region Character .

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 333;
            c.BlackBoxY = 333;
            c.CellIncX = 925;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(302, -333);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(302.25, 0));
            p0.TTFPoints.Add(new Point(302.25, -333.125));
            p0.TTFPoints.Add(new Point(635.375, -333.125));
            p0.TTFPoints.Add(new Point(635.375, 0));
            p0.TTFPoints.Add(new Point(302.25, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('.', c);

            #endregion Character .
        }

        #endregion Numbers and Symbols

        #region Lower Case

        private void InsertLowerCaseA()
        {
            #region Character a

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1590;
            c.BlackBoxY = 1804;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.OffsetY = 600;
            c.GlyphOrigin = new Point(120, -1765);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(1345.5, -212.875));
            p0.TTFPoints.Add(new Point(1265.012, -148.8906));
            p0.TTFPoints.Add(new Point(1186.047, -95.0625));
            p0.TTFPoints.Add(new Point(1108.605, -51.39063));
            p0.TTFPoints.Add(new Point(1032.688, -17.875));
            p0.TTFPoints.Add(new Point(956.1602, 7.007813));
            p0.TTFPoints.Add(new Point(876.8906, 24.78125));
            p0.TTFPoints.Add(new Point(794.8789, 35.44531));
            p0.TTFPoints.Add(new Point(710.125, 39));
            p0.TTFPoints.Add(new Point(576.1641, 30.31641));
            p0.TTFPoints.Add(new Point(458.6563, 4.265625));
            p0.TTFPoints.Add(new Point(357.6016, -39.15234));
            p0.TTFPoints.Add(new Point(273, -99.9375));
            p0.TTFPoints.Add(new Point(206.1719, -174.2305));
            p0.TTFPoints.Add(new Point(158.4375, -258.1719));
            p0.TTFPoints.Add(new Point(129.7969, -351.7617));
            p0.TTFPoints.Add(new Point(120.25, -455));
            p0.TTFPoints.Add(new Point(123.8555, -517.0039));
            p0.TTFPoints.Add(new Point(134.6719, -576.2656));
            p0.TTFPoints.Add(new Point(152.6992, -632.7852));
            p0.TTFPoints.Add(new Point(177.9375, -686.5625));
            p0.TTFPoints.Add(new Point(209.0156, -736.3789));
            p0.TTFPoints.Add(new Point(244.5625, -781.0156));
            p0.TTFPoints.Add(new Point(284.5781, -820.4727));
            p0.TTFPoints.Add(new Point(329.0625, -854.75));
            p0.TTFPoints.Add(new Point(377.2539, -884.5078));
            p0.TTFPoints.Add(new Point(428.3906, -910.4063));
            p0.TTFPoints.Add(new Point(482.4727, -932.4453));
            p0.TTFPoints.Add(new Point(539.5, -950.625));
            p0.TTFPoints.Add(new Point(588.0469, -961.8984));
            p0.TTFPoints.Add(new Point(647.5625, -972.9688));
            p0.TTFPoints.Add(new Point(718.0469, -983.8359));
            p0.TTFPoints.Add(new Point(799.5, -994.5));
            p0.TTFPoints.Add(new Point(964.9453, -1016.641));
            p0.TTFPoints.Add(new Point(1107.031, -1040.813));
            p0.TTFPoints.Add(new Point(1225.758, -1067.016));
            p0.TTFPoints.Add(new Point(1321.125, -1095.25));
            p0.TTFPoints.Add(new Point(1321.836, -1122.57));
            p0.TTFPoints.Add(new Point(1322.344, -1144.406));
            p0.TTFPoints.Add(new Point(1322.648, -1160.758));
            p0.TTFPoints.Add(new Point(1322.75, -1171.625));
            p0.TTFPoints.Add(new Point(1317.57, -1254.398));
            p0.TTFPoints.Add(new Point(1302.031, -1323.969));
            p0.TTFPoints.Add(new Point(1276.133, -1380.336));
            p0.TTFPoints.Add(new Point(1239.875, -1423.5));
            p0.TTFPoints.Add(new Point(1177.008, -1466.867));
            p0.TTFPoints.Add(new Point(1100.531, -1497.844));
            p0.TTFPoints.Add(new Point(1010.445, -1516.43));
            p0.TTFPoints.Add(new Point(906.75, -1522.625));
            p0.TTFPoints.Add(new Point(810.3164, -1518.105));
            p0.TTFPoints.Add(new Point(727.3906, -1504.547));
            p0.TTFPoints.Add(new Point(657.9727, -1481.949));
            p0.TTFPoints.Add(new Point(602.0625, -1450.313));
            p0.TTFPoints.Add(new Point(556.1055, -1407.199));
            p0.TTFPoints.Add(new Point(516.5469, -1350.172));
            p0.TTFPoints.Add(new Point(483.3867, -1279.23));
            p0.TTFPoints.Add(new Point(456.625, -1194.375));
            p0.TTFPoints.Add(new Point(170.625, -1233.375));
            p0.TTFPoints.Add(new Point(193.2734, -1320.77));
            p0.TTFPoints.Add(new Point(222.2188, -1399.328));
            p0.TTFPoints.Add(new Point(257.4609, -1469.051));
            p0.TTFPoints.Add(new Point(299, -1529.938));
            p0.TTFPoints.Add(new Point(348.6641, -1583.156));
            p0.TTFPoints.Add(new Point(408.2813, -1629.875));
            p0.TTFPoints.Add(new Point(477.8516, -1670.094));
            p0.TTFPoints.Add(new Point(557.375, -1703.813));
            p0.TTFPoints.Add(new Point(645.2266, -1730.473));
            p0.TTFPoints.Add(new Point(739.7813, -1749.516));
            p0.TTFPoints.Add(new Point(841.0391, -1760.941));
            p0.TTFPoints.Add(new Point(949, -1764.75));
            p0.TTFPoints.Add(new Point(1054.32, -1761.5));
            p0.TTFPoints.Add(new Point(1149.281, -1751.75));
            p0.TTFPoints.Add(new Point(1233.883, -1735.5));
            p0.TTFPoints.Add(new Point(1308.125, -1712.75));
            p0.TTFPoints.Add(new Point(1372.617, -1685.074));
            p0.TTFPoints.Add(new Point(1427.969, -1654.047));
            p0.TTFPoints.Add(new Point(1474.18, -1619.668));
            p0.TTFPoints.Add(new Point(1511.25, -1581.938));
            p0.TTFPoints.Add(new Point(1541.313, -1539.941));
            p0.TTFPoints.Add(new Point(1566.5, -1492.766));
            p0.TTFPoints.Add(new Point(1586.813, -1440.41));
            p0.TTFPoints.Add(new Point(1602.25, -1382.875));
            p0.TTFPoints.Add(new Point(1608.648, -1337.984));
            p0.TTFPoints.Add(new Point(1613.219, -1278.063));
            p0.TTFPoints.Add(new Point(1615.961, -1203.109));
            p0.TTFPoints.Add(new Point(1616.875, -1113.125));
            p0.TTFPoints.Add(new Point(1616.875, -723.125));
            p0.TTFPoints.Add(new Point(1618.043, -537.9258));
            p0.TTFPoints.Add(new Point(1621.547, -390.2031));
            p0.TTFPoints.Add(new Point(1627.387, -279.957));
            p0.TTFPoints.Add(new Point(1635.563, -207.1875));
            p0.TTFPoints.Add(new Point(1647.191, -153.7148));
            p0.TTFPoints.Add(new Point(1663.391, -101.3594));
            p0.TTFPoints.Add(new Point(1684.16, -50.12109));
            p0.TTFPoints.Add(new Point(1709.5, 0));
            p0.TTFPoints.Add(new Point(1404, 0));
            p0.TTFPoints.Add(new Point(1383.281, -47.42969));
            p0.TTFPoints.Add(new Point(1366.625, -98.71875));
            p0.TTFPoints.Add(new Point(1354.031, -153.8672));
            p0.TTFPoints.Add(new Point(1345.5, -212.875));
            p0.TTFPoints.Add(new Point(1345.5, -212.875));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(1321.125, -866.125));
            p1.TTFPoints.Add(new Point(1231.547, -834.8438));
            p1.TTFPoints.Add(new Point(1122.063, -806));
            p1.TTFPoints.Add(new Point(992.6719, -779.5938));
            p1.TTFPoints.Add(new Point(843.375, -755.625));
            p1.TTFPoints.Add(new Point(759.7891, -742.2188));
            p1.TTFPoints.Add(new Point(689.4063, -728));
            p1.TTFPoints.Add(new Point(632.2266, -712.9688));
            p1.TTFPoints.Add(new Point(588.25, -697.125));
            p1.TTFPoints.Add(new Point(553.0078, -678.9961));
            p1.TTFPoints.Add(new Point(522.0313, -657.1094));
            p1.TTFPoints.Add(new Point(495.3203, -631.4648));
            p1.TTFPoints.Add(new Point(472.875, -602.0625));
            p1.TTFPoints.Add(new Point(455.1016, -569.918));
            p1.TTFPoints.Add(new Point(442.4063, -536.0469));
            p1.TTFPoints.Add(new Point(434.7891, -500.4492));
            p1.TTFPoints.Add(new Point(432.25, -463.125));
            p1.TTFPoints.Add(new Point(437.7852, -407.0625));
            p1.TTFPoints.Add(new Point(454.3906, -355.875));
            p1.TTFPoints.Add(new Point(482.0664, -309.5625));
            p1.TTFPoints.Add(new Point(520.8125, -268.125));
            p1.TTFPoints.Add(new Point(570.2227, -234));
            p1.TTFPoints.Add(new Point(629.8906, -209.625));
            p1.TTFPoints.Add(new Point(699.8164, -195));
            p1.TTFPoints.Add(new Point(780, -190.125));
            p1.TTFPoints.Add(new Point(862.1641, -194.7461));
            p1.TTFPoints.Add(new Point(939.6563, -208.6094));
            p1.TTFPoints.Add(new Point(1012.477, -231.7148));
            p1.TTFPoints.Add(new Point(1080.625, -264.0625));
            p1.TTFPoints.Add(new Point(1142.07, -304.4336));
            p1.TTFPoints.Add(new Point(1194.781, -351.6094));
            p1.TTFPoints.Add(new Point(1238.758, -405.5898));
            p1.TTFPoints.Add(new Point(1274, -466.375));
            p1.TTFPoints.Add(new Point(1294.617, -521.8281));
            p1.TTFPoints.Add(new Point(1309.344, -589.0625));
            p1.TTFPoints.Add(new Point(1318.18, -668.0781));
            p1.TTFPoints.Add(new Point(1321.125, -758.875));
            p1.TTFPoints.Add(new Point(1321.125, -866.125));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('a', c);

            #endregion Character a
        }

        private void InsertLowerCaseB()
        {
            #region Character b

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1496;
            c.BlackBoxY = 2421;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(218, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(489.125, 0));
            p0.TTFPoints.Add(new Point(217.75, 0));
            p0.TTFPoints.Add(new Point(217.75, -2382.25));
            p0.TTFPoints.Add(new Point(510.25, -2382.25));
            p0.TTFPoints.Add(new Point(510.25, -1532.375));
            p0.TTFPoints.Add(new Point(609.2734, -1634.039));
            p0.TTFPoints.Add(new Point(721.0938, -1706.656));
            p0.TTFPoints.Add(new Point(845.7109, -1750.227));
            p0.TTFPoints.Add(new Point(983.125, -1764.75));
            p0.TTFPoints.Add(new Point(1061.684, -1760.738));
            p0.TTFPoints.Add(new Point(1138.109, -1748.703));
            p0.TTFPoints.Add(new Point(1212.402, -1728.645));
            p0.TTFPoints.Add(new Point(1284.563, -1700.563));
            p0.TTFPoints.Add(new Point(1352.508, -1665.219));
            p0.TTFPoints.Add(new Point(1414.156, -1623.375));
            p0.TTFPoints.Add(new Point(1469.508, -1575.031));
            p0.TTFPoints.Add(new Point(1518.563, -1520.188));
            p0.TTFPoints.Add(new Point(1561.98, -1459.098));
            p0.TTFPoints.Add(new Point(1600.422, -1392.016));
            p0.TTFPoints.Add(new Point(1633.887, -1318.941));
            p0.TTFPoints.Add(new Point(1662.375, -1239.875));
            p0.TTFPoints.Add(new Point(1685.125, -1156.391));
            p0.TTFPoints.Add(new Point(1701.375, -1070.063));
            p0.TTFPoints.Add(new Point(1711.125, -980.8906));
            p0.TTFPoints.Add(new Point(1714.375, -888.875));
            p0.TTFPoints.Add(new Point(1700.664, -679.6563));
            p0.TTFPoints.Add(new Point(1659.531, -495.625));
            p0.TTFPoints.Add(new Point(1590.977, -336.7813));
            p0.TTFPoints.Add(new Point(1495, -203.125));
            p0.TTFPoints.Add(new Point(1379.828, -97.19531));
            p0.TTFPoints.Add(new Point(1253.688, -21.53125));
            p0.TTFPoints.Add(new Point(1116.578, 23.86719));
            p0.TTFPoints.Add(new Point(968.5, 39));
            p0.TTFPoints.Add(new Point(823.9766, 23.05469));
            p0.TTFPoints.Add(new Point(695.9063, -24.78125));
            p0.TTFPoints.Add(new Point(584.2891, -104.5078));
            p0.TTFPoints.Add(new Point(489.125, -216.125));
            p0.TTFPoints.Add(new Point(489.125, 0));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(485.875, -875.875));
            p1.TTFPoints.Add(new Point(491.1563, -731.4531));
            p1.TTFPoints.Add(new Point(507, -608.5625));
            p1.TTFPoints.Add(new Point(533.4063, -507.2031));
            p1.TTFPoints.Add(new Point(570.375, -427.375));
            p1.TTFPoints.Add(new Point(645.5313, -328.5547));
            p1.TTFPoints.Add(new Point(732.875, -257.9688));
            p1.TTFPoints.Add(new Point(832.4063, -215.6172));
            p1.TTFPoints.Add(new Point(944.125, -201.5));
            p1.TTFPoints.Add(new Point(1036.75, -211.9102));
            p1.TTFPoints.Add(new Point(1122.875, -243.1406));
            p1.TTFPoints.Add(new Point(1202.5, -295.1914));
            p1.TTFPoints.Add(new Point(1275.625, -368.0625));
            p1.TTFPoints.Add(new Point(1336.766, -461.5508));
            p1.TTFPoints.Add(new Point(1380.438, -575.4531));
            p1.TTFPoints.Add(new Point(1406.641, -709.7695));
            p1.TTFPoints.Add(new Point(1415.375, -864.5));
            p1.TTFPoints.Add(new Point(1406.996, -1022.43));
            p1.TTFPoints.Add(new Point(1381.859, -1158.219));
            p1.TTFPoints.Add(new Point(1339.965, -1271.867));
            p1.TTFPoints.Add(new Point(1281.313, -1363.375));
            p1.TTFPoints.Add(new Point(1210.777, -1433.758));
            p1.TTFPoints.Add(new Point(1133.234, -1484.031));
            p1.TTFPoints.Add(new Point(1048.684, -1514.195));
            p1.TTFPoints.Add(new Point(957.125, -1524.25));
            p1.TTFPoints.Add(new Point(864.5, -1513.84));
            p1.TTFPoints.Add(new Point(778.375, -1482.609));
            p1.TTFPoints.Add(new Point(698.75, -1430.559));
            p1.TTFPoints.Add(new Point(625.625, -1357.688));
            p1.TTFPoints.Add(new Point(564.4844, -1265.113));
            p1.TTFPoints.Add(new Point(520.8125, -1153.953));
            p1.TTFPoints.Add(new Point(494.6094, -1024.207));
            p1.TTFPoints.Add(new Point(485.875, -875.875));
            p1.TTFPoints.Add(new Point(485.875, -875.875));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('b', c);

            #endregion Character b
        }

        private void InsertLowerCaseC()
        {
            #region Character c

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1503;
            c.BlackBoxY = 1804;
            c.CellIncX = 1664;
            c.CellIncY = 0;
            c.OffsetY = 600;
            c.GlyphOrigin = new Point(130, -1765);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(1345.5, -632.125));
            p0.TTFPoints.Add(new Point(1633.125, -594.75));
            p0.TTFPoints.Add(new Point(1600.371, -454.1367));
            p0.TTFPoints.Add(new Point(1549.234, -329.6719));
            p0.TTFPoints.Add(new Point(1479.715, -221.3555));
            p0.TTFPoints.Add(new Point(1391.813, -129.1875));
            p0.TTFPoints.Add(new Point(1289.184, -55.60547));
            p0.TTFPoints.Add(new Point(1175.484, -3.046875));
            p0.TTFPoints.Add(new Point(1050.715, 28.48828));
            p0.TTFPoints.Add(new Point(914.875, 39));
            p0.TTFPoints.Add(new Point(746.4336, 24.52734));
            p0.TTFPoints.Add(new Point(595.3594, -18.89063));
            p0.TTFPoints.Add(new Point(461.6523, -91.25391));
            p0.TTFPoints.Add(new Point(345.3125, -192.5625));
            p0.TTFPoints.Add(new Point(251.1133, -320.8867));
            p0.TTFPoints.Add(new Point(183.8281, -474.2969));
            p0.TTFPoints.Add(new Point(143.457, -652.793));
            p0.TTFPoints.Add(new Point(130, -856.375));
            p0.TTFPoints.Add(new Point(135.7891, -991.7578));
            p0.TTFPoints.Add(new Point(153.1563, -1118.406));
            p0.TTFPoints.Add(new Point(182.1016, -1236.32));
            p0.TTFPoints.Add(new Point(222.625, -1345.5));
            p0.TTFPoints.Add(new Point(274.9805, -1443.762));
            p0.TTFPoints.Add(new Point(339.4219, -1528.922));
            p0.TTFPoints.Add(new Point(415.9492, -1600.98));
            p0.TTFPoints.Add(new Point(504.5625, -1659.938));
            p0.TTFPoints.Add(new Point(601.3008, -1705.793));
            p0.TTFPoints.Add(new Point(702.2031, -1738.547));
            p0.TTFPoints.Add(new Point(807.2695, -1758.199));
            p0.TTFPoints.Add(new Point(916.5, -1764.75));
            p0.TTFPoints.Add(new Point(1050.664, -1755.863));
            p0.TTFPoints.Add(new Point(1172.031, -1729.203));
            p0.TTFPoints.Add(new Point(1280.602, -1684.77));
            p0.TTFPoints.Add(new Point(1376.375, -1622.563));
            p0.TTFPoints.Add(new Point(1457.727, -1544.004));
            p0.TTFPoints.Add(new Point(1523.031, -1450.516));
            p0.TTFPoints.Add(new Point(1572.289, -1342.098));
            p0.TTFPoints.Add(new Point(1605.5, -1218.75));
            p0.TTFPoints.Add(new Point(1321.125, -1174.875));
            p0.TTFPoints.Add(new Point(1296.902, -1256.43));
            p0.TTFPoints.Add(new Point(1264.859, -1327.219));
            p0.TTFPoints.Add(new Point(1224.996, -1387.242));
            p0.TTFPoints.Add(new Point(1177.313, -1436.5));
            p0.TTFPoints.Add(new Point(1123.027, -1474.891));
            p0.TTFPoints.Add(new Point(1063.359, -1502.313));
            p0.TTFPoints.Add(new Point(998.3086, -1518.766));
            p0.TTFPoints.Add(new Point(927.875, -1524.25));
            p0.TTFPoints.Add(new Point(822.5547, -1514.348));
            p0.TTFPoints.Add(new Point(727.5938, -1484.641));
            p0.TTFPoints.Add(new Point(642.9922, -1435.129));
            p0.TTFPoints.Add(new Point(568.75, -1365.813));
            p0.TTFPoints.Add(new Point(508.3203, -1275.066));
            p0.TTFPoints.Add(new Point(465.1563, -1161.266));
            p0.TTFPoints.Add(new Point(439.2578, -1024.41));
            p0.TTFPoints.Add(new Point(430.625, -864.5));
            p0.TTFPoints.Add(new Point(438.9531, -702.5078));
            p0.TTFPoints.Add(new Point(463.9375, -564.2813));
            p0.TTFPoints.Add(new Point(505.5781, -449.8203));
            p0.TTFPoints.Add(new Point(563.875, -359.125));
            p0.TTFPoints.Add(new Point(635.5781, -290.1641));
            p0.TTFPoints.Add(new Point(717.4375, -240.9063));
            p0.TTFPoints.Add(new Point(809.4531, -211.3516));
            p0.TTFPoints.Add(new Point(911.625, -201.5));
            p0.TTFPoints.Add(new Point(994.1953, -208.1016));
            p0.TTFPoints.Add(new Point(1069.656, -227.9063));
            p0.TTFPoints.Add(new Point(1138.008, -260.9141));
            p0.TTFPoints.Add(new Point(1199.25, -307.125));
            p0.TTFPoints.Add(new Point(1251.656, -367.0469));
            p0.TTFPoints.Add(new Point(1293.5, -441.1875));
            p0.TTFPoints.Add(new Point(1324.781, -529.5469));
            p0.TTFPoints.Add(new Point(1345.5, -632.125));
            p0.TTFPoints.Add(new Point(1345.5, -632.125));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('c', c);

            #endregion Character c
        }

        private void InsertLowerCaseD()
        {
            #region Character d

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1496;
            c.BlackBoxY = 2421;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(114, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(1339, 0));
            p0.TTFPoints.Add(new Point(1339, -217.75));
            p0.TTFPoints.Add(new Point(1247.289, -105.4219));
            p0.TTFPoints.Add(new Point(1136.281, -25.1875));
            p0.TTFPoints.Add(new Point(1005.977, 22.95313));
            p0.TTFPoints.Add(new Point(856.375, 39));
            p0.TTFPoints.Add(new Point(755.2695, 31.89063));
            p0.TTFPoints.Add(new Point(658.3281, 10.5625));
            p0.TTFPoints.Add(new Point(565.5508, -24.98438));
            p0.TTFPoints.Add(new Point(476.9375, -74.75));
            p0.TTFPoints.Add(new Point(395.2813, -137.2617));
            p0.TTFPoints.Add(new Point(323.375, -211.0469));
            p0.TTFPoints.Add(new Point(261.2188, -296.1055));
            p0.TTFPoints.Add(new Point(208.8125, -392.4375));
            p0.TTFPoints.Add(new Point(167.2227, -498.2148));
            p0.TTFPoints.Add(new Point(137.5156, -611.6094));
            p0.TTFPoints.Add(new Point(119.6914, -732.6211));
            p0.TTFPoints.Add(new Point(113.75, -861.25));
            p0.TTFPoints.Add(new Point(119.1328, -987.4414));
            p0.TTFPoints.Add(new Point(135.2813, -1107.641));
            p0.TTFPoints.Add(new Point(162.1953, -1221.848));
            p0.TTFPoints.Add(new Point(199.875, -1330.063));
            p0.TTFPoints.Add(new Point(248.3203, -1429.137));
            p0.TTFPoints.Add(new Point(307.5313, -1515.922));
            p0.TTFPoints.Add(new Point(377.5078, -1590.418));
            p0.TTFPoints.Add(new Point(458.25, -1652.625));
            p0.TTFPoints.Add(new Point(546.9141, -1701.68));
            p0.TTFPoints.Add(new Point(640.6563, -1736.719));
            p0.TTFPoints.Add(new Point(739.4766, -1757.742));
            p0.TTFPoints.Add(new Point(843.375, -1764.75));
            p0.TTFPoints.Add(new Point(919.2422, -1760.637));
            p0.TTFPoints.Add(new Point(990.8438, -1748.297));
            p0.TTFPoints.Add(new Point(1058.18, -1727.73));
            p0.TTFPoints.Add(new Point(1121.25, -1698.938));
            p0.TTFPoints.Add(new Point(1179.344, -1663.543));
            p0.TTFPoints.Add(new Point(1231.75, -1623.172));
            p0.TTFPoints.Add(new Point(1278.469, -1577.824));
            p0.TTFPoints.Add(new Point(1319.5, -1527.5));
            p0.TTFPoints.Add(new Point(1319.5, -2382.25));
            p0.TTFPoints.Add(new Point(1610.375, -2382.25));
            p0.TTFPoints.Add(new Point(1610.375, 0));
            p0.TTFPoints.Add(new Point(1339, 0));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(414.375, -861.25));
            p1.TTFPoints.Add(new Point(423.1094, -705.9609));
            p1.TTFPoints.Add(new Point(449.3125, -571.5938));
            p1.TTFPoints.Add(new Point(492.9844, -458.1484));
            p1.TTFPoints.Add(new Point(554.125, -365.625));
            p1.TTFPoints.Add(new Point(627.1484, -293.8203));
            p1.TTFPoints.Add(new Point(706.4688, -242.5313));
            p1.TTFPoints.Add(new Point(792.0859, -211.7578));
            p1.TTFPoints.Add(new Point(884, -201.5));
            p1.TTFPoints.Add(new Point(976.2695, -211.3008));
            p1.TTFPoints.Add(new Point(1061.328, -240.7031));
            p1.TTFPoints.Add(new Point(1139.176, -289.707));
            p1.TTFPoints.Add(new Point(1209.813, -358.3125));
            p1.TTFPoints.Add(new Point(1268.465, -447.0273));
            p1.TTFPoints.Add(new Point(1310.359, -556.3594));
            p1.TTFPoints.Add(new Point(1335.496, -686.3086));
            p1.TTFPoints.Add(new Point(1343.875, -836.875));
            p1.TTFPoints.Add(new Point(1335.344, -1002.219));
            p1.TTFPoints.Add(new Point(1309.75, -1144));
            p1.TTFPoints.Add(new Point(1267.094, -1262.219));
            p1.TTFPoints.Add(new Point(1207.375, -1356.875));
            p1.TTFPoints.Add(new Point(1135.164, -1429.391));
            p1.TTFPoints.Add(new Point(1055.031, -1481.188));
            p1.TTFPoints.Add(new Point(966.9766, -1512.266));
            p1.TTFPoints.Add(new Point(871, -1522.625));
            p1.TTFPoints.Add(new Point(777.5117, -1512.672));
            p1.TTFPoints.Add(new Point(692.0469, -1482.813));
            p1.TTFPoints.Add(new Point(614.6055, -1433.047));
            p1.TTFPoints.Add(new Point(545.1875, -1363.375));
            p1.TTFPoints.Add(new Point(487.957, -1272.273));
            p1.TTFPoints.Add(new Point(447.0781, -1158.219));
            p1.TTFPoints.Add(new Point(422.5508, -1021.211));
            p1.TTFPoints.Add(new Point(414.375, -861.25));
            p1.TTFPoints.Add(new Point(414.375, -861.25));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('d', c);

            #endregion Character d
        }

        private void InsertLowerCaseE()
        {
            #region Character e

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1591;
            c.BlackBoxY = 1804;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.OffsetY = 600;
            c.GlyphOrigin = new Point(122, -1765);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(1400.75, -555.75));
            p0.TTFPoints.Add(new Point(1703, -518.375));
            p0.TTFPoints.Add(new Point(1659.633, -393.3516));
            p0.TTFPoints.Add(new Point(1601.031, -283.1563));
            p0.TTFPoints.Add(new Point(1527.195, -187.7891));
            p0.TTFPoints.Add(new Point(1438.125, -107.25));
            p0.TTFPoints.Add(new Point(1334.734, -43.26563));
            p0.TTFPoints.Add(new Point(1217.938, 2.4375));
            p0.TTFPoints.Add(new Point(1087.734, 29.85938));
            p0.TTFPoints.Add(new Point(944.125, 39));
            p0.TTFPoints.Add(new Point(764.6133, 24.42578));
            p0.TTFPoints.Add(new Point(604.7031, -19.29688));
            p0.TTFPoints.Add(new Point(464.3945, -92.16797));
            p0.TTFPoints.Add(new Point(343.6875, -194.1875));
            p0.TTFPoints.Add(new Point(246.6445, -322.5117));
            p0.TTFPoints.Add(new Point(177.3281, -474.2969));
            p0.TTFPoints.Add(new Point(135.7383, -649.543));
            p0.TTFPoints.Add(new Point(121.875, -848.25));
            p0.TTFPoints.Add(new Point(135.8906, -1053.813));
            p0.TTFPoints.Add(new Point(177.9375, -1235));
            p0.TTFPoints.Add(new Point(248.0156, -1391.813));
            p0.TTFPoints.Add(new Point(346.125, -1524.25));
            p0.TTFPoints.Add(new Point(466.5781, -1629.469));
            p0.TTFPoints.Add(new Point(603.6875, -1704.625));
            p0.TTFPoints.Add(new Point(757.4531, -1749.719));
            p0.TTFPoints.Add(new Point(927.875, -1764.75));
            p0.TTFPoints.Add(new Point(1093.016, -1750.023));
            p0.TTFPoints.Add(new Point(1242.313, -1705.844));
            p0.TTFPoints.Add(new Point(1375.766, -1632.211));
            p0.TTFPoints.Add(new Point(1493.375, -1529.125));
            p0.TTFPoints.Add(new Point(1589.352, -1399.328));
            p0.TTFPoints.Add(new Point(1657.906, -1245.563));
            p0.TTFPoints.Add(new Point(1699.039, -1067.828));
            p0.TTFPoints.Add(new Point(1712.75, -866.125));
            p0.TTFPoints.Add(new Point(1712.648, -851.5));
            p0.TTFPoints.Add(new Point(1712.344, -833.625));
            p0.TTFPoints.Add(new Point(1711.836, -812.5));
            p0.TTFPoints.Add(new Point(1711.125, -788.125));
            p0.TTFPoints.Add(new Point(424.125, -788.125));
            p0.TTFPoints.Add(new Point(440.2734, -654.2656));
            p0.TTFPoints.Add(new Point(472.4688, -537.0625));
            p0.TTFPoints.Add(new Point(520.7109, -436.5156));
            p0.TTFPoints.Add(new Point(585, -352.625));
            p0.TTFPoints.Add(new Point(661.7813, -286.5078));
            p0.TTFPoints.Add(new Point(747.5, -239.2813));
            p0.TTFPoints.Add(new Point(842.1563, -210.9453));
            p0.TTFPoints.Add(new Point(945.75, -201.5));
            p0.TTFPoints.Add(new Point(1023.242, -206.7813));
            p0.TTFPoints.Add(new Point(1094.844, -222.625));
            p0.TTFPoints.Add(new Point(1160.555, -249.0313));
            p0.TTFPoints.Add(new Point(1220.375, -286));
            p0.TTFPoints.Add(new Point(1274.305, -334.5469));
            p0.TTFPoints.Add(new Point(1322.344, -395.6875));
            p0.TTFPoints.Add(new Point(1364.492, -469.4219));
            p0.TTFPoints.Add(new Point(1400.75, -555.75));
            p0.TTFPoints.Add(new Point(1400.75, -555.75));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(440.375, -1028.625));
            p1.TTFPoints.Add(new Point(1404, -1028.625));
            p1.TTFPoints.Add(new Point(1389.781, -1130.695));
            p1.TTFPoints.Add(new Point(1366.625, -1219.156));
            p1.TTFPoints.Add(new Point(1334.531, -1294.008));
            p1.TTFPoints.Add(new Point(1293.5, -1355.25));
            p1.TTFPoints.Add(new Point(1218.445, -1429.188));
            p1.TTFPoints.Add(new Point(1133.031, -1482));
            p1.TTFPoints.Add(new Point(1037.258, -1513.688));
            p1.TTFPoints.Add(new Point(931.125, -1524.25));
            p1.TTFPoints.Add(new Point(834.3867, -1515.82));
            p1.TTFPoints.Add(new Point(745.6719, -1490.531));
            p1.TTFPoints.Add(new Point(664.9805, -1448.383));
            p1.TTFPoints.Add(new Point(592.3125, -1389.375));
            p1.TTFPoints.Add(new Point(531.3242, -1316.25));
            p1.TTFPoints.Add(new Point(485.6719, -1231.75));
            p1.TTFPoints.Add(new Point(455.3555, -1135.875));
            p1.TTFPoints.Add(new Point(440.375, -1028.625));
            p1.TTFPoints.Add(new Point(440.375, -1028.625));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('e', c);

            #endregion Character e
        }

        private void InsertLowerCaseF()
        {
            #region Character f

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1009;
            c.BlackBoxY = 2423;
            c.CellIncX = 925;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(31, -2423);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(289.25, 0));
            p0.TTFPoints.Add(new Point(289.25, -1498.25));
            p0.TTFPoints.Add(new Point(30.875, -1498.25));
            p0.TTFPoints.Add(new Point(30.875, -1725.75));
            p0.TTFPoints.Add(new Point(289.25, -1725.75));
            p0.TTFPoints.Add(new Point(289.25, -1909.375));
            p0.TTFPoints.Add(new Point(291.1797, -1990.727));
            p0.TTFPoints.Add(new Point(296.9688, -2060.906));
            p0.TTFPoints.Add(new Point(306.6172, -2119.914));
            p0.TTFPoints.Add(new Point(320.125, -2167.75));
            p0.TTFPoints.Add(new Point(345.2617, -2221.934));
            p0.TTFPoints.Add(new Point(378.4219, -2270.734));
            p0.TTFPoints.Add(new Point(419.6055, -2314.152));
            p0.TTFPoints.Add(new Point(468.8125, -2352.188));
            p0.TTFPoints.Add(new Point(527.3633, -2383.113));
            p0.TTFPoints.Add(new Point(596.5781, -2405.203));
            p0.TTFPoints.Add(new Point(676.457, -2418.457));
            p0.TTFPoints.Add(new Point(767, -2422.875));
            p0.TTFPoints.Add(new Point(830.375, -2421.047));
            p0.TTFPoints.Add(new Point(897, -2415.563));
            p0.TTFPoints.Add(new Point(966.875, -2406.422));
            p0.TTFPoints.Add(new Point(1040, -2393.625));
            p0.TTFPoints.Add(new Point(996.125, -2138.5));
            p0.TTFPoints.Add(new Point(951.2344, -2145.609));
            p0.TTFPoints.Add(new Point(907.5625, -2150.688));
            p0.TTFPoints.Add(new Point(865.1094, -2153.734));
            p0.TTFPoints.Add(new Point(823.875, -2154.75));
            p0.TTFPoints.Add(new Point(762.125, -2151.195));
            p0.TTFPoints.Add(new Point(710.125, -2140.531));
            p0.TTFPoints.Add(new Point(667.875, -2122.758));
            p0.TTFPoints.Add(new Point(635.375, -2097.875));
            p0.TTFPoints.Add(new Point(611.2031, -2063.242));
            p0.TTFPoints.Add(new Point(593.9375, -2016.219));
            p0.TTFPoints.Add(new Point(583.5781, -1956.805));
            p0.TTFPoints.Add(new Point(580.125, -1885));
            p0.TTFPoints.Add(new Point(580.125, -1725.75));
            p0.TTFPoints.Add(new Point(916.5, -1725.75));
            p0.TTFPoints.Add(new Point(916.5, -1498.25));
            p0.TTFPoints.Add(new Point(580.125, -1498.25));
            p0.TTFPoints.Add(new Point(580.125, 0));
            p0.TTFPoints.Add(new Point(289.25, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('f', c);

            #endregion Character f
        }

        private void InsertLowerCaseG()
        {
            #region Character g

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1521;
            c.BlackBoxY = 2465;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.OffsetY = -50;
            c.GlyphOrigin = new Point(107, -1765);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(165.75, 143));
            p0.TTFPoints.Add(new Point(450.125, 185.25));
            p0.TTFPoints.Add(new Point(463.0234, 246.5938));
            p0.TTFPoints.Add(new Point(483.8438, 299));
            p0.TTFPoints.Add(new Point(512.5859, 342.4688));
            p0.TTFPoints.Add(new Point(549.25, 377));
            p0.TTFPoints.Add(new Point(608.6641, 412.5469));
            p0.TTFPoints.Add(new Point(678.0313, 437.9375));
            p0.TTFPoints.Add(new Point(757.3516, 453.1719));
            p0.TTFPoints.Add(new Point(846.625, 458.25));
            p0.TTFPoints.Add(new Point(942.3984, 453.1719));
            p0.TTFPoints.Add(new Point(1026.594, 437.9375));
            p0.TTFPoints.Add(new Point(1099.211, 412.5469));
            p0.TTFPoints.Add(new Point(1160.25, 377));
            p0.TTFPoints.Add(new Point(1211.031, 332.3125));
            p0.TTFPoints.Add(new Point(1252.875, 279.5));
            p0.TTFPoints.Add(new Point(1285.781, 218.5625));
            p0.TTFPoints.Add(new Point(1309.75, 149.5));
            p0.TTFPoints.Add(new Point(1319.602, 92.52344));
            p0.TTFPoints.Add(new Point(1326.406, 10.96875));
            p0.TTFPoints.Add(new Point(1330.164, -95.16406));
            p0.TTFPoints.Add(new Point(1330.875, -225.875));
            p0.TTFPoints.Add(new Point(1229.109, -127.0547));
            p0.TTFPoints.Add(new Point(1115.563, -56.46875));
            p0.TTFPoints.Add(new Point(990.2344, -14.11719));
            p0.TTFPoints.Add(new Point(853.125, 0));
            p0.TTFPoints.Add(new Point(685.2422, -16.04688));
            p0.TTFPoints.Add(new Point(537.4688, -64.1875));
            p0.TTFPoints.Add(new Point(409.8047, -144.4219));
            p0.TTFPoints.Add(new Point(302.25, -256.75));
            p0.TTFPoints.Add(new Point(216.9375, -391.5234));
            p0.TTFPoints.Add(new Point(156, -539.0938));
            p0.TTFPoints.Add(new Point(119.4375, -699.4609));
            p0.TTFPoints.Add(new Point(107.25, -872.625));
            p0.TTFPoints.Add(new Point(112.8359, -993.7383));
            p0.TTFPoints.Add(new Point(129.5938, -1110.078));
            p0.TTFPoints.Add(new Point(157.5234, -1221.645));
            p0.TTFPoints.Add(new Point(196.625, -1328.438));
            p0.TTFPoints.Add(new Point(246.3398, -1426.902));
            p0.TTFPoints.Add(new Point(306.1094, -1513.484));
            p0.TTFPoints.Add(new Point(375.9336, -1588.184));
            p0.TTFPoints.Add(new Point(455.8125, -1651));
            p0.TTFPoints.Add(new Point(544.4258, -1700.766));
            p0.TTFPoints.Add(new Point(640.4531, -1736.313));
            p0.TTFPoints.Add(new Point(743.8945, -1757.641));
            p0.TTFPoints.Add(new Point(854.75, -1764.75));
            p0.TTFPoints.Add(new Point(1000.797, -1749.313));
            p0.TTFPoints.Add(new Point(1133.438, -1703));
            p0.TTFPoints.Add(new Point(1252.672, -1625.813));
            p0.TTFPoints.Add(new Point(1358.5, -1517.75));
            p0.TTFPoints.Add(new Point(1358.5, -1725.75));
            p0.TTFPoints.Add(new Point(1628.25, -1725.75));
            p0.TTFPoints.Add(new Point(1628.25, -234));
            p0.TTFPoints.Add(new Point(1623.121, -47.17578));
            p0.TTFPoints.Add(new Point(1607.734, 110.2969));
            p0.TTFPoints.Add(new Point(1582.09, 238.418));
            p0.TTFPoints.Add(new Point(1546.188, 337.1875));
            p0.TTFPoints.Add(new Point(1499.164, 416.8633));
            p0.TTFPoints.Add(new Point(1440.156, 487.7031));
            p0.TTFPoints.Add(new Point(1369.164, 549.707));
            p0.TTFPoints.Add(new Point(1286.188, 602.875));
            p0.TTFPoints.Add(new Point(1192.09, 645.5313));
            p0.TTFPoints.Add(new Point(1087.734, 676));
            p0.TTFPoints.Add(new Point(973.1211, 694.2813));
            p0.TTFPoints.Add(new Point(848.25, 700.375));
            p0.TTFPoints.Add(new Point(701.2891, 691.6914));
            p0.TTFPoints.Add(new Point(569.1563, 665.6406));
            p0.TTFPoints.Add(new Point(451.8516, 622.2227));
            p0.TTFPoints.Add(new Point(349.375, 561.4375));
            p0.TTFPoints.Add(new Point(266.6016, 483.1836));
            p0.TTFPoints.Add(new Point(208.4063, 387.3594));
            p0.TTFPoints.Add(new Point(174.7891, 273.9648));
            p0.TTFPoints.Add(new Point(165.75, 143));
            p0.TTFPoints.Add(new Point(165.75, 143));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(407.875, -893.75));
            p1.TTFPoints.Add(new Point(416.3047, -735.4141));
            p1.TTFPoints.Add(new Point(441.5938, -600.0313));
            p1.TTFPoints.Add(new Point(483.7422, -487.6016));
            p1.TTFPoints.Add(new Point(542.75, -398.125));
            p1.TTFPoints.Add(new Point(614.4531, -329.875));
            p1.TTFPoints.Add(new Point(694.6875, -281.125));
            p1.TTFPoints.Add(new Point(783.4531, -251.875));
            p1.TTFPoints.Add(new Point(880.75, -242.125));
            p1.TTFPoints.Add(new Point(977.4375, -251.8242));
            p1.TTFPoints.Add(new Point(1066, -280.9219));
            p1.TTFPoints.Add(new Point(1146.438, -329.418));
            p1.TTFPoints.Add(new Point(1218.75, -397.3125));
            p1.TTFPoints.Add(new Point(1278.469, -485.9258));
            p1.TTFPoints.Add(new Point(1321.125, -596.5781));
            p1.TTFPoints.Add(new Point(1346.719, -729.2695));
            p1.TTFPoints.Add(new Point(1355.25, -884));
            p1.TTFPoints.Add(new Point(1346.465, -1032.688));
            p1.TTFPoints.Add(new Point(1320.109, -1161.875));
            p1.TTFPoints.Add(new Point(1276.184, -1271.563));
            p1.TTFPoints.Add(new Point(1214.688, -1361.75));
            p1.TTFPoints.Add(new Point(1140.801, -1432.133));
            p1.TTFPoints.Add(new Point(1059.703, -1482.406));
            p1.TTFPoints.Add(new Point(971.3945, -1512.57));
            p1.TTFPoints.Add(new Point(875.875, -1522.625));
            p1.TTFPoints.Add(new Point(782.0313, -1512.723));
            p1.TTFPoints.Add(new Point(695.5, -1483.016));
            p1.TTFPoints.Add(new Point(616.2813, -1433.504));
            p1.TTFPoints.Add(new Point(544.375, -1364.188));
            p1.TTFPoints.Add(new Point(484.6563, -1275.371));
            p1.TTFPoints.Add(new Point(442, -1167.359));
            p1.TTFPoints.Add(new Point(416.4063, -1040.152));
            p1.TTFPoints.Add(new Point(407.875, -893.75));
            p1.TTFPoints.Add(new Point(407.875, -893.75));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('g', c);

            #endregion Character g
        }

        private void InsertLowerCaseH()
        {
            #region Character h

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1406;
            c.BlackBoxY = 2382;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(219, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(219.375, 0));
            p0.TTFPoints.Add(new Point(219.375, -2382.25));
            p0.TTFPoints.Add(new Point(511.875, -2382.25));
            p0.TTFPoints.Add(new Point(511.875, -1527.5));
            p0.TTFPoints.Add(new Point(620.9531, -1631.297));
            p0.TTFPoints.Add(new Point(743.4375, -1705.438));
            p0.TTFPoints.Add(new Point(879.3281, -1749.922));
            p0.TTFPoints.Add(new Point(1028.625, -1764.75));
            p0.TTFPoints.Add(new Point(1121.352, -1760.027));
            p0.TTFPoints.Add(new Point(1207.781, -1745.859));
            p0.TTFPoints.Add(new Point(1287.914, -1722.246));
            p0.TTFPoints.Add(new Point(1361.75, -1689.188));
            p0.TTFPoints.Add(new Point(1427.41, -1647.801));
            p0.TTFPoints.Add(new Point(1483.016, -1599.203));
            p0.TTFPoints.Add(new Point(1528.566, -1543.395));
            p0.TTFPoints.Add(new Point(1564.063, -1480.375));
            p0.TTFPoints.Add(new Point(1590.723, -1406.234));
            p0.TTFPoints.Add(new Point(1609.766, -1317.063));
            p0.TTFPoints.Add(new Point(1621.191, -1212.859));
            p0.TTFPoints.Add(new Point(1625, -1093.625));
            p0.TTFPoints.Add(new Point(1625, 0));
            p0.TTFPoints.Add(new Point(1332.5, 0));
            p0.TTFPoints.Add(new Point(1332.5, -1093.625));
            p0.TTFPoints.Add(new Point(1326.559, -1195.848));
            p0.TTFPoints.Add(new Point(1308.734, -1283.141));
            p0.TTFPoints.Add(new Point(1279.027, -1355.504));
            p0.TTFPoints.Add(new Point(1237.438, -1412.938));
            p0.TTFPoints.Add(new Point(1184.98, -1456.66));
            p0.TTFPoints.Add(new Point(1122.672, -1487.891));
            p0.TTFPoints.Add(new Point(1050.512, -1506.629));
            p0.TTFPoints.Add(new Point(968.5, -1512.875));
            p0.TTFPoints.Add(new Point(904.4648, -1508.66));
            p0.TTFPoints.Add(new Point(842.3594, -1496.016));
            p0.TTFPoints.Add(new Point(782.1836, -1474.941));
            p0.TTFPoints.Add(new Point(723.9375, -1445.438));
            p0.TTFPoints.Add(new Point(670.7695, -1408.723));
            p0.TTFPoints.Add(new Point(625.8281, -1366.016));
            p0.TTFPoints.Add(new Point(589.1133, -1317.316));
            p0.TTFPoints.Add(new Point(560.625, -1262.625));
            p0.TTFPoints.Add(new Point(539.2969, -1199.453));
            p0.TTFPoints.Add(new Point(524.0625, -1125.313));
            p0.TTFPoints.Add(new Point(514.9219, -1040.203));
            p0.TTFPoints.Add(new Point(511.875, -944.125));
            p0.TTFPoints.Add(new Point(511.875, 0));
            p0.TTFPoints.Add(new Point(219.375, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('h', c);

            #endregion Character h
        }

        private void InsertLowerCaseI()
        {
            #region Character i

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 293;
            c.BlackBoxY = 2382;
            c.CellIncX = 739;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(221, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(221, -2045.875));
            p0.TTFPoints.Add(new Point(221, -2382.25));
            p0.TTFPoints.Add(new Point(513.5, -2382.25));
            p0.TTFPoints.Add(new Point(513.5, -2045.875));
            p0.TTFPoints.Add(new Point(221, -2045.875));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(221, 0));
            p1.TTFPoints.Add(new Point(221, -1725.75));
            p1.TTFPoints.Add(new Point(513.5, -1725.75));
            p1.TTFPoints.Add(new Point(513.5, 0));
            p1.TTFPoints.Add(new Point(221, 0));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('i', c);

            #endregion Character i
        }

        private void InsertLowerCaseJ()
        {
            #region Character j

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 663;
            c.BlackBoxY = 3082;
            c.CellIncX = 739;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(-153, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(217.75, -2042.625));
            p0.TTFPoints.Add(new Point(217.75, -2382.25));
            p0.TTFPoints.Add(new Point(510.25, -2382.25));
            p0.TTFPoints.Add(new Point(510.25, -2042.625));
            p0.TTFPoints.Add(new Point(217.75, -2042.625));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(-152.75, 669.5));
            p1.TTFPoints.Add(new Point(-97.5, 420.875));
            p1.TTFPoints.Add(new Point(-55.96094, 430.8281));
            p1.TTFPoints.Add(new Point(-19.09375, 437.9375));
            p1.TTFPoints.Add(new Point(13.10156, 442.2031));
            p1.TTFPoints.Add(new Point(40.625, 443.625));
            p1.TTFPoints.Add(new Point(82.46875, 439.918));
            p1.TTFPoints.Add(new Point(118.625, 428.7969));
            p1.TTFPoints.Add(new Point(149.0938, 410.2617));
            p1.TTFPoints.Add(new Point(173.875, 384.3125));
            p1.TTFPoints.Add(new Point(193.0703, 343.5352));
            p1.TTFPoints.Add(new Point(206.7813, 280.5156));
            p1.TTFPoints.Add(new Point(215.0078, 195.2539));
            p1.TTFPoints.Add(new Point(217.75, 87.75));
            p1.TTFPoints.Add(new Point(217.75, -1725.75));
            p1.TTFPoints.Add(new Point(510.25, -1725.75));
            p1.TTFPoints.Add(new Point(510.25, 94.25));
            p1.TTFPoints.Add(new Point(505.0703, 241.4141));
            p1.TTFPoints.Add(new Point(489.5313, 364.4063));
            p1.TTFPoints.Add(new Point(463.6328, 463.2266));
            p1.TTFPoints.Add(new Point(427.375, 537.875));
            p1.TTFPoints.Add(new Point(365.8281, 608.9688));
            p1.TTFPoints.Add(new Point(286.8125, 659.75));
            p1.TTFPoints.Add(new Point(190.3281, 690.2188));
            p1.TTFPoints.Add(new Point(76.375, 700.375));
            p1.TTFPoints.Add(new Point(17.57031, 698.4453));
            p1.TTFPoints.Add(new Point(-40.21875, 692.6563));
            p1.TTFPoints.Add(new Point(-96.99219, 683.0078));
            p1.TTFPoints.Add(new Point(-152.75, 669.5));
            p1.TTFPoints.Add(new Point(-152.75, 669.5));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('j', c);

            #endregion Character j
        }

        private void InsertLowerCaseK()
        {
            #region Character k

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1430;
            c.BlackBoxY = 2382;
            c.CellIncX = 1664;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(221, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(221, 0));
            p0.TTFPoints.Add(new Point(221, -2382.25));
            p0.TTFPoints.Add(new Point(513.5, -2382.25));
            p0.TTFPoints.Add(new Point(513.5, -1023.75));
            p0.TTFPoints.Add(new Point(1205.75, -1725.75));
            p0.TTFPoints.Add(new Point(1584.375, -1725.75));
            p0.TTFPoints.Add(new Point(924.625, -1085.5));
            p0.TTFPoints.Add(new Point(1651, 0));
            p0.TTFPoints.Add(new Point(1290.25, 0));
            p0.TTFPoints.Add(new Point(719.875, -882.375));
            p0.TTFPoints.Add(new Point(513.5, -684.125));
            p0.TTFPoints.Add(new Point(513.5, 0));
            p0.TTFPoints.Add(new Point(221, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('k', c);

            #endregion Character k
        }

        private void InsertLowerCaseL()
        {
            #region Character l

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 292;
            c.BlackBoxY = 2382;
            c.CellIncX = 739;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(213, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(212.875, 0));
            p0.TTFPoints.Add(new Point(212.875, -2382.25));
            p0.TTFPoints.Add(new Point(505.375, -2382.25));
            p0.TTFPoints.Add(new Point(505.375, 0));
            p0.TTFPoints.Add(new Point(212.875, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('l', c);

            #endregion Character l
        }

        private void InsertLowerCaseM()
        {
            #region Character m

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 2339;
            c.BlackBoxY = 1765;
            c.CellIncX = 2772;
            c.CellIncY = 0;
            c.OffsetY = 600;
            c.GlyphOrigin = new Point(219, -1765);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(219.375, 0));
            p0.TTFPoints.Add(new Point(219.375, -1725.75));
            p0.TTFPoints.Add(new Point(481, -1725.75));
            p0.TTFPoints.Add(new Point(481, -1483.625));
            p0.TTFPoints.Add(new Point(524.9766, -1543.902));
            p0.TTFPoints.Add(new Point(575.6563, -1597.984));
            p0.TTFPoints.Add(new Point(633.0391, -1645.871));
            p0.TTFPoints.Add(new Point(697.125, -1687.563));
            p0.TTFPoints.Add(new Point(766.8984, -1721.332));
            p0.TTFPoints.Add(new Point(841.3438, -1745.453));
            p0.TTFPoints.Add(new Point(920.4609, -1759.926));
            p0.TTFPoints.Add(new Point(1004.25, -1764.75));
            p0.TTFPoints.Add(new Point(1095.809, -1759.773));
            p0.TTFPoints.Add(new Point(1178.734, -1744.844));
            p0.TTFPoints.Add(new Point(1253.027, -1719.961));
            p0.TTFPoints.Add(new Point(1318.688, -1685.125));
            p0.TTFPoints.Add(new Point(1375.512, -1641.352));
            p0.TTFPoints.Add(new Point(1423.297, -1589.656));
            p0.TTFPoints.Add(new Point(1462.043, -1530.039));
            p0.TTFPoints.Add(new Point(1491.75, -1462.5));
            p0.TTFPoints.Add(new Point(1601.844, -1594.734));
            p0.TTFPoints.Add(new Point(1727.375, -1689.188));
            p0.TTFPoints.Add(new Point(1868.344, -1745.859));
            p0.TTFPoints.Add(new Point(2024.75, -1764.75));
            p0.TTFPoints.Add(new Point(2145.711, -1755.863));
            p0.TTFPoints.Add(new Point(2251.844, -1729.203));
            p0.TTFPoints.Add(new Point(2343.148, -1684.77));
            p0.TTFPoints.Add(new Point(2419.625, -1622.563));
            p0.TTFPoints.Add(new Point(2480.055, -1541.871));
            p0.TTFPoints.Add(new Point(2523.219, -1441.984));
            p0.TTFPoints.Add(new Point(2549.117, -1322.902));
            p0.TTFPoints.Add(new Point(2557.75, -1184.625));
            p0.TTFPoints.Add(new Point(2557.75, 0));
            p0.TTFPoints.Add(new Point(2266.875, 0));
            p0.TTFPoints.Add(new Point(2266.875, -1087.125));
            p0.TTFPoints.Add(new Point(2265.098, -1168.73));
            p0.TTFPoints.Add(new Point(2259.766, -1238.047));
            p0.TTFPoints.Add(new Point(2250.879, -1295.074));
            p0.TTFPoints.Add(new Point(2238.438, -1339.813));
            p0.TTFPoints.Add(new Point(2221.324, -1376.527));
            p0.TTFPoints.Add(new Point(2198.422, -1409.484));
            p0.TTFPoints.Add(new Point(2169.73, -1438.684));
            p0.TTFPoints.Add(new Point(2135.25, -1464.125));
            p0.TTFPoints.Add(new Point(2096.25, -1484.742));
            p0.TTFPoints.Add(new Point(2054, -1499.469));
            p0.TTFPoints.Add(new Point(2008.5, -1508.305));
            p0.TTFPoints.Add(new Point(1959.75, -1511.25));
            p0.TTFPoints.Add(new Point(1872.609, -1503.684));
            p0.TTFPoints.Add(new Point(1793.188, -1480.984));
            p0.TTFPoints.Add(new Point(1721.484, -1443.152));
            p0.TTFPoints.Add(new Point(1657.5, -1390.188));
            p0.TTFPoints.Add(new Point(1604.891, -1320.566));
            p0.TTFPoints.Add(new Point(1567.313, -1232.766));
            p0.TTFPoints.Add(new Point(1544.766, -1126.785));
            p0.TTFPoints.Add(new Point(1537.25, -1002.625));
            p0.TTFPoints.Add(new Point(1537.25, 0));
            p0.TTFPoints.Add(new Point(1244.75, 0));
            p0.TTFPoints.Add(new Point(1244.75, -1121.25));
            p0.TTFPoints.Add(new Point(1240.281, -1212.656));
            p0.TTFPoints.Add(new Point(1226.875, -1291.875));
            p0.TTFPoints.Add(new Point(1204.531, -1358.906));
            p0.TTFPoints.Add(new Point(1173.25, -1413.75));
            p0.TTFPoints.Add(new Point(1131.813, -1456.406));
            p0.TTFPoints.Add(new Point(1079, -1486.875));
            p0.TTFPoints.Add(new Point(1014.813, -1505.156));
            p0.TTFPoints.Add(new Point(939.25, -1511.25));
            p0.TTFPoints.Add(new Point(878.668, -1507.188));
            p0.TTFPoints.Add(new Point(820.4219, -1495));
            p0.TTFPoints.Add(new Point(764.5117, -1474.688));
            p0.TTFPoints.Add(new Point(710.9375, -1446.25));
            p0.TTFPoints.Add(new Point(662.1367, -1409.992));
            p0.TTFPoints.Add(new Point(620.5469, -1366.219));
            p0.TTFPoints.Add(new Point(586.168, -1314.93));
            p0.TTFPoints.Add(new Point(559, -1256.125));
            p0.TTFPoints.Add(new Point(538.3828, -1186.656));
            p0.TTFPoints.Add(new Point(523.6563, -1103.375));
            p0.TTFPoints.Add(new Point(514.8203, -1006.281));
            p0.TTFPoints.Add(new Point(511.875, -895.375));
            p0.TTFPoints.Add(new Point(511.875, 0));
            p0.TTFPoints.Add(new Point(219.375, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('m', c);

            #endregion Character m
        }

        private void InsertLowerCaseN()
        {
            #region Character n

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1403;
            c.BlackBoxY = 1765;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.OffsetY = 600;
            c.GlyphOrigin = new Point(219, -1765);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(219.375, 0));
            p0.TTFPoints.Add(new Point(219.375, -1725.75));
            p0.TTFPoints.Add(new Point(482.625, -1725.75));
            p0.TTFPoints.Add(new Point(482.625, -1480.375));
            p0.TTFPoints.Add(new Point(588.25, -1604.789));
            p0.TTFPoints.Add(new Point(715, -1693.656));
            p0.TTFPoints.Add(new Point(862.875, -1746.977));
            p0.TTFPoints.Add(new Point(1031.875, -1764.75));
            p0.TTFPoints.Add(new Point(1108.301, -1761.246));
            p0.TTFPoints.Add(new Point(1181.578, -1750.734));
            p0.TTFPoints.Add(new Point(1251.707, -1733.215));
            p0.TTFPoints.Add(new Point(1318.688, -1708.688));
            p0.TTFPoints.Add(new Point(1379.98, -1678.473));
            p0.TTFPoints.Add(new Point(1433.047, -1643.891));
            p0.TTFPoints.Add(new Point(1477.887, -1604.941));
            p0.TTFPoints.Add(new Point(1514.5, -1561.625));
            p0.TTFPoints.Add(new Point(1544.563, -1513.992));
            p0.TTFPoints.Add(new Point(1569.75, -1462.094));
            p0.TTFPoints.Add(new Point(1590.063, -1405.93));
            p0.TTFPoints.Add(new Point(1605.5, -1345.5));
            p0.TTFPoints.Add(new Point(1612.609, -1297.258));
            p0.TTFPoints.Add(new Point(1617.688, -1233.781));
            p0.TTFPoints.Add(new Point(1620.734, -1155.07));
            p0.TTFPoints.Add(new Point(1621.75, -1061.125));
            p0.TTFPoints.Add(new Point(1621.75, 0));
            p0.TTFPoints.Add(new Point(1329.25, 0));
            p0.TTFPoints.Add(new Point(1329.25, -1049.75));
            p0.TTFPoints.Add(new Point(1327.117, -1133.488));
            p0.TTFPoints.Add(new Point(1320.719, -1205.953));
            p0.TTFPoints.Add(new Point(1310.055, -1267.145));
            p0.TTFPoints.Add(new Point(1295.125, -1317.063));
            p0.TTFPoints.Add(new Point(1274.762, -1359.109));
            p0.TTFPoints.Add(new Point(1247.797, -1396.688));
            p0.TTFPoints.Add(new Point(1214.23, -1429.797));
            p0.TTFPoints.Add(new Point(1174.063, -1458.438));
            p0.TTFPoints.Add(new Point(1128.715, -1481.543));
            p0.TTFPoints.Add(new Point(1079.609, -1498.047));
            p0.TTFPoints.Add(new Point(1026.746, -1507.949));
            p0.TTFPoints.Add(new Point(970.125, -1511.25));
            p0.TTFPoints.Add(new Point(879.8867, -1503.836));
            p0.TTFPoints.Add(new Point(796.0469, -1481.594));
            p0.TTFPoints.Add(new Point(718.6055, -1444.523));
            p0.TTFPoints.Add(new Point(647.5625, -1392.625));
            p0.TTFPoints.Add(new Point(588.1992, -1320.008));
            p0.TTFPoints.Add(new Point(545.7969, -1220.781));
            p0.TTFPoints.Add(new Point(520.3555, -1094.945));
            p0.TTFPoints.Add(new Point(511.875, -942.5));
            p0.TTFPoints.Add(new Point(511.875, 0));
            p0.TTFPoints.Add(new Point(219.375, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('n', c);

            #endregion Character n
        }

        private void InsertLowerCaseO()
        {
            #region Character o

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1617;
            c.BlackBoxY = 1804;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.OffsetY = 600;
            c.GlyphOrigin = new Point(110, -1765);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(110.5, -862.875));
            p0.TTFPoints.Add(new Point(127.1563, -1087.023));
            p0.TTFPoints.Add(new Point(177.125, -1280.094));
            p0.TTFPoints.Add(new Point(260.4063, -1442.086));
            p0.TTFPoints.Add(new Point(377, -1573));
            p0.TTFPoints.Add(new Point(494.4063, -1656.891));
            p0.TTFPoints.Add(new Point(624, -1716.813));
            p0.TTFPoints.Add(new Point(765.7813, -1752.766));
            p0.TTFPoints.Add(new Point(919.75, -1764.75));
            p0.TTFPoints.Add(new Point(1089.563, -1750.176));
            p0.TTFPoints.Add(new Point(1243.125, -1706.453));
            p0.TTFPoints.Add(new Point(1380.438, -1633.582));
            p0.TTFPoints.Add(new Point(1501.5, -1531.563));
            p0.TTFPoints.Add(new Point(1600.32, -1403.848));
            p0.TTFPoints.Add(new Point(1670.906, -1253.891));
            p0.TTFPoints.Add(new Point(1713.258, -1081.691));
            p0.TTFPoints.Add(new Point(1727.375, -887.25));
            p0.TTFPoints.Add(new Point(1721.129, -729.5742));
            p0.TTFPoints.Add(new Point(1702.391, -589.6719));
            p0.TTFPoints.Add(new Point(1671.16, -467.543));
            p0.TTFPoints.Add(new Point(1627.438, -363.1875));
            p0.TTFPoints.Add(new Point(1571.781, -273.0508));
            p0.TTFPoints.Add(new Point(1504.75, -193.5781));
            p0.TTFPoints.Add(new Point(1426.344, -124.7695));
            p0.TTFPoints.Add(new Point(1336.563, -66.625));
            p0.TTFPoints.Add(new Point(1238.91, -20.41406));
            p0.TTFPoints.Add(new Point(1136.891, 12.59375));
            p0.TTFPoints.Add(new Point(1030.504, 32.39844));
            p0.TTFPoints.Add(new Point(919.75, 39));
            p0.TTFPoints.Add(new Point(747.2461, 24.47656));
            p0.TTFPoints.Add(new Point(592.1094, -19.09375));
            p0.TTFPoints.Add(new Point(454.3398, -91.71094));
            p0.TTFPoints.Add(new Point(333.9375, -193.375));
            p0.TTFPoints.Add(new Point(236.1836, -322.3594));
            p0.TTFPoints.Add(new Point(166.3594, -476.9375));
            p0.TTFPoints.Add(new Point(124.4648, -657.1094));
            p0.TTFPoints.Add(new Point(110.5, -862.875));
            p0.TTFPoints.Add(new Point(110.5, -862.875));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(411.125, -862.875));
            p1.TTFPoints.Add(new Point(420.1641, -707.5352));
            p1.TTFPoints.Add(new Point(447.2813, -573.0156));
            p1.TTFPoints.Add(new Point(492.4766, -459.3164));
            p1.TTFPoints.Add(new Point(555.75, -366.4375));
            p1.TTFPoints.Add(new Point(632.7344, -294.2773));
            p1.TTFPoints.Add(new Point(719.0625, -242.7344));
            p1.TTFPoints.Add(new Point(814.7344, -211.8086));
            p1.TTFPoints.Add(new Point(919.75, -201.5));
            p1.TTFPoints.Add(new Point(1024.055, -211.8594));
            p1.TTFPoints.Add(new Point(1119.219, -242.9375));
            p1.TTFPoints.Add(new Point(1205.242, -294.7344));
            p1.TTFPoints.Add(new Point(1282.125, -367.25));
            p1.TTFPoints.Add(new Point(1345.398, -460.9922));
            p1.TTFPoints.Add(new Point(1390.594, -576.4688));
            p1.TTFPoints.Add(new Point(1417.711, -713.6797));
            p1.TTFPoints.Add(new Point(1426.75, -872.625));
            p1.TTFPoints.Add(new Point(1417.66, -1022.988));
            p1.TTFPoints.Add(new Point(1390.391, -1153.953));
            p1.TTFPoints.Add(new Point(1344.941, -1265.52));
            p1.TTFPoints.Add(new Point(1281.313, -1357.688));
            p1.TTFPoints.Add(new Point(1204.176, -1429.848));
            p1.TTFPoints.Add(new Point(1118.203, -1481.391));
            p1.TTFPoints.Add(new Point(1023.395, -1512.316));
            p1.TTFPoints.Add(new Point(919.75, -1522.625));
            p1.TTFPoints.Add(new Point(814.7344, -1512.367));
            p1.TTFPoints.Add(new Point(719.0625, -1481.594));
            p1.TTFPoints.Add(new Point(632.7344, -1430.305));
            p1.TTFPoints.Add(new Point(555.75, -1358.5));
            p1.TTFPoints.Add(new Point(492.4766, -1265.977));
            p1.TTFPoints.Add(new Point(447.2813, -1152.531));
            p1.TTFPoints.Add(new Point(420.1641, -1018.164));
            p1.TTFPoints.Add(new Point(411.125, -862.875));
            p1.TTFPoints.Add(new Point(411.125, -862.875));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('o', c);

            #endregion Character o
        }

        private void InsertLowerCaseP()
        {
            #region Character p

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1499;
            c.BlackBoxY = 2426;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(219, -1765);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(219.375, 661.375));
            p0.TTFPoints.Add(new Point(219.375, -1725.75));
            p0.TTFPoints.Add(new Point(485.875, -1725.75));
            p0.TTFPoints.Add(new Point(485.875, -1501.5));
            p0.TTFPoints.Add(new Point(534.5234, -1563.199));
            p0.TTFPoints.Add(new Point(586.2188, -1616.672));
            p0.TTFPoints.Add(new Point(640.9609, -1661.918));
            p0.TTFPoints.Add(new Point(698.75, -1698.938));
            p0.TTFPoints.Add(new Point(761.2109, -1727.73));
            p0.TTFPoints.Add(new Point(829.9688, -1748.297));
            p0.TTFPoints.Add(new Point(905.0234, -1760.637));
            p0.TTFPoints.Add(new Point(986.375, -1764.75));
            p0.TTFPoints.Add(new Point(1093.625, -1757.641));
            p0.TTFPoints.Add(new Point(1194.375, -1736.313));
            p0.TTFPoints.Add(new Point(1288.625, -1700.766));
            p0.TTFPoints.Add(new Point(1376.375, -1651));
            p0.TTFPoints.Add(new Point(1455.695, -1588.285));
            p0.TTFPoints.Add(new Point(1524.656, -1513.891));
            p0.TTFPoints.Add(new Point(1583.258, -1427.816));
            p0.TTFPoints.Add(new Point(1631.5, -1330.063));
            p0.TTFPoints.Add(new Point(1669.18, -1223.98));
            p0.TTFPoints.Add(new Point(1696.094, -1112.922));
            p0.TTFPoints.Add(new Point(1712.242, -996.8867));
            p0.TTFPoints.Add(new Point(1717.625, -875.875));
            p0.TTFPoints.Add(new Point(1711.684, -746.7383));
            p0.TTFPoints.Add(new Point(1693.859, -624.2031));
            p0.TTFPoints.Add(new Point(1664.152, -508.2695));
            p0.TTFPoints.Add(new Point(1622.563, -398.9375));
            p0.TTFPoints.Add(new Point(1569.648, -299.1016));
            p0.TTFPoints.Add(new Point(1505.969, -211.6563));
            p0.TTFPoints.Add(new Point(1431.523, -136.6016));
            p0.TTFPoints.Add(new Point(1346.313, -73.9375));
            p0.TTFPoints.Add(new Point(1254.551, -24.52734));
            p0.TTFPoints.Add(new Point(1160.453, 10.76563));
            p0.TTFPoints.Add(new Point(1064.02, 31.94141));
            p0.TTFPoints.Add(new Point(965.25, 39));
            p0.TTFPoints.Add(new Point(894.0039, 35.14063));
            p0.TTFPoints.Add(new Point(826.5156, 23.5625));
            p0.TTFPoints.Add(new Point(762.7852, 4.265625));
            p0.TTFPoints.Add(new Point(702.8125, -22.75));
            p0.TTFPoints.Add(new Point(647.3086, -55.65625));
            p0.TTFPoints.Add(new Point(596.9844, -92.625));
            p0.TTFPoints.Add(new Point(551.8398, -133.6563));
            p0.TTFPoints.Add(new Point(511.875, -178.75));
            p0.TTFPoints.Add(new Point(511.875, 661.375));
            p0.TTFPoints.Add(new Point(219.375, 661.375));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(484.25, -853.125));
            p1.TTFPoints.Add(new Point(492.6797, -697.4297));
            p1.TTFPoints.Add(new Point(517.9688, -563.4688));
            p1.TTFPoints.Add(new Point(560.1172, -451.2422));
            p1.TTFPoints.Add(new Point(619.125, -360.75));
            p1.TTFPoints.Add(new Point(690.1172, -291.0781));
            p1.TTFPoints.Add(new Point(768.2188, -241.3125));
            p1.TTFPoints.Add(new Point(853.4297, -211.4531));
            p1.TTFPoints.Add(new Point(945.75, -201.5));
            p1.TTFPoints.Add(new Point(1039.746, -211.8086));
            p1.TTFPoints.Add(new Point(1126.734, -242.7344));
            p1.TTFPoints.Add(new Point(1206.715, -294.2773));
            p1.TTFPoints.Add(new Point(1279.688, -366.4375));
            p1.TTFPoints.Add(new Point(1340.473, -460.2305));
            p1.TTFPoints.Add(new Point(1383.891, -576.6719));
            p1.TTFPoints.Add(new Point(1409.941, -715.7617));
            p1.TTFPoints.Add(new Point(1418.625, -877.5));
            p1.TTFPoints.Add(new Point(1410.145, -1032.078));
            p1.TTFPoints.Add(new Point(1384.703, -1165.938));
            p1.TTFPoints.Add(new Point(1342.301, -1279.078));
            p1.TTFPoints.Add(new Point(1282.938, -1371.5));
            p1.TTFPoints.Add(new Point(1211.793, -1443.305));
            p1.TTFPoints.Add(new Point(1134.047, -1494.594));
            p1.TTFPoints.Add(new Point(1049.699, -1525.367));
            p1.TTFPoints.Add(new Point(958.75, -1535.625));
            p1.TTFPoints.Add(new Point(868.0039, -1524.707));
            p1.TTFPoints.Add(new Point(782.6406, -1491.953));
            p1.TTFPoints.Add(new Point(702.6602, -1437.363));
            p1.TTFPoints.Add(new Point(628.0625, -1360.938));
            p1.TTFPoints.Add(new Point(565.1445, -1263.691));
            p1.TTFPoints.Add(new Point(520.2031, -1146.641));
            p1.TTFPoints.Add(new Point(493.2383, -1009.785));
            p1.TTFPoints.Add(new Point(484.25, -853.125));
            p1.TTFPoints.Add(new Point(484.25, -853.125));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('p', c);

            #endregion Character p
        }

        private void InsertLowerCaseQ()
        {
            #region Character q

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1495;
            c.BlackBoxY = 2426;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(117, -1765);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(1319.5, 661.375));
            p0.TTFPoints.Add(new Point(1319.5, -183.625));
            p0.TTFPoints.Add(new Point(1281.973, -137.7188));
            p0.TTFPoints.Add(new Point(1237.641, -95.875));
            p0.TTFPoints.Add(new Point(1186.504, -58.09375));
            p0.TTFPoints.Add(new Point(1128.563, -24.375));
            p0.TTFPoints.Add(new Point(1066.254, 3.351563));
            p0.TTFPoints.Add(new Point(1002.016, 23.15625));
            p0.TTFPoints.Add(new Point(935.8477, 35.03906));
            p0.TTFPoints.Add(new Point(867.75, 39));
            p0.TTFPoints.Add(new Point(719.5195, 23.66406));
            p0.TTFPoints.Add(new Point(581.9531, -22.34375));
            p0.TTFPoints.Add(new Point(455.0508, -99.02344));
            p0.TTFPoints.Add(new Point(338.8125, -206.375));
            p0.TTFPoints.Add(new Point(241.7695, -340.4375));
            p0.TTFPoints.Add(new Point(172.4531, -497.25));
            p0.TTFPoints.Add(new Point(130.8633, -676.8125));
            p0.TTFPoints.Add(new Point(117, -879.125));
            p0.TTFPoints.Add(new Point(122.6367, -1005.773));
            p0.TTFPoints.Add(new Point(139.5469, -1125.719));
            p0.TTFPoints.Add(new Point(167.7305, -1238.961));
            p0.TTFPoints.Add(new Point(207.1875, -1345.5));
            p0.TTFPoints.Add(new Point(257.3594, -1442.441));
            p0.TTFPoints.Add(new Point(317.6875, -1526.891));
            p0.TTFPoints.Add(new Point(388.1719, -1598.848));
            p0.TTFPoints.Add(new Point(468.8125, -1658.313));
            p0.TTFPoints.Add(new Point(556.6133, -1704.879));
            p0.TTFPoints.Add(new Point(648.5781, -1738.141));
            p0.TTFPoints.Add(new Point(744.707, -1758.098));
            p0.TTFPoints.Add(new Point(845, -1764.75));
            p0.TTFPoints.Add(new Point(996.5313, -1747.891));
            p0.TTFPoints.Add(new Point(1131, -1697.313));
            p0.TTFPoints.Add(new Point(1248.406, -1613.016));
            p0.TTFPoints.Add(new Point(1348.75, -1495));
            p0.TTFPoints.Add(new Point(1348.75, -1725.75));
            p0.TTFPoints.Add(new Point(1612, -1725.75));
            p0.TTFPoints.Add(new Point(1612, 661.375));
            p0.TTFPoints.Add(new Point(1319.5, 661.375));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(417.625, -867.75));
            p1.TTFPoints.Add(new Point(426.3594, -711.5977));
            p1.TTFPoints.Add(new Point(452.5625, -576.2656));
            p1.TTFPoints.Add(new Point(496.2344, -461.7539));
            p1.TTFPoints.Add(new Point(557.375, -368.0625));
            p1.TTFPoints.Add(new Point(630.7031, -295.1914));
            p1.TTFPoints.Add(new Point(710.9375, -243.1406));
            p1.TTFPoints.Add(new Point(798.0781, -211.9102));
            p1.TTFPoints.Add(new Point(892.125, -201.5));
            p1.TTFPoints.Add(new Point(982.3125, -211.4023));
            p1.TTFPoints.Add(new Point(1066, -241.1094));
            p1.TTFPoints.Add(new Point(1143.188, -290.6211));
            p1.TTFPoints.Add(new Point(1213.875, -359.9375));
            p1.TTFPoints.Add(new Point(1272.883, -449.4648));
            p1.TTFPoints.Add(new Point(1315.031, -559.6094));
            p1.TTFPoints.Add(new Point(1340.32, -690.3711));
            p1.TTFPoints.Add(new Point(1348.75, -841.75));
            p1.TTFPoints.Add(new Point(1339.863, -1003.336));
            p1.TTFPoints.Add(new Point(1313.203, -1143.594));
            p1.TTFPoints.Add(new Point(1268.77, -1262.523));
            p1.TTFPoints.Add(new Point(1206.563, -1360.125));
            p1.TTFPoints.Add(new Point(1132.371, -1436.195));
            p1.TTFPoints.Add(new Point(1051.984, -1490.531));
            p1.TTFPoints.Add(new Point(965.4023, -1523.133));
            p1.TTFPoints.Add(new Point(872.625, -1534));
            p1.TTFPoints.Add(new Point(781.168, -1523.895));
            p1.TTFPoints.Add(new Point(696.9219, -1493.578));
            p1.TTFPoints.Add(new Point(619.8867, -1443.051));
            p1.TTFPoints.Add(new Point(550.0625, -1372.313));
            p1.TTFPoints.Add(new Point(492.1211, -1280.145));
            p1.TTFPoints.Add(new Point(450.7344, -1165.328));
            p1.TTFPoints.Add(new Point(425.9023, -1027.863));
            p1.TTFPoints.Add(new Point(417.625, -867.75));
            p1.TTFPoints.Add(new Point(417.625, -867.75));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('q', c);

            #endregion Character q
        }

        private void InsertLowerCaseR()
        {
            #region Character r

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 938;
            c.BlackBoxY = 1765;
            c.CellIncX = 1108;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(216, -1765);
            c.OffsetY = 600;
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(216.125, 0));
            p0.TTFPoints.Add(new Point(216.125, -1725.75));
            p0.TTFPoints.Add(new Point(479.375, -1725.75));
            p0.TTFPoints.Add(new Point(479.375, -1464.125));
            p0.TTFPoints.Add(new Point(528.7852, -1548.117));
            p0.TTFPoints.Add(new Point(576.2656, -1616.469));
            p0.TTFPoints.Add(new Point(621.8164, -1669.18));
            p0.TTFPoints.Add(new Point(665.4375, -1706.25));
            p0.TTFPoints.Add(new Point(709.1602, -1731.844));
            p0.TTFPoints.Add(new Point(755.0156, -1750.125));
            p0.TTFPoints.Add(new Point(803.0039, -1761.094));
            p0.TTFPoints.Add(new Point(853.125, -1764.75));
            p0.TTFPoints.Add(new Point(927.3672, -1758.859));
            p0.TTFPoints.Add(new Point(1002.219, -1741.188));
            p0.TTFPoints.Add(new Point(1077.68, -1711.734));
            p0.TTFPoints.Add(new Point(1153.75, -1670.5));
            p0.TTFPoints.Add(new Point(1053, -1399.125));
            p0.TTFPoints.Add(new Point(999.375, -1426.852));
            p0.TTFPoints.Add(new Point(945.75, -1446.656));
            p0.TTFPoints.Add(new Point(892.125, -1458.539));
            p0.TTFPoints.Add(new Point(838.5, -1462.5));
            p0.TTFPoints.Add(new Point(791.7813, -1458.895));
            p0.TTFPoints.Add(new Point(747.5, -1448.078));
            p0.TTFPoints.Add(new Point(705.6563, -1430.051));
            p0.TTFPoints.Add(new Point(666.25, -1404.813));
            p0.TTFPoints.Add(new Point(630.8047, -1373.176));
            p0.TTFPoints.Add(new Point(600.8438, -1335.953));
            p0.TTFPoints.Add(new Point(576.3672, -1293.145));
            p0.TTFPoints.Add(new Point(557.375, -1244.75));
            p0.TTFPoints.Add(new Point(536.0469, -1164.922));
            p0.TTFPoints.Add(new Point(520.8125, -1081.438));
            p0.TTFPoints.Add(new Point(511.6719, -994.2969));
            p0.TTFPoints.Add(new Point(508.625, -903.5));
            p0.TTFPoints.Add(new Point(508.625, 0));
            p0.TTFPoints.Add(new Point(216.125, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('r', c);

            #endregion Character r
        }

        private void InsertLowerCaseS()
        {
            #region Character s

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1434;
            c.BlackBoxY = 1804;
            c.CellIncX = 1664;
            c.CellIncY = 0;
            c.OffsetY = 600;
            c.GlyphOrigin = new Point(102, -1765);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(102.375, -515.125));
            p0.TTFPoints.Add(new Point(391.625, -560.625));
            p0.TTFPoints.Add(new Point(409.2461, -478.7656));
            p0.TTFPoints.Add(new Point(437.7344, -407.0625));
            p0.TTFPoints.Add(new Point(477.0898, -345.5156));
            p0.TTFPoints.Add(new Point(527.3125, -294.125));
            p0.TTFPoints.Add(new Point(588.5039, -253.6016));
            p0.TTFPoints.Add(new Point(660.7656, -224.6563));
            p0.TTFPoints.Add(new Point(744.0977, -207.2891));
            p0.TTFPoints.Add(new Point(838.5, -201.5));
            p0.TTFPoints.Add(new Point(932.75, -206.6289));
            p0.TTFPoints.Add(new Point(1014, -222.0156));
            p0.TTFPoints.Add(new Point(1082.25, -247.6602));
            p0.TTFPoints.Add(new Point(1137.5, -283.5625));
            p0.TTFPoints.Add(new Point(1180.156, -326.3711));
            p0.TTFPoints.Add(new Point(1210.625, -372.7344));
            p0.TTFPoints.Add(new Point(1228.906, -422.6523));
            p0.TTFPoints.Add(new Point(1235, -476.125));
            p0.TTFPoints.Add(new Point(1229.617, -523.0469));
            p0.TTFPoints.Add(new Point(1213.469, -564.6875));
            p0.TTFPoints.Add(new Point(1186.555, -601.0469));
            p0.TTFPoints.Add(new Point(1148.875, -632.125));
            p0.TTFPoints.Add(new Point(1107.641, -652.9453));
            p0.TTFPoints.Add(new Point(1044.063, -676.4063));
            p0.TTFPoints.Add(new Point(958.1406, -702.5078));
            p0.TTFPoints.Add(new Point(849.875, -731.25));
            p0.TTFPoints.Add(new Point(701.3398, -770.5039));
            p0.TTFPoints.Add(new Point(577.4844, -807.0156));
            p0.TTFPoints.Add(new Point(478.3086, -840.7852));
            p0.TTFPoints.Add(new Point(403.8125, -871.8125));
            p0.TTFPoints.Add(new Point(345.4141, -904.3125));
            p0.TTFPoints.Add(new Point(294.5313, -942.5));
            p0.TTFPoints.Add(new Point(251.1641, -986.375));
            p0.TTFPoints.Add(new Point(215.3125, -1035.938));
            p0.TTFPoints.Add(new Point(187.2305, -1089.715));
            p0.TTFPoints.Add(new Point(167.1719, -1146.234));
            p0.TTFPoints.Add(new Point(155.1367, -1205.496));
            p0.TTFPoints.Add(new Point(151.125, -1267.5));
            p0.TTFPoints.Add(new Point(154.4258, -1324.121));
            p0.TTFPoints.Add(new Point(164.3281, -1378.609));
            p0.TTFPoints.Add(new Point(180.832, -1430.965));
            p0.TTFPoints.Add(new Point(203.9375, -1481.188));
            p0.TTFPoints.Add(new Point(232.7305, -1528.262));
            p0.TTFPoints.Add(new Point(266.2969, -1571.172));
            p0.TTFPoints.Add(new Point(304.6367, -1609.918));
            p0.TTFPoints.Add(new Point(347.75, -1644.5));
            p0.TTFPoints.Add(new Point(384.9727, -1668.723));
            p0.TTFPoints.Add(new Point(428.3906, -1691.016));
            p0.TTFPoints.Add(new Point(478.0039, -1711.379));
            p0.TTFPoints.Add(new Point(533.8125, -1729.813));
            p0.TTFPoints.Add(new Point(593.7852, -1745.098));
            p0.TTFPoints.Add(new Point(655.8906, -1756.016));
            p0.TTFPoints.Add(new Point(720.1289, -1762.566));
            p0.TTFPoints.Add(new Point(786.5, -1764.75));
            p0.TTFPoints.Add(new Point(884.9648, -1761.094));
            p0.TTFPoints.Add(new Point(977.2344, -1750.125));
            p0.TTFPoints.Add(new Point(1063.309, -1731.844));
            p0.TTFPoints.Add(new Point(1143.188, -1706.25));
            p0.TTFPoints.Add(new Point(1214.941, -1674.41));
            p0.TTFPoints.Add(new Point(1276.641, -1637.391));
            p0.TTFPoints.Add(new Point(1328.285, -1595.191));
            p0.TTFPoints.Add(new Point(1369.875, -1547.813));
            p0.TTFPoints.Add(new Point(1403.594, -1493.629));
            p0.TTFPoints.Add(new Point(1431.625, -1431.016));
            p0.TTFPoints.Add(new Point(1453.969, -1359.973));
            p0.TTFPoints.Add(new Point(1470.625, -1280.5));
            p0.TTFPoints.Add(new Point(1184.625, -1241.5));
            p0.TTFPoints.Add(new Point(1170.254, -1304.469));
            p0.TTFPoints.Add(new Point(1146.641, -1360.125));
            p0.TTFPoints.Add(new Point(1113.785, -1408.469));
            p0.TTFPoints.Add(new Point(1071.688, -1449.5));
            p0.TTFPoints.Add(new Point(1020.145, -1482.203));
            p0.TTFPoints.Add(new Point(958.9531, -1505.563));
            p0.TTFPoints.Add(new Point(888.1133, -1519.578));
            p0.TTFPoints.Add(new Point(807.625, -1524.25));
            p0.TTFPoints.Add(new Point(714.0859, -1520.086));
            p0.TTFPoints.Add(new Point(634.9688, -1507.594));
            p0.TTFPoints.Add(new Point(570.2734, -1486.773));
            p0.TTFPoints.Add(new Point(520, -1457.625));
            p0.TTFPoints.Add(new Point(482.3203, -1422.891));
            p0.TTFPoints.Add(new Point(455.4063, -1385.313));
            p0.TTFPoints.Add(new Point(439.2578, -1344.891));
            p0.TTFPoints.Add(new Point(433.875, -1301.625));
            p0.TTFPoints.Add(new Point(436.1094, -1273.898));
            p0.TTFPoints.Add(new Point(442.8125, -1247.594));
            p0.TTFPoints.Add(new Point(453.9844, -1222.711));
            p0.TTFPoints.Add(new Point(469.625, -1199.25));
            p0.TTFPoints.Add(new Point(490.0391, -1176.703));
            p0.TTFPoints.Add(new Point(515.5313, -1156.188));
            p0.TTFPoints.Add(new Point(546.1016, -1137.703));
            p0.TTFPoints.Add(new Point(581.75, -1121.25));
            p0.TTFPoints.Add(new Point(614.3516, -1110.484));
            p0.TTFPoints.Add(new Point(668.2813, -1094.438));
            p0.TTFPoints.Add(new Point(743.5391, -1073.109));
            p0.TTFPoints.Add(new Point(840.125, -1046.5));
            p0.TTFPoints.Add(new Point(983.582, -1006.941));
            p0.TTFPoints.Add(new Point(1103.578, -971.1406));
            p0.TTFPoints.Add(new Point(1200.113, -939.0977));
            p0.TTFPoints.Add(new Point(1273.188, -910.8125));
            p0.TTFPoints.Add(new Point(1331.23, -881.4102));
            p0.TTFPoints.Add(new Point(1382.672, -846.0156));
            p0.TTFPoints.Add(new Point(1427.512, -804.6289));
            p0.TTFPoints.Add(new Point(1465.75, -757.25));
            p0.TTFPoints.Add(new Point(1496.32, -703.8281));
            p0.TTFPoints.Add(new Point(1518.156, -644.3125));
            p0.TTFPoints.Add(new Point(1531.258, -578.7031));
            p0.TTFPoints.Add(new Point(1535.625, -507));
            p0.TTFPoints.Add(new Point(1530.293, -434.9414));
            p0.TTFPoints.Add(new Point(1514.297, -365.0156));
            p0.TTFPoints.Add(new Point(1487.637, -297.2227));
            p0.TTFPoints.Add(new Point(1450.313, -231.5625));
            p0.TTFPoints.Add(new Point(1402.934, -170.625));
            p0.TTFPoints.Add(new Point(1346.109, -117));
            p0.TTFPoints.Add(new Point(1279.84, -70.6875));
            p0.TTFPoints.Add(new Point(1204.125, -31.6875));
            p0.TTFPoints.Add(new Point(1121.047, -0.7617188));
            p0.TTFPoints.Add(new Point(1032.688, 21.32813));
            p0.TTFPoints.Add(new Point(939.0469, 34.58203));
            p0.TTFPoints.Add(new Point(840.125, 39));
            p0.TTFPoints.Add(new Point(681.9414, 30.26563));
            p0.TTFPoints.Add(new Point(543.7656, 4.0625));
            p0.TTFPoints.Add(new Point(425.5977, -39.60938));
            p0.TTFPoints.Add(new Point(327.4375, -100.75));
            p0.TTFPoints.Add(new Point(247.2539, -179.0547));
            p0.TTFPoints.Add(new Point(183.0156, -274.2188));
            p0.TTFPoints.Add(new Point(134.7227, -386.2422));
            p0.TTFPoints.Add(new Point(102.375, -515.125));
            p0.TTFPoints.Add(new Point(102.375, -515.125));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('s', c);

            #endregion Character s
        }

        private void InsertLowerCaseT()
        {
            #region Character t

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 842;
            c.BlackBoxY = 2352;
            c.CellIncX = 925;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(58, -2329);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(858, -261.625));
            p0.TTFPoints.Add(new Point(900.25, -3.25));
            p0.TTFPoints.Add(new Point(840.125, 8.125));
            p0.TTFPoints.Add(new Point(783.25, 16.25));
            p0.TTFPoints.Add(new Point(729.625, 21.125));
            p0.TTFPoints.Add(new Point(679.25, 22.75));
            p0.TTFPoints.Add(new Point(604.0938, 19.60156));
            p0.TTFPoints.Add(new Point(537.875, 10.15625));
            p0.TTFPoints.Add(new Point(480.5938, -5.585938));
            p0.TTFPoints.Add(new Point(432.25, -27.625));
            p0.TTFPoints.Add(new Point(391.625, -54.79297));
            p0.TTFPoints.Add(new Point(357.5, -85.92188));
            p0.TTFPoints.Add(new Point(329.875, -121.0117));
            p0.TTFPoints.Add(new Point(308.75, -160.0625));
            p0.TTFPoints.Add(new Point(293.1094, -212.418));
            p0.TTFPoints.Add(new Point(281.9375, -287.4219));
            p0.TTFPoints.Add(new Point(275.2344, -385.0742));
            p0.TTFPoints.Add(new Point(273, -505.375));
            p0.TTFPoints.Add(new Point(273, -1498.25));
            p0.TTFPoints.Add(new Point(58.5, -1498.25));
            p0.TTFPoints.Add(new Point(58.5, -1725.75));
            p0.TTFPoints.Add(new Point(273, -1725.75));
            p0.TTFPoints.Add(new Point(273, -2153.125));
            p0.TTFPoints.Add(new Point(563.875, -2328.625));
            p0.TTFPoints.Add(new Point(563.875, -1725.75));
            p0.TTFPoints.Add(new Point(858, -1725.75));
            p0.TTFPoints.Add(new Point(858, -1498.25));
            p0.TTFPoints.Add(new Point(563.875, -1498.25));
            p0.TTFPoints.Add(new Point(563.875, -489.125));
            p0.TTFPoints.Add(new Point(564.8398, -432.1484));
            p0.TTFPoints.Add(new Point(567.7344, -386.3438));
            p0.TTFPoints.Add(new Point(572.5586, -351.7109));
            p0.TTFPoints.Add(new Point(579.3125, -328.25));
            p0.TTFPoints.Add(new Point(588.25, -311.2891));
            p0.TTFPoints.Add(new Point(599.625, -296.1563));
            p0.TTFPoints.Add(new Point(613.4375, -282.8516));
            p0.TTFPoints.Add(new Point(629.6875, -271.375));
            p0.TTFPoints.Add(new Point(649.0352, -262.1328));
            p0.TTFPoints.Add(new Point(672.1406, -255.5313));
            p0.TTFPoints.Add(new Point(699.0039, -251.5703));
            p0.TTFPoints.Add(new Point(729.625, -250.25));
            p0.TTFPoints.Add(new Point(755.9297, -250.9609));
            p0.TTFPoints.Add(new Point(786.0938, -253.0938));
            p0.TTFPoints.Add(new Point(820.1172, -256.6484));
            p0.TTFPoints.Add(new Point(858, -261.625));
            p0.TTFPoints.Add(new Point(858, -261.625));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('t', c);

            #endregion Character t
        }

        private void InsertLowerCaseU()
        {
            #region Character u

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1399;
            c.BlackBoxY = 1765;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.OffsetY = 600;
            c.GlyphOrigin = new Point(213, -1726);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(1350.375, 0));
            p0.TTFPoints.Add(new Point(1350.375, -253.5));
            p0.TTFPoints.Add(new Point(1240.586, -125.5313));
            p0.TTFPoints.Add(new Point(1112.719, -34.125));
            p0.TTFPoints.Add(new Point(966.7734, 20.71875));
            p0.TTFPoints.Add(new Point(802.75, 39));
            p0.TTFPoints.Add(new Point(727.6445, 35.34375));
            p0.TTFPoints.Add(new Point(655.0781, 24.375));
            p0.TTFPoints.Add(new Point(585.0508, 6.09375));
            p0.TTFPoints.Add(new Point(517.5625, -19.5));
            p0.TTFPoints.Add(new Point(455.6094, -50.62891));
            p0.TTFPoints.Add(new Point(402.1875, -85.51563));
            p0.TTFPoints.Add(new Point(357.2969, -124.1602));
            p0.TTFPoints.Add(new Point(320.9375, -166.5625));
            p0.TTFPoints.Add(new Point(291.2305, -213.332));
            p0.TTFPoints.Add(new Point(266.2969, -265.0781));
            p0.TTFPoints.Add(new Point(246.1367, -321.8008));
            p0.TTFPoints.Add(new Point(230.75, -383.5));
            p0.TTFPoints.Add(new Point(222.9297, -432.8594));
            p0.TTFPoints.Add(new Point(217.3438, -494.8125));
            p0.TTFPoints.Add(new Point(213.9922, -569.3594));
            p0.TTFPoints.Add(new Point(212.875, -656.5));
            p0.TTFPoints.Add(new Point(212.875, -1725.75));
            p0.TTFPoints.Add(new Point(505.375, -1725.75));
            p0.TTFPoints.Add(new Point(505.375, -768.625));
            p0.TTFPoints.Add(new Point(506.4922, -663.4063));
            p0.TTFPoints.Add(new Point(509.8438, -576.875));
            p0.TTFPoints.Add(new Point(515.4297, -509.0313));
            p0.TTFPoints.Add(new Point(523.25, -459.875));
            p0.TTFPoints.Add(new Point(540.9219, -405.2852));
            p0.TTFPoints.Add(new Point(566.3125, -356.8906));
            p0.TTFPoints.Add(new Point(599.4219, -314.6914));
            p0.TTFPoints.Add(new Point(640.25, -278.6875));
            p0.TTFPoints.Add(new Point(687.5781, -249.8945));
            p0.TTFPoints.Add(new Point(740.1875, -229.3281));
            p0.TTFPoints.Add(new Point(798.0781, -216.9883));
            p0.TTFPoints.Add(new Point(861.25, -212.875));
            p0.TTFPoints.Add(new Point(926.0469, -217.0898));
            p0.TTFPoints.Add(new Point(988.8125, -229.7344));
            p0.TTFPoints.Add(new Point(1049.547, -250.8086));
            p0.TTFPoints.Add(new Point(1108.25, -280.3125));
            p0.TTFPoints.Add(new Point(1161.723, -317.0781));
            p0.TTFPoints.Add(new Point(1206.766, -359.9375));
            p0.TTFPoints.Add(new Point(1243.379, -408.8906));
            p0.TTFPoints.Add(new Point(1271.563, -463.9375));
            p0.TTFPoints.Add(new Point(1292.535, -528.582));
            p0.TTFPoints.Add(new Point(1307.516, -606.3281));
            p0.TTFPoints.Add(new Point(1316.504, -697.1758));
            p0.TTFPoints.Add(new Point(1319.5, -801.125));
            p0.TTFPoints.Add(new Point(1319.5, -1725.75));
            p0.TTFPoints.Add(new Point(1612, -1725.75));
            p0.TTFPoints.Add(new Point(1612, 0));
            p0.TTFPoints.Add(new Point(1350.375, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('u', c);

            #endregion Character u
        }

        private void InsertLowerCaseV()
        {
            #region Character v

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1583;
            c.BlackBoxY = 1726;
            c.CellIncX = 1664;
            c.CellIncY = 0;
            c.OffsetY = 600;
            c.GlyphOrigin = new Point(42, -1726);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(698.75, 0));
            p0.TTFPoints.Add(new Point(42.25, -1725.75));
            p0.TTFPoints.Add(new Point(351, -1725.75));
            p0.TTFPoints.Add(new Point(721.5, -692.25));
            p0.TTFPoints.Add(new Point(750.9531, -607.75));
            p0.TTFPoints.Add(new Point(779.1875, -521.625));
            p0.TTFPoints.Add(new Point(806.2031, -433.875));
            p0.TTFPoints.Add(new Point(832, -344.5));
            p0.TTFPoints.Add(new Point(853.4297, -416.2031));
            p0.TTFPoints.Add(new Point(878.7188, -494.8125));
            p0.TTFPoints.Add(new Point(907.8672, -580.3281));
            p0.TTFPoints.Add(new Point(940.875, -672.75));
            p0.TTFPoints.Add(new Point(1324.375, -1725.75));
            p0.TTFPoints.Add(new Point(1625, -1725.75));
            p0.TTFPoints.Add(new Point(971.75, 0));
            p0.TTFPoints.Add(new Point(698.75, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('v', c);

            #endregion Character v
        }

        private void InsertLowerCaseW()
        {
            #region Character w

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 2367;
            c.BlackBoxY = 1726;
            c.CellIncX = 2403;
            c.CellIncY = 0;
            c.OffsetY = 600;
            c.GlyphOrigin = new Point(10, -1726);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(537.875, 0));
            p0.TTFPoints.Add(new Point(9.75, -1725.75));
            p0.TTFPoints.Add(new Point(312, -1725.75));
            p0.TTFPoints.Add(new Point(586.625, -729.625));
            p0.TTFPoints.Add(new Point(689, -359.125));
            p0.TTFPoints.Add(new Point(697.0234, -391.7266));
            p0.TTFPoints.Add(new Point(714.5938, -461.9063));
            p0.TTFPoints.Add(new Point(741.7109, -569.6641));
            p0.TTFPoints.Add(new Point(778.375, -715));
            p0.TTFPoints.Add(new Point(1053, -1725.75));
            p0.TTFPoints.Add(new Point(1353.625, -1725.75));
            p0.TTFPoints.Add(new Point(1612, -724.75));
            p0.TTFPoints.Add(new Point(1698.125, -394.875));
            p0.TTFPoints.Add(new Point(1797.25, -728));
            p0.TTFPoints.Add(new Point(2093, -1725.75));
            p0.TTFPoints.Add(new Point(2377.375, -1725.75));
            p0.TTFPoints.Add(new Point(1837.875, 0));
            p0.TTFPoints.Add(new Point(1534, 0));
            p0.TTFPoints.Add(new Point(1259.375, -1033.5));
            p0.TTFPoints.Add(new Point(1192.75, -1327.625));
            p0.TTFPoints.Add(new Point(843.375, 0));
            p0.TTFPoints.Add(new Point(537.875, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('w', c);

            #endregion Character w
        }

        private void InsertLowerCaseX()
        {
            #region Character x

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1616;
            c.BlackBoxY = 1726;
            c.CellIncX = 1664;
            c.CellIncY = 0;
            c.OffsetY = 600;
            c.GlyphOrigin = new Point(24, -1726);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(24.375, 0));
            p0.TTFPoints.Add(new Point(654.875, -897));
            p0.TTFPoints.Add(new Point(71.5, -1725.75));
            p0.TTFPoints.Add(new Point(437.125, -1725.75));
            p0.TTFPoints.Add(new Point(702, -1321.125));
            p0.TTFPoints.Add(new Point(737.5469, -1265.773));
            p0.TTFPoints.Add(new Point(769.4375, -1215.094));
            p0.TTFPoints.Add(new Point(797.6719, -1169.086));
            p0.TTFPoints.Add(new Point(822.25, -1127.75));
            p0.TTFPoints.Add(new Point(857.2891, -1179.852));
            p0.TTFPoints.Add(new Point(890.9063, -1228.906));
            p0.TTFPoints.Add(new Point(923.1016, -1274.914));
            p0.TTFPoints.Add(new Point(953.875, -1317.875));
            p0.TTFPoints.Add(new Point(1244.75, -1725.75));
            p0.TTFPoints.Add(new Point(1594.125, -1725.75));
            p0.TTFPoints.Add(new Point(997.75, -913.25));
            p0.TTFPoints.Add(new Point(1639.625, 0));
            p0.TTFPoints.Add(new Point(1280.5, 0));
            p0.TTFPoints.Add(new Point(926.25, -536.25));
            p0.TTFPoints.Add(new Point(832, -680.875));
            p0.TTFPoints.Add(new Point(378.625, 0));
            p0.TTFPoints.Add(new Point(24.375, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('x', c);

            #endregion Character x
        }

        private void InsertLowerCaseY()
        {
            #region Character y

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1581;
            c.BlackBoxY = 2426;
            c.CellIncX = 1664;
            c.CellIncY = 0;
            c.OffsetY = -80;
            c.GlyphOrigin = new Point(54, -1726);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(206.375, 664.625));
            p0.TTFPoints.Add(new Point(173.875, 390));
            p0.TTFPoints.Add(new Point(220.2891, 401.375));
            p0.TTFPoints.Add(new Point(263.6563, 409.5));
            p0.TTFPoints.Add(new Point(303.9766, 414.375));
            p0.TTFPoints.Add(new Point(341.25, 416));
            p0.TTFPoints.Add(new Point(387.5625, 413.9688));
            p0.TTFPoints.Add(new Point(429, 407.875));
            p0.TTFPoints.Add(new Point(465.5625, 397.7188));
            p0.TTFPoints.Add(new Point(497.25, 383.5));
            p0.TTFPoints.Add(new Point(525.1797, 365.625));
            p0.TTFPoints.Add(new Point(550.4688, 344.5));
            p0.TTFPoints.Add(new Point(573.1172, 320.125));
            p0.TTFPoints.Add(new Point(593.125, 292.5));
            p0.TTFPoints.Add(new Point(609.0703, 262.4375));
            p0.TTFPoints.Add(new Point(629.2813, 216.125));
            p0.TTFPoints.Add(new Point(653.7578, 153.5625));
            p0.TTFPoints.Add(new Point(682.5, 74.75));
            p0.TTFPoints.Add(new Point(687.1719, 61.14063));
            p0.TTFPoints.Add(new Point(693.0625, 44.6875));
            p0.TTFPoints.Add(new Point(700.1719, 25.39063));
            p0.TTFPoints.Add(new Point(708.5, 3.25));
            p0.TTFPoints.Add(new Point(53.625, -1725.75));
            p0.TTFPoints.Add(new Point(368.875, -1725.75));
            p0.TTFPoints.Add(new Point(728, -726.375));
            p0.TTFPoints.Add(new Point(762.0234, -630.0938));
            p0.TTFPoints.Add(new Point(794.2188, -531.375));
            p0.TTFPoints.Add(new Point(824.5859, -430.2188));
            p0.TTFPoints.Add(new Point(853.125, -326.625));
            p0.TTFPoints.Add(new Point(879.5313, -426.7656));
            p0.TTFPoints.Add(new Point(908.375, -525.6875));
            p0.TTFPoints.Add(new Point(939.6563, -623.3906));
            p0.TTFPoints.Add(new Point(973.375, -719.875));
            p0.TTFPoints.Add(new Point(1342.25, -1725.75));
            p0.TTFPoints.Add(new Point(1634.75, -1725.75));
            p0.TTFPoints.Add(new Point(978.25, 29.25));
            p0.TTFPoints.Add(new Point(928.3828, 160.3672));
            p0.TTFPoints.Add(new Point(884.4063, 269.3438));
            p0.TTFPoints.Add(new Point(846.3203, 356.1797));
            p0.TTFPoints.Add(new Point(814.125, 420.875));
            p0.TTFPoints.Add(new Point(773.7031, 488.3633));
            p0.TTFPoints.Add(new Point(730.4375, 546.2031));
            p0.TTFPoints.Add(new Point(684.3281, 594.3945));
            p0.TTFPoints.Add(new Point(635.375, 632.9375));
            p0.TTFPoints.Add(new Point(582.5625, 662.4414));
            p0.TTFPoints.Add(new Point(524.875, 683.5156));
            p0.TTFPoints.Add(new Point(462.3125, 696.1602));
            p0.TTFPoints.Add(new Point(394.875, 700.375));
            p0.TTFPoints.Add(new Point(351.4063, 698.1406));
            p0.TTFPoints.Add(new Point(305.5, 691.4375));
            p0.TTFPoints.Add(new Point(257.1563, 680.2656));
            p0.TTFPoints.Add(new Point(206.375, 664.625));
            p0.TTFPoints.Add(new Point(206.375, 664.625));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('y', c);

            #endregion Character y
        }

        private void InsertLowerCaseZ()
        {
            #region Character z

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1528;
            c.BlackBoxY = 1726;
            c.CellIncX = 1664;
            c.CellIncY = 0;
            c.OffsetY = 600;
            c.GlyphOrigin = new Point(65, -1726);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(65, 0));
            p0.TTFPoints.Add(new Point(65, -237.25));
            p0.TTFPoints.Add(new Point(1163.5, -1498.25));
            p0.TTFPoints.Add(new Point(1072.805, -1493.984));
            p0.TTFPoints.Add(new Point(987.5938, -1490.938));
            p0.TTFPoints.Add(new Point(907.8672, -1489.109));
            p0.TTFPoints.Add(new Point(833.625, -1488.5));
            p0.TTFPoints.Add(new Point(130, -1488.5));
            p0.TTFPoints.Add(new Point(130, -1725.75));
            p0.TTFPoints.Add(new Point(1540.5, -1725.75));
            p0.TTFPoints.Add(new Point(1540.5, -1532.375));
            p0.TTFPoints.Add(new Point(606.125, -437.125));
            p0.TTFPoints.Add(new Point(425.75, -237.25));
            p0.TTFPoints.Add(new Point(522.5391, -243.6484));
            p0.TTFPoints.Add(new Point(616.2813, -248.2188));
            p0.TTFPoints.Add(new Point(706.9766, -250.9609));
            p0.TTFPoints.Add(new Point(794.625, -251.875));
            p0.TTFPoints.Add(new Point(1592.5, -251.875));
            p0.TTFPoints.Add(new Point(1592.5, 0));
            p0.TTFPoints.Add(new Point(65, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('z', c);

            #endregion Character z
        }

        #endregion Lower Case

        #region Upper Case

        private void InsertUpperCaseA()
        {
            #region Character A

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 2230;
            c.BlackBoxY = 2382;
            c.CellIncX = 2220;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(-5, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(-4.875, 0));
            p0.TTFPoints.Add(new Point(910, -2382.25));
            p0.TTFPoints.Add(new Point(1249.625, -2382.25));
            p0.TTFPoints.Add(new Point(2224.625, 0));
            p0.TTFPoints.Add(new Point(1865.5, 0));
            p0.TTFPoints.Add(new Point(1587.625, -721.5));
            p0.TTFPoints.Add(new Point(591.5, -721.5));
            p0.TTFPoints.Add(new Point(329.875, 0));
            p0.TTFPoints.Add(new Point(-4.875, 0));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(682.5, -978.25));
            p1.TTFPoints.Add(new Point(1490.125, -978.25));
            p1.TTFPoints.Add(new Point(1241.5, -1638));
            p1.TTFPoints.Add(new Point(1188.281, -1781.609));
            p1.TTFPoints.Add(new Point(1142.375, -1911.813));
            p1.TTFPoints.Add(new Point(1103.781, -2028.609));
            p1.TTFPoints.Add(new Point(1072.5, -2132));
            p1.TTFPoints.Add(new Point(1047.414, -2017.641));
            p1.TTFPoints.Add(new Point(1017.656, -1903.688));
            p1.TTFPoints.Add(new Point(983.2266, -1790.141));
            p1.TTFPoints.Add(new Point(944.125, -1677));
            p1.TTFPoints.Add(new Point(682.5, -978.25));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('A', c);

            #endregion Character A
        }

        private void InsertUpperCaseB()
        {
            #region Character B

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1799;
            c.BlackBoxY = 2382;
            c.CellIncX = 2220;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(244, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(243.75, 0));
            p0.TTFPoints.Add(new Point(243.75, -2382.25));
            p0.TTFPoints.Add(new Point(1137.5, -2382.25));
            p0.TTFPoints.Add(new Point(1267.246, -2377.73));
            p0.TTFPoints.Add(new Point(1383.484, -2364.172));
            p0.TTFPoints.Add(new Point(1486.215, -2341.574));
            p0.TTFPoints.Add(new Point(1575.438, -2309.938));
            p0.TTFPoints.Add(new Point(1653.438, -2268.906));
            p0.TTFPoints.Add(new Point(1722.5, -2218.125));
            p0.TTFPoints.Add(new Point(1782.625, -2157.594));
            p0.TTFPoints.Add(new Point(1833.813, -2087.313));
            p0.TTFPoints.Add(new Point(1874.691, -2011.293));
            p0.TTFPoints.Add(new Point(1903.891, -1933.547));
            p0.TTFPoints.Add(new Point(1921.41, -1854.074));
            p0.TTFPoints.Add(new Point(1927.25, -1772.875));
            p0.TTFPoints.Add(new Point(1922.07, -1697.617));
            p0.TTFPoints.Add(new Point(1906.531, -1624.594));
            p0.TTFPoints.Add(new Point(1880.633, -1553.805));
            p0.TTFPoints.Add(new Point(1844.375, -1485.25));
            p0.TTFPoints.Add(new Point(1797.656, -1421.063));
            p0.TTFPoints.Add(new Point(1740.375, -1363.375));
            p0.TTFPoints.Add(new Point(1672.531, -1312.188));
            p0.TTFPoints.Add(new Point(1594.125, -1267.5));
            p0.TTFPoints.Add(new Point(1695.941, -1230.227));
            p0.TTFPoints.Add(new Point(1785.266, -1181.781));
            p0.TTFPoints.Add(new Point(1862.098, -1122.164));
            p0.TTFPoints.Add(new Point(1926.438, -1051.375));
            p0.TTFPoints.Add(new Point(1977.27, -971.5469));
            p0.TTFPoints.Add(new Point(2013.578, -884.8125));
            p0.TTFPoints.Add(new Point(2035.363, -791.1719));
            p0.TTFPoints.Add(new Point(2042.625, -690.625));
            p0.TTFPoints.Add(new Point(2038.207, -608.4102));
            p0.TTFPoints.Add(new Point(2024.953, -529.1406));
            p0.TTFPoints.Add(new Point(2002.863, -452.8164));
            p0.TTFPoints.Add(new Point(1971.938, -379.4375));
            p0.TTFPoints.Add(new Point(1934.512, -311.6445));
            p0.TTFPoints.Add(new Point(1892.922, -252.0781));
            p0.TTFPoints.Add(new Point(1847.168, -200.7383));
            p0.TTFPoints.Add(new Point(1797.25, -157.625));
            p0.TTFPoints.Add(new Point(1741.949, -121.0117));
            p0.TTFPoints.Add(new Point(1680.047, -89.17188));
            p0.TTFPoints.Add(new Point(1611.543, -62.10547));
            p0.TTFPoints.Add(new Point(1536.438, -39.8125));
            p0.TTFPoints.Add(new Point(1453.613, -22.39453));
            p0.TTFPoints.Add(new Point(1361.953, -9.953125));
            p0.TTFPoints.Add(new Point(1261.457, -2.488281));
            p0.TTFPoints.Add(new Point(1152.125, 0));
            p0.TTFPoints.Add(new Point(243.75, 0));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(559, -1381.25));
            p1.TTFPoints.Add(new Point(1074.125, -1381.25));
            p1.TTFPoints.Add(new Point(1171.523, -1382.977));
            p1.TTFPoints.Add(new Point(1254.094, -1388.156));
            p1.TTFPoints.Add(new Point(1321.836, -1396.789));
            p1.TTFPoints.Add(new Point(1374.75, -1408.875));
            p1.TTFPoints.Add(new Point(1431.168, -1429.695));
            p1.TTFPoints.Add(new Point(1480.172, -1456.406));
            p1.TTFPoints.Add(new Point(1521.762, -1489.008));
            p1.TTFPoints.Add(new Point(1555.938, -1527.5));
            p1.TTFPoints.Add(new Point(1582.598, -1571.578));
            p1.TTFPoints.Add(new Point(1601.641, -1620.938));
            p1.TTFPoints.Add(new Point(1613.066, -1675.578));
            p1.TTFPoints.Add(new Point(1616.875, -1735.5));
            p1.TTFPoints.Add(new Point(1613.32, -1793.035));
            p1.TTFPoints.Add(new Point(1602.656, -1847.016));
            p1.TTFPoints.Add(new Point(1584.883, -1897.441));
            p1.TTFPoints.Add(new Point(1560, -1944.313));
            p1.TTFPoints.Add(new Point(1528.516, -1985.852));
            p1.TTFPoints.Add(new Point(1490.938, -2020.281));
            p1.TTFPoints.Add(new Point(1447.266, -2047.602));
            p1.TTFPoints.Add(new Point(1397.5, -2067.813));
            p1.TTFPoints.Add(new Point(1335.242, -2082.387));
            p1.TTFPoints.Add(new Point(1254.094, -2092.797));
            p1.TTFPoints.Add(new Point(1154.055, -2099.043));
            p1.TTFPoints.Add(new Point(1035.125, -2101.125));
            p1.TTFPoints.Add(new Point(559, -2101.125));
            p1.TTFPoints.Add(new Point(559, -1381.25));
            c.OoXmlPolygons.Add(p1);
            OoXmlPolygon p2 = new OoXmlPolygon();
            p2.TTFPoints.Add(new Point(559, -281.125));
            p2.TTFPoints.Add(new Point(1152.125, -281.125));
            p2.TTFPoints.Add(new Point(1222.813, -281.8359));
            p2.TTFPoints.Add(new Point(1282.125, -283.9688));
            p2.TTFPoints.Add(new Point(1330.063, -287.5234));
            p2.TTFPoints.Add(new Point(1366.625, -292.5));
            p2.TTFPoints.Add(new Point(1418.828, -303.875));
            p2.TTFPoints.Add(new Point(1466.563, -318.5));
            p2.TTFPoints.Add(new Point(1509.828, -336.375));
            p2.TTFPoints.Add(new Point(1548.625, -357.5));
            p2.TTFPoints.Add(new Point(1583.563, -382.8398));
            p2.TTFPoints.Add(new Point(1615.25, -413.3594));
            p2.TTFPoints.Add(new Point(1643.688, -449.0586));
            p2.TTFPoints.Add(new Point(1668.875, -489.9375));
            p2.TTFPoints.Add(new Point(1689.492, -535.082));
            p2.TTFPoints.Add(new Point(1704.219, -583.5781));
            p2.TTFPoints.Add(new Point(1713.055, -635.4258));
            p2.TTFPoints.Add(new Point(1716, -690.625));
            p2.TTFPoints.Add(new Point(1711.734, -755.0664));
            p2.TTFPoints.Add(new Point(1698.938, -815.1406));
            p2.TTFPoints.Add(new Point(1677.609, -870.8477));
            p2.TTFPoints.Add(new Point(1647.75, -922.1875));
            p2.TTFPoints.Add(new Point(1610.324, -967.6875));
            p2.TTFPoints.Add(new Point(1566.297, -1005.875));
            p2.TTFPoints.Add(new Point(1515.668, -1036.75));
            p2.TTFPoints.Add(new Point(1458.438, -1060.313));
            p2.TTFPoints.Add(new Point(1391.254, -1077.73));
            p2.TTFPoints.Add(new Point(1310.766, -1090.172));
            p2.TTFPoints.Add(new Point(1216.973, -1097.637));
            p2.TTFPoints.Add(new Point(1109.875, -1100.125));
            p2.TTFPoints.Add(new Point(559, -1100.125));
            p2.TTFPoints.Add(new Point(559, -281.125));
            c.OoXmlPolygons.Add(p2);
            m_OoXmlCharacters.Add('B', c);

            #endregion Character B
        }

        private void InsertUpperCaseC()
        {
            #region Character C

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 2106;
            c.BlackBoxY = 2464;
            c.CellIncX = 2403;
            c.CellIncY = 0;
            c.OffsetY = -100;
            c.GlyphOrigin = new Point(166, -2423);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(1956.5, -835.25));
            p0.TTFPoints.Add(new Point(2271.75, -755.625));
            p0.TTFPoints.Add(new Point(2212.285, -572.9648));
            p0.TTFPoints.Add(new Point(2133.016, -413.3594));
            p0.TTFPoints.Add(new Point(2033.941, -276.8086));
            p0.TTFPoints.Add(new Point(1915.063, -163.3125));
            p0.TTFPoints.Add(new Point(1779.121, -74.08984));
            p0.TTFPoints.Add(new Point(1628.859, -10.35938));
            p0.TTFPoints.Add(new Point(1464.277, 27.87891));
            p0.TTFPoints.Add(new Point(1285.375, 40.625));
            p0.TTFPoints.Add(new Point(1101.801, 30.82422));
            p0.TTFPoints.Add(new Point(936.2031, 1.421875));
            p0.TTFPoints.Add(new Point(788.582, -47.58203));
            p0.TTFPoints.Add(new Point(658.9375, -116.1875));
            p0.TTFPoints.Add(new Point(545.4922, -203.3789));
            p0.TTFPoints.Add(new Point(446.4688, -308.1406));
            p0.TTFPoints.Add(new Point(361.8672, -430.4727));
            p0.TTFPoints.Add(new Point(291.6875, -570.375));
            p0.TTFPoints.Add(new Point(236.5898, -721.8047));
            p0.TTFPoints.Add(new Point(197.2344, -878.7188));
            p0.TTFPoints.Add(new Point(173.6211, -1041.117));
            p0.TTFPoints.Add(new Point(165.75, -1209));
            p0.TTFPoints.Add(new Point(174.6367, -1389.121));
            p0.TTFPoints.Add(new Point(201.2969, -1557.359));
            p0.TTFPoints.Add(new Point(245.7305, -1713.715));
            p0.TTFPoints.Add(new Point(307.9375, -1858.188));
            p0.TTFPoints.Add(new Point(386.5469, -1988.391));
            p0.TTFPoints.Add(new Point(480.1875, -2101.938));
            p0.TTFPoints.Add(new Point(588.8594, -2198.828));
            p0.TTFPoints.Add(new Point(712.5625, -2279.063));
            p0.TTFPoints.Add(new Point(847.082, -2341.98));
            p0.TTFPoints.Add(new Point(988.2031, -2386.922));
            p0.TTFPoints.Add(new Point(1135.926, -2413.887));
            p0.TTFPoints.Add(new Point(1290.25, -2422.875));
            p0.TTFPoints.Add(new Point(1461.891, -2411.5));
            p0.TTFPoints.Add(new Point(1619.313, -2377.375));
            p0.TTFPoints.Add(new Point(1762.516, -2320.5));
            p0.TTFPoints.Add(new Point(1891.5, -2240.875));
            p0.TTFPoints.Add(new Point(2004.133, -2140.633));
            p0.TTFPoints.Add(new Point(2098.281, -2021.906));
            p0.TTFPoints.Add(new Point(2173.945, -1884.695));
            p0.TTFPoints.Add(new Point(2231.125, -1729));
            p0.TTFPoints.Add(new Point(1920.75, -1655.875));
            p0.TTFPoints.Add(new Point(1874.641, -1777.039));
            p0.TTFPoints.Add(new Point(1819.188, -1880.531));
            p0.TTFPoints.Add(new Point(1754.391, -1966.352));
            p0.TTFPoints.Add(new Point(1680.25, -2034.5));
            p0.TTFPoints.Add(new Point(1596.359, -2086.398));
            p0.TTFPoints.Add(new Point(1502.313, -2123.469));
            p0.TTFPoints.Add(new Point(1398.109, -2145.711));
            p0.TTFPoints.Add(new Point(1283.75, -2153.125));
            p0.TTFPoints.Add(new Point(1152.074, -2144.898));
            p0.TTFPoints.Add(new Point(1031.672, -2120.219));
            p0.TTFPoints.Add(new Point(922.543, -2079.086));
            p0.TTFPoints.Add(new Point(824.6875, -2021.5));
            p0.TTFPoints.Add(new Point(739.3242, -1950.051));
            p0.TTFPoints.Add(new Point(667.6719, -1867.328));
            p0.TTFPoints.Add(new Point(609.7305, -1773.332));
            p0.TTFPoints.Add(new Point(565.5, -1668.063));
            p0.TTFPoints.Add(new Point(532.7969, -1556.293));
            p0.TTFPoints.Add(new Point(509.4375, -1442.797));
            p0.TTFPoints.Add(new Point(495.4219, -1327.574));
            p0.TTFPoints.Add(new Point(490.75, -1210.625));
            p0.TTFPoints.Add(new Point(496.2852, -1063.512));
            p0.TTFPoints.Add(new Point(512.8906, -926.0469));
            p0.TTFPoints.Add(new Point(540.5664, -798.2305));
            p0.TTFPoints.Add(new Point(579.3125, -680.0625));
            p0.TTFPoints.Add(new Point(629.7383, -573.8789));
            p0.TTFPoints.Add(new Point(692.4531, -482.0156));
            p0.TTFPoints.Add(new Point(767.457, -404.4727));
            p0.TTFPoints.Add(new Point(854.75, -341.25));
            p0.TTFPoints.Add(new Point(950.1172, -292.1953));
            p0.TTFPoints.Add(new Point(1049.344, -257.1563));
            p0.TTFPoints.Add(new Point(1152.43, -236.1328));
            p0.TTFPoints.Add(new Point(1259.375, -229.125));
            p0.TTFPoints.Add(new Point(1386.734, -238.6719));
            p0.TTFPoints.Add(new Point(1503.938, -267.3125));
            p0.TTFPoints.Add(new Point(1610.984, -315.0469));
            p0.TTFPoints.Add(new Point(1707.875, -381.875));
            p0.TTFPoints.Add(new Point(1792.273, -467.4922));
            p0.TTFPoints.Add(new Point(1861.844, -571.5938));
            p0.TTFPoints.Add(new Point(1916.586, -694.1797));
            p0.TTFPoints.Add(new Point(1956.5, -835.25));
            p0.TTFPoints.Add(new Point(1956.5, -835.25));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('C', c);

            #endregion Character C
        }

        private void InsertUpperCaseD()
        {
            #region Character D

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1969;
            c.BlackBoxY = 2382;
            c.CellIncX = 2403;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(257, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(256.75, 0));
            p0.TTFPoints.Add(new Point(256.75, -2382.25));
            p0.TTFPoints.Add(new Point(1077.375, -2382.25));
            p0.TTFPoints.Add(new Point(1208.086, -2380.117));
            p0.TTFPoints.Add(new Point(1322.344, -2373.719));
            p0.TTFPoints.Add(new Point(1420.148, -2363.055));
            p0.TTFPoints.Add(new Point(1501.5, -2348.125));
            p0.TTFPoints.Add(new Point(1600.117, -2319.789));
            p0.TTFPoints.Add(new Point(1691.219, -2281.906));
            p0.TTFPoints.Add(new Point(1774.805, -2234.477));
            p0.TTFPoints.Add(new Point(1850.875, -2177.5));
            p0.TTFPoints.Add(new Point(1939.184, -2092.34));
            p0.TTFPoints.Add(new Point(2015.609, -1996.109));
            p0.TTFPoints.Add(new Point(2080.152, -1888.809));
            p0.TTFPoints.Add(new Point(2132.813, -1770.438));
            p0.TTFPoints.Add(new Point(2173.691, -1642.113));
            p0.TTFPoints.Add(new Point(2202.891, -1504.953));
            p0.TTFPoints.Add(new Point(2220.41, -1358.957));
            p0.TTFPoints.Add(new Point(2226.25, -1204.125));
            p0.TTFPoints.Add(new Point(2222.289, -1072.297));
            p0.TTFPoints.Add(new Point(2210.406, -948.1875));
            p0.TTFPoints.Add(new Point(2190.602, -831.7969));
            p0.TTFPoints.Add(new Point(2162.875, -723.125));
            p0.TTFPoints.Add(new Point(2128.953, -622.832));
            p0.TTFPoints.Add(new Point(2090.563, -531.5781));
            p0.TTFPoints.Add(new Point(2047.703, -449.3633));
            p0.TTFPoints.Add(new Point(2000.375, -376.1875));
            p0.TTFPoints.Add(new Point(1949.645, -311.1875));
            p0.TTFPoints.Add(new Point(1896.578, -253.5));
            p0.TTFPoints.Add(new Point(1841.176, -203.125));
            p0.TTFPoints.Add(new Point(1783.438, -160.0625));
            p0.TTFPoints.Add(new Point(1721.484, -123.043));
            p0.TTFPoints.Add(new Point(1653.438, -90.79688));
            p0.TTFPoints.Add(new Point(1579.297, -63.32422));
            p0.TTFPoints.Add(new Point(1499.063, -40.625));
            p0.TTFPoints.Add(new Point(1412.684, -22.85156));
            p0.TTFPoints.Add(new Point(1320.109, -10.15625));
            p0.TTFPoints.Add(new Point(1221.34, -2.539063));
            p0.TTFPoints.Add(new Point(1116.375, 0));
            p0.TTFPoints.Add(new Point(256.75, 0));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(572, -281.125));
            p1.TTFPoints.Add(new Point(1080.625, -281.125));
            p1.TTFPoints.Add(new Point(1192.09, -283.8672));
            p1.TTFPoints.Add(new Point(1290.859, -292.0938));
            p1.TTFPoints.Add(new Point(1376.934, -305.8047));
            p1.TTFPoints.Add(new Point(1450.313, -325));
            p1.TTFPoints.Add(new Point(1513.941, -349.1719));
            p1.TTFPoints.Add(new Point(1570.766, -377.8125));
            p1.TTFPoints.Add(new Point(1620.785, -410.9219));
            p1.TTFPoints.Add(new Point(1664, -448.5));
            p1.TTFPoints.Add(new Point(1716.965, -509.3867));
            p1.TTFPoints.Add(new Point(1763.734, -579.9219));
            p1.TTFPoints.Add(new Point(1804.309, -660.1055));
            p1.TTFPoints.Add(new Point(1838.688, -749.9375));
            p1.TTFPoints.Add(new Point(1866.059, -849.6211));
            p1.TTFPoints.Add(new Point(1885.609, -959.3594));
            p1.TTFPoints.Add(new Point(1897.34, -1079.152));
            p1.TTFPoints.Add(new Point(1901.25, -1209));
            p1.TTFPoints.Add(new Point(1893.582, -1385.059));
            p1.TTFPoints.Add(new Point(1870.578, -1539.484));
            p1.TTFPoints.Add(new Point(1832.238, -1672.277));
            p1.TTFPoints.Add(new Point(1778.563, -1783.438));
            p1.TTFPoints.Add(new Point(1713.918, -1875.504));
            p1.TTFPoints.Add(new Point(1642.672, -1951.016));
            p1.TTFPoints.Add(new Point(1564.824, -2009.973));
            p1.TTFPoints.Add(new Point(1480.375, -2052.375));
            p1.TTFPoints.Add(new Point(1407.352, -2073.703));
            p1.TTFPoints.Add(new Point(1315.031, -2088.938));
            p1.TTFPoints.Add(new Point(1203.414, -2098.078));
            p1.TTFPoints.Add(new Point(1072.5, -2101.125));
            p1.TTFPoints.Add(new Point(572, -2101.125));
            p1.TTFPoints.Add(new Point(572, -281.125));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('D', c);

            #endregion Character D
        }

        private void InsertUpperCaseE()
        {
            #region Character E

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1778;
            c.BlackBoxY = 2382;
            c.CellIncX = 2220;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(263, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(263.25, 0));
            p0.TTFPoints.Add(new Point(263.25, -2382.25));
            p0.TTFPoints.Add(new Point(1985.75, -2382.25));
            p0.TTFPoints.Add(new Point(1985.75, -2101.125));
            p0.TTFPoints.Add(new Point(578.5, -2101.125));
            p0.TTFPoints.Add(new Point(578.5, -1371.5));
            p0.TTFPoints.Add(new Point(1896.375, -1371.5));
            p0.TTFPoints.Add(new Point(1896.375, -1092));
            p0.TTFPoints.Add(new Point(578.5, -1092));
            p0.TTFPoints.Add(new Point(578.5, -281.125));
            p0.TTFPoints.Add(new Point(2041, -281.125));
            p0.TTFPoints.Add(new Point(2041, 0));
            p0.TTFPoints.Add(new Point(263.25, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('E', c);

            #endregion Character E
        }

        private void InsertUpperCaseF()
        {
            #region Character F

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1607;
            c.BlackBoxY = 2382;
            c.CellIncX = 2033;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(273, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(273, 0));
            p0.TTFPoints.Add(new Point(273, -2382.25));
            p0.TTFPoints.Add(new Point(1880.125, -2382.25));
            p0.TTFPoints.Add(new Point(1880.125, -2101.125));
            p0.TTFPoints.Add(new Point(588.25, -2101.125));
            p0.TTFPoints.Add(new Point(588.25, -1363.375));
            p0.TTFPoints.Add(new Point(1706.25, -1363.375));
            p0.TTFPoints.Add(new Point(1706.25, -1082.25));
            p0.TTFPoints.Add(new Point(588.25, -1082.25));
            p0.TTFPoints.Add(new Point(588.25, 0));
            p0.TTFPoints.Add(new Point(273, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('F', c);

            #endregion Character F
        }

        private void InsertUpperCaseG()
        {
            #region Character G

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 2204;
            c.BlackBoxY = 2464;
            c.CellIncX = 2589;
            c.CellIncY = 0;
            c.OffsetY = -100;
            c.GlyphOrigin = new Point(177, -2423);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(1371.5, -934.375));
            p0.TTFPoints.Add(new Point(1371.5, -1213.875));
            p0.TTFPoints.Add(new Point(2380.625, -1215.5));
            p0.TTFPoints.Add(new Point(2380.625, -331.5));
            p0.TTFPoints.Add(new Point(2263.523, -244.6133));
            p0.TTFPoints.Add(new Point(2144.594, -169.2031));
            p0.TTFPoints.Add(new Point(2023.836, -105.2695));
            p0.TTFPoints.Add(new Point(1901.25, -52.8125));
            p0.TTFPoints.Add(new Point(1776.938, -11.93359));
            p0.TTFPoints.Add(new Point(1651, 17.26563));
            p0.TTFPoints.Add(new Point(1523.438, 34.78516));
            p0.TTFPoints.Add(new Point(1394.25, 40.625));
            p0.TTFPoints.Add(new Point(1222.762, 31.23047));
            p0.TTFPoints.Add(new Point(1059.297, 3.046875));
            p0.TTFPoints.Add(new Point(903.8555, -43.92578));
            p0.TTFPoints.Add(new Point(756.4375, -109.6875));
            p0.TTFPoints.Add(new Point(621.8164, -193.2227));
            p0.TTFPoints.Add(new Point(504.7656, -293.5156));
            p0.TTFPoints.Add(new Point(405.2852, -410.5664));
            p0.TTFPoints.Add(new Point(323.375, -544.375));
            p0.TTFPoints.Add(new Point(259.3906, -690.7266));
            p0.TTFPoints.Add(new Point(213.6875, -845.4063));
            p0.TTFPoints.Add(new Point(186.2656, -1008.414));
            p0.TTFPoints.Add(new Point(177.125, -1179.75));
            p0.TTFPoints.Add(new Point(186.2148, -1350.73));
            p0.TTFPoints.Add(new Point(213.4844, -1515.922));
            p0.TTFPoints.Add(new Point(258.9336, -1675.324));
            p0.TTFPoints.Add(new Point(322.5625, -1828.938));
            p0.TTFPoints.Add(new Point(403.2539, -1969.957));
            p0.TTFPoints.Add(new Point(499.8906, -2091.578));
            p0.TTFPoints.Add(new Point(612.4727, -2193.801));
            p0.TTFPoints.Add(new Point(741, -2276.625));
            p0.TTFPoints.Add(new Point(882.6797, -2340.609));
            p0.TTFPoints.Add(new Point(1034.719, -2386.313));
            p0.TTFPoints.Add(new Point(1197.117, -2413.734));
            p0.TTFPoints.Add(new Point(1369.875, -2422.875));
            p0.TTFPoints.Add(new Point(1495.965, -2417.645));
            p0.TTFPoints.Add(new Point(1615.859, -2401.953));
            p0.TTFPoints.Add(new Point(1729.559, -2375.801));
            p0.TTFPoints.Add(new Point(1837.063, -2339.188));
            p0.TTFPoints.Add(new Point(1935.832, -2293.23));
            p0.TTFPoints.Add(new Point(2023.328, -2239.047));
            p0.TTFPoints.Add(new Point(2099.551, -2176.637));
            p0.TTFPoints.Add(new Point(2164.5, -2106));
            p0.TTFPoints.Add(new Point(2220.258, -2025.563));
            p0.TTFPoints.Add(new Point(2268.906, -1933.75));
            p0.TTFPoints.Add(new Point(2310.445, -1830.563));
            p0.TTFPoints.Add(new Point(2344.875, -1716));
            p0.TTFPoints.Add(new Point(2060.5, -1638));
            p0.TTFPoints.Add(new Point(2032.063, -1724.125));
            p0.TTFPoints.Add(new Point(2000.375, -1800.5));
            p0.TTFPoints.Add(new Point(1965.438, -1867.125));
            p0.TTFPoints.Add(new Point(1927.25, -1924));
            p0.TTFPoints.Add(new Point(1883.172, -1973.41));
            p0.TTFPoints.Add(new Point(1830.563, -2017.641));
            p0.TTFPoints.Add(new Point(1769.422, -2056.691));
            p0.TTFPoints.Add(new Point(1699.75, -2090.563));
            p0.TTFPoints.Add(new Point(1623.781, -2117.934));
            p0.TTFPoints.Add(new Point(1543.75, -2137.484));
            p0.TTFPoints.Add(new Point(1459.656, -2149.215));
            p0.TTFPoints.Add(new Point(1371.5, -2153.125));
            p0.TTFPoints.Add(new Point(1267.094, -2149.012));
            p0.TTFPoints.Add(new Point(1170, -2136.672));
            p0.TTFPoints.Add(new Point(1080.219, -2116.105));
            p0.TTFPoints.Add(new Point(997.75, -2087.313));
            p0.TTFPoints.Add(new Point(922.7461, -2051.816));
            p0.TTFPoints.Add(new Point(855.3594, -2011.141));
            p0.TTFPoints.Add(new Point(795.5898, -1965.285));
            p0.TTFPoints.Add(new Point(743.4375, -1914.25));
            p0.TTFPoints.Add(new Point(697.7852, -1859.305));
            p0.TTFPoints.Add(new Point(657.5156, -1801.719));
            p0.TTFPoints.Add(new Point(622.6289, -1741.492));
            p0.TTFPoints.Add(new Point(593.125, -1678.625));
            p0.TTFPoints.Add(new Point(553.3125, -1565.789));
            p0.TTFPoints.Add(new Point(524.875, -1448.281));
            p0.TTFPoints.Add(new Point(507.8125, -1326.102));
            p0.TTFPoints.Add(new Point(502.125, -1199.25));
            p0.TTFPoints.Add(new Point(508.9805, -1046.5));
            p0.TTFPoints.Add(new Point(529.5469, -906.75));
            p0.TTFPoints.Add(new Point(563.8242, -780));
            p0.TTFPoints.Add(new Point(611.8125, -666.25));
            p0.TTFPoints.Add(new Point(672.9023, -565.9063));
            p0.TTFPoints.Add(new Point(746.4844, -479.375));
            p0.TTFPoints.Add(new Point(832.5586, -406.6563));
            p0.TTFPoints.Add(new Point(931.125, -347.75));
            p0.TTFPoints.Add(new Point(1037.563, -302.25));
            p0.TTFPoints.Add(new Point(1147.25, -269.75));
            p0.TTFPoints.Add(new Point(1260.188, -250.25));
            p0.TTFPoints.Add(new Point(1376.375, -243.75));
            p0.TTFPoints.Add(new Point(1478.141, -248.6758));
            p0.TTFPoints.Add(new Point(1578.688, -263.4531));
            p0.TTFPoints.Add(new Point(1678.016, -288.082));
            p0.TTFPoints.Add(new Point(1776.125, -322.5625));
            p0.TTFPoints.Add(new Point(1867.734, -362.6289));
            p0.TTFPoints.Add(new Point(1947.563, -404.0156));
            p0.TTFPoints.Add(new Point(2015.609, -446.7227));
            p0.TTFPoints.Add(new Point(2071.875, -490.75));
            p0.TTFPoints.Add(new Point(2071.875, -934.375));
            p0.TTFPoints.Add(new Point(1371.5, -934.375));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('G', c);

            #endregion Character G
        }

        private void InsertUpperCaseH()
        {
            #region Character H

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1869;
            c.BlackBoxY = 2382;
            c.CellIncX = 2403;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(266, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(266.5, 0));
            p0.TTFPoints.Add(new Point(266.5, -2382.25));
            p0.TTFPoints.Add(new Point(581.75, -2382.25));
            p0.TTFPoints.Add(new Point(581.75, -1404));
            p0.TTFPoints.Add(new Point(1820, -1404));
            p0.TTFPoints.Add(new Point(1820, -2382.25));
            p0.TTFPoints.Add(new Point(2135.25, -2382.25));
            p0.TTFPoints.Add(new Point(2135.25, 0));
            p0.TTFPoints.Add(new Point(1820, 0));
            p0.TTFPoints.Add(new Point(1820, -1122.875));
            p0.TTFPoints.Add(new Point(581.75, -1122.875));
            p0.TTFPoints.Add(new Point(581.75, 0));
            p0.TTFPoints.Add(new Point(266.5, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('H', c);

            #endregion Character H
        }

        private void InsertUpperCaseI()
        {
            #region Character I

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 316;
            c.BlackBoxY = 2382;
            c.CellIncX = 925;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(310, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(310.375, 0));
            p0.TTFPoints.Add(new Point(310.375, -2382.25));
            p0.TTFPoints.Add(new Point(625.625, -2382.25));
            p0.TTFPoints.Add(new Point(625.625, 0));
            p0.TTFPoints.Add(new Point(310.375, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('I', c);

            #endregion Character I
        }

        private void InsertUpperCaseJ()
        {
            #region Character J

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1317;
            c.BlackBoxY = 2423;
            c.CellIncX = 1664;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(89, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(95.875, -676));
            p0.TTFPoints.Add(new Point(380.25, -715));
            p0.TTFPoints.Add(new Point(390.9141, -589.2656));
            p0.TTFPoints.Add(new Point(411.5313, -485.0625));
            p0.TTFPoints.Add(new Point(442.1016, -402.3906));
            p0.TTFPoints.Add(new Point(482.625, -341.25));
            p0.TTFPoints.Add(new Point(532.4922, -297.1719));
            p0.TTFPoints.Add(new Point(591.0938, -265.6875));
            p0.TTFPoints.Add(new Point(658.4297, -246.7969));
            p0.TTFPoints.Add(new Point(734.5, -240.5));
            p0.TTFPoints.Add(new Point(791.7813, -243.9023));
            p0.TTFPoints.Add(new Point(845, -254.1094));
            p0.TTFPoints.Add(new Point(894.1563, -271.1211));
            p0.TTFPoints.Add(new Point(939.25, -294.9375));
            p0.TTFPoints.Add(new Point(978.9609, -324.5938));
            p0.TTFPoints.Add(new Point(1011.969, -359.125));
            p0.TTFPoints.Add(new Point(1038.273, -398.5313));
            p0.TTFPoints.Add(new Point(1057.875, -442.8125));
            p0.TTFPoints.Add(new Point(1072.094, -496.4883));
            p0.TTFPoints.Add(new Point(1082.25, -564.0781));
            p0.TTFPoints.Add(new Point(1088.344, -645.582));
            p0.TTFPoints.Add(new Point(1090.375, -741));
            p0.TTFPoints.Add(new Point(1090.375, -2382.25));
            p0.TTFPoints.Add(new Point(1405.625, -2382.25));
            p0.TTFPoints.Add(new Point(1405.625, -758.875));
            p0.TTFPoints.Add(new Point(1401.105, -617.8047));
            p0.TTFPoints.Add(new Point(1387.547, -493.5938));
            p0.TTFPoints.Add(new Point(1364.949, -386.2422));
            p0.TTFPoints.Add(new Point(1333.313, -295.75));
            p0.TTFPoints.Add(new Point(1291.875, -218.5625));
            p0.TTFPoints.Add(new Point(1239.875, -151.125));
            p0.TTFPoints.Add(new Point(1177.313, -93.4375));
            p0.TTFPoints.Add(new Point(1104.188, -45.5));
            p0.TTFPoints.Add(new Point(1022.379, -7.820313));
            p0.TTFPoints.Add(new Point(933.7656, 19.09375));
            p0.TTFPoints.Add(new Point(838.3477, 35.24219));
            p0.TTFPoints.Add(new Point(736.125, 40.625));
            p0.TTFPoints.Add(new Point(588.5039, 29.35156));
            p0.TTFPoints.Add(new Point(459.2656, -4.46875));
            p0.TTFPoints.Add(new Point(348.4102, -60.83594));
            p0.TTFPoints.Add(new Point(255.9375, -139.75));
            p0.TTFPoints.Add(new Point(183.4727, -240.9063));
            p0.TTFPoints.Add(new Point(132.6406, -364));
            p0.TTFPoints.Add(new Point(103.4414, -509.0313));
            p0.TTFPoints.Add(new Point(95.875, -676));
            p0.TTFPoints.Add(new Point(95.875, -676));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('J', c);

            #endregion Character J
        }

        private void InsertUpperCaseK()
        {
            #region Character K

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1969;
            c.BlackBoxY = 2382;
            c.CellIncX = 2220;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(244, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(243.75, 0));
            p0.TTFPoints.Add(new Point(243.75, -2382.25));
            p0.TTFPoints.Add(new Point(559, -2382.25));
            p0.TTFPoints.Add(new Point(559, -1200.875));
            p0.TTFPoints.Add(new Point(1742, -2382.25));
            p0.TTFPoints.Add(new Point(2169.375, -2382.25));
            p0.TTFPoints.Add(new Point(1170, -1417));
            p0.TTFPoints.Add(new Point(2213.25, 0));
            p0.TTFPoints.Add(new Point(1797.25, 0));
            p0.TTFPoints.Add(new Point(949, -1205.75));
            p0.TTFPoints.Add(new Point(559, -825.5));
            p0.TTFPoints.Add(new Point(559, 0));
            p0.TTFPoints.Add(new Point(243.75, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('K', c);

            #endregion Character K
        }

        private void InsertUpperCaseL()
        {
            #region Character L

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1488;
            c.BlackBoxY = 2382;
            c.CellIncX = 1851;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(244, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(243.75, 0));
            p0.TTFPoints.Add(new Point(243.75, -2382.25));
            p0.TTFPoints.Add(new Point(559, -2382.25));
            p0.TTFPoints.Add(new Point(559, -281.125));
            p0.TTFPoints.Add(new Point(1732.25, -281.125));
            p0.TTFPoints.Add(new Point(1732.25, 0));
            p0.TTFPoints.Add(new Point(243.75, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('L', c);

            #endregion Character L
        }

        private void InsertUpperCaseM()
        {
            #region Character M

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 2273;
            c.BlackBoxY = 2382;
            c.CellIncX = 2772;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(247, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(247, 0));
            p0.TTFPoints.Add(new Point(247, -2382.25));
            p0.TTFPoints.Add(new Point(721.5, -2382.25));
            p0.TTFPoints.Add(new Point(1285.375, -695.5));
            p0.TTFPoints.Add(new Point(1321.734, -585.1016));
            p0.TTFPoints.Add(new Point(1352.813, -489.5313));
            p0.TTFPoints.Add(new Point(1378.609, -408.7891));
            p0.TTFPoints.Add(new Point(1399.125, -342.875));
            p0.TTFPoints.Add(new Point(1422.281, -415.4922));
            p0.TTFPoints.Add(new Point(1451.125, -503.3438));
            p0.TTFPoints.Add(new Point(1485.656, -606.4297));
            p0.TTFPoints.Add(new Point(1525.875, -724.75));
            p0.TTFPoints.Add(new Point(2096.25, -2382.25));
            p0.TTFPoints.Add(new Point(2520.375, -2382.25));
            p0.TTFPoints.Add(new Point(2520.375, 0));
            p0.TTFPoints.Add(new Point(2216.5, 0));
            p0.TTFPoints.Add(new Point(2216.5, -1993.875));
            p0.TTFPoints.Add(new Point(1524.25, 0));
            p0.TTFPoints.Add(new Point(1239.875, 0));
            p0.TTFPoints.Add(new Point(550.875, -2028));
            p0.TTFPoints.Add(new Point(550.875, 0));
            p0.TTFPoints.Add(new Point(247, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('M', c);

            #endregion Character M
        }

        private void InsertUpperCaseN()
        {
            #region Character N

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1877;
            c.BlackBoxY = 2382;
            c.CellIncX = 2403;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(253, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(253.5, 0));
            p0.TTFPoints.Add(new Point(253.5, -2382.25));
            p0.TTFPoints.Add(new Point(576.875, -2382.25));
            p0.TTFPoints.Add(new Point(1828.125, -511.875));
            p0.TTFPoints.Add(new Point(1828.125, -2382.25));
            p0.TTFPoints.Add(new Point(2130.375, -2382.25));
            p0.TTFPoints.Add(new Point(2130.375, 0));
            p0.TTFPoints.Add(new Point(1807, 0));
            p0.TTFPoints.Add(new Point(555.75, -1872));
            p0.TTFPoints.Add(new Point(555.75, 0));
            p0.TTFPoints.Add(new Point(253.5, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('N', c);

            #endregion Character N
        }

        private void InsertUpperCaseO()
        {
            #region Character O

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 2278;
            c.BlackBoxY = 2466;
            c.CellIncX = 2589;
            c.CellIncY = 0;
            c.OffsetY = -100;
            c.GlyphOrigin = new Point(161, -2425);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(160.875, -1160.25));
            p0.TTFPoints.Add(new Point(180.7813, -1440.715));
            p0.TTFPoints.Add(new Point(240.5, -1688.984));
            p0.TTFPoints.Add(new Point(340.0313, -1905.059));
            p0.TTFPoints.Add(new Point(479.375, -2088.938));
            p0.TTFPoints.Add(new Point(650.2031, -2235.746));
            p0.TTFPoints.Add(new Point(844.1875, -2340.609));
            p0.TTFPoints.Add(new Point(1061.328, -2403.527));
            p0.TTFPoints.Add(new Point(1301.625, -2424.5));
            p0.TTFPoints.Add(new Point(1462.5, -2414.648));
            p0.TTFPoints.Add(new Point(1615.25, -2385.094));
            p0.TTFPoints.Add(new Point(1759.875, -2335.836));
            p0.TTFPoints.Add(new Point(1896.375, -2266.875));
            p0.TTFPoints.Add(new Point(2020.941, -2180.293));
            p0.TTFPoints.Add(new Point(2129.766, -2078.172));
            p0.TTFPoints.Add(new Point(2222.848, -1960.512));
            p0.TTFPoints.Add(new Point(2300.188, -1827.313));
            p0.TTFPoints.Add(new Point(2360.973, -1681.621));
            p0.TTFPoints.Add(new Point(2404.391, -1526.484));
            p0.TTFPoints.Add(new Point(2430.441, -1361.902));
            p0.TTFPoints.Add(new Point(2439.125, -1187.875));
            p0.TTFPoints.Add(new Point(2429.984, -1011.461));
            p0.TTFPoints.Add(new Point(2402.563, -844.5938));
            p0.TTFPoints.Add(new Point(2356.859, -687.2734));
            p0.TTFPoints.Add(new Point(2292.875, -539.5));
            p0.TTFPoints.Add(new Point(2212.133, -405.1836));
            p0.TTFPoints.Add(new Point(2116.156, -288.2344));
            p0.TTFPoints.Add(new Point(2004.945, -188.6523));
            p0.TTFPoints.Add(new Point(1878.5, -106.4375));
            p0.TTFPoints.Add(new Point(1741.797, -42.09766));
            p0.TTFPoints.Add(new Point(1599.813, 3.859375));
            p0.TTFPoints.Add(new Point(1452.547, 31.43359));
            p0.TTFPoints.Add(new Point(1300, 40.625));
            p0.TTFPoints.Add(new Point(1136.281, 30.46875));
            p0.TTFPoints.Add(new Point(981.5, 0));
            p0.TTFPoints.Add(new Point(835.6563, -50.78125));
            p0.TTFPoints.Add(new Point(698.75, -121.875));
            p0.TTFPoints.Add(new Point(574.3359, -210.5391));
            p0.TTFPoints.Add(new Point(465.9688, -314.0313));
            p0.TTFPoints.Add(new Point(373.6484, -432.3516));
            p0.TTFPoints.Add(new Point(297.375, -565.5));
            p0.TTFPoints.Add(new Point(237.6563, -708.0938));
            p0.TTFPoints.Add(new Point(195, -854.75));
            p0.TTFPoints.Add(new Point(169.4063, -1005.469));
            p0.TTFPoints.Add(new Point(160.875, -1160.25));
            p0.TTFPoints.Add(new Point(160.875, -1160.25));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(485.875, -1155.375));
            p1.TTFPoints.Add(new Point(500.3477, -951.4883));
            p1.TTFPoints.Add(new Point(543.7656, -770.4531));
            p1.TTFPoints.Add(new Point(616.1289, -612.2695));
            p1.TTFPoints.Add(new Point(717.4375, -476.9375));
            p1.TTFPoints.Add(new Point(840.582, -368.5195));
            p1.TTFPoints.Add(new Point(978.4531, -291.0781));
            p1.TTFPoints.Add(new Point(1131.051, -244.6133));
            p1.TTFPoints.Add(new Point(1298.375, -229.125));
            p1.TTFPoints.Add(new Point(1468.441, -244.7656));
            p1.TTFPoints.Add(new Point(1622.766, -291.6875));
            p1.TTFPoints.Add(new Point(1761.348, -369.8906));
            p1.TTFPoints.Add(new Point(1884.188, -479.375));
            p1.TTFPoints.Add(new Point(1984.785, -617.6016));
            p1.TTFPoints.Add(new Point(2056.641, -782.0313));
            p1.TTFPoints.Add(new Point(2099.754, -972.6641));
            p1.TTFPoints.Add(new Point(2114.125, -1189.5));
            p1.TTFPoints.Add(new Point(2107.98, -1330.316));
            p1.TTFPoints.Add(new Point(2089.547, -1461.891));
            p1.TTFPoints.Add(new Point(2058.824, -1584.223));
            p1.TTFPoints.Add(new Point(2015.813, -1697.313));
            p1.TTFPoints.Add(new Point(1960.969, -1799.688));
            p1.TTFPoints.Add(new Point(1894.75, -1889.875));
            p1.TTFPoints.Add(new Point(1817.156, -1967.875));
            p1.TTFPoints.Add(new Point(1728.188, -2033.688));
            p1.TTFPoints.Add(new Point(1630.637, -2085.941));
            p1.TTFPoints.Add(new Point(1527.297, -2123.266));
            p1.TTFPoints.Add(new Point(1418.168, -2145.66));
            p1.TTFPoints.Add(new Point(1303.25, -2153.125));
            p1.TTFPoints.Add(new Point(1141.715, -2138.754));
            p1.TTFPoints.Add(new Point(991.8594, -2095.641));
            p1.TTFPoints.Add(new Point(853.6836, -2023.785));
            p1.TTFPoints.Add(new Point(727.1875, -1923.188));
            p1.TTFPoints.Add(new Point(621.6133, -1788.973));
            p1.TTFPoints.Add(new Point(546.2031, -1616.266));
            p1.TTFPoints.Add(new Point(500.957, -1405.066));
            p1.TTFPoints.Add(new Point(485.875, -1155.375));
            p1.TTFPoints.Add(new Point(485.875, -1155.375));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('O', c);

            #endregion Character O
        }

        private void InsertUpperCaseP()
        {
            #region Character P

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1818;
            c.BlackBoxY = 2382;
            c.CellIncX = 2220;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(257, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(256.75, 0));
            p0.TTFPoints.Add(new Point(256.75, -2382.25));
            p0.TTFPoints.Add(new Point(1155.375, -2382.25));
            p0.TTFPoints.Add(new Point(1266.992, -2380.828));
            p0.TTFPoints.Add(new Point(1364.594, -2376.563));
            p0.TTFPoints.Add(new Point(1448.18, -2369.453));
            p0.TTFPoints.Add(new Point(1517.75, -2359.5));
            p0.TTFPoints.Add(new Point(1601.945, -2341.574));
            p0.TTFPoints.Add(new Point(1679.031, -2317.047));
            p0.TTFPoints.Add(new Point(1749.008, -2285.918));
            p0.TTFPoints.Add(new Point(1811.875, -2248.188));
            p0.TTFPoints.Add(new Point(1868.293, -2203.043));
            p0.TTFPoints.Add(new Point(1918.922, -2149.672));
            p0.TTFPoints.Add(new Point(1963.762, -2088.074));
            p0.TTFPoints.Add(new Point(2002.813, -2018.25));
            p0.TTFPoints.Add(new Point(2034.449, -1942.484));
            p0.TTFPoints.Add(new Point(2057.047, -1863.063));
            p0.TTFPoints.Add(new Point(2070.605, -1779.984));
            p0.TTFPoints.Add(new Point(2075.125, -1693.25));
            p0.TTFPoints.Add(new Point(2063.039, -1547.152));
            p0.TTFPoints.Add(new Point(2026.781, -1412.734));
            p0.TTFPoints.Add(new Point(1966.352, -1289.996));
            p0.TTFPoints.Add(new Point(1881.75, -1178.938));
            p0.TTFPoints.Add(new Point(1765.563, -1086.871));
            p0.TTFPoints.Add(new Point(1610.375, -1021.109));
            p0.TTFPoints.Add(new Point(1416.188, -981.6523));
            p0.TTFPoints.Add(new Point(1183, -968.5));
            p0.TTFPoints.Add(new Point(572, -968.5));
            p0.TTFPoints.Add(new Point(572, 0));
            p0.TTFPoints.Add(new Point(256.75, 0));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(572, -1249.625));
            p1.TTFPoints.Add(new Point(1187.875, -1249.625));
            p1.TTFPoints.Add(new Point(1329.555, -1256.734));
            p1.TTFPoints.Add(new Point(1449.094, -1278.063));
            p1.TTFPoints.Add(new Point(1546.492, -1313.609));
            p1.TTFPoints.Add(new Point(1621.75, -1363.375));
            p1.TTFPoints.Add(new Point(1677.914, -1426.039));
            p1.TTFPoints.Add(new Point(1718.031, -1500.281));
            p1.TTFPoints.Add(new Point(1742.102, -1586.102));
            p1.TTFPoints.Add(new Point(1750.125, -1683.5));
            p1.TTFPoints.Add(new Point(1745.402, -1755.559));
            p1.TTFPoints.Add(new Point(1731.234, -1822.234));
            p1.TTFPoints.Add(new Point(1707.621, -1883.527));
            p1.TTFPoints.Add(new Point(1674.563, -1939.438));
            p1.TTFPoints.Add(new Point(1633.785, -1988.137));
            p1.TTFPoints.Add(new Point(1587.016, -2027.797));
            p1.TTFPoints.Add(new Point(1534.254, -2058.418));
            p1.TTFPoints.Add(new Point(1475.5, -2080));
            p1.TTFPoints.Add(new Point(1427.258, -2089.242));
            p1.TTFPoints.Add(new Point(1362.156, -2095.844));
            p1.TTFPoints.Add(new Point(1280.195, -2099.805));
            p1.TTFPoints.Add(new Point(1181.375, -2101.125));
            p1.TTFPoints.Add(new Point(572, -2101.125));
            p1.TTFPoints.Add(new Point(572, -1249.625));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('P', c);

            #endregion Character P
        }

        private void InsertUpperCaseQ()
        {
            #region Character Q

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 2324;
            c.BlackBoxY = 2610;
            c.CellIncX = 2589;
            c.CellIncY = 0;
            c.OffsetY = -200;
            c.GlyphOrigin = new Point(143, -2425);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(2062.125, -255.125));
            p0.TTFPoints.Add(new Point(2169.68, -184.6406));
            p0.TTFPoints.Add(new Point(2272.969, -124.3125));
            p0.TTFPoints.Add(new Point(2371.992, -74.14063));
            p0.TTFPoints.Add(new Point(2466.75, -34.125));
            p0.TTFPoints.Add(new Point(2374.125, 185.25));
            p0.TTFPoints.Add(new Point(2245.852, 132.2344));
            p0.TTFPoints.Add(new Point(2117.781, 65.8125));
            p0.TTFPoints.Add(new Point(1989.914, -14.01563));
            p0.TTFPoints.Add(new Point(1862.25, -107.25));
            p0.TTFPoints.Add(new Point(1726.359, -42.55469));
            p0.TTFPoints.Add(new Point(1583.563, 3.65625));
            p0.TTFPoints.Add(new Point(1433.859, 31.38281));
            p0.TTFPoints.Add(new Point(1277.25, 40.625));
            p0.TTFPoints.Add(new Point(1119.32, 30.875));
            p0.TTFPoints.Add(new Point(968.9063, 1.625));
            p0.TTFPoints.Add(new Point(826.0078, -47.125));
            p0.TTFPoints.Add(new Point(690.625, -115.375));
            p0.TTFPoints.Add(new Point(566.5664, -201.2969));
            p0.TTFPoints.Add(new Point(457.6406, -303.0625));
            p0.TTFPoints.Add(new Point(363.8477, -420.6719));
            p0.TTFPoints.Add(new Point(285.1875, -554.125));
            p0.TTFPoints.Add(new Point(222.9805, -699.9688));
            p0.TTFPoints.Add(new Point(178.5469, -854.75));
            p0.TTFPoints.Add(new Point(151.8867, -1018.469));
            p0.TTFPoints.Add(new Point(143, -1191.125));
            p0.TTFPoints.Add(new Point(151.9375, -1363.477));
            p0.TTFPoints.Add(new Point(178.75, -1527.906));
            p0.TTFPoints.Add(new Point(223.4375, -1684.414));
            p0.TTFPoints.Add(new Point(286, -1833));
            p0.TTFPoints.Add(new Point(365.0664, -1968.992));
            p0.TTFPoints.Add(new Point(459.2656, -2087.719));
            p0.TTFPoints.Add(new Point(568.5977, -2189.18));
            p0.TTFPoints.Add(new Point(693.0625, -2273.375));
            p0.TTFPoints.Add(new Point(829.0039, -2339.492));
            p0.TTFPoints.Add(new Point(972.7656, -2386.719));
            p0.TTFPoints.Add(new Point(1124.348, -2415.055));
            p0.TTFPoints.Add(new Point(1283.75, -2424.5));
            p0.TTFPoints.Add(new Point(1444.625, -2414.699));
            p0.TTFPoints.Add(new Point(1597.375, -2385.297));
            p0.TTFPoints.Add(new Point(1742, -2336.293));
            p0.TTFPoints.Add(new Point(1878.5, -2267.688));
            p0.TTFPoints.Add(new Point(2003.066, -2181.461));
            p0.TTFPoints.Add(new Point(2111.891, -2079.594));
            p0.TTFPoints.Add(new Point(2204.973, -1962.086));
            p0.TTFPoints.Add(new Point(2282.313, -1828.938));
            p0.TTFPoints.Add(new Point(2343.098, -1683.449));
            p0.TTFPoints.Add(new Point(2386.516, -1528.922));
            p0.TTFPoints.Add(new Point(2412.566, -1365.355));
            p0.TTFPoints.Add(new Point(2421.25, -1192.75));
            p0.TTFPoints.Add(new Point(2415.664, -1049.395));
            p0.TTFPoints.Add(new Point(2398.906, -913.4531));
            p0.TTFPoints.Add(new Point(2370.977, -784.9258));
            p0.TTFPoints.Add(new Point(2331.875, -663.8125));
            p0.TTFPoints.Add(new Point(2281.5, -550.2148));
            p0.TTFPoints.Add(new Point(2219.75, -444.2344));
            p0.TTFPoints.Add(new Point(2146.625, -345.8711));
            p0.TTFPoints.Add(new Point(2062.125, -255.125));
            p0.TTFPoints.Add(new Point(2062.125, -255.125));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(1368.25, -658.125));
            p1.TTFPoints.Add(new Point(1498.758, -615.2656));
            p1.TTFPoints.Add(new Point(1617.281, -563.0625));
            p1.TTFPoints.Add(new Point(1723.82, -501.5156));
            p1.TTFPoints.Add(new Point(1818.375, -430.625));
            p1.TTFPoints.Add(new Point(1939.945, -573.3203));
            p1.TTFPoints.Add(new Point(2026.781, -747.9063));
            p1.TTFPoints.Add(new Point(2078.883, -954.3828));
            p1.TTFPoints.Add(new Point(2096.25, -1192.75));
            p1.TTFPoints.Add(new Point(2090.105, -1332.805));
            p1.TTFPoints.Add(new Point(2071.672, -1463.719));
            p1.TTFPoints.Add(new Point(2040.949, -1585.492));
            p1.TTFPoints.Add(new Point(1997.938, -1698.125));
            p1.TTFPoints.Add(new Point(1943.094, -1800.145));
            p1.TTFPoints.Add(new Point(1876.875, -1890.078));
            p1.TTFPoints.Add(new Point(1799.281, -1967.926));
            p1.TTFPoints.Add(new Point(1710.313, -2033.688));
            p1.TTFPoints.Add(new Point(1612.762, -2085.941));
            p1.TTFPoints.Add(new Point(1509.422, -2123.266));
            p1.TTFPoints.Add(new Point(1400.293, -2145.66));
            p1.TTFPoints.Add(new Point(1285.375, -2153.125));
            p1.TTFPoints.Add(new Point(1116.578, -2138.043));
            p1.TTFPoints.Add(new Point(962.8125, -2092.797));
            p1.TTFPoints.Add(new Point(824.0781, -2017.387));
            p1.TTFPoints.Add(new Point(700.375, -1911.813));
            p1.TTFPoints.Add(new Point(598.7109, -1776.277));
            p1.TTFPoints.Add(new Point(526.0938, -1610.984));
            p1.TTFPoints.Add(new Point(482.5234, -1415.934));
            p1.TTFPoints.Add(new Point(468, -1191.125));
            p1.TTFPoints.Add(new Point(482.3711, -972.2578));
            p1.TTFPoints.Add(new Point(525.4844, -780.4063));
            p1.TTFPoints.Add(new Point(597.3398, -615.5703));
            p1.TTFPoints.Add(new Point(697.9375, -477.75));
            p1.TTFPoints.Add(new Point(820.8789, -368.9766));
            p1.TTFPoints.Add(new Point(959.7656, -291.2813));
            p1.TTFPoints.Add(new Point(1114.598, -244.6641));
            p1.TTFPoints.Add(new Point(1285.375, -229.125));
            p1.TTFPoints.Add(new Point(1368.656, -233.0859));
            p1.TTFPoints.Add(new Point(1449.5, -244.9688));
            p1.TTFPoints.Add(new Point(1527.906, -264.7734));
            p1.TTFPoints.Add(new Point(1603.875, -292.5));
            p1.TTFPoints.Add(new Point(1528.922, -336.9844));
            p1.TTFPoints.Add(new Point(1451.938, -374.5625));
            p1.TTFPoints.Add(new Point(1372.922, -405.2344));
            p1.TTFPoints.Add(new Point(1291.875, -429));
            p1.TTFPoints.Add(new Point(1368.25, -658.125));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('Q', c);

            #endregion Character Q
        }

        private void InsertUpperCaseR()
        {
            #region Character R

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 2099;
            c.BlackBoxY = 2382;
            c.CellIncX = 2403;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(262, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(261.625, 0));
            p0.TTFPoints.Add(new Point(261.625, -2382.25));
            p0.TTFPoints.Add(new Point(1317.875, -2382.25));
            p0.TTFPoints.Add(new Point(1467.578, -2378.238));
            p0.TTFPoints.Add(new Point(1598.188, -2366.203));
            p0.TTFPoints.Add(new Point(1709.703, -2346.145));
            p0.TTFPoints.Add(new Point(1802.125, -2318.063));
            p0.TTFPoints.Add(new Point(1880.836, -2279.824));
            p0.TTFPoints.Add(new Point(1951.219, -2229.297));
            p0.TTFPoints.Add(new Point(2013.273, -2166.48));
            p0.TTFPoints.Add(new Point(2067, -2091.375));
            p0.TTFPoints.Add(new Point(2110.367, -2007.992));
            p0.TTFPoints.Add(new Point(2141.344, -1920.344));
            p0.TTFPoints.Add(new Point(2159.93, -1828.43));
            p0.TTFPoints.Add(new Point(2166.125, -1732.25));
            p0.TTFPoints.Add(new Point(2155.867, -1610.477));
            p0.TTFPoints.Add(new Point(2125.094, -1498.656));
            p0.TTFPoints.Add(new Point(2073.805, -1396.789));
            p0.TTFPoints.Add(new Point(2002, -1304.875));
            p0.TTFPoints.Add(new Point(1908.766, -1225.859));
            p0.TTFPoints.Add(new Point(1793.188, -1162.688));
            p0.TTFPoints.Add(new Point(1655.266, -1115.359));
            p0.TTFPoints.Add(new Point(1495, -1083.875));
            p0.TTFPoints.Add(new Point(1553.805, -1053.914));
            p0.TTFPoints.Add(new Point(1605.094, -1024.156));
            p0.TTFPoints.Add(new Point(1648.867, -994.6016));
            p0.TTFPoints.Add(new Point(1685.125, -965.25));
            p0.TTFPoints.Add(new Point(1753.273, -897.9141));
            p0.TTFPoints.Add(new Point(1819.594, -822.6563));
            p0.TTFPoints.Add(new Point(1884.086, -739.4766));
            p0.TTFPoints.Add(new Point(1946.75, -648.375));
            p0.TTFPoints.Add(new Point(2361.125, 0));
            p0.TTFPoints.Add(new Point(1964.625, 0));
            p0.TTFPoints.Add(new Point(1649.375, -495.625));
            p0.TTFPoints.Add(new Point(1583.359, -596.5781));
            p0.TTFPoints.Add(new Point(1523.438, -684.9375));
            p0.TTFPoints.Add(new Point(1469.609, -760.7031));
            p0.TTFPoints.Add(new Point(1421.875, -823.875));
            p0.TTFPoints.Add(new Point(1378.355, -876.4844));
            p0.TTFPoints.Add(new Point(1337.172, -920.5625));
            p0.TTFPoints.Add(new Point(1298.324, -956.1094));
            p0.TTFPoints.Add(new Point(1261.813, -983.125));
            p0.TTFPoints.Add(new Point(1226.316, -1004.148));
            p0.TTFPoints.Add(new Point(1190.516, -1021.719));
            p0.TTFPoints.Add(new Point(1154.41, -1035.836));
            p0.TTFPoints.Add(new Point(1118, -1046.5));
            p0.TTFPoints.Add(new Point(1086.922, -1051.477));
            p0.TTFPoints.Add(new Point(1047.313, -1055.031));
            p0.TTFPoints.Add(new Point(999.1719, -1057.164));
            p0.TTFPoints.Add(new Point(942.5, -1057.875));
            p0.TTFPoints.Add(new Point(576.875, -1057.875));
            p0.TTFPoints.Add(new Point(576.875, 0));
            p0.TTFPoints.Add(new Point(261.625, 0));
            c.OoXmlPolygons.Add(p0);
            OoXmlPolygon p1 = new OoXmlPolygon();
            p1.TTFPoints.Add(new Point(576.875, -1330.875));
            p1.TTFPoints.Add(new Point(1254.5, -1330.875));
            p1.TTFPoints.Add(new Point(1356.672, -1333.668));
            p1.TTFPoints.Add(new Point(1447.063, -1342.047));
            p1.TTFPoints.Add(new Point(1525.672, -1356.012));
            p1.TTFPoints.Add(new Point(1592.5, -1375.563));
            p1.TTFPoints.Add(new Point(1649.781, -1401.258));
            p1.TTFPoints.Add(new Point(1699.75, -1433.656));
            p1.TTFPoints.Add(new Point(1742.406, -1472.758));
            p1.TTFPoints.Add(new Point(1777.75, -1518.563));
            p1.TTFPoints.Add(new Point(1805.477, -1568.785));
            p1.TTFPoints.Add(new Point(1825.281, -1621.141));
            p1.TTFPoints.Add(new Point(1837.164, -1675.629));
            p1.TTFPoints.Add(new Point(1841.125, -1732.25));
            p1.TTFPoints.Add(new Point(1833.457, -1812.992));
            p1.TTFPoints.Add(new Point(1810.453, -1886.219));
            p1.TTFPoints.Add(new Point(1772.113, -1951.93));
            p1.TTFPoints.Add(new Point(1718.438, -2010.125));
            p1.TTFPoints.Add(new Point(1648.207, -2057.758));
            p1.TTFPoints.Add(new Point(1560.203, -2091.781));
            p1.TTFPoints.Add(new Point(1454.426, -2112.195));
            p1.TTFPoints.Add(new Point(1330.875, -2119));
            p1.TTFPoints.Add(new Point(576.875, -2119));
            p1.TTFPoints.Add(new Point(576.875, -1330.875));
            c.OoXmlPolygons.Add(p1);
            m_OoXmlCharacters.Add('R', c);

            #endregion Character R
        }

        private void InsertUpperCaseS()
        {
            #region Character S

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1897;
            c.BlackBoxY = 2464;
            c.CellIncX = 2220;
            c.CellIncY = 0;
            c.OffsetY = -100;
            c.GlyphOrigin = new Point(149, -2423);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(149.5, -765.375));
            p0.TTFPoints.Add(new Point(446.875, -791.375));
            p0.TTFPoints.Add(new Point(460.9414, -706.0117));
            p0.TTFPoints.Add(new Point(482.0156, -628.6719));
            p0.TTFPoints.Add(new Point(510.0977, -559.3555));
            p0.TTFPoints.Add(new Point(545.1875, -498.0625));
            p0.TTFPoints.Add(new Point(589.1133, -443.5234));
            p0.TTFPoints.Add(new Point(643.7031, -394.4688));
            p0.TTFPoints.Add(new Point(708.957, -350.8984));
            p0.TTFPoints.Add(new Point(784.875, -312.8125));
            p0.TTFPoints.Add(new Point(868.6641, -281.8867));
            p0.TTFPoints.Add(new Point(957.5313, -259.7969));
            p0.TTFPoints.Add(new Point(1051.477, -246.543));
            p0.TTFPoints.Add(new Point(1150.5, -242.125));
            p0.TTFPoints.Add(new Point(1238.047, -245.4766));
            p0.TTFPoints.Add(new Point(1320.313, -255.5313));
            p0.TTFPoints.Add(new Point(1397.297, -272.2891));
            p0.TTFPoints.Add(new Point(1469, -295.75));
            p0.TTFPoints.Add(new Point(1533.645, -325.0508));
            p0.TTFPoints.Add(new Point(1589.453, -359.3281));
            p0.TTFPoints.Add(new Point(1636.426, -398.582));
            p0.TTFPoints.Add(new Point(1674.563, -442.8125));
            p0.TTFPoints.Add(new Point(1704.066, -490.5977));
            p0.TTFPoints.Add(new Point(1725.141, -540.5156));
            p0.TTFPoints.Add(new Point(1737.785, -592.5664));
            p0.TTFPoints.Add(new Point(1742, -646.75));
            p0.TTFPoints.Add(new Point(1737.938, -701.0352));
            p0.TTFPoints.Add(new Point(1725.75, -751.7656));
            p0.TTFPoints.Add(new Point(1705.438, -798.9414));
            p0.TTFPoints.Add(new Point(1677, -842.5625));
            p0.TTFPoints.Add(new Point(1639.219, -882.7305));
            p0.TTFPoints.Add(new Point(1590.875, -919.5469));
            p0.TTFPoints.Add(new Point(1531.969, -953.0117));
            p0.TTFPoints.Add(new Point(1462.5, -983.125));
            p0.TTFPoints.Add(new Point(1400.039, -1004.402));
            p0.TTFPoints.Add(new Point(1308.531, -1030.859));
            p0.TTFPoints.Add(new Point(1187.977, -1062.496));
            p0.TTFPoints.Add(new Point(1038.375, -1099.313));
            p0.TTFPoints.Add(new Point(886.5391, -1138.16));
            p0.TTFPoints.Add(new Point(759.2813, -1175.891));
            p0.TTFPoints.Add(new Point(656.6016, -1212.504));
            p0.TTFPoints.Add(new Point(578.5, -1248));
            p0.TTFPoints.Add(new Point(498.6211, -1295.379));
            p0.TTFPoints.Add(new Point(429.6094, -1348.141));
            p0.TTFPoints.Add(new Point(371.4648, -1406.285));
            p0.TTFPoints.Add(new Point(324.1875, -1469.813));
            p0.TTFPoints.Add(new Point(287.5742, -1538.012));
            p0.TTFPoints.Add(new Point(261.4219, -1610.172));
            p0.TTFPoints.Add(new Point(245.7305, -1686.293));
            p0.TTFPoints.Add(new Point(240.5, -1766.375));
            p0.TTFPoints.Add(new Point(246.8984, -1855.09));
            p0.TTFPoints.Add(new Point(266.0938, -1940.859));
            p0.TTFPoints.Add(new Point(298.0859, -2023.684));
            p0.TTFPoints.Add(new Point(342.875, -2103.563));
            p0.TTFPoints.Add(new Point(399.9531, -2177.246));
            p0.TTFPoints.Add(new Point(468.8125, -2241.484));
            p0.TTFPoints.Add(new Point(549.4531, -2296.277));
            p0.TTFPoints.Add(new Point(641.875, -2341.625));
            p0.TTFPoints.Add(new Point(742.9297, -2377.172));
            p0.TTFPoints.Add(new Point(849.4688, -2402.563));
            p0.TTFPoints.Add(new Point(961.4922, -2417.797));
            p0.TTFPoints.Add(new Point(1079, -2422.875));
            p0.TTFPoints.Add(new Point(1207.527, -2417.543));
            p0.TTFPoints.Add(new Point(1328.234, -2401.547));
            p0.TTFPoints.Add(new Point(1441.121, -2374.887));
            p0.TTFPoints.Add(new Point(1546.188, -2337.563));
            p0.TTFPoints.Add(new Point(1641.504, -2289.879));
            p0.TTFPoints.Add(new Point(1725.141, -2232.141));
            p0.TTFPoints.Add(new Point(1797.098, -2164.348));
            p0.TTFPoints.Add(new Point(1857.375, -2086.5));
            p0.TTFPoints.Add(new Point(1905.516, -2000.883));
            p0.TTFPoints.Add(new Point(1941.063, -1909.781));
            p0.TTFPoints.Add(new Point(1964.016, -1813.195));
            p0.TTFPoints.Add(new Point(1974.375, -1711.125));
            p0.TTFPoints.Add(new Point(1672.125, -1688.375));
            p0.TTFPoints.Add(new Point(1652.676, -1794.406));
            p0.TTFPoints.Add(new Point(1618.703, -1886.625));
            p0.TTFPoints.Add(new Point(1570.207, -1965.031));
            p0.TTFPoints.Add(new Point(1507.188, -2029.625));
            p0.TTFPoints.Add(new Point(1428.527, -2080.102));
            p0.TTFPoints.Add(new Point(1333.109, -2116.156));
            p0.TTFPoints.Add(new Point(1220.934, -2137.789));
            p0.TTFPoints.Add(new Point(1092, -2145));
            p0.TTFPoints.Add(new Point(958.6992, -2138.449));
            p0.TTFPoints.Add(new Point(844.7969, -2118.797));
            p0.TTFPoints.Add(new Point(750.293, -2086.043));
            p0.TTFPoints.Add(new Point(675.1875, -2040.188));
            p0.TTFPoints.Add(new Point(617.957, -1985.09));
            p0.TTFPoints.Add(new Point(577.0781, -1924.609));
            p0.TTFPoints.Add(new Point(552.5508, -1858.746));
            p0.TTFPoints.Add(new Point(544.375, -1787.5));
            p0.TTFPoints.Add(new Point(550.1641, -1726.156));
            p0.TTFPoints.Add(new Point(567.5313, -1670.5));
            p0.TTFPoints.Add(new Point(596.4766, -1620.531));
            p0.TTFPoints.Add(new Point(637, -1576.25));
            p0.TTFPoints.Add(new Point(700.832, -1534.559));
            p0.TTFPoints.Add(new Point(801.3281, -1492.359));
            p0.TTFPoints.Add(new Point(938.4883, -1449.652));
            p0.TTFPoints.Add(new Point(1112.313, -1406.438));
            p0.TTFPoints.Add(new Point(1289.387, -1364.34));
            p0.TTFPoints.Add(new Point(1436.297, -1324.984));
            p0.TTFPoints.Add(new Point(1553.043, -1288.371));
            p0.TTFPoints.Add(new Point(1639.625, -1254.5));
            p0.TTFPoints.Add(new Point(1736.82, -1203.363));
            p0.TTFPoints.Add(new Point(1820.406, -1145.828));
            p0.TTFPoints.Add(new Point(1890.383, -1081.895));
            p0.TTFPoints.Add(new Point(1946.75, -1011.563));
            p0.TTFPoints.Add(new Point(1990.117, -935.2383));
            p0.TTFPoints.Add(new Point(2021.094, -853.3281));
            p0.TTFPoints.Add(new Point(2039.68, -765.832));
            p0.TTFPoints.Add(new Point(2045.875, -672.75));
            p0.TTFPoints.Add(new Point(2039.07, -579.0586));
            p0.TTFPoints.Add(new Point(2018.656, -488.1094));
            p0.TTFPoints.Add(new Point(1984.633, -399.9023));
            p0.TTFPoints.Add(new Point(1937, -314.4375));
            p0.TTFPoints.Add(new Point(1876.621, -235.0156));
            p0.TTFPoints.Add(new Point(1804.359, -164.9375));
            p0.TTFPoints.Add(new Point(1720.215, -104.2031));
            p0.TTFPoints.Add(new Point(1624.188, -52.8125));
            p0.TTFPoints.Add(new Point(1519.02, -11.93359));
            p0.TTFPoints.Add(new Point(1407.453, 17.26563));
            p0.TTFPoints.Add(new Point(1289.488, 34.78516));
            p0.TTFPoints.Add(new Point(1165.125, 40.625));
            p0.TTFPoints.Add(new Point(1009.988, 34.73438));
            p0.TTFPoints.Add(new Point(867.9531, 17.0625));
            p0.TTFPoints.Add(new Point(739.0195, -12.39063));
            p0.TTFPoints.Add(new Point(623.1875, -53.625));
            p0.TTFPoints.Add(new Point(519.7969, -106.6914));
            p0.TTFPoints.Add(new Point(428.1875, -171.6406));
            p0.TTFPoints.Add(new Point(348.3594, -248.4727));
            p0.TTFPoints.Add(new Point(280.3125, -337.1875));
            p0.TTFPoints.Add(new Point(225.5195, -434.9414));
            p0.TTFPoints.Add(new Point(185.4531, -538.8906));
            p0.TTFPoints.Add(new Point(160.1133, -649.0352));
            p0.TTFPoints.Add(new Point(149.5, -765.375));
            p0.TTFPoints.Add(new Point(149.5, -765.375));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('S', c);

            #endregion Character S
        }

        private void InsertUpperCaseT()
        {
            #region Character T

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1888;
            c.BlackBoxY = 2382;
            c.CellIncX = 2033;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(78, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(862.875, 0));
            p0.TTFPoints.Add(new Point(862.875, -2101.125));
            p0.TTFPoints.Add(new Point(78, -2101.125));
            p0.TTFPoints.Add(new Point(78, -2382.25));
            p0.TTFPoints.Add(new Point(1966.25, -2382.25));
            p0.TTFPoints.Add(new Point(1966.25, -2101.125));
            p0.TTFPoints.Add(new Point(1178.125, -2101.125));
            p0.TTFPoints.Add(new Point(1178.125, 0));
            p0.TTFPoints.Add(new Point(862.875, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('T', c);

            #endregion Character T
        }

        private void InsertUpperCaseU()
        {
            #region Character U

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1873;
            c.BlackBoxY = 2423;
            c.CellIncX = 2403;
            c.CellIncY = 0;
            c.OffsetY = -50;
            c.GlyphOrigin = new Point(262, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(1820, -2382.25));
            p0.TTFPoints.Add(new Point(2135.25, -2382.25));
            p0.TTFPoints.Add(new Point(2135.25, -1005.875));
            p0.TTFPoints.Add(new Point(2130.172, -835.5547));
            p0.TTFPoints.Add(new Point(2114.938, -683.7188));
            p0.TTFPoints.Add(new Point(2089.547, -550.3672));
            p0.TTFPoints.Add(new Point(2054, -435.5));
            p0.TTFPoints.Add(new Point(2005.199, -334.8008));
            p0.TTFPoints.Add(new Point(1940.047, -243.9531));
            p0.TTFPoints.Add(new Point(1858.543, -162.957));
            p0.TTFPoints.Add(new Point(1760.688, -91.8125));
            p0.TTFPoints.Add(new Point(1646.379, -33.87109));
            p0.TTFPoints.Add(new Point(1515.516, 7.515625));
            p0.TTFPoints.Add(new Point(1368.098, 32.34766));
            p0.TTFPoints.Add(new Point(1204.125, 40.625));
            p0.TTFPoints.Add(new Point(1044.367, 33.41406));
            p0.TTFPoints.Add(new Point(899.8438, 11.78125));
            p0.TTFPoints.Add(new Point(770.5547, -24.27344));
            p0.TTFPoints.Add(new Point(656.5, -74.75));
            p0.TTFPoints.Add(new Point(557.6797, -138.8867));
            p0.TTFPoints.Add(new Point(474.0938, -215.9219));
            p0.TTFPoints.Add(new Point(405.7422, -305.8555));
            p0.TTFPoints.Add(new Point(352.625, -408.6875));
            p0.TTFPoints.Add(new Point(312.8125, -527.9727));
            p0.TTFPoints.Add(new Point(284.375, -667.2656));
            p0.TTFPoints.Add(new Point(267.3125, -826.5664));
            p0.TTFPoints.Add(new Point(261.625, -1005.875));
            p0.TTFPoints.Add(new Point(261.625, -2382.25));
            p0.TTFPoints.Add(new Point(576.875, -2382.25));
            p0.TTFPoints.Add(new Point(576.875, -1007.5));
            p0.TTFPoints.Add(new Point(580.4805, -862.5195));
            p0.TTFPoints.Add(new Point(591.2969, -737.9531));
            p0.TTFPoints.Add(new Point(609.3242, -633.8008));
            p0.TTFPoints.Add(new Point(634.5625, -550.0625));
            p0.TTFPoints.Add(new Point(668.5859, -480.7461));
            p0.TTFPoints.Add(new Point(712.9688, -419.8594));
            p0.TTFPoints.Add(new Point(767.7109, -367.4023));
            p0.TTFPoints.Add(new Point(832.8125, -323.375));
            p0.TTFPoints.Add(new Point(907.0039, -288.5391));
            p0.TTFPoints.Add(new Point(989.0156, -263.6563));
            p0.TTFPoints.Add(new Point(1078.848, -248.7266));
            p0.TTFPoints.Add(new Point(1176.5, -243.75));
            p0.TTFPoints.Add(new Point(1337.883, -253.6016));
            p0.TTFPoints.Add(new Point(1474.281, -283.1563));
            p0.TTFPoints.Add(new Point(1585.695, -332.4141));
            p0.TTFPoints.Add(new Point(1672.125, -401.375));
            p0.TTFPoints.Add(new Point(1736.82, -498.3672));
            p0.TTFPoints.Add(new Point(1783.031, -631.7188));
            p0.TTFPoints.Add(new Point(1810.758, -801.4297));
            p0.TTFPoints.Add(new Point(1820, -1007.5));
            p0.TTFPoints.Add(new Point(1820, -2382.25));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('U', c);

            #endregion Character U
        }

        private void InsertUpperCaseV()
        {
            #region Character V

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 2179;
            c.BlackBoxY = 2382;
            c.CellIncX = 2220;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(15, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(937.625, 0));
            p0.TTFPoints.Add(new Point(14.625, -2382.25));
            p0.TTFPoints.Add(new Point(355.875, -2382.25));
            p0.TTFPoints.Add(new Point(975, -651.625));
            p0.TTFPoints.Add(new Point(1010.852, -549.25));
            p0.TTFPoints.Add(new Point(1043.656, -450.125));
            p0.TTFPoints.Add(new Point(1073.414, -354.25));
            p0.TTFPoints.Add(new Point(1100.125, -261.625));
            p0.TTFPoints.Add(new Point(1128.867, -359.125));
            p0.TTFPoints.Add(new Point(1159.844, -456.625));
            p0.TTFPoints.Add(new Point(1193.055, -554.125));
            p0.TTFPoints.Add(new Point(1228.5, -651.625));
            p0.TTFPoints.Add(new Point(1872, -2382.25));
            p0.TTFPoints.Add(new Point(2193.75, -2382.25));
            p0.TTFPoints.Add(new Point(1261, 0));
            p0.TTFPoints.Add(new Point(937.625, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('V', c);

            #endregion Character V
        }

        private void InsertUpperCaseW()
        {
            #region Character W

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 3063;
            c.BlackBoxY = 2382;
            c.CellIncX = 3141;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(41, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(672.75, 0));
            p0.TTFPoints.Add(new Point(40.625, -2382.25));
            p0.TTFPoints.Add(new Point(364, -2382.25));
            p0.TTFPoints.Add(new Point(726.375, -820.625));
            p0.TTFPoints.Add(new Point(754.6094, -698.1406));
            p0.TTFPoints.Add(new Point(780.8125, -576.0625));
            p0.TTFPoints.Add(new Point(804.9844, -454.3906));
            p0.TTFPoints.Add(new Point(827.125, -333.125));
            p0.TTFPoints.Add(new Point(867.9531, -503.8516));
            p0.TTFPoints.Add(new Point(899.4375, -634.1563));
            p0.TTFPoints.Add(new Point(921.5781, -724.0391));
            p0.TTFPoints.Add(new Point(934.375, -773.5));
            p0.TTFPoints.Add(new Point(1387.75, -2382.25));
            p0.TTFPoints.Add(new Point(1768, -2382.25));
            p0.TTFPoints.Add(new Point(2109.25, -1176.5));
            p0.TTFPoints.Add(new Point(2168.969, -955.6016));
            p0.TTFPoints.Add(new Point(2219.75, -741.4063));
            p0.TTFPoints.Add(new Point(2261.594, -533.9141));
            p0.TTFPoints.Add(new Point(2294.5, -333.125));
            p0.TTFPoints.Add(new Point(2318.977, -450.2266));
            p0.TTFPoints.Add(new Point(2346.906, -575.6563));
            p0.TTFPoints.Add(new Point(2378.289, -709.4141));
            p0.TTFPoints.Add(new Point(2413.125, -851.5));
            p0.TTFPoints.Add(new Point(2786.875, -2382.25));
            p0.TTFPoints.Add(new Point(3103.75, -2382.25));
            p0.TTFPoints.Add(new Point(2450.5, 0));
            p0.TTFPoints.Add(new Point(2146.625, 0));
            p0.TTFPoints.Add(new Point(1644.5, -1815.125));
            p0.TTFPoints.Add(new Point(1616.063, -1917.906));
            p0.TTFPoints.Add(new Point(1594.125, -1998.75));
            p0.TTFPoints.Add(new Point(1578.688, -2057.656));
            p0.TTFPoints.Add(new Point(1569.75, -2094.625));
            p0.TTFPoints.Add(new Point(1551.367, -2015.609));
            p0.TTFPoints.Add(new Point(1533.594, -1942.688));
            p0.TTFPoints.Add(new Point(1516.43, -1875.859));
            p0.TTFPoints.Add(new Point(1499.875, -1815.125));
            p0.TTFPoints.Add(new Point(994.5, 0));
            p0.TTFPoints.Add(new Point(672.75, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('W', c);

            #endregion Character W
        }

        private void InsertUpperCaseX()
        {
            #region Character X

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 2184;
            c.BlackBoxY = 2382;
            c.CellIncX = 2220;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(15, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(14.625, 0));
            p0.TTFPoints.Add(new Point(936, -1241.5));
            p0.TTFPoints.Add(new Point(123.5, -2382.25));
            p0.TTFPoints.Add(new Point(498.875, -2382.25));
            p0.TTFPoints.Add(new Point(931.125, -1771.25));
            p0.TTFPoints.Add(new Point(993.6875, -1681.672));
            p0.TTFPoints.Add(new Point(1046.5, -1603.063));
            p0.TTFPoints.Add(new Point(1089.563, -1535.422));
            p0.TTFPoints.Add(new Point(1122.875, -1478.75));
            p0.TTFPoints.Add(new Point(1164.516, -1544.461));
            p0.TTFPoints.Add(new Point(1209.813, -1611.594));
            p0.TTFPoints.Add(new Point(1258.766, -1680.148));
            p0.TTFPoints.Add(new Point(1311.375, -1750.125));
            p0.TTFPoints.Add(new Point(1790.75, -2382.25));
            p0.TTFPoints.Add(new Point(2133.625, -2382.25));
            p0.TTFPoints.Add(new Point(1296.75, -1259.375));
            p0.TTFPoints.Add(new Point(2198.625, 0));
            p0.TTFPoints.Add(new Point(1808.625, 0));
            p0.TTFPoints.Add(new Point(1209, -849.875));
            p0.TTFPoints.Add(new Point(1183.609, -887.25));
            p0.TTFPoints.Add(new Point(1157.813, -926.25));
            p0.TTFPoints.Add(new Point(1131.609, -966.875));
            p0.TTFPoints.Add(new Point(1105, -1009.125));
            p0.TTFPoints.Add(new Point(1068.031, -949.2031));
            p0.TTFPoints.Add(new Point(1036.75, -899.4375));
            p0.TTFPoints.Add(new Point(1011.156, -859.8281));
            p0.TTFPoints.Add(new Point(991.25, -830.375));
            p0.TTFPoints.Add(new Point(393.25, 0));
            p0.TTFPoints.Add(new Point(14.625, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('X', c);

            #endregion Character X
        }

        private void InsertUpperCaseY()
        {
            #region Character Y

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 2184;
            c.BlackBoxY = 2382;
            c.CellIncX = 2220;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(10, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(927.875, 0));
            p0.TTFPoints.Add(new Point(927.875, -1009.125));
            p0.TTFPoints.Add(new Point(9.75, -2382.25));
            p0.TTFPoints.Add(new Point(393.25, -2382.25));
            p0.TTFPoints.Add(new Point(862.875, -1664));
            p0.TTFPoints.Add(new Point(926.7578, -1563.25));
            p0.TTFPoints.Add(new Point(988.4063, -1462.5));
            p0.TTFPoints.Add(new Point(1047.82, -1361.75));
            p0.TTFPoints.Add(new Point(1105, -1261));
            p0.TTFPoints.Add(new Point(1161.469, -1357.383));
            p0.TTFPoints.Add(new Point(1223.625, -1459.656));
            p0.TTFPoints.Add(new Point(1291.469, -1567.82));
            p0.TTFPoints.Add(new Point(1365, -1681.875));
            p0.TTFPoints.Add(new Point(1826.5, -2382.25));
            p0.TTFPoints.Add(new Point(2193.75, -2382.25));
            p0.TTFPoints.Add(new Point(1243.125, -1009.125));
            p0.TTFPoints.Add(new Point(1243.125, 0));
            p0.TTFPoints.Add(new Point(927.875, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('Y', c);

            #endregion Character Y
        }

        private void InsertUpperCaseZ()
        {
            #region Character Z

            OoXmlCharacter c = new OoXmlCharacter();
            c.BlackBoxX = 1883;
            c.BlackBoxY = 2382;
            c.CellIncX = 2033;
            c.CellIncY = 0;
            c.GlyphOrigin = new Point(67, -2382);
            OoXmlPolygon p0 = new OoXmlPolygon();
            p0.TTFPoints.Add(new Point(66.625, 0));
            p0.TTFPoints.Add(new Point(66.625, -292.5));
            p0.TTFPoints.Add(new Point(1287, -1818.375));
            p0.TTFPoints.Add(new Point(1351.188, -1896.984));
            p0.TTFPoints.Add(new Point(1413.75, -1970.313));
            p0.TTFPoints.Add(new Point(1474.688, -2038.359));
            p0.TTFPoints.Add(new Point(1534, -2101.125));
            p0.TTFPoints.Add(new Point(204.75, -2101.125));
            p0.TTFPoints.Add(new Point(204.75, -2382.25));
            p0.TTFPoints.Add(new Point(1911, -2382.25));
            p0.TTFPoints.Add(new Point(1911, -2101.125));
            p0.TTFPoints.Add(new Point(573.625, -448.5));
            p0.TTFPoints.Add(new Point(429, -281.125));
            p0.TTFPoints.Add(new Point(1950, -281.125));
            p0.TTFPoints.Add(new Point(1950, 0));
            p0.TTFPoints.Add(new Point(66.625, 0));
            c.OoXmlPolygons.Add(p0);
            m_OoXmlCharacters.Add('Z', c);

            #endregion Character Z
        }

        #endregion Upper Case
    }
}