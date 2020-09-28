using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class ProcedureMap : EntityTypeConfiguration<Procedure>
    {
        public ProcedureMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.target)
                .IsRequired();

            this.Property(t => t.procedureScope)
                .IsRequired();

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Procedure");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.processID).HasColumnName("processID");
            this.Property(t => t.responsible).HasColumnName("responsible");
            this.Property(t => t.target).HasColumnName("target");
            this.Property(t => t.procedureScope).HasColumnName("procedureScope");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.statusID).HasColumnName("statusID");
            this.Property(t => t.version).HasColumnName("version");

            // Relationships
            this.HasMany(t => t.Glossaries)
                .WithMany(t => t.Procedures)
                .Map(m =>
                    {
                        m.ToTable("ProcedureGlossaries");
                        m.MapLeftKey("procedureID");
                        m.MapRightKey("glossaryID");
                    });

            this.HasRequired(t => t.Company)
                .WithMany(t => t.Procedures)
                .HasForeignKey(d => d.companyID);
            this.HasRequired(t => t.Position)
                .WithMany(t => t.Procedures)
                .HasForeignKey(d => d.responsible);
            this.HasRequired(t => t.Process)
                .WithMany(t => t.Procedures)
                .HasForeignKey(d => d.processID);
            this.HasRequired(t => t.Status)
                .WithMany(t => t.Procedures)
                .HasForeignKey(d => d.statusID);
            this.HasRequired(t => t.Rule)
                .WithMany(t => t.Procedures)
                .HasForeignKey(d => d.ruleID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Procedures)
                .HasForeignKey(d => d.createUser);

        }
    }
}
