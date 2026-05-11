using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features
    .CaloriesPredictions.DTOs;

namespace SmartFit.Application.Features.CaloriesPredictions.Queries.GetMyCaloriesPredictions
{
    public class GetMyCaloriesPredictionsQueryHandler
        : IRequestHandler<
            GetMyCaloriesPredictionsQuery,
            List<CaloriesPredictionHistoryDto>>
    {
        private readonly IApplicationDbContext
            _context;

        private readonly IHttpContextAccessor
            _httpContextAccessor;

        public GetMyCaloriesPredictionsQueryHandler(
            IApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;

            _httpContextAccessor =
                httpContextAccessor;
        }

        public async Task<
            List<CaloriesPredictionHistoryDto>>
            Handle(
            GetMyCaloriesPredictionsQuery request,
            CancellationToken cancellationToken)
        {
            // Current User
            var userId = _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst("uid")?
                .Value;

            // Query
            var predictions =
                await _context.CaloriesPredictions

                .Where(x => x.UserId == userId)

                .OrderByDescending(x => x.CreatedAt)

                .Select(x =>
                    new CaloriesPredictionHistoryDto
                    {
                        Id = x.Id,

                        WorkoutType =
                            x.WorkoutType,

                        PredictedBurnedCalories =
                            x.PredictedBurnedCalories,

                        WorkoutAnalysis =
                            x.WorkoutAnalysis,

                        WorkoutSummary =
                            x.WorkoutSummary,

                        CreatedAt =
                            x.CreatedAt
                    })

                .ToListAsync(cancellationToken);

            return predictions;
        }
    }
}
