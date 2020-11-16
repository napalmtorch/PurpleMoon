using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoonV2
{
    public class Point
    {
        // values
        public int X, Y;

        // constructors
        public Point() { this.X = 0; this.Y = 0; }
        public Point(int val) { this.X = val; this.Y = val; }
        public Point(int x, int y) { this.X = x; this.Y = y; }

        // constants
        public static Point Zero { get { return new Point(0); } }
        public static Point One { get { return new Point(1); } }
    }
}
