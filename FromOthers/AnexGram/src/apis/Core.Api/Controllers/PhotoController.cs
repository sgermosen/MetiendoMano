using System.Threading.Tasks;
using Core.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model.Shared;
using Services;

namespace Core.Api.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PhotoController(
            IPhotoService photoService,
            IHostingEnvironment hostingEnvironment
        )
        {
            _photoService = photoService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("photos")]
        public async Task<IActionResult> GetAll(
            [FromQuery] ApiFilter queryFilter
        )
        {
            return Ok(
                await _photoService.GetAll(queryFilter)
            );
        }

        [HttpPost("photos")]
        public async Task<IActionResult> ImagePut(
            [FromBody] PhotoCreateContainerViewModel container
        )
        {
            await _photoService.Create(
                container.Model,
                container.File
            );

            return NoContent();
        }
    }
}
