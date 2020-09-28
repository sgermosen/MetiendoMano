using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class HistProcess
    {
        public int processID { get; set; }
        public int ruleID { get; set; }
        public int processTypeID { get; set; }
        public int responsible { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int statusID { get; set; }
        public string target { get; set; }
        public string entries { get; set; }
        public string activities { get; set; }
        public string outputs { get; set; }
        public string customerRequirements { get; set; }
        public string controlMeasures { get; set; }
        public string outputRequirements { get; set; }
        public Nullable<System.DateTime> dateAdded { get; set; }
        public Nullable<int> createUser { get; set; }
        public string changeReason { get; set; }
        public int companyID { get; set; }
        public int ID { get; set; }
        public int version { get; set; }
        public virtual Process Process { get; set; }
        public virtual User User { get; set; }
    }
}
