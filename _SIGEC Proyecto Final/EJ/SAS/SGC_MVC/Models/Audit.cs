using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Audit
    {
        public Audit()
        {
            this.AuditAuditors = new List<AuditAuditor>();
            this.AuditMeetings = new List<AuditMeeting>();
            this.AuditPlans = new List<AuditPlan>();
            this.AuditProcesses = new List<AuditProcess>();
            this.Documents = new List<Document>();
        }

        public int ID { get; set; }
        public int auditTypeID { get; set; }
        public string name { get; set; }
        public int planningResponsible { get; set; }
        public System.DateTime fromDate { get; set; }
        public System.DateTime toDate { get; set; }
        public string objetive { get; set; }
        public string scope { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public string comments { get; set; }
        public int companyID { get; set; }
        public int stageID { get; set; }
        public virtual AuditStage AuditStage { get; set; }
        public virtual AuditType AuditType { get; set; }
        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<AuditAuditor> AuditAuditors { get; set; }
        public virtual ICollection<AuditMeeting> AuditMeetings { get; set; }
        public virtual ICollection<AuditPlan> AuditPlans { get; set; }
        public virtual ICollection<AuditProcess> AuditProcesses { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
