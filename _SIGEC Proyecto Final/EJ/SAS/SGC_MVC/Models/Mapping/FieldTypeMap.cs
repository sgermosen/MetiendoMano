using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class FieldTypeMap : EntityTypeConfiguration<FieldType>
    {
        public FieldTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.dataType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.htmlTag)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("FieldType");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.dataType).HasColumnName("dataType");
            this.Property(t => t.htmlTag).HasColumnName("htmlTag");
            this.Property(t => t.minLength).HasColumnName("minLength");
            this.Property(t => t.maxLength).HasColumnName("maxLength");
            this.Property(t => t.multiOptions).HasColumnName("multiOptions");
        }
    }
}
