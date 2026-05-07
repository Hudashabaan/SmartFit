using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Admin.Images.Commands.UpdateImageStatus;
using SmartFit.Application.Features.Admin.Images.Queries.GetAllImages;

namespace SmartFit.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/images")]
    public class ImageReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImageReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all body and food images
        /// Admin + Support
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Support")]
        public async Task<IActionResult> GetAllImages()
        {
            var result = await _mediator.Send(new GetAllImagesQuery());

            return Ok(result);
        }

        /// <summary>
        /// Approve or reject image
        /// Admin only
        /// </summary>
        [HttpPost("status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(
            [FromBody] UpdateImageStatusCommand command)
        {
            if (command == null)
                return BadRequest("Invalid request");

            var result = await _mediator.Send(command);

            if (!result)
                return BadRequest("Failed to update image status");

            return Ok("Image status updated successfully");
        }
    }
}