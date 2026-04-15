using Microsoft.AspNetCore.Http;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.BodyAnalysisFeature.Commands.CreateBodyAnalysis;

using SmartFit.Application.Features.BodyAnalysisFeature.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Infrastructure.AI
{
	public class AIService : IAIService
	{
		public async Task<BodyAnalysisResultDto> AnalyzeImageAsync(IFormFile image)
		{
			// ممكن ترمي exception لو الصورة مش واضحة
			return new BodyAnalysisResultDto
			{
				BMI = 23,
				BodyFatPercentage = 18,
				FatMass = 12,
				MuscleMass = 30,
				Waist = 80,
				BodyShape = "Fit",
				Confidence = 0.9
			};
		}

		public async Task<BodyAnalysisResultDto> AnalyzeDataAsync(
			double height,
			double weight,
			int? age,
			string? gender)
		{
			var bmi = weight / Math.Pow(height / 100, 2);

			return new BodyAnalysisResultDto
			{
				BMI = bmi,
				BodyFatPercentage = 20,
				FatMass = 14,
				MuscleMass = 28,
				Waist = 85,
				BodyShape = "Fit",
				Confidence = 0.7
			};
		}
	}
}
