using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Lifestyle.Commands.CompleteTask
{
    public class CompleteTaskHandler : IRequestHandler<CompleteTaskCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public CompleteTaskHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == request.TaskId, cancellationToken);

            if (task == null)
                throw new Exception("Task not found");

            if (task.UserId != request.UserId)
                throw new Exception("Unauthorized");

            var today = DateTime.UtcNow.Date;

            var alreadyCompleted = await _context.TaskLogs
                .AnyAsync(x => x.TaskId == request.TaskId &&
                               x.UserId == request.UserId &&
                               x.Date == today,
                          cancellationToken);

            if (alreadyCompleted)
                throw new Exception("Task already completed today");

            var log = new TaskLog
            {
                Id = Guid.NewGuid(),
                TaskId = request.TaskId,
                UserId = request.UserId,
                Date = today,
                IsCompleted = true
            };

            await _context.TaskLogs.AddAsync(log, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
