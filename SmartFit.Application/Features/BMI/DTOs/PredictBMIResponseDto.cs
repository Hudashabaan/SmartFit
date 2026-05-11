using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.BMI.DTOs
{
    public class PredictBMIResponseDto
    {
        public double PredictedBMI { get; set; }

        public string BodyType { get; set; }

        public string HealthStatus { get; set; }
    }
}
