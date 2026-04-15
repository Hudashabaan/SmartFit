using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.Features.Meals.DTOs;

namespace SmartFit.Application.Features.Meals.Commands.CreateMeal
{
    

    public class CreateMealCommand : IRequest<Guid>
    {
        public CreateMealDto Meal { get; set; }

        public CreateMealCommand(CreateMealDto meal)
        {
            Meal = meal;
        }
    }
}
