using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Common.Exceptions;

namespace SmartFit.Application.Features.Goals.Commands.UpdateGoal
{
    public class UpdateGoalCommandHandler
        : IRequestHandler<UpdateGoalCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public UpdateGoalCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            UpdateGoalCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            // 🔥 Validation الأول
            if (request.TargetWeight <= 0)
                throw new Exception("Invalid weight");

            if (request.DurationInDays <= 0)
                throw new Exception("Invalid duration");

            // 1. نجيب الـ Goal الحالي
            var goal = await _context.Goals
                .FirstOrDefaultAsync(
                    g => g.UserId == userId && g.IsActive,
                    cancellationToken);

            if (goal == null)
                throw new NotFoundException("No active goal found");

            // 2. نحدث
            goal.Update(
                request.TargetWeight,
                request.DurationInDays,
                request.Type
            );

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
