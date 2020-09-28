using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class ProcedureActivityMap : EntityTypeConfiguration<ProcedureActivity>
    {
        public ProcedureActivityMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.activity)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureActivities");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.procedureID).HasColumnName("procedureID");
            this.Property(t => t.responsible).HasColumnName("responsible");
            this.Property(t => t.activity).HasColumnName("activity");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");

            // Relationships
            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureActivities)
                .HasForeignKey(d => d.procedureID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.ProcedureActivities)
                .HasForeignKey(d => d.createUser);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ProcedureActivities1)
                .HasForeignKey(d => d.responsible);

        }
    }
}
