using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class TemplateFieldMap : EntityTypeConfiguration<TemplateField>
    {
        public TemplateFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.displayName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.helpText)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TemplateFields");
            this.Property(t => t.templateID).HasColumnName("templateID");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.displayName).HasColumnName("displayName");
            this.Property(t => t.isDefault).HasColumnName("isDefault");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.helpText).HasColumnName("helpText");
            this.Property(t => t.templateFieldTypeID).HasColumnName("templateFieldTypeID");
            this.Property(t => t.noOrder).HasColumnName("noOrder");

            // Relationships
            this.HasRequired(t => t.Template)
                .WithMany(t => t.TemplateFields)
                .HasForeignKey(d => d.templateID);
            this.HasRequired(t => t.TemplateFieldType)
                .WithMany(t => t.TemplateFields)
                .HasForeignKey(d => d.templateFieldTypeID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.TemplateFields)
                .HasForeignKey(d => d.createUser);

        }
    }
}
