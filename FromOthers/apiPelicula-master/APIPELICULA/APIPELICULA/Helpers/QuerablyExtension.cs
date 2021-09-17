using System.Linq;
using APIPELICULA.DTOS;

namespace APIPELICULA.Helpers
{
    public static class QuerablyExtension
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PagonacionDto pagonacionDto)
        {
            return queryable
                .Skip((pagonacionDto.Pagina - 1) * pagonacionDto.CantidadRegistrosPorPagina)
                .Take(pagonacionDto.CantidadRegistrosPorPagina);
        }
    }
}