using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class AuditStageMap : EntityTypeConfiguration<AuditStage>
    {
        public AuditStageMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.nombre)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AuditStage");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.nombre).HasColumnName("nombre");
        }
    }
}
