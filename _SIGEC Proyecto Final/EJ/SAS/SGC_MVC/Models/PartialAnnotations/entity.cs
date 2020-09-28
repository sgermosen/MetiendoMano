using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(EntityMetadata))]
    public partial class Entity
    {
    }

    class EntityMetadata
    {   
        [Key]
        public int ID { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        public string name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Dirección")]
        public string address { get; set; }
        [Required]
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }
        [ScaffoldColumn(false)]
        [Required]
        public int entityTypeID { get; set; }
        [ScaffoldColumn(false)]
        [Required]
        public string status { get; set; }
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> createDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> updateDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<int> createUser { get; set; }
    }
}
