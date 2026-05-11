using System.Text;
using System.Text.Json;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.BMI.DTOs;

namespace SmartFit.Infrastructure.Services
{
    public class BMIPredictionService : IBMIPredictionService
    {
        private readonly HttpClient _httpClient;

        public BMIPredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<double> PredictBMIAsync(
            PredictBMIRequestDto request)
        {
            var requestBody = new
            {
                Sex = request.Sex,
                Height_cm = request.HeightCm,
                Age = request.Age,
                Weight_kg = request.WeightKg
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(
                "predict",
                content);

            response.EnsureSuccessStatusCode();

            var responseContent =
                await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<BMIApiResponse>(
                responseContent,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return result.predicted_bmi;
        }

        private class BMIApiResponse
        {
            public double predicted_bmi { get; set; }
        }
    }
}