using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGC_MVC.Models.ViewModels
{
    public class PurposeEditViewModel
    {
        [Key]
        public int id { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [AllowHtml]
        [Display(Name="Contenido")]
        public string document_text { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Razón")]
        public string changeReason { get; set; }
        public string docType { get; set; }
        public bool isNewVersion { get; set; }
    }
}