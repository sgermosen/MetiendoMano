using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(InstructionWorkTaskMetadata))]
    public partial class InstructionWorkTask
    {
        
    }

    class InstructionWorkTaskMetadata 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> dateAdded { get; set; }
    }
}
