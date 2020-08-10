namespace Notif.Backend.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Apellido")]
        public string Lastname { get; set; }

        [Display(Name = "Direccion")]
        public string Address { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{this.Name} {this.Lastname}";
    }
}
