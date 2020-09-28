using SGC_MVC.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(DocumentMetadata))]
    public partial class Document
    {
        [NotMapped]
        [Display(Name ="Sub Documentos")]
        public virtual ICollection<Document> ChildDocuments
        {
            get { return Document1; }
            set { this.Document1 = value; }
        }

        [NotMapped]
        [Display(Name="Documento Padre")]
        public virtual Document ParentDocument
        {
            get { return Document2; }
            set { this.Document2 = value; }
        }
    }

    class DocumentMetadata 
    {
        [Key]
        [DisplayName("Código")]
        public int ID { get; set; }
        [Required]
        [DisplayName("Nombre")]
        [System.Web.Mvc.Remote("CheckKeysGeneral", "Home", AdditionalFields = "validateComposite,documentTypeID")]
        public string title { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Descripción")]
        public string description { get; set; }
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string documentText { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> createDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> updateDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<int> createUser { get; set; }
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "V-{0}.0", ApplyFormatInEditMode = false)]
        [Display(Name = "Version", ResourceType = typeof(Resources))]
        public int version { get; set; }
        [ScaffoldColumn(false)]
        public virtual DocumentType DocumentType { get; set; }
        [ScaffoldColumn(false)]
        public virtual Status Status { get; set; }
        [ScaffoldColumn(false)]
        public virtual Document Document2 { get; set; }
        [ScaffoldColumn(false)]
        public virtual ICollection<Document> Document1 { get; set; }
        [ScaffoldColumn(false)]
        public virtual User User { get; set; }
        [Display(Name="Normas aplicables")]
        public virtual ICollection<Rule> Rules { get; set; }
        [Display(Name = "Documento Asociado")]
        public string url { get; set; }
    }
}
