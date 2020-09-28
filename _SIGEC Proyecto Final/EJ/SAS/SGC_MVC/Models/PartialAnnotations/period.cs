using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(PeriodMetadata))]
    public partial class Period
    {
    }

    class PeriodMetadata
    {
        [Key]
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> createDate { get; set; }
    }
}
