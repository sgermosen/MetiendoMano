using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class TemplateMap : EntityTypeConfiguration<Template>
    {
        public TemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Template");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.actionID).HasColumnName("actionID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.description).HasColumnName("description");

            // Relationships
            this.HasRequired(t => t.Action)
                .WithMany(t => t.Templates)
                .HasForeignKey(d => d.actionID);
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Templates)
                .HasForeignKey(d => d.companyID);

        }
    }
}
