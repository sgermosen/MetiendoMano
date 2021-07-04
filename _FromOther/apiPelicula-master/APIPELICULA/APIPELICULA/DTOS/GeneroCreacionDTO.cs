using System.ComponentModel.DataAnnotations;

namespace APIPELICULA.DTOS
{
    public class GeneroCreacionDto
    {
        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; }
    }
}