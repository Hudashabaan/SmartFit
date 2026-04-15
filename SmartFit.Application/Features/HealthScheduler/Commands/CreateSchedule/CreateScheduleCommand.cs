using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.HealthScheduler.Commands.CreateSchedule
{
    

    public class CreateScheduleCommand : IRequest<Guid>
    {
        public string Title { get; set; }

        public ScheduleType Type { get; set; }

        public DateTime Time { get; set; }

        public RepeatType Repeat { get; set; }
    }
}
