using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Infrastructure.Services
{
    public class DefaultTaskService : IDefaultTaskService
    {
        private readonly IApplicationDbContext _context;

        public DefaultTaskService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateDefaultTasks(Guid userId)
        {
            var tasks = new List<SmartFit.Domain.Entities.Task>
        {
            new SmartFit.Domain.Entities.Task
            {
                Id = Guid.NewGuid(),
                Title = "Drink Water",
                Description = "Drink at least 2L of water",
                Type = TaskType.Water,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            },
            new SmartFit.Domain.Entities.Task
            {
                Id = Guid.NewGuid(),
                Title = "Walk 5000 Steps",
                Description = "Daily walking goal",
                Type = TaskType.Steps,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            },
            new SmartFit.Domain.Entities.Task
            {
                Id = Guid.NewGuid(),
                Title = "Workout",
                Description = "Do any workout",
                Type = TaskType.Workout,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            }
        };

            await _context.Tasks.AddRangeAsync(tasks);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
