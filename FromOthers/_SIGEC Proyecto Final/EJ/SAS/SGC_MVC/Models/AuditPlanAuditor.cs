using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class AuditPlanAuditor
    {
        public int ID { get; set; }
        public int auditor { get; set; }
        public int auditPlansID { get; set; }
        public virtual User User { get; set; }
    }
}
