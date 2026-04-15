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

        public async Task<string> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            // 🟢 1. تأكد إن اليوزر موجود
            var userId = _currentUser.UserId
                ?? throw new Exception("User not authenticated");

            // 🟢 2. شوف هل فيه Profile قبل كده
            var existing = await _context.UserProfiles
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (existing != null)
                return existing.UserId;

            // 🟢 3. اعمل Profile جديد
            var profile = new UserProfile(
                userId,
                "",   // name
                ""    // email
            );

            // 🟢 4. Save
            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync(cancellationToken);

            return profile.UserId;
        }
    }
}