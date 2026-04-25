using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Lifestyle.Commands.CompleteTask;
using SmartFit.Application.Features.Lifestyle.Commands.CreateTask;
using SmartFit.Application.Features.Lifestyle.Commands.DeleteTask;
using SmartFit.Application.Features.Lifestyle.Commands.UpdateTask;
using SmartFit.Application.Features.Lifestyle.DTOs;
using SmartFit.Application.Features.Lifestyle.Queries.GetProgress;
using SmartFit.Application.Features.Lifestyle.Queries.GetTodayTasks;

namespace SmartFit.API.Controllers
{
    [ApiController]
    [Route("api/lifestyle")]
    public class LifestyleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LifestyleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("task")]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            var result = await _mediator.Send(new CreateTaskCommand(dto));
            return Ok(result);
        }

        [HttpPost("complete")]
        public async Task<IActionResult> Complete(CompleteTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> Delete(Guid taskId)
        {
            var userId = User.FindFirst("uid")?.Value;

            var result = await _mediator.Send(new DeleteTaskCommand
            {
                TaskId = taskId,
                UserId = userId
            });

            return Ok(result);
        }

        [HttpGet("today")]
        public async Task<IActionResult> GetToday()
        {
            var userId = User.FindFirst("uid")?.Value;

            var result = await _mediator.Send(new GetTodayTasksQuery
            {
                UserId = userId
            });

            return Ok(result);
        }

        [HttpGet("progress")]
        public async Task<IActionResult> GetProgress()
        {
            var userId = User.FindFirst("uid")?.Value;

            var result = await _mediator.Send(new GetProgressQuery
            {
                UserId = userId
            });

            return Ok(result);
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> Update(Guid taskId, UpdateTaskCommand command)
        {
            command.TaskId = taskId;

            var userId = User.FindFirst("uid")?.Value;
            command.UserId = userId;

            var result = await _mediator.Send(command);

            return Ok(result);
        }

    }
}
