using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Infrastructure.Services
{
    public class FoodRecognitionService : IFoodRecognitionService
    {
        public async Task<FoodAnalysisDto> AnalyzeAsync(Stream imageStream)
        {
            // simulate delay (كأنه AI شغال)
            await Task.Delay(1000);

            return new FoodAnalysisDto
            {
                FoodName = "Chicken Rice",
                Calories = 500,
                Protein = 30,
                Carbs = 50,
                Fat = 10,
                Confidence = 0.85
            };
        }
    }
}
