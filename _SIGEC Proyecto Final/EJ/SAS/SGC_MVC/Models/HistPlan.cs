using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class HistPlan
    {
        public int ID { get; set; }
        public int planID { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int responsible { get; set; }
        public Nullable<System.DateTime> dateAdded { get; set; }
        public int companyID { get; set; }
        public Nullable<int> createUser { get; set; }
        public int processID { get; set; }
        public int version { get; set; }
        public string changeReason { get; set; }
    }
}
