using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Trainer.Commands.AcceptInvite;
using SmartFit.Application.Features.Trainer.Commands.AddFeedback;
using SmartFit.Application.Features.Trainer.Commands.CreateInvite;
using SmartFit.Application.Features.Trainer.Commands.RemoveClient;
using SmartFit.Application.Features.Trainer.DTOs;
using SmartFit.Application.Features.Trainer.Queries.GetClientDetails;
using SmartFit.Application.Features.Trainer.Queries.GetClientFeedbacks;
using SmartFit.Application.Features.Trainer.Queries.GetTrainerClients;
using SmartFit.Application.Features.Trainer.Queries.GetWeeklyProgress;
using SmartFit.Application.Features.Trainer.Queries.GetWeightHistory;

namespace SmartFit.API.Controllers
{
   

        [ApiController]
        [Route("api/[controller]")]
        public class TrainerController : ControllerBase
        {
            private readonly IMediator _mediator;

            public TrainerController(IMediator mediator)
            {
                _mediator = mediator;
            }

            // ✅ Create Invite (Trainer فقط)
            [HttpPost("create-invite")]
            [Authorize]
            public async Task<IActionResult> CreateInvite([FromBody] CreateInviteDto dto)
            {
                var result = await _mediator.Send(
                    new CreateInviteCommand { Dto = dto });

                return Ok(new
                {
                    Message = "Invite created successfully",
                    Code = result
                });
            }

            // ✅ Accept Invite (Client)
            [HttpPost("accept-invite")]
            [Authorize]
            public async Task<IActionResult> AcceptInvite([FromBody] AcceptInviteDto dto)
            {
                var result = await _mediator.Send(
                    new AcceptInviteCommand { Dto = dto });

                return Ok(new
                {
                    Message = result
                });
            }

        [HttpGet("clients")]
        [Authorize]
        public async Task<IActionResult> GetTrainerClients()
        {
            var result = await _mediator.Send(new GetTrainerClientsQuery());

            return Ok(result);
        }


        [HttpGet("client-details/{clientId}")]
        [Authorize]
        public async Task<IActionResult> GetClientDetails(string clientId)
        {
            var result = await _mediator.Send(
                new GetClientDetailsQuery { ClientId = clientId });

            return Ok(result);
        }

        [HttpPost("feedback")]
        public async Task<IActionResult> AddFeedback(AddFeedbackCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("feedbacks")]
        public async Task<IActionResult> GetMyFeedbacks()
        {
            var userId = User.FindFirst("uid")?.Value;

            var result = await _mediator.Send(new GetClientFeedbacksQuery
            {
                ClientId = userId
            });

            return Ok(result);
        }

        [HttpDelete("remove-client")]
        public async Task<IActionResult> RemoveClient([FromBody] RemoveClientCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("weekly-progress/{clientId}")]
        public async Task<IActionResult> GetWeeklyProgress(string clientId)
        {
            var result = await _mediator.Send(new GetWeeklyProgressQuery
            {
                ClientId = clientId
            });

            return Ok(result);
        }

        [HttpGet("weight-history/{clientId}")]
        public async Task<IActionResult> GetWeightHistory(string clientId)
        {
            var result = await _mediator.Send(new GetWeightHistoryQuery
            {
                ClientId = clientId
            });

            return Ok(result);
        }


    }
    

}
