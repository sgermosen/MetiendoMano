using SGC_MVC.CustomCode;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGC_MVC.Models.ViewModels
{
    public class GlossaryViewModel
    {
        public GlossaryViewModel(){
            allRules = new List<SelectListItem>();
        }
        public int glossaryID { get; set; }
        [Required]
        [Display(Name = "Término")]
        [System.Web.Mvc.Remote("CheckUniqGeneral", "Home", AdditionalFields = "validateUniq")]
        public string term { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Definición")]
        public string definition { get; set; }
        public List<SelectListItem> allRules { get; set; }
        public int[] selectedRules { get; set; }
    }
}