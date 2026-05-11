using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SmartFit.Domain.Entities
{
    public class BMIRecord
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }

        public float HeightCm { get; set; }

        public float WeightKg { get; set; }

        public double PredictedBMI { get; set; }

        public string BodyType { get; set; }

        public string HealthStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Property
        public ApplicationUser User { get; set; }
    }
}
