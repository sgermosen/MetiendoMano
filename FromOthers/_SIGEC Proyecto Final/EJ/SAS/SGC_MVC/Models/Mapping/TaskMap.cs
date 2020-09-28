using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class TaskMap : EntityTypeConfiguration<Task>
    {
        public TaskMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.description)
                .IsRequired();

            this.Property(t => t.howTo)
                .IsRequired();

            this.Property(t => t.whyDo)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Task");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.howTo).HasColumnName("howTo");
            this.Property(t => t.whyDo).HasColumnName("whyDo");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.workID).HasColumnName("workID");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.processID).HasColumnName("processID");

            // Relationships
            this.HasRequired(t => t.InstructionWork)
                .WithMany(t => t.Tasks)
                .HasForeignKey(d => new { d.ruleID, d.processID, d.workID });
            this.HasOptional(t => t.User)
                .WithMany(t => t.Tasks)
                .HasForeignKey(d => d.createUser);

        }
    }
}
