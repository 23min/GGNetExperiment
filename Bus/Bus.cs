using System;
using System.Collections.Concurrent;

namespace BlazorSvgChart.Bus
{
    public class Bus
    {
        private readonly ConcurrentQueue<Message> messages = new ConcurrentQueue<Message>();
        
        public Message Dequeue()
        {
            if (messages.TryDequeue(out var message))
            {
                return message;
            }
            else
            {
                // TODO: There should be a safer way to dequeue and send but for now we do the simlest thing. Nothing.
                return null;
            }
        }
    }
}