using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Chatbot.Interfaces;
using SmartFit.Domain.Entities;
using SmartFit.Domain.Enums;

namespace SmartFit.Infrastructure.Services
{
    public class ChatbotService : IChatbotService
    {
        private readonly IIntentClassifier _classifier;
        private readonly IApplicationDbContext _context;

        public ChatbotService(IIntentClassifier classifier, IApplicationDbContext context)
        {
            _classifier = classifier;
            _context = context;
        }

        public async Task<string> GenerateResponse(string message, string userId)
        {
            // 🔥 Save user message
            _context.ChatMessages.Add(new ChatMessage
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Message = message,
                IsFromUser = true,
                CreatedAt = DateTime.UtcNow
            });

            var intent = _classifier.Predict(message);

            string response = intent switch
            {
                ChatIntent.Greeting => "Hello 👋",
                ChatIntent.AskCalories => await HandleCalories(userId),
                ChatIntent.AskDiet => await HandleDiet(userId),
                ChatIntent.AskWeightLoss => await HandleWeightLoss(userId),
                ChatIntent.FoodCheck => await HandleFoodCheck(userId),
                ChatIntent.SmartAdvice => await HandleSmartAdvice(userId),
                ChatIntent.AskRecipes => HandleRecipes(), // 🔥 الجديد
                ChatIntent.MedicalConcern => "⚠️ Please consult a doctor for medical advice.",
                _ => "I didn’t understand"
            };

            // 🔥 Save bot response
            _context.ChatMessages.Add(new ChatMessage
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Message = response,
                IsFromUser = false,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync(CancellationToken.None);

            return response;
        }

        // 🔥 Calories
        private async Task<string> HandleCalories(string userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return "User not found";

            return $"Based on your profile, your daily calories ~ 2000 kcal";
        }

        // 🔥 Diet
        private async Task<string> HandleDiet(string userId)
        {
            var goal = await GetUserGoal(userId);

            if (goal == null)
                return "Please set your goal first.";

            return goal.Type switch
            {
                GoalType.LoseWeight => "For weight loss: Eat high protein, reduce carbs 🔥",
                GoalType.GainMuscle => "For muscle gain: Increase protein 💪",
                GoalType.MaintainWeight => "Maintain balanced diet ⚖️",
                _ => "Follow a balanced diet 🍽"
            };
        }

        // 🔥 BMI
        private async Task<string> HandleWeightLoss(string userId)
        {
            var body = await GetLatestBody(userId);

            if (body == null)
                return "Please complete your body analysis first.";

            var bmi = body.BMI;

            if (bmi < 18.5)
                return "You are underweight 🍎";

            if (bmi < 25)
                return "Normal weight 👍";

            if (bmi < 30)
                return "Overweight 🔥";

            return "Obese ⚠️";
        }

        // 🔥 Meals
        private async Task<string> HandleFoodCheck(string userId)
        {
            var totalCalories = await GetTodayCalories(userId);
            var targetCalories = 2000;

            if (totalCalories == 0)
                return "No meals today 🍽";

            if (totalCalories < targetCalories * 0.8)
                return $"You ate {totalCalories} kcal 👍";

            if (totalCalories <= targetCalories)
                return $"On track 🔥";

            return $"Exceeded calories ⚠️ ({totalCalories})";
        }

        // 🔥 Smart Advice
        private async Task<string> HandleSmartAdvice(string userId)
        {
            var body = await GetLatestBody(userId);
            var goal = await GetUserGoal(userId);
            var totalCalories = await GetTodayCalories(userId);

            if (body == null)
                return "Complete body analysis first.";

            if (goal == null)
                return "Set your goal first.";

            var bmi = body.BMI;

            if (bmi > 25 && goal.Type == GoalType.LoseWeight && totalCalories > 2000)
                return "Reduce food & exercise 🔥";

            if (bmi >= 18.5 && bmi < 25)
                return "You're doing great 👏";

            if (bmi < 18.5)
                return "Increase calories 💪";

            return "Keep going 👍";
        }

        // 🔥 Recipes
        private string HandleRecipes()
        {
            var recipes = new List<string>
            {
                "Grilled chicken + veggies 🍗🥦",
                "Oats with fruits 🥣🍓",
                "Boiled eggs + toast 🍞🥚",
                "Rice + tuna 🍚🐟"
            };

            var random = new Random();
            return recipes[random.Next(recipes.Count)];
        }

        // 🔹 Helpers

        private async Task<BodyAnalysis?> GetLatestBody(string userId)
        {
            return await _context.BodyAnalyses
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .FirstOrDefaultAsync();
        }

        private async Task<Goal?> GetUserGoal(string userId)
        {
            return await _context.Goals
                .FirstOrDefaultAsync(g => g.UserId == userId);
        }

        private async Task<double> GetTodayCalories(string userId)
        {
            var today = DateTime.UtcNow.Date;

            return await _context.Meals
                .Where(m => m.UserId == userId && m.Date.Date == today)
                .SumAsync(m => (double?)m.Calories) ?? 0;
        }
    }
}