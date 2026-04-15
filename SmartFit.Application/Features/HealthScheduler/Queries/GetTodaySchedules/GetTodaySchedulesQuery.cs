using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.Features.HealthScheduler.DTOs;
using System.Collections.Generic;

namespace SmartFit.Application.Features.HealthScheduler.Queries.GetTodaySchedules
{
    public class GetTodaySchedulesQuery : IRequest<List<ScheduleDto>>
    {
    }
}
