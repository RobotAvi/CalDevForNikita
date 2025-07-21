using System;
using System.Collections.Generic;
using CalDavServer.Models;

namespace CalDavServer.Services
{
    public class CalendarService
    {
        public IEnumerable<Calendar> GetAll() => new List<Calendar>();
        public Calendar Get(Guid id) => new Calendar { Id = id, Name = "Test Calendar" };
        public void Create(Calendar calendar) { }
        public void Update(Guid id, Calendar calendar) { }
        public void Delete(Guid id) { }
    }
}