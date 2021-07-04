using APIPELICULA.Validaciones;
using Microsoft.AspNetCore.Http;

namespace APIPELICULA.DTOS
{
    public class ActorCreacionDto : ActorPacthDto
    {

        [PesoArchivoValidacion(4)]
        [TipoArchivoValidacion(grupotipoArchivo: GrupotipoArchivo.Imagen )]
        public IFormFile Foto { get; set; }
    }
}