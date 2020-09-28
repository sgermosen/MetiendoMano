using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class AuditProcessMap : EntityTypeConfiguration<AuditProcess>
    {
        public AuditProcessMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.auditID, t.processID, t.documentID });

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.auditID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.processID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.documentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AuditProcesses");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.auditID).HasColumnName("auditID");
            this.Property(t => t.processID).HasColumnName("processID");
            this.Property(t => t.documentID).HasColumnName("documentID");
            this.Property(t => t.date).HasColumnName("date");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.processTypeID).HasColumnName("processTypeID");

            // Relationships
            this.HasRequired(t => t.Audit)
                .WithMany(t => t.AuditProcesses)
                .HasForeignKey(d => d.auditID);
            this.HasRequired(t => t.Document)
                .WithMany(t => t.AuditProcesses)
                .HasForeignKey(d => d.documentID);
            this.HasRequired(t => t.ProcessType)
                .WithMany(t => t.AuditProcesses)
                .HasForeignKey(d => d.processTypeID);

        }
    }
}
