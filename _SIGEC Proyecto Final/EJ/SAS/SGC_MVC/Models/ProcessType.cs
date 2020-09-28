using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class ProcessType
    {
        public ProcessType()
        {
            this.AuditPlans = new List<AuditPlan>();
            this.AuditProcesses = new List<AuditProcess>();
            this.Processes = new List<Process>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public int companyID { get; set; }
        public string description { get; set; }
        public virtual ICollection<AuditPlan> AuditPlans { get; set; }
        public virtual ICollection<AuditProcess> AuditProcesses { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
    }
}
