using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Task
    {
        public int ID { get; set; }
        public string description { get; set; }
        public string howTo { get; set; }
        public string whyDo { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public int workID { get; set; }
        public int ruleID { get; set; }
        public int processID { get; set; }
        public virtual InstructionWork InstructionWork { get; set; }
        public virtual User User { get; set; }
    }
}
