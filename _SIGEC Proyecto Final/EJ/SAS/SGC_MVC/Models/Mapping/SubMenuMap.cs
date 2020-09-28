using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class SubMenuMap : EntityTypeConfiguration<SubMenu>
    {
        public SubMenuMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SubMenu");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.menuID).HasColumnName("menuID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.viewID).HasColumnName("viewID");
            this.Property(t => t.noOrder).HasColumnName("noOrder");

            // Relationships
            this.HasRequired(t => t.Menu)
                .WithMany(t => t.SubMenus)
                .HasForeignKey(d => d.menuID);
            this.HasRequired(t => t.View)
                .WithMany(t => t.SubMenus)
                .HasForeignKey(d => d.viewID);

        }
    }
}
