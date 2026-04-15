using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.BodyAnalysisFeature.DTOs
{
    public class BodyAnalysisDto
    {
        public double Height { get; set; }
        public double Weight { get; set; }

        public double? BMI { get; set; }
        public double BodyFatPercentage { get; set; }
        public double FatMass { get; set; }
        public double MuscleMass { get; set; }
        public double Waist { get; set; }

        public string BodyShape { get; set; }

        public double Confidence { get; set; }

        public string? ImageUrl { get; set; }
        public string Source { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
