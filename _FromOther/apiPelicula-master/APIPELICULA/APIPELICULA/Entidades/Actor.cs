using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIPELICULA.Entidades
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public  DateTime FechaNacimiento { get; set; }
        public string Foto { get; set; }
        public List<PeliculasActores> PeliculasActoreses { get; set; }
    }
}