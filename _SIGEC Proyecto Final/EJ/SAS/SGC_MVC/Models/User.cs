using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class User
    {
        public User()
        {
            this.Advertisements = new List<Advertisement>();
            this.Audits = new List<Audit>();
            this.AuditAuditors = new List<AuditAuditor>();
            this.AuditMeetingParticipants = new List<AuditMeetingParticipant>();
            this.AuditPlanAuditors = new List<AuditPlanAuditor>();
            this.AuditPlanResponsibles = new List<AuditPlanResponsible>();
            this.AuditPlans = new List<AuditPlan>();
            this.Categories = new List<Category>();
            this.Dashboards = new List<Dashboard>();
            this.Departments = new List<Department>();
            this.Documents = new List<Document>();
            this.DocumentStatus = new List<DocumentStatu>();
            this.DocumentTypes = new List<DocumentType>();
            this.Entities = new List<Entity>();
            this.Forms = new List<Form>();
            this.Glossaries = new List<Glossary>();
            this.HistPolls = new List<HistPoll>();
            this.HistProcesses = new List<HistProcess>();
            this.HistRules = new List<HistRule>();
            this.Indicators = new List<Indicator>();
            this.Infrastructures = new List<Infrastructure>();
            this.InstructionWorks = new List<InstructionWork>();
            this.InstructionWorks1 = new List<InstructionWork>();
            this.Menus = new List<Menu>();
            this.Options = new List<Option>();
            this.Periods = new List<Period>();
            this.Plans = new List<Plan>();
            this.Polls = new List<Poll>();
            this.Procedures = new List<Procedure>();
            this.ProcedureActivities = new List<ProcedureActivity>();
            this.ProcedureActivities1 = new List<ProcedureActivity>();
            this.Processes = new List<Process>();
            this.Questions = new List<Question>();
            this.Rules = new List<Rule>();
            this.Status1 = new List<Status>();
            this.Subcategories = new List<Subcategory>();
            this.Tasks = new List<Task>();
            this.TemplateFields = new List<TemplateField>();
            this.User1 = new List<User>();
            this.UserIndexColums = new List<UserIndexColum>();
            this.UserTasks = new List<UserTask>();
            this.UserTasks1 = new List<UserTask>();
            this.Webpages_Roles = new List<Webpages_Roles>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string activeKey { get; set; }
        public Nullable<System.DateTime> lastVisitAt { get; set; }
        public bool superUser { get; set; }
        public Nullable<int> departmentID { get; set; }
        public Nullable<int> companyID { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public Nullable<int> positionID { get; set; }
        public string imageUrl { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
        public virtual ICollection<Audit> Audits { get; set; }
        public virtual ICollection<AuditAuditor> AuditAuditors { get; set; }
        public virtual ICollection<AuditMeetingParticipant> AuditMeetingParticipants { get; set; }
        public virtual ICollection<AuditPlanAuditor> AuditPlanAuditors { get; set; }
        public virtual ICollection<AuditPlanResponsible> AuditPlanResponsibles { get; set; }
        public virtual ICollection<AuditPlan> AuditPlans { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Dashboard> Dashboards { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<DocumentStatu> DocumentStatus { get; set; }
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
        public virtual ICollection<Entity> Entities { get; set; }
        public virtual ICollection<Form> Forms { get; set; }
        public virtual ICollection<Glossary> Glossaries { get; set; }
        public virtual ICollection<HistPoll> HistPolls { get; set; }
        public virtual ICollection<HistProcess> HistProcesses { get; set; }
        public virtual ICollection<HistRule> HistRules { get; set; }
        public virtual ICollection<Indicator> Indicators { get; set; }
        public virtual ICollection<Infrastructure> Infrastructures { get; set; }
        public virtual ICollection<InstructionWork> InstructionWorks { get; set; }
        public virtual ICollection<InstructionWork> InstructionWorks1 { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<Period> Periods { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual ICollection<Poll> Polls { get; set; }
        public virtual Position Position { get; set; }
        public virtual ICollection<Procedure> Procedures { get; set; }
        public virtual ICollection<ProcedureActivity> ProcedureActivities { get; set; }
        public virtual ICollection<ProcedureActivity> ProcedureActivities1 { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Rule> Rules { get; set; }
        public virtual ICollection<Status> Status1 { get; set; }
        public virtual ICollection<Subcategory> Subcategories { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<TemplateField> TemplateFields { get; set; }
        public virtual ICollection<User> User1 { get; set; }
        public virtual User User2 { get; set; }
        public virtual ICollection<UserIndexColum> UserIndexColums { get; set; }
        public virtual ICollection<UserTask> UserTasks { get; set; }
        public virtual ICollection<UserTask> UserTasks1 { get; set; }
        public virtual ICollection<Webpages_Roles> Webpages_Roles { get; set; }
    }
}
