using System.Text.Json.Serialization;

namespace SmartFit.Application.Features.CaloriesPredictions.DTOs
{
    public class CaloriesPredictionRequestDto
    {
        [JsonPropertyName("Age")]
        public int Age { get; set; }

        [JsonPropertyName("Gender")]
        public string Gender { get; set; }

        [JsonPropertyName("Weight_kg")]
        public double WeightKg { get; set; }

        [JsonPropertyName("Workout_Type")]
        public string WorkoutType { get; set; }

        [JsonPropertyName("Session_Duration_hours")]
        public double SessionDurationHours { get; set; }

        [JsonPropertyName("Avg_BPM")]
        public int AvgBPM { get; set; }

        [JsonPropertyName("Max_BPM")]
        public int MaxBPM { get; set; }

        [JsonPropertyName("Workout_Frequency_days_week")]
        public int WorkoutFrequencyDaysWeek { get; set; }

        [JsonPropertyName("BMI")]
        public double BMI { get; set; }
    }
}