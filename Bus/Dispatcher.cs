using System;
using System.Diagnostics;

namespace BlazorSvgChart.Bus
{
    public class Dispatcher
    {
        private SystemBus SystemBus;
        private MessageBus MessageBus;
        private Registry Registry;

        public Dispatcher(SystemBus systemBus, MessageBus messageBus, Registry registry)
        {
            SystemBus = systemBus;
            MessageBus = messageBus;
            Registry = registry;
        }

        public void Dispatch(int milliseconds)
        {
            Stopwatch sw = Stopwatch.StartNew();

            //Console.WriteLine($"Dispatch: {milliseconds} ms");
            while(sw.ElapsedMilliseconds < milliseconds)
            {
                //Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}");
                // read system messages and send to destination mailboxes
                var msg = SystemBus.Dequeue();
                // TODO: messagebus is still primitive. We need to check for null!
                if (msg != null) 
                {
                    // look up destination actor instance
                    var registration = Registry.Find(msg.To);
                    var destination = registration.Address;
                    if (destination.IsRemote)
                    {
                        // TODO: route to remote destination

                        continue;
                    }
                    
                    var actor = Registry.FindActor(registration.Id);
                    actor.TellSystem(msg);
                }
                else{
                    break;
                }
            }

            while(sw.ElapsedMilliseconds < milliseconds)
            {
                // read remaining messages and send to destination mailboxes
                var msg = MessageBus.Dequeue();
                // TODO: messagebus is still primitive. We need to check for null!
                if (msg != null) 
                {
                    // look up destination actor instance
                    var registration = Registry.Find(msg.To);
                    var destination = registration.Address;
                    if (destination.IsRemote)
                    {
                        // TODO: route to remote destination

                        continue;
                    }
                    
                    var actor = Registry.FindActor(registration.Id);
                    actor.Tell(msg);
                }
                else
                {
                    break;
                }

            }
        }
    }
}