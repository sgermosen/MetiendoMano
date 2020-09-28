using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class DocumentStatuMap : EntityTypeConfiguration<DocumentStatu>
    {
        public DocumentStatuMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("DocumentStatus");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.responsible).HasColumnName("responsible");
            this.Property(t => t.documentID).HasColumnName("documentID");
            this.Property(t => t.statusID).HasColumnName("statusID");
            this.Property(t => t.createDate).HasColumnName("createDate");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.DocumentStatus)
                .HasForeignKey(d => d.documentID);
            this.HasRequired(t => t.Status)
                .WithMany(t => t.DocumentStatus)
                .HasForeignKey(d => d.statusID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.DocumentStatus)
                .HasForeignKey(d => d.responsible);

        }
    }
}
