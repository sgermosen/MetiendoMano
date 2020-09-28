using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class IndicatorMap : EntityTypeConfiguration<Indicator>
    {
        public IndicatorMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.status)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.measureUnit)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Indicator");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.status).HasColumnName("status");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.processID).HasColumnName("processID");
            this.Property(t => t.target).HasColumnName("target");
            this.Property(t => t.dataSourceMeasure).HasColumnName("dataSourceMeasure");
            this.Property(t => t.measureUnit).HasColumnName("measureUnit");
            this.Property(t => t.periodID).HasColumnName("periodID");
            this.Property(t => t.responsibleOfGenerate).HasColumnName("responsibleOfGenerate");
            this.Property(t => t.responsableMonitor).HasColumnName("responsableMonitor");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.processTypeID).HasColumnName("processTypeID");
            this.Property(t => t.formula).HasColumnName("formula");
            this.Property(t => t.lowerLimit).HasColumnName("lowerLimit");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.goal).HasColumnName("goal");
            this.Property(t => t.upperLimit).HasColumnName("upperLimit");
            this.Property(t => t.frequency).HasColumnName("frequency");
            this.Property(t => t.version).HasColumnName("version");

            // Relationships
            this.HasMany(t => t.FormFields)
                .WithMany(t => t.Indicators)
                .Map(m =>
                    {
                        m.ToTable("IndicatorVariables");
                        m.MapLeftKey("indicatorID");
                        m.MapRightKey("formFieldID");
                    });

            this.HasRequired(t => t.Company)
                .WithMany(t => t.Indicators)
                .HasForeignKey(d => d.companyID);
            this.HasOptional(t => t.Period)
                .WithMany(t => t.Indicators)
                .HasForeignKey(d => d.periodID);
            this.HasOptional(t => t.Position)
                .WithMany(t => t.Indicators)
                .HasForeignKey(d => d.responsibleOfGenerate);
            this.HasOptional(t => t.Position1)
                .WithMany(t => t.Indicators1)
                .HasForeignKey(d => d.responsableMonitor);
            this.HasRequired(t => t.Process)
                .WithMany(t => t.Indicators)
                .HasForeignKey(d => d.processID);
            this.HasRequired(t => t.Rule)
                .WithMany(t => t.Indicators)
                .HasForeignKey(d => d.ruleID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Indicators)
                .HasForeignKey(d => d.createUser);

        }
    }
}
