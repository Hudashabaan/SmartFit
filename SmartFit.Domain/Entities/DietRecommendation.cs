namespace SmartFit.Domain.Entities
{
    public class DietRecommendation
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string RecommendedDiet { get; set; }

        public string Vegetables { get; set; }

        public string ProteinRecommendations { get; set; }

        public string JuiceRecommendations { get; set; }

        public string FitnessGoal { get; set; }

        public string FitnessType { get; set; }

        public double BMI { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation Property
        public ApplicationUser User { get; set; }
    }
}