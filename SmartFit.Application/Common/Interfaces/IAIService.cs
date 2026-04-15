using Microsoft.AspNetCore.Http;
using SmartFit.Application.Features.BodyAnalysisFeature.Commands.CreateBodyAnalysis;

using SmartFit.Application.Features.BodyAnalysisFeature.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Common.Interfaces
{
	public interface IAIService
	{
		Task<BodyAnalysisResultDto> AnalyzeImageAsync(IFormFile image);

		Task<BodyAnalysisResultDto> AnalyzeDataAsync(
			double height,
			double weight,
			int? age,
			string? gender);
	}
}
