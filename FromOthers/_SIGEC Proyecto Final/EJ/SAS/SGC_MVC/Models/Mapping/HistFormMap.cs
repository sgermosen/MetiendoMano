using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class HistFormMap : EntityTypeConfiguration<HistForm>
    {
        public HistFormMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.changeReason)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("HistForm");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.FormID).HasColumnName("FormID");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.processID).HasColumnName("processID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.dateAdded).HasColumnName("dateAdded");
            this.Property(t => t.version).HasColumnName("version");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.processTypeID).HasColumnName("processTypeID");
            this.Property(t => t.changeReason).HasColumnName("changeReason");
            this.Property(t => t.statusID).HasColumnName("statusID");
        }
    }
}
