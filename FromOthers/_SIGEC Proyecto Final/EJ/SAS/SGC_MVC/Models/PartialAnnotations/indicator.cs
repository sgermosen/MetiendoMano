using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(IndicatorMetadata))]
    public partial class Indicator
    {
        
    }

    class IndicatorMetadata 
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> createDate { get; set; }
    }
}
