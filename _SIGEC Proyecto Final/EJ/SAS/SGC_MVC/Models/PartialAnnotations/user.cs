using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
    }

    class UserMetadata
    {
        [Key]
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> createDate { get; set; }
        [Display(Name="Nombre")]
        public string name { get; set; }
        [Display(Name = "Estado")]
        public int status { get; set; }
        [Display(Name = "E-Mail")]
        public string username { get; set; }
        [Display(Name = "Contraseña")]
        public string password { get; set; }
        public string activeKey { get; set; }
        [Display(Name = "Última Visita")]
        public Nullable<System.DateTime> lastVisitAt { get; set; }
        [Display(Name = "Super Usuario?")]
        public bool superUser { get; set; }
        public Nullable<int> departmentID { get; set; }
        public Nullable<int> companyID { get; set; }
        public Nullable<int> createUser { get; set; }
        public Nullable<int> positionID { get; set; }
        public string imageUrl { get; set; }
        [Display(Name = "Registrado Por")]
        public virtual User User2 { get; set; }
        [Display(Name = "Departamento")]
        public virtual Department Department { get; set; }
        [Display(Name="Puesto")]
        public virtual Position Position { get; set; }
    }
}
