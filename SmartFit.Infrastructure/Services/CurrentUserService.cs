using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Enums;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SmartFit.Application.Common.Interfaces;

namespace SmartFit.Infrastructure.Services
{
    public class CurrentUserService
        : ICurrentUserService
    {
        private readonly IHttpContextAccessor
            _httpContextAccessor;

        public CurrentUserService(
            IHttpContextAccessor
                httpContextAccessor)
        {
            _httpContextAccessor =
                httpContextAccessor;
        }

        public string UserId =>
            _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirstValue(
                    ClaimTypes.NameIdentifier)
            ?? string.Empty;
    }
}
