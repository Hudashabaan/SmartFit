using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.CaloriesPredictions.DTOs;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Features.CaloriesPredictions.Commands
{
    public class CreateCaloriesPredictionCommandHandler
        : IRequestHandler<
            CreateCaloriesPredictionCommand,
            CaloriesPredictionResultDto>
    {
        private readonly IApplicationDbContext _context;

        private readonly ICaloriesPredictionService
            _caloriesPredictionService;

        private readonly IHttpContextAccessor
            _httpContextAccessor;

        public CreateCaloriesPredictionCommandHandler(
            IApplicationDbContext context,
            ICaloriesPredictionService caloriesPredictionService,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;

            _caloriesPredictionService =
                caloriesPredictionService;

            _httpContextAccessor =
                httpContextAccessor;
        }

        public async Task<CaloriesPredictionResultDto>
            Handle(
            CreateCaloriesPredictionCommand request,
            CancellationToken cancellationToken)
        {
            // Current User
            var userId =
                _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException(
                    "User not authenticated.");
            }

            // AI Request
            var aiRequest =
                new CaloriesPredictionRequestDto
                {
                    Age = request.Age,
                    Gender = request.Gender,
                    WeightKg = request.WeightKg,
                    WorkoutType = request.WorkoutType,
                    SessionDurationHours =
                        request.SessionDurationHours,
                    AvgBPM = request.AvgBPM,
                    MaxBPM = request.MaxBPM,
                    WorkoutFrequencyDaysWeek =
                        request.WorkoutFrequencyDaysWeek,
                    BMI = request.BMI
                };

            // AI Prediction
            var aiResult =
                await _caloriesPredictionService
                .PredictCaloriesAsync(aiRequest);

            // Workout Analysis
            var analysis =
                GenerateWorkoutAnalysis(
                    aiResult.PredictedBurnedCalories);

            // Workout Summary
            var summary =
                GenerateWorkoutSummary(
                    request.WorkoutType,
                    request.SessionDurationHours);

            // Entity
            var prediction =
                new CaloriesPrediction
                {
                    Id = Guid.NewGuid(),

                    Age = request.Age,

                    Gender = request.Gender,

                    WeightKg = request.WeightKg,

                    WorkoutType = request.WorkoutType,

                    SessionDurationHours =
                        request.SessionDurationHours,

                    AvgBPM = request.AvgBPM,

                    MaxBPM = request.MaxBPM,

                    WorkoutFrequencyDaysWeek =
                        request.WorkoutFrequencyDaysWeek,

                    BMI = request.BMI,

                    PredictedBurnedCalories =
                        aiResult.PredictedBurnedCalories,

                    WorkoutAnalysis = analysis,

                    WorkoutSummary = summary,

                    UserId = userId
                };

            await _context.CaloriesPredictions
                .AddAsync(prediction, cancellationToken);

            await _context.SaveChangesAsync(
                cancellationToken);

            // Response
            return new CaloriesPredictionResultDto
            {
                Id = prediction.Id,

                PredictedBurnedCalories =
                    prediction.PredictedBurnedCalories,

                WorkoutAnalysis =
                    prediction.WorkoutAnalysis,

                WorkoutSummary =
                    prediction.WorkoutSummary,

                CreatedAt = prediction.CreatedAt
            };
        }

        private string GenerateWorkoutAnalysis(
            double calories)
        {
            if (calories < 200)
                return "Low calorie burn workout.";

            if (calories < 500)
                return "Moderate calorie burn workout.";

            return "High intensity calorie burn workout.";
        }

        private string GenerateWorkoutSummary(
            string workoutType,
            double duration)
        {
            return
                $"{workoutType} workout completed " +
                $"for {duration} hours.";
        }
    }
}