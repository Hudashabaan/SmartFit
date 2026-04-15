using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Profile.Commands.CreateProfile;
using SmartFit.Application.Features.Profile.Commands.UpdateAge;
using SmartFit.Application.Features.Profile.Commands.UpdateGender;

using SmartFit.Application.Features.Profile.Commands.UpdateHeight;
using SmartFit.Application.Features.Profile.Commands.UpdateProfile;
using SmartFit.Application.Features.Profile.Commands.UpdateWeight;
using SmartFit.Application.Features.Profile.Queries.GetProfile;
using System.Security.Claims;

namespace SmartFit.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var result = await _mediator.Send(new CreateProfileCommand());

            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProfileCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _mediator.Send(new GetProfileQuery());
            return Ok(result);
        }

        [HttpPut("gender")]
        [Authorize]
        public async Task<IActionResult> UpdateGender(UpdateGenderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("age")]
        [Authorize]
        public async Task<IActionResult> UpdateAge(UpdateAgeCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("height")]
        [Authorize]
        public async Task<IActionResult> UpdateHeight(UpdateHeightCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("weight")]
        [Authorize]
        public async Task<IActionResult> UpdateWeight(UpdateWeightCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

       
    }
}
