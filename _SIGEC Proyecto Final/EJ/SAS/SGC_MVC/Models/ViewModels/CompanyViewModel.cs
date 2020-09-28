using SGC_MVC.CustomCode;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGC_MVC.Models.ViewModels
{
    public class CompanyViewModel
    {
        public CompanyViewModel()
        {
        }

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
        [MaxFileSize(1024 * 1024, ErrorMessage = "El archivo no puede exceder los {0} bytes")]
        [FileTypes("jpeg,jpg,png,gif,bmp")]
        //[FileExtensions(Extensions="jpg,jpeg,png,gif", ErrorMessage = "Los tipos de extensiones permitidas son: {1}")]
        public HttpPostedFileBase logoImage { get; set; }
        [Required]
        [Display(Name="Normas Adquiridas")]
        public int[] rulesCompany { get; set; }
        public string filename { get; set; }
    }
}