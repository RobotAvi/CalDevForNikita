using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CalDavServer.Models;
using CalDavServer.Data;

namespace CalDavServer.Services
{
    public class EventService
    {
        private readonly ApplicationDbContext _db;
        public EventService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Event> GetAll() => _db.Events.Include(e => e.Calendar).ToList();
        public Event Get(Guid id) => _db.Events.Include(e => e.Calendar).FirstOrDefault(e => e.Id == id);
        public void Create(Event ev)
        {
            _db.Events.Add(ev);
            _db.SaveChanges();
        }
        public void Update(Guid id, Event ev)
        {
            var existing = _db.Events.Find(id);
            if (existing == null) return;
            existing.Summary = ev.Summary;
            existing.Description = ev.Description;
            existing.Start = ev.Start;
            existing.End = ev.End;
            existing.IcsData = ev.IcsData;
            existing.CalendarId = ev.CalendarId;
            _db.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var ev = _db.Events.Find(id);
            if (ev == null) return;
            _db.Events.Remove(ev);
            _db.SaveChanges();
        }
    }
}