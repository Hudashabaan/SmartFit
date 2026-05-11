using SmartFit.Domain.Enums;

namespace SmartFit.Domain.Entities
{
    public class UserProfile
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;

        public string FullName { get; set; } = string.Empty;

        public int Age { get; set; }

        public float Height { get; set; }

        public float Weight { get; set; }

        public Gender Gender { get; set; }

        public bool HasHypertension { get; set; }

        public bool HasDiabetes { get; set; }

        public FitnessGoal FitnessGoal { get; set; }

        public FitnessType FitnessType { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}