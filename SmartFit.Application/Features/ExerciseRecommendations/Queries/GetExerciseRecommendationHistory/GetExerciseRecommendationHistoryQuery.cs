using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.Features
    .ExerciseRecommendations.DTOs;

namespace SmartFit.Application.Features
    .ExerciseRecommendations.Queries
    .GetExerciseRecommendationHistory
{
    public class GetExerciseRecommendationHistoryQuery
        : IRequest<
            List<ExerciseRecommendationHistoryDto>>
    {
    }
}
