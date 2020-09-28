using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class TemplateFieldType
    {
        public TemplateFieldType()
        {
            this.TemplateFields = new List<TemplateField>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public virtual ICollection<TemplateField> TemplateFields { get; set; }
    }
}
