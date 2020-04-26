using System;
using System.Collections;
using System.Collections.Generic;

namespace BlazorSvgChart.Bus
{
    // Every actor is listed in the registry 
    // address = route to actor instance
    // instance = unique Id 
    // An actor has exactly one parent ant 0 or more children

    public class Registry
    {
        private Dictionary<string, Registration> registrations = new Dictionary<string, Registration>();
        private Dictionary<string, Actor> actors = new Dictionary<string, Actor>();

        public Registry(Actor rootActor)
        {
            var root = new Registration(rootActor.Id);
            Add(rootActor, root);
        }

        public Registration Add(Actor actor)
        {
            var registration = new Registration(actor.Id);
            Add(actor, registration);
            return registration;
        }
        public Registration Add(Actor actor, string parent)
        {
            var parentRegistration = registrations[parent];
            var registration = new Registration(actor.Id, parentRegistration);
            Add(actor, registration);
            return registration;
        }
        public Registration AddRemote(Actor actor, string remoteAddress)
        {
            var registration = new Registration(actor.Id, remoteAddress);
            Add(actor, registration);
            return registration;
        }

        private void Add(Actor a, Registration r)
        {
            registrations.Add(r.Id, r);
            actors.Add(a.Id, a);
        }

        public Registration Find(string id)
        {
            return registrations[id];
        }

        public Actor FindActor(string id)
        {
            return actors[id];
        }

        public IEnumerable<Actor> AllActors()
        {
            return actors.Values;
        }
    }
    
    public class Registration
    {
        //public List<string> ChildrenAddresses = new List<string>();
        public Address Address {get; private set;}
        public string Id {get; private set;}
        public Action SystemMessages {get; private set;}
        public Action Messages {get; private set;}
        //public string ParentAddress {get; private set;}

        // local, root object, assume parent
        public Registration(string id)
        {
            Id = id;
            Address = new Address(@$"local:{id}");
        }

        public Registration(string id, Registration parent)
        {
            Id = id;
            Address = new Address($"local:{parent.Id}/{id}");
        }

        // remote
        public Registration(string id, string remoteLocation)
        {
            Id = id;
            Address = new Address($"{remoteLocation}/{id}", true);
        }

    }

    public class Address 
    {
        // TODO: For now, remote paths are just a string which we will parse in the dispatcher
        public string Path {get; private set;}
        public bool IsRemote {get; private set;}
        
        public Address(string path, bool isRemote = false)
        {
            Path = path;
            IsRemote = isRemote;
        }

    }

}