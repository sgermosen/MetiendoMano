using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGC_MVC.Models.ViewModels
{
    public class ManualViewModel
    {
        public int document_Id { get; set; }
        [Required]
        public string title { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string definition { get; set; }
        public List<SelectListItem> allRules { get; set; }
        public int[] selectedRules { get; set; }
    }
}