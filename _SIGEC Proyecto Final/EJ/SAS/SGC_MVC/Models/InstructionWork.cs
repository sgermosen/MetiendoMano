using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class InstructionWork
    {
        public InstructionWork()
        {
            this.HistInstructionWorks = new List<HistInstructionWork>();
            this.Tasks = new List<Task>();
        }

        public int ruleID { get; set; }
        public int processID { get; set; }
        public int ID { get; set; }
        public string name { get; set; }
        public int responsible { get; set; }
        public int statusID { get; set; }
        public int departmentID { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public int companyID { get; set; }
        public int version { get; set; }
        public virtual Company Company { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<HistInstructionWork> HistInstructionWorks { get; set; }
        public virtual Process Process { get; set; }
        public virtual Rule Rule { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
