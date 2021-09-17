using System.Collections.Generic;
using System.IO;
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
    [Route("api/acotres")]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAlmacenadorArchivo _almacenadorArchivo;
        private readonly string contenedor = "actore";


        public ActoresController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivo almacenadorArchivo )
        {
            _context = context;
            _mapper = mapper;
            _almacenadorArchivo = almacenadorArchivo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDto>>> Get([FromQuery] PagonacionDto pagonacionDto)
        {
            var queryble = _context.Actores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacion(queryble, pagonacionDto.CantidadRegistrosPorPagina);
            var entidades = await queryble.Paginar(pagonacionDto).ToListAsync();
            return _mapper.Map<List<ActorDto>>(entidades);
            
        }

        [HttpGet("{id}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDto>> Get(int id)
        {
            var entidad = await _context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }

            return _mapper.Map<ActorDto>(entidad);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDto actorCreacionDto)
        {
            var entidad = _mapper.Map<Actor>(actorCreacionDto);
            if (actorCreacionDto.Foto != null)
            {
                using (var memoryStrema = new MemoryStream())
                {
                    await actorCreacionDto.Foto.CopyToAsync(memoryStrema);
                    var contenido = memoryStrema.ToArray();
                    var extesion = Path.GetExtension(actorCreacionDto.Foto.FileName);
                    entidad.Foto = await _almacenadorArchivo.GuardarArchivo(contenido, extesion, contenedor,
                        actorCreacionDto.Foto.ContentType);
                }
            }
            _context.Add(entidad);
            await _context.SaveChangesAsync();
            var dto = _mapper.Map<ActorDto>(entidad);
            return new CreatedAtRouteResult("obtenerActor", new {id = entidad.Id}, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreacionDto actorCreacionDto)
        {
            var actoDb = await _context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (actoDb == null)
            {
                return NotFound();
                
            }

            actoDb = _mapper.Map(actorCreacionDto, actoDb);
            if (actorCreacionDto.Foto != null)
            {
                using (var memoryStrema = new MemoryStream())
                {
                    await actorCreacionDto.Foto.CopyToAsync(memoryStrema);
                    var contenido = memoryStrema.ToArray();
                    var extesion = Path.GetExtension(actorCreacionDto.Foto.FileName);
                    actoDb.Foto = await _almacenadorArchivo.EditarArchivo(contenido, extesion, contenedor,
                        actoDb.Foto,actorCreacionDto.Foto.ContentType);
                }
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPacthDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var entidadDb = await _context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDb == null)
            {
                return NotFound();
            }

            var entidadDto = _mapper.Map<ActorPacthDto>(entidadDb);
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
            var existe = await _context.Actores.AnyAsync(c => c.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            _context.Remove(new Actor() {Id = id});
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}