using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class PlanMap : EntityTypeConfiguration<Plan>
    {
        public PlanMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Plan");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.code).HasColumnName("code");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.responsible).HasColumnName("responsible");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.processID).HasColumnName("processID");
            this.Property(t => t.version).HasColumnName("version");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Plans)
                .HasForeignKey(d => d.companyID);
            this.HasRequired(t => t.Process)
                .WithMany(t => t.Plans)
                .HasForeignKey(d => d.processID);
            this.HasRequired(t => t.Position)
                .WithMany(t => t.Plans)
                .HasForeignKey(d => d.responsible);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Plans)
                .HasForeignKey(d => d.createUser);

        }
    }
}
