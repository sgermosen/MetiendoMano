using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class ProcessDocumentMap : EntityTypeConfiguration<ProcessDocument>
    {
        public ProcessDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.text)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProcessDocuments");
            this.Property(t => t.processID).HasColumnName("processID");
            this.Property(t => t.documentID).HasColumnName("documentID");
            this.Property(t => t.text).HasColumnName("text");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ISORequirement).HasColumnName("ISORequirement");

            // Relationships
            this.HasOptional(t => t.Document)
                .WithMany(t => t.ProcessDocuments)
                .HasForeignKey(d => d.documentID);
            this.HasRequired(t => t.Process)
                .WithMany(t => t.ProcessDocuments)
                .HasForeignKey(d => d.processID);

        }
    }
}
