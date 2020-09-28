using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Form
    {
        public Form()
        {
            this.FormFields = new List<FormField>();
        }

        public int ID { get; set; }
        public int ruleID { get; set; }
        public int processID { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public int companyID { get; set; }
        public int processTypeID { get; set; }
        public int version { get; set; }
        public int statusID { get; set; }
        public virtual Company Company { get; set; }
        public virtual Process Process { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<FormField> FormFields { get; set; }
        public virtual User User { get; set; }
    }
}
