using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SmartFit.Application.Features.Profile.Commands.UpdateWeight
{
    public class UpdateWeightValidator : AbstractValidator<UpdateWeightCommand>
    {
        public UpdateWeightValidator()
        {
            RuleFor(x => x.Weight)
                .GreaterThan(20).WithMessage("Weight must be greater than 20 kg")
                .LessThan(300).WithMessage("Weight must be less than 300 kg");
        }
    }
}
