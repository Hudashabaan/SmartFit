using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SmartFit.Application.Features.HealthScheduler.Commands.MarkScheduleAsDone
{ 
    public class MarkScheduleAsDoneCommandValidator
        : AbstractValidator<MarkScheduleAsDoneCommand>
    {
        public MarkScheduleAsDoneCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Schedule Id is required");
        }
    }
}
