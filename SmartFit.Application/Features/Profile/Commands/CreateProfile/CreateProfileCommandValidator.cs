using FluentValidation;

namespace SmartFit.Application.Features.Profile.Commands.CreateProfile
{
    public class CreateProfileCommandValidator
        : AbstractValidator<CreateProfileCommand>
    {
        public CreateProfileCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Age)
                .InclusiveBetween(10, 100);

            RuleFor(x => x.Height)
                .InclusiveBetween(100, 250)
                .WithMessage(
                    "Height must be between 100 and 250 cm");

            RuleFor(x => x.Weight)
                .InclusiveBetween(20, 300)
                .WithMessage(
                    "Weight must be between 20 and 300 kg");

            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithMessage(
                    "Invalid gender value");

            RuleFor(x => x.FitnessGoal)
                .IsInEnum();

            RuleFor(x => x.FitnessType)
                .IsInEnum();
        }
    }
}