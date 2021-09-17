using System.ComponentModel.DataAnnotations;

namespace APIPELICULA.DTOS
{
    public class GeneroDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; }
    }
}