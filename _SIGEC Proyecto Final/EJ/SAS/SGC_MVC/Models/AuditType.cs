using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class AuditType
    {
        public AuditType()
        {
            this.Audits = new List<Audit>();
        }

        public int ID { get; set; }
        public string nombre { get; set; }
        public int companyID { get; set; }
        public virtual ICollection<Audit> Audits { get; set; }
        public virtual Company Company { get; set; }
    }
}
