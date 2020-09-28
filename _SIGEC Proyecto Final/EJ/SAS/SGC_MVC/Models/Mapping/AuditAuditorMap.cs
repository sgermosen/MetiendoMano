using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class AuditAuditorMap : EntityTypeConfiguration<AuditAuditor>
    {
        public AuditAuditorMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.auditID, t.auditAuditorRoleID });

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.auditID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.auditAuditorRoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AuditAuditors");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.auditID).HasColumnName("auditID");
            this.Property(t => t.auditor).HasColumnName("auditor");
            this.Property(t => t.auditAuditorRoleID).HasColumnName("auditAuditorRoleID");

            // Relationships
            this.HasRequired(t => t.Audit)
                .WithMany(t => t.AuditAuditors)
                .HasForeignKey(d => d.auditID);
            this.HasRequired(t => t.AuditAuditorRole)
                .WithMany(t => t.AuditAuditors)
                .HasForeignKey(d => d.auditAuditorRoleID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.AuditAuditors)
                .HasForeignKey(d => d.auditor);

        }
    }
}
