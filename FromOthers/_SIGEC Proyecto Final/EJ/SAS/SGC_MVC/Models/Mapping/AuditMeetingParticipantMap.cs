using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class AuditMeetingParticipantMap : EntityTypeConfiguration<AuditMeetingParticipant>
    {
        public AuditMeetingParticipantMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AuditMeetingParticipants");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.auditMeetingID).HasColumnName("auditMeetingID");
            this.Property(t => t.userID).HasColumnName("userID");

            // Relationships
            this.HasRequired(t => t.AuditMeeting)
                .WithMany(t => t.AuditMeetingParticipants)
                .HasForeignKey(d => d.auditMeetingID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.AuditMeetingParticipants)
                .HasForeignKey(d => d.userID);

        }
    }
}
