using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(ProcedureMetadata))]
    public partial class Procedure
    {
        [DisplayName("Documentos Anexos")]
        [NotMapped]
        public ICollection<Document> Annexes
        {
            get { return this.Documents; }
            set { this.Documents = value; }
        }

        [DisplayName("Normativas")]
        [NotMapped]
        public ICollection<Document> Normatives
        {
            get { return this.Documents1; }
            set { this.Documents1 = value; }
        }

        [DisplayName("Referencias")]
        [NotMapped]
        public ICollection<Document> References
        {
            get { return this.Documents2; }
            set { this.Documents2 = value; }
        }

        [NotMapped]
        [AllowHtml]
        public string changeReason { get; set; }

        public void AddReference(Document doc)
        {
            this.Documents2.Add(doc);
        }
    }

    class ProcedureMetadata
    {
        [Key]
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> createDate { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Objetivo")]
        [Required]
        public string target { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Alcance")]
        [Required]
        public string procedureScope { get; set; }
        [DataType(DataType.MultilineText)]
        public Nullable<int> createUser { get; set; }
        [ScaffoldColumn(false)]
        public int statusID { get; set; }
        [System.Web.Mvc.Remote("CheckUniqGeneral", "Home", AdditionalFields = "validateUniq")]
        [Display(Name="Nombre")]
        [Required]
        public string name { get; set; }
        [Display(Name = "Norma")]
        public int ruleID { get; set; }
        [Display(Name = "Proceso")]
        public int processID { get; set; }
        [DisplayName("Terminos")]
        public virtual ICollection<Glossary> Glossaries { get; set; }
        [DisplayName("Actividades")]
        public virtual ICollection<ProcedureActivity> ProcedureActivities { get; set; }
    }
}
