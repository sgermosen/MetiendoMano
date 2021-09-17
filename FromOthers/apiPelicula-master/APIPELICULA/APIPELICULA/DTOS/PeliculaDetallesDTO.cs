using System.Collections.Generic;

namespace APIPELICULA.DTOS
{
    public class PeliculaDetallesDTO: PeliculaDto
    {
        public List<GeneroDto> Generos { get; set; }
        public List<ActorPeliculaDto> Actores { get; set; }
    }
}