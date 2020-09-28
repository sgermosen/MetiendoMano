using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class HistPlanMap : EntityTypeConfiguration<HistPlan>
    {
        public HistPlanMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.changeReason)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("HistPlan");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.planID).HasColumnName("planID");
            this.Property(t => t.code).HasColumnName("code");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.responsible).HasColumnName("responsible");
            this.Property(t => t.dateAdded).HasColumnName("dateAdded");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.processID).HasColumnName("processID");
            this.Property(t => t.version).HasColumnName("version");
            this.Property(t => t.changeReason).HasColumnName("changeReason");
        }
    }
}
