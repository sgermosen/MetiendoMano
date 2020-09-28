using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class AuditMap : EntityTypeConfiguration<Audit>
    {
        public AuditMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.objetive)
                .IsRequired();

            this.Property(t => t.scope)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Audit");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.auditTypeID).HasColumnName("auditTypeID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.planningResponsible).HasColumnName("planningResponsible");
            this.Property(t => t.fromDate).HasColumnName("fromDate");
            this.Property(t => t.toDate).HasColumnName("toDate");
            this.Property(t => t.objetive).HasColumnName("objetive");
            this.Property(t => t.scope).HasColumnName("scope");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.comments).HasColumnName("comments");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.stageID).HasColumnName("stageID");

            // Relationships
            this.HasMany(t => t.Documents)
                .WithMany(t => t.Audits)
                .Map(m =>
                    {
                        m.ToTable("AuditDocuments");
                        m.MapLeftKey("auditID");
                        m.MapRightKey("documentID");
                    });

            this.HasRequired(t => t.AuditStage)
                .WithMany(t => t.Audits)
                .HasForeignKey(d => d.stageID);
            this.HasRequired(t => t.AuditType)
                .WithMany(t => t.Audits)
                .HasForeignKey(d => d.auditTypeID);
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Audits)
                .HasForeignKey(d => d.companyID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Audits)
                .HasForeignKey(d => d.createUser);

        }
    }
}
