using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class HistRule
    {
        public int ruleID { get; set; }
        public int documentID { get; set; }
        public int version { get; set; }
        public string description { get; set; }
        public string userAdd { get; set; }
        public System.DateTime dateAdded { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public int ID { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public bool status { get; set; }
        public virtual Document Document { get; set; }
        public virtual Rule Rule { get; set; }
        public virtual User User { get; set; }
    }
}
