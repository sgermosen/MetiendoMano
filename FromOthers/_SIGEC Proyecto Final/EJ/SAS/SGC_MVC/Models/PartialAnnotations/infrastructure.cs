using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(InfrastructureMetadata))]
    public partial class Infrastructure
    {
        
    }


    class InfrastructureMetadata 
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> createDate { get; set; }
    }
}
