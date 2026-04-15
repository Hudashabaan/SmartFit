using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SmartFit.Application.Features.Profile.Commands.UpdateAge
{
    public class UpdateAgeValidator : AbstractValidator<UpdateAgeCommand>
    {
        public UpdateAgeValidator()
        {
            RuleFor(x => x.Age)
                .GreaterThan(0).WithMessage("Age must be greater than 0")
                .LessThan(120).WithMessage("Age must be less than 120");
        }
    }
}
