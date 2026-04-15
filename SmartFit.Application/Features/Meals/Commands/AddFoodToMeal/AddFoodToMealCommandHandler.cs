using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Meals.Commands.AddFoodToMeal.SmartFit.Application.Features.Meals.Commands.AddFoodToMeal;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Features.Meals.Commands.AddFoodToMeal
{
    public class AddFoodToMealCommandHandler
        : IRequestHandler<AddFoodToMealCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public AddFoodToMealCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Guid> Handle(
            AddFoodToMealCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId
                ?? throw new Exception("User not authenticated");

            var meal = new Meal
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                FoodName = request.FoodName,
                Calories = request.Calories,
                Protein = request.Protein,
                Carbs = request.Carbs,
                Fat = request.Fat,
                Date = DateTime.UtcNow
            };

            _context.Meals.Add(meal);
            await _context.SaveChangesAsync(cancellationToken);

            return meal.Id;
        }
    }
}
