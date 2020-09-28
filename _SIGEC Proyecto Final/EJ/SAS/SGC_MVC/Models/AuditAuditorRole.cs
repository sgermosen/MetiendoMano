using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class AuditAuditorRole
    {
        public AuditAuditorRole()
        {
            this.AuditAuditors = new List<AuditAuditor>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public virtual ICollection<AuditAuditor> AuditAuditors { get; set; }
    }
}
