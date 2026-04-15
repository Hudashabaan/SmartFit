using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SmartFit.Application.Features.HealthScheduler.Commands.SnoozeSchedule
{
   

    public class SnoozeScheduleCommandValidator
        : AbstractValidator<SnoozeScheduleCommand>
    {
        public SnoozeScheduleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Schedule Id is required");

            RuleFor(x => x.Minutes)
                .GreaterThan(0).WithMessage("Minutes must be greater than 0")
                .LessThanOrEqualTo(1440) // يوم كامل
                .WithMessage("Minutes can't exceed 24 hours");
        }
    }
}
