using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class HistRuleMap : EntityTypeConfiguration<HistRule>
    {
        public HistRuleMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.description)
                .IsRequired();

            this.Property(t => t.userAdd)
                .IsRequired();

            this.Property(t => t.code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("HistRules");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.documentID).HasColumnName("documentID");
            this.Property(t => t.version).HasColumnName("version");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.userAdd).HasColumnName("userAdd");
            this.Property(t => t.dateAdded).HasColumnName("dateAdded");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.code).HasColumnName("code");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.status).HasColumnName("status");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.HistRules)
                .HasForeignKey(d => d.documentID);
            this.HasRequired(t => t.Rule)
                .WithMany(t => t.HistRules)
                .HasForeignKey(d => d.ruleID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.HistRules)
                .HasForeignKey(d => d.createUser);

        }
    }
}
