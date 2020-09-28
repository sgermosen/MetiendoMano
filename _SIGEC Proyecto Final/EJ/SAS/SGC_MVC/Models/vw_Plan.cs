using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class vw_Plan
    {
        public int ID { get; set; }
        public string code { get; set; }
        public string planName { get; set; }
        public string responsible { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
    }
}
