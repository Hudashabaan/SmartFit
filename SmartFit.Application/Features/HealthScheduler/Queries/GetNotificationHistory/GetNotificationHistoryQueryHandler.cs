using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.HealthScheduler.DTOs;

namespace SmartFit.Application.Features.HealthScheduler.Queries.GetNotificationHistory
{
    public class GetNotificationHistoryQueryHandler
        : IRequestHandler<GetNotificationHistoryQuery, List<NotificationHistoryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetNotificationHistoryQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<NotificationHistoryDto>> Handle(
            GetNotificationHistoryQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            var notifications = await _context.NotificationHistories
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.SentAt)
                .Select(x => new NotificationHistoryDto
                {
                    Title = x.Title,
                    Message = x.Message,
                    SentAt = x.SentAt
                })
                .ToListAsync(cancellationToken);

            return notifications;
        }
    }
}
