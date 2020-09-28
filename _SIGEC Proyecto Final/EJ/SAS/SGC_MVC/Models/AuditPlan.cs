using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class AuditPlan
    {
        public int ID { get; set; }
        public int auditID { get; set; }
        public string name { get; set; }
        public int responsible { get; set; }
        public string place { get; set; }
        public System.DateTime fromDate { get; set; }
        public System.DateTime toDate { get; set; }
        public bool isProcess { get; set; }
        public int ruleID { get; set; }
        public int processTypeID { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public int departmentID { get; set; }
        public int companyID { get; set; }
        public int processID { get; set; }
        public virtual Audit Audit { get; set; }
        public virtual Company Company { get; set; }
        public virtual Process Process { get; set; }
        public virtual User User { get; set; }
        public virtual Department Department { get; set; }
        public virtual ProcessType ProcessType { get; set; }
    }
}
