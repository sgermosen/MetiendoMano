using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contactes.Web.Models
{
    public class Tipo
    {
        [Key]
        public int Identificador { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Debe contener maximo {0} caracteres")]
        public string Nombre { get; set; }

        public virtual ICollection<Persona> Personas { get; set; }
    }
}