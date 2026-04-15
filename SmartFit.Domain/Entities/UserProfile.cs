using SmartFit.Domain.Enums;

namespace SmartFit.Domain.Entities
{
    public class UserProfile
    {
        public string UserId { get; private set; } = string.Empty;

        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;

        public int Age { get; private set; }
        public double Height { get; private set; }
        public double Weight { get; private set; }
        public Gender Gender { get; private set; }

        public ActivityLevel ActivityLevel { get; private set; }

        public string? BodyImageUrl { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // 🔥 Constructor لـ EF
        private UserProfile() { }

        // 🔥 Constructor الأساسي
        public UserProfile(string userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(
            int age,
            double height,
            double weight,
            Gender gender,
            ActivityLevel? activityLevel)
        {
            Age = age;
            Height = height;
            Weight = weight;
            Gender = gender;

            if (activityLevel.HasValue)
                ActivityLevel = activityLevel.Value;

            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateGender(Gender gender)
        {
            Gender = gender;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateAge(int age)
        {
            Age = age;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateHeight(double height)
        {
            Height = height;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateWeight(double weight)
        {
            Weight = weight;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}