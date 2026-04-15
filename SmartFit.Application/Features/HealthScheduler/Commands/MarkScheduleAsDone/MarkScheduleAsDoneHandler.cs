using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Exceptions;
using SmartFit.Application.Common.Interfaces;
using System;


namespace SmartFit.Application.Features.HealthScheduler.Commands.MarkScheduleAsDone

{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using SmartFit.Application.Common.Interfaces;

    public class MarkScheduleAsDoneHandler
        : IRequestHandler<MarkScheduleAsDoneCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public MarkScheduleAsDoneHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            MarkScheduleAsDoneCommand request,
            CancellationToken cancellationToken)
        {
            var schedule = await _context.HealthSchedules
                .FirstOrDefaultAsync(x =>
                    x.Id == request.Id &&
                    x.UserId == _currentUser.UserId);

            if (schedule == null)
                throw new NotFoundException("Schedule not found");

            if (schedule.IsCompleted)
                throw new BadRequestException("Schedule already completed");

            schedule.IsCompleted = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value; // 👈 مهم جدًا
        }

        Task IRequestHandler<MarkScheduleAsDoneCommand>.Handle(MarkScheduleAsDoneCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
