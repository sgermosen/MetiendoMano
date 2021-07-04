using System.Collections.Generic;
using APIPELICULA.Helpers;
using APIPELICULA.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIPELICULA.DTOS
{
    public class PeliculaCreacionDto : PeliculaPachtDto
    {

        [PesoArchivoValidacion(4)]
        [TipoArchivoValidacion(GrupotipoArchivo.Imagen)]
        public IFormFile Poster { get; set; }
        [ModelBinder(typeof(TypeBinder<List<int>>))]
        public  List<int>GenerosIDs { get; set; }
        [ModelBinder(typeof(TypeBinder<List<ActorPeliculasCreacionDto>>))]
        public  List<ActorPeliculasCreacionDto>Actores { get; set; }
    }
}