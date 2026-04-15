using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SmartFit.Application.Features.FoodAnalysisFeature.Commands.AnalyzeFood
{ 
    public class AnalyzeFoodCommandValidator : AbstractValidator<AnalyzeFoodCommand>
    {
        public AnalyzeFoodCommandValidator()
        {
            RuleFor(x => x.Image)
                .NotNull().WithMessage("Image is required")
                .Must(file => file.Length > 0).WithMessage("Image cannot be empty")
                .Must(file => file.ContentType.StartsWith("image/"))
                .WithMessage("File must be an image");

            RuleFor(x => x.Image.Length)
                .LessThanOrEqualTo(5 * 1024 * 1024) // 5MB
                .WithMessage("Image size must be less than 5MB");


            RuleFor(x => x.Image.ContentType)
            .Must(type => type == "image/jpeg" || type == "image/png")
            .WithMessage("Only JPG or PNG allowed");

        }
    }
}
