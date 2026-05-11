using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartFit.Domain.Entities
{
    public class UserExerciseRecommendation
    {
        public Guid Id { get; set; }

        // =========================
        // User Info
        // =========================
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        // =========================
        // AI Recommendation Data
        // =========================

        // Recommended exercises from AI
        public string RecommendedExercises { get; set; } = null!;

        // Equipment suggested or selected
        public string RecommendedEquipment { get; set; } = null!;

        // AI explanation / reasoning
        public string RecommendationReason { get; set; } = null!;

        // =========================
        // Meta Data
        // =========================
        public DateTime RecommendedAt { get; set; } = DateTime.UtcNow;
    }
}
