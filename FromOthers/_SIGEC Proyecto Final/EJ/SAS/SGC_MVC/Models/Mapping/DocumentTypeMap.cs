using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class DocumentTypeMap : EntityTypeConfiguration<DocumentType>
    {
        public DocumentTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DocumentType");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");

            // Relationships
            this.HasMany(t => t.Rules)
                .WithMany(t => t.DocumentTypes)
                .Map(m =>
                    {
                        m.ToTable("RuleTypeDocuments");
                        m.MapLeftKey("documentTypeID");
                        m.MapRightKey("ruleID");
                    });

            this.HasOptional(t => t.User)
                .WithMany(t => t.DocumentTypes)
                .HasForeignKey(d => d.createUser);

        }
    }
}
