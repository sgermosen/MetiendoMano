using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class FormFieldOptionMap : EntityTypeConfiguration<FormFieldOption>
    {
        public FormFieldOptionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.formFieldID, t.value });

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.formFieldID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.value)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.label)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("FormFieldOption");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.formFieldID).HasColumnName("formFieldID");
            this.Property(t => t.value).HasColumnName("value");
            this.Property(t => t.label).HasColumnName("label");
            this.Property(t => t.defaultValue).HasColumnName("defaultValue");
            this.Property(t => t.isOtherOption).HasColumnName("isOtherOption");

            // Relationships
            this.HasRequired(t => t.FormField)
                .WithMany(t => t.FormFieldOptions)
                .HasForeignKey(d => d.formFieldID);

        }
    }
}
