using MediatR;
using Microsoft.AspNetCore.Http;
using SmartFit.Application.DTOs;

namespace SmartFit.Application.Features.FoodAnalysisFeature.Commands.AnalyzeFood
{
    public class AnalyzeFoodCommand : IRequest<FoodAnalysisDto>
    {
        public IFormFile Image { get; set; } = default!;
    }
}