using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlazorSvgChart.Common;

namespace BlazorSvgChart.Bus
{
    public class Actor
    {
        private IState State;
        private ConcurrentQueue<Message> Mailbox {get;set;} = new ConcurrentQueue<Message>();
        private ConcurrentQueue<Message> SystemMessages {get;set;} = new ConcurrentQueue<Message>();
        private const int ITERATIONS = 10;

        public string Id {get; private set;}

        // public api

        public void TellSystem(Message message)
        {
            SystemMessages.Append(message);
        }
        public void Tell(Message message)
        {
            Mailbox.Append(message);
        }

        // Aspects of the actor. "Is A" relationship. 
        // For example, actor Is A Rectangle
        // Then actor has a Rectangle component
        private List<object> Components = new List<object>();

        private Actor()
        {
            // TODO: Dependency injection? 
        }

        public static Actor Create(IState initialState, string id)
        {
            var actor = new Actor();
            // TODO: The actor can read it's previously stored state from storage.
            actor.State = initialState;
            actor.Id = id;
            return actor;
        }

        private void Serialize()
        {
            // TODO: Save actor's sate
        }

        public async Task ProcessMessages(double progressMs, long budgetMs)
        {
            var sw = Stopwatch.StartNew();

            // TODO: Read the messages in the mailbox
            //      process each message
            //      update state

            //Console.WriteLine($"Actor: {Id} processing...");

            int mailboxIterations = ITERATIONS - SystemMessages.Count();

            // Process System messages

            for(int iter = 1; iter <=ITERATIONS; iter++)
            {
                // TODO: Check time budget

                if (!SystemMessages.TryDequeue(out Message msg)) break;

                // TODO: Process System Message

                Console.WriteLine($"TODO: Process system message {msg}");
            }

            // Advance Time

            // hardcode rectangle that moves
            // For each component that has a time parameter
            foreach(var component in Components)
            {
                // A rectangle has a Direction. If it is not (0,0) then update it
                if (component is Rectangle r)
                {
                    if (!r.Direction.IsNullVector)
                    {
                        r.Update(progressMs);
                    }
                }
            }

            // Process any remaining commands

            for(int iter = 1; iter <= mailboxIterations; iter++)
            {
                var budgetRemaining = budgetMs - sw.ElapsedMilliseconds;
                if (budgetRemaining < 0)
                {
                    // TODO: Warn?
                    return;
                }

                if (!Mailbox.TryDequeue(out Message msg)) break;

                (bool result, IState state) = await ProcessMessage(State, msg, progressMs, budgetRemaining);
            }
        }

        private async Task<(bool, IState)> ProcessMessage(IState state, Message message, double progressMs, long elapsedMilliseconds)
        {

            var (cmd, msg) = ParseMessage(message);
            if (string.IsNullOrEmpty(cmd))
            {
                return await Task.FromResult((false, state));
            }

            // parse message body
            // switch
            // execute 
            Console.WriteLine($"Process: cmd: {cmd}, msg: {msg}");

            switch(cmd)
            {
                case "become":
                    break;
                case "do":
                    // execute 
                    if (string.IsNullOrEmpty(msg))
                    {
                        
                    }
                    else
                    {

                    }
                    break;
                default:
                    break;
            }

            


            return await Task.FromResult((true, state));
        }

        public string Render()
        {
            return State.ToString();
        }

        private (string command, string content) ParseMessage(Message message)
        {
            var parts = message.Body.Split(";");
            return (parts[0], parts[1]);
        }
        
    }


}