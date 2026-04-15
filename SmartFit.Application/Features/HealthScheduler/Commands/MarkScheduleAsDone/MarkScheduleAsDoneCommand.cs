using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.HealthScheduler.Commands.MarkScheduleAsDone
{
    public class MarkScheduleAsDoneCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
