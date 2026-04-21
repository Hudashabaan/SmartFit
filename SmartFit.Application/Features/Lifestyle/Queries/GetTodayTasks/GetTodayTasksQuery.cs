using MediatR;
using SmartFit.Application.Features.Lifestyle.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Lifestyle.Queries.GetTodayTasks
{
    public class GetTodayTasksQuery : IRequest<List<TaskDto>>
    {
        public Guid UserId { get; set; }
    }
}
