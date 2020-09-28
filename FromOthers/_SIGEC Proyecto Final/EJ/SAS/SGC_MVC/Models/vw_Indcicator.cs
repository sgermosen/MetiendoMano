using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class vw_Indcicator
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string department { get; set; }
        public string version { get; set; }
        public string rule { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
    }
}
