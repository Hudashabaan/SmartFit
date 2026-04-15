using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SmartFit.Application.Features.HealthScheduler.Commands.SnoozeSchedule
{
    public class SnoozeScheduleCommand : IRequest
    {
        public Guid Id { get; set; }

        public int Minutes { get; set; }
    }
}
