using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class FormField
    {
        public FormField()
        {
            this.FormFieldOptions = new List<FormFieldOption>();
            this.FormRecords = new List<FormRecord>();
            this.Indicators = new List<Indicator>();
        }

        public int formID { get; set; }
        public string question { get; set; }
        public string variableShortcut { get; set; }
        public string tooltip { get; set; }
        public int noOrder { get; set; }
        public int fieldTypeID { get; set; }
        public Nullable<bool> required { get; set; }
        public string requiredText { get; set; }
        public string rangeFrom { get; set; }
        public string rangeTo { get; set; }
        public int ID { get; set; }
        public string panel { get; set; }
        public string validationMessage { get; set; }
        public virtual FieldType FieldType { get; set; }
        public virtual Form Form { get; set; }
        public virtual ICollection<FormFieldOption> FormFieldOptions { get; set; }
        public virtual ICollection<FormRecord> FormRecords { get; set; }
        public virtual ICollection<Indicator> Indicators { get; set; }
    }
}
