using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Trainer.DTOs;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Trainer.Queries.GetClientDetails
{
    public class GetClientDetailsQueryHandler
        : IRequestHandler<GetClientDetailsQuery, ClientDetailsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetClientDetailsQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<ClientDetailsDto> Handle(
            GetClientDetailsQuery request,
            CancellationToken cancellationToken)
        {
            // 🔥 1. Check Trainer
            if (_currentUser.Role != UserRole.Trainer)
                throw new Exception("Only trainers can view details");

            // 🔥 2. Check Relation
            var relationExists = await _context.TrainerClientRelations
                .AnyAsync(x =>
                    x.TrainerId == _currentUser.UserId &&
                    x.ClientId == request.ClientId &&
                    x.Status == RelationStatus.Accepted,
                    cancellationToken);

            if (!relationExists)
                throw new Exception("Not authorized to view this client");

            // 🔥 3. Get Client + Profile
            var client = await _context.Users
                .Include(x => x.Profile)
                .FirstOrDefaultAsync(x => x.Id == request.ClientId, cancellationToken);

            if (client == null)
                throw new Exception("Client not found");

            // 🔥 4. Get Last Body Analysis
            var body = await _context.BodyAnalyses
                .Where(x => x.UserId == request.ClientId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);

            // 🔥 5. Get Meals
            var meals = await _context.Meals
                .Where(x => x.UserId == request.ClientId)
                .OrderByDescending(x => x.Date)
                .Take(5)
                .Select(x => x.FoodName)
                .ToListAsync(cancellationToken);

            // 🔥 6. Lifestyle Tasks
            var tasks = await _context.Tasks
                .Where(x => x.UserId == request.ClientId)
                .ToListAsync(cancellationToken);

            // 🔥 7. Goal
            var goal = await _context.Goals
                .FirstOrDefaultAsync(x => x.UserId == request.ClientId, cancellationToken);

            // 🔥 8. Return DTO (بدون AI)
            return new ClientDetailsDto
            {
                ClientId = client.Id,
                FullName = client.FullName,
                Age = client.Profile.Age,
                Weight = client.Profile.Weight,
                Height = client.Profile.Height,
                ActivityLevel = client.Profile.ActivityLevel.ToString(),

                BMI = body?.BMI,
                BodyFatPercentage = body?.BodyFatPercentage,

                RecentMeals = meals,

                Goal = goal?.Type.ToString(),

                CompletedTasks = tasks.Count(x => x.IsCompleted),
                TotalTasks = tasks.Count()
            };
        }
    }
}