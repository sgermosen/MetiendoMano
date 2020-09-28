using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(RuleMetadata))]
    public partial class Rule
    {
    }

    class RuleMetadata
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name="Nombre")]
        [System.Web.Mvc.Remote("CheckUniq", "Home", AdditionalFields = "validateUniq")]
        public string name { get; set; }
        [Required]
        [Display(Name = "Código")]
        [System.Web.Mvc.Remote("CheckUniq", "Home", AdditionalFields = "validateUniqCode")]
        public string code { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> createDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> updateDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<int> createUser { get; set; }
        [ScaffoldColumn(false)]
        public virtual HistRules HistRules { get; set; }
        [ScaffoldColumn(false)]
        public virtual User User { get; set; }
        [Display(Name = "Activo")]
        public bool status { get; set; }
    }
}
