using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class HistPollMap : EntityTypeConfiguration<HistPoll>
    {
        public HistPollMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.code)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("HistPoll");
            this.Property(t => t.pollID).HasColumnName("pollID");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.dateAdded).HasColumnName("dateAdded");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.code).HasColumnName("code");

            // Relationships
            this.HasRequired(t => t.Poll)
                .WithMany(t => t.HistPolls)
                .HasForeignKey(d => d.pollID);
            this.HasRequired(t => t.Rule)
                .WithMany(t => t.HistPolls)
                .HasForeignKey(d => d.ruleID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.HistPolls)
                .HasForeignKey(d => d.createUser);

        }
    }
}
