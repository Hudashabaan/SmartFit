using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.BMI.DTOs;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Features.BMI.Commands.PredictBMI
{
    public class PredictBMICommandHandler
        : IRequestHandler<
            PredictBMICommand,
            PredictBMIResponseDto>
    {
        private readonly IBMIPredictionService _bmiService;
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<PredictBMICommandHandler> _logger;

        public PredictBMICommandHandler(
            IBMIPredictionService bmiService,
            IApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            ILogger<PredictBMICommandHandler> logger)
        {
            _bmiService = bmiService;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<PredictBMIResponseDto> Handle(
            PredictBMICommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Starting BMI Prediction");

            // Get Current User Id
            var userId = _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new UnauthorizedAccessException(
                    "User is not authenticated");
            }

            // Get User Profile
            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(
                    x => x.UserId == userId,
                    cancellationToken);

            if (profile == null)
            {
                throw new Exception(
                    "User profile not found");
            }

            // Prepare DTO For AI Service
            var dto = new PredictBMIRequestDto
            {
                Sex = profile.Gender
                    .ToString()
                    .ToLower(),

                HeightCm = profile.Height,
                Age = profile.Age,
                WeightKg = profile.Weight
            };

            // Call AI API
            var predictedBMI =
                await _bmiService
                    .PredictBMIAsync(dto);

            // Classification
            var bodyType =
                GetBodyType(predictedBMI);

            var healthStatus =
                GetHealthStatus(predictedBMI);

            // Save Database
            var bmiRecord = new BMIRecord
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Age = profile.Age,
                Sex = profile.Gender
                    .ToString(),

                HeightCm = profile.Height,
                WeightKg = profile.Weight,

                PredictedBMI = predictedBMI,
                BodyType = bodyType,
                HealthStatus = healthStatus,
                CreatedAt = DateTime.UtcNow
            };

            await _context.BMIRecords
                .AddAsync(
                    bmiRecord,
                    cancellationToken);

            await _context.SaveChangesAsync(
                cancellationToken);

            _logger.LogInformation(
                "BMI Prediction Saved Successfully");

            return new PredictBMIResponseDto
            {
                PredictedBMI = predictedBMI,
                BodyType = bodyType,
                HealthStatus = healthStatus
            };
        }

        private string GetBodyType(double bmi)
        {
            if (bmi < 18.5)
                return "ectomorph";

            if (bmi < 25)
                return "mesomorph";

            if (bmi < 30)
                return "overweight";

            return "obese";
        }

        private string GetHealthStatus(double bmi)
        {
            if (bmi < 18.5)
                return "Underweight";

            if (bmi < 25)
                return "Healthy";

            if (bmi < 30)
                return "Overweight";

            return "Obese";
        }
    }
}