using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.DTOs;

namespace SmartFit.Application.Features.Goals.Queries.GetCurrentGoal
{
    public class GetCurrentGoalQueryHandler
        : IRequestHandler<GetCurrentGoalQuery, GoalDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetCurrentGoalQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<GoalDto> Handle(
            GetCurrentGoalQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            var goal = await _context.Goals
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    g => g.UserId == userId && g.IsActive,
                    cancellationToken);

            if (goal == null)
                throw new Exception("No active goal found");

            return new GoalDto
            {
                Id = goal.Id,
                StartWeight = goal.StartWeight,
                TargetWeight = goal.TargetWeight,
                DurationInDays = goal.DurationInDays,
                Type = goal.Type.ToString(),
                StartDate = goal.StartDate,
                IsActive = goal.IsActive
            };
        }
    }
}
