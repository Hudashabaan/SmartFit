using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SmartFit.Application.Features.Dashboard.DTOs
{
    public class LastFoodAnalysisDto
    {
        public string FoodName { get; set; } = string.Empty;

        public double Calories { get; set; }

        public double Protein { get; set; }

        public double Carbs { get; set; }

        public double Fat { get; set; }

        public double Confidence { get; set; }

        public DateTime Date { get; set; }
    }
}
