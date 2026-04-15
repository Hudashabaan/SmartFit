using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Features.Goals.Commands.CreateGoal
{
    public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateGoalCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Guid> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
        {
            // 1. نجيب اليوزر الحالي
            var userId = _currentUser.UserId;

            // 2. نجيب Profile علشان نعرف الوزن الحالي
            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                throw new Exception("Profile not found");

            var startWeight = profile.Weight;

            // 3. نقفل أي Goal قديم
            var oldGoals = await _context.Goals
                .Where(g => g.UserId == userId && g.IsActive)
                .ToListAsync();

            foreach (var goal in oldGoals)
            {
                goal.IsActive = false;
            }

            // 4. نعمل Goal جديد
            var newGoal = new Goal
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                StartWeight = startWeight,
                TargetWeight = request.TargetWeight,
                DurationInDays = request.DurationInDays,
                Type = request.Type,
                StartDate = DateTime.UtcNow,
                IsActive = true
            };

            _context.Goals.Add(newGoal);

            // 5. Save
            await _context.SaveChangesAsync(cancellationToken);

            return newGoal.Id;
        }
    }
}
