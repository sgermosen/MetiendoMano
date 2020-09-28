using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class HistProcedure
    {
        public int ID { get; set; }
        public int procedureID { get; set; }
        public int ruleID { get; set; }
        public int processID { get; set; }
        public int responsible { get; set; }
        public string target { get; set; }
        public string procedureScope { get; set; }
        public Nullable<System.DateTime> dateAdd { get; set; }
        public Nullable<int> createUser { get; set; }
        public int companyID { get; set; }
        public string name { get; set; }
        public int statusID { get; set; }
        public string changeReason { get; set; }
        public int version { get; set; }
    }
}
