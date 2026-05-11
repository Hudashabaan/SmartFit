using FluentValidation;

namespace SmartFit.Application.Features
    .ExerciseRecommendations.Queries
    .GetExerciseRecommendation
{
    public class
        GetExerciseRecommendationQueryValidator
        : AbstractValidator<
            GetExerciseRecommendationQuery>
    {
        public
            GetExerciseRecommendationQueryValidator()
        {
            // =========================
            // Sex
            // =========================

            When(x => !string.IsNullOrWhiteSpace(x.Sex), () =>
            {
                RuleFor(x => x.Sex)
                    .Must(x =>
                        x == "Male" ||
                        x == "Female")
                    .WithMessage(
                        "Sex must be Male or Female");
            });

            // =========================
            // Age
            // =========================

            When(x => x.Age.HasValue, () =>
            {
                RuleFor(x => x.Age)
                    .InclusiveBetween(10, 100);
            });

            // =========================
            // Height
            // =========================

            When(x => x.Height.HasValue, () =>
            {
                RuleFor(x => x.Height)
                    .GreaterThan(0);
            });

            // =========================
            // Weight
            // =========================

            When(x => x.Weight.HasValue, () =>
            {
                RuleFor(x => x.Weight)
                    .GreaterThan(0);
            });

            // =========================
            // Hypertension
            // =========================

            When(x => !string.IsNullOrWhiteSpace(x.Hypertension), () =>
            {
                RuleFor(x => x.Hypertension)
                    .Must(x =>
                        x == "Yes" ||
                        x == "No")
                    .WithMessage(
                        "Hypertension must be Yes or No");
            });

            // =========================
            // Diabetes
            // =========================

            When(x => !string.IsNullOrWhiteSpace(x.Diabetes), () =>
            {
                RuleFor(x => x.Diabetes)
                    .Must(x =>
                        x == "Yes" ||
                        x == "No")
                    .WithMessage(
                        "Diabetes must be Yes or No");
            });

            // =========================
            // Fitness Goal
            // =========================

            When(x => !string.IsNullOrWhiteSpace(x.Fitness_Goal), () =>
            {
                RuleFor(x => x.Fitness_Goal)
                    .Must(x =>
                        x == "Weight Gain" ||
                        x == "Weight Loss")
                    .WithMessage(
                        "Invalid Fitness Goal");
            });

            // =========================
            // Fitness Type
            // =========================

            When(x => !string.IsNullOrWhiteSpace(x.Fitness_Type), () =>
            {
                RuleFor(x => x.Fitness_Type)
                    .Must(x =>
                        x == "Cardio Fitness" ||
                        x == "Muscular Fitness")
                    .WithMessage(
                        "Invalid Fitness Type");
            });

            // =========================
            // Equipment
            // =========================

            RuleFor(x => x.Equipment)
                .NotEmpty();
        }
    }
}