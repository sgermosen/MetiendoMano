using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Status
    {
        public Status()
        {
            this.Documents = new List<Document>();
            this.DocumentStatus = new List<DocumentStatu>();
            this.Forms = new List<Form>();
            this.InstructionWorks = new List<InstructionWork>();
            this.Procedures = new List<Procedure>();
            this.Processes = new List<Process>();
            this.UserTasks = new List<UserTask>();
            this.Webpages_Roles = new List<Webpages_Roles>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> companyID { get; set; }
        public Nullable<int> createUser { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<DocumentStatu> DocumentStatus { get; set; }
        public virtual ICollection<Form> Forms { get; set; }
        public virtual ICollection<InstructionWork> InstructionWorks { get; set; }
        public virtual ICollection<Procedure> Procedures { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserTask> UserTasks { get; set; }
        public virtual ICollection<Webpages_Roles> Webpages_Roles { get; set; }
    }
}
