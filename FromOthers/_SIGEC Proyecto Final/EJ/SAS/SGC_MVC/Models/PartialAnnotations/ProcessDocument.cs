using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(ProcessDocumentMetadata))]
    public partial class ProcessDocument
    { }

    class ProcessDocumentMetadata
    {
        [DisplayName("Proceso")]
        public int processID { get; set; }
        [DisplayName("Documento Asociado")]
        public Nullable<int> documentID { get; set; }
        [DisplayName("Texto Libre")]
        public string text { get; set; }
        [Key]
        public int ID { get; set; }
        [DisplayName("Documento Asociado")]
        public virtual Document Document { get; set; }
        [DisplayName("Proceso")]
        public virtual Process Process { get; set; }
    }
}
