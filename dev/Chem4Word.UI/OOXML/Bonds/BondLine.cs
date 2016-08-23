using System;
using System.Windows;

namespace Chem4Word.UI.OOXML.Bonds
{
    public class BondLine
    {
        //public Point StartPoint { get; set; }
        //public Point EndPoint { get; set; }
        public String Type { get; set; }
        public double StartX { get; set; }
        public double StartY { get; set; }
        public double EndX { get; set; }
        public double EndY { get; set; }

        public BondLine(Point startPoint, Point endPoint, String type)
        {
            StartX = startPoint.X;
            StartY = startPoint.Y;
            EndX = endPoint.X;
            EndY = endPoint.Y;
            Type = type;
        }

        public BondLine GetParallel(double offset)
        {
            double xDifference = StartX - EndX;
            double yDifference = StartY - EndY;
            double length = Math.Sqrt(Math.Pow(xDifference, 2) + Math.Pow(yDifference, 2));

            Point newStartPoint = new Point((float)(StartX - offset * yDifference / length),
                                            (float)(StartY + offset * xDifference / length));
            Point newEndPoint = new Point((float)(EndX - offset * yDifference / length),
                                          (float)(EndY + offset * xDifference / length));

            return new BondLine(newStartPoint, newEndPoint, Type);
        }
    }
}
