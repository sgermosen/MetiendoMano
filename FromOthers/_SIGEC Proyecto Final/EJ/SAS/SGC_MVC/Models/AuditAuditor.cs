using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class AuditAuditor
    {
        public int ID { get; set; }
        public int auditID { get; set; }
        public Nullable<int> auditor { get; set; }
        public int auditAuditorRoleID { get; set; }
        public virtual Audit Audit { get; set; }
        public virtual AuditAuditorRole AuditAuditorRole { get; set; }
        public virtual User User { get; set; }
    }
}
