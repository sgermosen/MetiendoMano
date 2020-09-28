using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class DeashboardItemMap : EntityTypeConfiguration<DeashboardItem>
    {
        public DeashboardItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DeashboardItem");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.actionID).HasColumnName("actionID");
            this.Property(t => t.description).HasColumnName("description");

            // Relationships
            this.HasRequired(t => t.Action)
                .WithMany(t => t.DeashboardItems)
                .HasForeignKey(d => d.actionID);

        }
    }
}
