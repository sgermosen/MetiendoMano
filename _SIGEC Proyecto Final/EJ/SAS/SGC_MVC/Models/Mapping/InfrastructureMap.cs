using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class InfrastructureMap : EntityTypeConfiguration<Infrastructure>
    {
        public InfrastructureMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.description)
                .IsRequired();

            this.Property(t => t.status)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Infrastructure");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.status).HasColumnName("status");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.createUser).HasColumnName("createUser");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Infrastructures)
                .HasForeignKey(d => d.companyID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Infrastructures)
                .HasForeignKey(d => d.createUser);

        }
    }
}
