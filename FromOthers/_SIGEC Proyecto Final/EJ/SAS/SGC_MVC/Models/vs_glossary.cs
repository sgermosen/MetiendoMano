using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class vs_glossary
    {
        public int ID { get; set; }
        public string term { get; set; }
        public string definition { get; set; }
        public int companyID { get; set; }
        public string Normas { get; set; }
    }
}
