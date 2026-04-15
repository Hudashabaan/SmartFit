using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.Features.HealthScheduler.DTOs;

namespace SmartFit.Application.Features.HealthScheduler.Queries.GetUserSchedules
{
    public class GetUserSchedulesQuery : IRequest<List<ScheduleDto>>
    {
    }
}
