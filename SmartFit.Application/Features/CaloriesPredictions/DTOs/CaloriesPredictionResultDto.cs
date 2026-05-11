using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartFit.Application.Features.CaloriesPredictions.DTOs
{
    public class CaloriesPredictionResultDto
    {
        public Guid Id { get; set; }

        public double PredictedBurnedCalories { get; set; }

        public string WorkoutAnalysis { get; set; } = string.Empty;

        public string WorkoutSummary { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
