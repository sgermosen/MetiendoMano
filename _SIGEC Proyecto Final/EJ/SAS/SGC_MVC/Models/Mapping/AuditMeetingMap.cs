using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class AuditMeetingMap : EntityTypeConfiguration<AuditMeeting>
    {
        public AuditMeetingMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("AuditMeeting");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.auditID).HasColumnName("auditID");
            this.Property(t => t.isOpening).HasColumnName("isOpening");

            // Relationships
            this.HasRequired(t => t.Audit)
                .WithMany(t => t.AuditMeetings)
                .HasForeignKey(d => d.auditID);

        }
    }
}
