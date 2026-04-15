using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.DTOs;

namespace SmartFit.Application.Features.Profile.Queries.GetProfile
{
    public class GetProfileQueryHandler
        : IRequestHandler<GetProfileQuery, ProfileDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetProfileQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<ProfileDto> Handle(
           GetProfileQuery request,
           CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            var profile = await _context.UserProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (profile == null)
                throw new Exception("Profile not found");

            return new ProfileDto
            {
                Age = profile.Age,
                Height = profile.Height,
                Weight = profile.Weight,

                Gender = profile.Gender.ToString(),
                ActivityLevel = profile.ActivityLevel.ToString()
            };
        }
    }
}