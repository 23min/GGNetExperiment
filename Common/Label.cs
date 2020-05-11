namespace BlazorSvgChart.Common
{
    public class Label : IShape
    {
        public string Id {get;set;}
        public int Width {get;set;}
        public int Height {get;set;}
        public int X {get;set;}
        public int Y {get;set;}

        public string Text {get;set;}

        public void Update(double milliseconds)
        {
        }

        public string Render()
        {
            return Text;
        }
    }
}