using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class PlanObjective
    {
        public PlanObjective()
        {
            this.ObjectiveResources = new List<ObjectiveResource>();
        }

        public int ID { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public int responsible { get; set; }
        public int planID { get; set; }
        public string actions { get; set; }
        public string name { get; set; }
        public Nullable<int> createUser { get; set; }
        public virtual ICollection<ObjectiveResource> ObjectiveResources { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual Position Position { get; set; }
    }
}
