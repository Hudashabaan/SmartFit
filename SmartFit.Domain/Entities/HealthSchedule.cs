using SmartFit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Domain.Entities
{
    public class HealthSchedule
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; } // Drink water

        public ScheduleType Type { get; set; }

        public DateTime Time { get; set; }

        public RepeatType Repeat { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsCompleted { get; set; } = false;

        public DateTime? LastTriggeredAt { get; set; }
    }
}
