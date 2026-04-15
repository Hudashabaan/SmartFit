using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Features.Profile.Commands.UpdateWeight
{
    public class UpdateWeightCommandHandler
        : IRequestHandler<UpdateWeightCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public UpdateWeightCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
    UpdateWeightCommand request,
    CancellationToken cancellationToken)
        {
            // 🟢 1. تأكد إن فيه user
            if (_currentUser.UserId == null)
                throw new UnauthorizedAccessException("User not authenticated");

            var userId = _currentUser.UserId; // 🔥 الحل هنا

            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (profile == null)
            {
                profile = new UserProfile(userId, "", "");
                _context.UserProfiles.Add(profile);
            }

            profile.UpdateWeight(request.Weight);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
