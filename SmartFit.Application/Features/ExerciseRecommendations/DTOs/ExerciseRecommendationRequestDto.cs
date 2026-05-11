using System.Text.Json.Serialization;

namespace SmartFit.Application.Features
    .ExerciseRecommendations.DTOs
{
    public class ExerciseRecommendationRequestDto
    {
        [JsonPropertyName("Sex")]
        public string Sex { get; set; } = null!;

        [JsonPropertyName("Age")]
        public int Age { get; set; }

        [JsonPropertyName("Height")]
        public double Height { get; set; }

        [JsonPropertyName("Weight")]
        public double Weight { get; set; }

        [JsonPropertyName("Hypertension")]
        public string Hypertension { get; set; } = null!;

        [JsonPropertyName("Diabetes")]
        public string Diabetes { get; set; } = null!;

        [JsonPropertyName("BMI")]
        public double BMI { get; set; }

        [JsonPropertyName("Level")]
        public string Level { get; set; } = null!;

        [JsonPropertyName("Fitness_Goal")]
        public string Fitness_Goal { get; set; } = null!;

        [JsonPropertyName("Fitness_Type")]
        public string Fitness_Type { get; set; } = null!;

        [JsonPropertyName("Equipment")]
        public string Equipment { get; set; } = null!;
    }
}