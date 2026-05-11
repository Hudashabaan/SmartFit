using Microsoft.AspNetCore.Identity;
using SmartFit.Domain.Entities;
using SmartFit.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using SmartFit.Domain.Enums;

namespace SmartFit.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        public int Age { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public Gender Gender { get; set; }

        public bool HasHypertension { get; set; }

        public bool HasDiabetes { get; set; }

        public FitnessGoal FitnessGoal { get; set; }

        public FitnessType FitnessType { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public UserProfile? Profile { get; set; }

        public string? FcmToken { get; set; }
    }
}