using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department
    {
    }

    class DepartmentMetadata
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [System.Web.Mvc.Remote("CheckUniqGeneral", "Home", AdditionalFields = "validateUniq")]
        public string name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Descripción")]
        public string description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> createDate { get; set; }
        [Display(Name = "Activo")]
        public bool status { get; set; }
    }
}
