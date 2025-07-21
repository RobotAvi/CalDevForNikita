using System;

namespace CalDavServer.DTOs
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Summary { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}