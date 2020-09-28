using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class FormRecordMap : EntityTypeConfiguration<FormRecord>
    {
        public FormRecordMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.value)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("FormRecords");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.formFieldID).HasColumnName("formFieldID");
            this.Property(t => t.value).HasColumnName("value");

            // Relationships
            this.HasRequired(t => t.FormField)
                .WithMany(t => t.FormRecords)
                .HasForeignKey(d => d.formFieldID);

        }
    }
}
