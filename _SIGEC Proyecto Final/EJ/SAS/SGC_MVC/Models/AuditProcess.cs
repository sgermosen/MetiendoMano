using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class AuditProcess
    {
        public int ID { get; set; }
        public int auditID { get; set; }
        public int processID { get; set; }
        public int documentID { get; set; }
        public System.DateTime date { get; set; }
        public int ruleID { get; set; }
        public int processTypeID { get; set; }
        public virtual Audit Audit { get; set; }
        public virtual Document Document { get; set; }
        public virtual ProcessType ProcessType { get; set; }
    }
}
