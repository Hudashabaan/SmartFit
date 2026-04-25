using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Trainer.Commands.RemoveClient
{
    public class RemoveClientCommandHandler
    : IRequestHandler<RemoveClientCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public RemoveClientCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(
            RemoveClientCommand request,
            CancellationToken cancellationToken)
        {
            // 🔥 1. Check Trainer
            if (_currentUser.Role != UserRole.Trainer)
                throw new Exception("Only trainers can remove clients");

            // 🔥 2. Get Relation
            var relation = await _context.TrainerClientRelations
                .FirstOrDefaultAsync(x =>
                    x.TrainerId == _currentUser.UserId &&
                    x.ClientId == request.ClientId,
                    cancellationToken);

            if (relation == null)
                throw new Exception("Relation not found");

            // 🔥 3. Update Status
            relation.Status = RelationStatus.Removed;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
