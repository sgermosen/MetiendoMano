using System.Collections.Generic;
using System.Threading.Tasks;
using APIPELICULA.DTOS;
using APIPELICULA.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIPELICULA.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    { 
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenerosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<GeneroDto>>> Get()
        {
            var entidades =  await _context.Generos.ToListAsync();
            var dtos = _mapper.Map<List<GeneroDto>>(entidades);
            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenerGenero")]
        public async Task<ActionResult<GeneroDto>> Get(int id)
        {
            var entidad = await _context.Generos.FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<GeneroDto>(entidad);
            return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDto generoCreacionDto)
        {
            var entidad = _mapper.Map<Genero>(generoCreacionDto);
            _context.Add(entidad);
            await _context.SaveChangesAsync();
            var generoDto = _mapper.Map<GeneroDto>(entidad);
            return  new CreatedAtRouteResult("obtenerGenero", new {id = generoDto.Id}, generoDto);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> Put(int id, [FromBody] GeneroCreacionDto generoCreacionDto)
        {
            var entidad = _mapper.Map<Genero>(generoCreacionDto);
            entidad.Id = id;
            _context.Entry(entidad).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Generos.AnyAsync(c => c.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            _context.Remove(new Genero() {Id = id});
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}