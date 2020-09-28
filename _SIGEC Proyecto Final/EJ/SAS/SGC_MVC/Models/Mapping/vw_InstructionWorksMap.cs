using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class vw_InstructionWorksMap : EntityTypeConfiguration<vw_InstructionWorks>
    {
        public vw_InstructionWorksMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Rule, t.process, t.username, t.department, t.name, t.ID });

            // Properties
            this.Property(t => t.Rule)
                .IsRequired()
                .HasMaxLength(153);

            this.Property(t => t.process)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.username)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.department)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vw_InstructionWorks");
            this.Property(t => t.Rule).HasColumnName("Rule");
            this.Property(t => t.process).HasColumnName("process");
            this.Property(t => t.username).HasColumnName("username");
            this.Property(t => t.department).HasColumnName("department");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
