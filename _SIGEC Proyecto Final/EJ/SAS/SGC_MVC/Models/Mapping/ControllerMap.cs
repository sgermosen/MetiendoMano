using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class ControllerMap : EntityTypeConfiguration<Controller>
    {
        public ControllerMap()
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
            this.ToTable("Controller");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.displayName).HasColumnName("displayName");
        }
    }
}
