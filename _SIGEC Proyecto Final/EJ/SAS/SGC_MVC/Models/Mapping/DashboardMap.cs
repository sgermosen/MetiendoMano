using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class DashboardMap : EntityTypeConfiguration<Dashboard>
    {
        public DashboardMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Dashboard");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.userID).HasColumnName("userID");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Dashboards)
                .HasForeignKey(d => d.userID);

        }
    }
}
