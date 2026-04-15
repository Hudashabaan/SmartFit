using MediatR;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Features.Meals.Commands.CreateMeal
{
    public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateMealCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Guid> Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {
            // 🛡️ حماية - التأكد إن المستخدم logged in
            var userId = _currentUser.UserId
                ?? throw new UnauthorizedAccessException("User not logged in");

            var meal = new Meal
            {
                Id = Guid.NewGuid(),
                FoodName = request.Meal.FoodName,
                Calories = request.Meal.Calories,
                Protein = request.Meal.Protein,
                Carbs = request.Meal.Carbs,
                Fat = request.Meal.Fat,
                Date = DateTime.UtcNow,
                UserId = userId // ✅ خلاص بقى Guid مش nullable
            };

            _context.Meals.Add(meal);

            await _context.SaveChangesAsync(cancellationToken);

            return meal.Id;
        }
    }
}