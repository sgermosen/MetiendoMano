using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SGC_MVC.Models.Mapping;

namespace SGC_MVC.Models
{
    public partial class SASContext : DbContext
    {
        static SASContext()
        {
            Database.SetInitializer<SASContext>(null);
        }

        public SASContext()
            : base("Name=SASContext")
        {
        }

        public DbSet<Action> Actions { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<AuditAuditorRole> AuditAuditorRoles { get; set; }
        public DbSet<AuditAuditor> AuditAuditors { get; set; }
        public DbSet<AuditMeeting> AuditMeetings { get; set; }
        public DbSet<AuditMeetingParticipant> AuditMeetingParticipants { get; set; }
        public DbSet<AuditPlanAuditor> AuditPlanAuditors { get; set; }
        public DbSet<AuditPlanResponsible> AuditPlanResponsibles { get; set; }
        public DbSet<AuditPlan> AuditPlans { get; set; }
        public DbSet<AuditProcess> AuditProcesses { get; set; }
        public DbSet<AuditStage> AuditStages { get; set; }
        public DbSet<AuditType> AuditTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Controller> Controllers { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<DashboardItem> DashboardItems { get; set; }
        public DbSet<DeashboardItem> DeashboardItems { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentStatu> DocumentStatus { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<FieldType> FieldTypes { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormField> FormFields { get; set; }
        public DbSet<FormFieldOption> FormFieldOptions { get; set; }
        public DbSet<FormRecord> FormRecords { get; set; }
        public DbSet<Glossary> Glossaries { get; set; }
        public DbSet<HistDocument> HistDocuments { get; set; }
        public DbSet<HistForm> HistForms { get; set; }
        public DbSet<HistInstructionWork> HistInstructionWorks { get; set; }
        public DbSet<HistPlan> HistPlans { get; set; }
        public DbSet<HistPoll> HistPolls { get; set; }
        public DbSet<HistProcedure> HistProcedures { get; set; }
        public DbSet<HistProcess> HistProcesses { get; set; }
        public DbSet<HistRule> HistRules { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<Infrastructure> Infrastructures { get; set; }
        public DbSet<InstructionWork> InstructionWorks { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<ObjectiveResource> ObjectiveResources { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanObjective> PlanObjectives { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<ProcedureActivity> ProcedureActivities { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<ProcessDocument> ProcessDocuments { get; set; }
        public DbSet<ProcessType> ProcessTypes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<SubMenu> SubMenus { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateField> TemplateFields { get; set; }
        public DbSet<TemplateFieldType> TemplateFieldTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserIndexColum> UserIndexColums { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Webpages_Membership> Webpages_Membership { get; set; }
        public DbSet<Webpages_OAuthMembership> Webpages_OAuthMembership { get; set; }
        public DbSet<Webpages_Roles> Webpages_Roles { get; set; }
        public DbSet<vs_glossary> vs_glossary { get; set; }
        public DbSet<vw_BaseLegal> vw_BaseLegal { get; set; }
        public DbSet<vw_Indcicator> vw_Indcicator { get; set; }
        public DbSet<vw_InstructionWorks> vw_InstructionWorks { get; set; }
        public DbSet<vw_manual> vw_manual { get; set; }
        public DbSet<vw_objetives> vw_objetives { get; set; }
        public DbSet<vw_Plan> vw_Plan { get; set; }
        public DbSet<vw_policies> vw_policies { get; set; }
        public DbSet<vw_Procedure> vw_Procedure { get; set; }
        public DbSet<vw_process> vw_process { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ActionMap());
            modelBuilder.Configurations.Add(new AdvertisementMap());
            modelBuilder.Configurations.Add(new AuditMap());
            modelBuilder.Configurations.Add(new AuditAuditorRoleMap());
            modelBuilder.Configurations.Add(new AuditAuditorMap());
            modelBuilder.Configurations.Add(new AuditMeetingMap());
            modelBuilder.Configurations.Add(new AuditMeetingParticipantMap());
            modelBuilder.Configurations.Add(new AuditPlanAuditorMap());
            modelBuilder.Configurations.Add(new AuditPlanResponsibleMap());
            modelBuilder.Configurations.Add(new AuditPlanMap());
            modelBuilder.Configurations.Add(new AuditProcessMap());
            modelBuilder.Configurations.Add(new AuditStageMap());
            modelBuilder.Configurations.Add(new AuditTypeMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new ControllerMap());
            modelBuilder.Configurations.Add(new DashboardMap());
            modelBuilder.Configurations.Add(new DashboardItemMap());
            modelBuilder.Configurations.Add(new DeashboardItemMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new DocumentMap());
            modelBuilder.Configurations.Add(new DocumentStatuMap());
            modelBuilder.Configurations.Add(new DocumentTypeMap());
            modelBuilder.Configurations.Add(new EntityMap());
            modelBuilder.Configurations.Add(new EntityTypeMap());
            modelBuilder.Configurations.Add(new FieldTypeMap());
            modelBuilder.Configurations.Add(new FormMap());
            modelBuilder.Configurations.Add(new FormFieldMap());
            modelBuilder.Configurations.Add(new FormFieldOptionMap());
            modelBuilder.Configurations.Add(new FormRecordMap());
            modelBuilder.Configurations.Add(new GlossaryMap());
            modelBuilder.Configurations.Add(new HistDocumentMap());
            modelBuilder.Configurations.Add(new HistFormMap());
            modelBuilder.Configurations.Add(new HistInstructionWorkMap());
            modelBuilder.Configurations.Add(new HistPlanMap());
            modelBuilder.Configurations.Add(new HistPollMap());
            modelBuilder.Configurations.Add(new HistProcedureMap());
            modelBuilder.Configurations.Add(new HistProcessMap());
            modelBuilder.Configurations.Add(new HistRuleMap());
            modelBuilder.Configurations.Add(new IndicatorMap());
            modelBuilder.Configurations.Add(new InfrastructureMap());
            modelBuilder.Configurations.Add(new InstructionWorkMap());
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new ObjectiveResourceMap());
            modelBuilder.Configurations.Add(new OptionMap());
            modelBuilder.Configurations.Add(new PeriodMap());
            modelBuilder.Configurations.Add(new PlanMap());
            modelBuilder.Configurations.Add(new PlanObjectiveMap());
            modelBuilder.Configurations.Add(new PollMap());
            modelBuilder.Configurations.Add(new PositionMap());
            modelBuilder.Configurations.Add(new ProcedureMap());
            modelBuilder.Configurations.Add(new ProcedureActivityMap());
            modelBuilder.Configurations.Add(new ProcessMap());
            modelBuilder.Configurations.Add(new ProcessDocumentMap());
            modelBuilder.Configurations.Add(new ProcessTypeMap());
            modelBuilder.Configurations.Add(new QuestionMap());
            modelBuilder.Configurations.Add(new QuestionTypeMap());
            modelBuilder.Configurations.Add(new RuleMap());
            modelBuilder.Configurations.Add(new StatusMap());
            modelBuilder.Configurations.Add(new SubcategoryMap());
            modelBuilder.Configurations.Add(new SubMenuMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TaskMap());
            modelBuilder.Configurations.Add(new TemplateMap());
            modelBuilder.Configurations.Add(new TemplateFieldMap());
            modelBuilder.Configurations.Add(new TemplateFieldTypeMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserIndexColumMap());
            modelBuilder.Configurations.Add(new UserTaskMap());
            modelBuilder.Configurations.Add(new ViewMap());
            modelBuilder.Configurations.Add(new Webpages_MembershipMap());
            modelBuilder.Configurations.Add(new Webpages_OAuthMembershipMap());
            modelBuilder.Configurations.Add(new Webpages_RolesMap());
            modelBuilder.Configurations.Add(new vs_glossaryMap());
            modelBuilder.Configurations.Add(new vw_BaseLegalMap());
            modelBuilder.Configurations.Add(new vw_IndcicatorMap());
            modelBuilder.Configurations.Add(new vw_InstructionWorksMap());
            modelBuilder.Configurations.Add(new vw_manualMap());
            modelBuilder.Configurations.Add(new vw_objetivesMap());
            modelBuilder.Configurations.Add(new vw_PlanMap());
            modelBuilder.Configurations.Add(new vw_policiesMap());
            modelBuilder.Configurations.Add(new vw_ProcedureMap());
            modelBuilder.Configurations.Add(new vw_processMap());
        }
    }
}
