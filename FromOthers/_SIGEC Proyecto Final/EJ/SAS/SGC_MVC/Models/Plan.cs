using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Plan
    {
        public Plan()
        {
            this.PlanObjectives = new List<PlanObjective>();
        }

        public int ID { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int responsible { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public int companyID { get; set; }
        public Nullable<int> createUser { get; set; }
        public int processID { get; set; }
        public int version { get; set; }
        public virtual Company Company { get; set; }
        public virtual Process Process { get; set; }
        public virtual ICollection<PlanObjective> PlanObjectives { get; set; }
        public virtual Position Position { get; set; }
        public virtual User User { get; set; }
    }
}
