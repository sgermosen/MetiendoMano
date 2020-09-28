using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(PlanResourceMetadata))]
    public partial class PlanResource
    { }

    class PlanResourceMetadata
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Infraestructura")]
        public string infrastructure { get; set; }
        [DisplayName("Humanos")]
        public string humans { get; set; }
    }
}
