using MediatR;
using SmartFit.Application.Features
    .ExerciseRecommendations.DTOs;

namespace SmartFit.Application.Features
    .ExerciseRecommendations.Queries
    .GetExerciseRecommendation
{
    public class GetExerciseRecommendationQuery
        : IRequest<ExerciseRecommendationResponseDto>
    {
        // Optional Manual Inputs

        public string? Sex { get; set; }

        public int? Age { get; set; }

        public float? Height { get; set; }

        public float? Weight { get; set; }

        public string? Hypertension { get; set; }

        public string? Diabetes { get; set; }

        public string? Fitness_Goal { get; set; }

        public string? Fitness_Type { get; set; }

        public string Equipment { get; set; } = null!;
    }
}