using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Shared;
using Services;

namespace Core.Api.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(
            ICommentService commentService
        )
        {
            _commentService = commentService;
        }

        [HttpGet("comments")]
        public async Task<IActionResult> GetAll(
            [FromQuery] ApiFilter queryFilter
        )
        {
            return Ok(
                await _commentService.GetAll(queryFilter)
            );
        }

        [HttpPost("comments")]
        public async Task<IActionResult> ImagePut(
            [FromBody] CommentCreateDto model
        )
        {
            await _commentService.Create(model);

            return NoContent();
        }
    }
}
