using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SmartFit.Application.Features.Goals.Commands.CreateGoal
{

    public class CreateGoalValidator : AbstractValidator<CreateGoalCommand>
    {
        public CreateGoalValidator()
        {
            RuleFor(x => x.TargetWeight)
                .GreaterThan(0);

            RuleFor(x => x.DurationInDays)
                .GreaterThan(0);
        }
    }
}
