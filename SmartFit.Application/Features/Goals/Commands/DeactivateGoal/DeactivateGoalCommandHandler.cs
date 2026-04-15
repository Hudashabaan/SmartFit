using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Common.Exceptions;

namespace SmartFit.Application.Features.Goals.Commands.DeactivateGoal
{
    public class DeactivateGoalCommandHandler
        : IRequestHandler<DeactivateGoalCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public DeactivateGoalCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            DeactivateGoalCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            var goal = await _context.Goals
                .FirstOrDefaultAsync(
                    g => g.UserId == userId && g.IsActive,
                    cancellationToken);

            if (goal == null)
                throw new NotFoundException("No active goal found");

            // ✅ بدل Delete
            goal.Deactivate();

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}