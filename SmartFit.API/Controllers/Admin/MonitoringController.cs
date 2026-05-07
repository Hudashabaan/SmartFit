using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Admin.Monitoring.Queries.GetAdminMonitoring;

namespace SmartFit.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/monitoring")]
    public class MonitoringController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MonitoringController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get monitoring dashboard statistics
        /// Admin + Support
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Support")]
        public async Task<IActionResult> GetMonitoring()
        {
            var result = await _mediator.Send(new GetAdminMonitoringQuery());

            return Ok(result);
        }
    }
}