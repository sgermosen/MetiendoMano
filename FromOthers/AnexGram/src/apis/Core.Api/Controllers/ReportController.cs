using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Shared;
using Services;

namespace Core.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(
            IReportService reportService
        )
        {
            _reportService = reportService;
        }

        [HttpPost("reports/GreaterUsersParticipation")]
        public async Task<IActionResult> GetAll(
            [FromBody] DataGrid data
        )
        {
            return Ok(
                await _reportService.GreaterUsersParticipation(data)
            );
        }
    }
}
