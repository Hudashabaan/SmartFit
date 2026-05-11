using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace SmartFit.Application.Features.CaloriesPredictions.DTOs
{
    public class CaloriesPredictionResponseDto
    {
        [JsonPropertyName("predicted_burned_calories")]
        public double PredictedBurnedCalories { get; set; }
    }
}
