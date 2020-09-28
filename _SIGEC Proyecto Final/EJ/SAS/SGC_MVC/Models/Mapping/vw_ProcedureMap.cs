using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class vw_ProcedureMap : EntityTypeConfiguration<vw_Procedure>
    {
        public vw_ProcedureMap()
        {
            // Primary Key
            this.HasKey(t => new { t.name, t.username, t.ID });

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.username)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vw_Procedure");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.username).HasColumnName("username");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
