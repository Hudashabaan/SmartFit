using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.DTOs;

namespace SmartFit.Application.Features.Goals.Queries.GetGoalHistory
{
    public class GetGoalHistoryQueryHandler
        : IRequestHandler<GetGoalHistoryQuery, List<GoalDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetGoalHistoryQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<GoalDto>> Handle(
            GetGoalHistoryQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            var goals = await _context.Goals
                .AsNoTracking()
                .Where(g => g.UserId == userId)
                .OrderByDescending(g => g.StartDate) // 🔥 الأحدث الأول
                .ToListAsync(cancellationToken);

            return goals.Select(g => new GoalDto
            {
                Id = g.Id,
                StartWeight = g.StartWeight,
                TargetWeight = g.TargetWeight,
                DurationInDays = g.DurationInDays,
                Type = g.Type.ToString(),
                StartDate = g.StartDate,
                IsActive = g.IsActive
            }).ToList();
        }
    }
}
