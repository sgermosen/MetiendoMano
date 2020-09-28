using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(FormMetadata))]
    public partial class Form 
    {
        [NotMapped]
        public string submitVal { get; set; }

        [NotMapped]
        public string changeReason { get; set; }
    }

    class FormMetadata
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Norma aplicable")]
        public int ruleID { get; set; }
        [DisplayName("Proceso")]
        public int processID { get; set; }
        [DisplayName("Nombre")]
        [System.Web.Mvc.Remote("CheckUniqGeneral", "Home", AdditionalFields = "validateUniq")]
        public string name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> createDate { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name="Ultima actualizacion")]
        public Nullable<System.DateTime> updateDate { get; set; }
        [DisplayName("Creado por")]
        public Nullable<int> createUser { get; set; }
        [DisplayName("Compañia")]
        public int companyID { get; set; }
        [DisplayName("Tipo de proceso")]
        public int processTypeID { get; set; }
        [DisplayFormat(DataFormatString="{0}.0")]
        public int version { get; set; }
        [DisplayName("Campos del formulario")]
        public virtual ICollection<FormField> FormFields { get; set; }
    }
}
