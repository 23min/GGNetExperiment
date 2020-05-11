using System;
using BlazorSvgChart.Common;

namespace BlazorSvgChart.Bus
{
    public enum ContainerBehavior
    {
        WRAP,
        STICK,
        BOUNCE
    }

    [Flags]
    public enum TranslationResultEvents
    {

    }

    public static class Translations
    {
        public static (int X, int Y, Vector Direction) NextPosition(double milliseconds, Rectangle bounds, ContainerBehavior containerBehaviour, Vector direction, int x, int y)
        {
            x = (int) Math.Round((double)x + (milliseconds/10) * direction.X);
            y = (int) Math.Round((double)y + (milliseconds/10) * direction.Y);

            switch (containerBehaviour)
            {
                case ContainerBehavior.BOUNCE:
                    if (x < 0 || x > bounds.X) direction = new Vector(-direction.X, direction.Y);
                    if (y < 0 || y > bounds.Y) direction = new Vector(direction.X, -direction.Y);
                    break;

                case ContainerBehavior.STICK:
                    direction = new Vector(0,0);
                    break;

                case ContainerBehavior.WRAP:
                    if (x < 0) x=1000;
                    if (y > bounds.X) x = 0;
                    if (y < 0) y=1000;
                    if (y > bounds.Y) y = 0;
                    break;                
            }

            return (x, y, direction);
        }
    }
}
