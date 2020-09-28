using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class vs_glossaryMap : EntityTypeConfiguration<vs_glossary>
    {
        public vs_glossaryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.term, t.definition, t.companyID });

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.term)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.definition)
                .IsRequired();

            this.Property(t => t.companyID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vs_glossary");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.term).HasColumnName("term");
            this.Property(t => t.definition).HasColumnName("definition");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.Normas).HasColumnName("Normas");
        }
    }
}
