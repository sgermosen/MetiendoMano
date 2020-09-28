using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(ActionMetadata))]
    public partial class Action
    {

        
    }

    class ActionMetadata
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Is View?")]
        public bool isView { get; set; }
        [Display(Name = "Nombre de muestra")]
        public string displayName { get; set; }
    }
}
