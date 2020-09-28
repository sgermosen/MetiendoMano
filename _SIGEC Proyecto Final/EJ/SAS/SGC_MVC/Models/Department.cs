using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Department
    {
        public Department()
        {
            this.AuditPlans = new List<AuditPlan>();
            this.InstructionWorks = new List<InstructionWork>();
            this.Users = new List<User>();
            this.Webpages_Roles = new List<Webpages_Roles>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public int companyID { get; set; }
        public virtual ICollection<AuditPlan> AuditPlans { get; set; }
        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<InstructionWork> InstructionWorks { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Webpages_Roles> Webpages_Roles { get; set; }
    }
}
