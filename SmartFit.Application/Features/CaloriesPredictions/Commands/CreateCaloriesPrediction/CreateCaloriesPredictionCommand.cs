using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.Features.CaloriesPredictions.DTOs;

namespace SmartFit.Application.Features.CaloriesPredictions.Commands
{
    public class CreateCaloriesPredictionCommand
        : IRequest<CaloriesPredictionResultDto>
    {
        public int Age { get; set; }

        public string Gender { get; set; } = string.Empty;

        public double WeightKg { get; set; }

        public string WorkoutType { get; set; } = string.Empty;

        public double SessionDurationHours { get; set; }

        public int AvgBPM { get; set; }

        public int MaxBPM { get; set; }

        public int WorkoutFrequencyDaysWeek { get; set; }

        public double BMI { get; set; }
    }
}
