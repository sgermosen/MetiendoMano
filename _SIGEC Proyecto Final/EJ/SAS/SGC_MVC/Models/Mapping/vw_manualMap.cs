using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class vw_manualMap : EntityTypeConfiguration<vw_manual>
    {
        public vw_manualMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.title, t.Version, t.companyID });

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsRequired()
                .HasMaxLength(16);

            this.Property(t => t.companyID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vw_manual");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.title).HasColumnName("title");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.Normas).HasColumnName("Normas");
        }
    }
}
