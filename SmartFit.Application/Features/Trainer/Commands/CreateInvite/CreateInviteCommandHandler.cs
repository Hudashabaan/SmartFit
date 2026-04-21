using MediatR;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Trainer.Commands.CreateInvite
{
    public class CreateInviteCommandHandler
        : IRequestHandler<CreateInviteCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateInviteCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<string> Handle(
            CreateInviteCommand request,
            CancellationToken cancellationToken)
        {
            // 🔥 1. Check إن المستخدم Trainer
            if (_currentUser.Role != UserRole.Trainer)
                throw new Exception("Only trainers can create invites");

            // 🔥 2. Generate Code
            var code = Guid.NewGuid()
                .ToString("N")
                .Substring(0, 8)
                .ToUpper();

            // 🔥 3. Create Invite
            var invite = new TrainerInvite
            {
                Id = Guid.NewGuid(),
                Code = code,
                TrainerId = _currentUser.UserId,
                ExpiryDate = DateTime.UtcNow.AddDays(request.Dto.ExpiryDays),
                IsUsed = false
            };

            // 🔥 4. Save
            _context.TrainerInvites.Add(invite);
            await _context.SaveChangesAsync(cancellationToken);

            return code;
        }
    }
}