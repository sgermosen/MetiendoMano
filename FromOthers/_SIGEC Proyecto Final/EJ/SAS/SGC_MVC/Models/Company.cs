using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Company
    {
        public Company()
        {
            this.Advertisements = new List<Advertisement>();
            this.Audits = new List<Audit>();
            this.AuditPlans = new List<AuditPlan>();
            this.AuditTypes = new List<AuditType>();
            this.Categories = new List<Category>();
            this.Departments = new List<Department>();
            this.Documents = new List<Document>();
            this.Entities = new List<Entity>();
            this.Forms = new List<Form>();
            this.Glossaries = new List<Glossary>();
            this.Indicators = new List<Indicator>();
            this.Infrastructures = new List<Infrastructure>();
            this.InstructionWorks = new List<InstructionWork>();
            this.Menus = new List<Menu>();
            this.Plans = new List<Plan>();
            this.Polls = new List<Poll>();
            this.Positions = new List<Position>();
            this.Procedures = new List<Procedure>();
            this.Processes = new List<Process>();
            this.ProcessTypes = new List<ProcessType>();
            this.Status1 = new List<Status>();
            this.Templates = new List<Template>();
            this.Users = new List<User>();
            this.UserTasks = new List<UserTask>();
            this.Rules = new List<Rule>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string companyText { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public string email { get; set; }
        public string logo { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
        public virtual ICollection<Audit> Audits { get; set; }
        public virtual ICollection<AuditPlan> AuditPlans { get; set; }
        public virtual ICollection<AuditType> AuditTypes { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Entity> Entities { get; set; }
        public virtual ICollection<Form> Forms { get; set; }
        public virtual ICollection<Glossary> Glossaries { get; set; }
        public virtual ICollection<Indicator> Indicators { get; set; }
        public virtual ICollection<Infrastructure> Infrastructures { get; set; }
        public virtual ICollection<InstructionWork> InstructionWorks { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual ICollection<Poll> Polls { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<Procedure> Procedures { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
        public virtual ICollection<ProcessType> ProcessTypes { get; set; }
        public virtual ICollection<Status> Status1 { get; set; }
        public virtual ICollection<Template> Templates { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<UserTask> UserTasks { get; set; }
        public virtual ICollection<Rule> Rules { get; set; }
    }
}
