using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(PlanMetaData))]
    public partial class Plan
    {
        [NotMapped]
        [AllowHtml]
        public string changeReason { get; set; }
    }

    class PlanMetaData
    {
        [Key]
        public int ID { get; set; }

        [Display(Name="Código")]
        [Required]
        public string code { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        public string name { get; set; }
        [Display(Name = "Responsable")]
        [Required]
        public int responsible { get; set; }
        [Required]
        [DisplayName("Proceso relacionado")]
        public int processID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> createDate { get; set; }
        [Display(Name = "Ultima Modificación")]
        public Nullable<System.DateTime> updateDate { get; set; }
        [Display(Name = "Posición")]
        public virtual Position Position { get; set; }
    }
}
