using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Profile.Commands.UpdateProfile
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.Age)
                .GreaterThan(0).WithMessage("Age must be greater than 0");

            RuleFor(x => x.Height)
                .GreaterThan(0).WithMessage("Height must be greater than 0");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0");

            RuleFor(x => x.Gender)
                .IsInEnum();

            RuleFor(x => x.ActivityLevel)
     .IsInEnum()
     .When(x => x.ActivityLevel.HasValue);

           
        }
    }
}
