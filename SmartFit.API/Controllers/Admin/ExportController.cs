using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Admin.Export.Queries.ExportUsersCsv;

namespace SmartFit.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/export")]
    public class ExportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Export users to CSV
        /// Admin only
        /// </summary>
        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ExportUsers()
        {
            var file = await _mediator.Send(new ExportUsersCsvQuery());

            return File(
                file,
                "text/csv",
                "users.csv");
        }
    }
}
