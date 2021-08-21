using System;
namespace ConsoleFractale
{
    public class Point
    {

        public decimal X;
        public decimal Y;

        public Point(decimal X, decimal Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
}
