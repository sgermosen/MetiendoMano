using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class AuditPlanResponsibleMap : EntityTypeConfiguration<AuditPlanResponsible>
    {
        public AuditPlanResponsibleMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("AuditPlanResponsibles");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.responsible).HasColumnName("responsible");
            this.Property(t => t.auditPlanID).HasColumnName("auditPlanID");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.AuditPlanResponsibles)
                .HasForeignKey(d => d.responsible);

        }
    }
}
