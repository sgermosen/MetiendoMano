using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class ProcessResourceMap : EntityTypeConfiguration<ProcessResource>
    {
        public ProcessResourceMap()
        {
            // Primary Key
            this.HasKey(t => t.processResourceID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProcessResources");
            this.Property(t => t.processResourceID).HasColumnName("processResourceID");
            this.Property(t => t.subcategoryID).HasColumnName("subcategoryID");
            this.Property(t => t.processID).HasColumnName("processID");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.categoryID).HasColumnName("categoryID");

            // Relationships
            this.HasOptional(t => t.User)
                .WithMany(t => t.ProcessResources)
                .HasForeignKey(d => d.createUser);
            this.HasRequired(t => t.Subcategory)
                .WithMany(t => t.ProcessResources)
                .HasForeignKey(d => new { d.categoryID, d.subcategoryID });

        }
    }
}
