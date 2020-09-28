using DataAnnotationsExtensions;
using SGC_MVC.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(FormFieldMetadata))]
    public partial class FormField
    {
        
    }

    class FormFieldMetadata
    {
        public int formID { get; set; }
        [Display(Name = "Question", ResourceType = typeof(Resources))]
        public string question { get; set; }
        [Display(Name = "VariableShorcut", ResourceType = typeof(Resources))]
        public string variableShortcut { get; set; }
        [Display(Name = "Tooltip", ResourceType = typeof(Resources))]
        public string tooltip { get; set; }
        [Display(Name = "NoOrder", ResourceType = typeof(Resources))]
        [Integer(ErrorMessageResourceName = "NoOrderErrMessage", ErrorMessageResourceType = typeof(Resources))]
        public int noOrder { get; set; }
        [Display(Name = "FieldType", ResourceType = typeof(Resources))]
        public int fieldTypeID { get; set; }
        [Display(Name = "ValidationMessage", ResourceType = typeof(Resources))]
        public string validationMessage { get; set; }
        public Nullable<bool> required { get; set; }
        public string requiredText { get; set; }
        public string rangeFrom { get; set; }
        public string rangeTo { get; set; }
        public int ID { get; set; }
        public string panel { get; set; }
        public virtual FieldType FieldType { get; set; }
        public virtual Form Form { get; set; }
        public virtual ICollection<FormFieldOption> FormFieldOptions { get; set; }
        public virtual ICollection<FormRecord> FormRecords { get; set; }
        public virtual ICollection<Indicator> Indicators { get; set; }
    }
}
