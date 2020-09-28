using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class EntityMap : EntityTypeConfiguration<Entity>
    {
        public EntityMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.address)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.phone)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Entity");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.address).HasColumnName("address");
            this.Property(t => t.phone).HasColumnName("phone");
            this.Property(t => t.entityTypeID).HasColumnName("entityTypeID");
            this.Property(t => t.status).HasColumnName("status");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.createUser).HasColumnName("createUser");

            // Relationships
            this.HasMany(t => t.Processes)
                .WithMany(t => t.Entities)
                .Map(m =>
                    {
                        m.ToTable("ProcessEntities");
                        m.MapLeftKey("entityID");
                        m.MapRightKey("processID");
                    });

            this.HasRequired(t => t.Company)
                .WithMany(t => t.Entities)
                .HasForeignKey(d => d.companyID);
            this.HasRequired(t => t.EntityType)
                .WithMany(t => t.Entities)
                .HasForeignKey(d => d.entityTypeID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Entities)
                .HasForeignKey(d => d.createUser);

        }
    }
}
