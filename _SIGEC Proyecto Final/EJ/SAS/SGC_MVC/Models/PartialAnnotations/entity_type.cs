using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(EntityTypeMetadata))]
    public partial class EntityType
    { 
    }

    class EntityTypeMetadata
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        [ScaffoldColumn(false)]
        public string status { get; set; }
    }
}
