using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Trainer.DTOs;
using SmartFit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Trainer.Queries.GetWeeklyProgress
{
    public class GetWeeklyProgressQueryHandler
    : IRequestHandler<GetWeeklyProgressQuery, WeeklyProgressDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetWeeklyProgressQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<WeeklyProgressDto> Handle(
            GetWeeklyProgressQuery request,
            CancellationToken cancellationToken)
        {
            // 🔥 1. Check Trainer
            if (_currentUser.Role != UserRole.Trainer)
                throw new Exception("Only trainers");

            // 🔥 2. Check Relation
            var relation = await _context.TrainerClientRelations
                .AnyAsync(x =>
                    x.TrainerId == _currentUser.UserId &&
                    x.ClientId == request.ClientId &&
                    x.Status == RelationStatus.Accepted,
                    cancellationToken);

            if (!relation)
                throw new Exception("Unauthorized");

            // 🔥 3. Date Range (آخر 7 أيام)
            var fromDate = DateTime.UtcNow.AddDays(-7);

            // 🔥 4. Tasks
            var tasks = await _context.Tasks
                .Where(x => x.UserId == request.ClientId &&
                            x.CreatedAt >= fromDate)
                .ToListAsync(cancellationToken);

            var completedTasks = tasks.Count(x => x.IsCompleted);
            var totalTasks = tasks.Count;

            // 🔥 5. Meals
            var meals = await _context.Meals
                .Where(x => x.UserId == request.ClientId &&
                            x.Date >= fromDate)
                .ToListAsync(cancellationToken);

            var avgCalories = meals.Any()
                ? meals.Average(x => x.Calories)
                : 0;

            // 🔥 6. Body Analysis
            var body = await _context.BodyAnalyses
                .Where(x => x.UserId == request.ClientId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);

            // 🔥 7. Return DTO
            return new WeeklyProgressDto
            {
                CompletedTasks = completedTasks,
                TotalTasks = totalTasks,
                AverageCalories = avgCalories,
                Weight = body?.Weight
            };
        }
    }
}
