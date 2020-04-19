using System;

namespace BlazorSvgChart.Common
{
    public class Rectangle
    {
        public string Id;
        public int Width;
        public int Height; 
        public int X;
        public int Y;
        public (int, int, int) RgbFill;
        public (int, int, int) RgbStroke;

        public Vector Direction;

        private int boundsx = 1000;
        private int boundsy = 1000;

        public void Update(double milliseconds)
        {
            // new position = current position + factor * direction

            X = (int) Math.Round((double)X + (milliseconds/1000) * Direction.X);
            Y = (int) Math.Round((double)Y + (milliseconds/1000) * Direction.Y);

            if (X < 0) X=1000;
            if (X > boundsx) X = 0;
            if (Y < 0) Y=1000;
            if (Y > boundsy) Y = 0;
        }
    }
}