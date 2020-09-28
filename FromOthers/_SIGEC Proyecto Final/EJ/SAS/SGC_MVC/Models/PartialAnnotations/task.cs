using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(TaskMetadata))]
    public partial class Task
    {
    }

    class TaskMetadata
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name="Descripcion")]
        public string description { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "¿Como hacerla?")]
        public string howTo { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "¿Por que hacerla?")]
        public string whyDo { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> createDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> updateDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<int> createUser { get; set; }
        [ScaffoldColumn(false)]
        public int workID { get; set; }
        [ScaffoldColumn(false)]
        public int ruleID { get; set; }
        [ScaffoldColumn(false)]
        public int processID { get; set; }
    }
}
