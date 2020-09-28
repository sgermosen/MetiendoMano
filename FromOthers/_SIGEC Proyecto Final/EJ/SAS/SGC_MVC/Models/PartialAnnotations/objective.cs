using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(ObjectiveMetadata))]
    public partial class Objective
    {
    }

    class ObjectiveMetadata
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> createDate { get; set; }

        [Display(Name = "Título")]
        public string title { get; set; }

        [Display(Name = "Contenido")]
        public string document_text { get; set; }
    }
}
