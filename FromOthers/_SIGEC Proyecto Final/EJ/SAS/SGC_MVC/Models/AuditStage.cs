using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class AuditStage
    {
        public AuditStage()
        {
            this.Audits = new List<Audit>();
        }

        public int ID { get; set; }
        public string nombre { get; set; }
        public virtual ICollection<Audit> Audits { get; set; }
    }
}
