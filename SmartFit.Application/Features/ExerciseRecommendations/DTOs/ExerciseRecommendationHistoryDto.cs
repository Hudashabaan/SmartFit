using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartFit.Application.Features
    .ExerciseRecommendations.DTOs
{
    public class ExerciseRecommendationHistoryDto
    {
        public Guid Id { get; set; }

        public string RecommendedExercises { get; set; } = null!;

        public string RecommendedEquipment { get; set; } = null!;

        public string RecommendationReason { get; set; } = null!;

        public DateTime RecommendedAt { get; set; }
    }
}
