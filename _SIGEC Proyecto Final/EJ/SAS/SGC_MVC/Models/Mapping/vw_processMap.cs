using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class vw_processMap : EntityTypeConfiguration<vw_process>
    {
        public vw_processMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.name, t.department });

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.createBy)
                .HasMaxLength(30);

            this.Property(t => t.department)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("vw_process");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.createBy).HasColumnName("createBy");
            this.Property(t => t.department).HasColumnName("department");
        }
    }
}
