using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class OptionMap : EntityTypeConfiguration<Option>
    {
        public OptionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Option");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.questionID).HasColumnName("questionID");
            this.Property(t => t.noOrder).HasColumnName("noOrder");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");

            // Relationships
            this.HasRequired(t => t.Question)
                .WithMany(t => t.Options)
                .HasForeignKey(d => d.questionID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Options)
                .HasForeignKey(d => d.createUser);

        }
    }
}
