using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Profile.DTOs
{
    public class ProfileDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;

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

        public DateTime CreatedAt { get; set; }
    }
}