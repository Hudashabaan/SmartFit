using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Admin.Users.Commands.DisableUser;
using SmartFit.Application.Features.Admin.Users.Queries.GetAllUsers;

namespace SmartFit.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all users
        /// Admin + Support
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Support")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());

            return Ok(result);
        }

        /// <summary>
        /// Disable user account
        /// Admin only
        /// </summary>
        [HttpPost("disable")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DisableUser(
            [FromBody] DisableUserCommand command)
        {
            if (command == null)
                return BadRequest("Invalid request");

            var result = await _mediator.Send(command);

            if (!result)
                return NotFound("User not found");

            return Ok("User disabled successfully");
        }
    }
}