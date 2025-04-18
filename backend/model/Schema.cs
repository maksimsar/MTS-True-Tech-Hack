using System;
using System.Collections.Generic;

namespace MTSTrueTechHack.Models
{
    public class Schema
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string JSONSchema { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; } = null!;
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}