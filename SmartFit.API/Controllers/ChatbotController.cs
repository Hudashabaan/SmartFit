using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Chatbot.Commands;
using SmartFit.Application.Features.Chatbot.DTOs;
using SmartFit.Application.Features.Chatbot.Queries;

namespace SmartFit.API.Controllers
{
    [ApiController]
    [Route("api/chatbot")]
    public class ChatbotController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatbotController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageDto dto)
        {
            var result = await _mediator.Send(new SendMessageCommand
            {
                Message = dto.Message,
                UserId = "current-user-id"
            });

            return Ok(result);
        }
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory(int pageNumber = 1, int pageSize = 20)
        {
            var userId = "current-user-id"; // بعدين من JWT

            var result = await _mediator.Send(new GetChatHistoryQuery
            {
                UserId = userId,
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            return Ok(result);
        }
    }
}
