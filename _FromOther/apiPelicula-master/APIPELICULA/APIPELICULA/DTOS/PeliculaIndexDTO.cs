using System.Collections.Generic;

namespace APIPELICULA.DTOS
{
    public class PeliculaIndexDTO
    {
        public List<PeliculaDto> FuturosEstrenos { get; set; }
        public List<PeliculaDto> EnCines { get; set; }
    }
}