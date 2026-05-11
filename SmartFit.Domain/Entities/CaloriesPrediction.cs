using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Domain.Entities
{
    public class CaloriesPrediction
    {
        public Guid Id { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; } = string.Empty;

        public double WeightKg { get; set; }

        public string WorkoutType { get; set; } = string.Empty;

        public double SessionDurationHours { get; set; }

        public int AvgBPM { get; set; }

        public int MaxBPM { get; set; }

        public int WorkoutFrequencyDaysWeek { get; set; }

        public double BMI { get; set; }

        public double PredictedBurnedCalories { get; set; }

        public string WorkoutAnalysis { get; set; } = string.Empty;

        public string WorkoutSummary { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // Relation
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;
    }
}
