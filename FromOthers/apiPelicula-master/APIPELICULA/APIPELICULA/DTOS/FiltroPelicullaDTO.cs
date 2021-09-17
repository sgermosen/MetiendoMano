namespace APIPELICULA.DTOS
{
    public class FiltroPelicullaDTO
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistrosPorPagina { get; set; } = 10;
        //public PagonacionDto Paginacion
        //{
            
        //    get { return new PagonacionDto() { Pagina = 1, CantidadRegistrosPorPagina = 1 }; }
        //}

        public string Titulo { get; set; }
        public int GeneroId { get; set; }
        public bool EnCines { get; set; }
        public bool ProximosEstrenos { get; set; }
   
    }
}