using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class SubcategoryMap : EntityTypeConfiguration<Subcategory>
    {
        public SubcategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Subcategory");
            this.Property(t => t.categoryID).HasColumnName("categoryID");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.status).HasColumnName("status");
            this.Property(t => t.createDate).HasColumnName("createDate");
            this.Property(t => t.updateDate).HasColumnName("updateDate");
            this.Property(t => t.createUser).HasColumnName("createUser");

            // Relationships
            this.HasRequired(t => t.Category)
                .WithMany(t => t.Subcategories)
                .HasForeignKey(d => d.categoryID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Subcategories)
                .HasForeignKey(d => d.createUser);

        }
    }
}
