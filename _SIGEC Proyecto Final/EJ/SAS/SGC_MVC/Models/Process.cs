using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Process
    {
        public Process()
        {
            this.AuditPlans = new List<AuditPlan>();
            this.Forms = new List<Form>();
            this.HistProcesses = new List<HistProcess>();
            this.Indicators = new List<Indicator>();
            this.InstructionWorks = new List<InstructionWork>();
            this.Plans = new List<Plan>();
            this.Procedures = new List<Procedure>();
            this.ProcessDocuments = new List<ProcessDocument>();
            this.Entities = new List<Entity>();
            this.Documents = new List<Document>();
            this.Subcategories = new List<Subcategory>();
            this.Rules = new List<Rule>();
        }

        public int ID { get; set; }
        public int ruleID { get; set; }
        public int processTypeID { get; set; }
        public int responsible { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int statusID { get; set; }
        public string target { get; set; }
        public string entries { get; set; }
        public string activities { get; set; }
        public string outputs { get; set; }
        public string customerRequirements { get; set; }
        public string controlMeasures { get; set; }
        public string outputRequirements { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public int companyID { get; set; }
        public int version { get; set; }
        public virtual ICollection<AuditPlan> AuditPlans { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Form> Forms { get; set; }
        public virtual ICollection<HistProcess> HistProcesses { get; set; }
        public virtual ICollection<Indicator> Indicators { get; set; }
        public virtual ICollection<InstructionWork> InstructionWorks { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual Position Position { get; set; }
        public virtual ICollection<Procedure> Procedures { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ProcessDocument> ProcessDocuments { get; set; }
        public virtual ProcessType ProcessType { get; set; }
        public virtual ICollection<Entity> Entities { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Subcategory> Subcategories { get; set; }
        public virtual ICollection<Rule> Rules { get; set; }
    }
}
