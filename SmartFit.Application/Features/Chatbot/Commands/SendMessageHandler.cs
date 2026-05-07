using MediatR;
using SmartFit.Application.Features.Chatbot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Chatbot.Commands
{
    public class SendMessageHandler : IRequestHandler<SendMessageCommand, string>
    {
        private readonly IChatbotService _chatbot;

        public SendMessageHandler(IChatbotService chatbot)
        {
            _chatbot = chatbot;
        }

        public async Task<string> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            return await _chatbot.GenerateResponse(request.Message, request.UserId);
        }
    }
}
