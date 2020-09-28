using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class ProcessMap : EntityTypeConfiguration<Process>
    {
        public ProcessMap()
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
            this.ToTable("Process");
            this.Property(t => t.ID).HasColumnName("ID");
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
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.version).HasColumnName("version");

            // Relationships
            this.HasMany(t => t.Subcategories)
                .WithMany(t => t.Processes)
                .Map(m =>
                    {
                        m.ToTable("ProcessResources");
                        m.MapLeftKey("processID");
                        m.MapRightKey("subcategoryID");
                    });

            this.HasMany(t => t.Rules)
                .WithMany(t => t.Processes)
                .Map(m =>
                    {
                        m.ToTable("ProcessRules");
                        m.MapLeftKey("processID");
                        m.MapRightKey("ruleID");
                    });

            this.HasRequired(t => t.Company)
                .WithMany(t => t.Processes)
                .HasForeignKey(d => d.companyID);
            this.HasRequired(t => t.Position)
                .WithMany(t => t.Processes)
                .HasForeignKey(d => d.responsible);
            this.HasRequired(t => t.Status)
                .WithMany(t => t.Processes)
                .HasForeignKey(d => d.statusID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Processes)
                .HasForeignKey(d => d.createUser);
            this.HasRequired(t => t.ProcessType)
                .WithMany(t => t.Processes)
                .HasForeignKey(d => d.processTypeID);

        }
    }
}
