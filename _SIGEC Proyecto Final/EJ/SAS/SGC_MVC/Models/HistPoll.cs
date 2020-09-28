using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class HistPoll
    {
        public int pollID { get; set; }
        public int ruleID { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> dateAdded { get; set; }
        public Nullable<int> createUser { get; set; }
        public int ID { get; set; }
        public int companyID { get; set; }
        public string code { get; set; }
        public virtual Poll Poll { get; set; }
        public virtual Rule Rule { get; set; }
        public virtual User User { get; set; }
    }
}
