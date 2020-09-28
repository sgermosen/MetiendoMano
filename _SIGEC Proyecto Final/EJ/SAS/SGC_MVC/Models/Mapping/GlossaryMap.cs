using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class GlossaryMap : EntityTypeConfiguration<Glossary>
    {
        public GlossaryMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.term)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.definition)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Glossary");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.term).HasColumnName("term");
            this.Property(t => t.definition).HasColumnName("definition");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.createUser).HasColumnName("createUser");

            // Relationships
            this.HasMany(t => t.Rules)
                .WithMany(t => t.Glossaries)
                .Map(m =>
                    {
                        m.ToTable("GlossaryRules");
                        m.MapLeftKey("glossaryID");
                        m.MapRightKey("ruleID");
                    });

            this.HasRequired(t => t.Company)
                .WithMany(t => t.Glossaries)
                .HasForeignKey(d => d.companyID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Glossaries)
                .HasForeignKey(d => d.createUser);

        }
    }
}
