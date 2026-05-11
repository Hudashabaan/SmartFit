using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.BMI.DTOs;
using System.Security.Claims;

namespace SmartFit.Application.Features.BMI.Queries.GetBMIHistory
{
    public class GetBMIHistoryQueryHandler
        : IRequestHandler<
            GetBMIHistoryQuery,
            List<PredictBMIResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetBMIHistoryQueryHandler(
            IApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor =
                httpContextAccessor;
        }

        public async Task<List<PredictBMIResponseDto>>
            Handle(
                GetBMIHistoryQuery request,
                CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirstValue(
                    ClaimTypes.NameIdentifier);

            var history = await _context.BMIRecords
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new PredictBMIResponseDto
                {
                    PredictedBMI = x.PredictedBMI,
                    BodyType = x.BodyType,
                    HealthStatus = x.HealthStatus
                })
                .ToListAsync(cancellationToken);

            return history;
        }
    }
}
