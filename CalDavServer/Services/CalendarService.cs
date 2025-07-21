using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CalDavServer.Models;
using CalDavServer.Data;

namespace CalDavServer.Services
{
    public class CalendarService
    {
        private readonly ApplicationDbContext _db;
        public CalendarService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Calendar> GetAll() => _db.Calendars.Include(c => c.Owner).Include(c => c.Events).ToList();
        public Calendar Get(Guid id) => _db.Calendars.Include(c => c.Owner).Include(c => c.Events).FirstOrDefault(c => c.Id == id);
        public void Create(Calendar calendar)
        {
            _db.Calendars.Add(calendar);
            _db.SaveChanges();
        }
        public void Update(Guid id, Calendar calendar)
        {
            var existing = _db.Calendars.Find(id);
            if (existing == null) return;
            existing.Name = calendar.Name;
            existing.OwnerId = calendar.OwnerId;
            _db.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var calendar = _db.Calendars.Find(id);
            if (calendar == null) return;
            _db.Calendars.Remove(calendar);
            _db.SaveChanges();
        }
    }
}