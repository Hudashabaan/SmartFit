using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Goals.Commands.CreateGoal;
using SmartFit.Application.Features.Goals.Commands.DeactivateGoal;
using SmartFit.Application.Features.Goals.Commands.UpdateGoal;
using SmartFit.Application.Features.Goals.Queries.GetCurrentGoal;
using SmartFit.Application.Features.Goals.Queries.GetGoalHistory;
using SmartFit.Application.Features.Goals.Queries.GetProgress;
namespace SmartFit.API.Controllers
{
 

    [ApiController]
    [Route("api/[controller]")]
    public class GoalsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GoalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateGoalCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentGoal()
        {
            var result = await _mediator.Send(new GetCurrentGoalQuery());
            return Ok(result);
        }

        [Authorize]
        [HttpGet("progress")]
        public async Task<IActionResult> GetProgress()
        {
            var result = await _mediator.Send(new GetProgressQuery());
            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateGoalCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Deactivate()
        {
            await _mediator.Send(new DeactivateGoalCommand());
            return NoContent();
        }

        [Authorize]
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var result = await _mediator.Send(new GetGoalHistoryQuery());
            return Ok(result);
        }
    }
}
