using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Authentication.Commands.register
{
    public class RegisterCommandHandler
        : IRequestHandler<RegisterCommand, string>
    {
        private readonly IIdentityService _identityService;

        public RegisterCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<string> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            // Check password confirmation
            if (request.Password != request.ConfirmPassword)
                throw new Exception("Passwords do not match");

            // Check if user already exists
            var userExists = await _identityService.UserExistsAsync(request.Email);

            if (userExists)
                throw new Exception("User with this email already exists");

            try
            {
                // Register user through IdentityService
                var userId = await _identityService.RegisterAsync(
                    request.Email,
                    request.Password,
                    request.FullName
                );

                return userId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
                



        }
    }
}

