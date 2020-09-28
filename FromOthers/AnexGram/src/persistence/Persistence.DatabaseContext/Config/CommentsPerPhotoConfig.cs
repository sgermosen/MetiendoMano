using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.DatabaseContext.Config
{
    public class CommentsPerPhotoConfig
    {
        public CommentsPerPhotoConfig(EntityTypeBuilder<CommentsPerPhoto> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Comment).IsRequired().HasMaxLength(200);
        }
    }
}
