using SGC_MVC.CustomCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(PlanObjectiveMetadata))]
    public partial class PlanObjective { }

    class PlanObjectiveMetadata
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Fecha Inicio")]
        [Required]
        [DateStart]
        [DataType(DataType.Date)]
        public System.DateTime startDate { get; set; }
        [DisplayName("Fecha Fin")]
        [Required]
        [DateEnd(DateStartProperty = "startDate", ErrorMessage= "{0} debe ser mayor que Fecha Inicio")]
        [DataType(DataType.Date)]
        public System.DateTime endDate { get; set; }
        [Required]
        [DisplayName("Responsable")]
        public int responsible { get; set; }
        [DisplayName("Acciones")]
        [Required]
        public string actions { get; set; }
        [DisplayName("Nombre")]
        [Required]
        public string name { get; set; }
        [DisplayName("Plan")]
        public int planID { get; set; }
    }
}
