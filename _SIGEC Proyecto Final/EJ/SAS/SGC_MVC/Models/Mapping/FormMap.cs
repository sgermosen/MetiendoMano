using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class FormMap : EntityTypeConfiguration<Form>
    {
        public FormMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Form");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.processID).HasColumnName("processID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.processTypeID).HasColumnName("processTypeID");
            this.Property(t => t.version).HasColumnName("version");
            this.Property(t => t.statusID).HasColumnName("statusID");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Forms)
                .HasForeignKey(d => d.companyID);
            this.HasRequired(t => t.Process)
                .WithMany(t => t.Forms)
                .HasForeignKey(d => d.processID);
            this.HasRequired(t => t.Status)
                .WithMany(t => t.Forms)
                .HasForeignKey(d => d.statusID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Forms)
                .HasForeignKey(d => d.createUser);

        }
    }
}
