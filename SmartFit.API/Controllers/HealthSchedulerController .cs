using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SmartFit.Application.Features.HealthScheduler.Commands.CreateSchedule;
using SmartFit.Application.Features.HealthScheduler.Queries.GetUserSchedules;
using SmartFit.Application.Features.HealthScheduler.Commands.MarkScheduleAsDone;
using SmartFit.Application.Features.HealthScheduler.Commands.SnoozeSchedule;
using SmartFit.Application.Features.HealthScheduler.Queries.GetTodaySchedules;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;
using SmartFit.Application.Features.HealthScheduler.Queries.GetNotificationHistory;

namespace SmartFit.API.Controllers
{
  

    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 👈 هنا
    public class HealthSchedulerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUser;


        public HealthSchedulerController(IMediator mediator, UserManager<ApplicationUser> userManager,
        ICurrentUserService currentUser)
        {
            _mediator = mediator;
            _userManager = userManager;
            _currentUser = currentUser;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateScheduleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserSchedules()
        {
            var result = await _mediator.Send(new GetUserSchedulesQuery());
            return Ok(result);
        }

        [HttpPut("{id}/done")]
        public async Task<IActionResult> MarkAsDone(Guid id)
        {
            await _mediator.Send(new MarkScheduleAsDoneCommand { Id = id });
            return NoContent();
        }

        [HttpPut("{id}/snooze")]
        public async Task<IActionResult> Snooze(Guid id, [FromQuery] int minutes)
        {
            await _mediator.Send(new SnoozeScheduleCommand
            {
                Id = id,
                Minutes = minutes
            });

            return NoContent();
        }

        [HttpGet("today")]
        public async Task<IActionResult> GetTodaySchedules()
        {
            var result = await _mediator.Send(new GetTodaySchedulesQuery());
            return Ok(result);
        }


        [HttpPost("fcm-token")]
        public async Task<IActionResult> SaveFcmToken([FromBody] string token)
        {
            var userId = _currentUser.UserId;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            user.FcmToken = token;

            await _userManager.UpdateAsync(user);

            return Ok();
        }



        // 💥 Notification History Endpoint
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var result = await _mediator.Send(new GetNotificationHistoryQuery());
            return Ok(result);
        }
    }
}
