using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartFit.Application.Features.Dashboard.DTOs
{
    public class LastBodyAnalysisDto
    {
        public double? BMI { get; set; }

        public double? BodyFat { get; set; }

        public string? Category { get; set; }

        public DateTime Date { get; set; }
    }
}
