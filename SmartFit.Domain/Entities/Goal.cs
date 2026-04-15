using SmartFit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Domain.Entities
{
    public class Goal
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public double StartWeight { get; set; }

        public double TargetWeight { get; set; }

        public int DurationInDays { get; set; }

        public GoalType Type { get; set; }

        public DateTime StartDate { get; set; }

        public bool IsActive { get; set; }

        public void Update(double targetWeight, int durationInDays, GoalType type)
        {
            TargetWeight = targetWeight;
            DurationInDays = durationInDays;
            Type = type;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
