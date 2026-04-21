using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Trainer.DTOs
{
 
        public class ClientDetailsDto
        {
            public string ClientId { get; set; }

            public string FullName { get; set; }

            public int Age { get; set; }

            public double Weight { get; set; }

            public double Height { get; set; }

            public string ActivityLevel { get; set; }

            public double? BMI { get; set; }

            public double? BodyFatPercentage { get; set; }

            public List<string> RecentMeals { get; set; }

            public string Goal { get; set; }

            public int CompletedTasks { get; set; }

            public int TotalTasks { get; set; }

            // 🤖 AI
            public string SuggestedFeedback { get; set; }
        }
    
}
