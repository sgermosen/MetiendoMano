using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class HistDocumentMap : EntityTypeConfiguration<HistDocument>
    {
        public HistDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.documentText)
                .IsRequired();

            this.Property(t => t.url)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.changeReason)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("HistDocument");
            this.Property(t => t.documentID).HasColumnName("documentID");
            this.Property(t => t.documentParentID).HasColumnName("documentParentID");
            this.Property(t => t.documentTypeID).HasColumnName("documentTypeID");
            this.Property(t => t.EDT).HasColumnName("EDT");
            this.Property(t => t.title).HasColumnName("title");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.documentText).HasColumnName("documentText");
            this.Property(t => t.url).HasColumnName("url");
            this.Property(t => t.dateAdded).HasColumnName("dateAdded");
            this.Property(t => t.createUser).HasColumnName("createUser");
            this.Property(t => t.version).HasColumnName("version");
            this.Property(t => t.companyID).HasColumnName("companyID");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.changeReason).HasColumnName("changeReason");
            this.Property(t => t.responsible).HasColumnName("responsible");
        }
    }
}
