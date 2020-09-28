using System;
using System.Collections.Generic;

namespace Premy.Chatovatko.Server.Database.Models
{
    public partial class Logs
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Class { get; set; }
        public bool Error { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public string Source { get; set; }
    }
}
