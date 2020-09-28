using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.DatabaseContext.Config
{
    public class ApplicationUserConfig
    {
        public ApplicationUserConfig(EntityTypeBuilder<ApplicationUser> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.SeoUrl).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.AboutUs).HasMaxLength(500);
            entityBuilder.Property(x => x.Image).HasMaxLength(100);

            entityBuilder.Property(x => x.Name).HasMaxLength(50);
            entityBuilder.Property(x => x.Lastname).HasMaxLength(50);
        }
    }
}
