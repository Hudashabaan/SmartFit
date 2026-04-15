using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SmartFit.Application.Features.Meals.Commands.Update_Meal
{
   
    public class UpdateMealCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string FoodName { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fat { get; set; }
    }
}
