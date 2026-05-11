using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.ExerciseRecommendations.DTOs;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Features
    .ExerciseRecommendations.Queries
    .GetExerciseRecommendation
{
    public class GetExerciseRecommendationQueryHandler
        : IRequestHandler<
            GetExerciseRecommendationQuery,
            ExerciseRecommendationResponseDto>
    {
        private readonly IExerciseRecommendationAiService _aiService;

        private readonly IApplicationDbContext _context;

        private readonly ICurrentUserService _userContext;

        public GetExerciseRecommendationQueryHandler(
            IExerciseRecommendationAiService aiService,
            IApplicationDbContext context,
            ICurrentUserService userContext)
        {
            _aiService = aiService;

            _context = context;

            _userContext = userContext;
        }

        public async Task<
            ExerciseRecommendationResponseDto>
            Handle(
                GetExerciseRecommendationQuery request,
                CancellationToken cancellationToken)
        {
            // =========================
            // 1. Get Current User
            // =========================

            var userId = _userContext.UserId;

            // =========================
            // 2. Get User Profile
            // =========================

            var profile = await _context
                .UserProfiles
                .FirstOrDefaultAsync(
                    x => x.UserId == userId,
                    cancellationToken);

            if (profile is null)
            {
                throw new Exception(
                    "User profile not found.");
            }

            // =========================
            // 3. Get Latest BMI Record
            // =========================

            var latestBMIRecord = await _context
                .BMIRecords
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);

            if (latestBMIRecord is null)
            {
                throw new Exception(
                    "No BMI analysis found for this user.");
            }

            // =========================
            // 4. Hybrid Inputs
            // =========================

            var sex =
                request.Sex
                ?? profile.Gender.ToString();

            var age =
                request.Age
                ?? profile.Age;

            var height =
                request.Height
                ?? profile.Height;

            var weight =
                request.Weight
                ?? profile.Weight;

            var hypertension =
                request.Hypertension
                ?? (profile.HasHypertension
                    ? "Yes"
                    : "No");

            var diabetes =
                request.Diabetes
                ?? (profile.HasDiabetes
                    ? "Yes"
                    : "No");

            var fitnessGoal =
     request.Fitness_Goal
     ?? MapFitnessGoal(
         profile.FitnessGoal.ToString());

            var fitnessType =
                request.Fitness_Type
                ?? MapFitnessType(
                    profile.FitnessType.ToString());

            // =========================
            // 5. Map BodyType To Level
            // =========================

            var level =
                MapBodyTypeToLevel(
                    latestBMIRecord.BodyType);

            // =========================
            // 6. Prepare AI Request
            // =========================

            var aiRequest =
                new ExerciseRecommendationRequestDto
                {
                    Sex = sex,

                    Age = age,

                    Height = height,

                    Weight = weight,

                    Hypertension = hypertension,

                    Diabetes = diabetes,

                    BMI = latestBMIRecord.PredictedBMI,

                    Level = level,

                    Fitness_Goal = fitnessGoal,

                    Fitness_Type = fitnessType,

                    Equipment = request.Equipment
                };

            // =========================
            // 7. Call AI API
            // =========================

            var aiResponse =
                await _aiService
                    .GetRecommendationAsync(
                        aiRequest);

            // =========================
            // 8. Save Recommendation
            // =========================

            var recommendation =
                new UserExerciseRecommendation
                {
                    UserId = userId,

                    RecommendedExercises =
                        aiResponse
                            .Recommended_Exercise,

                    RecommendedEquipment =
                        request.Equipment,

                    RecommendationReason =
                        $"Based on BMI {latestBMIRecord.PredictedBMI} and goal {fitnessGoal}",

                    RecommendedAt =
                        DateTime.UtcNow
                };

            await _context
                .UserExerciseRecommendations
                .AddAsync(
                    recommendation,
                    cancellationToken);

            await _context
                .SaveChangesAsync(
                    cancellationToken);

            // =========================
            // 9. Return Response
            // =========================

            return aiResponse;
        }

        // =========================
        // Mapping Method
        // =========================

        private string MapBodyTypeToLevel(
            string bodyType)
        {
            return bodyType.ToLower() switch
            {
                "ectomorph" => "Underweight",

                "mesomorph" => "Normal",

                "overweight" => "Overweight",

                "obese" => "Obuse",

                _ => "Normal"
            };
        }

        private string MapFitnessGoal(
    string goal)
        {
            return goal switch
            {
                "WeightGain" => "Weight Gain",

                "WeightLoss" => "Weight Loss",

                _ => "Weight Loss"
            };
        }

        private string MapFitnessType(
            string type)
        {
            return type switch
            {
                "CardioFitness" => "Cardio Fitness",

                "MuscularFitness" => "Muscular Fitness",

                _ => "Cardio Fitness"
            };
        }
    }
}