using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Dashboard.DTOs;
using SmartFit.Application.Features.Meals.DTOs;
using SmartFit.Application.Features.HealthScheduler.DTOs;
using SmartFit.Application.DTOs;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Dashboard.Queries.GetDashboard
{
    public class GetDashboardQueryHandler
        : IRequestHandler<GetDashboardQuery, DashboardDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetDashboardQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<DashboardDto> Handle(
            GetDashboardQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId
                ?? throw new UnauthorizedAccessException("User not logged in");

            var start = request.Date.Date;
            var end = start.AddDays(1);

            // 🔥 Meals Query
            var mealsQuery = _context.Meals
                .Where(x => x.UserId == userId
                         && x.Date >= start
                         && x.Date < end);

            // ✅ Meals List
            var meals = await mealsQuery
                .OrderByDescending(x => x.Date)
                .Select(x => new MealDto
                {
                    FoodName = x.FoodName,
                    Calories = x.Calories,
                    Protein = x.Protein,
                    Carbs = x.Carbs,
                    Fat = x.Fat,
                    Date = x.Date
                })
                .ToListAsync(cancellationToken);

            // ✅ Summary
            var summary = await mealsQuery
                .GroupBy(x => 1)
                .Select(g => new
                {
                    Calories = g.Sum(x => x.Calories),
                    Protein = g.Sum(x => x.Protein),
                    Carbs = g.Sum(x => x.Carbs),
                    Fat = g.Sum(x => x.Fat)
                })
                .FirstOrDefaultAsync(cancellationToken);

            // 🔥 Current Goal
            var goal = await _context.Goals
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.UserId == userId && g.IsActive, cancellationToken);

            GoalDto? goalDto = null;
            double progress = 0;

            if (goal != null)
            {
                goalDto = new GoalDto
                {
                    Id = goal.Id,
                    StartWeight = goal.StartWeight,
                    TargetWeight = goal.TargetWeight,
                    DurationInDays = goal.DurationInDays,
                    Type = goal.Type.ToString(),
                    StartDate = goal.StartDate,
                    IsActive = goal.IsActive
                };

                var profile = await _context.UserProfiles
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

                if (profile != null)
                {
                    var currentWeight = profile.Weight;

                    if (goal.StartWeight == goal.TargetWeight)
                    {
                        progress = 100;
                    }
                    else
                    {
                        switch (goal.Type)
                        {
                            case GoalType.LoseWeight:
                                progress = (goal.StartWeight - currentWeight) /
                                           (goal.StartWeight - goal.TargetWeight);
                                break;

                            case GoalType.GainMuscle:
                                progress = (currentWeight - goal.StartWeight) /
                                           (goal.TargetWeight - goal.StartWeight);
                                break;

                            case GoalType.MaintainWeight:
                                progress = 100;
                                break;
                        }

                        progress = Math.Max(0, Math.Min(progress, 1)) * 100;
                    }
                }
            }

            // 🔥 Last Food Analysis
            var lastFood = await _context.FoodAnalyses
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new LastFoodAnalysisDto
                {
                    FoodName = x.FoodName,
                    Calories = x.Calories,
                    Protein = x.Protein,
                    Carbs = x.Carbs,
                    Fat = x.Fat,
                    Confidence = x.Confidence,
                    Date = x.CreatedAt
                })
                .FirstOrDefaultAsync(cancellationToken);

            // 🔥 Last Body Analysis
            var lastBodyEntity = await _context.BodyAnalyses
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);

            LastBodyAnalysisDto? lastBody = null;

            if (lastBodyEntity != null)
            {
                string? category = null;

                if (lastBodyEntity.BMI.HasValue)
                {
                    var bmi = lastBodyEntity.BMI.Value;

                    if (bmi < 18.5)
                        category = "Underweight";
                    else if (bmi < 25)
                        category = "Normal";
                    else if (bmi < 30)
                        category = "Overweight";
                    else
                        category = "Obese";
                }

                lastBody = new LastBodyAnalysisDto
                {
                    BMI = lastBodyEntity.BMI,
                    BodyFat = lastBodyEntity.BodyFatPercentage,
                    Category = category,
                    Date = lastBodyEntity.CreatedAt
                };
            }

            // 🔥 Notifications
            var notificationsQuery = _context.NotificationHistories
                .Where(x => x.UserId == userId);

            var notificationsCount = await notificationsQuery.CountAsync(cancellationToken);

            var latestNotifications = await notificationsQuery
                .OrderByDescending(x => x.SentAt)
                .Take(5)
                .Select(x => new NotificationHistoryDto
                {
                    Title = x.Title,
                    Message = x.Message,
                    SentAt = x.SentAt
                })
                .ToListAsync(cancellationToken);

            // 🎯 Final Response
            return new DashboardDto
            {
                TotalCalories = summary?.Calories ?? 0,
                TotalProtein = summary?.Protein ?? 0,
                TotalCarbs = summary?.Carbs ?? 0,
                TotalFat = summary?.Fat ?? 0,

                Meals = meals,

                CurrentGoal = goalDto,
                Progress = progress,

                LastFoodAnalysis = lastFood,
                LastBodyAnalysis = lastBody,

                // 🆕 Notifications
                NotificationsCount = notificationsCount,
                LatestNotifications = latestNotifications
            };
        }
    }
}