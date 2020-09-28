using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class HistInstructionWorkMap : EntityTypeConfiguration<HistInstructionWork>
    {
        public HistInstructionWorkMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.instructionWorkID });

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.instructionWorkID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("HistInstructionWorks");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.instructionWorkID).HasColumnName("instructionWorkID");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.processID).HasColumnName("processID");
            this.Property(t => t.workID).HasColumnName("workID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.responsible).HasColumnName("responsible");
            this.Property(t => t.statusID).HasColumnName("statusID");
            this.Property(t => t.departmentID).HasColumnName("departmentID");
            this.Property(t => t.dateAdded).HasColumnName("dateAdded");
            this.Property(t => t.changeReason).HasColumnName("changeReason");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.version).HasColumnName("version");

            // Relationships
            this.HasRequired(t => t.InstructionWork)
                .WithMany(t => t.HistInstructionWorks)
                .HasForeignKey(d => new { d.ruleID, d.processID, d.instructionWorkID });

        }
    }
}
