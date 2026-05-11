using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using SmartFit.Application.Features.CaloriesPredictions.DTOs;

namespace SmartFit.Application.Features.CaloriesPredictions.Queries.GetMyCaloriesPredictions
{
        public class GetMyCaloriesPredictionsQuery
            : IRequest<
                List<CaloriesPredictionHistoryDto>>
        {
        }
    
}
