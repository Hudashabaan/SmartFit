using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.DTOs
{
    public class GoalDto
    {
        public Guid Id { get; set; }

        public double StartWeight { get; set; }

        public double TargetWeight { get; set; }

        public int DurationInDays { get; set; }

        public string Type { get; set; }

        public DateTime StartDate { get; set; }

        public bool IsActive { get; set; }
    }
}
