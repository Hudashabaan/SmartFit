using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Profile.Commands.CreateProfile;
using SmartFit.Application.Features.Profile.Commands.UpdateProfile;
using SmartFit.Application.Features.Profile.Queries.GetProfile;

namespace SmartFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _mediator.Send(new GetProfileQuery());

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(
        UpdateProfileCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile(
        CreateProfileCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}