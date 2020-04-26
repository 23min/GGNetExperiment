using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BlazorSvgChart.Bus
{
    public class ActorSystem
    {
        private Registry registry;
        private SystemBus systemBus;
        private MessageBus messageBus;
        private Dispatcher dispatcher;
        private Actor root;

        public ActorSystem()
        {
            root = new Actor(new State(), "root");
            registry = new Registry(root);
            systemBus = new SystemBus();
            messageBus = new MessageBus();
            dispatcher = new Dispatcher(systemBus, messageBus, registry);
        }

        public void LoadInitialState(string actorSystemJson)
        {
            // TODO: Currently ignoring Json. Hardcode two rectangle actors

            // Add two actors to the root
            var actor1 = new Actor(new State(), "rect1");
            var actor2 = new Actor(new State(), "rect2");
            registry.Add(actor1);
            registry.Add(actor2);
        }

        public void DispatchMessages(int milliseconds)
        {
            dispatcher.Dispatch(milliseconds);
        }

        // TODO: Add milliseconds timeslice and a deadline
        public async Task Process(long milliseconds)
        {
            Stopwatch sw = Stopwatch.StartNew();

            var allActors = registry.AllActors();
            foreach(var actor in allActors)
            {
                //Console.WriteLine($"ActorSystem.Process ElapsedMs: {sw.ElapsedMilliseconds} of {milliseconds}");
                if (sw.ElapsedMilliseconds > milliseconds)
                {
                    Console.WriteLine($"Warning: time budget {milliseconds} ms exceeded for Processing. Aborting");
                    return;
                }
                // TODO: Check actor state before scheduling it to process)
                await actor.ProcessMessages();
            }
        }
    }
}