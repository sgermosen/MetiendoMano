using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.companyText)
                .IsRequired();

            this.Property(t => t.description)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.email)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.logo)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Company");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.companyText).HasColumnName("companyText");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.status).HasColumnName("status");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.logo).HasColumnName("logo");

            // Relationships
            this.HasMany(t => t.Rules)
                .WithMany(t => t.Companies)
                .Map(m =>
                    {
                        m.ToTable("CompanyRules");
                        m.MapLeftKey("companyID");
                        m.MapRightKey("ruleID");
                    });


        }
    }
}
