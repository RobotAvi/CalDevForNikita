using System;

namespace CalDavServer.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public Guid CalendarId { get; set; }
        public Calendar Calendar { get; set; }
        public string IcsData { get; set; } // Сырые данные iCalendar
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
    }
}