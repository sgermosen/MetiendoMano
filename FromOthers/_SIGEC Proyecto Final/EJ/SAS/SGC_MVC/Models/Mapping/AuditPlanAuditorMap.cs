using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class AuditPlanAuditorMap : EntityTypeConfiguration<AuditPlanAuditor>
    {
        public AuditPlanAuditorMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("AuditPlanAuditors");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.auditor).HasColumnName("auditor");
            this.Property(t => t.auditPlansID).HasColumnName("auditPlansID");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.AuditPlanAuditors)
                .HasForeignKey(d => d.auditor);

        }
    }
}
