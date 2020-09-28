using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class ObjectiveResourceMap : EntityTypeConfiguration<ObjectiveResource>
    {
        public ObjectiveResourceMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.infrastructure)
                .IsRequired();

            this.Property(t => t.humans)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ObjectiveResources");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.infrastructure).HasColumnName("infrastructure");
            this.Property(t => t.humans).HasColumnName("humans");
            this.Property(t => t.objectiveID).HasColumnName("objectiveID");
            this.Property(t => t.createUser).HasColumnName("createUser");

            // Relationships
            this.HasRequired(t => t.PlanObjective)
                .WithMany(t => t.ObjectiveResources)
                .HasForeignKey(d => d.objectiveID);

        }
    }
}
