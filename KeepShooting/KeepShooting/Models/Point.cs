using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; }
        public int Y { get; }
    }

    public static class CCPointExtention
    {
        public static Point ToPoint(this CCPoint point)
        {
            return new Point((int)point.X, (int)point.Y);
        }

        public static CCPoint ToCCPoint(this Point point)
        {
            return new CCPoint(point.X, point.Y);
        }
    }
}
