using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIPELICULA.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }
        [Required]
        [StringLength(320)]
        public string Titulo { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string Poster { get; set; }
        public List<PeliculasActores> PeliculasActoreses { get; set; }
        public List<PeliclasGeneros> PeliclasGeneroses { get; set; }
    }
}