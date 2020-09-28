using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class DocumentMap : EntityTypeConfiguration<Document>
    {
        public DocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.documentText)
                .IsRequired();

            this.Property(t => t.url)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("Document");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.documentParentID).HasColumnName("documentParentID");
            this.Property(t => t.documentTypeID).HasColumnName("documentTypeID");
            this.Property(t => t.EDT).HasColumnName("EDT");
            this.Property(t => t.title).HasColumnName("title");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.documentText).HasColumnName("documentText");
            this.Property(t => t.url).HasColumnName("url");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.version).HasColumnName("version");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.statusID).HasColumnName("statusID");
            this.Property(t => t.responsible).HasColumnName("responsible");

            // Relationships
            this.HasMany(t => t.Rules)
                .WithMany(t => t.Documents)
                .Map(m =>
                    {
                        m.ToTable("DocumentRules");
                        m.MapLeftKey("documentID");
                        m.MapRightKey("ruleID");
                    });

            this.HasMany(t => t.Procedures)
                .WithMany(t => t.Documents)
                .Map(m =>
                    {
                        m.ToTable("ProcedureAnnexes");
                        m.MapLeftKey("documentID");
                        m.MapRightKey("procedureID");
                    });

            this.HasMany(t => t.Procedures1)
                .WithMany(t => t.Documents1)
                .Map(m =>
                    {
                        m.ToTable("ProcedureNormatives");
                        m.MapLeftKey("documentID");
                        m.MapRightKey("procedureID");
                    });

            this.HasMany(t => t.Procedures2)
                .WithMany(t => t.Documents2)
                .Map(m =>
                    {
                        m.ToTable("ProcedureReferences");
                        m.MapLeftKey("documentID");
                        m.MapRightKey("procedureID");
                    });

            this.HasMany(t => t.Processes)
                .WithMany(t => t.Documents)
                .Map(m =>
                    {
                        m.ToTable("ProcessRequirements");
                        m.MapLeftKey("documentID");
                        m.MapRightKey("processID");
                    });

            this.HasRequired(t => t.Company)
                .WithMany(t => t.Documents)
                .HasForeignKey(d => d.companyID);
            this.HasRequired(t => t.DocumentType)
                .WithMany(t => t.Documents)
                .HasForeignKey(d => d.documentTypeID);
            this.HasOptional(t => t.Position)
                .WithMany(t => t.Documents)
                .HasForeignKey(d => d.responsible);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Documents)
                .HasForeignKey(d => d.createUser);
            this.HasOptional(t => t.Document2)
                .WithMany(t => t.Document1)
                .HasForeignKey(d => d.documentParentID);
            this.HasOptional(t => t.Status)
                .WithMany(t => t.Documents)
                .HasForeignKey(d => d.statusID);

        }
    }
}
