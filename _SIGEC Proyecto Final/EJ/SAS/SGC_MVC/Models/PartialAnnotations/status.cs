using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(StatusMetadata))]
    public partial class Status
    {
    }

    class StatusMetadata
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Descripción")]
        public string description { get; set; }
        [Required]
        [System.Web.Mvc.Remote("CheckUniqGeneral", "Home", AdditionalFields = "validateUniq")]
        [Display(Name = "Nombre")]
        public string name { get; set; }
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> createDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> updateDate { get; set; }
    }
}
