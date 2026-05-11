using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features
    .ExerciseRecommendations.Queries
    .GetExerciseRecommendation;
using SmartFit.Application.Features.ExerciseRecommendations.Queries.GetExerciseRecommendationHistory;

namespace SmartFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExerciseRecommendationsController
        : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExerciseRecommendationsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GetRecommendation(
        GetExerciseRecommendationQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<IActionResult>
       GetHistory()
        {
            var result = await _mediator.Send(
                new GetExerciseRecommendationHistoryQuery());

            return Ok(result);
        }
    }
}
