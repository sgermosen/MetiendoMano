using SGC_MVC.CustomCode;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(CompanyMetadata))]
    public partial class Company
    {
        
    }

    class CompanyMetadata
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [System.Web.Mvc.Remote("CheckUniq", "Home", AdditionalFields = "validateUniq")]
        public string name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Slogan")]
        public string companyText { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Descripción")]
        public string description { get; set; }
        [EmailAddress]
        [Required]
        [Display(Name = "Correo Eléctronico")]
        public string email { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Logo")]
        public string logo { get; set; }
        [Display(Name = "¿Activa?")]
        public bool status { get; set; }
    }
}
