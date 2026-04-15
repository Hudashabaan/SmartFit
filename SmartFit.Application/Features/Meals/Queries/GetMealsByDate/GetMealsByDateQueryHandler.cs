using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Meals.DTOs;

namespace SmartFit.Application.Features.Meals.Queries.GetMealsByDate
{
    public class GetMealsByDateQueryHandler
        : IRequestHandler<GetMealsByDateQuery, List<MealDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetMealsByDateQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<MealDto>> Handle(
            GetMealsByDateQuery request,
            CancellationToken cancellationToken)
        {
            // 🔐 JWT
            var userId = _currentUser.UserId
                ?? throw new UnauthorizedAccessException("User not logged in");

            // 🔥 Optimization (بدل Date.Date)
            var start = request.Date.Date;
            var end = start.AddDays(1);

            var meals = await _context.Meals
                .Where(x => x.UserId == userId
                         && x.Date >= start
                         && x.Date < end)
                .OrderByDescending(x => x.Date) // 🔥 ترتيب
                .Select(x => new MealDto
                {
                    FoodName = x.FoodName,
                    Calories = x.Calories,
                    Protein = x.Protein,
                    Carbs = x.Carbs,
                    Fat = x.Fat,
                    Date = x.Date
                })
                .ToListAsync(cancellationToken);

            return meals;
        }
    }
}