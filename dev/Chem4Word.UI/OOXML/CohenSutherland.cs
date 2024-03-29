﻿using System.Windows;

namespace Chem4Word.UI.OOXML
{
    public static class CohenSutherland
    {
        // Fixed with help from https://gist.github.com/oliverheilig/7777382 http://jsil.org/try/#7717256

        public const int Left = 1;
        public const int Right = 2;
        public const int Top = 8;
        public const int Bottom = 4;

        private const double epsilon = 1e-4;

        public static int ComputeOutCode(Rect rect, double x, double y)
        {
            int code = 0;

            if (y - rect.Bottom > epsilon)  // y > rect.Bottom
                code |= Bottom;
            if (rect.Top - y > epsilon)     // y < rect.Top
                code |= Top;
            if (x - rect.Right > epsilon)   // x > rect.Right
                code |= Right;
            if (rect.Left - x > epsilon)    // x < rect.Left
                code |= Left;

            return code;
        }

        public static int ComputeInCode(Rect rect, double x, double y)
        {
            int code = 0;

            if (rect.Bottom - y > epsilon)  // y < rect.Bottom
                code |= Bottom;
            if (y - rect.Top > epsilon)     // y > rect.Top
                code |= Top;
            if (rect.Right - x > epsilon)   // x < rect.Right
                code |= Right;
            if (x - rect.Left > epsilon)    // x > rect.Left
                code |= Left;

            return code;
        }

        public static bool ClipLine(Rect rect, ref Point a, ref Point b, out int attempts)
        {
            double x1 = a.X, x2 = b.X, y1 = a.Y, y2 = b.Y;
            double xmin = rect.Left, xmax = rect.Right, ymin = rect.Top, ymax = rect.Bottom;

            int outCode1 = ComputeOutCode(rect, x1, y1);
            int outCode2 = ComputeOutCode(rect, x2, y2);

            bool accept = false, done = false;
            attempts = 0;
            while (!done)
            {
                // Trivial accept, both points inside rectangle
                if ((outCode1 | outCode2) == 0)
                    done = accept = true;
                // Trivial reject, both points outside rectangle and cannot cross it
                else if ((outCode1 & outCode2) > 0)
                    done = true;
                else
                {
                    // Calculate the line segment to clip from an outside point to an intersection with clip edge
                    double x = 0, y = 0;
                    // At least one endpoint is outside the clip rectangle; pick it.
                    int outCodeOut = outCode1 != 0 ? outCode1 : outCode2;

                    // Now find the intersection point;
                    //  use formulas y = y0 + slope * (x - x0), x = x0 + (1/slope)* (y - y0)
                    if ((outCodeOut & Top) > 0)
                    {
                        x = x1 + (x2 - x1) * (ymin - y1) / (y2 - y1);
                        y = ymin;
                    }
                    else if ((outCodeOut & Bottom) > 0)
                    {
                        x = x1 + (x2 - x1) * (ymax - y1) / (y2 - y1);
                        y = ymax;
                    }
                    else if ((outCodeOut & Right) > 0)
                    {
                        y = y1 + (y2 - y1) * (xmax - x1) / (x2 - x1);
                        x = xmax;
                    }
                    else if ((outCodeOut & Left) > 0)
                    {
                        y = y1 + (y2 - y1) * (xmin - x1) / (x2 - x1);
                        x = xmin;
                    }

                    // Now we move outside point to intersection point to clip and get ready for next pass.
                    if (outCodeOut == outCode1)
                        outCode1 = ComputeOutCode(rect, x1 = x, y1 = y);
                    else
                        outCode2 = ComputeOutCode(rect, x2 = x, y2 = y);
                }

                attempts++;
                if (attempts > 15)
                {
                    done = true;
                }
            }

            if (accept)
            {
                a.X = x1;
                a.Y = y1;
                b.X = x2;
                b.Y = y2;
            }

            return accept;
        }
    }
}
