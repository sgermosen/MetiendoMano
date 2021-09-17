using System;
using System.ComponentModel.DataAnnotations;

namespace APIPELICULA.DTOS
{
    public class ActorPacthDto
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public  DateTime FechaNacimiento { get; set; }
    }
}