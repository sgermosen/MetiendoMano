using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public DepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Department");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.status).HasColumnName("status");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.companyID).HasColumnName("companyID");

            // Relationships
            this.HasMany(t => t.Webpages_Roles)
                .WithMany(t => t.Departments)
                .Map(m =>
                    {
                        m.ToTable("RolesDepartments");
                        m.MapLeftKey("departmentID");
                        m.MapRightKey("roleID");
                    });

            this.HasRequired(t => t.Company)
                .WithMany(t => t.Departments)
                .HasForeignKey(d => d.companyID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Departments)
                .HasForeignKey(d => d.createUser);

        }
    }
}
