using System;
using System.Collections.Generic;

namespace CalDavServer.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public ICollection<Calendar> Calendars { get; set; }
    }
}