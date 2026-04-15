using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartFit.Domain.Enums;

namespace SmartFit.Domain.Entities
{


	public class BodyAnalysis
	{
		public Guid Id { get; set; }

		public string UserId { get; set; }

		public double Height { get; set; }
		public double Weight { get; set; }

        public double? BMI { get; set; }
        public double BodyFatPercentage { get; set; }
		public double FatMass { get; set; }
		public double MuscleMass { get; set; }
		public double Waist { get; set; }

		public BodyShape BodyShape { get; set; }
        public AnalysisSource Source { get; set; }

        public double Confidence { get; set; }

		public string? ImageUrl { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}
