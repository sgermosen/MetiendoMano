using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGC_MVC.Models.ViewModels
{
    public class PurposeViewModel
    {
        public Document mission { get; set; }
        public Document vision { get; set; }
        public Document values { get; set; }
        public Document policies { get; set; }
    }
}