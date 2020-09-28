using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class HistProcedureMap : EntityTypeConfiguration<HistProcedure>
    {
        public HistProcedureMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.target)
                .IsRequired();

            this.Property(t => t.procedureScope)
                .IsRequired();

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.changeReason)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("HistProcedure");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.procedureID).HasColumnName("procedureID");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.processID).HasColumnName("processID");
            this.Property(t => t.responsible).HasColumnName("responsible");
            this.Property(t => t.target).HasColumnName("target");
            this.Property(t => t.procedureScope).HasColumnName("procedureScope");
            this.Property(t => t.dateAdd).HasColumnName("dateAdd");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.statusID).HasColumnName("statusID");
            this.Property(t => t.changeReason).HasColumnName("changeReason");
            this.Property(t => t.version).HasColumnName("version");
        }
    }
}
