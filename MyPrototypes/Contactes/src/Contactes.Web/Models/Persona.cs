using Contactes.Web.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contactes.Web.Models
{
    public class Persona
    {
        // [Key]
        public int Identificador { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Debe contener maximo {0} caracteres")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Debe contener maximo {0} caracteres")]
        public string Apellido { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Debe contener maximo {0} caracteres")]
        public string Email { get; set; }

        [StringLength(13, ErrorMessage = "Debe contener maximo {0} caracteres")]
        public string Telefono { get; set; }

        [NumeroValido]
        public decimal? MontoAdeudado { get; set; }

        [StringLength(250, ErrorMessage = "Debe contener maximo {0} caracteres")]
        public string Direccion { get; set; }

        [Required]
        public int TipoIdentificador { get; set; }

        [ForeignKey("TipoIdentificador")]
        public Tipo TipoContacte { get; set; }

        public bool Eliminado { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto => $"{Nombre} {Apellido}";

        public string Estado => Eliminado ? "El pana No está :'(" : "El tipo aún es tu pana :D";
          
    }
}
