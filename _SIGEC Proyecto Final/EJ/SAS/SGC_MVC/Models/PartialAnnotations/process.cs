using SGC_MVC.CustomCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(ProcessMetadata))]
    public partial class Process
    {
        [DisplayName("Clientes")]
        [NotMapped]
        public virtual ICollection<Entity> Clients
        {
            get { return this.Entities.Where(e => e.entityTypeID == (int)Helper.EntityTypes.Cliente).ToList(); }
        }

        [DisplayName("Proveedores")]
        [NotMapped]
        public virtual ICollection<Entity> Providers
        {
            get { return this.Entities.Where(e => e.entityTypeID == (int)Helper.EntityTypes.Proveedor).ToList(); }
        }

        [NotMapped]
        [AllowHtml]
        public string changeReason { get; set; }
    }

    class ProcessMetadata
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string description { get; set; }
        [System.Web.Mvc.Remote("CheckUniqGeneral", "Home", AdditionalFields = "validateUniq")]
        [Display(Name = "Nombre")]
        [Required]
        public string name { get; set; }
        [Required]
        [DisplayName("Norma")]
        public int ruleID { get; set; }
        [DisplayName("Responsable")]
        public int responsible { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> createDate { get; set; }

        [DisplayName("Objetivo del Proceso")]
        [DataType(DataType.MultilineText)]
        public string target { get; set; }

        [DisplayName("Entradas")]
        [DataType(DataType.MultilineText)]
        public string entries { get; set; }

        [DisplayName("Actividades")]
        [DataType(DataType.MultilineText)]
        public string activities { get; set; }

        [DisplayName("Salidas")]
        [DataType(DataType.MultilineText)]
        public string outputs { get; set; }

        [DisplayName("Requisitos de las Salidas")]
        [DataType(DataType.MultilineText)]
        public string outputRequirements { get; set; }

        [DisplayName("Medidas de Control")]
        [DataType(DataType.MultilineText)]
        public string controlMeasures { get; set; }

        [DisplayName("Requisito del Cliente")]
        [DataType(DataType.MultilineText)]
        public string customerRequirements { get; set; }

        [DisplayName("Tipo de proceso")]
        public int processTypeID { get; set; }

        [DisplayName("Documentos Asociados: ")]
        public virtual ICollection<ProcessDocument> ProcessDocuments { get; set; }

        [DisplayName("Requisitos Iso")]
        public virtual ICollection<Document> Documents { get; set; }

        [DisplayName("Recursos")]
        public virtual ICollection<Subcategory> Subcategories { get; set; }

        [DisplayName("Creado Por")]
        public virtual User User { get; set; }
    }
}
