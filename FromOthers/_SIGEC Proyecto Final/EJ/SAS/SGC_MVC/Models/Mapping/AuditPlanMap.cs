using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class AuditPlanMap : EntityTypeConfiguration<AuditPlan>
    {
        public AuditPlanMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.ruleID, t.processTypeID, t.processID });

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.place)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ruleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.processTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.processID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AuditPlans");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.auditID).HasColumnName("auditID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.responsible).HasColumnName("responsible");
            this.Property(t => t.place).HasColumnName("place");
            this.Property(t => t.fromDate).HasColumnName("fromDate");
            this.Property(t => t.toDate).HasColumnName("toDate");
            this.Property(t => t.isProcess).HasColumnName("isProcess");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.processTypeID).HasColumnName("processTypeID");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.departmentID).HasColumnName("departmentID");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.processID).HasColumnName("processID");

            // Relationships
            this.HasRequired(t => t.Audit)
                .WithMany(t => t.AuditPlans)
                .HasForeignKey(d => d.auditID);
            this.HasRequired(t => t.Company)
                .WithMany(t => t.AuditPlans)
                .HasForeignKey(d => d.companyID);
            this.HasRequired(t => t.Process)
                .WithMany(t => t.AuditPlans)
                .HasForeignKey(d => d.processID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.AuditPlans)
                .HasForeignKey(d => d.createUser);
            this.HasRequired(t => t.Department)
                .WithMany(t => t.AuditPlans)
                .HasForeignKey(d => d.departmentID);
            this.HasRequired(t => t.ProcessType)
                .WithMany(t => t.AuditPlans)
                .HasForeignKey(d => d.processTypeID);

        }
    }
}
