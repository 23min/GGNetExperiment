namespace BlazorSvgChart.Common
{
    public interface IShape
    {
        string Id {get;set;}
        int Width {get;set;}
        int Height {get;set;}
        int X {get;set;}
        int Y {get;set;}
        void Update(double Milliseconds);
        string Render();
    }

    public class Shape : IShape
    {
        public string Id {get;set;}
        public int Width {get;set;}
        public int Height {get;set;}
        public int X {get;set;}
        public int Y {get;set;}

        public void Update(double milliseconds)
        {
        }

        public string Render()
        {
            return string.Empty;
        }
    }
}