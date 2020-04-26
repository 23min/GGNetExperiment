using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSvgChart.Bus
{
    public class Actor
    {
        private IState State;
        private ConcurrentQueue<Message> Mailbox {get;set;} = new ConcurrentQueue<Message>();
        private ConcurrentQueue<Message> SystemMessages {get;set;} = new ConcurrentQueue<Message>();
        private const int ITERATIONS = 10;

        public string Id {get; private set;}

        // public Action TellSystem(Message message)
        // {
        //     return () => SystemMessages.Append(message);
        // }
        // public Action Tell(Message message)
        // {
        //     return () => Mailbox.Append(message);
        // }
        public void TellSystem(Message message)
        {
            SystemMessages.Append(message);
        }
        public void Tell(Message message)
        {
            Mailbox.Append(message);
        }

        public Actor(IState initialState, string id)
        {
            State = initialState;
            Id = id;
        }

        public async Task ProcessMessages()
        {
            // TODO: Read the messages in the mailbox
            //      process each message
            //      update state

            //Console.WriteLine($"Actor: {Id} processing...");

            int mailboxIterations = ITERATIONS - SystemMessages.Count();

            for(int iter = 1; iter <=ITERATIONS; iter++)
            {
                if (!SystemMessages.TryDequeue(out Message msg)) break;
                // TODO: Process System Message
                Console.WriteLine($"TODO: Process system message {msg}");
            }

            for(int iter = 1; iter <= mailboxIterations; iter++)
            {
                if (!Mailbox.TryDequeue(out Message msg)) break;

                (bool result, IState state) = await ProcessMessage(State, msg);
            }


        }

        private async Task<(bool, IState)> ProcessMessage(IState state, Message message)
        {
            var parts = message.Body.Split(";");
            var (cmd, operation) = (parts[0], parts[1]);
            if (cmd == null || operation == null)
            {
                return await Task.FromResult((false, state));
            }
            // parse message body
            // switch
            // execute 
            Console.WriteLine($"Process: cmd: {cmd}, op: {operation}");
            return await Task.FromResult((true, state));
        }
        
    }

    public interface IState
    {

    }

    public class State : IState
    {
        public State()
        {

        }
    }
}