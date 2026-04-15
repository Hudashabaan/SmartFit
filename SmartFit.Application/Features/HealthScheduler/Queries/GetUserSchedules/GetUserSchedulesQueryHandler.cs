using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.HealthScheduler.DTOs;

namespace SmartFit.Application.Features.HealthScheduler.Queries.GetUserSchedules
{
    public class GetUserSchedulesQueryHandler
        : IRequestHandler<GetUserSchedulesQuery, List<ScheduleDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetUserSchedulesQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<ScheduleDto>> Handle(
            GetUserSchedulesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.HealthSchedules
                .Where(x => x.UserId == _currentUser.UserId)
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
