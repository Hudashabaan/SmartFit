using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartFit.Application.Features.CaloriesPredictions.DTOs
{
    public class CaloriesPredictionHistoryDto
    {
        public Guid Id { get; set; }

        public string WorkoutType { get; set; }
            = string.Empty;

        public double PredictedBurnedCalories
        { get; set; }

        public string WorkoutAnalysis
        { get; set; } = string.Empty;

        public string WorkoutSummary
        { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
