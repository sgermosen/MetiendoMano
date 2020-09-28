using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class TemplateField
    {
        public int templateID { get; set; }
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string displayName { get; set; }
        public bool isDefault { get; set; }
        public int createUser { get; set; }
        public string helpText { get; set; }
        public int templateFieldTypeID { get; set; }
        public Nullable<int> noOrder { get; set; }
        public virtual Template Template { get; set; }
        public virtual TemplateFieldType TemplateFieldType { get; set; }
        public virtual User User { get; set; }
    }
}
