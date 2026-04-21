using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Lifestyle.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Lifestyle.Queries.GetProgress
{
    public class GetProgressHandler
    : IRequestHandler<GetProgressQuery, ProgressDto>
    {
        private readonly IApplicationDbContext _context;

        public GetProgressHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProgressDto> Handle(
            GetProgressQuery request,
            CancellationToken cancellationToken)
        {
            var today = DateTime.UtcNow.Date;

            // 1️⃣ كل الـ tasks
            var totalTasks = await _context.Tasks
                .CountAsync(t => t.UserId == request.UserId, cancellationToken);

            // 2️⃣ اللي اتعمل النهارده
            var completedTasks = await _context.TaskLogs
                .CountAsync(l => l.UserId == request.UserId && l.Date == today, cancellationToken);

            // 3️⃣ حساب النسبة
            double percentage = 0;

            if (totalTasks > 0)
            {
                percentage = (double)completedTasks / totalTasks * 100;
            }

            return new ProgressDto
            {
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                ProgressPercentage = percentage
            };
        }
    }
}
