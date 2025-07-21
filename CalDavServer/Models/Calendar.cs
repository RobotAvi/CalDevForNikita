using System;
using System.Collections.Generic;

namespace CalDavServer.Models
{
    public class Calendar
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<AclEntry> AclEntries { get; set; } // ACL записи
    }
}