using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(SubMenuMetadata))]
    public partial class SubMenu
    {
    }

    class SubMenuMetadata
    {
        [Key]
        public int ID { get; set; }
        [Display(Name="Menu")]
        public int menuID { get; set; }
        
        [Display(Name = "Nombre")]
        [Required]
        [System.Web.Mvc.Remote("CheckUniq", "Home", AdditionalFields = "validateUniq")]
        public string name { get; set; }
        
        [Display(Name = "Pantalla asignada")]
        [Required]
        public int viewID { get; set; }
        [Display(Name = "No. de órden")]
        public Nullable<int> noOrder { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual View View { get; set; }
    }
}
