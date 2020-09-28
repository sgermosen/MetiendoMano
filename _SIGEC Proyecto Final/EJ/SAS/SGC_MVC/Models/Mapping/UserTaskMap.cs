using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class UserTaskMap : EntityTypeConfiguration<UserTask>
    {
        public UserTaskMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UserTask");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.parentTaskID).HasColumnName("parentTaskID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.ruleID).HasColumnName("ruleID");
            this.Property(t => t.fromDate).HasColumnName("fromDate");
            this.Property(t => t.toDate).HasColumnName("toDate");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.statusID).HasColumnName("statusID");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.responsible).HasColumnName("responsible");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.UserTasks)
                .HasForeignKey(d => d.companyID);
            this.HasRequired(t => t.Rule)
                .WithMany(t => t.UserTasks)
                .HasForeignKey(d => d.ruleID);
            this.HasRequired(t => t.Status)
                .WithMany(t => t.UserTasks)
                .HasForeignKey(d => d.statusID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.UserTasks)
                .HasForeignKey(d => d.createUser);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.UserTasks1)
                .HasForeignKey(d => d.responsible);
            this.HasOptional(t => t.UserTask2)
                .WithMany(t => t.UserTask1)
                .HasForeignKey(d => d.parentTaskID);

        }
    }
}
