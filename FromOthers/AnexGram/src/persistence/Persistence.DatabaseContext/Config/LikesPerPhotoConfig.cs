using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.DatabaseContext.Config
{
    public class LikesPerPhotoConfig
    {
        public LikesPerPhotoConfig(EntityTypeBuilder<LikesPerPhoto> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
        }
    }
}
