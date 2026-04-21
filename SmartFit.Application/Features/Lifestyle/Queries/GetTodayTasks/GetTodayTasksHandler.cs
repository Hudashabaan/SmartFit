using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Lifestyle.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Lifestyle.Queries.GetTodayTasks
{
    public class GetTodayTasksHandler
    : IRequestHandler<GetTodayTasksQuery, List<TaskDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetTodayTasksHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskDto>> Handle(
            GetTodayTasksQuery request,
            CancellationToken cancellationToken)
        {
            var today = DateTime.UtcNow.Date;

            // 1️⃣ كل الـ tasks
            var tasks = await _context.Tasks
                .Where(t => t.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            // 2️⃣ كل الـ logs النهارده
            var logs = await _context.TaskLogs
                .Where(l => l.UserId == request.UserId && l.Date == today)
                .ToListAsync(cancellationToken);

            // 3️⃣ ربط بينهم
            var result = tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Type = t.Type,
                IsCompleted = logs.Any(l => l.TaskId == t.Id)
            }).ToList();

            return result;
        }
    }
}
