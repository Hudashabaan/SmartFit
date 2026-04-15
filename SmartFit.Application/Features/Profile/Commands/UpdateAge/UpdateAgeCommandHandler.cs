using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Features.Profile.Commands.UpdateAge
    {
        public class UpdateAgeCommandHandler
            : IRequestHandler<UpdateAgeCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUser;

            public UpdateAgeCommandHandler(
                IApplicationDbContext context,
                ICurrentUserService currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

        public async Task<Unit> Handle(
UpdateAgeCommand request,
CancellationToken cancellationToken)
        {
            // 🟢 1. تأكد إن فيه user
            if (_currentUser.UserId == null)
                throw new UnauthorizedAccessException("User not authenticated");

            var userId = _currentUser.UserId;

            // 🟢 2. هات البروفايل
            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            // 🟢 3. لو مش موجود اعمله
            if (profile == null)
            {
                profile = new UserProfile(userId, "", "");
                _context.UserProfiles.Add(profile);
            }

            // 🟢 4. update
            profile.UpdateAge(request.Age);

            // 🟢 5. save
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
    }

