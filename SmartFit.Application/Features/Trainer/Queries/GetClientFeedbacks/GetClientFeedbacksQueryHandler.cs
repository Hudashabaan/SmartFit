using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Trainer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Trainer.Queries.GetClientFeedbacks
{
    public class GetClientFeedbacksQueryHandler
    : IRequestHandler<GetClientFeedbacksQuery, List<FeedbackDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetClientFeedbacksQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<FeedbackDto>> Handle(
            GetClientFeedbacksQuery request,
            CancellationToken cancellationToken)
        {
            // 🔥 Security: Client يشوف نفسه بس
            if (_currentUser.UserId != request.ClientId)
                throw new Exception("Unauthorized");

            var feedbacks = await _context.Feedbacks
                .Where(x => x.ClientId == request.ClientId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new FeedbackDto
                {
                    Id = x.Id,
                    Message = x.Message,
                    CreatedAt = x.CreatedAt,

                    TrainerName = _context.Users
                   .Where(u => u.Id == x.TrainerId)
                        .Select(u => u.FullName)
                          .FirstOrDefault()
                })
                .ToListAsync(cancellationToken);

            return feedbacks;
        }
    }
}
