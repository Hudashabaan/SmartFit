using FluentValidation;
using SmartFit.Application.Features.BodyAnalysisFeature.Commands.CreateBodyAnalysis;

public class CreateBodyAnalysisCommandValidator
    : AbstractValidator<CreateBodyAnalysisCommand>
{
    public CreateBodyAnalysisCommandValidator()
    {
        // 🟢 لازم يا صورة يا بيانات
        RuleFor(x => x)
            .Must(x => x.Image != null || (x.Height.HasValue && x.Weight.HasValue))
            .WithMessage("You must provide either an image or height & weight");

        // 🟡 لو مفيش صورة → ساعتها بس نvalidate البيانات
        When(x => x.Image == null, () =>
        {
            RuleFor(x => x.Height)
                .NotNull().WithMessage("Height is required")
                .GreaterThan(0).WithMessage("Height must be greater than 0");

            RuleFor(x => x.Weight)
                .NotNull().WithMessage("Weight is required")
                .GreaterThan(0).WithMessage("Weight must be greater than 0");

            RuleFor(x => x.Age)
                .GreaterThan(0)
                .When(x => x.Age.HasValue);

            RuleFor(x => x.Gender)
                .NotEmpty()
                .When(x => x.Gender != null);
        });
    }
}