using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;

namespace SmartFit.Application.Features.Admin.Users.Commands.DisableUser
{
    public class DisableUserCommandHandler : IRequestHandler<DisableUserCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdminLogService _logService;
        private readonly ICurrentUserService _currentUserService;

        public DisableUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            IAdminLogService logService,
            ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _logService = logService;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(
            DisableUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
                return false;

            user.IsActive = false;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return false;

            // 🔥 Save Log
            await _logService.LogAsync(
                _currentUserService.UserId,
                "Disable User",
                "User",
                user.Id
            );

            return true;
        }
    }
}