using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartFit.Application.Features.Meals.DTOs;
using MediatR;

namespace SmartFit.Application.Features.Meals.Queries.GetMealsByDate
{
   
 
    public class GetMealsByDateQuery : IRequest<List<MealDto>>
    {
        public DateTime Date { get; set; }

        public GetMealsByDateQuery(DateTime date)
        {
            Date = date;
        }
    }
}
