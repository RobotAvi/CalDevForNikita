using System;
using System.Collections.Generic;
using CalDavServer.Models;

namespace CalDavServer.Services
{
    public class EventService
    {
        public IEnumerable<Event> GetAll() => new List<Event>();
        public Event Get(Guid id) => new Event { Id = id, Summary = "Test Event" };
        public void Create(Event ev) { }
        public void Update(Guid id, Event ev) { }
        public void Delete(Guid id) { }
    }
}