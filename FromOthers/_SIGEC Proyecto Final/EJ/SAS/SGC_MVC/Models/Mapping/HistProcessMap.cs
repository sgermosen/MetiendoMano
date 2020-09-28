using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class HistProcessMap : EntityTypeConfiguration<HistProcess>
    {
        public HistProcessMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.target)
                .IsRequired();

            this.Property(t => t.entries)
                .IsRequired();

            this.Property(t => t.activities)
                .IsRequired();

            this.Property(t => t.outputs)
                .IsRequired();

            this.Property(t => t.customerRequirements)
                .IsRequired();

            this.Property(t => t.controlMeasures)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("HistProcess");
            this.Property(t => t.processID).HasColumnName("processID");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.processTypeID).HasColumnName("processTypeID");
            this.Property(t => t.responsible).HasColumnName("responsible");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.statusID).HasColumnName("statusID");
            this.Property(t => t.target).HasColumnName("target");
            this.Property(t => t.entries).HasColumnName("entries");
            this.Property(t => t.activities).HasColumnName("activities");
            this.Property(t => t.outputs).HasColumnName("outputs");
            this.Property(t => t.customerRequirements).HasColumnName("customerRequirements");
            this.Property(t => t.controlMeasures).HasColumnName("controlMeasures");
            this.Property(t => t.outputRequirements).HasColumnName("outputRequirements");
            this.Property(t => t.dateAdded).HasColumnName("dateAdded");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.changeReason).HasColumnName("changeReason");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.version).HasColumnName("version");

            // Relationships
            this.HasRequired(t => t.Process)
                .WithMany(t => t.HistProcesses)
                .HasForeignKey(d => d.processID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.HistProcesses)
                .HasForeignKey(d => d.createUser);

        }
    }
}
