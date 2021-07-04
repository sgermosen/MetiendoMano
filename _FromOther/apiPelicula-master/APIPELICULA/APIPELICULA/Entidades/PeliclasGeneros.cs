namespace APIPELICULA.Entidades
{
    public class PeliclasGeneros
    {
        public int GeneroId { get; set; }
        public int peliculaId { get; set; }
        public Genero Genero { get; set; }
        public Pelicula Pelicula { get; set; }
    }
}