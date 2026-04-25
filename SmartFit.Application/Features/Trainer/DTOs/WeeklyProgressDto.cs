using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Trainer.DTOs
{
    public class WeeklyProgressDto
    {
        public int CompletedTasks { get; set; }

        public int TotalTasks { get; set; }

        public double? AverageCalories { get; set; }

        public double? Weight { get; set; }
    }
}
