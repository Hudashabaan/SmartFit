using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Http;
using SmartFit.Application.Features.BodyAnalysisFeature.DTOs;

namespace SmartFit.Application.Features.BodyAnalysisFeature.Commands.CreateBodyAnalysis
{

    public class CreateBodyAnalysisCommand : IRequest<BodyAnalysisResultDto>
    {
        public IFormFile? Image { get; set; }

        public double? Height { get; set; }
        public double? Weight { get; set; }

        public int? Age { get; set; }
        public string? Gender { get; set; }
    }
}
