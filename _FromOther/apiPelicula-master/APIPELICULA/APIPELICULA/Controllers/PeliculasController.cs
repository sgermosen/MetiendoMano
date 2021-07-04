using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using APIPELICULA.DTOS;
using APIPELICULA.Entidades;
using APIPELICULA.Helpers;
using APIPELICULA.Servicios;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIPELICULA.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAlmacenadorArchivo _almacenadorArchivo;
        private readonly string contenedor = "peliculas";

        public PeliculasController(ApplicationDbContext context, IMapper mapper,
            IAlmacenadorArchivo 
            almacenadorArchivo )
        {
            _context = context;
            _mapper = mapper;
            _almacenadorArchivo = almacenadorArchivo;
        }

        [HttpGet]
        public async Task<ActionResult<PeliculaIndexDTO>> Get()
        {

            var top = 5;
            var hoy = DateTime.Today;
            
            var proximoExtrnos = await _context.Peliculas
                .Where(x => x.FechaEstreno > hoy)
                .OrderBy(x => x.FechaEstreno)
                .Take(top)
                .ToListAsync();

            var enCines = await _context.Peliculas
                .Where(x => x.EnCines)
                .Take(top)
                .ToListAsync();
            var resultado = new PeliculaIndexDTO();
            resultado.FuturosEstrenos = _mapper.Map<List<PeliculaDto>>(proximoExtrnos);
            resultado.EnCines = _mapper.Map<List<PeliculaDto>>(enCines);
            return resultado;

            //var pelicuas = await _context.Peliculas.ToListAsync();
            //return _mapper.Map<List<PeliculaDto>>(pelicuas);
        }

        [HttpGet("filtro")]
        public async Task<ActionResult<List<PeliculaDto>>> Filtrar([FromQuery] FiltroPelicullaDTO filtroPelicullaDto)
        {
            var peliculaQueryable = _context.Peliculas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtroPelicullaDto.Titulo))
            {
                peliculaQueryable = peliculaQueryable.Where(x => x.Titulo.Contains(filtroPelicullaDto.Titulo));
            }

            if (filtroPelicullaDto.EnCines)
            {
                peliculaQueryable = peliculaQueryable.Where(x => x.EnCines);
            }


            if (filtroPelicullaDto.ProximosEstrenos)
            {
                var hoy = DateTime.Today;
                peliculaQueryable = peliculaQueryable.Where(x => x.FechaEstreno > hoy);
            }

            if (filtroPelicullaDto.GeneroId != 0)
            {
                peliculaQueryable = peliculaQueryable
                    .Where(x => x.PeliclasGeneroses.Select(y => y.GeneroId)
                        .Contains(filtroPelicullaDto.GeneroId));
            }
            await HttpContext.InsertarParametrosPaginacion(peliculaQueryable,
                filtroPelicullaDto.CantidadRegistrosPorPagina);

            //  var peliculas = await peliculaQueryable.Paginar(filtroPelicullaDto.Paginacion).ToListAsync();

            return Ok();// _mapper.Map<List<PeliculaDto>>(peliculas);
        }

        [HttpGet("{id}", Name = "obtenerpelicula")]
        public async Task<ActionResult<PeliculaDto>> Get(int id)
        {
            var pelicuas = await _context.Peliculas
                .Include(x => x.PeliculasActoreses).ThenInclude(x => x.Actor)
                .Include(x => x.PeliclasGeneroses).ThenInclude(x => x.Genero)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (pelicuas == null)
            {
                return NotFound();
            }

            pelicuas.PeliculasActoreses = pelicuas.PeliculasActoreses.OrderBy(x => x.Orden).ToList();
            return _mapper.Map<PeliculaDetallesDTO>(pelicuas);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PeliculaCreacionDto peliculaCreacionDto)
        {
            var pelicula = _mapper.Map<Pelicula>(peliculaCreacionDto);
           
            if (peliculaCreacionDto.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDto.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extesion = Path.GetExtension(peliculaCreacionDto.Poster.FileName);
                    pelicula.Poster = await _almacenadorArchivo
                        .GuardarArchivo(contenido, extesion, contenedor,
                        peliculaCreacionDto.Poster.ContentType);
                }
            }
            asignarOrdenActores(pelicula);
            _context.Add(pelicula);
            await _context.SaveChangesAsync();
            var pelilaDto = _mapper.Map<PeliculaDto>(pelicula);
             return new CreatedAtRouteResult("obtenerpelicula",
                 new {id = pelicula.Id}, pelilaDto);
        }

        private void asignarOrdenActores(Pelicula pelicula)
        {
            if (pelicula.PeliculasActoreses != null)
            {
                for (int i = 0; i < pelicula.PeliculasActoreses.Count; i++)
                {
                    pelicula.PeliculasActoreses[i].Orden = i;
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromForm] PeliculaCreacionDto peliculaCreacionDto)
        {
            var peliculaDb = await _context.Peliculas
                .Include(x => x.PeliculasActoreses)
                .Include(x => x.PeliclasGeneroses)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (peliculaDb == null)
            {
                return NotFound();
                
            }

            peliculaDb = _mapper.Map(peliculaCreacionDto, peliculaDb);
            if (peliculaCreacionDto.Poster != null)
            {
                using (var memoryStrema = new MemoryStream())
                {
                    await peliculaCreacionDto.Poster.CopyToAsync(memoryStrema);
                    var contenido = memoryStrema.ToArray();
                    var extesion = Path.GetExtension(peliculaCreacionDto.Poster.FileName);
                    peliculaDb.Poster = await _almacenadorArchivo.EditarArchivo(contenido, extesion, contenedor,
                        peliculaDb.Poster,peliculaCreacionDto.Poster.ContentType);
                }
            }

            asignarOrdenActores(peliculaDb);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaPachtDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var entidadDb = await _context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDb == null)
            {
                return NotFound();
            }

            var entidadDto = _mapper.Map<PeliculaPachtDto>(entidadDb);
            patchDocument.ApplyTo(entidadDto,ModelState);

            var esValido = TryValidateModel(entidadDto);

            if (!esValido)
            {
                return BadRequest();
            }

            _mapper.Map(entidadDto, entidadDb);
            await _context.SaveChangesAsync();
            return NoContent();

        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Peliculas.AnyAsync(c => c.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            _context.Remove(new Pelicula() {Id = id});
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}