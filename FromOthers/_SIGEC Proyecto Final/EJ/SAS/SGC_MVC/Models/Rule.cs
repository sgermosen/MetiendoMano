using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Rule
    {
        public Rule()
        {
            this.HistPolls = new List<HistPoll>();
            this.HistRules = new List<HistRule>();
            this.Indicators = new List<Indicator>();
            this.InstructionWorks = new List<InstructionWork>();
            this.Polls = new List<Poll>();
            this.Procedures = new List<Procedure>();
            this.UserTasks = new List<UserTask>();
            this.Companies = new List<Company>();
            this.Documents = new List<Document>();
            this.Glossaries = new List<Glossary>();
            this.Processes = new List<Process>();
            this.DocumentTypes = new List<DocumentType>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public bool status { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public int documentID { get; set; }
        public virtual ICollection<HistPoll> HistPolls { get; set; }
        public virtual ICollection<HistRule> HistRules { get; set; }
        public virtual ICollection<Indicator> Indicators { get; set; }
        public virtual ICollection<InstructionWork> InstructionWorks { get; set; }
        public virtual ICollection<Poll> Polls { get; set; }
        public virtual ICollection<Procedure> Procedures { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserTask> UserTasks { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Glossary> Glossaries { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
    }
}
