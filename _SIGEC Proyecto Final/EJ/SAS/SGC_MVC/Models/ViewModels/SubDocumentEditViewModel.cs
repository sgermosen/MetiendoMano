using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGC_MVC.Models.ViewModels
{
    public class SubDocumentEditViewModel
    {
        public SubDocumentEditViewModel()
        {
            document = new Document();
        }
        public Document document { get; set; }
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string changeReason { get; set; }
    }
}