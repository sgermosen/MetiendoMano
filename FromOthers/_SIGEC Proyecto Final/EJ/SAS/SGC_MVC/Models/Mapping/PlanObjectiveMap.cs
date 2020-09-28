using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class PlanObjectiveMap : EntityTypeConfiguration<PlanObjective>
    {
        public PlanObjectiveMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.actions)
                .IsRequired();

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PlanObjectives");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.startDate).HasColumnName("startDate");
            this.Property(t => t.endDate).HasColumnName("endDate");
            this.Property(t => t.responsible).HasColumnName("responsible");
            this.Property(t => t.planID).HasColumnName("planID");
            this.Property(t => t.actions).HasColumnName("actions");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.createUser).HasColumnName("createUser");

            // Relationships
            this.HasRequired(t => t.Plan)
                .WithMany(t => t.PlanObjectives)
                .HasForeignKey(d => d.planID);
            this.HasRequired(t => t.Position)
                .WithMany(t => t.PlanObjectives)
                .HasForeignKey(d => d.responsible);

        }
    }
}
