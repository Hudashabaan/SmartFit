using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartFit.Domain.Enums
{
    public enum ChatIntent
    {
        Greeting,
        AskCalories,
        AskDiet,
        AskWorkout,
        AskWeightLoss,
        AskRecipes,
        FoodCheck,
        SmartAdvice, // 🔥 الجديد
        MedicalConcern,
        Unknown
    }
}
