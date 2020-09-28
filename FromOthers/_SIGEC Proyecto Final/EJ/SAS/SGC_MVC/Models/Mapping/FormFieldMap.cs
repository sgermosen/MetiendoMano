using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class FormFieldMap : EntityTypeConfiguration<FormField>
    {
        public FormFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.question)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.variableShortcut)
                .HasMaxLength(50);

            this.Property(t => t.tooltip)
                .HasMaxLength(50);

            this.Property(t => t.requiredText)
                .HasMaxLength(100);

            this.Property(t => t.rangeFrom)
                .HasMaxLength(50);

            this.Property(t => t.rangeTo)
                .HasMaxLength(50);

            this.Property(t => t.panel)
                .HasMaxLength(50);

            this.Property(t => t.validationMessage)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("FormField");
            this.Property(t => t.formID).HasColumnName("formID");
            this.Property(t => t.question).HasColumnName("question");
            this.Property(t => t.variableShortcut).HasColumnName("variableShortcut");
            this.Property(t => t.tooltip).HasColumnName("tooltip");
            this.Property(t => t.noOrder).HasColumnName("noOrder");
            this.Property(t => t.fieldTypeID).HasColumnName("fieldTypeID");
            this.Property(t => t.required).HasColumnName("required");
            this.Property(t => t.requiredText).HasColumnName("requiredText");
            this.Property(t => t.rangeFrom).HasColumnName("rangeFrom");
            this.Property(t => t.rangeTo).HasColumnName("rangeTo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.panel).HasColumnName("panel");
            this.Property(t => t.validationMessage).HasColumnName("validationMessage");

            // Relationships
            this.HasRequired(t => t.FieldType)
                .WithMany(t => t.FormFields)
                .HasForeignKey(d => d.fieldTypeID);
            this.HasRequired(t => t.Form)
                .WithMany(t => t.FormFields)
                .HasForeignKey(d => d.formID);

        }
    }
}
