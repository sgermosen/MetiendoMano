using SGC_MVC.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Resources;

namespace SGC_MVC.Models
{
    public partial class FieldType
    {

        [NotMapped]
        public string GetName
        {
            get
            {
                ResourceManager manager = new ResourceManager(typeof(Resources));
                return manager.GetString(this.name);
            }
        }
    }

    class FieldTypeMetadada
    {
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
