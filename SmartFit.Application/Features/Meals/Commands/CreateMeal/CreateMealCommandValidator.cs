using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SmartFit.Application.Features.Meals.Commands.CreateMeal
{
   

    public class CreateMealCommandValidator : AbstractValidator<CreateMealCommand>
    {
        public CreateMealCommandValidator()
        {
            RuleFor(x => x.Meal.FoodName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Meal.Calories)
                .GreaterThan(0);

            RuleFor(x => x.Meal.Protein)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Meal.Carbs)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Meal.Fat)
                .GreaterThanOrEqualTo(0);
        }
    }
}
