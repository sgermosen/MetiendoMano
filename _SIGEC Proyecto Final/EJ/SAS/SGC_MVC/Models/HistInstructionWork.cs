using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class HistInstructionWork
    {
        public int ID { get; set; }
        public int instructionWorkID { get; set; }
        public int ruleID { get; set; }
        public int processID { get; set; }
        public int workID { get; set; }
        public string name { get; set; }
        public Nullable<int> responsible { get; set; }
        public int statusID { get; set; }
        public int departmentID { get; set; }
        public Nullable<System.DateTime> dateAdded { get; set; }
        public string changeReason { get; set; }
        public int companyID { get; set; }
        public Nullable<int> createUser { get; set; }
        public int version { get; set; }
        public virtual InstructionWork InstructionWork { get; set; }
    }
}
