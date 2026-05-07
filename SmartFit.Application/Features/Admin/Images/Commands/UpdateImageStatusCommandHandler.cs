using MediatR;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Admin.Images.Commands.UpdateImageStatus
{
    public class UpdateImageStatusCommandHandler
        : IRequestHandler<UpdateImageStatusCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAdminLogService _logService;
        private readonly ICurrentUserService _currentUserService;

        public UpdateImageStatusCommandHandler(
            IApplicationDbContext context,
            IAdminLogService logService,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _logService = logService;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(
            UpdateImageStatusCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Source == "Body")
            {
                var entity = await _context.BodyAnalyses.FindAsync(request.Id);

                if (entity == null)
                    return false;

                entity.Status = request.Status;
            }
            else if (request.Source == "Food")
            {
                var entity = await _context.FoodAnalyses.FindAsync(request.Id);

                if (entity == null)
                    return false;

                entity.Status = request.Status;
            }
            else
            {
                return false;
            }

            await _context.SaveChangesAsync(cancellationToken);

            // 🔥 Save Admin Log
            await _logService.LogAsync(
                _currentUserService.UserId,
                $"{request.Status} Image",
                request.Source,
                request.Id.ToString()
            );

            return true;
        }
    }
}