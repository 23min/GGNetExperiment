using System;

namespace BlazorSvgChart.Bus
{
    public class Message{

        public string From {get; private set;}
        public string To {get; private set;}
        public string Body {get; private set;}
        public DateTime Timestamp {get; private set;}
        public bool SystemMessage {get; private set;}
    }
}
