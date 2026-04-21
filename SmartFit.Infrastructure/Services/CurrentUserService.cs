using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Enums;

namespace SmartFit.Infrastructure.Services
{ 
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId =>
      _httpContextAccessor.HttpContext?.User?
      .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public UserRole Role => throw new NotImplementedException();
    }
}
