using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Chatbot.Queries
{
    public class ChatMessageDto
    {
        public string Message { get; set; }
        public bool IsFromUser { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
