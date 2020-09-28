using System.Collections.Generic;

namespace Gitter.Models
{
    public class Message
    {
        public object editedAt { get; set; }

        public User fromUser { get; set; }

        public string html { get; set; }

        public string id { get; set; }

        public List<object> issues { get; set; }

        public List<object> mentions { get; set; }

        public int readBy { get; set; }

        public string sent { get; set; }

        public string text { get; set; }

        public bool unread { get; set; }

        public List<object> urls { get; set; }

        public int v { get; set; }
    }
}