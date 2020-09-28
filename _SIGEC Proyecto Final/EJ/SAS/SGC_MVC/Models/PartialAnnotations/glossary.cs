using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(GlossaryMetadata))]
    public partial class Glossary
    {
       
    }

    class GlossaryMetadata
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Término")]
        public string term { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Definición")]
        public string definition { get; set; }
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> createDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> updateDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<int> createUser { get; set; }
        [ScaffoldColumn(false)]
        public virtual User User { get; set; }
    }
}
