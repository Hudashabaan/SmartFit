using System;

namespace SmartFit.Domain.Entities
{
    public class Meal
    {
        public Guid Id { get; set; }

        public string FoodName { get; set; } = string.Empty;

        public double Calories { get; set; }

        public double Protein { get; set; }

        public double Carbs { get; set; }

        public double Fat { get; set; }

        public DateTime Date { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 🔗 العلاقة مع اليوزر
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; }
    }
}
