using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(WebpagesRolesMetadata))]
    public partial class Webpages_Roles
    {
        
    }

    class WebpagesRolesMetadata
    {
        [Key]
        public int RoleID { get; set; }
        [Required]
        [System.Web.Mvc.Remote("CheckUniq", "Home", AdditionalFields = "validateUniq")]
        [Display(Name="Nombre")]
        public string RoleName { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string RoleDescription { get; set; }
    }
}
