using System;
using System.Threading.Tasks;
using System.Diagnostics;
using BlazorSvgChart.Common;

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
            //root = new Actor(new State(), "root");
            root = Actor.Create(new State(), "root");
            registry = new Registry(root);
            systemBus = new SystemBus();
            messageBus = new MessageBus();
            dispatcher = new Dispatcher(systemBus, messageBus, registry);
        }

        public void LoadInitialState(string actorSystemJson)
        {
            // TODO: Currently ignoring Json. Hardcode two rectangle actors

            // Add two actors to the root
            var actor1 = Actor.Create(new State(), "rect1");
            var actor2 = Actor.Create(new State(), "rect2");
            registry.Add(actor1);
            registry.Add(actor2);
        }

        // Forward all the messages waiting to their destinations
        public void DispatchMessages(int budgetMs)
        {
            dispatcher.Dispatch(budgetMs);
        }

        // Tell all actors to process their inbox
        public async Task Process(double progressMs, long budgetMs)
        {
            Stopwatch sw = Stopwatch.StartNew();

            var allActors = registry.AllActors();
            foreach(var actor in allActors)
            {
                //Console.WriteLine($"ActorSystem.Process ElapsedMs: {sw.ElapsedMilliseconds} of {milliseconds}");
                if (sw.ElapsedMilliseconds > budgetMs)
                {
                    Console.WriteLine($"Warning: time budget {budgetMs} ms exceeded for Processing. Aborting");
                    return;
                }
                // TODO: Check actor state before scheduling it to process)
                var budgetRemaining = budgetMs - sw.ElapsedMilliseconds;
                if (budgetRemaining < 0)
                {
                    // TODO: Warn somehow?
                    return;
                }
                await actor.ProcessMessages(progressMs, budgetRemaining);
            }
        }

        // public async Task Render(long milliseconds)
        // {
        //     return Task.FromResult()
        // }
    }
}