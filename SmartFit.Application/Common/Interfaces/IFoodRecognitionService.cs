using SmartFit.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Common.Interfaces
{
    public interface IFoodRecognitionService
    {
        Task<FoodAnalysisDto> AnalyzeAsync(Stream imageStream);
    }
}
