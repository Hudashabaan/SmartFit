using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Meals.DTOs;

namespace SmartFit.Application.Features.Meals.Queries.Summary
{
    public class GetDailySummaryQueryHandler
        : IRequestHandler<GetDailySummaryQuery, DailySummaryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetDailySummaryQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<DailySummaryDto> Handle(
            GetDailySummaryQuery request,
            CancellationToken cancellationToken)
        {
            // 🔐 JWT
            var userId = _currentUser.UserId
                ?? throw new UnauthorizedAccessException("User not logged in");

            // 🔥 Optimization
            var start = request.Date.Date;
            var end = start.AddDays(1);

            var summary = await _context.Meals
                .Where(x => x.UserId == userId
                         && x.Date >= start
                         && x.Date < end)
                .GroupBy(x => 1)
                .Select(g => new DailySummaryDto
                {
                    TotalCalories = g.Sum(x => x.Calories),
                    TotalProtein = g.Sum(x => x.Protein),
                    TotalCarbs = g.Sum(x => x.Carbs),
                    TotalFat = g.Sum(x => x.Fat)
                })
                .FirstOrDefaultAsync(cancellationToken);

            return summary ?? new DailySummaryDto();
        }
    }
}