using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Admin.Logs.Queries.GetLogs;

namespace SmartFit.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/logs")]
    public class LogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get admin logs
        /// Admin + Support
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Support")]
        public async Task<IActionResult> GetLogs()
        {
            var result = await _mediator.Send(new GetLogsQuery());

            return Ok(result);
        }
    }
}
