using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGC_MVC.CustomCode;

namespace SGC_MVC.Models.ViewModels
{
    public class ObjectivesViewModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Título")]
        [System.Web.Mvc.Remote("CheckKeysGeneral", "Home", AdditionalFields = "validateComposite,documentTypeID")]
        public string title { get; set; }
        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Contenido")]
        public string document_text { get; set; }
        public List<SelectListItem> allRules { get; set; }
        public int[] selectedRules { get; set; }
        [AllowHtml]
        [Display(Name="Razon de cambio de version")]
        public string changeReason { get; set; }
        public string submitVal { get; set; }
        public ObjectivesViewModel(Document document)
        {
            this.id = document.ID;
            this.title = document.title;
            this.document_text = document.documentText;
            this.selectedRules = document.Rules.Select(r => r.ID).ToArray();
        }

        public ObjectivesViewModel()
        {
            this.allRules = new List<SelectListItem>();
        }
    }
}