using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGC_MVC.Models.ViewModels
{
    public class DocumentEditViewModel
    {
        public DocumentEditViewModel()
        {
            document = new Document();
        }
        public Document document { get; set; }
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string changeReason { get; set; }
        [Required]
        [Display(Name="Normas Aplicables")]
        public int[] selectedRules { get; set; }
        [Display(Name="Versión")]
        public int version { get; set; }  
    }
}