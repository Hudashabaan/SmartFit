using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.CaloriesPredictions.Commands;
using SmartFit.Application.Features.CaloriesPredictions.Queries.GetMyCaloriesPredictions;

namespace SmartFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CaloriesPredictionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CaloriesPredictionController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PredictCalories(
            CreateCaloriesPredictionCommand command)
        {
            var result =
                await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("my-history")]
        public async Task<IActionResult>
        GetMyHistory()
        {
            var result =
                await _mediator.Send(
                    new GetMyCaloriesPredictionsQuery());

            return Ok(result);
        }
    }
}
