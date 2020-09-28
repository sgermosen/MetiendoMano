using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Position
    {
        public Position()
        {
            this.Documents = new List<Document>();
            this.Indicators = new List<Indicator>();
            this.Indicators1 = new List<Indicator>();
            this.Plans = new List<Plan>();
            this.PlanObjectives = new List<PlanObjective>();
            this.Procedures = new List<Procedure>();
            this.Processes = new List<Process>();
            this.Users = new List<User>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public int companyID { get; set; }
        public string description { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Indicator> Indicators { get; set; }
        public virtual ICollection<Indicator> Indicators1 { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual ICollection<PlanObjective> PlanObjectives { get; set; }
        public virtual ICollection<Procedure> Procedures { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
