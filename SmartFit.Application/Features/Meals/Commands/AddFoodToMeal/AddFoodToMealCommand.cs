using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace SmartFit.Application.Features.Meals.Commands.AddFoodToMeal
{
    

    namespace SmartFit.Application.Features.Meals.Commands.AddFoodToMeal
    {
        public class AddFoodToMealCommand : IRequest<Guid>
        {
            public string FoodName { get; set; } = string.Empty;

            public double Calories { get; set; }

            public double Protein { get; set; }

            public double Carbs { get; set; }

            public double Fat { get; set; }
        }
    }
}
