using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Template
    {
        public Template()
        {
            this.TemplateFields = new List<TemplateField>();
        }

        public int ID { get; set; }
        public int actionID { get; set; }
        public string name { get; set; }
        public int companyID { get; set; }
        public string description { get; set; }
        public virtual Action Action { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<TemplateField> TemplateFields { get; set; }
    }
}
