using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Dashboard.Queries.GetDashboard;

namespace SmartFit.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDashboard([FromQuery] DateTime date)
        {
            var result = await _mediator.Send(new GetDashboardQuery
            {
                Date = date
            });

            return Ok(result);
        }
    }
}
