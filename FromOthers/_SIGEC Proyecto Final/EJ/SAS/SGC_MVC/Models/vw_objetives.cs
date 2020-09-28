using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class vw_objetives
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string Version { get; set; }
        public int companyID { get; set; }
        public string Normas { get; set; }
        public string Status { get; set; }
    }
}
