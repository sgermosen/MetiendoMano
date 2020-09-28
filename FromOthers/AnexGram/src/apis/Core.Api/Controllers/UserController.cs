using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model.Shared;
using Services;

namespace Core.Api.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UserController(
            IUserService userService,
            IHostingEnvironment hostingEnvironment
        )
        {
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAll([FromQuery] ApiFilter queryFilter)
        {
            return Ok(
                await _userService.GetAll(queryFilter)
            );
        }

        /// Agregué el identifier porque este endpoint retorna un solo registro
        /// y de esta manera evito que tenga conflicto con el endpoint que trae todo
        /// los registros.
        /// 
        /// El identifier no lo usamos para nada, simplemente es un decorador para nuestra
        /// definir nuestra ruta del GET
        [HttpGet("users/{identifier}")]
        public async Task<IActionResult> Get([FromQuery] ApiFilter queryFilter)
        {
            return Ok(
                await _userService.GetByFilter(queryFilter)
            );
        }

        [HttpPatch("users/{id}")]
        public async Task<IActionResult> Patch(
            string id,
            [FromBody]UserPartialDto model
        )
        {
            await _userService.PartialUpdate(id, model);

            return NoContent();
        }

        [HttpPut("users/{id}/image")]
        public async Task<IActionResult> ImagePut(
            string id,
            [FromBody]FileDto model
        )
        {
            var fileName = model.UniqueName;
            var filePath = $"{_hostingEnvironment.WebRootPath}\\Uploads\\{fileName}";

            using (var ms = model.ReadAsStream())
            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                await ms.CopyToAsync(file);
            }

            await _userService.PartialUpdate(id, new UserPartialDto {
                Image = fileName
            });

            return NoContent();
        }
    }
}
