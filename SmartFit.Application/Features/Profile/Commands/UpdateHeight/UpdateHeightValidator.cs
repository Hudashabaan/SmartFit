using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace SmartFit.Application.Features.Profile.Commands.UpdateHeight
{
    public class UpdateHeightValidator : AbstractValidator<UpdateHeightCommand>
    {
        public UpdateHeightValidator()
        {
            RuleFor(x => x.Height)
                .GreaterThan(50).WithMessage("Height must be greater than 50 cm")
                .LessThan(250).WithMessage("Height must be less than 250 cm");
        }
    }
}
