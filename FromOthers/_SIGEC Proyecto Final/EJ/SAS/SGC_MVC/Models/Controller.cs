using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Controller
    {
        public Controller()
        {
            this.Actions = new List<Action>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string displayName { get; set; }
        public virtual ICollection<Action> Actions { get; set; }
    }
}
