using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;

namespace SmartFit.Application.Features.Meals.Commands.Delete_Meal
{
    public class DeleteMealCommandHandler : IRequestHandler<DeleteMealCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public DeleteMealCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId
                ?? throw new UnauthorizedAccessException("User not logged in");

            var meal = await _context.Meals
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (meal == null)
                throw new Exception("Meal not found");

            // 🔐 Security
            if (meal.UserId != userId)
                throw new UnauthorizedAccessException("You can't delete this meal");

            _context.Meals.Remove(meal);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
