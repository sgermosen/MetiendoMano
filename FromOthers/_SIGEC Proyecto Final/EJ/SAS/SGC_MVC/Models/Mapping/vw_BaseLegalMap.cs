using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class vw_BaseLegalMap : EntityTypeConfiguration<vw_BaseLegal>
    {
        public vw_BaseLegalMap()
        {
            // Primary Key
            this.HasKey(t => new { t.title, t.document, t.url, t.ID });

            // Properties
            this.Property(t => t.title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.document)
                .IsRequired()
                .HasMaxLength(4000);

            this.Property(t => t.url)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("vw_BaseLegal");
            this.Property(t => t.title).HasColumnName("title");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.document).HasColumnName("document");
            this.Property(t => t.url).HasColumnName("url");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
