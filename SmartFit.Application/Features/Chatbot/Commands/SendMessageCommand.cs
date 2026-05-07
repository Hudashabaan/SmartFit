using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Chatbot.Commands
{
    public class SendMessageCommand : IRequest<string>
    {
        public string Message { get; set; }
        public string UserId { get; set; }
    }
}
