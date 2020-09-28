using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGC_MVC.Models.ViewModels
{
    public class DocumentCreateViewModel
    {
        [Key]
        [DisplayName("Código")]
        public int ID { get; set; }
        [Required]
        [DisplayName("Nombre")]
        [System.Web.Mvc.Remote("CheckKeysGeneral", "Home", AdditionalFields = "validateComposite,documentTypeID")]
        public string title { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Descripción")]
        public string description { get; set; }
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string documentText { get; set; }
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "V-{0}.0", ApplyFormatInEditMode = false)]
        [Display(Name = "Versión")]
        public int version { get; set; }
        [Display(Name = "Normas aplicables")]
        public virtual ICollection<Rule> Rules { get; set; }
        [Display(Name = "Documento Asociado")]
        public string url { get; set; }
        [Display(Name = "Normas aplicables")]
        [Required]
        public int[] selectedRules { get; set; }
        public Nullable<int> documentParentID { get; set; }
        [Required]
        public int documentTypeID { get; set; }
        [ScaffoldColumn(false)]
        public double EDT { get; set; }

        public void SetValues(Document doc)
        {
            doc.title = title;
            doc.documentParentID = documentParentID;
            doc.documentTypeID = documentTypeID;
            doc.description = description;
            doc.documentText = documentText;
        }
    }
}