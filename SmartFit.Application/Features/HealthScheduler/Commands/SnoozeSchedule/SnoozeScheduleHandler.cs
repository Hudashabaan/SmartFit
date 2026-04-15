using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Exceptions;
using SmartFit.Application.Common.Interfaces;

namespace SmartFit.Application.Features.HealthScheduler.Commands.SnoozeSchedule
{

    public class SnoozeScheduleHandler
        : IRequestHandler<SnoozeScheduleCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public SnoozeScheduleHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            SnoozeScheduleCommand request,
            CancellationToken cancellationToken)
        {
            var schedule = await _context.HealthSchedules
                .FirstOrDefaultAsync(x =>
                    x.Id == request.Id &&
                    x.UserId == _currentUser.UserId);

            if (schedule == null)
                throw new NotFoundException("Schedule not found");

            // 🔥 Business Logic
            schedule.Time = schedule.Time.AddMinutes(request.Minutes);
            schedule.IsCompleted = false;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        Task IRequestHandler<SnoozeScheduleCommand>.Handle(SnoozeScheduleCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
