using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.HealthScheduler.DTOs
{
    public class ScheduleDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public ScheduleType Type { get; set; }

        public DateTime Time { get; set; }

        public RepeatType Repeat { get; set; }

        public bool IsCompleted { get; set; }
    }
}
