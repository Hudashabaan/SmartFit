using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace SmartFit.Application.Features.Meals.Commands.Delete_Meal
{
   

    public class DeleteMealCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
