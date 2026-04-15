using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.BodyAnalysisFeature.Commands.CreateBodyAnalysis;
using SmartFit.Application.Features.Profile.Queries.GetUserBodyAnalyses;
using Microsoft.AspNetCore.Authorization;


namespace SmartFit.API.Controllers
{
    [Authorize]
    [ApiController]
	[Route("api/[controller]")]
	public class BodyAnalysisController : ControllerBase
	{
		private readonly IMediator _mediator;

		public BodyAnalysisController(IMediator mediator)
		{
			_mediator = mediator;
		}

		

		[Authorize]
		[HttpPost("analyze")]
		public async Task<IActionResult> Analyze([FromForm] CreateBodyAnalysisCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[Authorize]
		[HttpGet("history")]
		public async Task<IActionResult> GetHistory()
		{
			var result = await _mediator.Send(new GetUserBodyAnalysesQuery());
			return Ok(result);
		}
	}
}
