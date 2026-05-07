using SmartFit.Application.Features.Chatbot.Interfaces;
using SmartFit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartFit.Infrastructure.Services
{
    public class IntentClassifierService : IIntentClassifier
    {
        public ChatIntent Predict(string message)
        {
            message = message.ToLower();

            // 🔴 Medical (أولوية)
            var medicalKeywords = new List<string>
            {
                "مرض", "سكر", "ضغط", "diabetes", "disease"
            };

            if (medicalKeywords.Any(k => message.Contains(k)))
                return ChatIntent.MedicalConcern;

            // 🍽 Food
            if (message.Contains("اكل") || message.Contains("food") || message.Contains("calories"))
                return ChatIntent.AskCalories;

            // 🥗 Diet
            if (message.Contains("دايت") || message.Contains("diet"))
                return ChatIntent.AskDiet;

            // ⚖️ Weight
            if (message.Contains("وزن") || message.Contains("weight"))
                return ChatIntent.AskWeightLoss;

            // 🍳 Recipes
            if (message.Contains("وصفة") || message.Contains("recipe"))
                return ChatIntent.AskRecipes;

            // 📊 Advice
            if (message.Contains("نصيحة") || message.Contains("advice"))
                return ChatIntent.SmartAdvice;

            // 🍽 Meals Check
            if (message.Contains("اكلت") || message.Contains("ate"))
                return ChatIntent.FoodCheck;

            // 👋 Greeting
            if (message.Contains("hello") || message.Contains("hi") || message.Contains("اهلا"))
                return ChatIntent.Greeting;

            return ChatIntent.Unknown;
        }
    }
}
