using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SGC_MVC.Models.Mapping
{
    public class Webpages_OAuthMembershipMap : EntityTypeConfiguration<Webpages_OAuthMembership>
    {
        public Webpages_OAuthMembershipMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Provider, t.ProviderUserID });

            // Properties
            this.Property(t => t.Provider)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.ProviderUserID)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Webpages_OAuthMembership");
            this.Property(t => t.Provider).HasColumnName("Provider");
            this.Property(t => t.ProviderUserID).HasColumnName("ProviderUserID");
            this.Property(t => t.UserID).HasColumnName("UserID");
        }
    }
}
