using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Goals.Commands.UpdateGoal
{
    public class UpdateGoalCommand : IRequest<Unit>
    {
        public double TargetWeight { get; set; }
        public int DurationInDays { get; set; }
        public GoalType Type { get; set; }
    }
}
