using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;
using SmartFit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Trainer.Commands.AddFeedback
{
    public class AddFeedbackCommandHandler
    : IRequestHandler<AddFeedbackCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public AddFeedbackCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(
            AddFeedbackCommand request,
            CancellationToken cancellationToken)
        {
            // 🔥 1. Check Trainer
            if (_currentUser.Role != UserRole.Trainer)
                throw new Exception("Only trainers can send feedback");

            // 🔥 2. Check relation
            var relation = await _context.TrainerClientRelations
                .AnyAsync(x =>
                    x.TrainerId == _currentUser.UserId &&
                    x.ClientId == request.ClientId &&
                    x.Status == RelationStatus.Accepted,
                    cancellationToken);

            if (!relation)
                throw new Exception("Not authorized");

            // 🔥 3. Create feedback
            var feedback = new Feedback
            {
                Id = Guid.NewGuid(),
                TrainerId = _currentUser.UserId,
                ClientId = request.ClientId,
                Message = request.Message
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
