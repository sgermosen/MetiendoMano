namespace APIPELICULA.DTOS
{
    public class PagonacionDto
    {
        public int Pagina { get; set; } = 1;

        private int cantidadRegistroPorPagina = 10;
        private readonly int cantidadMaximaRegistroPorPagina = 50;


        public int CantidadRegistrosPorPagina
        {
            get => cantidadRegistroPorPagina;
            set
            {
                CantidadRegistrosPorPagina =
                    (value > cantidadMaximaRegistroPorPagina) ? cantidadMaximaRegistroPorPagina : value;
            }
        }
    }
}