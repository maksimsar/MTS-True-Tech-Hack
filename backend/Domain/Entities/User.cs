using System;
using System.Collections.Generic;

namespace MTSTrueTechHack.Domain.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public List<Schema> Schemas { get; set; } = new List<Schema>();
    }
}