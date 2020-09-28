using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class AuditTypeMap : EntityTypeConfiguration<AuditType>
    {
        public AuditTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.nombre)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AuditType");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.nombre).HasColumnName("nombre");
            this.Property(t => t.companyID).HasColumnName("companyID");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.AuditTypes)
                .HasForeignKey(d => d.companyID);

        }
    }
}
