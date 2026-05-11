using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmartFit.Application.Features.ExerciseRecommendations.DTOs;

namespace SmartFit.Application.Common.Interfaces
{
    public interface IExerciseRecommendationAiService
    {
        Task<ExerciseRecommendationResponseDto>
            GetRecommendationAsync(
                ExerciseRecommendationRequestDto request);
    }
}
