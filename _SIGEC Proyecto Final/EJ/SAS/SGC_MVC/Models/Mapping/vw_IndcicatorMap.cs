using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class vw_IndcicatorMap : EntityTypeConfiguration<vw_Indcicator>
    {
        public vw_IndcicatorMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.name, t.department, t.version, t.rule });

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.department)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.version)
                .IsRequired()
                .HasMaxLength(16);

            this.Property(t => t.rule)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("vw_Indcicator");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.department).HasColumnName("department");
            this.Property(t => t.version).HasColumnName("version");
            this.Property(t => t.rule).HasColumnName("rule");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
        }
    }
}
