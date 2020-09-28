using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class ActionMap : EntityTypeConfiguration<Action>
    {
        public ActionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.description)
                .HasMaxLength(50);

            this.Property(t => t.displayName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Action");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.controllerID).HasColumnName("controllerID");
            this.Property(t => t.isView).HasColumnName("isView");
            this.Property(t => t.displayName).HasColumnName("displayName");

            // Relationships
            this.HasMany(t => t.Webpages_Roles)
                .WithMany(t => t.Actions)
                .Map(m =>
                    {
                        m.ToTable("RolesActions");
                        m.MapLeftKey("ActionID");
                        m.MapRightKey("RoleID");
                    });

            this.HasRequired(t => t.Controller)
                .WithMany(t => t.Actions)
                .HasForeignKey(d => d.controllerID);

        }
    }
}
