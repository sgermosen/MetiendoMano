using System.Collections.Generic;
using APIPELICULA.DTOS;
using APIPELICULA.Entidades;
using AutoMapper;

namespace APIPELICULA.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDto>().ReverseMap();
            CreateMap<GeneroCreacionDto,Genero>();

            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<ActorCreacionDto, Actor>()
                .ForMember(c => c.Foto, 
                    options => options.Ignore());
            CreateMap<ActorPacthDto, Actor>().ReverseMap();
            
            CreateMap<Pelicula, PeliculaDto>().ReverseMap();
            CreateMap<PeliculaCreacionDto, Pelicula>()
                .ForMember(c => c.Poster, 
                    options => options.Ignore())
                .ForMember(x => x.PeliclasGeneroses, 
                    option=>  option.MapFrom(MapPeliculasGeneros))
                .ForMember(x => x.PeliculasActoreses, 
                    options => options.MapFrom(MapPeliculasActores));
            CreateMap<PeliculaPachtDto, Pelicula>().ReverseMap();

            CreateMap<Pelicula, PeliculaDetallesDTO>()
                .ForMember(x => x.Generos, 
                    options => options
                        .MapFrom(MapPeliculasGeneros))
                .ForMember(x => x.Actores, options 
                    => options.MapFrom(mapPeliculasActores));
            
        }
        
        private List<ActorPeliculaDto>mapPeliculasActores(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDto)
        {
            var resultado = new List<ActorPeliculaDto>();
            if (pelicula.PeliculasActoreses == null)
            {
                return resultado;
            }

            foreach (var actoresPelicula in pelicula.PeliculasActoreses)
            {
                resultado.Add( new ActorPeliculaDto() { ActorId = actoresPelicula.ActorId, 
                    Personaje = actoresPelicula.Personaje,
                    NombrePErsona = actoresPelicula.Actor.Nombre});
            }

            return resultado;
        }

        private List<GeneroDto> MapPeliculasGeneros(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDto)
        {
            var resultado = new List<GeneroDto>();
            if (pelicula.PeliclasGeneroses == null)
            {
                return resultado;
            }

            foreach (var generoPelicula in pelicula.PeliclasGeneroses)
            {
                resultado.Add(new GeneroDto(){Id = generoPelicula.GeneroId, Nombre =  generoPelicula.Genero.Nombre});
            }

            return resultado;

        }
        

        private List<PeliclasGeneros> MapPeliculasGeneros(PeliculaCreacionDto peliculaCreacionDto, Pelicula pelicula)
        {
            var resultado = new List<PeliclasGeneros>();

            if (peliculaCreacionDto.GenerosIDs == null)
            {
                return resultado;
            }

            foreach (var id in peliculaCreacionDto.GenerosIDs)
            {
                resultado.Add(new PeliclasGeneros(){GeneroId = id});
            }

            return resultado;
        }
        private List<PeliculasActores> MapPeliculasActores(PeliculaCreacionDto peliculaCreacionDto, Pelicula pelicula)
        {
            var resultado = new List<PeliculasActores>();

            if (peliculaCreacionDto.Actores == null)
            {
                return resultado;
            }

            foreach (var actor in peliculaCreacionDto.Actores)
            {
                resultado.Add(new PeliculasActores(){ActorId = actor.ActorId, Personaje = actor.Personaje});
            }

            return resultado;
        }
    }
}