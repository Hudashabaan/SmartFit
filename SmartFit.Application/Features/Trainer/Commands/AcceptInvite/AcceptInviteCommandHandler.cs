using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    using SmartFit.Application.Common.Interfaces;
    using SmartFit.Domain.Entities;
    using SmartFit.Domain.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
   

    namespace SmartFit.Application.Features.Trainer.Commands.AcceptInvite
    {
        public class AcceptInviteCommandHandler
            : IRequestHandler<AcceptInviteCommand, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUser;

            public AcceptInviteCommandHandler(
                IApplicationDbContext context,
                ICurrentUserService currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<string> Handle(
                AcceptInviteCommand request,
                CancellationToken cancellationToken)
            {
                // 🔥 1. Get Invite
                var invite = await _context.TrainerInvites
                    .FirstOrDefaultAsync(x => x.Code == request.Dto.Code, cancellationToken);

                if (invite == null)
                    throw new Exception("Invalid invite code");

                // 🔥 2. Check if used
                if (invite.IsUsed)
                    throw new Exception("This invite is already used");

                // 🔥 3. Check expiry
                if (invite.ExpiryDate < DateTime.UtcNow)
                    throw new Exception("This invite has expired");

                // 🔥 4. Prevent self connection
                if (invite.TrainerId == _currentUser.UserId)
                    throw new Exception("You cannot accept your own invite");

                // 🔥 5. Check existing relation
                var exists = await _context.TrainerClientRelations
                    .AnyAsync(x =>
                        x.TrainerId == invite.TrainerId &&
                        x.ClientId == _currentUser.UserId,
                        cancellationToken);

                if (exists)
                    throw new Exception("Already connected to this trainer");

                // 🔥 6. Create relation
                var relation = new TrainerClientRelation
                {
                    Id = Guid.NewGuid(),
                    TrainerId = invite.TrainerId,
                    ClientId = _currentUser.UserId,
                    Status = RelationStatus.Accepted,
                    CreatedAt = DateTime.UtcNow
                };

                _context.TrainerClientRelations.Add(relation);

                // 🔥 7. Mark invite as used
                invite.IsUsed = true;

                // 🔥 8. Save changes
                await _context.SaveChangesAsync(cancellationToken);

                return "Connected successfully to trainer";
            }
        }
    }

