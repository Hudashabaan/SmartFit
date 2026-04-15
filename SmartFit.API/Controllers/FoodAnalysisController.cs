using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.FoodAnalysisFeature.Commands.AnalyzeFood;

namespace SmartFit.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodAnalysisController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FoodAnalysisController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 🔥 Analyze Food
        [HttpPost("analyze")]
        [Authorize] // 🔐 لازم يكون مسجل دخول
        public async Task<IActionResult> AnalyzeFood([FromForm] AnalyzeFoodCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
