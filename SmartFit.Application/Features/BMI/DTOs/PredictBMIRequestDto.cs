using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartFit.Application.Features.BMI.DTOs
{
    public class PredictBMIRequestDto
    {
        public string Sex { get; set; }

        public float HeightCm { get; set; }

        public int Age { get; set; }

        public float WeightKg { get; set; }
    }
}
