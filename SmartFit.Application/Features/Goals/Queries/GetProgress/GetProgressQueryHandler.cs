using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Common.Exceptions;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Goals.Queries.GetProgress
{
    public class GetProgressQueryHandler
        : IRequestHandler<GetProgressQuery, double>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetProgressQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<double> Handle(
            GetProgressQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            // 1. نجيب Goal الحالي
            var goal = await _context.Goals
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    g => g.UserId == userId && g.IsActive,
                    cancellationToken);

            if (goal == null)
                throw new NotFoundException("No active goal found");

            // 🔥 Guard Clause
            if (goal.StartWeight == goal.TargetWeight)
                return 100;

            // 2. نجيب Profile
            var profile = await _context.UserProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            var currentWeight = profile.Weight;

            double progress = 0;

            // 3. الحساب حسب النوع الجديد
            switch (goal.Type)
            {
                case GoalType.LoseWeight:
                    progress = (goal.StartWeight - currentWeight) /
                               (goal.StartWeight - goal.TargetWeight);
                    break;

                case GoalType.GainMuscle:
                    progress = (currentWeight - goal.StartWeight) /
                               (goal.TargetWeight - goal.StartWeight);
                    break;

                case GoalType.MaintainWeight:
                    return 100;
            }

            // 4. Clamp
            progress = Math.Max(0, Math.Min(progress, 1));

            // 5. Percentage
            return progress * 100;
        }
    }
}