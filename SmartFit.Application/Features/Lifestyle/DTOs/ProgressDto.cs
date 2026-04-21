using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Lifestyle.DTOs
{
    public class ProgressDto
    {
        public int TotalTasks { get; set; }

        public int CompletedTasks { get; set; }

        public double ProgressPercentage { get; set; }
    }
}
