using Microsoft.AspNetCore.Identity;
using SmartFit.Domain.Entities;
using SmartFit.Domain.Enums;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? FcmToken { get; set; }

    public UserRole Role { get; set; } = UserRole.User;

    public UserProfile Profile { get; set; }

    public ICollection<Meal> Meals { get; set; }
}