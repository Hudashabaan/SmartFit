using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Features.Profile.Commands.CreateProfile
{
    public class CreateProfileCommandHandler
        : IRequestHandler<CreateProfileCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateProfileCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<string> Handle(
            CreateProfileCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");

            var existingProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(
                    x => x.UserId == userId,
                    cancellationToken);

            if (existingProfile != null)
                throw new Exception("Profile already exists");

            var profile = new UserProfile
            {
                Id = Guid.NewGuid(),
                UserId = userId,

                FullName = request.FullName,
                Age = request.Age,
                Height = request.Height,
                Weight = request.Weight,

                Gender = request.Gender,

                HasHypertension = request.HasHypertension,
                HasDiabetes = request.HasDiabetes,

                FitnessGoal = request.FitnessGoal,
                FitnessType = request.FitnessType,

                ProfilePictureUrl = request.ProfilePictureUrl,

                CreatedAt = DateTime.UtcNow
            };

            _context.UserProfiles.Add(profile);

            await _context.SaveChangesAsync(cancellationToken);

            return "Profile created successfully";
        }
    }
}