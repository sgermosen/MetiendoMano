using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(MenuMetadata))]
    public partial class Menu
    {
    }

    class MenuMetadata
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name="Nombre")]
        [System.Web.Mvc.Remote("CheckUniq", "Home", AdditionalFields = "validateUniq")]
        public string name { get; set; } 
        [Required]
        [Display(Name = "No. de orden")]
        public int noOrder { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string description { get; set; }
    }
}
