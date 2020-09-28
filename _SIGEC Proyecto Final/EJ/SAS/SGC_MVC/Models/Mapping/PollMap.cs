using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class PollMap : EntityTypeConfiguration<Poll>
    {
        public PollMap()
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

            // Table & Column Mappings
            this.ToTable("Poll");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.code).HasColumnName("code");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.createUser).HasColumnName("createUser");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Polls)
                .HasForeignKey(d => d.companyID);
            this.HasRequired(t => t.Rule)
                .WithMany(t => t.Polls)
                .HasForeignKey(d => d.ruleID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Polls)
                .HasForeignKey(d => d.createUser);

        }
    }
}
