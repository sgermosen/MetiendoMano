namespace Gitter.Models
{
    public class Room
    {
        public string githubType { get; set; }

        public string id { get; set; }

        public string lastAccessTime { get; set; }

        public bool lurk { get; set; }

        public int mentions { get; set; }

        public string name { get; set; }

        public bool oneToOne { get; set; }

        public string security { get; set; }

        public string topic { get; set; }

        public int unreadItems { get; set; }

        public string uri { get; set; }

        public string url { get; set; }

        public User user { get; set; }

        public int? userCount { get; set; }

        public int? v { get; set; }
    }
}