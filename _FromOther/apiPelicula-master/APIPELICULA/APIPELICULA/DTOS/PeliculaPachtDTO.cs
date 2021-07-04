using System;
using System.ComponentModel.DataAnnotations;

namespace APIPELICULA.DTOS
{
    public class PeliculaPachtDto
    {
       
        [Required]
        [StringLength(320)]
        public string Titulo { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
    }
}