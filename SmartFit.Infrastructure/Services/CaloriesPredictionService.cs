using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.CaloriesPredictions.DTOs;

namespace SmartFit.Infrastructure.Services
{
    public class CaloriesPredictionService : ICaloriesPredictionService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CaloriesPredictionService> _logger;

        public CaloriesPredictionService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<CaloriesPredictionService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<CaloriesPredictionResponseDto> PredictCaloriesAsync(
            CaloriesPredictionRequestDto request)
        {
            try
            {
                var endpoint =
                    _configuration["AIModels:CaloriesPredictionUrl"];

                _logger.LogInformation(
                    "Calling AI Calories API: {Endpoint}",
                    endpoint);

                var response =
                    await _httpClient.PostAsJsonAsync(endpoint, request);

                var rawResponse =
                    await response.Content.ReadAsStringAsync();

                _logger.LogInformation(
                    "AI Raw Response: {Response}",
                    rawResponse);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError(
                        "AI Calories API failed. Status: {StatusCode}, Body: {Body}",
                        response.StatusCode,
                        rawResponse);

                    throw new Exception(
                        $"AI Service Error: {rawResponse}");
                }

                var result =
                    System.Text.Json.JsonSerializer.Deserialize<
                        CaloriesPredictionResponseDto>(
                        rawResponse,
                        new System.Text.Json.JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                if (result is null)
                {
                    _logger.LogError(
                        "AI returned null response");

                    throw new Exception(
                        "Invalid AI response (null)");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Calories Prediction Service Exception");

                throw new Exception(
                    $"Calories Service Failed: {ex.Message}");
            }
        }
    }
}