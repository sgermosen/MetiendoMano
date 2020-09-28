using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class QuestionMap : EntityTypeConfiguration<Question>
    {
        public QuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Question");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.pollID).HasColumnName("pollID");
            this.Property(t => t.noOrder).HasColumnName("noOrder");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.questionTypeID).HasColumnName("questionTypeID");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");

            // Relationships
            this.HasRequired(t => t.Poll)
                .WithMany(t => t.Questions)
                .HasForeignKey(d => d.pollID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Questions)
                .HasForeignKey(d => d.createUser);
            this.HasRequired(t => t.QuestionType)
                .WithMany(t => t.Questions)
                .HasForeignKey(d => d.questionTypeID);

        }
    }
}
