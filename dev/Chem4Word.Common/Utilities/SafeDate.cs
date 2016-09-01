using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chem4Word.Common.Utilities
{
    public static class SafeDate
    {
        public static DateTime Parse(string input)
        {
            DateTime result = DateTime.MinValue;

            string[] parts = input.Split('-');

            if (parts.Length == 3)
            {
                int day = Int32.Parse(parts[0]);
                int month = 0;
                switch (parts[1].ToLower())
                {
                    case "jan":
                        month = 1;
                        break;
                    case "feb":
                        month = 2;
                        break;
                    case "mar":
                        month = 3;
                        break;
                    case "apr":
                        month = 4;
                        break;
                    case "may":
                        month = 5;
                        break;
                    case "jun":
                        month = 6;
                        break;
                    case "jul":
                        month = 7;
                        break;
                    case "aug":
                        month = 8;
                        break;
                    case "sep":
                        month = 9;
                        break;
                    case "oct":
                        month = 10;
                        break;
                    case "nov":
                        month = 11;
                        break;
                    case "dec":
                        month = 12;
                        break;
                }
                int year = Int32.Parse(parts[2]);

                result = new DateTime(year, month, day);
            }

            return result;
        }
    }
}
