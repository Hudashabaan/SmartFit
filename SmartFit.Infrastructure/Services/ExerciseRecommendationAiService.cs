using System.Net.Http.Json;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features
    .ExerciseRecommendations.DTOs;

namespace SmartFit.Infrastructure.Services
{
    public class ExerciseRecommendationAiService
        : IExerciseRecommendationAiService
    {
        private readonly HttpClient _httpClient;

        public ExerciseRecommendationAiService(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ExerciseRecommendationResponseDto>
            GetRecommendationAsync(
                ExerciseRecommendationRequestDto request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "predict/exercise",
                request);

            response.EnsureSuccessStatusCode();

            var result =
                await response.Content
                .ReadFromJsonAsync
                <ExerciseRecommendationResponseDto>();

            return result!;
        }
    }
}