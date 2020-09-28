using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class UserIndexColumMap : EntityTypeConfiguration<UserIndexColum>
    {
        public UserIndexColumMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.config)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("UserIndexColums");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.config).HasColumnName("config");
            this.Property(t => t.userID).HasColumnName("userID");

            // Relationships
            this.HasOptional(t => t.User)
                .WithMany(t => t.UserIndexColums)
                .HasForeignKey(d => d.userID);

        }
    }
}
