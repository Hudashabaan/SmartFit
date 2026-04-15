using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Domain.Enums;

namespace SmartFit.Application.Features.Goals.Commands.CreateGoal
{
  
        public class CreateGoalCommand : IRequest<Guid>
        {
            public double TargetWeight { get; set; }

            public int DurationInDays { get; set; }

            public GoalType Type { get; set; }
        }
    
}
