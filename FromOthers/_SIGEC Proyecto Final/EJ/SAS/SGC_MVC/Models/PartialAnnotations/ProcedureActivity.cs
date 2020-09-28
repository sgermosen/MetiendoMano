using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(ProcedureActivityMetadata))]
    public partial class ProcedureActivity { }

    public class ProcedureActivityMetadata 
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string activity { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> createDate { get; set; }
       
    }
}
