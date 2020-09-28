using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class vw_PlanMap : EntityTypeConfiguration<vw_Plan>
    {
        public vw_PlanMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.code, t.planName, t.responsible });

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.planName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.responsible)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("vw_Plan");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.code).HasColumnName("code");
            this.Property(t => t.planName).HasColumnName("planName");
            this.Property(t => t.responsible).HasColumnName("responsible");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
        }
    }
}
