using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class DashboardItemMap : EntityTypeConfiguration<DashboardItem>
    {
        public DashboardItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("DashboardItems");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.dashboardID).HasColumnName("dashboardID");
            this.Property(t => t.expanded).HasColumnName("expanded");
            this.Property(t => t.numOrder).HasColumnName("numOrder");

            // Relationships
            this.HasRequired(t => t.Dashboard)
                .WithMany(t => t.DashboardItems)
                .HasForeignKey(d => d.dashboardID);

        }
    }
}
