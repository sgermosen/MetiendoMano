using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.username)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.password)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.activeKey)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.imageUrl)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.status).HasColumnName("status");
            this.Property(t => t.username).HasColumnName("username");
            this.Property(t => t.password).HasColumnName("password");
            this.Property(t => t.activeKey).HasColumnName("activeKey");
            this.Property(t => t.lastVisitAt).HasColumnName("lastVisitAt");
            this.Property(t => t.superUser).HasColumnName("superUser");
            this.Property(t => t.departmentID).HasColumnName("departmentID");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.positionID).HasColumnName("positionID");
            this.Property(t => t.imageUrl).HasColumnName("imageUrl");

            // Relationships
            this.HasMany(t => t.Webpages_Roles)
                .WithMany(t => t.Users)
                .Map(m =>
                    {
                        m.ToTable("Webpages_UsersInRoles");
                        m.MapLeftKey("UserID");
                        m.MapRightKey("RoleID");
                    });

            this.HasOptional(t => t.Company)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.companyID);
            this.HasOptional(t => t.Department)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.departmentID);
            this.HasOptional(t => t.Position)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.positionID);
            this.HasOptional(t => t.User2)
                .WithMany(t => t.User1)
                .HasForeignKey(d => d.createUser);

        }
    }
}
