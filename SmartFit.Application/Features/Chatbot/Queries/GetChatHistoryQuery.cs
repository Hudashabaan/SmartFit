using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SmartFit.Application.Features.Chatbot.Queries
{
    public class GetChatHistoryQuery : IRequest<List<ChatMessageDto>>
    {
        public string UserId { get; set; }

        // 🔥 Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
