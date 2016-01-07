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
                switch (parts[1])
                {
                    case "Jan":
                        month = 1;
                        break;
                    case "Feb":
                        month = 2;
                        break;
                    case "Mar":
                        month = 3;
                        break;
                    case "Apr":
                        month = 4;
                        break;
                    case "May":
                        month = 5;
                        break;
                    case "Jun":
                        month = 6;
                        break;
                    case "Jul":
                        month = 7;
                        break;
                    case "Aug":
                        month = 8;
                        break;
                    case "Sep":
                        month = 9;
                        break;
                    case "Oct":
                        month = 10;
                        break;
                    case "Nov":
                        month = 11;
                        break;
                    case "Dec":
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
