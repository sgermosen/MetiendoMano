using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class FieldType
    {
        public FieldType()
        {
            this.FormFields = new List<FormField>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string dataType { get; set; }
        public string htmlTag { get; set; }
        public Nullable<int> minLength { get; set; }
        public Nullable<int> maxLength { get; set; }
        public Nullable<bool> multiOptions { get; set; }
        public virtual ICollection<FormField> FormFields { get; set; }
    }
}
