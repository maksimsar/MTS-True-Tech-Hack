using System;

namespace MTSTrueTechHack.Models
{
    public class Message
    {
        public int ID { get; set; }
        public int SchemaID { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsFromUser { get; set; }
        public DateTime Timestamp { get; set; }

        public Schema Schema { get; set; } = null!;
    }
}