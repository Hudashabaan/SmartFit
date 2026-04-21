using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Lifestyle.Commands.DeleteTask
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTaskHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == request.TaskId, cancellationToken);

            if (task == null)
                throw new Exception("Task not found");

            if (task.UserId != request.UserId)
                throw new Exception("Unauthorized");

            // 🔥 حذف الـ logs المرتبطة (مهم)
            var logs = _context.TaskLogs
                .Where(x => x.TaskId == request.TaskId);

            _context.TaskLogs.RemoveRange(logs);

            // حذف المهمة
            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
