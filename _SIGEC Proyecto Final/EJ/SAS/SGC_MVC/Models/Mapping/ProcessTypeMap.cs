using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class ProcessTypeMap : EntityTypeConfiguration<ProcessType>
    {
        public ProcessTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.description)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ProcessType");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.description).HasColumnName("description");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.ProcessTypes)
                .HasForeignKey(d => d.companyID);

        }
    }
}
