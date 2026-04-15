using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SmartFit.Application.Features.HealthScheduler.Commands.CreateSchedule
{
   

    public class CreateScheduleCommandValidator : AbstractValidator<CreateScheduleCommand>
    {
        public CreateScheduleCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100);

            RuleFor(x => x.Time)
                .GreaterThan(DateTime.Now)
                .WithMessage("Time must be in the future")
                .Must(BeValidTimeInterval)
                .WithMessage("Time must be in 5-minute intervals (e.g., 10:00, 10:05)");

            RuleFor(x => x.Type)
                .IsInEnum();

            RuleFor(x => x.Repeat)
                .IsInEnum();
        }

        private bool BeValidTimeInterval(DateTime time)
        {
            return time.Minute % 5 == 0;
        }
    }
}
