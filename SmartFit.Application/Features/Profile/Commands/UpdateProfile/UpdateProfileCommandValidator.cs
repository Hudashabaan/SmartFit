using FluentValidation;

namespace SmartFit.Application.Features.Profile.Commands.UpdateProfile
{
    public class UpdateProfileCommandValidator
        : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
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
                .IsInEnum()
                .WithMessage(
                    "Invalid fitness goal");

            RuleFor(x => x.FitnessType)
                .IsInEnum()
                .WithMessage(
                    "Invalid fitness type");

            RuleFor(x => x.ProfilePictureUrl)
                .MaximumLength(500)
                .When(x =>
                    !string.IsNullOrWhiteSpace(
                        x.ProfilePictureUrl));
        }
    }
}