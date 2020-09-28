using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class AuditMeetingParticipant
    {
        public int ID { get; set; }
        public int auditMeetingID { get; set; }
        public int userID { get; set; }
        public virtual AuditMeeting AuditMeeting { get; set; }
        public virtual User User { get; set; }
    }
}
