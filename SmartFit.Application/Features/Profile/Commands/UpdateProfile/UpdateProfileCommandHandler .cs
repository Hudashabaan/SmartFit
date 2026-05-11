using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;

namespace SmartFit.Application.Features.Profile.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler
        : IRequestHandler<UpdateProfileCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public UpdateProfileCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<string> Handle(
            UpdateProfileCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");

            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(
                    x => x.UserId == userId,
                    cancellationToken);

            if (profile == null)
                throw new Exception("Profile not found");

            profile.FullName = request.FullName;
            profile.Age = request.Age;
            profile.Height = request.Height;
            profile.Weight = request.Weight;

            profile.Gender = request.Gender;

            profile.HasHypertension = request.HasHypertension;
            profile.HasDiabetes = request.HasDiabetes;

            profile.FitnessGoal = request.FitnessGoal;
            profile.FitnessType = request.FitnessType;

            profile.ProfilePictureUrl = request.ProfilePictureUrl;

            await _context.SaveChangesAsync(cancellationToken);

            return "Profile updated successfully";
        }
    }
}