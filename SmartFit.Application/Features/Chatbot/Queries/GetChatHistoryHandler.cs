using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;

namespace SmartFit.Application.Features.Chatbot.Queries
{
    public class GetChatHistoryHandler : IRequestHandler<GetChatHistoryQuery, List<ChatMessageDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetChatHistoryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChatMessageDto>> Handle(GetChatHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _context.ChatMessages
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.CreatedAt) // 🔥 أحدث الأول
                .Skip((request.PageNumber - 1) * request.PageSize) // 🔥
                .Take(request.PageSize) // 🔥
                .Select(x => new ChatMessageDto
                {
                    Message = x.Message,
                    IsFromUser = x.IsFromUser,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
