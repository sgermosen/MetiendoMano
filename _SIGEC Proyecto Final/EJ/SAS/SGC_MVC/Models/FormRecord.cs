using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class FormRecord
    {
        public int ID { get; set; }
        public int formFieldID { get; set; }
        public string value { get; set; }
        public virtual FormField FormField { get; set; }
    }
}
