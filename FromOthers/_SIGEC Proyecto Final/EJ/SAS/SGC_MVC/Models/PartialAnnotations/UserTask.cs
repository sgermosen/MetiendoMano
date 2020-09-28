using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SGC_MVC.CustomCode;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(UserTaskMetadata))]
    public partial class UserTask { }

    class UserTaskMetadata
    {
        [Key]
        public int ID { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<int> parentTaskID { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [System.Web.Mvc.Remote("CheckUniqGeneral", "Home", AdditionalFields = "validateUniq")]
        public string name { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string description { get; set; }
        [Display(Name = "Norma Aplicable")]
        public int ruleID { get; set; }
        [Display(Name = "Desde")]
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fromDate { get; set; }
        [Display(Name = "Hasta")]
        [DataType(DataType.Date)]
        [Required]
        [DateEnd(DateStartProperty="fromDate")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> toDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> createDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> updateDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<int> createUser { get; set; }
        [ScaffoldColumn(false)]
        public int statusID { get; set; }
        [Display(Name = "Responsable")]
        public int responsible { get; set; }
        public virtual Rule Rule { get; set; }
        public virtual Status Status { get; set; }
        [ScaffoldColumn(false)]
        public virtual User User { get; set; }
        [ScaffoldColumn(false)]
        public virtual User User1 { get; set; }
        [ScaffoldColumn(false)]
        public virtual ICollection<UserTask> UserTask1 { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "Tarea Padre")]
        public virtual UserTask UserTask2 { get; set; }
    }
}
