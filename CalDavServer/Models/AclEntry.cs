using System;

namespace CalDavServer.Models
{
    public class AclEntry
    {
        public Guid Id { get; set; }
        public Guid CalendarId { get; set; }
        public Calendar Calendar { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Role { get; set; } // Например: owner, read, write, delegate
    }
}