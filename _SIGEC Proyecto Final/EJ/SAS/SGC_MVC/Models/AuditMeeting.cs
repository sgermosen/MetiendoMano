using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class AuditMeeting
    {
        public AuditMeeting()
        {
            this.AuditMeetingParticipants = new List<AuditMeetingParticipant>();
        }

        public int ID { get; set; }
        public int auditID { get; set; }
        public Nullable<bool> isOpening { get; set; }
        public virtual Audit Audit { get; set; }
        public virtual ICollection<AuditMeetingParticipant> AuditMeetingParticipants { get; set; }
    }
}
