using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cabanas.API.Models;
using Cabanas.API.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Cabanas.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]
    [ApiController]
    public class CabanasController : ControllerBase
    {
        private readonly CabanasContext _context;

        public CabanasController(CabanasContext context)
        {
            _context = context;
        }

        // GET: api/Cabanas
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Cabana>))]
        public async Task<ActionResult<IEnumerable<Cabana>>> GetCabanas(string sortBy, string searchString, int? pageIndex)
        {
            IQueryable<Cabana> cabanasIQ = from c in _context.Cabanas
                                            select c;

            //Searching (non case-sensitive)
            if (!String.IsNullOrEmpty(searchString))
            {
                cabanasIQ = cabanasIQ.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                       || s.Address.ToUpper().Contains(searchString.ToUpper()));
            }
            //Sorting
            switch (sortBy)
            {
                case "name":
                    cabanasIQ = cabanasIQ.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    cabanasIQ = cabanasIQ.OrderByDescending(s => s.Name);
                    break;
                case "address":
                    cabanasIQ = cabanasIQ.OrderBy(s => s.Address);
                    break;
                case "address_desc":
                    cabanasIQ = cabanasIQ.OrderByDescending(s => s.Address);
                    break;
                default:
                    cabanasIQ = cabanasIQ.OrderBy(s => s.Id);
                    break;
            }
            //Paging
            int pageSize = 10;
            var paginatedResult = await PaginatedList<Cabana>.CreateAsync(
                cabanasIQ, pageIndex ?? 1, pageSize);
            return paginatedResult;
        }

        // GET: api/Cabanas/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Cabana))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Cabana>> GetCabana(int id)
        {
            var cabana = await _context.Cabanas.FindAsync(id);

            if (cabana == null)
            {
                return NotFound();
            }

            return cabana;
        }

        // PUT: api/Cabanas/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutCabana(int id, Cabana cabana)
        {
            if (id != cabana.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Entry(cabana).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CabanaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cabanas
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Cabana))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Cabana>> PostCabana(Cabana cabana)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Cabanas.Add(cabana);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCabana", new { id = cabana.Id }, cabana);
        }

        // DELETE: api/Cabanas/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Cabana))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Cabana>> DeleteCabana(int id)
        {
            var cabana = await _context.Cabanas.FindAsync(id);
            if (cabana == null)
            {
                return NotFound();
            }

            _context.Cabanas.Remove(cabana);
            await _context.SaveChangesAsync();

            return cabana;
        }

        private bool CabanaExists(int id)
        {
            return _context.Cabanas.Any(e => e.Id == id);
        }
    }
}
