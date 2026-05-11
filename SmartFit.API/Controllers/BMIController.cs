using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.BMI.Commands.PredictBMI;
using SmartFit.Application.Features.BMI.Queries.GetBMIHistory;

namespace SmartFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BMIController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BMIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("predict")]
        public async Task<IActionResult> Predict(
            PredictBMICommand command)
        {
            var result =
                await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var result =
                await _mediator.Send(
                    new GetBMIHistoryQuery());

            return Ok(result);
        }
    }
}
