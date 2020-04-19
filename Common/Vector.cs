using System;

namespace BlazorSvgChart.Common
{
    public struct Vector
    {
        private readonly double x;
        private readonly double y;

        public double X => x;
        public double Y => y;

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double Length()
        {
            return Math.Sqrt(x * x + y * y);
        }
    }
}