using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class AuditPlanResponsible
    {
        public int ID { get; set; }
        public int responsible { get; set; }
        public int auditPlanID { get; set; }
        public virtual User User { get; set; }
    }
}
