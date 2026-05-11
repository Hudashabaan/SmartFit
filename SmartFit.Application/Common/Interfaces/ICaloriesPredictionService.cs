using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmartFit.Application.Features.CaloriesPredictions.DTOs;

namespace SmartFit.Application.Common.Interfaces
{
    public interface ICaloriesPredictionService
    {
        Task<CaloriesPredictionResponseDto> PredictCaloriesAsync(
            CaloriesPredictionRequestDto request);
    }
}
