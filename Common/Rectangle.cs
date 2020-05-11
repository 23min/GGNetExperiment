using System;

namespace BlazorSvgChart.Common
{
    public class Rectangle : IShape
    {
        public string Id {get;set;}
        public int Width {get;set;}
        public int Height {get;set;}
        public int X {get;set;}
        public int Y {get;set;}

        public (int, int, int) RgbFill;
        public (int, int, int) RgbStroke;

        public Vector Direction;

        private int boundsx = 1000;
        private int boundsy = 1000;

        public string Render()
        {
            throw new NotImplementedException();
        }

        public void Update(double milliseconds)
        {
            // new position = current position + factor * direction

            X = (int) Math.Round((double)X + (milliseconds/10) * Direction.X);
            Y = (int) Math.Round((double)Y + (milliseconds/10) * Direction.Y);

            if (X < 0) X=1000;
            if (X > boundsx) X = 0;
            if (Y < 0) Y=1000;
            if (Y > boundsy) Y = 0;
        }
    }
}