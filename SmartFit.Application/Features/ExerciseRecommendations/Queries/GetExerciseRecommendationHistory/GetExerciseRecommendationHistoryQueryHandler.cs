using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features
    .ExerciseRecommendations.DTOs;

namespace SmartFit.Application.Features
    .ExerciseRecommendations.Queries
    .GetExerciseRecommendationHistory
{
    public class
        GetExerciseRecommendationHistoryQueryHandler
        : IRequestHandler<
            GetExerciseRecommendationHistoryQuery,
            List<ExerciseRecommendationHistoryDto>>
    {
        private readonly IApplicationDbContext _context;

        private readonly ICurrentUserService
            _userContext;

        public
            GetExerciseRecommendationHistoryQueryHandler(
                IApplicationDbContext context,

                ICurrentUserService userContext)
        {
            _context = context;

            _userContext = userContext;
        }

        public async Task<
            List<ExerciseRecommendationHistoryDto>>
            Handle(
                GetExerciseRecommendationHistoryQuery request,

                CancellationToken cancellationToken)
        {
            // =========================
            // Current User
            // =========================

            var userId = _userContext.UserId;

            // =========================
            // Get History
            // =========================

            var history = await _context
                .UserExerciseRecommendations
                .Where(x => x.UserId == userId)

                .OrderByDescending(x => x.RecommendedAt)

                .Select(x =>
                    new ExerciseRecommendationHistoryDto
                    {
                        Id = x.Id,

                        RecommendedExercises =
                            x.RecommendedExercises,

                        RecommendedEquipment =
                            x.RecommendedEquipment,

                        RecommendationReason =
                            x.RecommendationReason,

                        RecommendedAt =
                            x.RecommendedAt
                    })

                .ToListAsync(cancellationToken);

            return history;
        }
    }
}
