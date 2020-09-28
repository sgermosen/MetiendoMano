using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(ProcedureDocumentsMetadata))]
    public partial class ProcedureDocuments
    {
    }

    class ProcedureDocumentsMetadata 
    {
        [Key]
        public int ruleID { get; set; }
    }
}
