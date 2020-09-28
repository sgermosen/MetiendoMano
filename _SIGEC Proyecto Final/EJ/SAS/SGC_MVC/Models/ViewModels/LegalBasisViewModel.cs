using SGC_MVC.CustomCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SGC_MVC.Models.ViewModels
{
    public class LegalBasisViewModel
    {
        public int? docId { get; set; }
        public string url { get; set; }
        public int? companyID { get; set; }
        public int documentTypeID { get; set; }
        public int? createUser { get; set; }
        [Required]
        [DisplayName("Nombre")]
        [System.Web.Mvc.Remote("CheckKeysGeneral", "Home", AdditionalFields = "validateComposite,documentTypeID")]
        public string name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [DisplayName("Descripción")]
        public string description { get; set; }
        [FileSize(1024000)]
        [FileTypes("doc,docx,xlsx,pdf,txt,xls")]
        [DisplayName("Documento")]
        public HttpPostedFileBase Document { get; set; }
    }
}