using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(InstructionWorkMetadata))]
    public partial class InstructionWork
    {
        [NotMapped]
        [AllowHtml]
        public string changeReason { get; set; }
    }

    class InstructionWorkMetadata
    {
        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> createDate { get; set; }
        [ScaffoldColumn(false)]
        public int statusID { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> updateDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<int> createUser { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [System.Web.Mvc.Remote("CheckUniqGeneral", "Home", AdditionalFields = "validateUniq")]
        public int name { get; set; }

        [Display(Name = "Norma Aplicable")]
        public int ruleID { get; set; }

        [Display(Name = "Proceso")]
        public int processID { get; set; }

        [Display(Name = "Responsable")]
        public int responsible { get; set; }

        [Display(Name = "Departamento")]
        public int departmentID { get; set; }

        [Display(Name = "Tareas de la instrucción de trabajo:")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
