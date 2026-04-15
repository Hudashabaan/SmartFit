using SmartFit.Application.DTOs;
using SmartFit.Application.Features.Meals.DTOs;
using SmartFit.Application.Features.HealthScheduler.DTOs;

namespace SmartFit.Application.Features.Dashboard.DTOs
{
    public class DashboardDto
    {
        // 🔥 Nutrition
        public double TotalCalories { get; set; }
        public double TotalProtein { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalFat { get; set; }

        public List<MealDto> Meals { get; set; } = new();

        // 🔥 Goals
        public GoalDto? CurrentGoal { get; set; }
        public double Progress { get; set; }

        // 🔥 AI
        public LastFoodAnalysisDto? LastFoodAnalysis { get; set; }
        public LastBodyAnalysisDto? LastBodyAnalysis { get; set; }

        // 🆕 Notifications
        public int NotificationsCount { get; set; }
        public List<NotificationHistoryDto> LatestNotifications { get; set; } = new();
    }
}