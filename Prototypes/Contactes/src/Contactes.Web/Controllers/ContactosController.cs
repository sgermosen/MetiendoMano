using Contactes.Web.Models;
using Contactes.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Contactes.Web.Controllers
{
    public class ContactosController : Controller
    {
        private readonly DataContex _context;

        public ContactosController(DataContex context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Listado()
        {
            var contactos = _context.Personas.ToList();
            return View(contactos);
        }

        [Authorize]
        public IActionResult Create()
        {
            var tipos = _context.Tipos;

            var vm = new PersonaViewModel
            {
                TiposDeContactos = new SelectList(tipos, "Identificador", "Nombre"),
                TiposDeContactosx = tipos
            };

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(PersonaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var persona = new Persona
            {
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Direccion = vm.Direccion,
                Email = vm.Email,
                Telefono = vm.Telefono,
                TipoIdentificador = vm.TipoIdentificador
            };

            _context.Add(persona);

            _context.SaveChanges();


            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EliminarContacto(int identificador)
        {
            var contacto = await _context.Personas.FindAsync(identificador);

            contacto.Eliminado = true;
            await _context.SaveChangesAsync();
            return Ok();
        }



    }
}