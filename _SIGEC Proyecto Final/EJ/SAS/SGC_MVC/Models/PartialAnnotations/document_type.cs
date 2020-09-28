using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(DocumentTypeMetadata))]
    public partial class DocumentType
    {        
    }

    class DocumentTypeMetadata
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [System.Web.Mvc.Remote("CheckUniq", "Home", AdditionalFields = "validateUniq")]
        public string name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
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
        public virtual User User { get; set; }
    }
}
