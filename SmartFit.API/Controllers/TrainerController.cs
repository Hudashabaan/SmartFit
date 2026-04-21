using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Trainer.Commands.AcceptInvite;
using SmartFit.Application.Features.Trainer.Commands.CreateInvite;
using SmartFit.Application.Features.Trainer.DTOs;
using SmartFit.Application.Features.Trainer.Queries.GetClientDetails;
using SmartFit.Application.Features.Trainer.Queries.GetTrainerClients;

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


    }
    

}
