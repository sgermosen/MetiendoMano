using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class FormFieldOption
    {
        public int ID { get; set; }
        public int formFieldID { get; set; }
        public string value { get; set; }
        public string label { get; set; }
        public Nullable<bool> defaultValue { get; set; }
        public Nullable<bool> isOtherOption { get; set; }
        public virtual FormField FormField { get; set; }
    }
}
