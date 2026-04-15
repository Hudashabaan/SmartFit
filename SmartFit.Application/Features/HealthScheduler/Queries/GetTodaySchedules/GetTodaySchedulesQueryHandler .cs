using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.HealthScheduler.DTOs;

namespace SmartFit.Application.Features.HealthScheduler.Queries.GetTodaySchedules
{ 
    public class GetTodaySchedulesQueryHandler
        : IRequestHandler<GetTodaySchedulesQuery, List<ScheduleDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetTodaySchedulesQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<ScheduleDto>> Handle(
            GetTodaySchedulesQuery request,
            CancellationToken cancellationToken)
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            return await _context.HealthSchedules
                .Where(x =>
                    x.UserId == _currentUser.UserId &&
                    x.Time >= today &&
                    x.Time < tomorrow &&
                    x.IsActive)
                .Select(x => new ScheduleDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Type = x.Type,
                    Time = x.Time,
                    Repeat = x.Repeat,
                    IsCompleted = x.IsCompleted
                })
                .ToListAsync(cancellationToken);
        }
    }
}
